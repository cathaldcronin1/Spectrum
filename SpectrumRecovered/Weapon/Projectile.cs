using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectrumRecovered
{
    class Projectile
    {
        #region Variables
        protected Texture2D image;
        protected Vector2 pos;
        protected Vector2 direction;
        protected Vector2 centre;
        protected float speed;
        protected String name;
        protected float radius;
        protected bool destroyed;
        #endregion

        #region Properties
        public Vector2 Centre
        {
            get { return centre; }
            set { centre = value; }
        }

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public bool Destroyed
        {
            get { return destroyed; }
            set { destroyed = value; }
        }
        #endregion

        #region Constructor
        public Projectile(Texture2D image, String name, Vector2 pos, Vector2 direction, float speed)
        {
            this.image = image;
            this.name = name;
            this.pos = pos;
            this.speed = speed;
            this.direction = direction;
            this.centre = new Vector2((int)(pos.X + image.Width / 2), (int)(pos.Y + (image.Height / 2)));
            this.radius = image.Width / 2;
            destroyed = false;
        }
        #endregion

        #region Update & Draw
        public void Update(GameTime gameTime)
        {
            direction.Normalize();
            pos += direction * speed;

            centre.X = (pos.X + image.Width / 2);
            centre.Y = (pos.Y + image.Height / 2);

            if (pos.X > 1280 || pos.Y > 720  || pos.X < 0 || pos.Y < 0)
            {
                destroyed = true;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image,pos, null,Color.White, 0, Vector2.Zero,1, SpriteEffects.None, 1f);
        }
        #endregion

    }   
}
