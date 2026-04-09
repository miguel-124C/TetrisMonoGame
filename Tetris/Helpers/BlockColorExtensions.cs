using System;
using Microsoft.Xna.Framework;
using Tetris.Enums;

namespace Tetris.Helpers
{
    public static class BlockColorExtensions
    {
        private static readonly Random _random = new();

        // 1. El conversor (Extension Method + Switch Expression)
        public static Color ToXnaColor(this BlockColor color)
        {
            return color switch
            {
                BlockColor.Empty => Color.Black,
                BlockColor.Cyan => Color.Cyan,
                BlockColor.Blue => Color.Blue,
                BlockColor.Orange => Color.Orange,
                BlockColor.Yellow => Color.Yellow,
                BlockColor.Green => Color.Green,
                BlockColor.Purple => Color.Magenta,
                BlockColor.Red => Color.Red,
                _ => Color.Black // Caso por defecto
            };
        }

        public static BlockColor GetRandomColor()
        {
            // Como Black es el primer elemento (índice 0), 
            // generamos un número aleatorio entre 1 y 7 (el 8 es exclusivo).
            // Esto es más rápido y consume menos memoria que filtrar arrays como en Java.
            int randomIndex = _random.Next(1, 8);
            return (BlockColor)randomIndex;
        }
    }
}