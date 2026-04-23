using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris.Helpers;
using Tetris.Models;

namespace Tetris.Views.Gameplay
{
    internal class UIRenderer : IRenderer
    {
        private readonly GameState _gameState;

        private readonly int _blockSize = Constants.BlockSize;

        private readonly Vector2 _uiOffset = new(456, 19);
        private readonly int WidthUi = 150;
        private readonly int HeightUi = 442;

        private string _scoreText = "Score:\n000000";
        private string _linesText = "Lines:\n00";
        private string _levelText = "Level:\n00";

        private Piece _nextPiece;
        private Color colorBox = new(15,15,15);

        private readonly int WidthSquare = 140;
        private readonly int HeightSquare = 90;
        private readonly int offsetSquareX;

        public UIRenderer(GameState gameState, GraphicsDeviceManager gdm, ContentManager cm)
            : base(gdm, cm)
        {
            _gameState = gameState;
            _gameState.OnScoreChanged += UpdateScoreText;
            _gameState.OnLinesChanged += UpdateLinesText;
            _gameState.OnLevelChanged += UpdateLevelText;
            _gameState.OnNextPieceChanged += UpdateNextPiece;
            offsetSquareX = (int)_uiOffset.X + 5;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var position = new Rectangle((int)_uiOffset.X, (int)_uiOffset.Y, WidthUi, HeightUi);
            spriteBatch.Draw(_blankTexture, position, Color.Gray);

            DrawScore(spriteBatch);
            DrawNextPieceBox(spriteBatch);
            DrawLevel(spriteBatch);
            DrawLines(spriteBatch);
        }

        public override void LoadContent(){
            LoadBasicContent();
        }

        private void DrawScore(SpriteBatch spriteBatch)
        {
            var y = (int)_uiOffset.Y + 7;
            DrawBoxAndText(spriteBatch, offsetSquareX, y, WidthSquare, HeightSquare, _scoreText);
        }

        private void DrawNextPieceBox(SpriteBatch spriteBatch)
        {
            var y = (int)_uiOffset.Y + 103;

            var widthNextPieceBox = WidthSquare;
            var heightNextPieceBox = WidthSquare;

            DrawBox(spriteBatch, new Rectangle(offsetSquareX, y, widthNextPieceBox, heightNextPieceBox), colorBox);

            if (_nextPiece == null) return;

            int minX = _nextPiece.GetCoordMaxLeft();
            int maxX = _nextPiece.GetCoordMaxRight();
            int minY = _nextPiece.GetCoordMaxBottom();
            int maxY = _nextPiece.GetCoordMaxTop();

            var sizeBlock = _blockSize + 5;
            int pieceWidth = (maxX - minX + 1) * sizeBlock;
            int pieceHeight = (maxY - minY + 1) * sizeBlock;

            int startX = offsetSquareX + (widthNextPieceBox - pieceWidth) / 2;
            int startY = y + (heightNextPieceBox - pieceHeight) / 2;

            Color pieceColor = _nextPiece.Color.ToXnaColor();
            foreach (var cord in _nextPiece.Cords)
            {
                int drawX = startX + (cord.X - minX) * sizeBlock;
                int drawY = startY + (cord.Y - minY) * sizeBlock;

                DrawBox(spriteBatch, new Rectangle(drawX, drawY, sizeBlock - 2, sizeBlock - 2), pieceColor);
            }
        }

        private void DrawLevel(SpriteBatch spriteBatch)
        {
            var y = (int)_uiOffset.Y + 249;
            DrawBoxAndText(spriteBatch, offsetSquareX, y, WidthSquare, HeightSquare, _levelText);
        }

        private void DrawLines(SpriteBatch spriteBatch)
        {
            var y = (int)_uiOffset.Y + 345;
            DrawBoxAndText(spriteBatch, offsetSquareX, y, WidthSquare, HeightSquare, _linesText);
        }

        private void DrawBoxAndText(
            SpriteBatch spriteBatch, int x, int y, int width, int height, string text
        )
        {
            var position = new Rectangle(x, y, width, height);
            DrawBox(spriteBatch, position, colorBox);

            var heightText = _spriteFont18.MeasureString(text).Y;
            var positionText = new Vector2(position.X + 5, position.Y + (height - heightText) / 2);
            spriteBatch.DrawString(_spriteFont18, text, positionText, Color.White);
        }

        private void DrawBox(SpriteBatch spriteBatch, Rectangle position, Color color)
            => spriteBatch.Draw(_blankTexture, position, color);

        private void UpdateScoreText(int newScore) => _scoreText = $"Score:\n{newScore}";
        private void UpdateLinesText(int newLines) => _linesText = $"Lines:\n{newLines}";
        private void UpdateLevelText(int newLevel) => _levelText = $"Level:\n{newLevel}";
        private void UpdateNextPiece(Piece newNextPiece) => _nextPiece = newNextPiece;

        public override void UnLoad()
        {
            throw new NotImplementedException();
        }
    }
}