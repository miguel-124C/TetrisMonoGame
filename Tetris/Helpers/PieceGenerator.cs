using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.Helpers
{
    public class PieceGenerator
    {
        private readonly List<PieceType> _bag = [];
        private static readonly Random _random = new();

        public Piece GetNextPiece()
        {
            if (_bag.Count == 0) ReFillBag();

            var index = _random.Next(_bag.Count);
            var type = _bag.ElementAt(index);
            _bag.Remove(type);

            return Piece.Create(type);
        }

        public void ClearBag()
        {
            _bag.Clear();
        }

        private void ReFillBag()
        {
            _bag.AddRange([
                PieceType.I, PieceType.J, PieceType.L,
                PieceType.O, PieceType.S, PieceType.T, PieceType.Z
            ]);
        }
    }
}
