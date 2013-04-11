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
        SoundState sfx;
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

            //Gets the state of the soundEffect, to prevent the soundInstance, to change state more than once a frame.
            sfx = soundInstance.State;

            //Checking when key is pressed and released, in order too prevent multiple clicks in one press.
            previousKey = keyboard;
            keyboard = Keyboard.GetState();

            playSound(soundInstance);
            stopSound(soundInstance);
            pauseSound(soundInstance);
            adjustVolume(soundInstance);

            //Used to display sfx volume
            soundVolume = soundInstance.Volume * 10;
        }

        public void playSound(SoundEffectInstance soundInstance)
        {
            if (keyboard.IsKeyDown(Keys.A) && previousKey.IsKeyUp(Keys.A) && soundInstance.State != SoundState.Playing && soundInstance.State == sfx)
            {
                soundInstance.Play();
            }
        }

        public void stopSound(SoundEffectInstance soundInstance)
        {
            if (keyboard.IsKeyDown(Keys.D) && previousKey.IsKeyUp(Keys.D) && soundInstance.State == SoundState.Playing)
            {
                soundInstance.Stop();
            }
        }

        public void pauseSound(SoundEffectInstance soundInstance)
        {
            if (keyboard.IsKeyDown(Keys.A) && previousKey.IsKeyUp(Keys.A) && soundInstance.State == SoundState.Playing && soundInstance.State == sfx)
            {
                soundInstance.Pause();
            }
        }

        public void adjustVolume(SoundEffectInstance soundInstance)
        {
            if (keyboard.IsKeyDown(Keys.F) && previousKey.IsKeyUp(Keys.F))
            {
                if (soundInstance.Volume > 0.1f)
                {
                    soundInstance.Volume -= 0.1f;
                }
            }

            if (keyboard.IsKeyDown(Keys.G) && previousKey.IsKeyUp(Keys.G))
            {
                if (soundInstance.Volume < 1)
                {
                    soundInstance.Volume += 0.1f;
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