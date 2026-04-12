using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tetris.Enums;
using Tetris.Helpers;
using Tetris.Models;

namespace Tetris.Views
{
    internal class BoardRenderer(GameState gameState, GraphicsDeviceManager gdm) : IRenderer
    {
        private readonly GameState _gameState = gameState;
        private readonly GraphicsDeviceManager _gdm = gdm;

        private Texture2D _blankTexture;

        private readonly int _blockSize = Constants.BlockSize;

        private readonly Vector2 _boardOffset = new(234, 19);
        private readonly Vector2 _boardSize = new(222, 442);
        private readonly int _gridSize = 2;

        public void LoadContent(ContentManager content)
        {
            _blankTexture = new Texture2D(_gdm.GraphicsDevice, 1, 1);
            _blankTexture.SetData([Color.White]);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var board = new Rectangle((int)_boardOffset.X, (int)_boardOffset.Y, (int)_boardSize.X, (int)_boardSize.Y);
            var colorEmpty = _gameState.GameBoard.Empty.ToXnaColor();
            DrawBox(spriteBatch, board, colorEmpty);

            DrawAllPieces(spriteBatch);
            DrawGrid(spriteBatch);
            DrawGhostPiece(spriteBatch);
            DrawCurrentPiece(spriteBatch);
        }

        private void DrawGrid(SpriteBatch spriteBatch)
        {
            var board = _gameState.GameBoard;
            var colorGrids = new Color(100, 100, 100);
            // Dibujar líneas verticales
            for (int x = 0; x <= board.Col; x++)
            { 
                int lineX = (int)_boardOffset.X + (x * (_blockSize + _gridSize));
                var lineRect = new Rectangle(lineX, (int)_boardOffset.Y, _gridSize, (int)_boardSize.Y);
                DrawBox(spriteBatch, lineRect, colorGrids);
            }
            // Dibujar líneas horizontales
            for (int y = 0; y <= board.Row; y++)
            {
                int lineY = (int)_boardOffset.Y + (y * (_blockSize + _gridSize));
                var lineRect = new Rectangle((int)_boardOffset.X, lineY, (int)_boardSize.X, _gridSize);
                DrawBox(spriteBatch, lineRect, colorGrids);
            }
        }

        private void DrawCurrentPiece(SpriteBatch spriteBatch)
        {
            var currentPiece = _gameState.CurrentPiece;
            foreach (var coord in currentPiece.Cords)
                DrawBlockPiece(spriteBatch, coord.X, coord.Y, currentPiece.Color.ToXnaColor());
        }

        private void DrawGhostPiece(SpriteBatch spriteBatch)
        {
            var ghostPiece = _gameState.GameBoard.GhostCoords;
            foreach (var coord in ghostPiece)
                DrawBlockPiece(spriteBatch, coord.X, coord.Y, Color.White, true);
        }

        private void DrawAllPieces(SpriteBatch spriteBatch)
        {
            var board = _gameState.GameBoard;
            for (int y = board.HighestRow; y < board.Row; y++)
                for (int x = 0; x < board.Col; x++)
                {
                    BlockColor colorPiece = board.GetValue(x, y);
                    Color renderColor = colorPiece.ToXnaColor();

                    if (colorPiece != board.Empty)
                        DrawBlockPiece(spriteBatch, x, y, renderColor);
                }
        }

        private void DrawBlockPiece(SpriteBatch spriteBatch, int x, int y, Color color, bool isGhost = false)
        {
            if (isGhost) color *= 0.3f;

            int drawX = (int)_boardOffset.X + (x * (_blockSize + _gridSize));
            int drawY = (int)_boardOffset.Y + (y * (_blockSize + _gridSize));

            var destinationRect = new Rectangle(drawX, drawY, _blockSize, _blockSize);
            DrawBox(spriteBatch, destinationRect, color);
        }

        private void DrawBox(SpriteBatch spriteBatch, Rectangle position, Color color)
            => spriteBatch.Draw(_blankTexture, position, color);

        public void UnLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}