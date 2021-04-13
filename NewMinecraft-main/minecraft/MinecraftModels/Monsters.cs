using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.MinecraftModels
{
    public class Archer
    {
        int health;
        Point position;
        Point positionArrow;

        public Archer(Point position)
        {
            this.position = position;
            positionArrow = position;
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

    public class Zombi
    {
        int health;
        Point position;

        public Zombi(Point position)
        {
            this.position = position;
        }

        public Point GetPosition()
        {
            return new Point();
        }

        public void ChangeHealth(int damage)
        {

        }

        public bool IsDeadInConflict()
        {
            return false;
        }
    }

    public class Spider
    {
        int health;
        Point position;

        public Spider(Point position)
        {
            this.position = position;
        }

        public Point GetPosition()
        {
            return new Point();
        }

        public void ChangeHealth(int damage)
        {

        }

        public bool IsDeadInConflict()
        {
            return false;
        }
    }
}
