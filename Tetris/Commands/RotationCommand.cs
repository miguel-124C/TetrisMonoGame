using System;
using Tetris.Enums;
using Tetris.Events;
using Tetris.Models;

namespace Tetris.Commands
{
    public class RotationCommand(GameState gameState) : ICommand
    {
        private readonly GameState _gameState = gameState;

        public void Execute()
        {
            var currentPiece = _gameState.CurrentPiece;
            if (!currentPiece.CanRotate) return;

            var gameBoard = _gameState.GameBoard;
            var previousCoords = currentPiece.Cords;

            var simulatedCoords = currentPiece.GetSimulatedRotation();

            if (!gameBoard.HasCollision(simulatedCoords))
            {
                Rotate(currentPiece, simulatedCoords, gameBoard);
            }
            else // Si no se pudo rotar debido a una colisión, intentamos mover la pieza
            {    // horizontalmente en la dirección opuesta al borde más cercano y luego rotar nuevamente.
                var directionX = GetDirectionX(currentPiece);
                var directionToMove = directionX switch
                {
                    Direction.Left => Direction.Right,
                    Direction.Right => Direction.Left,
                    _ => throw new InvalidOperationException("Invalid direction for horizontal movement.")
                };

                var simulatedMoveCoords = currentPiece.GetSimulatedMove(directionToMove);
                currentPiece.ApplyMove(simulatedMoveCoords);
                var simulatedRotateCoords = currentPiece.GetSimulatedRotation();

                if (!gameBoard.HasCollision(simulatedRotateCoords))
                    Rotate(currentPiece, simulatedRotateCoords, gameBoard);
                else // Si aun asi no se pudo rotar, revertimos el movimiento horizontal para
                     // mantener la pieza en su posición original.
                    currentPiece.ApplyMove(previousCoords);
            }
        }

        private void Rotate(Piece currentPiece, Coordinate[] simulatedCoords, Board gameBoard)
        {
            currentPiece.ApplyMove(simulatedCoords);
            gameBoard.GhostCoords = _gameState.CalculateGhostPiece();
            GameEvents.TriggerPieceRotated();
        }

        private Direction GetDirectionX(Piece currentPiece)
        {
            var coordMaxLeft = currentPiece.GetCoordMaxLeft();
            var coordMaxRight = currentPiece.GetCoordMaxRight();

            var difference = (_gameState.GameBoard.Col - 1) - coordMaxRight;
            return (difference < coordMaxLeft) ? Direction.Right : Direction.Left;
        }
    }
}