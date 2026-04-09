using Tetris.Enums;
using Tetris.Events;
using Tetris.Models;

namespace Tetris.Commands
{
    public class MoveCommand(GameState gameState, Direction direction) : ICommand
    {
        private readonly GameState _gameState = gameState;
        private readonly Direction _direction = direction;

        public void Execute()
        {
            var gameBoard = _gameState.GameBoard;
            var currentPiece = _gameState.CurrentPiece;

            var simulatedCoords = currentPiece.GetSimulatedMove(_direction);

            if (!gameBoard.HasCollision(simulatedCoords))
            {
                _gameState.CurrentPiece.ApplyMove(simulatedCoords);

                if (_direction == Direction.Left || _direction == Direction.Right)
                    gameBoard.GhostCoords = _gameState.CalculateGhostPiece();

                GameEvents.TriggerPieceMoved();
            }
        }
    }
}
