using Microsoft.Xna.Framework.Graphics;
using Sokoban.Extentions;

namespace Sokoban.UI.Controls
{
    public abstract class UITexturedControl : UIControl
    {
        protected Texture2D Texture;

        public override void Draw(SpriteBatch sb)
        {
            if (!IsVisible) return;

            sb.Draw(Texture, Bounds, Shade, Layer);
            AdditionalDraw(sb);
        }

        protected override void UpdateContained()
        {
            Bounds = CalculateBounds(Texture.Bounds.Size);
        }

        protected override void UpdateNotContained()
        {
            ScaleFactor = GetScaleFactor(Bounds.Size, Texture.Bounds.Size);
            Bounds = new(Bounds.Location, Bounds.Size);
        }
    }
}
