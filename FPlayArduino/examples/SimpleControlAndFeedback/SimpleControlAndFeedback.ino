//******************************************************************
// These two configurations MUST be done before including
// FPlayArduino.h

// We do not wish to receive frequency data from the player
#define FPlayBinCount 0

// If we were using Arduino Mega, or another Arduino with more
// serial ports, we could specify which one to use here:
// #define FPlaySerial Serial1
//******************************************************************

#include <FPlayArduino.h>

#define RED_LED 5
#define YELLOW_LED 6
#define GREEN_LED 7

byte btnPrevious, btnPlayPause, btnNext;

void setup()
{
  // Initialize the serial port used by the library
  FPlay.begin(9600);
  
  // Prepare the buttons to control the player
  btnPrevious = 0;
  pinMode(2, INPUT);
  
  btnPlayPause = 0;
  pinMode(3, INPUT);
  
  btnNext = 0;
  pinMode(4, INPUT);
  
  // Add three led's to display the player's state
  pinMode(RED_LED, OUTPUT);
  pinMode(YELLOW_LED, OUTPUT);
  pinMode(GREEN_LED, OUTPUT);
}

void loop()
{
  // This line could go into serialEvent() if you wish to do so...
  FPlay.process();
  
  // Check if we have received detailed information from the player
  // (The information returned by isPlaying() and isLoading() is always 
  // available, but hasNewStateArrived() returns true ONLY when this
  // information first arrives)
  if (FPlay.hasNewStateArrived())
  {
    // Turn the led's on/off accordingly
    if (FPlay.isPlaying())
    {
      digitalWrite(RED_LED, 0);
      digitalWrite(YELLOW_LED, 0);
      digitalWrite(GREEN_LED, 1);
    }
    else if (FPlay.isLoading())
    {
      digitalWrite(RED_LED, 0);
      digitalWrite(YELLOW_LED, 1);
      digitalWrite(GREEN_LED, 0);
    }
    else
    {
      digitalWrite(RED_LED, 1);
      digitalWrite(YELLOW_LED, 0);
      digitalWrite(GREEN_LED, 0);
    }
  }
  
  byte btn;
  
  // Check the three buttons that control the player
  btn = digitalRead(2);
  if (btn != btnPrevious)
  {
    if (btn != 0)
    {
      // Skip to the previous song
      FPlay.previous();
    }
    btnPrevious = btn;
    // Simple debounce for the button
    delay(100);
  }
  
  btn = digitalRead(3);
  if (btn != btnPlayPause)
  {
    if (btn != 0)
    {
      // Either play or pause the player
      FPlay.playPause();
    }
    btnPlayPause = btn;
    // Simple debounce for the button
    delay(100);
  }
  
  btn = digitalRead(4);
  if (btn != btnNext)
  {
    if (btn != 0)
    {
      // Skip to the next song
      FPlay.next();
    }
    btnNext = btn;
    // Simple debounce for the button
    delay(100);
  }
}
