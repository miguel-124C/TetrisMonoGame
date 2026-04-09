using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Tetris.Events;

namespace Tetris.Managers
{
    public class AudioManager
    {
        private SoundEffect _moveSound;
        private SoundEffect _rotateSound;
        private SoundEffect _hardDropSound;
        private SoundEffect _lineClearSound;
        private SoundEffect _tetrisSound;

        private readonly string pathSounds;

        public AudioManager(string pathSoundEffects)
        {
            this.pathSounds = pathSoundEffects;

            GameEvents.OnPieceMoved += PlayMoveSound;
            GameEvents.OnPieceRotated += PlayRotateSound;
            GameEvents.OnPieceHardDropped += PlayHardDropSound;
            GameEvents.OnLineClear += PlayLineClearSound;
            GameEvents.OnTetris4Lines += PlayTetrisSound;
        }

        public void LoadContent(ContentManager content)
        {
            _moveSound = content.Load<SoundEffect>($"{pathSounds}/move_piece");
            _rotateSound = content.Load<SoundEffect>($"{pathSounds}/rotate_piece");
            _hardDropSound = content.Load<SoundEffect>($"{pathSounds}/piece_landed");
            _lineClearSound = content.Load<SoundEffect>($"{pathSounds}/line_clear");
            _tetrisSound = content.Load<SoundEffect>($"{pathSounds}/tetris_4_lines");
        }

        private void PlayRotateSound() => _rotateSound.Play();
        private void PlayHardDropSound() => _hardDropSound.Play();
        private void PlayMoveSound() => _moveSound.Play();
        private void PlayLineClearSound() => _lineClearSound.Play();
        private void PlayTetrisSound() => _tetrisSound.Play();
    }
}
