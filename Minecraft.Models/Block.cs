using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class Block
    {
        public int distanceBetween { get; private set; }//нужно сделать 1 private
        public Point pointUp { get;private set; }
        public Point pointDown { get; private set; }

        public int lenghtUpPart { get; private set; }
        public int lengthDownPart { get; private set; }
        public int width { get; private set; }

        public Block(int clientSizeWidth, int clientSizeHeight, ScreenPoint screen, int tick)
        {
            width = 100;
            var rnd = new Random();
            distanceBetween = rnd.Next(50, 100);
            var pointStartBlockDown = rnd.Next(distanceBetween + 100, clientSizeHeight - 100);
            pointUp = new Point() { X = clientSizeWidth + screen.GetPointX(), Y = 0 };
            lenghtUpPart = pointStartBlockDown - distanceBetween;
            pointDown = new Point() { X = clientSizeWidth + screen.GetPointX(), Y = pointStartBlockDown };
            lengthDownPart = clientSizeHeight - pointStartBlockDown;
        }
    }
}
