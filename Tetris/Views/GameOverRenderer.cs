using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris.Models;

namespace Tetris.Views
{
    internal class GameOverRenderer
        (GameState gameState, GraphicsDeviceManager gdm, ContentManager cm) : IRenderer(gdm, cm)
    {
        private readonly GameState _gameState = gameState;
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float alpha = (float)Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3));
            DrawModalWithTitle(spriteBatch, "Game Over", Color.White);

            var widthSquareInfo = 500;
            var heightSquareInfo = 300;
            spriteBatch.Draw(
                _blankTexture,
                new Rectangle((_width - widthSquareInfo) / 2, (_height - heightSquareInfo) / 2,
                widthSquareInfo, heightSquareInfo),
                Color.Black
            );

            var titleInfo = "Your Stats";
            var sizeTitleInfo = _spriteFont18.MeasureString(titleInfo);
            var positionTitleInfo = new Vector2((_width - sizeTitleInfo.X) / 2, (_height - heightSquareInfo) / 2 + 20);
            spriteBatch.DrawString(_spriteFont18, titleInfo, positionTitleInfo, Color.White);

            var statsText = $"Score: {_gameState.Score}\nLevel: {_gameState.Level}\nLines Cleared: {_gameState.Lines}";
            var sizeStatsText = _spriteFont18.MeasureString(statsText);
            var positionStatsText = new Vector2((_width - sizeStatsText.X) / 2, (_height - sizeStatsText.Y) / 2);
            spriteBatch.DrawString(_spriteFont18, statsText, positionStatsText, Color.White);

            DrawInstruccion(spriteBatch, "Press Enter to Restart", alpha, false);
            DrawInstruccion(spriteBatch, "Press Escape to Exit", alpha, true);
        }

        private void DrawInstruccion(SpriteBatch sb, string text, float alpha, bool isRight)
        {
            var offsetText = 10;
            var sizeText = _spriteFont18.MeasureString(text);
            var position = (isRight)
                ? new Vector2(_width - sizeText.X - offsetText, _height - sizeText.Y - offsetText)
                : new Vector2(offsetText, _height - sizeText.Y - offsetText);
            sb.DrawString(_spriteFont18, text, position, Color.White * alpha);
        }

        public override void LoadContent()
        {
            LoadBasicContent();
        }

        public override void UnLoad()
        {
            throw new NotImplementedException();
        }
    }
}
