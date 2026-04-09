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

        // Métodos para "Gritar" el evento
        public static void TriggerPieceMoved() => OnPieceMoved?.Invoke();
        public static void TriggerPieceRotated() => OnPieceRotated?.Invoke();
        public static void TriggerPieceHardDropped() => OnPieceHardDropped?.Invoke();
        public static void TriggerLineClear() => OnLineClear?.Invoke();
        public static void TriggerTetris4Lines() => OnTetris4Lines?.Invoke();
    }
}