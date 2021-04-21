using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class Zombi
    {
        int health;
        Point position;

        public Zombi(Point newPosition)
        {
            position = newPosition;
        }

        public Point GetPosition()
        {
            return position;
        }

        public void ChangePositionX(int deltax)
        {
            position.X += deltax;
        }

        public void ChangePositionY(int deltay)
        {
            position.Y += deltay;
        }

        public void ChangePosition(Point newPoint)
        {
            position = newPoint;
        }

        public void ChangeHealth(int damage)
        {
            health += damage;
        }

        public bool IsDeadInConflict()
        {
            return false;
        }
    }
}
