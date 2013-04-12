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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
    
        public enum GameMode
        {
            MenuMode,
            PlayingMode
        }

        GameMode gameMode;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sound soundMaster;
        Music musicMaster;
        Background myBackground;
        StartMenu startMenu;
        GameModel gameModel;
        Board board;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 675;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            MediaPlayer.IsRepeating = true;
            myBackground = new Background();
            musicMaster = new Music();
            soundMaster = new Sound();
            startMenu = new StartMenu();
            gameMode = GameMode.MenuMode;
            board = new Board();
        }

        protected override void Initialize()
        {
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            musicMaster.LoadContent(Content);
            soundMaster.LoadContent(Content);
            myBackground.LoadContent(Content);
            startMenu.LoadContent(Content);
            board.LoadContent(Content); 
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameMode)
            {
                case GameMode.MenuMode:
                    soundMaster.update(gameTime);
                    musicMaster.update(gameTime);
                    GameModel model = startMenu.update(spriteBatch);
                    if (model != null)
                    {
                        gameModel = model;
                        gameMode = GameMode.PlayingMode;
                        board.setModel(gameModel);
                        if (model.GetType() == typeof(ExitLudo))
                            Exit();
                    }
                    break;

                case GameMode.PlayingMode:
                    soundMaster.update(gameTime);
                    musicMaster.update(gameTime);
                    gameModel.update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameMode = GameMode.MenuMode;
                    break;       
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            switch(gameMode){
                case GameMode.MenuMode:
                    GraphicsDevice.Clear(Color.Coral);
                    startMenu.Draw(spriteBatch, graphics.GraphicsDevice);
                    musicMaster.Draw(spriteBatch, graphics.GraphicsDevice);
                    break;

                case GameMode.PlayingMode:
                    GraphicsDevice.Clear(Color.LightBlue);
                    myBackground.Draw(spriteBatch);
                    musicMaster.Draw(spriteBatch, graphics.GraphicsDevice);
                    board.Draw(spriteBatch, graphics.GraphicsDevice);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}