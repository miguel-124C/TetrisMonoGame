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
        private SoundEffect _levelUpSound;
        private SoundEffect _tetrisSound;
        private SoundEffect _gameOverSound;

        private SoundEffect _gameTheme;
        private SoundEffectInstance _instanceTheme;

        private bool IsPlayTheme = false;

        private readonly string pathSounds;

        public AudioManager(string pathSoundEffects)
        {
            this.pathSounds = pathSoundEffects;

            GameEvents.OnPieceMoved += PlayMoveSound;
            GameEvents.OnPieceRotated += PlayRotateSound;
            GameEvents.OnPieceHardDropped += PlayHardDropSound;
            GameEvents.OnLineClear += PlayLineClearSound;
            GameEvents.OnLevelUp += PlayLevelUpSound;
            GameEvents.OnTetris4Lines += PlayTetrisSound;
            GameEvents.OnTetrisThemePlay += PlayTetrisTheme;
            GameEvents.OnTetrisThemeTogglePause += TogglePauseTetrisTheme;
            GameEvents.OnGameOver += PlayGameOverSound;
        }

        public void LoadContent(ContentManager content)
        {
            _moveSound = content.Load<SoundEffect>($"{pathSounds}/move_piece");
            _rotateSound = content.Load<SoundEffect>($"{pathSounds}/rotate_piece");
            _hardDropSound = content.Load<SoundEffect>($"{pathSounds}/piece_landed");
            _lineClearSound = content.Load<SoundEffect>($"{pathSounds}/line_clear");
            _tetrisSound = content.Load<SoundEffect>($"{pathSounds}/tetris_4_lines");
            _levelUpSound = content.Load<SoundEffect>($"{pathSounds}/level_up");
            _gameTheme = content.Load<SoundEffect>($"{pathSounds}/tetrisTheme");
            _instanceTheme = _gameTheme.CreateInstance();
            _gameOverSound = content.Load<SoundEffect>($"{pathSounds}/game_over");
        }

        private void PlayRotateSound() => _rotateSound.Play();
        private void PlayHardDropSound() => _hardDropSound.Play();
        private void PlayMoveSound() => _moveSound.Play();
        private void PlayLineClearSound() => _lineClearSound.Play();
        private void PlayLevelUpSound() => _levelUpSound.Play();
        private void PlayTetrisSound() => _tetrisSound.Play();
        private void PlayTetrisTheme()
        {
            _instanceTheme.IsLooped = true;
            _instanceTheme.Play();
            _instanceTheme.Pause();
            IsPlayTheme = true;
        }

        private void TogglePauseTetrisTheme()
        {
            IsPlayTheme = !IsPlayTheme;
            
            if (IsPlayTheme)
                _instanceTheme.Pause();
            else
                _instanceTheme.Resume();
        }

        private void PlayGameOverSound()
        {
            _instanceTheme.Stop();
            _gameOverSound.Play();
        }
    }
}
