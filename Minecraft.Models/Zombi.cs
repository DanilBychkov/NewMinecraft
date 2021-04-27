using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class Zombi:ICreature
    {
        private int health;
        private Point position;
        private bool sleep;

        public Zombi(Point newPosition)
        {
            position = newPosition;
            sleep = true;
            health = 10;
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

        public void ChangeSleep(bool newSleep)
        {
            sleep = newSleep;
        }

        public bool IsSleep()
        {
            return sleep;
        }

        public bool IsDeadInConflict()
        {
            return health <= 0;
        }

        public void ChangePoisition(Point lastPosition)
        {
            position = lastPosition;
        }
    }
}
