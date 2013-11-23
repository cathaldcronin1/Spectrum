using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectrumRecovered
{
    class Enemy : Sprite
    {
        #region Variables
        protected bool destroyed;
        protected float speed = 1f;
        protected double angle = 0f;
        protected Vector2 dist = Vector2.Zero;
        protected Vector2 movement = Vector2.Zero;

        // Animation stuff
        protected int timeSinceLastFrame = 0;
        protected int millisecondsPerFrame = 90;

        protected Texture2D glowImage;
        protected Point frameSize = new Point(50, 50);
        protected Point currentFrame = new Point(0, 0);
        protected Point sheetSize = new Point(9, 1);

        protected bool inCircle;
        #endregion

        #region Properties
        public bool InCircle
        {
            get { return inCircle; }
            set { inCircle = value; }
        }
        public Vector2 Dist
        {
            get { return dist; }
            set { dist = value; }
        }

        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public Vector2 Movement
        {
            get { return movement; }
            set { movement = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public bool Destroyed
        {
            get { return destroyed; }
            set { destroyed = value; }
        }
        #endregion

        #region Constructor
        public Enemy(Texture2D image,Texture2D glowImage, String name, Vector2 pos, Vector2 speed, float scale)
            : base(image, name, pos, speed, 1)
        {
            this.glowImage = glowImage;
            destroyed = false;
        }
        #endregion

        public override void Update(GameTime gameTime)
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image,pos, null,Color.White, 0, Vector2.Zero,scale, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(glowImage, pos - new Vector2(9, 9),new Rectangle(currentFrame.X * frameSize.X,currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y),Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0.1f);

        }

    }
}
