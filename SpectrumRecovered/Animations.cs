using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpectrumRecovered
{
    public class Animations
    {
         protected Texture2D textureImage;
         protected Point frameSize = new Point(60,60);
         protected Point currentFrame;
         protected Point sheetSize = new Point(9,1);
         protected Vector2 pos;
         protected bool destroyed;
         protected int count = 0;

         protected int timeSinceLastFrame = 0;
         protected int millisecondsPerFrame = 90;

         public bool Destroyed
         {
             get { return destroyed; }
             set { destroyed = value; }
         }


         public Animations(Texture2D texture, Vector2 pos)
         {
             this.textureImage = texture;
             this.pos = pos;  
        
         }

        public  void Update(GameTime gameTime)
        {
            if (!destroyed && count < sheetSize.X * 2)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    // Increment to next frame
                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    if (currentFrame.X >= sheetSize.X)
                    {
                        currentFrame.X = 0;
                        ++currentFrame.Y;
                        if (currentFrame.Y >= sheetSize.Y)
                            currentFrame.Y = 0;
                    }
                }

            }
            else
            {
                destroyed = true;
            }
            count++;
           
        }


        public  void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(!destroyed)           
            spriteBatch.Draw(textureImage,pos, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y,frameSize.X, frameSize.Y),Color.White, 0, Vector2.Zero,1f, SpriteEffects.None, 1);
        }
    }
}



