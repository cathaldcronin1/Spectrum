using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpectrumRecovered.Menus
{
   class PopUpScreen : GameScreen
    {
        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        public PopUpScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image)
            : base(game, spriteBatch)
        {
            string[] menuItems = { "Yes", "No" };
            menuComponent = new MenuComponent(game, spriteBatch,spriteFont, menuItems);
            Components.Add(menuComponent);
            this.image = image;
            imageRectangle = new Rectangle((Game.Window.ClientBounds.Width - this.image.Width) / 2,(Game.Window.ClientBounds.Height - this.image.Height) / 2,this.image.Width,this.image.Height);
            menuComponent.Position = new Vector2( (imageRectangle.Width - menuComponent.Width) / 2 + 425,imageRectangle.Bottom - menuComponent.Height - 100);
            
               
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(image, imageRectangle, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

