using Tetris.Enums;
using Tetris.Events;
using Tetris.Models;

namespace Tetris.Commands
{
    public class MoveCommand(GameState gameState, Direction direction, bool isSoftDrop = true) : ICommand
    {
        private readonly GameState _gameState = gameState;
        private readonly Direction _direction = direction;
        private readonly bool isSoftDrop = isSoftDrop;

        public void Execute()
        {
            var gameBoard = _gameState.GameBoard;
            var currentPiece = _gameState.CurrentPiece;

            var simulatedCoords = currentPiece.GetSimulatedMove(_direction);
            var hasCollision = gameBoard.HasCollision(simulatedCoords);

            if (!hasCollision)
            {
                _gameState.CurrentPiece.ApplyMove(simulatedCoords);

                if (_direction == Direction.Left || _direction == Direction.Right)
                    gameBoard.GhostCoords = _gameState.CalculateGhostPiece();

                if (isSoftDrop)
                {
                    GameEvents.TriggerPieceMoved();
                    if (_direction == Direction.Down)
                        _gameState.AddScore(1);
                }
            }

            if (_direction == Direction.Down)
            {
                if (hasCollision)
                    GameEvents.TriggerPieceTouchedFloor();
                else
                    GameEvents.TriggerResetGravityTimer();
            }
        }
    }
}