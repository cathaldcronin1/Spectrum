using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpectrumRecovered
{
    static class Player
    {
        #region Variables
        public static float radius;
        public  static Vector2 pos;
        public static Vector2 centre;
        public static Vector2 speed;
        public static Texture2D image;
        public static String name;
        public static Vector2 originalPos;
        public static int health;
        private static Rectangle scrollArea = new Rectangle(150, 100, 500, 400);
        private static Vector2 keyMovement;

        // Drawing the player moving//
        private static Point currentFrame;
        private static int frameWidth;
        private static int frameHeight;
        private static Rectangle rectangle;

        private static float timer;
        private static  float interval = 100f;

        public static int timeSinceLastFrame = 75;
        private static int millisecondsPerFrame = 75;

        //roatition for player
        private static float rotation = 0f;

        //Friction for player
        private static float friction = 0.3f;

        private static TimeSpan TargetElapsedTime;

        private static bool isLeft = false;// false meaning its right 
        private static float gpsX;
        private static float gpsY;

        #endregion

        #region Properties

        public static Rectangle Frame
        {
            get { return rectangle; }
        }

        public static Vector2 KeyMoves
        {
            get { return keyMovement; }
            set { keyMovement = value; }
        }

        public static int Health
        {
            get { return health; }
            set { health = value; }
        }

        public static Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public static Vector2 Centre
        {
            get { return centre; }
            private set { centre = value; }
        }

        public static Vector2 Speed
        {
            get { return speed; }
            private set { speed = value; }
        }

        public static String Name
        {
            get { return name; }
            private set { name = value; }
        }

        public static float Radius
        {
            get { return radius; }
            private set { radius = value; }
        }

     
        #endregion

        #region Initialize

        public static void Initialize(Texture2D image1, String name1, Vector2 pos1, Vector2 speed1, int newFrameHeight, int newFrameWidth)
        {
          
            image = image1;
            name = name1;
            pos = pos1;
            speed = speed1;
            centre = new Vector2((int)(pos.X + (newFrameWidth / 2)), (int)(pos.Y + (newFrameHeight / 2)));
            radius = newFrameWidth / 2;
            originalPos = pos;
            health = 256;
            frameHeight = newFrameHeight;
            frameWidth = newFrameWidth;

            TargetElapsedTime = new TimeSpan(0, 0, 0, 0,75);
          

           
        }
        #endregion

        #region Update & Draw
        public static void Update(GameTime gameTime)
        {
       
            rectangle = new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight, frameWidth, frameHeight);
            HandleInput(gameTime);
           
            Player.Centre = new Vector2(Position.X + frameWidth / 2, Position.Y + frameHeight / 2);

             if (keyMovement != Vector2.Zero) // as long as there's movement subtract form the velocity of the player
            {
                Vector2 i = keyMovement;
                keyMovement = i -= friction * i;
            }
             GamePadAnimations(gameTime);

        }

        public static void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, pos, rectangle, Color.White, rotation, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
        }
        #endregion

        #region Input

      

        //Added gametime to method for the player animatinons
        private static Vector2 HandleKeyboardMovement(KeyboardState keyState,GameTime gameTime)
        {
           // keyMovement = Vector2.Zero;
            if (keyState.IsKeyDown(Keys.W))
            {
                if (Position.Y <= 0)                
                   keyMovement.Y = 0;               
                else
                   keyMovement.Y--;

                AnimateUp(gameTime);
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                if (Position.X <= 0)
                    keyMovement.X = 0;
                else
                    keyMovement.X--;

                isLeft = true;             
                AnimateLeft(gameTime);    
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                if (Position.Y >= Game1.screenHeight - Player.frameHeight)
                    keyMovement.Y = 0; 
                else
                    keyMovement.Y++;

               
                AnimateDown(gameTime);
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                if (Position.X >= Game1.screenWidth - Player.frameWidth)
                    keyMovement.X = 0;
                else
                    keyMovement.X++;

                isLeft = false;
                AnimateRight(gameTime); 
            }

            if (keyState.IsKeyDown(Keys.D) && (keyState.IsKeyDown(Keys.W)))
            {
                AnimateUpRight(gameTime);
            }
            if (keyState.IsKeyDown(Keys.A) && (keyState.IsKeyDown(Keys.W)))
            {
                AnimateUpLeft(gameTime);
            }

            if (keyState.IsKeyDown(Keys.S) && (keyState.IsKeyDown(Keys.A)))
            {
                AnimateDownLeft(gameTime);
            }
            if (keyState.IsKeyDown(Keys.S) && (keyState.IsKeyDown(Keys.D)))
            {
                AnimateDownRight(gameTime);
            }

            if (isLeft && keyMovement == Vector2.Zero)
            {
               AnimateIdleLeft(gameTime);
            }
            if(!isLeft && (keyMovement == Vector2.Zero))
              AnimateIdleRight(gameTime);


          
            
            return keyMovement;
        }

        private static void AnimateIdleRight(GameTime gameTime)
        {
            if (currentFrame.Y != 1)
            {
                currentFrame.Y = 1;
                currentFrame.X = 0;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X == 7)
                {
                    currentFrame.X = 0;
                }
            } 
        }

        private static void AnimateIdleLeft(GameTime gameTime)
        {
            if (currentFrame.Y != 0)
            {
                currentFrame.Y = 0;
                currentFrame.X = 0;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X == 7)
                {
                    currentFrame.X = 0;
                }
            } 
        }

        private static void AnimateUpRight(GameTime gameTime)
        {
            if (currentFrame.Y !=1 )
            {
                currentFrame.Y = 1;
                currentFrame.X = 0;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                --currentFrame.X;
                if (currentFrame.X == 7)
                {
                    currentFrame.X = 0;
                }
            }  
        }

        private static void AnimateUpLeft(GameTime gameTime)
        {
            if (currentFrame.Y != 7)
            {
                currentFrame.Y = 7;
                currentFrame.X = 5;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                --currentFrame.X;
                if (currentFrame.X == 0)
                {
                    currentFrame.X = 5;
                }

            }
        }

        private static void AnimateDownLeft(GameTime gameTime)
        {
            if (currentFrame.Y != 5)
            {
                currentFrame.Y = 5;
                currentFrame.X = 4;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                --currentFrame.X;
                if (currentFrame.X == 0)
                {
                    currentFrame.X = 4;
                }
            }
        }

        private static void AnimateDownRight(GameTime gameTime)
        {
            if (currentFrame.Y != 3)
            {
                currentFrame.Y = 3;
                currentFrame.X = 0;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                ++currentFrame.X;
                if (currentFrame.X == 4)
                {
                    currentFrame.X = 0;
                }
            }
        }

        private static void AnimateLeft(GameTime gameTime)
        {
            if (currentFrame.Y != 6)
            {
                currentFrame.Y = 6;
                currentFrame.X = 5;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                --currentFrame.X;
                if (currentFrame.X == 0)
                {
                    currentFrame.X = 5;
                }

            }
        }

        private static void AnimateRight(GameTime gameTime)
        {
            if (currentFrame.Y !=2)
            {
                currentFrame.Y = 2;
                currentFrame.X = 0;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
             { 
                timeSinceLastFrame -= millisecondsPerFrame;
               
                ++currentFrame.X;
                if (currentFrame.X == 5)
                {
                    currentFrame.X = 0;
                }
            }
        }

        private static void AnimateUp(GameTime gameTime)
        {
            if (currentFrame.Y != 8)
            {
                currentFrame.Y = 8;
                currentFrame.X = 0;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                ++currentFrame.X;
                if (currentFrame.X == 3)
                {
                    currentFrame.X = 0;
                }
            }
        }

        private static void AnimateDown(GameTime gameTime)
        {
            if (currentFrame.Y != 4)
            {
                currentFrame.Y = 4;
                currentFrame.X = 0;
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                ++currentFrame.X;
                if (currentFrame.X == 4)
                {
                    currentFrame.X = 0;
                }
            }
        }
        

        private static Vector2 HandleGamePadMovement(GamePadState gamepadState,GameTime gameTime)
        {         
            return new Vector2(gamepadState.ThumbSticks.Left.X, -gamepadState.ThumbSticks.Left.Y);

        }

        public static Vector2 HandleKeyboardShots(KeyboardState keyState)
        {
            Vector2 keyShots = Vector2.Zero;

            if (keyState.IsKeyDown(Keys.NumPad1))
                keyShots = new Vector2(-1, 1);

            if (keyState.IsKeyDown(Keys.NumPad2))
                keyShots = new Vector2(0, 1);

            if (keyState.IsKeyDown(Keys.NumPad3))
                keyShots = new Vector2(1, 1);

            if (keyState.IsKeyDown(Keys.NumPad4))
                keyShots = new Vector2(-1, 0);

            if (keyState.IsKeyDown(Keys.NumPad6))
                keyShots = new Vector2(1, 0);

            if (keyState.IsKeyDown(Keys.NumPad7))
                keyShots = new Vector2(-1, -1);

            if (keyState.IsKeyDown(Keys.NumPad8))
                keyShots = new Vector2(0, -1);

            if (keyState.IsKeyDown(Keys.NumPad9))
                keyShots = new Vector2(1, -1);

            return keyShots;
        }

        public static Vector2 HandleGamePadShots(GamePadState gamepadState)
        {
            return new Vector2(gamepadState.ThumbSticks.Right.X, -gamepadState.ThumbSticks.Right.Y);
        }

        private static void HandleInput(GameTime gameTime)
        {
            pos += 5f * HandleKeyboardMovement(Keyboard.GetState(),gameTime);
            pos += 5f * HandleGamePadMovement(GamePad.GetState(PlayerIndex.Two),gameTime);

        }

 

        public static void GamePadAnimations(GameTime gameTime) // call in update
        {
            gpsX = GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X;
            gpsY = GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y;

            if (gpsX < 0)
            {
                AnimateLeft(gameTime);
            }
            else if (gpsX > 0)
            {
                AnimateRight(gameTime);
            }
            else if (gpsY > 0)
            {
                AnimateUp(gameTime);
            }
            else if (gpsY < 0)
            {
                AnimateDown(gameTime);
            }
            else if (gpsX < 0 && gpsY > 0) // Up Left
            {
                AnimateUpLeft(gameTime);
            }
            else if (gpsX < 0 && gpsY < 0) // Down Left
            {
                AnimateDownLeft(gameTime);
            }
            else if (gpsX > 0 && gpsY > 0) // Up Right
            {
                AnimateUpRight(gameTime);
            }
            else if (gpsX > 0 && gpsY < 0) // Down Right
            {
                AnimateDownRight(gameTime);
            }
        }


        #endregion

    }
}
