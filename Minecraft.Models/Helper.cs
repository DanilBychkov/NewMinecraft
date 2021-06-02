using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Minecraft.Models
{
    public static class Helper
    {
        public static bool IsWay(this Point pointCreature, int[,] map, int[] decorationObject, int size)
        {
            var point1 = IsDecorationObject(new Point() { X = pointCreature.X, Y = pointCreature.Y}, map, decorationObject);
            var point2 = IsDecorationObject(new Point() { X = pointCreature.X, Y = pointCreature.Y  + size },  map, decorationObject);
            var point3 = IsDecorationObject(new Point() { X = pointCreature.X + size, Y = pointCreature.Y },map, decorationObject);
            var point4 = IsDecorationObject(new Point() { X = pointCreature.X  + size, Y = pointCreature.Y  + size }, map, decorationObject);
            return point1 && point2 && point3 && point4;
        }

        private static bool IsDecorationObject(Point playerPoint, int[,] map, int[] decorationObject)
        {
            return decorationObject.Contains(map[(playerPoint.X) / 60, (playerPoint.Y) / 60]);
        }

        public static bool IsPointIntoAreaSquear(this Point areaPoint, Point objectPoint, int sizeArea)
        {
            return areaPoint.X <= objectPoint.X && areaPoint.X + sizeArea >= objectPoint.X
               && areaPoint.Y <= objectPoint.Y && areaPoint.Y + sizeArea >= objectPoint.Y;
        }

        public static bool IsIntoBall(this Point pointPlayer, Point objectPoint, int radious)
        {
            return Math.Sqrt((pointPlayer.X - objectPoint.X) * (pointPlayer.X - objectPoint.X) + (pointPlayer.Y - objectPoint.Y) * (pointPlayer.Y - objectPoint.Y)) <= radious;
        }
    }
}
