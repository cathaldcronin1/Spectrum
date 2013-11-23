using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpectrumRecovered
{
    class SurvialOver : GameScreen
    {
        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        public SurvialOver(Game game,SpriteBatch spriteBatch,SpriteFont spriteFont,Texture2D image): base(game, spriteBatch)
        {
            string[] menuItems = {"Well Done, your score is: "} ;

            menuComponent = new MenuComponent(game,spriteBatch,spriteFont,menuItems);
            Components.Add(menuComponent);
            this.image = image;

            imageRectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(image, imageRectangle,null, Color.White,0f,Vector2.Zero, SpriteEffects.None,1f);
            spriteBatch.DrawString(ScoreSystem.scoreFont, "" + ScoreSystem.score, new Vector2((Game.Window.ClientBounds.Width + 300) / 2, (Game.Window.ClientBounds.Height) / 2) , Color.Red, 0f, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
