FPlayArduino
============

Library to control the Android music player FPlay using an Arduino :)

FPlay communicates with Arduino using Bluetooth SPP - Serial Port Profile.

FPlay is available on GitHub at https://github.com/carlosrafaelgn/FPlayAndroid and on Google Play at https://play.google.com/store/apps/details?id=br.com.carlosrafaelgn.fplay

Control Methods
===============

FPlayArduino consists of one object, `FPlay`, and several methods. The most common methods, control-related, are described below.

`FPlay.begin(speed)`
--------------------

Initializes all internal values and starts the serial port used by the library, using the given speed. For example: `FPlay.begin(9600)`

Note: If you own an Arduino Mega or other board with more than one serial port, you can choose which serial port the library will use by placing a macro before including the header. Here is an example of how to use `Serial2` in Arduino Mega:

```c++
#define FPlaySerial Serial1
#include <FPlayArduino.h>
...
```

`FPlay.process()`
-----------------

Processes all data received from the player (This *MUST* be placed somewhere inside `loop()`, and should be called as many times as possible to prevent data loss).

Example:

```c++
void loop() {
  FPlay.process();
  ...
}
```

`FPlay.isLoading()`
-------------------

Returns `true` or `false` to indicate whether the player is in the process of loading a song.

Note: this information is only available after `hasNewStateArrived()` has returned `true` at least once (see below).

`FPlay.isPlaying()`
-------------------

Returns `true` or `false` to indicate whether the player is currently playing a song or is paused.

Note: this information is only available after `hasNewStateArrived()` has returned `true` at least once (see below).

`FPlay.songPosition()`
----------------------

Returns a value indicating the playback position of the current song in milliseconds, or `-1` if the song is still loading or if the playback position is unknown.

Note: this information is only available after `hasNewStateArrived()` has returned `true` at least once (see below).

`FPlay.songLength()`
-------------------

Returns a value indicating the length of the current song in milliseconds, or `-1` if the song is still loading or if its length is unknown.

Note: this information is only available after `hasNewStateArrived()` has returned `true` at least once (see below).

`FPlay.volume()`
----------------

Returns a value from `0` to `100` indicating the player's current volume level.

Note: this information is only available after `hasNewStateArrived()` has returned `true` at least once (see below).

`FPlay.setVolume(volume)`
-------------------------

Changes the player's current volume level. `volume` must be a value from `0` to `100`.

`FPlay.increaseVolume()`
-------------------------

Increases the volume level by one unit (this could be more than 1%).

`FPlay.decreaseVolume()`
-------------------------

Decreases the volume level by one unit (this could be more than 1%).

`FPlay.play()`
--------------

Starts playing the current song (nothing happens if the player was already playing).

`FPlay.pause()`
--------------

Pauses the playback (nothing happens if the player was already paused).

`FPlay.playPause()`
-------------------

Either starts or pauses the playback, depending on the current state of the player.

`FPlay.previous()`
-------------------

Skips to the previous song in the current playlist.

`FPlay.next()`
--------------

Skips to the next song in the current playlist.

`FPlay.updateState()`
---------------------

Requests the player to send fresh information with its internal state (whether it is playing/loading or not, current volume level, current song's position and length).

The player already sends this information automatically in key moments, like when one song ends and another starts, for example. However, if you want to keep track of current song's position, you should manually call this method (for this purpose, a recommended interval between successive calls is 250 ms).

`FPlay.hasNewStateArrived()`
----------------------------

Returns `true` the first time it is called after the player has sent its internal state, and returns `false` all other times. In other words, when `hasNewStateArrived()` returns `true` means `isPlaying()`, `isLoading()`, `volume()`, `songPosition()` and `songLength()` have had their values updated with fresh information.

You can always call the methods mentioned here before and they will always return valid values. The point is, these values are most of the time old (not perfectly synchronized with the player actual values). The values are only up-to-date when `hasNewStateArrived()` returns `true`.

Controlling and Receiving Frequency Bins
========================================

FPlay is also able to send frequency-domain data in realtime, suitable for creating visualizers. This data consists of a series of bytes, each one indicating the amplitude of one or more frequencies (the frequency bins).

In order to receive such information you must first inform how many bins you would like to receive, using the `FPlayBinCount` macro. This *MUST* be placed before including the header:

```c++
#define FPlayBinCount 16
#include <FPlayArduino.h>
...
```

Unlike the `FPlaySerial` macro, `FPlayBinCount` *IS NOT* optional and *MUST* always be specified. Valid values are either `0`, meaning you do not wish to receive any frequency bins, or `4`, `8`, `16`, `32`, `64`, `128` and `256`.

You can control when the player starts and stops sending the bins either manually, by using the controls provided on the player's screen, or via code, by calling the methods `FPlay.startFrequencyBinsTransmission()` and `FPlay.stopFrequencyBinsTransmission()`.

You can check when the bins are received by calling `FPlay.haveNewFrequencyBinsArrived()`. This method behaves just like `hasNewStateArrived()`, and will return `true` only for the first time it is called after the bins have been received (even though bin data is always available).

In order to improve performance, and reduce memory consumption, all bin data can be obtained accessing their array directly. For example:

```c++
...
analogWrite(5, FPlay.bins[0]);
analogWrite(6, FPlay.bins[1]);
analogWrite(9, FPlay.bins[2]);
analogWrite(10, FPlay.bins[3]);
...
```

Each element in the array is one `byte`. Therefore, its value ranges from `0` to `255`.

Bin Frenquecies
---------------

Each bin contains the amplitude of one or more frequencies. FPlay always works with 256 frequency bins internally and groups those frequencies as needed before sending them.

If you request 256 bins, these are the frequencies whose amplitudes are represented in each bin (0 Hz to 11025 Hz linearly distributed over 256 values):
- `FPlay.bin[0]`: DC (0 Hz)
- `FPlay.bin[1]`: 43 Hz
- `FPlay.bin[2]`: 86 Hz
- `FPlay.bin[3]`: 129 Hz
- ...
- `FPlay.bin[254]`: 10982 Hz
- `FPlay.bin[255]`: 11025 Hz

When you request less than 256 bins, the player will group more than one amplitude into one bin using different techniques, according to the situation. This grouping does not follow a linear scale, though. Instead, it uses an almost-logarithmic scale.

Note: For more information regarding this grouping/mapping, please, refer to the source code of the function `commonProcess()` in the file [Common.h](https://github.com/carlosrafaelgn/FPlayAndroid/blob/master/jni/Common.h).

Special / Rarely Used Methods
=============================

`FPlay.begin(speed, config)`
-------------

Does the same initialization `begin(speed)` does, but calls `begin(speed, config)` on the serial port.

`FPlay.end()`
-------------

Terminates the serial port used by the library by calling `end()` on the serial port.

Examples
========

Here you can find the layout for the examples that come with this library. The layouts were created using [Fritzing](http://fritzing.org/) and can be found [here, in the Fritzing folder](https://github.com/carlosrafaelgn/FPlayArduino/tree/master/Fritzing).

The source code for the examples is in the library ([here](https://github.com/carlosrafaelgn/FPlayArduino/tree/master/FPlayArduino/examples)) and are not being shown here to keep this file better organized ;)

SimpleControl
-------------

In this example you will use three buttons to control the player's functions previous, playPause and next.

![Layout for the example Simple Control](https://raw.githubusercontent.com/carlosrafaelgn/FPlayArduino/master/Fritzing/SimpleControl.png "Layout for the example Simple Control")

SimpleControlAndFeedback
------------------------

In this example you will use three buttons to control the player's functions previous, playPause and next. Also, there are three LEDs indicating the state of the player (whether it is playing or paused and if it is still loading a song).

![Layout for the example Simple Control And Feedback](https://raw.githubusercontent.com/carlosrafaelgn/FPlayArduino/master/Fritzing/SimpleControlAndFeedback.png "Layout for the example Simple Control And Feedback")

ExtendedFeedback
----------------

In this example you will use three buttons to control the player's functions previous, playPause and next, and you will use a potentiometer to change the player's volume. Also, there is a LCD displaying both the position and length of the current song (in seconds).

![Layout for the example Extended Feedback](https://raw.githubusercontent.com/carlosrafaelgn/FPlayArduino/master/Fritzing/ExtendedFeedback.png "Layout for the example Extended Feedback")

SimpleControlAndBins
--------------------

In this example you will use three buttons to control the player's functions previous, playPause and next. Also, there are four LEDs "pulsing" along with the amplitude of four frequency bins.

![Layout for the example Simple Control And Bins](https://raw.githubusercontent.com/carlosrafaelgn/FPlayArduino/master/Fritzing/SimpleControlAndBins.png "Layout for the example Simple Control And Bins")

----

This projected is licensed under the terms of the FreeBSD License. See LICENSE.txt for more details.
