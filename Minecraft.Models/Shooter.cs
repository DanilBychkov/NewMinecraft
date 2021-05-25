using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Minecraft.Models
{
    public class Shooter:ICreature
    {
        private Point position;
        private double health;
        private string type = "monster";
        private int lastTick ;
        private int Size = 80;

        public Shooter(Point point,int newLastTick)
        {
            position = point;
            health = 20;
            lastTick = newLastTick;
        }

        public void ChangeHealth(double damage)
        {
            health -= damage;
        }

        public void ChangeSleep(Point playerPosition)
        {

        }

        public Point GetPosition()
        {
            position = new Point() { X = position.X , Y = position.Y  };
            return position;
        }

        public bool IsSleep()
        {
            return false;
        }

        public Point GetChangePosition(int[,] map, ICreature playerPosition, int[] decorationObject, ScreenPoint ChangingScreen)
        {
            return position;
        }

        public void MakeNewICreature(Point creatureFinish, string type, int tickCount, List<FlyingObject> flyingObjects)
        {
            if (tickCount - lastTick >=15)
            {
                lastTick = tickCount;
                flyingObjects.Add(new FlyingObject(position, creatureFinish, "monster",Size));
            }
        }

        public bool IsDiedInConflit()
        {
            return health <= 0;
        }

        public string GetTypeCreature()
        {
            return type;
        }

        public int GetSize()
        {
            return Size;
        }
    }
}
