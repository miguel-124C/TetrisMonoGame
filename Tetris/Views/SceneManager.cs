using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.Views
{
    public class SceneManager
    {
        public IRenderer CurrentRenderer { get; private set; }

        private GameState _gameState;
        private GraphicsDeviceManager _gdm;

        private MenuRenderer _menuRenderer;
        private GameplayRenderer _gamePlayRenderer;


        public SceneManager(GameState gameState, GraphicsDeviceManager gdm)
        {
            _gameState = gameState;
            _gdm = gdm;
            _gameState.OnStatusChanged += ChangeScene;

            _menuRenderer = new MenuRenderer(_gameState, _gdm);
            _gamePlayRenderer = new GameplayRenderer(_gameState, _gdm);

            ChangeScene(_gameState.CurrentStatus);
        }

        public void LoadContent(ContentManager content)
        {
            _menuRenderer.LoadContent(content);
            _gamePlayRenderer.LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_gameState.CurrentStatus == GameStatus.Paused
                || _gameState.CurrentStatus == GameStatus.GameOver
                || _gameState.CurrentStatus == GameStatus.Countdown
            )
            {
                // El juego
                CurrentRenderer.Draw(spriteBatch, gameTime);
            } else
                CurrentRenderer.Draw(spriteBatch, gameTime);
        }

        public void ChangeScene(GameStatus gameState)
        {
            CurrentRenderer = gameState switch
            {
                GameStatus.Menu => _menuRenderer,
                GameStatus.Paused => _menuRenderer,
                GameStatus.GameOver => _menuRenderer,
                GameStatus.Playing => _gamePlayRenderer,
                _ => throw new NotImplementedException($"Scene for {gameState} is not implemented yet.")
            };
        }
    }
}
