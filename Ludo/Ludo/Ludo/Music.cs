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
        int song;
        Song[] songList = new Song[3];
        float songVolume;
        float songVolumeReserve;

        bool isMuted = false;

        TimeSpan time;
        TimeSpan songDuration;

        KeyboardState keyboard;
        KeyboardState previousKey;

        SpriteFont soundMusic;

        MediaState media;

        

        public Music()
        {

        }

        public void LoadContent(ContentManager content)
        {
            songList[0] = content.Load<Song>("130_Staff_Credits_Redux_ZREO");
            songList[1] = content.Load<Song>("1-08_Clock_Town_-_Day_1_ZREO");
            songList[2] = content.Load<Song>("19_Hyrule_Field_Main_Theme_ZREO");

            soundMusic = content.Load<SpriteFont>("Sound_Music");

            MediaPlayer.Stop();
            media = MediaPlayer.State;
        }

        public void update(GameTime gameTime)
        {
            media = MediaPlayer.State;

            previousKey = keyboard;
            keyboard = Keyboard.GetState();

            playSong(keyboard, previousKey, songList[song]);
            stopSong(keyboard);
            pauseSong(keyboard, previousKey);
            resumeSong(keyboard, previousKey);
            adjustVolume(previousKey, keyboard);
            muteSong(keyboard, previousKey);
            unMuteSong(keyboard, previousKey);

            song = songChosen(previousKey, keyboard, songList);

            time = MediaPlayer.PlayPosition;
            songDuration = songList[song].Duration;
            songVolume = MediaPlayer.Volume * 10;
        }


        public void playSong(KeyboardState key, KeyboardState previousKey, Song music)
        {
            if (key.IsKeyDown(Keys.P) && previousKey.IsKeyUp(Keys.P) && MediaPlayer.State != MediaState.Playing && MediaPlayer.State != MediaState.Paused && 
                MediaPlayer.State == media )
            {
                MediaPlayer.Play(music);
            }
        }

        public void stopSong(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.S))
            {
                MediaPlayer.Stop();
            }
        }

        public void pauseSong(KeyboardState key, KeyboardState previousKey)
        {
            if (key.IsKeyDown(Keys.P) && previousKey.IsKeyUp(Keys.P) && MediaPlayer.State == MediaState.Playing && MediaPlayer.State == media)
            {
                MediaPlayer.Pause();
            }
        }

        public void resumeSong(KeyboardState key, KeyboardState previousKey)
        {
            if (key.IsKeyDown(Keys.P) && previousKey.IsKeyUp(Keys.P) && MediaPlayer.State == MediaState.Paused && MediaPlayer.State == media)
            {
                MediaPlayer.Resume();
            }
        }

        public void adjustVolume(KeyboardState previousKey, KeyboardState key)
        {
            if (key.IsKeyDown(Keys.Subtract) && previousKey.IsKeyUp(Keys.Subtract))
            {
                MediaPlayer.Volume -= 0.1f;
            }

            if (key.IsKeyDown(Keys.Add) && previousKey.IsKeyUp(Keys.Add))
            {
                MediaPlayer.Volume += 0.1f;
            }
        }

        public void muteSong(KeyboardState key, KeyboardState previousKey)
        {
            if (key.IsKeyDown(Keys.M) && previousKey.IsKeyUp(Keys.M))
            {
                songVolumeReserve = songVolume;
                MediaPlayer.Volume = 0.0f;
                isMuted = true;
            }
        }

        public void unMuteSong(KeyboardState key, KeyboardState previousKey)
        {
            if (key.IsKeyDown(Keys.U) && previousKey.IsKeyUp(Keys.U) && isMuted == true)
            {
                MediaPlayer.Volume = songVolumeReserve;
                isMuted = false;
            }
        }

        public int songChosen(KeyboardState previousKey, KeyboardState key, Song[] list)
        {
            int notChanged = song;
            if (key.IsKeyDown(Keys.RightShift) && previousKey.IsKeyUp(Keys.RightShift))
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

            if (key.IsKeyDown(Keys.LeftShift) && previousKey.IsKeyUp(Keys.LeftShift))
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

            spriteBatch.DrawString(soundMusic,
                songList[song].Name, new Vector2(screenWidth - 400, screenHeight - 850), Color.Black);

            spriteBatch.DrawString(soundMusic,
                progressbar(time) + " / " + progressbar(songDuration),
                new Vector2(screenWidth - 400, screenHeight - 800), Color.Black);

            spriteBatch.DrawString(soundMusic,
                "Volume: " + (int)songVolume + " / " + "10",
                new Vector2(screenWidth - 400, screenHeight - 750), Color.Black);
        }

    }
}