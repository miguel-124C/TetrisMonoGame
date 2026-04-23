using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tetris.Enums;
using Tetris.Models;
using Tetris.Views.Gameplay;

namespace Tetris.Views
{
    public class SceneManager
    {
        private readonly GameState _gameState;

        private readonly MenuRenderer _menuRenderer;
        private readonly GameOverRenderer _gameOverRenderer;
        private readonly CountDownRenderer _countDownRenderer;
        private readonly GameplayRenderer _gamePlayRenderer;
        private readonly PauseRenderer _pauseRenderer;

        public SceneManager(GameState gameState, GraphicsDeviceManager gdm, ContentManager content)
        {
            _gameState = gameState;
            //_gameState.OnStatusChanged += ChangeScene;

            _menuRenderer = new MenuRenderer(gdm, content);
            _gamePlayRenderer = new GameplayRenderer(_gameState, gdm, content);
            _gameOverRenderer = new GameOverRenderer(_gameState, gdm, content);
            _countDownRenderer = new CountDownRenderer(_gameState, gdm, content);
            _pauseRenderer = new PauseRenderer(gdm, content);
        }

        public void LoadContent()
        {
            _menuRenderer.LoadContent();
            _gamePlayRenderer.LoadContent();
            _gameOverRenderer.LoadContent();
            _countDownRenderer.LoadContent();
            _pauseRenderer.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (_gameState.CurrentStatus)
            {
                case GameStatus.Menu:
                    _menuRenderer.Draw(spriteBatch, gameTime);
                    break;
                case GameStatus.Playing:
                    _gamePlayRenderer.Draw(spriteBatch, gameTime);
                    break;
                case GameStatus.Paused:
                    _gamePlayRenderer.Draw(spriteBatch, gameTime);
                    _pauseRenderer.Draw(spriteBatch, gameTime);
                    break;
                case GameStatus.GameOver:
                    _gamePlayRenderer.Draw(spriteBatch, gameTime);
                    _gameOverRenderer.Draw(spriteBatch, gameTime);
                    break;
                case GameStatus.Countdown:
                    _gamePlayRenderer.Draw(spriteBatch, gameTime);
                    _countDownRenderer.Draw(spriteBatch, gameTime);
                    break;
            }
        }
    }
}