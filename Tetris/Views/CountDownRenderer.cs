using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris.Models;

namespace Tetris.Views
{
    internal class CountDownRenderer
        (GameState gameState, GraphicsDeviceManager gdm, ContentManager cm) : IRenderer(gdm, cm)
    {
        private readonly GameState _gameState = gameState;

        public int CountDownSeconds { get; private set; } = 3;
        public float CountDownTimer { get; private set; } = 0;
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var secondsText = CountDownSeconds.ToString();
            var sizeText = _spriteFont48.MeasureString(secondsText);

            var positionSecondsText = new Vector2((_width - sizeText.X) / 2, 20);
            spriteBatch.DrawString(_spriteFont48, secondsText, positionSecondsText, Color.White);

            var deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            CountDownTimer += (float)deltaTime;

            if (CountDownTimer >= 1) // Si ha pasado un segundo, decrementamos el contador
            {
                CountDownSeconds--;
                CountDownTimer = 0;
                if (CountDownSeconds <= 0)
                {
                    _gameState.TogglePause();
                    CountDownSeconds = 3;
                }
            }
        }

        public override void LoadContent()
        {
            base.LoadBasicContent();
        }

        public override void UnLoad()
        {
            throw new NotImplementedException();
        }
    }
}
