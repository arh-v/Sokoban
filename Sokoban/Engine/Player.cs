using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sokoban.Extentions;
using System;

namespace Sokoban.Engine
{
    public class Player : GameObject
    {
        public Player(GameMap map, Texture2D texture, int xPos, int yPos) : base(map, texture, xPos, yPos, ObjectTypes.Player, false)
        {
        }

        public event EventHandler OnMove;
        public event EventHandler OnPush;

        public override void Draw(SpriteBatch renderer)
        {
            //IsMoving = false;

            renderer.Draw(Texture, VisualBounds, Color.White, Layer);
        }

        public override void Update()
        {
            base.Update();

            if (IsMoving) return;

            var x = LogicalLocation.X;
            var y = LogicalLocation.Y;

            Direction = GetDirection();

            if (Direction == Point.Zero || !TryToMove(x + Direction.X, y + Direction.Y))
            {
                return;
            }

            IsMoving = true;
            Map.SavePreviousObjectLocation(LogicalLocation.X, LogicalLocation.Y);
            LogicalLocation += Direction;
            OnMove.Invoke(this, EventArgs.Empty);
        }

        private bool TryToMove(int x, int y)
        {
            if (IsOutsideMap(x, y)) return false;

            var targetObject = Map.GetObject(x, y);

            if (targetObject == null) return true;

            if (targetObject.IsMovable)
            {
                if (IsOutsideMap(x + Direction.X, y + Direction.Y)) return false;

                if (Map.GetObject(x + Direction.X, y + Direction.Y) != null) return false;

                targetObject.Move(Direction);
                Map.SavePreviousObjectLocation(x, y);
                OnPush.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        private Point GetDirection()
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Up)) return new(0, -1);

            if (keyboard.IsKeyDown(Keys.Down)) return new(0, 1);

            if (keyboard.IsKeyDown(Keys.Left)) return new(-1, 0);

            if (keyboard.IsKeyDown(Keys.Right)) return new(1, 0);

            return new(0);
        }
    }
}