using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectrumRecovered
{
    class Kiro : Enemy
    {
        #region Variables
        protected int pick;
        protected Vector2 speed = Vector2.Zero;
        protected List<Vector2> enemyMoves = new List<Vector2>();
        protected Vector2 move1 = new Vector2(2, 2); //up
        protected Vector2 move2 = new Vector2(-3, -3);//down
        protected Vector2 move3 = new Vector2(-2, 5);//left
        protected Vector2 move4 = new Vector2(1, 2);//right

        protected TimeSpan Wtimer;
        protected TimeSpan timer;
        protected Random rnd = new Random();
        protected Rectangle clientBounds = new Rectangle(0, 0, 1280, 720);
        #endregion

        public Kiro(Texture2D image,Texture2D glowImage, String name, Vector2 pos, Vector2 speed, float scale)
            : base(image,glowImage, name, pos, speed, 1)
        {
            this.speed = speed;
            timer = TimeSpan.FromSeconds(1f);

            enemyMoves.Add(move1);
            enemyMoves.Add(move2);
            enemyMoves.Add(move3);
            enemyMoves.Add(move4);
        }

        public override void Update(GameTime gameTime)
        {
            Position += speed;


            while (gameTime.TotalGameTime - Wtimer > timer)
            {
                Choice();
                Wtimer = gameTime.TotalGameTime;
            }

            if (pos.X > clientBounds.Width - image.Width || pos.X < 0)
                speed.X *= -1;
            if (pos.Y > clientBounds.Height - image.Height || pos.Y < 0)
                speed.Y *= -1;

            base.Update(gameTime);
        }

        public void Choice()
        {
            switch (rnd.Next(4))
            {
                case 0:
                    pick = 0;
                    break;
                case 1:
                    pick = 1;
                    break;
                case 2:
                    pick = 2;
                    break;
                case 3:
                    pick = 3;
                    break;
            }
            speed = enemyMoves[pick];

        }
    }
}
