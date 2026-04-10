using System;
using Tetris.Enums;
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
            
            var maxTopPreviusDropped = currentPiece.GetCoordMaxTop();

            var ghostCoords = gameBoard.GhostCoords;
            currentPiece.ApplyMove(ghostCoords);

            var maxTopInGhost = currentPiece.GetCoordMaxTop();

            var amountOfRowsDropped = maxTopInGhost - maxTopPreviusDropped;
            _gameState.AddScore(amountOfRowsDropped * 2);

            gameBoard.InsertPiece(currentPiece);
            gameBoard.CheckAndClearLines();

            _gameState.ChangePiece();

            GameEvents.TriggerPieceHardDropped();
        }
    }
}
