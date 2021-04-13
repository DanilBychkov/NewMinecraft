using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.MinecraftModels
{
    public enum Object
    {
        Sword,
        Bow,
        Shield
    }

    public enum Direction
    { 
        Left=-1,
        None=0,
        Right=1
    }

    public class Player
    {
        int health;
        Point position;
        Object[] objects = new Object[3];

        public Player(Point position)
        {
            this.position = position;
        }

        public Point GetPosition()
        {
            return position;
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
