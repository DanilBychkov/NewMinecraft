using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class Player
    {
        private int health;
        private int countToFall;
        private Point position;
        private Object[] objects = new Object[3];
        private Direction direction;

        public Player(Point position)
        {
            this.position = position;
        }

        public void ChangeDirection(Direction newDirection)
        {
            direction = newDirection;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public int GetCountToFall()
        {
            return countToFall;
        }

        public void ChangeCountToFall(int delta)
        {
            countToFall += delta;
        }

        public Point GetPosition()
        {
            return position;
        }

        public void ChangePositionX(int changeX)
        {
            position.X += changeX;
        }

        public void ChangePositionY(int changeY)
        {
            position.Y += changeY;
        }

        public void ChangePoisition(Point lastPosition)
        {
            position = lastPosition;
        }

        public void AddWeapon(Object weapon)
        {

        }

        public Object[] GetWeapon()
        {
            return objects;
        }

        public bool IsDeadInConflict()
        {
            return false;
        }
    }
}
