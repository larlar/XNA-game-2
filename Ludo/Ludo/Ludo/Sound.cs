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

        public void playSound(KeyboardState key, SoundEffectInstance sound)
        {
            if (key.IsKeyDown(Keys.P))
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
            if (key.IsKeyDown(Keys.E) && sound.State == SoundState.Playing)
            {
                sound.Pause();
            }
        }

    }
}
