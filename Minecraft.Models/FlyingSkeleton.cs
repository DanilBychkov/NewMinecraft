using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Models
{
    public class FlyingSkeleton:ICreature
    {
        private Point position;
        private double health;
        private List<Point> positions;
        private Point deltaChangePoisition = new Point() { X = 0, Y = 0 };
        private string type = "monster";
        private int lastTick ;
        private int size = 100;

        public FlyingSkeleton(Point point,int tick)
        {
            CreateListDelta();
            position = point;
            health = 20;
            lastTick = tick;
        }

        public void ChangeHealth(double damage)
        {
            health -= damage;
        }

        public Point GetPosition()
        {
            if (positions.Count == 0)
            {
                CreateListDelta();
                position = new Point() { X = position.X - positions[0].X, Y = position.Y - positions[0].Y };
            }
            return position;
        }

        public bool IsSleep()
        {
            return false;
        }

        public Point GetChangePosition(int[,] map, ICreature playerPosition, int[] decorationObject, ScreenPoint ChangingScreen, List<GlowingBalls> flyingObjects)
        {
            if (positions.Count == 0)
                CreateListDelta();
            position = new Point() { X = position.X - positions[0].X, Y = position.Y - positions[0].Y };
            positions.RemoveAt(0);
            return position;
        }

        public void MakeNewICreature(Point creatureFinish, string type, int tickCount, List<GlowingBalls> flyingObjects, int[,] map, int[] decorationObject)
        {
            if (tickCount-lastTick >= 70)
            {
                lastTick = tickCount;
                flyingObjects.Add(new GlowingBalls(position, creatureFinish, "monster",size,map,decorationObject));
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
            return size;
        }

        public void ChangeSleep(Point playerPosition)
        {

        }

        private void CreateListDelta()
        {
            var random = new Random();
            if (Math.Abs(deltaChangePoisition.X) >= 90)
                ChangePoints(30, -1 * deltaChangePoisition.X / Math.Abs(deltaChangePoisition.X), random.Next(-1, 1));
            else if (Math.Abs(deltaChangePoisition.Y) >= 30)
                ChangePoints(10, random.Next(-1, 1), -1 * deltaChangePoisition.Y / Math.Abs(deltaChangePoisition.Y));
            else
                ChangePoints(random.Next(5, 13), random.Next(-1, 1), random.Next(-1, 1));
        }

        private void ChangePoints(int count,int deltaX,int deltaY)
        {
            var points = new List<Point>();
            for (var i = 0; i < count; i++)
                points.Add(new Point(deltaX * 3, deltaY * 3));
            deltaChangePoisition.X += count * deltaX * 3;
            deltaChangePoisition.Y += count * deltaY * 3;
            positions = points;
        }
    }
}
