using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sokoban.Extentions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sokoban.Engine
{
    public class GameMap
    {
        private GameObject[,] Objects;
        private GameObject[,] Destinations;
        private Sokoban Game;
        private Dictionary<GameObject, Point> Changed;
        private Player Player;

        public Rectangle Bounds { get; private set; }

        public int ElementScale { get; private set; }

        public Point LogicalSize { get; private set; }

        public bool IsFinished
        {
            get
            {
                if (Objects == null) return false;

                for (var x = 0; x < Objects.GetLength(0); x++)
                {
                    for (var y = 0; y < Objects.GetLength(1); y++)
                    {
                        if (Destinations[x, y] == null) continue;

                        if (Objects[x, y] != null &&
                            !Objects[x, y].TypeIs(ObjectTypes.Box) ||
                            Objects[x, y] == null)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public GameMap(Sokoban game, string filepath)
        {
            Game = game;
            LoadFromFile(filepath);
            Changed = new();
        }

        public void Update()
        {
            UpdateBounds();

            for (var x = 0; x < Objects.GetLength(0); x++)
            {
                for (var y = 0; y < Objects.GetLength(1); y++)
                {
                    Destinations[x, y]?.Update();

                    if (Objects[x, y] == null) continue;

                    Objects[x, y].Update();
                    var logical = Objects[x, y].LogicalLocation;
                    var actual = new Point(x, y);

                    if (logical == actual) continue;

                    if (IsThereCollision(actual, logical)) throw new Exception("Unxpected collision");
                }
            }

            ChangeLocations();
        }

        private void UpdateBounds()
        {
            var window = Game.Graphics.GraphicsDevice.Viewport.Bounds;
            var orig = Objects.GetLengths();
            var scale = window.Size / orig;
            ElementScale = Math.Min(scale.X, scale.Y);
            var minScale = new Point(ElementScale);
            var mapSize = orig * minScale;
            Bounds = new(window.Center - mapSize / new Point(2), mapSize);
        }

        private void ChangeLocations()
        {
            foreach (var (obj, coords) in Changed)
            {
                if (Objects[coords.X, coords.Y] == obj)
                {
                    Objects[coords.X, coords.Y] = null;
                }

                Objects[obj.LogicalLocation.X, obj.LogicalLocation.Y] = obj;
            }

            Changed.Clear();
        }

        private bool IsThereCollision(Point location, Point target)
        {
            var first = Objects[location.X, location.Y];
            var second = Objects[target.X, target.Y];

            return first != second && first?.LogicalLocation == second?.LogicalLocation;
        }

        public void Draw()
        {
            for (var x = 0; x < Objects.GetLength(0); x++)
            {
                for (var y = 0; y < Objects.GetLength(1); y++)
                {
                    Objects[x, y]?.Draw(Game.Renderer);
                    Destinations[x, y]?.Draw(Game.Renderer);
                }
            }
        }

        private void LoadFromFile(string filepath, string separator = "\r\n")
        {
            var mapFile = File.ReadAllText(filepath);
            var rows = mapFile.Split(new[] { separator, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var width = rows.GetMaxLength();
            var height = rows.Length;

            if (width < 3 && height < 6 || width < 6 && height < 3)
            {
                throw new Exception($"Wrong map '{mapFile}'");
            }

            Objects = new GameObject[width, height];
            Destinations = new GameObject[width, height];
            LogicalSize = new(width, height);

            for (var y = 0; y < rows.Length; y++)
            {
                for (var x = 0; x < rows[y].Length; x++)
                {
                    var obj = CreateGameObject(rows[y][x], x, y);

                    if (rows[y][x] == 'D')
                    {
                        Destinations[x, y] = obj;
                        continue;
                    }

                    Objects[x, y] = obj;
                }
            }
        }

        private GameObject CreateGameObject(char c, int x, int y)
        {
            var playerTexture = Game.Resources.GetTexture("player");
            var wallTexture = Game.Resources.GetTexture("wall");
            var destinationPlaceTexture = Game.Resources.GetTexture("destinationPlace");
            var boxTexture = Game.Resources.GetTexture("box");

            return c switch
            {
                'P' => CreatePlayer(playerTexture, x, y),
                '#' => CreateGameObject(typeof(Wall), wallTexture, x, y),
                'D' => CreateGameObject(typeof(DestinationPlace), destinationPlaceTexture, x, y),
                'B' => CreateGameObject(typeof(Box), boxTexture, x, y),
                ' ' => null,
                _ => throw new Exception($"wrong character for GameObject '{c}'")
            };
        }

        private GameObject CreatePlayer(Texture2D texture, int x, int y)
        {
            Player = (Player)CreateGameObject(typeof(Player), texture, x, y);
            Player.OnMove += (o, e) => Game.Scene.Score.IncreaseMovementsCount();
            Player.OnPush += (o, e) => Game.Scene.Score.IncreasePushesCount();

            return Player;
        }

        private GameObject CreateGameObject(Type t, Texture2D texture, int x, int y)
        {
            return (GameObject)Activator.CreateInstance(t, this, texture, x, y);
        }

        public GameObject GetObject(int x, int y) => Objects[x, y];

        public void SavePreviousObjectLocation(int x, int y) => Changed[Objects[x, y]] = new(x, y);
    }
}