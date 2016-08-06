using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Tamagochi
{
    class Hero_anim
    {
        public Texture2D spriteTexture;  
        public Rectangle rect;
        int FrameCount; //Количество всех фреймов в изображении (у нас это 8)
        int frame;//какой фрейм нарисован в данный момент
        float TimeForFrame;//Сколько времени нужно показывать один фрейм (скорость)
        double TotalTime;//сколько времени прошло с показа предыдущего фрейма
        bool isLeft;
        bool isRight;

        public Hero_anim(int speedAnimation)
        {   
            frame = 0;
            TimeForFrame = (float)1 / speedAnimation;
            TotalTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            FrameCount = spriteTexture.Width / spriteTexture.Height;
            TotalTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (TotalTime > TimeForFrame)
            {
                frame++;
                frame = frame % (FrameCount - 1);
                TotalTime -= TimeForFrame;
            }
            Rectangle next = rect;
            int dx = 3 * gameTime.ElapsedGameTime.Milliseconds / 10;
            if (isLeft)
            next.Offset(+dx, 0);
            else if (isRight)
            next.Offset(-dx, 0);
            if (!isLeft && !isRight)
            next.Offset(0, 0);   
            if(next.Left > 60 && next.Right < 780)
            rect = next;
        }

        public void LoadContent(ContentManager Content, String texture)
        {
            spriteTexture = Content.Load<Texture2D>(texture);  
        }

        public void DrawAnimation(SpriteBatch spriteBatch, bool Left, bool right)
        {
            isLeft = Left;
            isRight = right;  
            int frameWidth = spriteTexture.Width / FrameCount;
            Rectangle rectangle = new Rectangle(frameWidth * frame, 0, frameWidth, spriteTexture.Height);
            if (right)
            {
                SpriteEffects effect = new SpriteEffects();
                effect = SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(spriteTexture, rect, rectangle, Color.White, 0, Vector2.Zero, effect, 0);
            }
            else if(Left)
            spriteBatch.Draw(spriteTexture, rect, rectangle, Color.White);
            }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, rect, Color.White);
        } 
    }
}
