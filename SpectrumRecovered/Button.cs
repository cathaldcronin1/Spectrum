using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpectrumRecovered
{
   public class Button
    {
        Texture2D texture;
        Vector2 posistion;
        Rectangle rectangle;

        public MouseState current, previous;
        Color color = new Color(255, 255, 255, 255);

        public Vector2 size;



        public Button(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            //screen Width = 1280, screen Height = 720
            //image width = 100 image Height = 28;

            size = new Vector2(graphics.Viewport.Width / 12.8f, graphics.Viewport.Height / 25.71f);

        }

        bool down;

        public bool isClicked;

        public void Update(MouseState mouse)
        {
            previous = current;
            current = Mouse.GetState();

            rectangle = new Rectangle((int)posistion.X, (int)posistion.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (color.A == 255)
                    down = false;
                if (color.A == 0)
                    down = true;
                if (down)
                    color.A += 3;
                else
                    color.A -= 3;

                if (current.LeftButton == ButtonState.Pressed && previous.LeftButton == ButtonState.Released)
                    isClicked = true;
            }
            else if (color.A < 255)
            {
                color.A += 3;
                isClicked = false;
            }

        }

        public void setPosisiton(Vector2 newPosition)
        {
            posistion = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {           
            spriteBatch.Draw(texture, rectangle, null,color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

    }


}