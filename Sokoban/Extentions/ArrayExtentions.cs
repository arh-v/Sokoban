
using Microsoft.Xna.Framework;

namespace Sokoban.Extentions
{
    internal static class ArrayExtentions
    {
        public static Point GetLengths(this object[,] array)
        {
            return new Point(array.GetLength(0), array.GetLength(1));
        }

        public static int GetMaxLength(this string[] array)
        {
            var maxLength = 0;

            foreach (var item in array)
            {
                if (item == null) continue;

                if(item.Length > maxLength)
                {
                    maxLength = item.Length;
                }
            }

            return maxLength;
        }
    }
}
