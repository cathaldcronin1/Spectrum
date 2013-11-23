using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpectrumRecovered
{
    class Midori : Enemy
    {
        public Midori(Texture2D image,Texture2D glowImage, String name, Vector2 pos, Vector2 speed, float scale)
            : base(image,glowImage, name, pos, speed, 1)
        {
        }
    }
}
