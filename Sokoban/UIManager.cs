using Microsoft.Xna.Framework.Graphics;
using Sokoban.UI.Groups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sokoban
{
    public class UIManager
    {
        private Dictionary<string, Menu> menus;
        public Dictionary<string, Menu>.ValueCollection Menus => menus.Values;

        private readonly Sokoban Game;

        public UIManager(Sokoban game)
        {
            Game = game;
            menus = new();
        }

        public void AddMenu(Type t, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (menus.ContainsKey(name)) throw new ArgumentException($"{nameof(name)} is already exists.");

            menus[name] = (Menu)Activator.CreateInstance(t, Game);
        }

        public Menu GetMenu(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (!menus.ContainsKey(name)) throw new ArgumentException($"{nameof(name)} does not exists.");

            return menus[name];
        }

        public void LoadUIs()
        {
            ActionOnUIs(element =>
            {
                element.LoadUI();
                element.Hide();
            });
        }

        public void UpdateUIs() => ActionOnUIs(element => element.UpdateUI());

        public void DrawUIs() => ActionOnUIs(element => element.DrawUI());

        private void ActionOnUIs(Action<Menu> action)
        {
            foreach (var element in Menus)
            {
                action(element);
            }
        }
    }
}
