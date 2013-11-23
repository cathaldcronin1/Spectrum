using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectrumRecovered
{
   public static class AnimationManager
    {


        #region Variables

        public static List<Animations> AnimationsList = new List<Animations>();
        public static Texture2D textureImage;
        public static Vector2 animationPos;
        public static int count;
       

        #endregion
        public static List<Animations> Animations
        {
            get { return AnimationsList; }
        }
        #region Properties

        #endregion

        #region Initialize

        public static void Initialize(Texture2D texture)
        {
            textureImage = texture;
            count = 0;
           
        }
        #endregion

        #region Update & Draw

        public static void Update(GameTime gameTime)
        {
            for (int i = AnimationsList.Count - 1; i >= 0; i--)
            {
                AnimationsList[i].Update(gameTime);
                if (AnimationsList[i].Destroyed == true)
                    AnimationsList.RemoveAt(i);
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Animations a in AnimationsList)
            {
                a.Draw(gameTime, spriteBatch);
            }
        }

        public static void Explosion(Vector2 pos)
        {
            AnimationsList.Add(new Animations(textureImage,new Vector2(pos.X - 14, pos.Y - 14)));
        }
        #endregion
    }
}

       