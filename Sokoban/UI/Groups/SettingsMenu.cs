using Sokoban.UI.Controls;
using System;

namespace Sokoban.UI.Groups
{
    public class SettingsMenu : Menu
    {
        public SettingsMenu(Sokoban game) : base(game)
        {
            RatioToWindowSize = new(2, 3);
        }

        public override void LoadUI()
        {
            var font = Game.Resources.GetFont("Tiny5");
            var button = Game.Resources.GetTexture("button");
            var menuTexture = Game.Resources.GetTexture("mainMenu");

            Container = new(menuTexture, 2, 4);
            Container.Add(new Label(font, "Имя Игрока", row: 0));
            Container.Add(new Label(font, "Разрешение экрана", row: 1));
            Container.Add(new Label(font, "Полноэкранный режим", row: 2));
            Container.Add(new Label(font, "TextBox", row: 0, column: 1));
            Container.Add(new Label(font, "ComboBox", row: 1, column: 1));
            Container.Add(new Label(font, "CheckBox", row: 2, column: 1));
            Container.Add(new Button(button, font, "OK", row: 3, column: 0));
            Container.Add(new Button(button, font, "Отмена", row: 3, column: 1));

            ((Button)Container.Content[0, 3]).OnClick += OKClicked;
            ((Button)Container.Content[1, 3]).OnClick += CancelClicked;
        }

        private void OKClicked(object sender, EventArgs e)
        {
            CancelClicked(sender, e);
        }
        private void CancelClicked(object sender, EventArgs e)
        {
            Hide();
            Game.UI.GetMenu("MainMenu").Show();
        }
    }
}
