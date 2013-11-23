using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace SpectrumRecovered
{
   static class ScoreSystem
   {
       #region Vairables 
          public static int score;
          public static SpriteFont scoreFont ;
          public static Vector2 scorePos;
          public static string scoreTxt;

     

          public static int Score
          {
              get { return score; }
              set { score = value; }
          }
         
       #endregion

          #region Initialize

              public static void Initialize(SpriteFont spriteFont, int scoreI)
            {
                scoreFont = spriteFont;
                score = scoreI;

                scorePos = new Vector2(20, 30);
                  
            }

          #region loadContent
            
         #endregion 

       #endregion

              #region Update & Draw
              public static void Update(GameTime gametime)
              {
                  //addScore();
              }

             
              public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
              {
                  spriteBatch.DrawString(scoreFont, "Score: " + score, scorePos, Color.Red, 0f, Vector2.Zero, 1, SpriteEffects.None, 1);                            
              }

              public static void addScore()
              {
                  score += 100;
              }
       
              #endregion




   }
}

