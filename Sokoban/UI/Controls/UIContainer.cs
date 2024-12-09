using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.UI.Controls
{
    public class UIContainer : UITexturedControl
    {
        public UIControl[,] Content { get; }

        public int Padding { get; set; }

        public UIContainer(Texture2D texture, int columns, int rows, int row = 0, int col = 0)
        {
            Texture = texture;
            Content = new UIControl[columns, rows];
            Padding = 2;
            Row = row;
            Column = col;
        }

        public void Add(UIControl element)
        {
            element.Container = this;
            Content[element.Column, element.Row] = element;
        }

        protected override void AdditionalDraw(SpriteBatch sb)
        {
            foreach (UIControl element in Content)
            {
                element?.Draw(sb);
            }
        }

        protected override void UpdateLogic()
        {
            foreach (UIControl element in Content)
            {
                element?.Update();
            }
        }
    }
}
