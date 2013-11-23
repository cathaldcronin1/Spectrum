using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpectrumRecovered
{
    public class StartScreen : GameScreen
    {
        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;

  

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

           // btnPlay = new Button(Game.Content.Load<Texture2D>(@"Images/Button"), Game.GraphicsDevice);
           // btnPlay.setPosisiton(new Vector2(0, 0));
        }

     
        public StartScreen(Game game,SpriteBatch spriteBatch,SpriteFont spriteFont,Texture2D image): base(game, spriteBatch)
        {
            string[] menuItems = {"Start Game", "End Game"};

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
            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.NonPremultiplied);
            spriteBatch.Draw(image, imageRectangle,null, Color.White,0f,Vector2.Zero, SpriteEffects.None,1f);
            //Game1.btnPlay.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
