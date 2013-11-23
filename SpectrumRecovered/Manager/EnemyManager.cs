using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpectrumRecovered
{
    static class EnemyManager 
    {
        #region Variables
        public static List<Enemy> enemyList = new List<Enemy>();
        public static int enemySpawnMinMilliseconds = 500;
        public static int enemySpawnMaxMilliseconds = 2000;
        public static int enemyMinSpeed = 2;
        public static int enemyMaxSpeed = 5;
        public static int nextSpawnTime = 0;
        public static int nextSpawnTimeChange = 2000;
        public static int timeSinceLastSpawnTimeChange = 0;
        public static Random rnd = new Random();

        //Graphic Stuff
        public static Texture2D red;
        public static Texture2D blue;
        public static Texture2D yellow;
        public static Texture2D green;
        public static Texture2D glowImage;
        #endregion

        #region Properties
        public static List<Enemy> Enemies
        {
            get { return enemyList; }
        }

        #endregion

        #region Initialize
        public static void Initialize(Texture2D Red,Texture2D Blue,Texture2D Green,Texture2D Yellow,Texture2D GlowImage)
        {
            red = Red;
            blue = Blue;
            green = Green;
            yellow = Yellow;
            glowImage = GlowImage;
        }
        #endregion

        #region Update & Draw

        public static void Update(GameTime gameTime)
        {
            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemies[i].Update(gameTime);
                if (Vector2.Distance(Player.Centre, Enemies[i].Centre) <= (Player.Radius * 2))
                {
                    Player.Health -= 50;
                    Enemies[i].Destroyed = true;
                }
                if (Enemies[i].Destroyed)
                {
                    Enemies.RemoveAt(i);
                }
           
            }
            Player.Health = (int)MathHelper.Clamp(Player.Health, 0, 256);

            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            AdjustSpawnTimes(gameTime);

            if (nextSpawnTime < 0)
            {
                if (enemyList.Count < 50)
                {
                    SpawnEnemy();
                    ResetSpawnTime();
                }
            }
        }

        public static void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(gameTime,spriteBatch);
            }
        }
        #endregion

        #region Other Methods
        private static void AdjustSpawnTimes(GameTime gameTime)
        {

            if (enemySpawnMaxMilliseconds > 500)
            {
                timeSinceLastSpawnTimeChange += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastSpawnTimeChange > nextSpawnTimeChange)
                {
                    timeSinceLastSpawnTimeChange -= nextSpawnTimeChange;
                    if (enemySpawnMaxMilliseconds > 1000)
                    {
                        enemySpawnMaxMilliseconds -= 100;
                        enemySpawnMinMilliseconds -= 100;
                    }
                    else
                    {
                        enemySpawnMaxMilliseconds -= 10;
                        enemySpawnMinMilliseconds -= 10;
                    }
                }
            }
        }

        private static void ResetSpawnTime()
        {
            // Set the next spawn time for an enemy
            nextSpawnTime = rnd.Next(
                enemySpawnMinMilliseconds,
                enemySpawnMaxMilliseconds);
        }

        private static void SpawnEnemy()
        {
            Vector2 speed = Vector2.Zero;
            Vector2 position = Vector2.Zero;

            // Default frame size
            Point frameSize = new Point(1, 1);

            switch (rnd.Next(4))
            {
                case 0: // LEFT to RIGHT
                    position = new Vector2(17, rnd.Next(17, 720));
                    speed = new Vector2(rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed), 1);
                    break;

                case 1: // RIGHT to LEFT
                    position = new Vector2(1280 - 32, rnd.Next(17, 720));

                    speed = new Vector2(-rnd.Next(
                        enemyMinSpeed, enemyMaxSpeed), 1);
                    break;
                case 2: // BOTTOM to TOP

                    position = new Vector2(rnd.Next(17, 1280), 688);

                    speed = new Vector2(-1,
                        -rnd.Next(enemyMinSpeed,
                        enemyMaxSpeed));
                    break;
                case 3: // TOP to BOTTOM

                    position = new Vector2(rnd.Next(17, 1280), 17);

                    speed = new Vector2(1,
                        rnd.Next(enemyMinSpeed,
                        enemyMaxSpeed));
                    break;
            }
            switch (rnd.Next(4))
            {
                case 0:
                    enemyList.Add(new Kiro(blue,glowImage, "blue", position, speed,1));
                    break;
                case 1:
                    enemyList.Add(new Ao(green,glowImage, "green", position, speed, 1));
                    break;
                case 2:
                    enemyList.Add(new Aka(red,glowImage, "red", position,  speed, 1));
                    break;
                case 3:
                   // enemyList.Add(new Midori(yellow,glowImage, "yellow", position,  speed, 1));
                    break;
            }
        }
        #endregion
    }
}
