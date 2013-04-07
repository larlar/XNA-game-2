using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ludo
{
    class Music
    {
        //Song related variables
        int song;
        Song[] songList = new Song[3];
        float songVolume;
        float songVolumeReserve;

        //Keeps check of music being muted or not
        bool isMuted;
        bool muteChanged; 

        //Keeps track of song playtime and duration
        TimeSpan time;
        TimeSpan songDuration;

        KeyboardState key;
        KeyboardState previousKey;

        MouseState mouse;
        MouseState previousMouse;
        Rectangle mousePos;
        Rectangle prevMousePos;


        //Font for the writing
        SpriteFont soundMusic;
        MediaState media;

        //Media player buttons
        Texture2D playInactive;
        Texture2D playActive;
        Texture2D stopInactive;
        Texture2D stopActive;
        Texture2D pauseInactive;
        Texture2D pauseActive;
        Texture2D muteInactive;
        Texture2D muteActive;
        Texture2D volumeUp;
        Texture2D volumeDown;
        Texture2D prev;
        Texture2D next;
        Texture2D volumeBG;
        Texture2D titleBG;
        Texture2D durationBG;

        Rectangle[] buttons = new Rectangle[11];

        public Music(){}

        public void LoadContent(ContentManager content)
        {
            songList[0] = content.Load<Song>("Staff Credits");
            songList[1] = content.Load<Song>("ClockTown - Day1");
            songList[2] = content.Load<Song>("Hyrule Field Main Theme");

            soundMusic = content.Load<SpriteFont>("Sound_Music");

            isMuted = false;
            muteChanged = false;

            MediaPlayer.Stop();
            media = MediaPlayer.State;

            mousePos = new Rectangle(0,0,1,1);
            prevMousePos = new Rectangle(0, 0, 1, 1);
            playInactive = content.Load<Texture2D>("play-button-inactive");
            playActive = content.Load<Texture2D>("play-button-active");
            stopInactive = content.Load<Texture2D>("stop-button-inactive");
            stopActive = content.Load<Texture2D>("stop-button-active");
            pauseInactive = content.Load<Texture2D>("pause-button-inactive");
            pauseActive = content.Load<Texture2D>("pause-button-active");
            muteInactive = content.Load<Texture2D>("mute-button-inactive");
            muteActive = content.Load<Texture2D>("mute-button-active");
            volumeUp = content.Load<Texture2D>("volume-button-up");
            volumeDown = content.Load<Texture2D>("volume-button-down");
            prev = content.Load<Texture2D>("previous-button");
            next = content.Load<Texture2D>("next-button");
            volumeBG = content.Load<Texture2D>("volume-field");
            titleBG = content.Load<Texture2D>("song-title-field");
            durationBG = content.Load<Texture2D>("duration-field");
        }

        public void update(GameTime gameTime)
        {
            media = MediaPlayer.State;

            muteChanged = isMuted;

            //Preventing multiple presses in one click
            previousKey = key;
            key = Keyboard.GetState();

            //Preventing multiple presses in one click
            previousMouse = mouse;
            mouse = Mouse.GetState();

            mousePos.X = mouse.X;
            mousePos.Y = mouse.Y;
            
            playSong();
            pauseSong();
            stopSong();
            adjustVolume();
            muteSong();
            unMuteSong();

            song = songChosen();

            time = MediaPlayer.PlayPosition;
            songDuration = songList[song].Duration;
            songVolume = MediaPlayer.Volume * 10;
        }


        public void playSong()
        {
            if ((key.IsKeyDown(Keys.P) && previousKey.IsKeyUp(Keys.P) && MediaPlayer.State != MediaState.Playing && MediaPlayer.State != MediaState.Paused &&
                MediaPlayer.State == media) || 
                (mousePos.Intersects(buttons[0]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released
                && MediaPlayer.State != MediaState.Playing && MediaPlayer.State != MediaState.Paused && MediaPlayer.State == media))
            {
                Console.WriteLine("play song");
                MediaPlayer.Play(songList[song]);
            }
            //allows to resume if paused
            if (media == MediaState.Paused && (mousePos.Intersects(buttons[0]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released))
                MediaPlayer.Resume();                
        }

        public void pauseSong()
        {
            if ((key.IsKeyDown(Keys.P) && previousKey.IsKeyUp(Keys.P) && MediaPlayer.State == MediaState.Playing && MediaPlayer.State == media) ||
                (mousePos.Intersects(buttons[1]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released
                && MediaPlayer.State == MediaState.Playing && MediaPlayer.State == media))
            {
                Console.WriteLine("pause");
                MediaPlayer.Pause();
            }
        }

        public void stopSong()
        {
            if (key.IsKeyDown(Keys.S) || 
                (mousePos.Intersects(buttons[2]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released))
            {
                Console.WriteLine("stop song");
                MediaPlayer.Stop();
            }
        }

        public void adjustVolume()
        {
            if ((key.IsKeyDown(Keys.Subtract) && previousKey.IsKeyUp(Keys.Subtract)) ||
                (mousePos.Intersects(buttons[6]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released))
            {
                MediaPlayer.Volume -= 0.1f;
            }

            if ((key.IsKeyDown(Keys.Add) && previousKey.IsKeyUp(Keys.Add)) ||
                (mousePos.Intersects(buttons[5]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released))
            {
                MediaPlayer.Volume += 0.1f;
                isMuted = false;
            }
        }

        public void muteSong()
        {
            if ((key.IsKeyDown(Keys.M) && previousKey.IsKeyUp(Keys.M) && isMuted == false && muteChanged == isMuted) ||
                (mousePos.Intersects(buttons[3]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released && isMuted == false && muteChanged == isMuted))
            {
                Console.WriteLine("mute");
                songVolumeReserve = songVolume/10f;
                MediaPlayer.Volume = 0.0f;
                isMuted = true;
            }
        }

        public void unMuteSong()
        {
            if ((key.IsKeyDown(Keys.M) && previousKey.IsKeyUp(Keys.M) && isMuted == true && muteChanged == isMuted) ||
                (mousePos.Intersects(buttons[3]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released && isMuted == true && muteChanged == isMuted))
            {
                MediaPlayer.Volume = songVolumeReserve;
                isMuted = false;
            }
        }

        public int songChosen()
        {
            int notChanged = song;
            if ((key.IsKeyDown(Keys.RightShift) && previousKey.IsKeyUp(Keys.RightShift)) ||
                (mousePos.Intersects(buttons[8]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released))
            {
                if (song == (songList.Length - 1))
                    song = 0;
                else
                    song += 1;
            }

            if ((key.IsKeyDown(Keys.LeftShift) && previousKey.IsKeyUp(Keys.LeftShift)) ||
                (mousePos.Intersects(buttons[7]) && mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released))
            {
                if (song == 0)
                    song = (songList.Length - 1);
                else
                    song -= 1;
            }

            if (song != notChanged)
                MediaPlayer.Play(songList[song]);
            return song;
        }

        public string progressbar(TimeSpan time)
        {
            int minutes = time.Minutes;
            int seconds = time.Seconds;

            if (seconds < 10)
                return minutes + ":0" + seconds;
            else
                return minutes + ":" + seconds;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            buttons[0] = new Rectangle(screenWidth - 260, 45, 31, 31); //play (260, 45)
            buttons[1] = new Rectangle(screenWidth - 230, 45, 31, 31); //pause (230,45)
            buttons[2] = new Rectangle(screenWidth - 200, 45, 31, 31); //stop
            buttons[3] = new Rectangle(screenWidth - 170, 45, 31, 31); //mute
            buttons[4] = new Rectangle(screenWidth - 140, 45, 80, 31); //volumeBG
            buttons[5] = new Rectangle(screenWidth - 61, 45, 31, 31); //volumeUp
            buttons[6] = new Rectangle(screenWidth - 31, 45, 31, 31); //volumeDown
            buttons[7] = new Rectangle(screenWidth - 260, 75, 121, 16); //previousSong
            buttons[8] = new Rectangle(screenWidth - 140, 75, 140, 16); //nextSong
            buttons[9] = new Rectangle(screenWidth - 260, 0, 200, 46); //songtitleBG
            buttons[10] = new Rectangle(screenWidth - 61, 0, 61, 46); //durationBG   

            //PLAY BUTTON [0]
            if (media == MediaState.Playing)
                spriteBatch.Draw(playActive, buttons[0], Color.White);
            else
                spriteBatch.Draw(playInactive, buttons[0], Color.White);

            //PAUSE BUTTON [1]
            if (media != MediaState.Paused)
                spriteBatch.Draw(pauseInactive, buttons[1], Color.White);
            else
                spriteBatch.Draw(pauseActive, buttons[1], Color.White);

            //STOP BUTTON [2]
            if (media != MediaState.Stopped)
                spriteBatch.Draw(stopInactive, buttons[2], Color.White);
            else
                spriteBatch.Draw(stopActive, buttons[2], Color.White);

            //MUTE BUTTON [3]
            if (isMuted)
                spriteBatch.Draw(muteActive, buttons[3], Color.White);    
            else
                spriteBatch.Draw(muteInactive, buttons[3], Color.White);

            //VOLUME BACKGROUND [4]
            spriteBatch.Draw(volumeBG, buttons[4], Color.White);

            //VOLUME UP [5]
            spriteBatch.Draw(volumeUp, buttons[5], Color.White);

            //VOLUME DOWN [6]
            spriteBatch.Draw(volumeDown, buttons[6], Color.White);

            //PREVIOUS SONG [7]
            spriteBatch.Draw(prev, buttons[7], Color.White);

            //NEXT SONG [8]
            spriteBatch.Draw(next, buttons[8], Color.White);

            //SONG TITLE BACKGROUND[9]
            spriteBatch.Draw(titleBG, buttons[9], Color.White);

            //SONG DURATION BACKGROUND[10]
            spriteBatch.Draw(durationBG, buttons[10], Color.White);

            //Drawing name of song
            spriteBatch.DrawString(soundMusic,
                songList[song].Name, new Vector2(screenWidth - 250, 22), Color.Black);

            //Drawing how long the song has played, and duration
            spriteBatch.DrawString(soundMusic,
                progressbar(time) + "/\n" + progressbar(songDuration),
                new Vector2(screenWidth - 50, 15), Color.Black);

            //Drawing volume for the media player
            spriteBatch.DrawString(soundMusic,
                "Vol:" + Math.Round(songVolume) + "/" + "10",
                new Vector2(screenWidth - 130, 50), Color.Black);


        }

    }
}