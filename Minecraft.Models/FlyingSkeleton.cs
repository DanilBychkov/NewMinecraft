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
        Point position;
        double health;
        List<Point> positions;
        Point deltaChangePoisition = new Point() { X = 0, Y = 0 };
        int count = 0;
        string type = "monster";
        int lastTick = -70;
        int Size = 100;

        public FlyingSkeleton(Point point)
        {
            CreateListDelta();
            position = point;
            health = 20;
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

        public bool IsWay(Point pointPlayer,int[,] map, int[] decorationObject,int size)
        {
            var point1 = IsDecorationObject(new Point() { X = pointPlayer.X , Y = pointPlayer.Y  }, map, decorationObject);
            var point2 = IsDecorationObject(new Point() { X = pointPlayer.X , Y = pointPlayer.Y + size }, map, decorationObject);
            var point3 = IsDecorationObject(new Point() { X = pointPlayer.X + size, Y = pointPlayer.Y },  map, decorationObject);
            var point4 = IsDecorationObject(new Point() { X = pointPlayer.X + size, Y = pointPlayer.Y + size }, map, decorationObject);
            return point1 && point2 && point3 && point4;
        }

        private bool IsDecorationObject(Point playerPoint,int[,] map, int[] decorationObject)
        {
            return decorationObject.Contains(map[(int)(playerPoint.X) / 60, (int)(playerPoint.Y) / 60]);
        }

        public Point GetChangePosition(int[,] map, ICreature playerPosition,int[] decorationObject, ScreenPoint ChangingScreen)
        {
            
            if (positions.Count == 0)
                CreateListDelta();
            position = new Point() { X = position.X - positions[0].X, Y = position.Y - positions[0].Y };
            positions.RemoveAt(0);
            return position;
        }

        public void MakeNewICreature(Point creatureFinish, string type, int tickCount, List<FlyingObject> flyingObjects)
        {
            if (tickCount-lastTick >= 70)
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

        private void CreateListDelta()
        {
            count += 1;
            if (count == 5)
            { }
            var points = new List<Point>();
            var random = new Random();
            var rnd = random.Next(5, 13);
            var rndX = -random.Next(-1, 1);
            var rndY = -random.Next(-1, 1);

            if (Math.Abs(deltaChangePoisition.X) >= 90)
            {
                rnd = 30;
                rndX = -1 * deltaChangePoisition.X / Math.Abs(deltaChangePoisition.X);
            }
            else if (Math.Abs(deltaChangePoisition.Y) >= 30)
            {
                rnd = 10;
                rndY = -1 * deltaChangePoisition.Y / Math.Abs(deltaChangePoisition.Y);
            }

            for (var i = 0; i < rnd; i++)
            {
                points.Add(new Point(rndX * 3, rndY * 3));
            }
            deltaChangePoisition.X += rnd * rndX * 3;
            deltaChangePoisition.Y += rnd * rndY * 3;
            positions = points;
        }
    }
}
