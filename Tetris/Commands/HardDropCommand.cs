using System;
using Tetris.Events;
using Tetris.Models;

namespace Tetris.Commands
{
    public class HardDropCommand(GameState gameState) : ICommand
    {
        private readonly GameState _gameState = gameState;

        public void Execute()
        {
            var gameBoard = _gameState.GameBoard;
            var currentPiece = _gameState.CurrentPiece;

            var ghostCoords = gameBoard.GhostCoords;
            currentPiece.ApplyMove(ghostCoords);

            gameBoard.InsertPiece(currentPiece);
            gameBoard.CheckAndClearLines();

            _gameState.ChangePiece();

            GameEvents.TriggerPieceHardDropped();
        }
    }
}
