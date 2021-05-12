using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class Player:ICreature
    {
        private double health;
        private int countToFall;
        private Point position;
        private string type = "player";
        private int Size = 40;
        public Player(Point position)
        {
            this.position = position;
            health = 100;
        }

        public double GetHealth()
        {
            return health;
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

        public void ChangeHealth(double damage)
        {
            health -= damage;
        }

        public bool IsSleep()
        {
            throw new NotImplementedException();
        }

        public void ChangeSleep(Point playerPosition)
        {
            throw new NotImplementedException();
        }

        public Point GetChangePosition(int[,] map, ICreature playerPosition,int[] decorationObject, ScreenPoint ChangingScreen)
        {
            throw new NotImplementedException();
        }

        public void MakeNewICreature(Point creatureFinish, string type,int tickCount, List<FlyingObject> flyingObjects)
        {
            flyingObjects.Add(new FlyingObject(position, creatureFinish, "player"));
        }

        public bool IsDiedInConflit()
        {
            return false;
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
