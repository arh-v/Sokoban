using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sokoban.Engine.Interfaces;
using Sokoban.Extentions;
using System;


namespace Sokoban.Engine
{
    public enum ObjectTypes
    {
        Player,
        Wall,
        DestinationPlace,
        Box
    }

    public abstract class GameObject : IMovableObject, IUpdateableObject, IDrawableObject
    {
        protected float Layer = 0.001f;
        protected GameMap Map;
        private Point logicalLocation;
        private Point direction;

        public Texture2D Texture { get; }

        public Rectangle VisualBounds { get; protected set; }

        public Point LogicalLocation
        {
            get => logicalLocation;
            set
            {
                if (IsOutsideMap(value.X, value.Y)) throw new Exception("Is outside map.");

                logicalLocation = value;
            }
        }

        public ObjectTypes ObjectType { get; }

        public Point Direction {
            get => direction;
            set
            {
                CheckDirection(value);
                direction = value;
            }
        }

        protected void CheckDirection(Point p)
        {
            if (p.ToVector2().Length() > 1) throw new Exception("Wrong direction.");
        }

        public bool IsMoving { get; protected set; }

        public bool IsMovable { get; protected set; }

        public float Speed { get; set; }

        public bool TypeIs(ObjectTypes type) => type == ObjectType;

        protected GameObject(GameMap map, Texture2D texture, int xPos, int yPos, ObjectTypes type, bool isMovable)
        {
            Texture = texture;
            Map = map;
            LogicalLocation = new Point(xPos, yPos);
            ObjectType = type;
            IsMovable = isMovable;
            Speed = 0.1f;
        }

        public virtual void Update()
        {
            var objectScale = new Point(Map.ElementScale);
            var logical = Map.Bounds.Location + LogicalLocation * objectScale;

            if (IsMoving)
            {
                var stepSize = Speed * Map.ElementScale;
                var visual = VisualBounds.Location + (Direction.ToVector2() * stepSize).ToPoint();
                var direction = Direction.X + Direction.Y;
                var logicalLength = logical.ToVector2().Length();
                var visualLength = visual.ToVector2().Length();

                if (direction > 0 && visualLength < logicalLength ||
                    direction < 0 && visualLength > logicalLength)
                {
                    VisualBounds = new(visual, objectScale);
                    return;
                }
            }

            IsMoving = false;
            VisualBounds = new(logical, objectScale);
        }

        public void Move(Point direction)
        {
            CheckDirection(direction);
            LogicalLocation += direction;
            Direction = direction;
            IsMoving = true;
        }

        public virtual void Draw(SpriteBatch renderer)
        {
            renderer.Draw(Texture, VisualBounds, Color.White, Layer); ;
        }

        protected bool IsOutsideMap(int x, int y)
        {
            return x < 0 || x > Map.LogicalSize.X || y < 0 || y > Map.LogicalSize.Y;
        }
    }
}
