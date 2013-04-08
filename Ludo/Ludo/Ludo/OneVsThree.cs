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
    class OneVsThree: GameModel
    {
        Rectangle[] fields = new Rectangle[92];
        Rectangle[] yellowPath = new Rectangle[63];
        Rectangle[] greenPath = new Rectangle[63];
        Rectangle[] bluePath = new Rectangle[63];
        Rectangle[] redPath = new Rectangle[63];

        Texture2D yellowBrickOne;
        Texture2D yellowBrickTwo;
        Texture2D yellowBrickThree;
        Texture2D yellowBrickFour;
        Texture2D greenBrickOne;
        Texture2D greenBrickTwo;
        Texture2D greenBrickThree;
        Texture2D greenBrickFour;
        Texture2D blueBrickOne;
        Texture2D blueBrickTwo;
        Texture2D blueBrickThree;
        Texture2D blueBrickFour;
        Texture2D redBrickOne;
        Texture2D redBrickTwo;
        Texture2D redBrickThree;
        Texture2D redBrickFour;

        KeyboardState key;
        KeyboardState previousKey;

        MouseState mouse;
        MouseState previousMouse;
        Rectangle mousePos;
        Rectangle prevMousePos;

        public OneVsThree() { }
       
        public override GameModel update()
        {
            Console.WriteLine("OneVsThree!");
            return this;
        }

        public void LoadContent(ContentManager content)
        {
            mousePos = new Rectangle(0, 0, 1, 1);
            prevMousePos = new Rectangle(0, 0, 1, 1);

            yellowBrickOne = content.Load<Texture2D>("yellow-brick-one");
            yellowBrickTwo = content.Load<Texture2D>("yellow-brick-two");
            yellowBrickThree = content.Load<Texture2D>("yellow-brick-three");
            yellowBrickFour = content.Load<Texture2D>("yellow-brick-four");

            greenBrickOne = content.Load<Texture2D>("green-brick-one");
            greenBrickTwo = content.Load<Texture2D>("green-brick-two");
            greenBrickThree = content.Load<Texture2D>("green-brick-three");
            greenBrickFour = content.Load<Texture2D>("green-brick-four");

            blueBrickOne = content.Load<Texture2D>("blue-brick-one");
            blueBrickTwo = content.Load<Texture2D>("blue-brick-two");
            blueBrickThree = content.Load<Texture2D>("blue-brick-three");
            blueBrickFour = content.Load<Texture2D>("blue-brick-four");

            redBrickOne = content.Load<Texture2D>("red-brick-one");
            redBrickTwo = content.Load<Texture2D>("red-brick-two");
            redBrickThree = content.Load<Texture2D>("red-brick-three");
            redBrickFour = content.Load<Texture2D>("red-brick-four");
        }

        public void update(GameTime gameTime)
        {
            //Preventing multiple presses in one click
            previousKey = key;
            key = Keyboard.GetState();

            //Preventing multiple presses in one click
            previousMouse = mouse;
            mouse = Mouse.GetState();

            mousePos.X = mouse.X;
            mousePos.Y = mouse.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            fieldMaker();

            //starting fields
            for(int i = 0; i < 4; i++){
                spriteBatch.Draw(yellowBrickOne, fields[i], Color.White);
                spriteBatch.Draw(greenBrickOne, fields[i+4], Color.White);
                spriteBatch.Draw(blueBrickOne, fields[i+8], Color.White);
                spriteBatch.Draw(redBrickOne, fields[i+12], Color.White);
            }

            //normal path
            for (int i = 16; i < 68; i++){
                spriteBatch.Draw(greenBrickOne, fields[i], Color.White);
            }
            
            //center branches
            for(int i = 68; i < 92; i++){
                spriteBatch.Draw(redBrickOne, fields[i], Color.White);
            }

        }

        public void fieldMaker()
        {
            //yellow startingfield
            fields[0] = new Rectangle(67,  472, 45, 45);
            fields[1] = new Rectangle(157, 472, 45, 45);
            fields[2] = new Rectangle(67,  562, 45, 45);
            fields[3] = new Rectangle(157, 562, 45, 45);

            //green startingfield
            fields[4] = new Rectangle(67,  67,  45, 45);
            fields[5] = new Rectangle(157, 67,  45, 45);
            fields[6] = new Rectangle(67,  157, 45, 45);
            fields[7] = new Rectangle(157, 157, 45, 45);

            //blue startingfield
            fields[8] = new Rectangle(472,  67, 45, 45);
            fields[9] = new Rectangle(562,  67, 45, 45);
            fields[10] = new Rectangle(472, 157, 45, 45);
            fields[11] = new Rectangle(562, 157, 45, 45);

            //red startingfield
            fields[12] = new Rectangle(472, 472, 45, 45);
            fields[13] = new Rectangle(562, 472, 45, 45);
            fields[14] = new Rectangle(472, 562, 45, 45);
            fields[15] = new Rectangle(562, 562, 45, 45);

            //normal paths
            fields[16] = new Rectangle(270, 585, 45, 45);
            fields[17] = new Rectangle(270, 540, 45, 45);
            fields[18] = new Rectangle(270, 495, 45, 45);
            fields[19] = new Rectangle(270, 450, 45, 45);
            fields[20] = new Rectangle(270, 405, 45, 45);
            fields[21] = new Rectangle(225, 360, 45, 45);
            fields[22] = new Rectangle(180, 360, 45, 45);
            fields[23] = new Rectangle(135, 360, 45, 45);
            fields[24] = new Rectangle(90, 360, 45, 45);
            fields[25] = new Rectangle(45, 360, 45, 45);
            fields[26] = new Rectangle(0, 360, 45, 45);
            fields[27] = new Rectangle(0, 315, 45, 45);
            fields[28] = new Rectangle(0, 270, 45, 45);
            fields[29] = new Rectangle(45, 270, 45, 45);
            fields[30] = new Rectangle(90, 270, 45, 45);
            fields[31] = new Rectangle(135, 270, 45, 45);
            fields[32] = new Rectangle(180, 270, 45, 45);
            fields[33] = new Rectangle(225, 270, 45, 45);
            fields[34] = new Rectangle(270, 225, 45, 45);
            fields[35] = new Rectangle(270, 180, 45, 45);
            fields[36] = new Rectangle(270, 135, 45, 45);
            fields[37] = new Rectangle(270, 90, 45, 45);
            fields[38] = new Rectangle(270, 45, 45, 45);
            fields[39] = new Rectangle(270, 0, 45, 45);
            fields[40] = new Rectangle(315, 0, 45, 45);
            fields[41] = new Rectangle(360, 0, 45, 45);
            fields[42] = new Rectangle(360, 45, 45, 45);
            fields[43] = new Rectangle(360, 90, 45, 45);
            fields[44] = new Rectangle(360, 135, 45, 45);
            fields[45] = new Rectangle(360, 180, 45, 45);
            fields[46] = new Rectangle(360, 225, 45, 45);
            fields[47] = new Rectangle(405, 270, 45, 45);
            fields[48] = new Rectangle(450, 270, 45, 45);
            fields[49] = new Rectangle(495, 270, 45, 45);
            fields[50] = new Rectangle(540, 270, 45, 45);
            fields[51] = new Rectangle(585, 270, 45, 45);
            fields[52] = new Rectangle(630, 270, 45, 45);
            fields[53] = new Rectangle(630, 315, 45, 45);
            fields[54] = new Rectangle(630, 360, 45, 45);
            fields[55] = new Rectangle(585, 360, 45, 45);
            fields[56] = new Rectangle(540, 360, 45, 45);
            fields[57] = new Rectangle(495, 360, 45, 45);
            fields[58] = new Rectangle(450, 360, 45, 45);
            fields[59] = new Rectangle(405, 360, 45, 45);
            fields[60] = new Rectangle(360, 405, 45, 45);
            fields[61] = new Rectangle(360, 450, 45, 45);
            fields[62] = new Rectangle(360, 495, 45, 45);
            fields[63] = new Rectangle(360, 540, 45, 45);
            fields[64] = new Rectangle(360, 585, 45, 45);
            fields[65] = new Rectangle(360, 630, 45, 45);
            fields[66] = new Rectangle(315, 630, 45, 45);
            fields[67] = new Rectangle(270, 630, 45, 45);
            
            //yellow branch
            fields[68] = new Rectangle(315, 585, 45, 45);
            fields[69] = new Rectangle(315, 540, 45, 45);
            fields[70] = new Rectangle(315, 495, 45, 45);
            fields[71] = new Rectangle(315, 450, 45, 45);
            fields[72] = new Rectangle(315, 405, 45, 45);
            fields[73] = new Rectangle(315, 360, 45, 45);
            
            //green branch
            fields[74] = new Rectangle(45,  315, 45, 45);
            fields[75] = new Rectangle(90,  315, 45, 45);
            fields[76] = new Rectangle(135, 315, 45, 45);
            fields[77] = new Rectangle(180, 315, 45, 45);
            fields[78] = new Rectangle(225, 315, 45, 45);
            fields[79] = new Rectangle(270, 315, 45, 45);
            
            //blue branch
            fields[80] = new Rectangle(315, 45, 45, 45);
            fields[81] = new Rectangle(315, 90, 45, 45);
            fields[82] = new Rectangle(315, 135, 45, 45);
            fields[83] = new Rectangle(315, 180, 45, 45);
            fields[84] = new Rectangle(315, 225, 45, 45);
            fields[85] = new Rectangle(315, 270, 45, 45);
            
            //red branch
            fields[86] = new Rectangle(585, 315, 45, 45);
            fields[87] = new Rectangle(540, 315, 45, 45);
            fields[88] = new Rectangle(495, 315, 45, 45);
            fields[89] = new Rectangle(450, 315, 45, 45);
            fields[90] = new Rectangle(405, 315, 45, 45);
            fields[91] = new Rectangle(360, 315, 45, 45);
        }

        /* 
         * Class specific logic goes here. 
         * Mostly how many AI's. 
         */ 
        
    }
}
