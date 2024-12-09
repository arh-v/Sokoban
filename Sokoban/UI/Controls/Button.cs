using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sokoban.Extentions;
using System;
using System.Data;

namespace Sokoban.UI.Controls
{
    internal class Button : UITexturedControl
    {
        private bool IsPressed = false;
        private SpriteFont Font;

        public string Text { get; set; }

        public Button(Texture2D texture,
            SpriteFont font, string text, int row = 0, int column = 0)
        {
            Texture = texture;
            Text = text;
            Font = font;
            Row = row;
            Column = column;
        }

        protected override void AdditionalDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Bounds, Alignment.Center, Shade, Layer + 0.2f);
        }

        protected override void UpdateLogic()
        {
            CheckClicked();
        }

        public event EventHandler OnClick;

        private void CheckClicked()
        {
            var MouseState = Mouse.GetState();
            var MousePosition = MouseState.Position;
            var cursor = new Rectangle(MousePosition.X, MousePosition.Y, 1, 1);

            if (cursor.Intersects(Bounds))
            {
                Shade = Color.Gray;

                if (!IsPressed && MouseState.LeftButton == ButtonState.Pressed)
                {
                    OnClick?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                Shade = Color.White;
            }

            IsPressed = MouseState.LeftButton != ButtonState.Released;
        }
    }
}