using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Caveman_Survival
{
    class Credits
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    class ScrollingCredits : Background
    {
        public ScrollingCredits(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public void Update()
        {
            if (rectangle.Y != -600)
            {
                rectangle.Y -= 3;
            }
            else
            {
                Game1.CreditsExit();
            }
        }
    }
}