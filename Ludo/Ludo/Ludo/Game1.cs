using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ludo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SoundEffect sound;
        SoundEffectInstance soundInstance;
        float soundVolume;
        Sound soundMaster;

        Song[] songList = new Song[3];
        int song;
        float songVolume;
        Music musicMaster;

        KeyboardState keyboard;
        KeyboardState previousKey;

        SpriteFont soundMusic;

        TimeSpan time;
        TimeSpan songDuration;

        Background myBackground;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            MediaPlayer.IsRepeating = true;

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;

            this.myBackground = new Background();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            songList[0] = Content.Load<Song>("130_Staff_Credits_Redux_ZREO");
            songList[1] = Content.Load<Song>("1-08_Clock_Town_-_Day_1_ZREO");
            songList[2] = Content.Load<Song>("19_Hyrule_Field_Main_Theme_ZREO");
            musicMaster = new Music();

            sound = Content.Load<SoundEffect>("01_Title_Theme_ZREO");
            soundInstance = sound.CreateInstance();
            soundMaster = new Sound();

            soundMusic = Content.Load<SpriteFont>("Sound_Music");

            myBackground.LoadContent(Content);


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
                soundInstance.Dispose();
            }

            // TODO: Add your update logic here

            previousKey = keyboard;
            keyboard = Keyboard.GetState();

            soundMaster.update(gameTime, soundInstance, keyboard, previousKey);

            song = musicMaster.songChosen(previousKey, keyboard, songList);
            musicMaster.update(gameTime, songList[song], keyboard, previousKey);

            time = MediaPlayer.PlayPosition;
            songDuration = songList[song].Duration;

            soundVolume = soundInstance.Volume * 10;
            songVolume = MediaPlayer.Volume * 10;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.DrawString(soundMusic,
                songList[song].Name, new Vector2(100, 100), Color.Black);

            spriteBatch.DrawString(soundMusic,
                musicMaster.progressbar(time) + " / " + musicMaster.progressbar(songDuration),
                new Vector2(100, 150), Color.Black);

            spriteBatch.DrawString(soundMusic,
                "Volume: " + (int)songVolume + " / " + "10",
                new Vector2(100, 200), Color.Black);

            spriteBatch.DrawString(soundMusic,
              "sfx: " + (int)soundVolume + " / " + "10",
                new Vector2(100, 250), Color.Black);

            //Draw Background
            myBackground.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
