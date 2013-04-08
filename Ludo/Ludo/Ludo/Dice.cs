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
    // A single 6-sided die
    class Die
    {
        public const int sides = 6;
        private static Random rand = new Random();
        private int diceSide;

        // setting defaulted diceSide to number1
        public Die()
        {
            this.diceSide = 1;
        }

        // get method for the diceSide value
        public int DiceFace
        {
            get { return this.diceSide; }
        }

        // The basic roll method that rolles the dice to a random side every thime the dice(s) are rolled.
        public void Roll()
        {
            this.diceSide = Die.rand.Next(1, sides + 1);
        }
    }
    
    class Dice : DrawableGameComponent
    {
        // Creates private variables and arrays
        private Texture2D[] textures;
        private Vector2 firstDiePosition;
        private Die[] dice;
        private bool rolledLastUpdate = false;

        // Constructor that is used to create objects of the dice class.
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
            // for loop that loads all the textures into the LoadContent method
            for (int i = 0; i < this.textures.Length; i++)
            {
                this.textures[i] = this.Game.Content.Load<Texture2D>("Die_" + (i + 1));
            }

            base.LoadContent();
        }

        // Uppdate method that rolls the Dice when the spesific button is pressed.
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

        // Dice draw method. The code is written so its posible to draw more than 1 dice if that is choosen in the constructor
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
        
        // Roll method that rolles the dice(s) depending on how many dices are made
        public void Roll()
        {
            foreach (Die die in this.dice)
            {
                die.Roll();
            }
        }

        // A method that return the total score from the dice or dices.
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
}
