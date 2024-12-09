using Sokoban.UI.Controls;
using System;

namespace Sokoban.UI.Groups
{
    public class MainMenu : Menu
    {
        public MainMenu(Sokoban game) : base(game)
        {
            RatioToWindowSize = new(3, 2);
        }

        public override void LoadUI()
        {
            var font = Game.Resources.GetFont("Tiny5");
            var button = Game.Resources.GetTexture("button");
            var menuTexture = Game.Resources.GetTexture("mainMenu");

            Container = new(menuTexture, 1, 4);
            Container.Add(new Button(button, font, "Играть", row: 0));
            Container.Add(new Button(button, font, "Настройки", row: 1));
            Container.Add(new Button(button, font, "Счет", row: 2));
            Container.Add(new Button(button, font, "Выход", row: 3));

            ((Button)Container.Content[0, 0]).OnClick += PlayClicked;
            ((Button)Container.Content[0, 1]).OnClick += SettingsClicked;
            ((Button)Container.Content[0, 2]).OnClick += ScoreClicked;
            ((Button)Container.Content[0, 3]).OnClick += ExitClicked;
        }

        private void PlayClicked(object sender, EventArgs e)
        {
            Hide();
            Game.Scene.Start();
        }

        private void SettingsClicked(object sender, EventArgs e)
        {
            Hide();
            Game.UI.GetMenu("SettingsMenu").Show();
        }

        private void ScoreClicked(object sender, EventArgs e)
        {
            Hide();
            Game.UI.GetMenu("ScoreMenu").Show();
        }

        private void ExitClicked(object sender, EventArgs e) => Game.Exit();
    }
}
