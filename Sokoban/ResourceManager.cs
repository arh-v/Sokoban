using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class ResourceManager
    {
        private readonly Dictionary<string, Texture2D> Textures;
        private readonly Dictionary<string, SpriteFont> Fonts;
        private ContentManager Content;

        public ResourceManager(ContentManager content)
        {
            Textures = new();
            Fonts = new();
            Content = content;
        }

        public Texture2D GetTexture(string name) => GetResource<Texture2D>(Textures, name);

        public Texture2D AddTexture(string name) => AddResource<Texture2D>(Textures, name);

        public SpriteFont GetFont(string name) => GetResource<SpriteFont>(Fonts, name);

        public SpriteFont AddFont(string name) => AddResource<SpriteFont>(Fonts, name);

        private T GetResource<T>(Dictionary<string, T> resourceDict, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (!resourceDict.ContainsKey(name)) throw new ArgumentException($"{nameof(name)} does not exists.");

            return resourceDict[name];
        }

        private T AddResource<T>(Dictionary<string, T> resourceDict, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (resourceDict.ContainsKey(name)) throw new ArgumentException($"{nameof(name)} is already exists.");

            resourceDict[name] = Content.Load<T>(name);
            return resourceDict[name];
        }
    }
}
