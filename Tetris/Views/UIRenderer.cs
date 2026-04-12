using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tetris.Helpers;
using Tetris.Models;

namespace Tetris.Views
{
    internal class UIRenderer : IRenderer
    {
        private readonly GameState _gameState;
        private readonly GraphicsDeviceManager _gdm;

        private readonly int _blockSize = Constants.BlockSize;

        private readonly Vector2 _uiOffset = new(456, 19);
        private readonly int WidthUi = 150;
        private readonly int HeightUi = 442;

        private Texture2D _blankTexture;
        private SpriteFont _font;

        private string _scoreText = "Score:\n000000";
        private string _linesText = "Lines:\n00";
        private string _levelText = "Level:\n00";

        private Piece _nextPiece;

        public UIRenderer(GameState gameState, GraphicsDeviceManager gdm)
        {
            _gameState = gameState;
            _gdm = gdm;
            _gameState.OnScoreChanged += UpdateScoreText;
            _gameState.OnLinesChanged += UpdateLinesText;
            _gameState.OnLevelChanged += UpdateLevelText;
            _gameState.OnNextPieceChanged += UpdateNextPiece;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var position = new Rectangle((int)_uiOffset.X, (int)_uiOffset.Y, WidthUi, HeightUi);
            spriteBatch.Draw(_blankTexture, position, Color.Black);

            DrawScore(spriteBatch);
            DrawNextPieceBox(spriteBatch);
            DrawLevel(spriteBatch);
            DrawLines(spriteBatch);
        }

        public void LoadContent(ContentManager content)
        {
            _blankTexture = new Texture2D(_gdm.GraphicsDevice, 1, 1);
            _blankTexture.SetData([Color.White]);
            _font = content.Load<SpriteFont>("Fonts/UIFont");
        }

        private void DrawScore(SpriteBatch spriteBatch)
        {
            var x = (int)_uiOffset.X + 5;
            var y = (int)_uiOffset.Y + 5;
            DrawBoxAndText(spriteBatch, x, y, 140, 70, _scoreText);
        }

        private void DrawNextPieceBox(SpriteBatch spriteBatch)
        {
            var x = (int)_uiOffset.X + 5;
            var y = (int)_uiOffset.Y + 80;

            var widthNextPieceBox = 140;
            var heightNextPieceBox = 140;

            DrawBox(spriteBatch, new Rectangle(x, y, widthNextPieceBox, heightNextPieceBox), Color.DarkBlue);

            if (_nextPiece == null) return;

            int minX = _nextPiece.GetCoordMaxLeft();
            int maxX = _nextPiece.GetCoordMaxRight();
            int minY = _nextPiece.GetCoordMaxBottom();
            int maxY = _nextPiece.GetCoordMaxTop();

            int pieceWidth = (maxX - minX + 1) * _blockSize;
            int pieceHeight = (maxY - minY + 1) * _blockSize;

            int startX = x + (widthNextPieceBox - pieceWidth) / 2;
            int startY = y + (heightNextPieceBox - pieceHeight) / 2;

            Color pieceColor = _nextPiece.Color.ToXnaColor();
            foreach (var cord in _nextPiece.Cords)
            {
                int drawX = startX + ((cord.X - minX) * _blockSize);
                int drawY = startY + ((cord.Y - minY) * _blockSize);

                DrawBox(spriteBatch, new Rectangle(drawX, drawY, _blockSize, _blockSize), pieceColor);
            }
        }

        private void DrawLevel(SpriteBatch spriteBatch)
        {
            var x = (int)_uiOffset.X + 5;
            var y = (int)_uiOffset.Y + 225;
            DrawBoxAndText(spriteBatch, x, y, 140, 70, _levelText);
        }

        private void DrawLines(SpriteBatch spriteBatch)
        {
            var x = (int)_uiOffset.X + 5;
            var y = (int)_uiOffset.Y + 300;
            DrawBoxAndText(spriteBatch, x, y, 140, 70, _linesText);
        }

        private void DrawBoxAndText(
            SpriteBatch spriteBatch, int x, int y, int width, int height, string text
        )
        {
            var position = new Rectangle(x, y, width, height);
            DrawBox(spriteBatch, position, Color.DarkBlue);

            var heightText = _font.MeasureString(text).Y;
            var positionText = new Vector2(position.X + 5, (position.Y + (height - heightText) / 2));
            spriteBatch.DrawString(_font, text, positionText, Color.White);
        }

        private void DrawBox(SpriteBatch spriteBatch, Rectangle position, Color color)
            => spriteBatch.Draw(_blankTexture, position, color);

        private void UpdateScoreText(int newScore) => _scoreText = $"Score:\n{newScore}";
        private void UpdateLinesText(int newLines) => _linesText = $"Lines:\n{newLines}";
        private void UpdateLevelText(int newLevel) => _levelText = $"Level:\n{newLevel}";
        private void UpdateNextPiece(Piece newNextPiece) {
            _nextPiece = newNextPiece;
            _gameState.GameBoard.GhostCoords = _gameState.CalculateGhostPiece();
        }

        public void UnLoad()
        {
            throw new NotImplementedException();
        }
    }
}