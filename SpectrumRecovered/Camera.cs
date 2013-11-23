using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpectrumRecovered
{
    public class Camera
    {
        public Matrix transform; //used to draw camera on screen
        Viewport view; //where the camera is looking
        Vector2 centre; //on player


        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            centre = new Vector2(Player.Position.X + (Player.Frame.Width / 2 - 662), (Player.Position.Y + (Player.Frame.Height / 2 - 383)));
            transform = Matrix.CreateScale(new Vector3(1,1,0)) * Matrix.CreateTranslation(new Vector3(-centre.X,-centre.Y,0));
    }
    }
}
