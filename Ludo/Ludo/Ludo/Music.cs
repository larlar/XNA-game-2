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

        KeyboardState keyboard;
        KeyboardState previousKey;

        MouseState mouse;
        MouseState previousMouse;
        Rectangle mousePos;
        Rectangle prevMousePos;

        //Font for the writing
        SpriteFont soundMusic;

        MediaState media;

        //Media player buttons
        Texture2D play;
        Texture2D stop;
        Texture2D resume;
        Texture2D pause;
        Texture2D mute;
        Texture2D unMute;
        Texture2D volumeUp;
        Texture2D volumeDown;
        Texture2D prev;
        Texture2D next;

        Rectangle[] buttons = new Rectangle[8];

        public Music()
        {

        }

        public void LoadContent(ContentManager content)
        {
            songList[0] = content.Load<Song>("130_Staff_Credits_Redux_ZREO");
            songList[1] = content.Load<Song>("1-08_Clock_Town_-_Day_1_ZREO");
            songList[2] = content.Load<Song>("19_Hyrule_Field_Main_Theme_ZREO");

            soundMusic = content.Load<SpriteFont>("Sound_Music");

            isMuted = false;
            muteChanged = false;

            MediaPlayer.Stop();
            media = MediaPlayer.State;

            mousePos = new Rectangle(0,0,20,20);
            prevMousePos = new Rectangle(0, 0, 20, 20);

            play = content.Load<Texture2D>("Play");
            stop = content.Load<Texture2D>("Stop");
            resume = content.Load<Texture2D>("Resume");
            pause = content.Load<Texture2D>("Pause");
            mute = content.Load<Texture2D>("Mute");
            unMute = content.Load<Texture2D>("UnMute");
            volumeUp = content.Load<Texture2D>("VolumeUp");
            volumeDown = content.Load<Texture2D>("VolumeDown");
            prev = content.Load<Texture2D>("Prev");
            next = content.Load<Texture2D>("Next");

            buttons[0] = new Rectangle(1200, 300, 200, 50);
            buttons[1] = new Rectangle(1200, 400, 200, 50);
            buttons[2] = new Rectangle(1200, 500, 200, 50);
            buttons[3] = new Rectangle(1200, 600, 200, 50);
            

            buttons[4] = new Rectangle(1175, 700, 50, 50);
            buttons[5] = new Rectangle(1250, 700, 50, 50);
            buttons[6] = new Rectangle(1325, 700, 50, 50);
            buttons[7] = new Rectangle(1400, 700, 50, 50);
        }

        public void update(GameTime gameTime)
        {
            media = MediaPlayer.State;

            muteChanged = isMuted;

            //Preventing multiple presses in one click
            previousKey = keyboard;
            keyboard = Keyboard.GetState();

            //Preventing multiple presses in one click
            previousMouse = mouse;
            mouse = Mouse.GetState();

            mousePos.X = mouse.X;
            mousePos.Y = mouse.Y;

            //Checking status of media player, the arguments arent really neccesarry, but added for flexsibility (example: using methods outside music.cs)
            playSong(keyboard, previousKey, mouse, previousMouse, mousePos, buttons[0], songList[song]);
            stopSong(keyboard, mouse, previousMouse, mousePos, buttons[1]);
            pauseSong(keyboard, previousKey, mouse, previousMouse, mousePos, buttons[2]);
            resumeSong(keyboard, previousKey, mouse, previousMouse, mousePos, buttons[2]);
            adjustVolume(previousKey, keyboard, mouse, previousMouse, mousePos, buttons);
            muteSong(keyboard, previousKey, mouse, previousMouse, mousePos, buttons[3]);
            unMuteSong(keyboard, previousKey, mouse, previousMouse, mousePos, buttons[3]);

            song = songChosen(previousKey, keyboard, songList, mouse, previousMouse, mousePos, buttons);

            time = MediaPlayer.PlayPosition;
            songDuration = songList[song].Duration;
            songVolume = MediaPlayer.Volume * 10;
        }


        public void playSong(KeyboardState key, KeyboardState previousKey, MouseState mouse, MouseState prevMouse, Rectangle mousePos, Rectangle buttonPos, Song music)
        {
            if ((key.IsKeyDown(Keys.P) && previousKey.IsKeyUp(Keys.P) && MediaPlayer.State != MediaState.Playing && MediaPlayer.State != MediaState.Paused &&
                MediaPlayer.State == media) || 
                (mousePos.Intersects(buttonPos) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released
                && MediaPlayer.State != MediaState.Playing && MediaPlayer.State != MediaState.Paused && MediaPlayer.State == media))
            {
                MediaPlayer.Play(music);
            }
        }

        public void stopSong(KeyboardState key, MouseState mouse, MouseState prevMouse, Rectangle mousePos, Rectangle buttonPos)
        {
            if (key.IsKeyDown(Keys.S) || 
                (mousePos.Intersects(buttonPos) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released))
            {
                MediaPlayer.Stop();
            }
        }

        public void pauseSong(KeyboardState key, KeyboardState previousKey, MouseState mouse, MouseState prevMouse, Rectangle mousePos, Rectangle buttonPos)
        {
            if ((key.IsKeyDown(Keys.P) && previousKey.IsKeyUp(Keys.P) && MediaPlayer.State == MediaState.Playing && MediaPlayer.State == media) ||
                (mousePos.Intersects(buttonPos) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released
                && MediaPlayer.State == MediaState.Playing && MediaPlayer.State == media))
            {
                MediaPlayer.Pause();
            }
        }

        public void resumeSong(KeyboardState key, KeyboardState previousKey, MouseState mouse, MouseState prevMouse, Rectangle mousePos, Rectangle buttonPos)
        {
            if ((key.IsKeyDown(Keys.P) && previousKey.IsKeyUp(Keys.P) && MediaPlayer.State == MediaState.Paused && MediaPlayer.State == media) ||
                (mousePos.Intersects(buttonPos) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released
                && MediaPlayer.State == MediaState.Paused && MediaPlayer.State == media))
            {
                MediaPlayer.Resume();
            }
        }

        public void adjustVolume(KeyboardState previousKey, KeyboardState key, MouseState mouse, MouseState prevMouse, Rectangle mousePos, Rectangle[] buttonPos)
        {
            if ((key.IsKeyDown(Keys.Subtract) && previousKey.IsKeyUp(Keys.Subtract)) ||
                (mousePos.Intersects(buttonPos[5]) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released))
            {
                MediaPlayer.Volume -= 0.1f;
            }

            if ((key.IsKeyDown(Keys.Add) && previousKey.IsKeyUp(Keys.Add)) ||
                (mousePos.Intersects(buttonPos[6]) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released))
            {
                MediaPlayer.Volume += 0.1f;
            }
        }

        public void muteSong(KeyboardState key, KeyboardState previousKey, MouseState mouse, MouseState prevMouse, Rectangle mousePos, Rectangle buttonPos)
        {
            if ((key.IsKeyDown(Keys.M) && previousKey.IsKeyUp(Keys.M) && isMuted == false && muteChanged == isMuted) ||
                (mousePos.Intersects(buttonPos) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released && isMuted == false && muteChanged == isMuted))
            {
                songVolumeReserve = songVolume/10f;
                MediaPlayer.Volume = 0.0f;
                isMuted = true;
            }
        }

        public void unMuteSong(KeyboardState key, KeyboardState previousKey, MouseState mouse, MouseState prevMouse, Rectangle mousePos, Rectangle buttonPos)
        {
            if ((key.IsKeyDown(Keys.M) && previousKey.IsKeyUp(Keys.M) && isMuted == true && muteChanged == isMuted) ||
                (mousePos.Intersects(buttonPos) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released && isMuted == true && muteChanged == isMuted))
            {
                MediaPlayer.Volume = songVolumeReserve;
                isMuted = false;
            }
        }

        public int songChosen(KeyboardState previousKey, KeyboardState key, Song[] list, MouseState mouse, MouseState prevMouse, Rectangle mousePos, Rectangle[] buttonPos)
        {
            int notChanged = song;
            if ((key.IsKeyDown(Keys.RightShift) && previousKey.IsKeyUp(Keys.RightShift)) ||
                (mousePos.Intersects(buttonPos[4]) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released))
            {
                if (song == (list.Length - 1))
                {
                    song = 0;
                }

                else
                {
                    song += 1;
                }
            }

            if ((key.IsKeyDown(Keys.LeftShift) && previousKey.IsKeyUp(Keys.LeftShift)) ||
                (mousePos.Intersects(buttonPos[7]) && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released))
            {
                if (song == 0)
                {
                    song = (list.Length - 1);
                }

                else
                {
                    song -= 1;
                }
            }

            if (song != notChanged)
            {
                MediaPlayer.Play(list[song]);
            }

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

            buttons[0] = new Rectangle(screenWidth - 400, screenHeight - 600, 200, 50);
            buttons[1] = new Rectangle(screenWidth - 400, screenHeight - 500, 200, 50);
            buttons[2] = new Rectangle(screenWidth - 400, screenHeight - 400, 200, 50);
            buttons[3] = new Rectangle(screenWidth - 400, screenHeight - 300, 200, 50);

            buttons[4] = new Rectangle(screenWidth - 425, screenHeight - 200, 50, 50);
            buttons[5] = new Rectangle(screenWidth - 350, screenHeight - 200, 50, 50);
            buttons[6] = new Rectangle(screenWidth - 275, screenHeight - 200, 50, 50);
            buttons[7] = new Rectangle(screenWidth - 200, screenHeight - 200, 50, 50);

            //Drawing name of song
            spriteBatch.DrawString(soundMusic,
                songList[song].Name, new Vector2(screenWidth - 400, screenHeight - 850), Color.Black);

            //Drawing how long the song has played, and duration
            spriteBatch.DrawString(soundMusic,
                progressbar(time) + " / " + progressbar(songDuration),
                new Vector2(screenWidth - 400, screenHeight - 800), Color.Black);

            //Drawing volume for the media player
            spriteBatch.DrawString(soundMusic,
                "Volume: " + Math.Round(songVolume) + " / " + "10",
                new Vector2(screenWidth - 400, screenHeight - 750), Color.Black);

            //Bigger buttons, such as play, pause and mute
            spriteBatch.Draw(play, buttons[0], Color.White);

            spriteBatch.Draw(stop, buttons[1], Color.White);

            if (media == MediaState.Paused)
            {
                spriteBatch.Draw(resume, buttons[2], Color.White);
            }
            else
            {
                spriteBatch.Draw(pause, buttons[2], Color.White);
            }

            if (isMuted == false)
            {
                spriteBatch.Draw(mute, buttons[3], Color.White);
            }

            else
            {
                spriteBatch.Draw(unMute, buttons[3], Color.White);
            }
            

            //Smaller buttons, such as prev and next
            spriteBatch.Draw(prev, buttons[4], Color.White);
            spriteBatch.Draw(volumeDown, buttons[5], Color.White);
            spriteBatch.Draw(volumeUp, buttons[6], Color.White);
            spriteBatch.Draw(next, buttons[7], Color.White);
        }

    }
}