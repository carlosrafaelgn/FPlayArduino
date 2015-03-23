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

// We will use a LCD display to show song information
#include <LiquidCrystal.h>
LiquidCrystal lcd(12, 11, 5, 4, 3, 2);

int lastTime, lastVolumeTime;
byte btnPrevious, btnPlayPause, btnNext, lastVolume;

void setup()
{
  // Set up the LCD's number of columns and rows
  lcd.begin(16, 2);
  
  // Initialize the serial port used by the library
  FPlay.begin(9600);
  
  // Prepare the buttons to control the player
  btnPrevious = 0;
  pinMode(8, INPUT);
  
  btnPlayPause = 0;
  pinMode(7, INPUT);
  
  btnNext = 0;
  pinMode(6, INPUT);
  
  // Force the volume to be sent at least once
  lastVolume = 255;
  
  // Control the interval between updates
  lastTime = millis();
  
  // Control the interval between volume changes
  lastVolumeTime = lastTime;
}

void loop()
{
  // This line could go into serialEvent() if you wish to do so...
  FPlay.process();
  
  int now = millis();
  
  // Check if we have received detailed information from the player
  // (The information returned by songPosition() and songLength() is
  // always available, but hasNewStateArrived() returns true ONLY when
  // this information first arrives)
  if (FPlay.hasNewStateArrived())
  {
    // Display song's length and current position (in seconds)
    lcd.setCursor(0, 0);
    if (FPlay.songPosition() < 0)
    {
      // Invalid position
      lcd.print("-");
    }
    else
    {
      lcd.print(FPlay.songPosition() / 1000);
    }
    lcd.print("    ");
    
    lcd.setCursor(0, 1);
    if (FPlay.songLength() < 0)
    {
      // Invalid length
      lcd.print("-");
    }
    else
    {
      lcd.print(FPlay.songLength() / 1000);
    }
    lcd.print("    ");
    
    lastTime = now;
  }
  else
  {
    // Force the information to be updated every 250ms
    if ((now - lastTime) >= 250)
    {
      lastTime = now;
      FPlay.updateState();
    }
  }
  
  byte btn;
  
  // Check the three buttons that control the player
  btn = digitalRead(8);
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
  
  btn = digitalRead(7);
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
  
  btn = digitalRead(6);
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
  
  // Check the volume potentiometer 10 times per second (every 100ms)
  if ((now - lastVolumeTime) >= 100)
  {
    lastVolumeTime = now;

    // Reuse this variable to store the potentiometer's value
    btn = analogRead(0) / 10;
    if (btn > 100)
    {
      btn = 100;
    }
    
    // Send the command *only* if the volume has changed!
    if (btn != lastVolume)
    {
      lastVolume = btn;
      FPlay.setVolume(lastVolume);
    }
  }
}
