using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Tetris.Commands;
using Tetris.Models;
using Tetris.Enums;

namespace Tetris.Controllers
{
    public class InputManager(GameState gameState)
    {
        private float ElapsedTime = 0;
        private readonly GameState _gameState = gameState;

        private KeyboardState _previousKeyboardState = Keyboard.GetState();

        public void Update(GameTime gameTime)
        {
            if (_gameState.CurrentStatus == GameStatus.Menu)
            {
                HandleInputInMenu();
            } else if (_gameState.CurrentStatus == GameStatus.Playing)
            {
                HandleInputInPlaying(gameTime);
            }
        }

        private void HandleInputInPlaying(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (IsKeyPressed(Keys.Left, currentKeyboardState))
            {
                ICommand moveLeft = new MoveCommand(_gameState, Direction.Left);
                moveLeft.Execute();
            }
            else if (IsKeyPressed(Keys.Right, currentKeyboardState))
            {
                ICommand moveRight = new MoveCommand(_gameState, Direction.Right);
                moveRight.Execute();
            }
            else if (IsKeyPressed(Keys.Up, currentKeyboardState))
            {
                ICommand rotate = new RotationCommand(_gameState);
                rotate.Execute();
            }
            else if (IsKeyPressed(Keys.Space, currentKeyboardState))
            {
                ICommand hardDrop = new HardDropCommand(_gameState);
                hardDrop.Execute();
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                ElapsedTime += deltaTime;

                if (ElapsedTime >= 0.05f)
                {
                    ElapsedTime -= 0.05f;
                    ICommand softDrop = new MoveCommand(_gameState, Direction.Down);
                    softDrop.Execute();
                }
            }
            else ElapsedTime = 0f;

            _previousKeyboardState = currentKeyboardState;
        }

        private void HandleInputInMenu()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            if (IsKeyPressed(Keys.Enter, currentKeyboardState))
                _gameState.StartGame();

            _previousKeyboardState = currentKeyboardState;
        }

        private bool IsKeyPressed(Keys key, KeyboardState currentState)
        {
            return currentState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }
    }
}
