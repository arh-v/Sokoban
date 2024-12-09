using Microsoft.Xna.Framework;
using System;

namespace Sokoban.Extentions
{
    internal static class PointExtentions
    {
        public static Vector2 ToVector2(this Point p) => new(p.X, p.Y);

        public static bool HasMatch(this Point p1, Point p2) => p1.X == p2.X || p1.Y == p2.Y;
    }
}
