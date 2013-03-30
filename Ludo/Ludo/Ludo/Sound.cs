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
    class Sound
    {

        public Sound()
        {

        }

        public void update(GameTime gameTime, SoundEffectInstance sound, KeyboardState key, KeyboardState previousKey)
        {
            playSound(key, sound);
            stopSound(key, sound);
            pauseSound(key, sound);
            adjustVolume(previousKey, key, sound);
        }

        public void playSound(KeyboardState key, SoundEffectInstance sound)
        {
            if (key.IsKeyDown(Keys.A))
            {
                sound.Play();
            }
        }

        public void stopSound(KeyboardState key, SoundEffectInstance sound)
        {
            if (key.IsKeyDown(Keys.S))
            {
                sound.Stop();
            }
        }

        public void pauseSound(KeyboardState key, SoundEffectInstance sound)
        {
            if (key.IsKeyDown(Keys.D) && sound.State == SoundState.Playing)
            {
                sound.Pause();
            }
        }

        public void adjustVolume(KeyboardState previousKey, KeyboardState key, SoundEffectInstance sound)
        {
            if (key.IsKeyDown(Keys.F) && previousKey.IsKeyUp(Keys.F))
            {
                if (sound.Volume > 0.1f)
                {
                    sound.Volume -= 0.1f;
                }
            }

            if (key.IsKeyDown(Keys.G) && previousKey.IsKeyUp(Keys.G))
            {
                if (sound.Volume < 1)
                {
                    sound.Volume += 0.1f;
                }
            }
        }

    }
}
