using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectrumRecovered
{
    static class CircleManager
    {
        #region Variables
        public static List<Circle> circleList = new List<Circle>();
        public static Circle currentCircle;
        public static Texture2D red;
        public static Texture2D blue;
        public static Texture2D yellow;
        public static Texture2D green;
        #endregion

        #region Properties
        public static List<Circle> Circles
        {
            get { return circleList; }
        }

        public static Circle CurrentCircle
        {
            get { return currentCircle; }
            set { currentCircle = value; }
        }
        #endregion

        #region Initialize
        public static void Initialize(Texture2D Red,Texture2D Blue,Texture2D Yellow,Texture2D Green)
        {
            red = Red;
            blue = Blue;
            green = Green;
            yellow = Yellow;
            

            Circles.Add(new Circle(red, "red", new Vector2(200, 200), .5f));
            Circles.Add(new Circle(blue, "blue", new Vector2(500, 500), .5f));
        }

        public static void InitialiseSurvival(Texture2D green)
        {
           
            green = green;         
            Circles.Add(new Circle(green, "green", new Vector2(450,150),0.5f));
            Circles.Add(new Circle(green, "green", new Vector2(1500, 50), 0.5f));
        }
        #endregion

        #region Update & Draw
        public static void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            foreach(Circle c in Circles)
            {
                c.Draw(gameTime,spriteBatch);
            }
        }

        public static void Update(GameTime gameTime)
        {


           SetCurrentCircle();
           foreach (Circle c in Circles)
           {
               c.Update(gameTime);
               if (Vector2.Distance(Player.Centre, CurrentCircle.Centre) > CurrentCircle.Radius)
               {
                   Player.Health = 0;
               }
           }

         
        }
        #endregion

        #region Color Logic

        public static void SetCurrentCircle()
        {
            
            double smallest = Vector2.Distance(Circles[0].Centre,Player.Centre);
            CurrentCircle = circleList[0];

            for (int i = 1; i < Circles.Count; i++)
            {
                if (Vector2.Distance(Circles[i].Centre, Player.Centre) < smallest)
                {
                    smallest = Vector2.Distance(Circles[i].Centre, Player.Centre);
                    if (smallest < circleList[i].Radius)
                        currentCircle = circleList[i];
                }
            }
        }
        
        public static void ColorLogic(Circle c, Enemy e)
        {
            if (c.Name == e.Name)
            {
                c.ModifyScale(0.1f);
                ScoreSystem.Score += 100;
            }
            else
            {
                c.ModifyScale(0.1f);
                ScoreSystem.Score += 10;
            }
        }

        #endregion
    }
}
