using System.Linq;
using Tetris.Enums;
using Tetris.Helpers;

namespace Tetris.Models
{
    public class Piece(Coordinate[] coords, BlockColor color, PieceType type, int pivotIndex, bool canRotate)
    {
        public Coordinate[] Cords { get; private set; } = coords;
        public BlockColor Color { get; private set; } = color;
        public PieceType Type { get; private set; } = type;

        private readonly int _pivotIndex = pivotIndex;
        public bool CanRotate { get; private set; } = canRotate;

        public static Piece Create(PieceType type) // Factory method to create a new piece based on the type
        {
            var (cords, pivotIndex) = type.GetInitialsCoords();
            bool canRotate = type != PieceType.O;

            return new Piece(cords, BlockColorExtensions.GetRandomColor(), type, pivotIndex, canRotate);
        }

        // --- MÉTODOS DE MOVIMIENTO INMUTABLE ---

        // En lugar de modificar los propios Cords, devolvemos un arreglo con la proyección.
        // Esto es MAGIA para el Patrón Command y para validar colisiones en el Board.
        public Coordinate[] GetSimulatedMove(Direction direction)
        {
            int deltaX = direction == Direction.Right ? 1 : (direction == Direction.Left ? -1 : 0);
            int deltaY = direction == Direction.Down ? 1 : 0;

            // Usamos LINQ (muy C-sharpiano) para generar las nuevas coordenadas.
            // Al ser struct, esto es rapidísimo y no genera basura en el Heap.
            //return Cords.Select(c => new Coordinate(c.X + deltaX, c.Y + deltaY)).ToArray();
            return [.. Cords.Select(c => new Coordinate(c.X + deltaX, c.Y + deltaY))];
        }

        public Coordinate[] GetSimulatedRotation()
        {
            if (!CanRotate) return Cords;

            // Obtenemos el pivote actual usando el índice
            Coordinate pivot = Cords[_pivotIndex];

            return [.. Cords.Select(c => c.RotateAround(pivot))];
        }

        // Una vez que el Board validó que la simulación es correcta, aplicamos el cambio.
        public void ApplyMove(Coordinate[] newCords)
        {
            Cords = newCords; // Como Cords es un array de structs, es una asignación directa.
        }


        // --- MÉTODOS DE LIMITES (Simplificados con LINQ) ---
        // Enseñar LINQ en Udemy es un gran valor añadido.
        // Obtiene la coordenada mas cerca a la parte baja de la matriz
        public int GetCoordMaxTop() => Cords.Max(c => c.Y);

        // Obtiene la coordenada mas cerca a la parte alta de la matriz
        public int GetCoordMaxBottom() => Cords.Min(c => c.Y);

        // Obtiene la coordenada mas cerca a la parte izquierda de la matriz
        public int GetCoordMaxLeft() => Cords.Min(c => c.X);

        // Obtiene la coordenada mas cerca a la parte derecha de la matriz
        public int GetCoordMaxRight() => Cords.Max(c => c.X);
    }
}