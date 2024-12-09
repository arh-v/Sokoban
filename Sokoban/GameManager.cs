using Sokoban.Engine;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sokoban
{
    public enum GameStates
    {
        InGame,
        NotInGame,
        Paused
    }

    public class GameManager
    {
        private Sokoban Game;
        private List<string> MapsPaths;
        private int CurrentMapIndex;

        public GameMap Map { get; private set; }

        public PlayerScore Score { get; private set; }

        public GameStates State { get; private set; }

        public GameManager(Sokoban game)
        {
            Game = game;
            State = GameStates.NotInGame;
            MapsPaths = new();
        }

        public void Load(string mapsFolder)
        {
            var mapsDirectory = new DirectoryInfo(mapsFolder);

            foreach (var file in mapsDirectory.GetFiles("*.txt"))
            {
                MapsPaths.Add(file.FullName);
            }

            if (MapsPaths.Count == 0) throw new Exception("Levels not found.");
        }

        public void Update()
        {
            if (State != GameStates.InGame) return;

            if (Map != null && Map.IsFinished) CurrentMapIndex++;

            if (CurrentMapIndex == MapsPaths.Count)
            {
                End();
            }

            if (Map == null || Map.IsFinished)
            {
                if (CurrentMapIndex == MapsPaths.Count)
                {
                    End();
                    return;
                }

                Map = new(Game, MapsPaths[CurrentMapIndex]);
            }

            Map.Update();
        }

        public void Draw()
        {
            if (State == GameStates.NotInGame) return;

            Map.Draw();
        }

        public void Start()
        {
            State = GameStates.InGame;
            CurrentMapIndex = 0;
            Score = new("Default", Game);
        }

        public void End()
        {
            State = GameStates.NotInGame;
            Map = null;
            Game.UI.GetMenu("MainMenu").Show();
        }

        public void Stop()
        {
            if (State != GameStates.InGame) return;

            State = GameStates.Paused;
        }

        public void Resume()
        {
            if (State != GameStates.Paused) return;

            State = GameStates.InGame;
        }
    }
}
