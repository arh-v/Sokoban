using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;

namespace Sokoban.UI.Controls
{
    public abstract class UIControl
    {
        private int row;
        private int column;
        private bool isVisible = true;
        private UIContainer container;
        protected Color Shade = Color.White;
        protected Point ScaleFactor;
        protected float Layer = 0.1f;

        public bool IsVisible
        {
            get => isVisible;
            set { isVisible = value; }
        }

        public Rectangle Bounds { get; set; }

        public UIContainer Container
        {
            get => container;
            set
            {
                if (value == null)
                {
                    Container.Content[column, row] = null;
                }

                container = value;
            }
        }

        public int Row
        {
            get => row;
            set
            {
                if (row < 0) throw new ArgumentOutOfRangeException($"{nameof(row)} should be >= 0.");

                row = value;
            }
        }

        public int Column
        {
            get => column;
            set
            {
                if (column < 0) throw new ArgumentOutOfRangeException($"{nameof(column)} should be >= 0.");

                column = value;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsVisible) return;

            AdditionalDraw(spriteBatch);
        }

        public void Update()
        {
            if (!IsVisible) return;

            if (Container != null)
            {
                UpdateContained();
            }
            else
            {
                UpdateNotContained();
            }

            UpdateLogic();
        }

        protected abstract void AdditionalDraw(SpriteBatch spriteBatch);

        protected abstract void UpdateLogic();

        protected virtual void UpdateContained()
        {
            var cellSize = GetCellSize();
            Bounds = CalculateBounds(cellSize);
        }

        protected virtual void UpdateNotContained()
        {
            ScaleFactor = new Point(1);
            Bounds = new(Bounds.Location, Bounds.Size);
        }

        protected Point GetCellSize()
        {
            var rows = Container.Content.GetLength(1);
            var columns = Container.Content.GetLength(0);
            return Container.Bounds.Size / new Point(columns, rows);
        }

        protected Point GetCellCoords(Point cellSize)
        {
            return Container.Bounds.Location + cellSize * new Point(Column, Row);
        }

        protected virtual Rectangle CalculateBounds(Point origSize)
        {
            var cellSize = GetCellSize();
            var cellCoords = GetCellCoords(cellSize);
            ScaleFactor = GetScaleFactor(cellSize, origSize);
            var padding = new Point(Container.Padding) * ScaleFactor;
            return new(cellCoords + padding, cellSize - padding * new Point(2));
        }

        protected Rectangle CalculateBounds(Vector2 origSize)
        {
            return CalculateBounds(origSize.ToPoint());
        }

        protected Point GetScaleFactor(Point size, Point origSize)
        {
            var scale = (size - origSize) / origSize;
            scale.X = scale.X == 0 ? 1 : scale.X;
            scale.Y = scale.Y == 0 ? 1 : scale.Y;
            return scale;
        }

        protected Point GetScaleFactor(Point size, Vector2 origSize)
        {
            return GetScaleFactor(size, origSize.ToPoint());
        }

    }
}
