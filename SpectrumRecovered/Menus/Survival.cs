using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpectrumRecovered
{
    public class Survival : GameScreen
    {
       KeyboardState keyboardState;
        Texture2D image;
        Rectangle imageRectangle;
        Texture2D healtbar;
        Texture2D healthbar2;
        Game1 mygame;

        //camera stuff
        Camera camera;

        public Camera CameraSt
        {
            get { return camera; }
        }

        float scaleFactor;

        public Survival(Game game, SpriteBatch spriteBatch, Texture2D image) : base(game,spriteBatch)
        {

            this.image = image;
            mygame = (Game1)game;
            imageRectangle = new Rectangle(0, 0, mygame.GraphicsDevice.Viewport.Width, mygame.GraphicsDevice.Viewport.Height);// Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
           
        }


        public override void Initialize()
        {
       
            EnemyManager.Initialize(Game.Content.Load<Texture2D>(@"Images/Sprites/redIro"),Game.Content.Load<Texture2D>(@"Images/Sprites/blueIro"),Game.Content.Load<Texture2D>(@"Images/Sprites/greenIro"),Game.Content.Load<Texture2D>(@"Images/Sprites/yellowIro"),Game.Content.Load<Texture2D>(@"Images/WhiteGlowSprite"));
            CircleManager.InitialiseSurvival(Game.Content.Load<Texture2D>(@"Images/green"));
            AnimationManager.Initialize(Game.Content.Load<Texture2D>(@"Images/ExplosionSprite"));              
            ProjectileManager.Initialize(Game.Content.Load<Texture2D>(@"Images/projectile"));                    
            ScoreSystem.Initialize(Game.Content.Load<SpriteFont>(@"Fonts/menufont"),0);
            Player.Initialize(Game.Content.Load<Texture2D>(@"Images/Sprites/Player_Sprite_Sheet"), "player", new Vector2(640, 360),new Vector2(0,0),32,32);
            AnimationManager.Initialize(Game.Content.Load<Texture2D>(@"Images/ExplosionSprite"));
           
            
            healtbar = Game.Content.Load<Texture2D>(@"HUD/HealthBar");
            camera = new Camera(mygame.GraphicsDevice.Viewport);

            base.Initialize();
        }

      

        public override void Update(GameTime gameTime)
        {       
            #region main stuff
            Player.Update(gameTime);
            EnemyManager.Update(gameTime);
            ProjectileManager.Update(gameTime);
            CircleManager.Update(gameTime);
            AnimationManager.Update(gameTime);
            ScoreSystem.Update(gameTime);
            camera.Update(gameTime, mygame);


            #endregion 


            CircleManager.CurrentCircle.ModifyScale(scaleFactor);

            if (CircleManager.Circles[0].Scale > 2f)
                scaleFactor = -0.01f;
            else if (CircleManager.Circles[0].Scale <= 1f)
                scaleFactor = -0.001f;
      
            keyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            spriteBatch.Begin();
            spriteBatch.Draw(image, imageRectangle, null,Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0); //draws background of survival mode
            EnemyManager.Draw(gameTime, spriteBatch);
            CircleManager.Draw(gameTime, spriteBatch);
            ProjectileManager.Draw(gameTime, spriteBatch);
            AnimationManager.Draw(gameTime, spriteBatch);
            Player.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(healtbar, new Rectangle(20, 60, Player.health, 40), new Rectangle(0, 0, Player.Health, 24), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);       
            ScoreSystem.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            


 
            base.Draw(gameTime);
        }
    }
}


    