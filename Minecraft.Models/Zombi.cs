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
            if (IsIntoBall(position, playerPosition, 200))
               sleep=false;
        }

        public bool IsSleep()
        {
            return sleep;
        }

        public Point GetChangePosition(int[,] map, ICreature player,int[] decorationObject, ScreenPoint ChangingScreen)
        {
            GetNextPointToPlayer(map, player, decorationObject, ChangingScreen);
            return position;
        }

        public Point GetPosition()
        {
            return position;
        }

        public void MakeNewICreature(Point creatureFinish, string type,int tickCount, List<FlyingObject> flyingObjects)
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

        private static Dictionary<Point, SinglyLinkedList<Point>> FindWays(int[,] map, Point start, ScreenPoint ChangingScreen, int[] decorationObject, Point end)//
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
                for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                    {
                        if (dx == 0 && dy == 0 || dx != 0 && dy != 0) continue;
                        var nextPoint = new Point() { X = point.X + dx * 5, Y = point.Y + dy * 5 };
                        if (!ways.ContainsKey(nextPoint))
                        {
                            if (!IsWay(point, ChangingScreen, map, decorationObject, new Point() { X = dx * 10, Y = dy * 10 }, 40)) continue;
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

        private static bool IsWay(Point pointPlayer, ScreenPoint ChangingScreen, int[,] map, int[] decorationObject, Point delta, int size)
        {
            return IsDecorationObject(new Point() { X = pointPlayer.X + delta.X, Y = pointPlayer.Y + delta.Y }, map, decorationObject)
            && IsDecorationObject(new Point() { X = pointPlayer.X + delta.X, Y = pointPlayer.Y + delta.Y + size }, map, decorationObject)
            && IsDecorationObject(new Point() { X = pointPlayer.X + delta.X + size, Y = pointPlayer.Y + delta.Y }, map, decorationObject)
            && IsDecorationObject(new Point() { X = pointPlayer.X + delta.X + size, Y = pointPlayer.Y + delta.Y + size }, map, decorationObject);
        }

        private static bool IsDecorationObject(Point playerPoint,int[,] map, int[] decorationObject)
        {
            return decorationObject.Contains(map[(int)(playerPoint.X) / 60, (int)(playerPoint.Y) / 60]);
        }

        private void GetNextPointToPlayer(int[,] map, ICreature player, int[] decorationObject, ScreenPoint ChangingScreen)
        {
            var ways = FindWays(map, position, ChangingScreen, decorationObject, new Point() { X = player.GetPosition().X, Y = player.GetPosition().Y }).ToArray();
            if (ways.Count() <= 5)
                player.ChangeHealth(0.5);
            if (ways.Count() > 1)
            {
                var t = ways[ways.Length - 1].Value.Reverse().Skip(1).First();
                position = (new Point() { X = t.X, Y = t.Y });
            }
        }

        private static bool IsIntoBall(Point pointPlayer, Point objectPoint, int radious)
        {
            return Math.Sqrt((pointPlayer.X - objectPoint.X) * (pointPlayer.X - objectPoint.X) + (pointPlayer.Y - objectPoint.Y) * (pointPlayer.Y - objectPoint.Y)) <= radious;
        }
    }
}
