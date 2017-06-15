using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace ProtocolTest {
	public class FPlayProtocol {
		private const byte StartOfHeading = 0x01;
		private const byte Escape = 0x1B;
		private const byte EndOfTransmission = 0x04;

		private const byte MessageBins4 = 0x20;
		private const byte MessageBins8 = 0x21;
		private const byte MessageBins16 = 0x22;
		private const byte MessageBins32 = 0x23;
		private const byte MessageBins64 = 0x24;
		private const byte MessageBins128 = 0x25;
		private const byte MessageBins256 = 0x26;
		private const byte MessageStartBinTransmission = 0x30;
		private const byte PayloadBins4 = MessageBins4;
		private const byte PayloadBins8 = MessageBins8;
		private const byte PayloadBins16 = MessageBins16;
		private const byte PayloadBins32 = MessageBins32;
		private const byte PayloadBins64 = MessageBins64;
		private const byte PayloadBins128 = MessageBins128;
		private const byte PayloadBins256 = MessageBins256;
		private const byte MessageStopBinTransmission = 0x31;
		private const byte MessagePlayerCommand = 0x32;
		private const byte PayloadPlayerCommandUpdateState = 0x00;
		private const byte PayloadPlayerCommandPrevious = 0x58;
		private const byte PayloadPlayerCommandPlayPause = 0x55;
		private const byte PayloadPlayerCommandNext = 0x57;
		private const byte PayloadPlayerCommandPlay = 0x7E;
		private const byte PayloadPlayerCommandPause = 0x56;
		private const byte PayloadPlayerCommandIncreaseVolume = 0x18;
		private const byte PayloadPlayerCommandDecreaseVolume = 0x19;
		private const byte PayloadPlayerCommandSetVolume = 0xD1;
		private const byte MessagePlayerState = 0x33;
		private const byte PayloadPlayerStateFlagPlaying = 0x01;
		private const byte PayloadPlayerStateFlagLoading = 0x02;

		private bool StateEscape;
		private int State, PayloadLength, PayloadReceivedLength, PayloadActualLength;

		public bool GraphActive;
		public readonly byte[] ReceiveBuffer = new byte[512];
		private readonly byte[] SendBuffer = new byte[512];
		public readonly StringBuilder Console = new StringBuilder(2048 + 256);

		public void Reset() {
			StateEscape = false;
			State = 0;
			PayloadLength = 0;
			PayloadReceivedLength = 0;
			PayloadActualLength = 0;
			Console.Clear();
		}

		public int DataReceived(SerialPort port) {
			int tot = port.BytesToRead;

			if (GraphActive) {
				while (tot > 0) {
					int b = port.ReadByte();
					if (b == 1) {
						State = 1;
					} else if (State != 0) {
						switch (State) {
							case 1:
								State = ((b >= MessageBins4 && b <= MessageBins256) ? 2 : 0);
								break;
							case 2:
								PayloadLength = b >> 1;
								State = 3;
								break;
							case 3:
								PayloadLength |= (b << 6);
								State = ((PayloadLength <= ReceiveBuffer.Length) ? 4 : 0);
								PayloadReceivedLength = 0;
								PayloadActualLength = 0;
								break;
							case 4:
								PayloadReceivedLength++;
								if (PayloadReceivedLength >= PayloadLength)
									State = 5;
								if (b == 0x1b) {
									StateEscape = true;
									break;
								}
								if (StateEscape) {
									StateEscape = false;
									b ^= 1;
								}
								ReceiveBuffer[PayloadActualLength++] = (byte)b;
								break;
							case 5:
								State = 0;
								if (b == 4)
									return PayloadActualLength;
								break;
						}
					}
					tot--;
				}
			} else {
				if (Console.Length >= 2048)
					Console.Clear();
				while (tot > 0) {
					int b = port.ReadByte();
					if (b == 1)
						Console.AppendLine();
					Console.AppendFormat("{0:x2} ", b);
					tot--;
				}
				return -1;
			}
			return 0;
		}

		private void SendEmptyMessage(SerialPort port, byte msg) {
			SendBuffer[0] = StartOfHeading;
			SendBuffer[1] = msg;
			// (payload length << 0) = (0 << 1) = 0
			SendBuffer[2] = 0;
			SendBuffer[3] = 0;
			SendBuffer[4] = EndOfTransmission;

			port.Write(SendBuffer, 0, 5);
		}

		// For the sake of simplicity, payload MUST be a byte
		// that does not need to be escaped
		private void SendOneByteMessage(SerialPort port, byte msg, byte payload) {
			SendBuffer[0] = StartOfHeading;
			SendBuffer[1] = msg;
			// (payload length << 1) = (1 << 1) = 2
			SendBuffer[2] = 2;
			SendBuffer[3] = 0;
			SendBuffer[4] = payload;
			SendBuffer[5] = EndOfTransmission;

			port.Write(SendBuffer, 0, 6);
		}

		// For the sake of simplicity, payload MUST two bytes
		// that does not need to be escaped
		private void SendTwoByteMessage(SerialPort port, byte msg, byte payload0, byte payload1) {
			SendBuffer[0] = StartOfHeading;
			SendBuffer[1] = msg;
			// (payload length << 1) = (2 << 1) = 4
			SendBuffer[2] = 4;
			SendBuffer[3] = 0;
			SendBuffer[4] = payload0;
			SendBuffer[5] = payload1;
			SendBuffer[6] = EndOfTransmission;

			port.Write(SendBuffer, 0, 7);
		}

		public void StartBinTransmission(SerialPort port) {
			SendOneByteMessage(port, MessageStartBinTransmission, MessageBins16);
		}

		public void StopBinTransmission(SerialPort port) {
			SendEmptyMessage(port, MessageStopBinTransmission);
		}

		public void Previous(SerialPort port) {
			SendOneByteMessage(port, MessagePlayerCommand, PayloadPlayerCommandPrevious);
		}

		public void PlayPause(SerialPort port) {
			SendOneByteMessage(port, MessagePlayerCommand, PayloadPlayerCommandPlayPause);
		}

		public void Next(SerialPort port) {
			SendOneByteMessage(port, MessagePlayerCommand, PayloadPlayerCommandNext);
		}

		public void Play(SerialPort port) {
			SendOneByteMessage(port, MessagePlayerCommand, PayloadPlayerCommandPlay);
		}

		public void Pause(SerialPort port) {
			SendOneByteMessage(port, MessagePlayerCommand, PayloadPlayerCommandPause);
		}

		public void IncreaseVolume(SerialPort port) {
			SendOneByteMessage(port, MessagePlayerCommand, PayloadPlayerCommandIncreaseVolume);
		}

		public void DecreaseVolume(SerialPort port) {
			SendOneByteMessage(port, MessagePlayerCommand, PayloadPlayerCommandDecreaseVolume);
		}
	}
}
