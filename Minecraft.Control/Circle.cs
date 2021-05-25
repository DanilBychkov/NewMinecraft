using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Control
{
    public class Circle
    {
        public bool IsIntoBall(Point pointPlayer, Point objectPoint,int radious)
        {
            return Math.Sqrt((pointPlayer.X - objectPoint.X) * (pointPlayer.X - objectPoint.X) + (pointPlayer.Y- objectPoint.Y) * (pointPlayer.Y- objectPoint.Y)) <= radious;
        }
    }
}
