using System;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.Commands
{
    public class MoveCommand(GameState gameState, Direction direction) : ICommand
    {
        private readonly GameState _gameState = gameState;
        private readonly Direction _direction = direction;

        public event Action<bool> OnPieceMove;

        public void Execute()
        {
            var gameBoard = _gameState.GameBoard;
            var currentPiece = _gameState.CurrentPiece;

            var simulatedCoords = currentPiece.GetSimulatedMove(_direction);

            if (!gameBoard.HasCollision(simulatedCoords))
            {
                _gameState.CurrentPiece.ApplyMove(simulatedCoords);

                if (_direction == Direction.Left || _direction == Direction.Right)
                {
                    var ghostCoords = _gameState.CalculateGhostPiece();
                    gameBoard.GhostCoords = ghostCoords;

                }

                OnPieceMove?.Invoke(true); // Evento para reproducir sonido de movimiento
            }
        }
    }
}
