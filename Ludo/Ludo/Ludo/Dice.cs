﻿using System;
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

namespace Ludo
{
    // A single 6-sided die
    class Die
    {
        public const int sides = 6;
        private static Random rand = new Random();
        private int value;

        // setting defaulted diceSide to number1
        public Die()
        {
            value = 1;
        }

        // get method for the diceSide value
        public int DiceFace
        {
            get { return value; }
        }

        // The basic roll method that rolles the dice to a random side every thime the dice(s) are rolled.
        public void Roll()
        {
            value = Die.rand.Next(1, sides + 1);
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
            textures = new Texture2D[Die.sides];
            this.firstDiePosition = firstDiePosition;
            dice = new Die[numberOfDice];

            for (int i = 0; i < this.dice.Length; i++)
            {
                this.dice[i] = new Die();
            }
        }

        public void LoadContent(ContentManager content)
        {
            textures[0] = content.Load<Texture2D>("Die_1");
            textures[1] = content.Load<Texture2D>("Die_2");
            textures[2] = content.Load<Texture2D>("Die_3");
            textures[3] = content.Load<Texture2D>("Die_4");
            textures[4] = content.Load<Texture2D>("Die_5");
            textures[5] = content.Load<Texture2D>("Die_6");
        }

        // Uppdate method that rolls the Dice when the spesific button is pressed.
        public void update(GameTime gameTime)
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
        }

        //draw funksjon
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.dice.Length; i++)
            {
                Vector2 position = new Vector2(this.firstDiePosition.X, this.firstDiePosition.Y);
                position.X += i * (textures[0].Width);

                Die die = this.dice[i];
                spriteBatch.Draw(this.textures[die.DiceFace - 1], position, Color.White);
            }
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

        public int getValue() 
        {
            return dice[0].DiceFace;
        }

    }
}

