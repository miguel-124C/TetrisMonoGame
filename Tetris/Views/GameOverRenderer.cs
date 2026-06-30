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

            var instruction = "Press Enter to Resume - Esc to go Menu";
            var sizeInst = _spriteFont12.MeasureString(instruction);
            var posInst = new Vector2((_width - sizeInst.X) / 2, (_height - sizeInst.Y));
            spriteBatch.DrawString(_spriteFont12, instruction, posInst, Color.White * alpha);
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
