using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpectrumRecovered.Menus;

namespace SpectrumRecovered
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //screens
        GameScreen activeScreen; //current screen
        StartScreen startScreen;
        GameOver EndScreen;
        Survival survivalGame;
        SurvialOver highscore;
        PauseScreen pauseMenu;
        PopUpScreen quitScreen;
        

        KeyboardState current, previous;

        //Screen bounding

        public static int screenWidth; // need to be properties
        public static int screenHeight;


      //  Menu Button and mouse 
       // public static Button btnPlay;
        MouseState mouse = Mouse.GetState(); 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            current = Keyboard.GetState();
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.IsFullScreen = true;
            this.graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            survivalGame = new Survival(this, spriteBatch, Content.Load<Texture2D>(@"Menu Images/background"));
            Components.Add(survivalGame);
            survivalGame.Hide();

            startScreen = new StartScreen(this, spriteBatch, Content.Load<SpriteFont>(@"Fonts/menufont"), Content.Load<Texture2D>(@"Menu Images/title")); //Content.Load<Texture2D>(@"Images/background"));
            Components.Add(startScreen);
            startScreen.Hide();

            highscore = new SurvialOver(this, spriteBatch, Content.Load<SpriteFont>(@"Fonts/menufont"), Content.Load<Texture2D>(@"Menu Images/title"));
            Components.Add(highscore);
            highscore.Hide();

            quitScreen = new PopUpScreen(this, spriteBatch, Content.Load<SpriteFont>(@"Fonts/menufont"), Content.Load<Texture2D>(@"Menu Images/quit"));
            Components.Add(quitScreen);
            quitScreen.Hide();

            pauseMenu = new PauseScreen(this, spriteBatch, Content.Load<SpriteFont>(@"Fonts/menufont"), Content.Load<Texture2D>(@"Menu Images/pause"));
            Components.Add(pauseMenu);
            pauseMenu.Hide();

          
            activeScreen = startScreen;
            activeScreen.Show();

            screenWidth = this.graphics.PreferredBackBufferWidth; // This is hardcoded needs to be changed later 
            screenHeight = this.graphics.PreferredBackBufferHeight ;

            IsMouseVisible = true;
           // btnPlay = new Button(Content.Load<Texture2D>(@"Menu Images/Button"), graphics.GraphicsDevice);
           // btnPlay.setPosisiton(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 60 , graphics.GraphicsDevice.Viewport.Height / 2 - 30));
            
            
        }

        protected override void Update(GameTime gameTime)
        {
            previous = current;
            current = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (activeScreen == startScreen)
            {
                HandleStartScreen();
            }     
            else if (activeScreen == survivalGame)
            {
                HandleSurvivalGame();        
            }
            else if (activeScreen == highscore)
            {
                HandleHighScoreScreen();
            }
            else if (activeScreen == pauseMenu)
            {
                HandlePauseScreen();
            }
            else if (activeScreen == quitScreen)
            {
                HandleQuitScreen();
            }

           
            base.Update(gameTime);


        }

        protected override void Draw(GameTime gameTime)
        {
 
            base.Draw(gameTime);
        }

        public bool CheckKey(Keys theKey)
        {
            return current.IsKeyUp(theKey) &&previous.IsKeyDown(theKey);
        }

        public void HandleStartScreen()
        {
             mouse = Mouse.GetState(); 

            //if (btnPlay.isClicked &&  btnPlay.previous.LeftButton == ButtonState.Released == true)
            //{
            //    activeScreen.Hide();
            //    activeScreen = survivalGame;
            //    survivalGame.Initialize();
            //    activeScreen.Show(); 
            //}

            //btnPlay.Update(mouse);
            if (CheckKey(Keys.Enter) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A))
            {

                //if (startScreen.SelectedIndex == 0)
                //{
                //    activeScreen.Hide();
                //    activeScreen = survivalGame;
                //    survivalGame.Initialize();
                //    activeScreen.Show();
                //}

                //Change to 1 if adding back in start game for the start screen 
                if (startScreen.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = survivalGame;
                    survivalGame.Initialize();
                    activeScreen.Show();
                }
            }
        }

        public void HandleSurvivalGame()
        {
            if (CheckKey(Keys.Escape) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.Start))
            {
                activeScreen.Enabled = false;
                activeScreen = pauseMenu;
                activeScreen.Show();
            }
            if (CheckKey(Keys.P))
            {
                activeScreen.Enabled = false;
                activeScreen = quitScreen;
                activeScreen.Show();
            }

            if (Player.Health <= 0)
            {
                activeScreen.Hide();
                activeScreen = highscore;
                CircleManager.Circles.Clear();
                EnemyManager.Enemies.Clear();
                ProjectileManager.Projectiles.Clear();
                activeScreen.Show();
            }   
        }

        public void HandlePauseScreen()
        {
            if (CheckKey(Keys.Enter) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A))
            {
                if (pauseMenu.SelectedIndex == 0)
                {
              
                   
                    activeScreen.Hide();
                    activeScreen = survivalGame;
                    activeScreen.Show();
                }
                if (pauseMenu.SelectedIndex == 1)
                {
                    survivalGame.Hide();
                    activeScreen.Hide();
                    activeScreen = startScreen;
                    CircleManager.Circles.Clear();
                    EnemyManager.Enemies.Clear();
                    ProjectileManager.Projectiles.Clear();
                    activeScreen.Show();
                }

            }
        }

        public void HandleQuitScreen()
        {
            if (CheckKey(Keys.Enter) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A))
            {
                if (quitScreen.SelectedIndex == 0)
                {
                    survivalGame.Hide();
                    activeScreen.Hide();
                    activeScreen = startScreen;
                    CircleManager.Circles.Clear();
                    EnemyManager.Enemies.Clear();
                    ProjectileManager.Projectiles.Clear();
                    activeScreen.Show();
                }
                if (quitScreen.SelectedIndex == 1)
                {
                    activeScreen.Hide();
                    activeScreen = survivalGame;
                    activeScreen.Show();
                }
            }
        }

        public void HandleHighScoreScreen()
        {
            if (CheckKey(Keys.Enter) || GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.A))
            {
                if (highscore.SelectedIndex == 0)
                {
                    activeScreen.Hide();
                    activeScreen = startScreen;
                    activeScreen.Show();
                }
            }
        }

        

     
    }
}
