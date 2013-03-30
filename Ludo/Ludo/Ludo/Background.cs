using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ludo
{
    class Background
    {

        //Create the full grid of the board, where the numbers refer to the sprites that is supposed to be
        // placed here. the array starts from the top of the bord from left to right and then goes downwards.
        int[,] myArray = new  int[,] {
            {3,3,3,3,3,3,4,4,4,0,0,0,0,0,0},
		    {3,24,25,24,25,3,4,0,0,0,12,13,12,13,0},
		    {3,22,23,22,23,3,4,0,4,0,10,11,10,11,0},
		    {3,24,25,24,25,3,4,0,4,0,12,13,12,13,0},
		    {3,22,23,22,23,3,4,0,4,0,10,11,10,11,0},
		    {3,3,3,3,3,3,4,0,4,0,0,0,0,0,0},
		    {4,3,4,4,4,4,6,0,5,4,4,4,4,4,4},
		    {4,3,3,3,3,3,3,9,1,1,1,1,1,1,4},
		    {4,4,4,4,4,4,7,2,8,4,4,4,4,1,4},
		    {2,2,2,2,2,2,4,2,4,1,1,1,1,1,1},
		    {2,20,21,20,21,2,4,2,4,1,16,17,16,17,1},
		    {2,18,19,18,19,2,4,2,4,1,14,15,14,15,1},
		    {2,20,21,20,21,2,4,2,4,1,16,17,16,17,1},
		    {2,18,19,18,19,2,2,2,4,1,14,15,14,15,1},
		    {2,2,2,2,2,2,4,4,4,1,1,1,1,1,1}};
        
        //Create array that will gives the numbers in myArray the right sprites.
        Texture2D[] textureArray = new Texture2D[26];

        // ContentManager that is loadedes textures and position for the objects in to game1
        public void LoadContent(ContentManager content)
        {
         
            // Binds the places in textureArray with the right sprites
            textureArray[0] = content.Load<Texture2D>("blue-block");
            textureArray[1] = content.Load<Texture2D>("red-block");
            textureArray[2] = content.Load<Texture2D>("yellow-block");
            textureArray[3] = content.Load<Texture2D>("green-block");
            textureArray[4] = content.Load<Texture2D>("white-block");
            textureArray[5] = content.Load<Texture2D>("blue-red-block");
            textureArray[6] = content.Load<Texture2D>("green-blue-block");
            textureArray[7] = content.Load<Texture2D>("green-yellow-block");
            textureArray[8] = content.Load<Texture2D>("yellow-red-block");
            textureArray[9] = content.Load<Texture2D>("x-center-block");
            textureArray[10] = content.Load<Texture2D>("blue-bottomleft-block");
            textureArray[11] = content.Load<Texture2D>("blue-bottomright-block");
            textureArray[12] = content.Load<Texture2D>("blue-topleft-block");
            textureArray[13] = content.Load<Texture2D>("blue-topright-block");
            textureArray[14] = content.Load<Texture2D>("red-bottomleft-block");
            textureArray[15] = content.Load<Texture2D>("red-bottomright-block");
            textureArray[16] = content.Load<Texture2D>("red-topleft-block");
            textureArray[17] = content.Load<Texture2D>("red-topright-block");
            textureArray[18] = content.Load<Texture2D>("yellow-bottomleft-block");
            textureArray[19] = content.Load<Texture2D>("yellow-bottomright-block");
            textureArray[20] = content.Load<Texture2D>("yellow-topleft-block");
            textureArray[21] = content.Load<Texture2D>("yellow-topright-block");
            textureArray[22] = content.Load<Texture2D>("green-bottomleft-block");
            textureArray[23] = content.Load<Texture2D>("green-bottomright-block");
            textureArray[24] = content.Load<Texture2D>("green-topleft-block");
            textureArray[25] = content.Load<Texture2D>("green-topright-block");    
        }

        // Method that draws the sprites to the screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < 15; y++) {
                for (int x = 0; x < 15; x++) {
                    spriteBatch.Draw(textureArray[myArray[y,x]], new Rectangle(x * 45, y * 45, 45, 45), Color.White);
                }
            }
        }
    } 
}
