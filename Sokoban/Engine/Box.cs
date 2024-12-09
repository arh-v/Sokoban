using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban.Engine
{
    public class Box : GameObject
    {
        public Box(GameMap map, Texture2D texture, int xPos, int yPos) : base(map, texture, xPos, yPos, ObjectTypes.Box, true)
        {
            
        }
    }
}
