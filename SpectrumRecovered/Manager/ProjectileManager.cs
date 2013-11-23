using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpectrumRecovered
{
    static class ProjectileManager
    {
        #region Variables
        public static List<Projectile> projectileList = new List<Projectile>();
        public static TimeSpan weaponTime;
        public static TimeSpan timer;
        public static Texture2D image;
        #endregion

        #region Properties
        public static List<Projectile> Projectiles
        {
            get { return projectileList; }
        }
        #endregion

        #region Initialize
        public static void Initialize(Texture2D Image)
        {
            image = Image;
            timer = TimeSpan.FromSeconds(.10f);
        }
        #endregion

        #region Update & Draw
        public static void Update(GameTime gameTime)
        {
            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                Projectiles[i].Update(gameTime);
                if (Projectiles[i].Destroyed)
                {
                    Projectiles.RemoveAt(i);
                }
            }
            CheckCollisions();
            if (Player.HandleGamePadShots(GamePad.GetState(PlayerIndex.Two)) != Vector2.Zero
                || Player.HandleKeyboardShots(Keyboard.GetState()) != Vector2.Zero)
            {
                if (gameTime.TotalGameTime - weaponTime > timer)
                {
                    Vector2 shootAngle = Vector2.Zero;
                    shootAngle += Player.HandleGamePadShots(GamePad.GetState(PlayerIndex.Two));
                    shootAngle += Player.HandleKeyboardShots(Keyboard.GetState());
                    Shoot(shootAngle);
                    weaponTime = gameTime.TotalGameTime;
                }
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Projectile enemy in Projectiles)
            {
                enemy.Draw(gameTime, spriteBatch);
            }
        }
        #endregion

        #region Collisions
        public static void CheckCollisions()
        {
            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                for (int j = EnemyManager.Enemies.Count - 1; j >= 0; j--)
                {
                    if(OnTarget(EnemyManager.Enemies[j],Projectiles[i]))
                    {
                       CircleManager.ColorLogic(CircleManager.CurrentCircle, EnemyManager.Enemies[j]);
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position - new Vector2(2,2));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position + new Vector2(2, 2));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position - new Vector2(20, 0));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position + new Vector2(20, 0));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position - new Vector2(0, 20));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position + new Vector2(0, 20));
                       //AnimationManager.Explosion(EnemyManager.Enemies[j].Position - new Vector2(20, 20));
                       //AnimationManager.Explosion(EnemyManager.Enemies[j].Position + new Vector2(20, 20));
                       //AnimationManager.Explosion(EnemyManager.Enemies[j].Position - new Vector2(-20, 20));
                      // AnimationManager.Explosion(EnemyManager.Enemies[j].Position + new Vector2(20, -20));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position - new Vector2(10, 0));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position + new Vector2(10, 0));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position - new Vector2(0, 10));
                       AnimationManager.Explosion(EnemyManager.Enemies[j].Position + new Vector2(0, 10));
                       EnemyManager.Enemies[j].Destroyed = true;
                       Projectiles[i].Destroyed = true;
                       ScoreSystem.addScore();
                      
                        break;
                    }
                }
            }
            
        }
        #endregion

        #region Other Methods
        public static bool OnTarget(Enemy e, Projectile p)
        {
            float dist = Vector2.Distance(e.Centre, p.Centre);
            if (dist <= (e.Radius + p.Radius))
                return true;
            else
                return false;
        }

        public static void Shoot(Vector2 shootAngle)
        {
            shootAngle.Normalize();
            Projectiles.Add(new Projectile(image, "shot", Player.Centre, shootAngle, 10));
        }
        #endregion

        
    }
}
