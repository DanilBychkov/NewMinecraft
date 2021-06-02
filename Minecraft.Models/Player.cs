using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Minecraft.Models
{
    public class Player:ICreature
    {
        private double health;
        private int countToFall=0;
        private Point position;
        private string type = "player";
        private int Size = 40;
        private bool isIntoSpaceSheep = false;

        public Player(Point newPosition)
        {
            position = newPosition;
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

        public Point GetChangePosition(int[,] map, ICreature playerPosition, int[] decorationObject, ScreenPoint ChangingScreen, List<GlowingBalls> flyingObjects)
        {
            throw new NotImplementedException();
        }

        public void MakeNewICreature(Point creatureFinish, string type,int tickCount, List<GlowingBalls> flyingObjects, int[,] map, int[] decorationObject)
        {
            flyingObjects.Add(new GlowingBalls(position, creatureFinish, "player",Size,map,decorationObject));
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

        public bool IsintoSpaceSheep()
        {
            return isIntoSpaceSheep;
        }

        public void ChangeIsIntoSpaceSheep(bool isInto,int countCreatures)
        {
            if (isInto == true && new Point() { X = 700, Y = 455 }.IsPointIntoAreaSquear( position, 150) && countCreatures == 0)
                isIntoSpaceSheep = true;
            else if (isInto == false)
                isIntoSpaceSheep = false;
        }
    }
}
