using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Tamagochi
{
    public class Sprite
    {
        public Texture2D spriteTexture;
        //public Vector2 spritePosition;
        public Rectangle SpritePlacement;
        public Point spriteWorldPosition;
        public Point SpriteSize;
        public Vector2 spritePosition;
        int FrameCount; //Количество всех фреймов в изображении 
        int frame;//какой фрейм нарисован в данный момент
        float TimeForFrame;//Сколько времени нужно показывать один фрейм (скорость)
        float TotalTime;//сколько времени прошло с показа предыдущего фрейма 

        public Sprite(int speedAnimation)
        {
            frame = 0;
            TimeForFrame = (float)1 / speedAnimation;
            TotalTime = 0;
        }
        public void Update(GameTime gameTime)
        {
            FrameCount = this.spriteTexture.Width / spriteTexture.Height;
            TotalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (TotalTime > TimeForFrame)
            {
                frame++;
                frame = frame % (FrameCount - 1);
                TotalTime -= TimeForFrame;
            }
        }
        public void DrawAnimation(SpriteBatch spriteBatch, Rectangle ScreenPosition)
        {
            
            int frameWidth = spriteTexture.Width / FrameCount;
            Rectangle rectanglе = new Rectangle(frameWidth * frame, 0, frameWidth, spriteTexture.Height);
            spriteBatch.Draw(spriteTexture, ScreenPosition, rectanglе, Color.White);
        }

        public void LoadContent(ContentManager Content, String texture)
        {
            spriteTexture = Content.Load<Texture2D>(texture);          
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle ScreenPosition)
       {
           spriteBatch.Draw(spriteTexture, ScreenPosition, Color.White);
        }
    }
}
