using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris.Models;

namespace Tetris.Views
{
    public class GameplayRenderer(GameState gameState, GraphicsDeviceManager gdm) : IRenderer
    {
        private BoardRenderer _boardRenderer = new(gameState, gdm);
        private UIRenderer _uiRenderer = new(gameState, gdm);

        public void LoadContent(ContentManager content)
        {
            _boardRenderer.LoadContent(content);
            _uiRenderer.LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _boardRenderer.Draw(spriteBatch, gameTime);
            _uiRenderer.Draw(spriteBatch, gameTime);
        }

        public void UnLoad()
        {
            
        }
    }
}
