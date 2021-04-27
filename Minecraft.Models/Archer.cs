using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class Archer:ICreature
    {
        private int health;
        private Point position;
        private Point positionArrow;

        public Archer(Point position)
        {
            this.position = position;
            positionArrow = position;
            health = 100;
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

        public void ChangePositionX(int changeX)
        {
            throw new NotImplementedException();
        }

        public void ChangePositionY(int changeY)
        {
            throw new NotImplementedException();
        }

        public void ChangePoisition(Point lastPosition)
        {
            throw new NotImplementedException();
        }

        public bool IsSleep()
        {
            return false;
        }

        public void ChangeSleep(bool newSleep)
        {
            throw new NotImplementedException();
        }
    }
}
