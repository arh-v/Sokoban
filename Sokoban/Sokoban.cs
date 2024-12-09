using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sokoban.UI.Groups;
using System;

namespace Sokoban
{
    public class Sokoban : Game
    {
        public GraphicsDeviceManager Graphics { get; }

        public SpriteBatch Renderer { get; private set; }

        public ResourceManager Resources { get; }

        public UIManager UI { get; }

        public GameManager Scene { get; }

        public Sokoban()
        {
            Graphics = new(this);
            Content.RootDirectory = "Content";
            Resources = new(Content);
            UI = new(this);
            Scene = new(this);
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Renderer = new SpriteBatch(GraphicsDevice);

            Resources.AddFont("Tiny5");
            Resources.AddTexture("button");
            Resources.AddTexture("mainMenu");
            Resources.AddTexture("player");
            Resources.AddTexture("wall");
            Resources.AddTexture("destinationPlace");
            Resources.AddTexture("box");

            UI.AddMenu(typeof(MainMenu), "MainMenu");
            UI.AddMenu(typeof(SettingsMenu), "SettingsMenu");
            UI.AddMenu(typeof(ScoreMenu), "ScoreMenu");
            UI.AddMenu(typeof(InGameMenu), "InGameMenu");
            UI.LoadUIs();
            UI.GetMenu("MainMenu").Show();

            Scene.Load("levels");
        }

        private bool IsPressed;

        protected override void Update(GameTime gameTime)
        {
            if (!IsPressed && Keyboard.GetState().IsKeyDown(Keys.Escape) && Scene.State == GameStates.InGame)
            {
                UI.GetMenu("InGameMenu").Show();
                Scene.Stop();
                IsPressed = true;
            }

            if (!IsPressed && Keyboard.GetState().IsKeyDown(Keys.Escape) && Scene.State == GameStates.Paused)
            {
                UI.GetMenu("InGameMenu").Hide();
                Scene.Resume();
            }

            IsPressed = Keyboard.GetState().IsKeyDown(Keys.Escape);

            UI.UpdateUIs();
            Scene.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Turquoise);
            Renderer.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);
            UI.DrawUIs();
            Scene.Draw();
            Renderer.End();
            base.Draw(gameTime);
        }
    }
}
