using Tetris.Events;

namespace Tetris.Managers
{
    public class AudioManager
    {
        public AudioManager()
        {
            // Se suscribe UNA sola vez al inicio del juego
            GameEvents.OnPieceMoved += PlayMoveSound;
            GameEvents.OnPieceRotated += PlayRotateSound;
            GameEvents.OnPieceHardDropped += PlayDropSound;
            GameEvents.OnLineClear += PlayLineClearSound;
            GameEvents.OnTetris4Lines += PlayTetrisSound;
        }

        private void PlayRotateSound()
        {
        }

        private void PlayDropSound()
        {
        }

        private void PlayMoveSound()
        {
        }

        private void PlayLineClearSound()
        {
        }

        private void PlayTetrisSound()
        {
        }
    }
}
