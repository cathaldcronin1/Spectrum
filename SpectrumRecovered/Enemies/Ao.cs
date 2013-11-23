using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectrumRecovered
{
    class Ao : Enemy
    {
        protected Vector2 dist = Vector2.Zero;
        protected Vector2 movement = Vector2.Zero;
        protected Rectangle clientBounds = new Rectangle(0, 0, 1280, 720);
       // float Angle = 0f;

        public Vector2 Dist
        {
            get { return dist; }
            set { dist = value; }
        }

        public Ao(Texture2D image,Texture2D glowImage, String name, Vector2 pos, Vector2 speed, float scale)
            : base(image,glowImage, name, pos, speed, 1)
        {
        }

        public override void Update(GameTime gameTime)
        {
            dist = Player.Position - this.pos;
            Angle = (float)Math.Atan2(dist.Y, dist.X);
            movement = new Vector2((float)Math.Cos(Angle) * Speed, (float)Math.Sin(Angle) * Speed);
            pos += (movement * 2);

        
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
         
   

            base.Draw(gameTime, spriteBatch);
        }
    }
}
