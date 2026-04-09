using System;
using Tetris.Enums;

namespace Tetris.Models
{
    public class Board
    {
        private readonly BlockColor _empty;
        private readonly int _row;
        private readonly int _col;
        private readonly BlockColor[,] _grid;

        public int HighestRow { get; private set; }
        public Coordinate[] GhostCoords { get; set; }
        public bool ShowGhostCoords { get; set; } = true;

        public event Action<int> OnLinesCleared;

        public Board(int rows, int columns, BlockColor empty)
        {
            _row = rows;
            _col = columns;
            _empty = empty;
            HighestRow = rows;

            _grid = new BlockColor[_row, _col];
            InitGrid();
        }

        public void InsertPiece(Piece piece)
        {
            foreach (var cord in piece.Cords)
                Insert(cord.X, cord.Y, piece.Color);

            // Actualiza el valor del tope donde hay piezas fijas
            var cordUp = piece.GetCoordMaxBottom();
            if (cordUp < HighestRow) HighestRow = cordUp;
        }

        public bool HasCollision(Coordinate[] simulatedCoords)
        {
            foreach (var cord in simulatedCoords)
            {
                if (cord.X < 0 || cord.X > _col - 1) return true; // 1. Validar paredes (salida de la matriz)
                if (cord.Y > _row - 1) return true; // 2. Validar piso

                var value = GetValue(cord.X, cord.Y);
                if (value != _empty) return true; // 3. Validar si value tiene un bloque fijo distinto a _empty
            }
            return false;
        }

        public void CheckAndClearLines()
        {
            int cantLines = 0;
            for (int i = HighestRow; i < _row; i++)
            {
                var cantValues = 0;
                for (int j = 0; j < _col; j++)
                {
                    if (_grid[i, j] == _empty) break;
                    cantValues++;
                }

                if (cantValues == _col)
                {
                    DestroyLines(i);
                    cantLines++;
                }
            }
            
            if (cantLines > 0)
                OnLinesCleared?.Invoke(cantLines); // ¡Gritamos que se rompieron líneas!
        }

        public BlockColor GetValue(int x, int y)
        {
            if (!ExistPos(x, y)) return _empty;
            return _grid[y, x];
        }


        private void InitGrid()
        {
            for (int i = 0; i < _row; i++)
                for (int j = 0; j < _col; j++)
                    _grid[i, j] = _empty;
        }

        private void DestroyLines(int row)
        {
            for (int j = 0; j < _col; j++)
                _grid[row, j] = _empty;
            
            // Mueve todas las filas superiores hacia abajo
            for (int i = row - 1; i >= HighestRow; i--)
                for (int j = 0; j < _col; j++)
                {
                    var color = _grid[i, j];
                    if (color != _empty)
                    {
                        _grid[i + 1, j] = color;
                        _grid[i, j] = _empty;
                    }
                }
        }

        private void Insert(int x, int y, BlockColor value)
        {
            if (!ExistPos(x, y)) return;
            _grid[y, x] = value;
        }

        private bool ExistPos(int x, int y) => (x >= 0 && x < _col) && (y >= 0 && y < _row);
    }
}