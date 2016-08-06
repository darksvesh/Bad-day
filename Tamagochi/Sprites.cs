using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Tamagochi
{
    public class Sprites
    {        
        public Texture2D spriteTexture;
        public Rectangle SpritePlacement;
        public Point spriteWorldPosition;
        public Point SpriteSize;
       
        public Sprites()
        {
        } 
              
        public void LoadContent(ContentManager Content, String texture)
        {
            this.spriteTexture = Content.Load<Texture2D>(texture);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle ScreenPosition)
        {
            spriteBatch.Draw(spriteTexture, ScreenPosition, Color.White);
        }
    }
}
