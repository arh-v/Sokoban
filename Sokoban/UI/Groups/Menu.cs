using Microsoft.Xna.Framework;
using Sokoban.UI.Controls;

namespace Sokoban.UI.Groups
{
    public abstract class Menu
    {
        protected UIContainer Container;
        protected readonly Sokoban Game;
        protected Vector2 RatioToWindowSize;

        protected Menu(Sokoban game)
        {
            Game = game;
        }

        public abstract void LoadUI();

        public virtual void UpdateUI()
        {
            var window = Game.GraphicsDevice.Viewport.Bounds;
            var containerSize = Container.Bounds.Size;
            var menuSize = (window.Size.ToVector2() / RatioToWindowSize).ToPoint();
            Container.Bounds = new(window.Center - menuSize / new Point(2, 2), menuSize);
            Container.Update();
        }

        public void DrawUI() => Container.Draw(Game.Renderer);

        public void Show() => Container.IsVisible = true;

        public void Hide() => Container.IsVisible = false;
    }
}
