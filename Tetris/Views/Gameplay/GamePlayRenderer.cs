using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tetris.Models;

namespace Tetris.Views.Gameplay
{
    public class GameplayRenderer(GameState gameState, GraphicsDeviceManager gdm, ContentManager content)
        : IRenderer(gdm, content)
    {
        private readonly BoardRenderer _boardRenderer = new(gameState, gdm, content);
        private readonly UIRenderer _uiRenderer = new(gameState, gdm, content);

        public override void LoadContent()
        {
            _boardRenderer.LoadContent();
            _uiRenderer.LoadContent();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _boardRenderer.Draw(spriteBatch, gameTime);
            _uiRenderer.Draw(spriteBatch, gameTime);
        }

        public override void UnLoad()
        {
            
        }
    }
}
