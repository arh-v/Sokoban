using Microsoft.Xna.Framework;

namespace Sokoban.Extentions
{
    internal static class Vector2Extentions
    {
        public static Point ToPoint(this Vector2 v) => new((int)v.X, (int)v.Y);
    }
}
