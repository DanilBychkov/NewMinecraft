using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.MinecraftControl
{
    public class ChangeObject
    {
        public Point GetNewPosition()
        {
            return new Point { X=1, Y=2 };
        }

        public int GetNewHealth()
        {
            return 1;
        }

        public bool IsEmptyBelow(Point pointPlayer, Point ChangingScreen, int direction, int[,] map)
        {
            return true;
        }

        public bool IsWay(Point pointPlayer, Point ChangingScreen, int direction, int[,] map)
        {
            return true;
        }

    }
}
