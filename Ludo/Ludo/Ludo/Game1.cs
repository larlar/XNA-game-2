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
    
        public enum GameMode
        {
            MenuMode,
            PlayingMode,
        }

        GameMode gameMode = GameMode.MenuMode;
        GameLogic currentState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sound soundMaster;
        Music musicMaster;
        Background myBackground;
        MenuButton myButtons;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            MediaPlayer.IsRepeating = true;
            myBackground = new Background();
            musicMaster = new Music();
            soundMaster = new Sound();
            myButtons = new MenuButton();          
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
            graphics.ApplyChanges();
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
            musicMaster.LoadContent(Content);
            soundMaster.LoadContent(Content);
            myBackground.LoadContent(Content);
            myButtons.LoadContent(Content);       
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
            switch (gameMode)
            {
                case GameMode.MenuMode:
                    soundMaster.update(gameTime);
                    musicMaster.update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    {
                        currentState = new OneVsThree();
                        gameMode = GameMode.PlayingMode;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D2))
                    {
                        currentState = new TwoVsTwo();
                        gameMode = GameMode.PlayingMode;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D3))
                    {
                        currentState = new ThreeVsOne();
                        gameMode = GameMode.PlayingMode;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D4))
                    {
                        currentState = new FourVsZero();
                        gameMode = GameMode.PlayingMode;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D5))
                    {
                        currentState = new ZeroVsFour();
                        gameMode = GameMode.PlayingMode;
                    }
                    break;

                case GameMode.PlayingMode:
                    currentState = currentState.update();
                    soundMaster.update(gameTime);
                    musicMaster.update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameMode = GameMode.MenuMode;
                    break;
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch(gameMode){
                case GameMode.MenuMode:
                    GraphicsDevice.Clear(Color.Coral);
                    myButtons.Draw(spriteBatch, graphics.GraphicsDevice);
                    musicMaster.Draw(spriteBatch, graphics.GraphicsDevice);
                    soundMaster.Draw(spriteBatch, graphics.GraphicsDevice);
                    break;
                case GameMode.PlayingMode:
                    myBackground.Draw(spriteBatch);
                    musicMaster.Draw(spriteBatch, graphics.GraphicsDevice);
                    soundMaster.Draw(spriteBatch, graphics.GraphicsDevice);
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}