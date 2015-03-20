//******************************************************************
// These two configurations MUST be done before including
// FPlayArduino.h

// We want to receive 4 frequency bins from the player
#define FPlayBinCount 4

// If we were using Arduino Mega, or another Arduino with more
// serial ports, we could specify which one to use here:
// #define FPlaySerial Serial1
//******************************************************************

#include <FPlayArduino.h>

#define LED1 5
#define LED2 6
#define LED3 9
#define LED4 10

byte btnPrevious, btnPlayPause, btnNext;

void setup()
{
  // Initialize the serial port used by the library (use a high baud rate
  // to make sure data will arrive with some spare time)
  FPlay.begin(115200);
  
  // Prepare the buttons to control the player
  btnPrevious = 0;
  pinMode(2, INPUT);
  
  btnPlayPause = 0;
  pinMode(3, INPUT);
  
  btnNext = 0;
  pinMode(4, INPUT);
  
  // You can request the player to start sending the frequency bins
  // either in code, or manually, using the menu on the player
  FPlay.startSpectrum();
}

void loop()
{
  // This line could go into serialEvent() if you wish to do so...
  FPlay.process();
  
  // Check if we have received the frequency bins from the player
  // (The information in bins is always available, but hasNewSpectrumArrived()
  // returns true ONLY when this information first arrives)
  if (FPlay.hasNewSpectrumArrived())
  {
    // FPlay.bin[x] contains the amplitude for the xth bin,
	// starting at 0. Each amplitude ranges from 0 to 255.
    analogWrite(LED1, FPlay.bin[0]);
    analogWrite(LED2, FPlay.bin[1]);
    analogWrite(LED3, FPlay.bin[2]);
    analogWrite(LED4, FPlay.bin[3]);
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
