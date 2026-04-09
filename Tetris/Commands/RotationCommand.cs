using System;
using Tetris.Models;

namespace Tetris.Commands
{
    public class RotationCommand(GameState gameState) : ICommand
    {
        private readonly GameState _gameState = gameState;

        public event Action<bool> OnPieceRotate;

        public void Execute()
        {
            var gameBoard = _gameState.GameBoard;
            var currentPiece = _gameState.CurrentPiece;

            var simulatedCoords = currentPiece.GetSimulatedRotation();

            if (!gameBoard.HasCollision(simulatedCoords))
            {
                currentPiece.ApplyMove(simulatedCoords);

                var ghostCoords = _gameState.CalculateGhostPiece();
                gameBoard.GhostCoords = ghostCoords;

                OnPieceRotate?.Invoke(true); // Evento para reproducir sonido de rotación
            }
        }
    }
}