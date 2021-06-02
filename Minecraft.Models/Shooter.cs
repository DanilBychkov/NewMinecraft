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
        private int Size = 40;
        private List<Point> positions = new List<Point>();
        private int deltaPoint=0;
        private bool isFly = false;

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

        public Point GetPosition()
        {
            return position;
        }

        public bool IsSleep()
        {
            return false;
        }

        private void ChanePositionLeftRight()
        {
            var rnd = new Random();
            var count = rnd.Next(5, 15);
            var speed = 5;
            if (Math.Abs(deltaPoint) >= 100)
                for (var i = 0; i < 20; i++)
                {
                    var direction = -Math.Abs(deltaPoint)/deltaPoint;
                    positions.Add(new Point() { X = position.X + i * speed * direction, Y = position.Y });
                    deltaPoint += speed * direction;
                }
            else
            {
                var direction = rnd.Next(-1, 1);
                for (var i = 0; i < count; i++)
                {
                    positions.Add(new Point() { X = position.X + i * speed * direction, Y = position.Y });
                    deltaPoint += speed * direction;
                }
            }
        }

        public Point GetChangePosition(int[,] map, ICreature playerPosition, int[] decorationObject, ScreenPoint ChangingScreen,List<GlowingBalls> flyingObjects)
        { 
            if (!isFly)
                foreach (var flyingObject in flyingObjects)
                {
                    var positionFlyingObject = flyingObject.GetPositionWithOutRemove();
                    if (Math.Abs((position.X - positionFlyingObject.X)) <150 && position.Y<positionFlyingObject.Y)
                    {
                        positions = new List<Point>();
                        for (var i = 0; i < 10; i++)
                        {
                            positions.Add(new Point() { X = position.X, Y = position.Y - 13 * i });
                        }
                        var newPosition = positions.Last();
                        for (var i = 0; i < 10; i++)
                        {
                            positions.Add(new Point() { X = newPosition.X, Y = newPosition.Y + 13 * i });
                        }
                        isFly = true;
                        break;
                    }
                }
            if (positions.Count == 0)
            {
                ChanePositionLeftRight();
                isFly = false;
            }
            position = positions[0];
            positions.RemoveAt(0);
            return position;
        }

        public void MakeNewICreature(Point creatureFinish, string type, int tickCount, List<GlowingBalls> flyingObjects,
            int[,] map, int[] decorationObject)
        {
            if (tickCount - lastTick >=15)
            {
                lastTick = tickCount;
                flyingObjects.Add(new GlowingBalls(position, creatureFinish, "monster",Size,map,decorationObject));
            }
        }

        public void ChangeSleep(Point playerPosition)
        {

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
