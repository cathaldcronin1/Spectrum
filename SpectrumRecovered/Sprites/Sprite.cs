using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpectrumRecovered
{
    abstract class Sprite
    {
        #region Variables
        protected float radius;
        public float scale;
        protected Vector2 pos;
        protected Vector2 centre;
        protected Vector2 speed; 
        protected Texture2D image;
        protected String name;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public Vector2 Centre
        {
            get { return centre; }
            private set { centre = value; }
        }

        public Vector2 Speed
        {
            get { return speed; }
            private set { speed = value; }
        }

        public String Name
        {
            get { return name; }
            private set { name = value; }
        }

        public float Radius
        {
            get { return radius; }
            private set { radius = value; }
        }

        public float Scale
        {
            get { return scale; }
            private set { scale = value; }
        }
        #endregion

        #region Contructor
        public Sprite(Texture2D image, String name, Vector2 pos,Vector2 speed, float scale)
        {
            this.image = image;
            this.name = name;
            this.pos = pos;
            this.speed = speed;          
            this.scale = scale;
            this.centre = new Vector2((int)(pos.X + image.Width / 2), (int)(pos.Y + (image.Height / 2 )));
            this.radius = (image.Width / 2);
        }
        #endregion

        #region Update & Draw
        public virtual void Update(GameTime gameTime)
        {
            Centre = new Vector2((int)(pos.X + image.Width / 2 * scale), (int)(pos.Y + (image.Height / 2 * scale)));
            Radius = (image.Width / 2 * scale);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(image,pos, null,Color.White, 0, Vector2.Zero,scale, SpriteEffects.None, 0);

        }
        #endregion

        #region Other Methods
        
        #endregion
    }
}
