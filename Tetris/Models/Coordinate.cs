
namespace Tetris.Models
{
    public struct Coordinate(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;

        public readonly Coordinate RotateAround(Coordinate pivot)
        {
            // Si es el pivot, no se rota
            if (this.X == pivot.X && this.Y == pivot.Y)
                return this;

            var xRel = this.X - pivot.X;
            var yRel = this.Y - pivot.Y;

            var xRotateRel = -1 * yRel;

            var xAbs = xRotateRel + pivot.X;
            var yAbs = xRel + pivot.Y;

            return new Coordinate(xAbs, yAbs);
        }

    }
}
