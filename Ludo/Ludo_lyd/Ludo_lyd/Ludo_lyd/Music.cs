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

namespace Ludo_lyd
{
    class Music
    {
         public Music()
        {

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
            if (key.IsKeyDown(Keys.R) && MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
        }

        public void resumeSong(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.T) && MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
        }

        public void adjustVolume(KeyboardState previousKey, KeyboardState key)
        {
            if (key.IsKeyDown(Keys.V) && previousKey.IsKeyUp(Keys.V))
            {
                MediaPlayer.Volume -= 0.1f;
            }

            if (key.IsKeyDown(Keys.B) && previousKey.IsKeyUp(Keys.B))
            {
                MediaPlayer.Volume += 0.1f;
            }
        }
    }
}
