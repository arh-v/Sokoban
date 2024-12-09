using Microsoft.Xna.Framework;
using System;

namespace Sokoban.Engine
{
    public class PlayerScore
    {
        private Game Game;

        public string PlayerName { get; }

        public int Level { get; private set; }

        public int MovementsCount { get; private set; }

        public int PushesCount { get; private set; }

        public TimeSpan Time { get; private set; }

        public PlayerScore(string playerName, Game game)
        {
            PlayerName = playerName;
            Game = game;
        }

        public void IncreaseMovementsCount() => MovementsCount++;

        public void IncreasePushesCount() => PushesCount++;

        public void UpdateTime()
        {
        }

        public void NextLevel()
        {
            Level++;
        }
    }
}