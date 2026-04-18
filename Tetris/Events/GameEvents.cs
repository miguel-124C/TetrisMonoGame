using System;

namespace Tetris.Events
{
    // Clase estática centralizada para eventos globales del juego
    public static class GameEvents
    {
        // Los eventos (La torre de radio)
        public static event Action OnPieceMoved;
        public static event Action OnPieceRotated;
        public static event Action OnPieceHardDropped;
        public static event Action OnLineClear;
        public static event Action OnTetris4Lines;
        public static event Action OnLevelUp;
        public static event Action OnGameOver;

        public static event Action OnTetrisTheme;

        public static event Action OnPieceLanded;
        public static event Action OnResetGravityTimer;
        public static event Action OnPieceTouchedFloor;

        // Métodos para "Gritar" el evento
        public static void TriggerPieceMoved() => OnPieceMoved?.Invoke();
        public static void TriggerPieceRotated() => OnPieceRotated?.Invoke();
        public static void TriggerPieceHardDropped() => OnPieceHardDropped?.Invoke();
        public static void TriggerLineClear() => OnLineClear?.Invoke();
        public static void TriggerTetris4Lines() => OnTetris4Lines?.Invoke();
        public static void TriggerLevelUp() => OnLevelUp?.Invoke();
        public static void TriggerTetrisTheme() => OnTetrisTheme?.Invoke();

        public static void TriggerPieceLanded() => OnPieceLanded?.Invoke();
        public static void TriggerResetGravityTimer() => OnResetGravityTimer?.Invoke();
        public static void TriggerPieceTouchedFloor() => OnPieceTouchedFloor?.Invoke();
        public static void TriggerGameOver() => OnGameOver?.Invoke();
    }
}