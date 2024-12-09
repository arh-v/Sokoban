using Microsoft.Xna.Framework.Input;
using Sokoban.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.UI.Groups
{
    public class InGameMenu : Menu
    {
        public InGameMenu(Sokoban game) : base(game)
        {
            RatioToWindowSize = new(3, 4);
        }

        public override void LoadUI()
        {
            var font = Game.Resources.GetFont("Tiny5");
            var button = Game.Resources.GetTexture("button");
            var menuTexture = Game.Resources.GetTexture("mainMenu");
            Container = new(menuTexture, 2, 1);
            Container.Add(new Button(button, font, "Продолжить", column: 0, row: 0));
            Container.Add(new Button(button, font, "Выйти в меню", column: 1, row: 0));
            ((Button)Container.Content[0, 0]).OnClick += ContinueClicked;
            ((Button)Container.Content[1, 0]).OnClick += ExitClicked;
        }

        private void ContinueClicked(object sender, EventArgs e)
        {
            Hide();
            Game.Scene.Resume();
        }

        private void ExitClicked(object sender, EventArgs e)
        {
            Hide();
            Game.UI.GetMenu("MainMenu").Show();
            Game.Scene.End();
        }
    }
}
