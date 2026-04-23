using Microsoft.Xna.Framework;
using Tetris.Commands;
using Tetris.Enums;
using Tetris.Events;
using Tetris.Models;

namespace Tetris.Controllers
{
    public class GameLoopManager
    {
        public float GravityTimer { get; private set; }
        public float TimeDrop { get; private set; }
        private readonly GameState _gameState;
        private readonly Board board;

        public bool IsLocking = false;
        private float LockTimer = 0f;
        private const float LockDelayLimit = 0.5f;

        public GameLoopManager(GameState gameState)
        {
            _gameState = gameState;
            board = _gameState.GameBoard;
            GravityTimer = 0f;
            TimeDrop = (float)_gameState.GetTimeDrop();

            // Cuando el juego comienza, reseteamos el timer de gravedad y cancelamos cualquier lock delay activo
            GameEvents.OnGameStarted += () =>
            {
                GravityTimer = 0f;
                IsLocking = false;
                CancelLockDelay();
            };

            _gameState.OnChangedTimeDrop += ChangeTimeDrop;
            GameEvents.OnResetGravityTimer += ()=> GravityTimer = 0f;
            GameEvents.OnPieceMoved += VerifyTouchingGround;
            GameEvents.OnPieceRotated += VerifyTouchingGround;

            GameEvents.OnPieceLanded += PieceLanded;
            GameEvents.OnPieceTouchedFloor += StartLockDelay;
        }

        public void Update(GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (IsLocking)
            {
                LockTimer += time;
                if (LockTimer >= LockDelayLimit)
                    PieceLanded();
                
                return;
            }

            GravityTimer += time;

            if (GravityTimer >= TimeDrop)
            {
                GravityTimer = 0;
                var softDrop = new MoveCommand(_gameState, Direction.Down, false);
                softDrop.Execute();
            }
        }

        private void StartLockDelay()
        {
            if (!IsLocking)
            {
                IsLocking = true;
                ResetLockTimer();
            }
        }

        private void PieceLanded()
        {
            board.InsertPiece(_gameState.CurrentPiece);
            board.CheckAndClearLines();
            _gameState.ChangePiece();
            CancelLockDelay();

            var coordsPiece = _gameState.CurrentPiece.Cords;
            var bornWithCollision = board.HasCollision(coordsPiece);
            
            if (bornWithCollision)
            {
                _gameState.TriggerGameOver();
                GameEvents.TriggerGameOver();
            }
        }

        private void VerifyTouchingGround()
        {
            var currentPiece = _gameState.CurrentPiece;
            if (IsLocking)
            {
                var downSimulated = currentPiece.GetSimulatedMove(Direction.Down);
                if (board.HasCollision(downSimulated))
                    ResetLockTimer();
                else
                    CancelLockDelay();
            }
        }

        private void ChangeTimeDrop(double newTime) => TimeDrop = (float)newTime;
        private void ResetLockTimer() => LockTimer = 0f;
        private void CancelLockDelay()
        {
            IsLocking = false;
            ResetLockTimer();
        }
    }
}