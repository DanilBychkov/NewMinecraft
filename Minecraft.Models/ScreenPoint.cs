using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class ScreenPoint
    {
        private Point pointScreen;

        public ScreenPoint(Point point)
        {
            pointScreen = point;
        }

        public int GetPointX()
        {
            return pointScreen.X;
        }

        public int GetPointY()
        {
            return pointScreen.Y;
        }

        public void ChangePointScreenX(int deltaX)
        {
            pointScreen.X += deltaX;
        }

        public void ChangePointScreenY(int deltaY)
        {
            pointScreen.Y += deltaY;
        }
    }
}
