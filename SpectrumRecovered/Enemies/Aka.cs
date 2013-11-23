using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectrumRecovered
{
    class Aka : Enemy
    {
        protected Vector2 speed = Vector2.Zero;
        protected Rectangle clientBounds = new Rectangle(0, 0, 1280, 720);

        public Aka(Texture2D image,Texture2D glowImage, String name, Vector2 pos, Vector2 speed, float scale)
            : base(image,glowImage, name, pos, speed, 1)
        {
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            pos.X += speed.X;
            pos.Y += speed.Y;
            //Reverse direction if hit a side
            if (pos.X > clientBounds.Width - image.Width || pos.X < 0)
                speed.X *= -1;
            if (pos.Y > clientBounds.Height - image.Height || pos.Y < 0)
                speed.Y *= -1;

            base.Update(gameTime);
        }
    }
}
