using System;
using System.Linq;
using Tetris.Enums;

namespace Tetris.Models
{
    public class Board
    {
        public BlockColor Empty { get; }
        public int Row { get; }
        public int Col { get; }
        private readonly BlockColor[,] _grid;

        public int HighestRow { get; private set; }
        public Coordinate[] GhostCoords { get; set; }

        public event Action<int> OnLinesCleared;

        public Board(int rows, int columns, BlockColor empty)
        {
            Row = rows;
            Col = columns;
            Empty = empty;
            HighestRow = rows;

            _grid = new BlockColor[Row, Col];
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
                if (cord.X < 0 || cord.X > Col - 1) return true; // 1. Validar paredes (salida de la matriz)
                if (cord.Y > Row - 1) return true; // 2. Validar piso

                var value = GetValue(cord.X, cord.Y);
                if (value != Empty) return true; // 3. Validar si value tiene un bloque fijo distinto a _empty
            }
            return false;
        }

        public void CheckAndClearLines()
        {
            int cantLines = 0;
            for (int i = HighestRow; i < Row; i++)
            {
                var cantValues = 0;
                for (int j = 0; j < Col; j++)
                {
                    if (_grid[i, j] == Empty) break;
                    cantValues++;
                }

                if (cantValues == Col)
                {
                    DestroyLines(i);
                    cantLines++;
                }
            }
            
            if (cantLines > 0)
            {
                if (HighestRow < Row - 1)
                    HighestRow += cantLines;

                OnLinesCleared?.Invoke(cantLines); // ¡Gritamos que se rompieron líneas!
            }
        }

        public BlockColor GetValue(int x, int y)
        {
            if (!ExistPos(x, y)) return Empty;
            return _grid[y, x];
        }


        private void InitGrid()
        {
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    _grid[i, j] = Empty;
        }

        private void DestroyLines(int row)
        {
            for (int j = 0; j < Col; j++)
                _grid[row, j] = Empty;
            
            // Mueve todas las filas superiores hacia abajo
            for (int i = row - 1; i >= HighestRow; i--)
                for (int j = 0; j < Col; j++)
                {
                    var color = _grid[i, j];
                    if (color != Empty)
                    {
                        _grid[i + 1, j] = color;
                        _grid[i, j] = Empty;
                    }
                }
        }

        private void Insert(int x, int y, BlockColor value)
        {
            if (!ExistPos(x, y)) return;
            _grid[y, x] = value;
        }

        private bool ExistPos(int x, int y) => x >= 0 && x < Col && y >= 0 && y < Row;
    }
}