using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sokoban.Extentions
{
    [Flags]
    public enum Alignment { Center = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 }

    internal static class SpriteBatchExtensions
    {
        public static void DrawString(this SpriteBatch sb,
            SpriteFont font, string text, Rectangle bounds, Alignment align, Color color, float layerDepth)
        {
            var size = font.MeasureString(text);
            var pos = bounds.Center;
            var origin = size * 0.5f;

            if (align.HasFlag(Alignment.Left))
                origin.X += bounds.Width / 2 - size.X / 2;

            if (align.HasFlag(Alignment.Right))
                origin.X -= bounds.Width / 2 - size.X / 2;

            if (align.HasFlag(Alignment.Top))
                origin.Y += bounds.Height / 2 - size.Y / 2;

            if (align.HasFlag(Alignment.Bottom))
                origin.Y -= bounds.Height / 2 - size.Y / 2;

            var boundsSize = new Vector2(bounds.Size.X, bounds.Size.Y);
            var scale = boundsSize / size;
            scale = new(Math.Min(scale.X, scale.Y));

            sb.DrawString(font,
                text, new Vector2(pos.X, pos.Y), color, 0, origin, scale, SpriteEffects.None, layerDepth);
        }

        public static void Draw(this SpriteBatch sb,
            Texture2D texture, Rectangle destinationRectangle, Color color, float layerDepth)
        {
            sb.Draw(texture, destinationRectangle, null, color, 0, Vector2.Zero, SpriteEffects.None, layerDepth);
        }
    }
}
