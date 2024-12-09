using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Engine
{
    public class DestinationPlace : GameObject
    {
        public DestinationPlace(GameMap map, Texture2D texture, int xPos, int yPos) :
            base(map, texture, xPos, yPos, ObjectTypes.DestinationPlace, false)
        {
            Layer /= 2;
        }
    }
}
