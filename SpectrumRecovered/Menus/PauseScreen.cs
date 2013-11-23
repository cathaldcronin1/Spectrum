using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpectrumRecovered
{
    class PauseScreen : GameScreen
    {
        MenuComponent pauseComponents;
        Texture2D pauseImage;
        Rectangle pauseImageRectangle;

        public int SelectedIndex
        {
            get { return pauseComponents.SelectedIndex; }
            set { pauseComponents.SelectedIndex = value; }
        }

        public PauseScreen(Game game, SpriteBatch spritBatch, SpriteFont spriteFont, Texture2D image) : base(game,spritBatch)
        {
            string[] menuItems = { "Resume Game", "Quit to main menu" };
            pauseComponents = new MenuComponent(game, spriteBatch, spriteFont, menuItems);
            Components.Add(pauseComponents);
            this.pauseImage = image;

            pauseImageRectangle = new Rectangle((Game.Window.ClientBounds.Width - this.pauseImage.Width) / 2, (Game.Window.ClientBounds.Height - this.pauseImage.Height) / 2, this.pauseImage.Width, this.pauseImage.Height);
            pauseComponents.Position = new Vector2((pauseImageRectangle.Width - pauseComponents.Width) / 2 + 450, pauseImageRectangle.Bottom - pauseComponents.Height - 75);
           // pauseImageRectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(pauseImage, pauseImageRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
