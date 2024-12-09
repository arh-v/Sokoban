using Microsoft.Xna.Framework.Graphics;
using Sokoban.Extentions;

namespace Sokoban.UI.Controls
{
    internal class Label : UIControl
    {
        private SpriteFont Font;

        public string Text { get; set; }

        public Label(SpriteFont font, string text, int row = 0, int column = 0)
        {
            Font = font;
            Text = text;
            Row = row;
            Column = column;
        }

        protected override void AdditionalDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Bounds, Alignment.Center, Shade, Layer * 1.1f);
        }

        protected override void UpdateContained()
        {
            var textSize = Font.MeasureString(Text);
            Bounds = CalculateBounds(textSize);
        }

        protected override void UpdateLogic()
        {

        }
    }
}
