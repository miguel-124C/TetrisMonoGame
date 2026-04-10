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

        private readonly int _blockSize = 20;

        private readonly Vector2 _boardOffset = new(234, 19);
        private readonly Vector2 _boardSize = new(222, 442);
        private readonly int _gridSize = 2;

        public void LoadContent(ContentManager content)
        {
            // Creamos una textura de 1 píxel por 1 píxel
            _blankTexture = new Texture2D(_gdm.GraphicsDevice, 1, 1);

            // La pintamos de blanco puro. (Al ser blanca, SpriteBatch la puede teñir de cualquier color luego)
            _blankTexture.SetData([Color.White]);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            var board = new Rectangle((int)_boardOffset.X, (int)_boardOffset.Y, (int)_boardSize.X, (int)_boardSize.Y);
            var colorEmpty = _gameState.GameBoard.Empty.ToXnaColor();
            spriteBatch.Draw(_blankTexture, board, colorEmpty);

            DrawAllPieces(spriteBatch);
            DrawGrid(spriteBatch);
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
                spriteBatch.Draw(_blankTexture, lineRect, colorGrids);
            }
            // Dibujar líneas horizontales
            for (int y = 0; y <= board.Row; y++)
            {
                int lineY = (int)_boardOffset.Y + (y * (_blockSize + _gridSize));
                var lineRect = new Rectangle((int)_boardOffset.X, lineY, (int)_boardSize.X, _gridSize);
                spriteBatch.Draw(_blankTexture, lineRect, colorGrids);
            }
        }

        private void DrawAllPieces(SpriteBatch spriteBatch)
        {
            var board = _gameState.GameBoard;
            for (int y = board.HighestRow; y < board.Row; y++)
                for (int x = 0; x < board.Col; x++)
                {
                    BlockColor colorPiece = board.GetValue(x, y);
                    Color renderColor = colorPiece.ToXnaColor();

                    int drawX = (int)_boardOffset.X + (x * (_blockSize + _gridSize));
                    int drawY = (int)_boardOffset.Y + (y * (_blockSize + _gridSize));

                    var destinationRect = new Rectangle(drawX, drawY, _blockSize, _blockSize);
                    spriteBatch.Draw(_blankTexture, destinationRect, renderColor);
                }
        }

        public void UnLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}