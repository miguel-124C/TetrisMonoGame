using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tetris.Controllers;
using Tetris.Enums;
using Tetris.Helpers;
using Tetris.Managers;
using Tetris.Models;
using Tetris.Views;

namespace Tetris
{
    public class TetrisGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameState _gameState;
        private InputManager _inputManager;
        private AudioManager _audioManager;
        private GameLoopManager _gameLoopManager;

        private SceneManager _sceneRenderer;

        public TetrisGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            var board = new Board(20, 10, BlockColor.Empty);
            var pieceGenerator = new PieceGenerator();
            _gameState = new GameState(board, pieceGenerator);
            _inputManager = new InputManager(_gameState);
            _audioManager = new AudioManager("soundEffects");
            _sceneRenderer = new SceneManager(_gameState, _graphics);

            _ = new ScoreManager(_gameState);
            _gameLoopManager = new GameLoopManager(_gameState);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _audioManager.LoadContent(this.Content);
            _sceneRenderer.LoadContent(this.Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _inputManager.Update(gameTime);
            
            if (_gameState.CurrentStatus == GameStatus.Playing)
                _gameLoopManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _sceneRenderer.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

