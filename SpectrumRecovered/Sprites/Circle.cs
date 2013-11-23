using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpectrumRecovered
{
    class Circle : Sprite
    {
        public float minScale = 0.1f;
        public float maxScale = 10f;

        public float MaxScale
        {
            get { return maxScale; }
           
        }

        public float MinScale
        {
            get { return minScale; }

        }
        public Circle(Texture2D image, String name, Vector2 pos, float scale)
            : base(image, name, pos,Vector2.Zero, scale)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();

            spriteBatch.Draw(image,
                pos, null,
                Color.White, 0, Vector2.Zero,
                scale, SpriteEffects.None, 0.2f);

            //spriteBatch.End();
        }

        public void ModifyScale(float mod)
        {


            ResetCentre();

            if (Scale < 0.1f)
                scale = 0.105f;
            else
                scale += mod;


            ResetCentre();
      
        }


        public void ResetCentre()
        {
            Vector2 oldCentre = centre;
            centre = new Vector2((int)(pos.X + image.Width / 2 * scale), (int)(pos.Y + (image.Height / 2 * scale)));
            float Xdistance = centre.X - oldCentre.X;
            float Ydistance = centre.Y - oldCentre.Y;
            pos.X -= Xdistance;
            pos.Y -= Ydistance;

        }
    }
}
