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

        SoundEffect sound;
        SoundEffectInstance soundInstance;
        float soundVolume;

        KeyboardState keyboard;
        KeyboardState previousKey;

        SpriteFont soundMusic;

        public Sound()
        {

        }

        public void LoadContent(ContentManager content)
        {
            sound = content.Load<SoundEffect>("Zelda Title Theme");
            soundInstance = sound.CreateInstance();

            soundMusic = content.Load<SpriteFont>("Sound_Music");
        }

        public void update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                soundInstance.Dispose();
            }

            previousKey = keyboard;
            keyboard = Keyboard.GetState();

            playSound(keyboard, soundInstance);
            stopSound(keyboard, soundInstance);
            pauseSound(keyboard, soundInstance);
            adjustVolume(previousKey, keyboard, soundInstance);

            soundVolume = soundInstance.Volume * 10;
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

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            spriteBatch.DrawString(soundMusic,
               "sfx: " + Math.Round(soundVolume) + " / " + "10",
                 new Vector2(screenWidth-400, screenHeight-700), Color.Black);
        }
    }
}