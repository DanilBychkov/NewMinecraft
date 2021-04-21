using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class Archer
    {
        private int health;
        private Point position;
        private Point positionArrow;

        public Archer(Point newpPosition)
        {
            position = newpPosition;
            positionArrow = newpPosition;
        }

        public Point GetPosition()
        {
            return new Point();
        }

        public Point GetPositionArrow()
        {
            return positionArrow;
        }

        public void ChangeHealth(int damage)
        {
            health -= damage;
        }

        public bool IsDeadInConflict()
        {
            return false;
        }

    }
}
