using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.Helpers
{
    public static class PieceTypeExtensions
    {
        public static (Coordinate[] Cords, int PivotIndex) GetInitialsCoords(this PieceType pieceType)
        {
            return pieceType switch
            {
                PieceType.I => ([ new Coordinate(3, 0), new Coordinate(4, 0), new Coordinate(5, 0), new Coordinate(6, 0)], 1),
                PieceType.J => ([ new Coordinate(3, 0), new Coordinate(3, 1), new Coordinate(4, 1), new Coordinate(5, 1)], 2),
                PieceType.L => ([ new Coordinate(5, 0), new Coordinate(5, 1), new Coordinate(4, 1), new Coordinate(3, 1)], 2),
                PieceType.O => ([ new Coordinate(4, 0), new Coordinate(5, 0), new Coordinate(4, 1), new Coordinate(5, 1)], 0),
                PieceType.S => ([ new Coordinate(3, 1), new Coordinate(4, 1), new Coordinate(4, 0), new Coordinate(5, 0)], 1),
                PieceType.T => ([ new Coordinate(3, 1), new Coordinate(4, 0), new Coordinate(4, 1), new Coordinate(5, 1)], 2),
                PieceType.Z => ([ new Coordinate(3, 0), new Coordinate(4, 0), new Coordinate(4, 1), new Coordinate(5, 1)], 2),
                _ => (Array.Empty<Coordinate>(), 0) // Caso por defecto
            };
        }
    }
}
