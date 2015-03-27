FPlayArduino
============

Library to control the Android music player FPlay using an Arduino :)

FPlay communicates with Arduino using Bluetooth SPP - Serial Port Profile.

FPlay is available on GitHub at https://github.com/carlosrafaelgn/FPlayAndroid and on Google Play at https://play.google.com/store/apps/details?id=br.com.carlosrafaelgn.fplay

Common Methods
==============

FPlayArduino consists of one object, `FPlay`, and several methods. The most common methods are described below.

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

The player already sends this information automatically in key moments, like when one song ends and another starts, for example. However, if you want to keep track of current song's position, you should manually call this method (for this purpose, a recommended interval between successive calls is 250ms).

`FPlay.hasNewStateArrived()`
----------------------------

Returns `true` the first time it is called after the player has sent its internal state, and returns `false` all other times. In other words, when `hasNewStateArrived()` returns `true` means `isPlaying()`, `isLoading()`, `volume()`, `songPosition()` and `songLength()` have had their values updated with fresh information.

You can always call the methods mentioned here before and they will always return valid values. The point is, these values are most of the time old (not perfectly synchronized with the player actual values). The values are only up-to-date when `hasNewStateArrived()` returns `true`.

Special/Rarely used methods
===========================

`FPlay.begin(speed, config)`
-------------

Does the same initialization `begin(speed)` does, but calls `begin(speed, config)` on the serial port.

`FPlay.end()`
-------------

Terminates the serial port used by the library by calling `end()` on the serial port.

----

This projected is licensed under the terms of the FreeBSD License. See LICENSE.txt for more details.
