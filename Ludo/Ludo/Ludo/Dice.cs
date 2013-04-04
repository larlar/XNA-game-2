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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace diceTest
{
    class Dice : DrawableGameComponent
    {
        private Texture2D[] textures;
        private Vector2 firstDiePosition;
        private Die[] dice;

        private bool rolledLastUpdate = false;

        public Dice(Game game, int numberOfDice, Vector2 firstDiePosition)
            : base(game)
        {
            this.textures = new Texture2D[Die.sides];
            this.firstDiePosition = firstDiePosition;
            this.dice = new Die[numberOfDice];

            for (int i = 0; i < this.dice.Length; i++)
            {
                this.dice[i] = new Die();
            }
        }

        
        protected override void LoadContent()
        {
            for (int i = 0; i < this.textures.Length; i++)
            {
                this.textures[i] = this.Game.Content.Load<Texture2D>("Die_" + (i + 1));
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (!rolledLastUpdate)
                {
                    this.Roll();
                }

                rolledLastUpdate = true;
            }
            else
            {
                rolledLastUpdate = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = ((Game1)this.Game).SpriteBatch;

            sb.Begin();

            for (int i = 0; i < this.dice.Length; i++)
            {
                Vector2 position = new Vector2(this.firstDiePosition.X, this.firstDiePosition.Y);
                position.X += i * (textures[0].Width + 10);

                Die die = this.dice[i];
                sb.Draw(this.textures[die.DiceFace - 1], position, Color.White);
            }

            sb.End();

            base.Draw(gameTime);
        }
       

       
        public void Roll()
        {
            foreach (Die die in this.dice)
            {
                die.Roll();
            }
        }

        public int Total()
        {
            int total = 0;

            foreach (Die die in this.dice)
            {
                total = total + die.DiceFace;
            }

            return total;
        }
        
    }

    // A single 6-sided die, defaulted to start at number 1 DiceSide.
    class Die
    {
        public const int sides = 6;
        private static Random rand = new Random();
        private int diceSide;

        public Die()
        {
            this.diceSide = 1;
        }

        public int DiceFace
        {
            get { return this.diceSide; }
        }

        public void Roll()
        {
            this.diceSide = Die.rand.Next(1, sides + 1);
        }
    }
}
