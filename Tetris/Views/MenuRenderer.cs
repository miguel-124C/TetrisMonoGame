using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tetris.Models;
using Tetris.Enums;

namespace Tetris.Views
{
    public class MenuRenderer
        (GameState gameState, GraphicsDeviceManager graphicsDeviceManager)
        : IRenderer
    {
        private readonly GameState _gameState = gameState;
        private readonly GraphicsDeviceManager _graphicsDeviceManager = graphicsDeviceManager;

        private Texture2D _backgroundMenu;
        private SpriteFont _spriteFont;

        public void LoadContent(ContentManager content)
        {
            _spriteFont = content.Load<SpriteFont>("Fonts/FontMain");
            _backgroundMenu = content.Load<Texture2D>("Backgrounds/menuBg");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (_gameState.CurrentStatus)
            {
                case GameStatus.Menu:
                    ShowMenu(spriteBatch);
                    break;
                case GameStatus.Paused:
                    ShowPause(spriteBatch);
                    break;
                case GameStatus.GameOver:
                    ShowGameOver(spriteBatch);
                    break;
            }
        }

        private void ShowMenu(SpriteBatch spriteBatch)
        {
            var titleGame = "Tetris";
            var instruction = "Press Enter to Start";

            var width = _graphicsDeviceManager.PreferredBackBufferWidth;
            var height = _graphicsDeviceManager.PreferredBackBufferHeight;

            var sizeTitle = _spriteFont.MeasureString(titleGame);
            var titleGameY = (height - sizeTitle.Y) / 2;
            var positionTitle = new Vector2((width - sizeTitle.X) / 2, titleGameY);

            var sizeInstruction = _spriteFont.MeasureString(instruction);
            var positionInstruction = new Vector2((width - sizeInstruction.X) / 2,
                (titleGameY + sizeTitle.Y) + 50);

            spriteBatch.Draw(_backgroundMenu, new Rectangle(0, 0, width, height), Color.White);

            spriteBatch.DrawString(_spriteFont, titleGame, positionTitle, Color.White);
            spriteBatch.DrawString(_spriteFont, instruction, positionInstruction, Color.White);
        }

        private void ShowPause(SpriteBatch spriteBatch)
        {

        }

        private void ShowGameOver(SpriteBatch spriteBatch)
        {
        }

        public void UnLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}
