//
// FPlayArduino is distributed under the FreeBSD License
//
// Copyright (c) 2015, Carlos Rafael Gimenes das Neves
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// The views and conclusions contained in the software and documentation are those
// of the authors and should not be interpreted as representing official policies,
// either expressed or implied, of the FreeBSD Project.
//
// https://github.com/carlosrafaelgn/FPlayArduino
//

// Before using this library, please, define the macro FPlayBinCount to indicate
// if you intend to receive frequency data from the player. Valid values for the
// macro are:
// - 0 (meaning you do not desire to receive frequency data)
// - 4, 8, 16, 32, 64, 128 or 256 (to receive the specified amount of bins)
//
// For exemple:
// #define FPlayBinCount 16

// Also, please, define which serial the library should use, using the FPlaySerial
// macro. If nothing is defined the library assumes the default: Serial
//
// For example:
// #define FPlaySerial Serial1

// Message Format
// | 01h Start of Heading
// | XXh Message type [20h - FFh]
// | YYh Payload length (bits 0 -  6 left shifted by 1) [00h - FEh]
// | ZZh Payload length (bits 7 - 13 left shifted by 1) [00h - FEh]
// |   Payload bytes
// | 04h End of Transmission
//
// Payload Length refers only to the length of the payload itself,
// which includes neither the four-byte header nor the last EoT byte
//
// Bytes 01h and 1Bh (Escape) must be escaped within the payload,
// using the following scheme:
// 1Bh WWh
// Where WWh = OriginalByte XOR 01h
// Therefore byte 01h must be transmitted as the sequence 1Bh 00h,
// whereas byte 1Bh must be transmitted as the sequence 1Bh 1Ah.
// All other escape sequences are undefined.
//
// Byte 04h (End of Transmission) can be freely transmitted within
// the payload, as it cannot be used to prematurely terminate a message.
// It is only used so that its absence at the end of a message invalidates
// the message.

#ifndef FPlayArduino_h
#define FPlayArduino_h

#include <Arduino.h>
#include <inttypes.h>

#ifndef FPlayBinCount
#error ("Please, define FPlayBinCount before using this library!")
#elif ((FPlayBinCount != 0) && (FPlayBinCount != 4) && (FPlayBinCount != 8) && (FPlayBinCount != 16) && (FPlayBinCount != 32) && (FPlayBinCount != 64) && (FPlayBinCount != 128) && (FPlayBinCount != 256))
#error ("FPlayBinCount must be either 0, 4, 8, 16, 32, 64, 128 or 256!")
#endif

#ifndef FPlaySerial
#define FPlaySerial Serial
#endif

#ifndef FPlayMaxBytesAtATime
#define FPlayMaxBytesAtATime 16
#endif

#define fs FPlaySerial

#define FlagState 0x07
#define FlagEscape 0x08
#define FlagBinsArrived 0x10
#define FlagPlayerStateArrived 0x20

#define StartOfHeading 0x01
#define Escape 0x1B
#define EndOfTransmission 0x04

#define MessageBins0 0x00 //not actually transmitted
#define MessageBins4 0x20
#define MessageBins8 0x21
#define MessageBins16 0x22
#define MessageBins32 0x23
#define MessageBins64 0x24
#define MessageBins128 0x25
#define MessageBins256 0x26
#define MessageStartBinTransmission 0x30
#define PayloadBins4 MessageBins4
#define PayloadBins8 MessageBins8
#define PayloadBins16 MessageBins16
#define PayloadBins32 MessageBins32
#define PayloadBins64 MessageBins64
#define PayloadBins128 MessageBins128
#define PayloadBins256 MessageBins256
#define MessageStopBinTransmission 0x31
#define MessagePlayerCommand 0x32
#define PayloadPlayerCommandUpdateState 0x00
#define PayloadPlayerCommandPrevious 0x58
#define PayloadPlayerCommandPlayPause 0x55
#define PayloadPlayerCommandNext 0x57
#define PayloadPlayerCommandPlay 0x7E
#define PayloadPlayerCommandPause 0x56
#define PayloadPlayerCommandIncreaseVolume 0x18
#define PayloadPlayerCommandDecreaseVolume 0x19
#define PayloadPlayerCommandSetVolume 0xD1
#define MessagePlayerState 0x33
#define PayloadPlayerStateFlagPlaying 0x01
#define PayloadPlayerStateFlagLoading 0x02

#if (FPlayBinCount == 256)
#define MessageBins MessageBins256
#elif (FPlayBinCount == 128)
#define MessageBins MessageBins128
#elif (FPlayBinCount == 64)
#define MessageBins MessageBins64
#elif (FPlayBinCount == 32)
#define MessageBins MessageBins32
#elif (FPlayBinCount == 16)
#define MessageBins MessageBins16
#elif (FPlayBinCount == 8)
#define MessageBins MessageBins8
#elif (FPlayBinCount == 4)
#define MessageBins MessageBins4
#else
#define MessageBins MessageBins0
#endif

class _FPlay {
private:
  struct _PlayerState {
    uint8_t state;
    uint8_t volume;
    int32_t songPosition;
    int32_t songLength;
  } __attribute__ ((packed));

  static uint8_t state, currentMessage, payloadIndex;
  static _PlayerState playerState;
  static uint16_t payloadLength;

  static void sendEmptyMessage(uint8_t msg) {
    fs.write(StartOfHeading);
    fs.write(msg);
    // (payload length << 0) = (0 << 1) = 0
    fs.write(0);
    fs.write(0);
    fs.write(EndOfTransmission);
  }

  // For the sake of simplicity, payload MUST be a byte
  // that does not need to be escaped
  static void sendOneByteMessage(uint8_t msg, uint8_t payload) {
    fs.write(StartOfHeading);
    fs.write(msg);
    // (payload length << 1) = (1 << 1) = 2
    fs.write(2);
    fs.write(0);
    fs.write(payload);
    fs.write(EndOfTransmission);
  }

  // For the sake of simplicity, payload MUST two bytes
  // that does not need to be escaped
  static void sendTwoByteMessage(uint8_t msg, uint8_t payload0, uint8_t payload1) {
    fs.write(StartOfHeading);
    fs.write(msg);
    // (payload length << 1) = (2 << 1) = 4
    fs.write(4);
    fs.write(0);
    fs.write(payload0);
    fs.write(payload1);
    fs.write(EndOfTransmission);
  }

  static void resetState() {
    playerState.state = 0;
    playerState.songPosition = -1;
    playerState.songLength = -1;
#if (FPlayBinCount != 0)
    // Nice trick for the case when FPlayBinCount = 256 ;)
    state = (uint8_t)(FPlayBinCount);
    do {
      state--;
      bins[state] = 0;
    } while (state);
#else
    state = 0;
#endif
  }

public:
  static uint8_t bins[FPlayBinCount];

  inline static void begin(uint32_t baud) {
    resetState();
    fs.begin(baud);
    // Wait for serial port to connect (needed for Leonardo only)
    while (!fs) {
      ;
    }
  }

  inline static void begin(uint32_t baud, uint8_t config) {
    resetState();
    fs.begin(baud, config);
    // Wait for serial port to connect (needed for Leonardo only)
    while (!fs) {
      ;
    }
  }

  inline static void end() {
    resetState();
    fs.end();
  }

  inline static bool isPlaying() {
    return ((playerState.state & PayloadPlayerStateFlagPlaying) ? true : false);
  }

  inline static bool isLoading() {
    return ((playerState.state & PayloadPlayerStateFlagLoading) ? true : false);
  }

  inline static uint8_t volume() {
    return playerState.volume;
  }

  inline static int32_t songPosition() {
    return playerState.songPosition;
  }

  inline static int32_t songLength() {
    return playerState.songLength;
  }

  inline static void startFrequencyBinsTransmission() {
#if (FPlayBinCount != 0)
    sendOneByteMessage(MessageStartBinTransmission, MessageBins);
#endif
  }

  inline static void stopFrequencyBinsTransmission() {
#if (FPlayBinCount != 0)
    sendEmptyMessage(MessageStopBinTransmission);
#endif
  }

  inline static void updateState() {
    sendOneByteMessage(MessagePlayerCommand, PayloadPlayerCommandUpdateState);
  }

  inline static void previous() {
    sendOneByteMessage(MessagePlayerCommand, PayloadPlayerCommandPrevious);
  }

  inline static void playPause() {
    sendOneByteMessage(MessagePlayerCommand, PayloadPlayerCommandPlayPause);
  }

  inline static void next() {
    sendOneByteMessage(MessagePlayerCommand, PayloadPlayerCommandNext);
  }

  inline static void play() {
    sendOneByteMessage(MessagePlayerCommand, PayloadPlayerCommandPlay);
  }

  inline static void pause() {
    sendOneByteMessage(MessagePlayerCommand, PayloadPlayerCommandPause);
  }

  inline static void increaseVolume() {
    sendOneByteMessage(MessagePlayerCommand, PayloadPlayerCommandIncreaseVolume);
  }

  inline static void decreaseVolume() {
    sendOneByteMessage(MessagePlayerCommand, PayloadPlayerCommandDecreaseVolume);
  }

  inline static void setVolume(byte volume) {
    sendTwoByteMessage(MessagePlayerCommand, PayloadPlayerCommandSetVolume, (volume >= 100) ? 200 : (volume << 1));
  }

  inline static bool hasNewStateArrived() {
    if ((state & FlagPlayerStateArrived)) {
      // Clear the flag so hasNewStateArrived() returns
      // true only once after receiving the player's state
      state &= (~FlagPlayerStateArrived);
      return true;
    }
    return false;
  }

  inline static bool haveNewFrequencyBinsArrived() {
#if (FPlayBinCount != 0)
    if ((state & FlagBinsArrived)) {
      // Clear the flag so haveNewFrequencyBinsArrived() returns true
      // only once after receiving the song's frequency bins
      state &= (~FlagBinsArrived);
      return true;
    }
#endif
    return false;
  }

  static void process() {
    uint8_t count = FPlayMaxBytesAtATime;
    while (fs.available() && count) {
      count--;
      uint8_t data = fs.read();
      if (data == StartOfHeading) {
        // Restart the state machine
        state &= (~(FlagEscape | FlagState));
        continue;
      }
      switch ((state & FlagState)) {
      case 0:
        // This byte should be the message type
#if (FPlayBinCount != 0)
        if (data == MessageBins || data == MessagePlayerState) {
#else
        if (data == MessagePlayerState) {
#endif
          // Take the state machine to its next state
          currentMessage = data;
          state++;
        } else {
          // Take the state machine to its error state
          state |= FlagState;
        }
        continue;
      case 1:
        // This should be payload length's first byte
        // (bits 0 - 6 left shifted by 1)
        if ((data & 0x01)) {
          // Take the state machine to its error state
          state |= FlagState;
        } else {
          payloadLength = (uint16_t)(data >> 1);
          // Take the state machine to its next state
          state++;
        }
        continue;
      case 2:
        // This should be payload length's second byte
        // (bits 7 - 13 left shifted by 1)
        if ((data & 0x01)) {
          // Take the state machine to its error state
          state |= FlagState;
        } else {
          payloadLength |= ((uint16_t)data << 6);

          // Sanity check
          if (!payloadLength) {
            // Take the state machine to its error state
            state |= FlagState;
            continue;
          }
          if (currentMessage == MessagePlayerState) {
            if (payloadLength > 18) {
              // Take the state machine to its error state
              state |= FlagState;
              continue;
            }
          } else {
#if (FPlayBinCount != 0)
            if (payloadLength > (FPlayBinCount * 2)) {
              // Take the state machine to its error state
              state |= FlagState;
              continue;
            }
#endif
          }
          // Take the state machine to its next state
          payloadIndex = 0;
          state++;
        }
        continue;
      case 3:
        // We are receiving the payload
        payloadLength--;

        if (data == Escape) {
          // Wait for the next byte before proceeding
          state |= FlagEscape;
          continue;
        } else if ((state & FlagEscape)) {
          // If the previous byte was Escape, XOR this byte with
          // 0x01 before actually using it
          state &= (~FlagEscape);
          data ^= 0x01;
        }

        if (currentMessage == MessagePlayerState) {
#ifdef FPlayBigEndian
          // All Arduinos I know are little endian, but you can
          // define FPlayBigEndian to make it work otherwise...
          // Maybe some other time... ;)
#else
          ((uint8_t*)&playerState)[payloadIndex++] = data;
#endif
          if (payloadIndex == 10) {
            if (!payloadLength) {
              // Take the state machine to its next state
              state++;
            } else {
              // Something went wrong...
              // Take the state machine to its error state
              state |= FlagState;
            }
          }
        } else {
#if (FPlayBinCount != 0)
          bins[payloadIndex++] = data;

          if (payloadIndex == FPlayBinCount) {
            if (!payloadLength) {
              // Take the state machine to its next state
              state++;
            } else {
              // Something went wrong...
              // Take the state machine to its error state
              state |= FlagState;
            }
          }
#endif
        }
        continue;
      case 4:
        // Take the state machine to its error state
        state |= FlagState;

        // Sanity check: data should be EoT
        if (data == EndOfTransmission) {
          if (currentMessage == MessagePlayerState) {
            state |= FlagPlayerStateArrived;
          } else {
#if (FPlayBinCount != 0)
            state |= FlagBinsArrived;
#endif
          }
        }
      }
    }
  }
};

uint8_t _FPlay::state, _FPlay::currentMessage, _FPlay::payloadIndex;
_FPlay::_PlayerState _FPlay::playerState;
uint16_t _FPlay::payloadLength;
uint8_t _FPlay::bins[FPlayBinCount];

_FPlay FPlay;

#undef fs
#undef FlagState
#undef FlagEscape
#undef FlagBins
#undef FlagPlayerState
#undef StartOfHeading
#undef Escape
#undef EndOfTransmission
#undef MessageBins0
#undef MessageBins4
#undef MessageBins8
#undef MessageBins16
#undef MessageBins32
#undef MessageBins64
#undef MessageBins128
#undef MessageBins256
#undef MessageStartBinTransmission
#undef PayloadBins4
#undef PayloadBins8
#undef PayloadBins16
#undef PayloadBins32
#undef PayloadBins64
#undef PayloadBins128
#undef PayloadBins256
#undef MessageStopBinTransmission
#undef MessagePlayerCommand
#undef PayloadPlayerCommandUpdateState
#undef PayloadPlayerCommandPrevious
#undef PayloadPlayerCommandPlayPause
#undef PayloadPlayerCommandNext
#undef PayloadPlayerCommandPlay
#undef PayloadPlayerCommandPause
#undef PayloadPlayerCommandIncreaseVolume
#undef PayloadPlayerCommandDecreaseVolume
#undef PayloadPlayerCommandSetVolume
#undef MessagePlayerState
#undef PayloadPlayerStateFlagPlaying
#undef PayloadPlayerStateFlagLoading
#undef MessageBins

#endif
