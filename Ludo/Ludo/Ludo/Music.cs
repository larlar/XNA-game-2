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

        public Music()
        {

        }

        public void update(GameTime gameTime, Song song, KeyboardState key, KeyboardState previousKey)
        {
            playSong(key, song);
            stopSong(key);
            pauseSong(key);
            resumeSong(key);
            adjustVolume(previousKey, key);
        }

        public void playSong(KeyboardState key, Song music)
        {
            if (key.IsKeyDown(Keys.Q))
            {
                MediaPlayer.Play(music);
            }
        }

        public void stopSong(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.W))
            {
                MediaPlayer.Stop();
            }
        }

        public void pauseSong(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.E) && MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
        }

        public void resumeSong(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.R) && MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
        }

        public void adjustVolume(KeyboardState previousKey, KeyboardState key)
        {
            if (key.IsKeyDown(Keys.T) && previousKey.IsKeyUp(Keys.T))
            {
                MediaPlayer.Volume -= 0.1f;
            }

            if (key.IsKeyDown(Keys.Y) && previousKey.IsKeyUp(Keys.Y))
            {
                MediaPlayer.Volume += 0.1f;
            }
        }

        public int songChosen(KeyboardState previousKey, KeyboardState key, Song[] list)
        {
            int notChanged = song;
            if (key.IsKeyDown(Keys.U) && previousKey.IsKeyUp(Keys.U))
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

            if (key.IsKeyDown(Keys.I) && previousKey.IsKeyUp(Keys.I))
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

    }
}
