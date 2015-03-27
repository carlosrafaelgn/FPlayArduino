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
-------------------

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

Requests the player


- `FPlay.end()`: Terminates the serial port used by the library (this is rarely used

----

This projected is licensed under the terms of the FreeBSD License. See LICENSE.txt for more details.
