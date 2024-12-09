using Sokoban.UI.Controls;
using System;
using System.IO;

namespace Sokoban.UI.Groups
{
    public class ScoreMenu : Menu
    {
        private readonly string[][] Scores;

        public ScoreMenu(Sokoban game) : base(game)
        {
            RatioToWindowSize = new(2, 1.5f);
            var rows = 8;
            Scores = new string[rows][];
        }

        public override void LoadUI()
        {
            var font = Game.Resources.GetFont("Tiny5");
            var button = Game.Resources.GetTexture("button");
            var menuTexture = Game.Resources.GetTexture("mainMenu");
            var sr = new StreamReader("data\\Scores.txt");
            Container = new(menuTexture, 1, 3);
            Container.Add(new Label(font, "Счет", row: 0));
            var ScoresContainer = new UIContainer(menuTexture, 5, 8, row: 1);
            Container.Add(ScoresContainer);
            ScoresContainer.Add(new Label(font, "Имя игрока", row: 0, column: 0));
            ScoresContainer.Add(new Label(font, "Уровень", row: 0, column: 1));
            ScoresContainer.Add(new Label(font, "Движений", row: 0, column: 2));
            ScoresContainer.Add(new Label(font, "Толчков", row: 0, column: 3));
            ScoresContainer.Add(new Label(font, "Время", row: 0, column: 4));
            Container.Add(new Button(button, font, "OK", row: 2));

            for (int row = 1; row < Scores.Length; row++)
            {
                var line = sr.ReadLine().Split(' ');
                Scores[row] = line;

                for (int col = 0; col < line.Length; col++)
                {
                    ScoresContainer.Add(new Label(font, Scores[row][col], row: row, column: col));
                }
            }

            sr.Close();
            ((Button)Container.Content[0, 2]).OnClick += OKClicked;
        }

        private void OKClicked(object sender, EventArgs e)
        {
            Hide();
            Game.UI.GetMenu("MainMenu").Show();
        }

    }
}
