using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Minecraft.Models
{
    public class Zombi:ICreature
    {
        private double health;
        private Point position;
        private bool sleep;
        private string type = "monster";
        private int size = 40;
        public Zombi(Point newPosition)
        {
            position = newPosition;
            sleep = true;
            health = 10;
        }

        public void ChangeHealth(double damage)
        {
            health += damage;
        }

        public void ChangeSleep(Point playerPosition)
        {
            if (position.IsIntoBall( playerPosition, 200))
               sleep=false;
        }

        public bool IsSleep()
        {
            return sleep;
        }

        public Point GetChangePosition(int[,] map, ICreature player, int[] decorationObject, ScreenPoint ChangingScreen, List<GlowingBalls> flyingObjects)
        {
            GetNextPointToPlayer(map, player, decorationObject);
            return position;
        }

        public Point GetPosition()
        {
            return position;
        }

        public void MakeNewICreature(Point creatureFinish, string type,int tickCount, List<GlowingBalls> flyingObjects, int[,] map, int[] decorationObject)
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
            return size;
        }

        private static Dictionary<Point, SinglyLinkedList<Point>> FindWays(int[,] map, Point start,int[] decorationObject, Point end)//
        {
            var ways = new Dictionary<Point, SinglyLinkedList<Point>>();
            var queue = new Queue<Point>();
            var visited = new HashSet<Point>();
            queue.Enqueue(start);
            visited.Add(start);
            ways.Add(start, new SinglyLinkedList<Point>(start));
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                var points = new Point[] { new Point() { X = 1, Y = 0 }, new Point() { X = -1, Y = 0 }, new Point() { X = 0, Y = 1 }, new Point() { X = 0, Y = -1 } };
                foreach (var t in points)
                {
                    var nextPoint = new Point() { X = point.X + t.X * 5, Y = point.Y + t.Y * 5 };
                    if (!ways.ContainsKey(nextPoint))
                    {
                        if (!nextPoint.IsWay( map, decorationObject, 40)) continue;
                        queue.Enqueue(nextPoint);
                        visited.Add(nextPoint);
                        ways.Add(nextPoint, new SinglyLinkedList<Point>(nextPoint, ways[point]));
                        if (IsPointPlayer(end, nextPoint))
                            return ways;
                    }
                }
            }
            return ways;
        }

        private static bool IsPointPlayer(Point pointPlayer, Point pointMonster)
        {
            if (Math.Abs(pointPlayer.X - pointMonster.X) < 40 && Math.Abs(pointMonster.Y - pointPlayer.Y) < 40) 
                return true;
            return false;
        }

        private void GetNextPointToPlayer(int[,] map, ICreature player, int[] decorationObject)
        {
            var ways = FindWays(map, position, decorationObject, new Point() { X = player.GetPosition().X, Y = player.GetPosition().Y }).ToArray();
            if (ways.Count() <= 5)
                player.ChangeHealth(0.5);
            if (ways.Count() > 1)
            {
                var t = ways[ways.Length - 1].Value.Reverse().Skip(1).First();
                position = (new Point() { X = t.X, Y = t.Y });
            }
        }
    }
}
