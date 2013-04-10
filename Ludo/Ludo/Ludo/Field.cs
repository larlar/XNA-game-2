using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Ludo
{
    class Field
    {
        Rectangle rect;
        public Field(Rectangle rectangle)
        {
            this.rect = rectangle;
        }
        
        public Rectangle getRectangle()
        {
            return rect;
        }

        public void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, rect, Color.White);
            //Console.WriteLine("Null!");
        }
    }
}
