using Tetris.Events;
using Tetris.Models;

namespace Tetris.Commands
{
    public class RotationCommand(GameState gameState) : ICommand
    {
        private readonly GameState _gameState = gameState;

        public void Execute()
        {
            var gameBoard = _gameState.GameBoard;
            var currentPiece = _gameState.CurrentPiece;

            var simulatedCoords = currentPiece.GetSimulatedRotation();

            if (!gameBoard.HasCollision(simulatedCoords))
            {
                currentPiece.ApplyMove(simulatedCoords);
                gameBoard.GhostCoords = _gameState.CalculateGhostPiece();

                GameEvents.TriggerPieceRotated();
            }
        }
    }
}