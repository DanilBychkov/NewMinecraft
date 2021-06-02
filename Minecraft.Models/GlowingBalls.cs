using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Minecraft.Models
{
    public class GlowingBalls
    {
        private List<Point> position;
        private int damage = 5;
        private string type;
        private double health = 5;
        private int size=15;
        public GlowingBalls(Point creatureStart,Point creatureFinsh,string newType,int size,int[,] map,int[]decorationObject)
        {
            ChangeListPoisitons(creatureStart, creatureFinsh,size,map,decorationObject);
            type = newType;
        }

        public string GetTypeObject()
        {
            return type;
        }

        public Point GetPosition()
        {
            var pos = new Point();
            if (position != null && position.Count!=0)
            {
                pos = position[0];
                position.RemoveAt(0);
            }
            return pos;
        }

        public Point GetPositionWithOutRemove()
        {
            var pos = new Point();
            if (position != null && position.Count != 0)
            {
                pos = position[0];
            }
            return pos;
        }

        public int GetSize()
        {
            return size;
        }

        public bool IsDied(List<ICreature> icreatures,List<GlowingBalls> flyingObjects,Player player,int[,] map,int[] decorationObject)
        {
            if (position!=null &&position.Count != 0)
                IsDiedInConflict(position[0], icreatures,player, flyingObjects, map, decorationObject);
            else
                health = 0;
            return health <= 0;
        }

        public void ChahgeHealth(double damage)
        {
            health -= damage;
        }

        private void IsDiedInConflict(Point position,List<ICreature> icreatures,Player player,List<GlowingBalls> flyingObjects,int[,] map,int[]decorationObject)//сделать массив bool один возвращает deadinConflic а второй был ли это объект //нужно передавать ещё игрока
        {
            if (type == "monster" && IsBreak(player, position))
            {
                player.ChangeHealth(damage);
                health = 0;
            }
            else if (type == "player")
                foreach (var icreature in icreatures)
                {
                    if (IsBreak(icreature, position))
                    {
                        icreature.ChangeHealth(damage);
                        health = 0;
                        break;
                    }
                }
            if (this.position.Count == 0 || !position.IsWay(map, decorationObject, size))
                health = 0;
        }

        private bool IsPointIntoAreaSquear(Point areaPoint, Point objectPoint, int sizeArea)
        {
            return areaPoint.X <= objectPoint.X && areaPoint.X + sizeArea >= objectPoint.X
               && areaPoint.Y <= objectPoint.Y && areaPoint.Y + sizeArea >= objectPoint.Y;
        }

        private bool IsBreak(ICreature icreature,Point position)
        {
            return IsPointIntoAreaSquear(icreature.GetPosition(), position, icreature.GetSize())
                || IsPointIntoAreaSquear(icreature.GetPosition(), new Point() { X = position.X+size, Y = position.Y }, icreature.GetSize())
                || IsPointIntoAreaSquear(icreature.GetPosition(), new Point() { X = position.X, Y = position.Y+size }, icreature.GetSize())
                || IsPointIntoAreaSquear(icreature.GetPosition(), new Point() { X = position.X+size, Y = position.Y+size }, icreature.GetSize());
        }

        private void ChangeListPoisitons(Point positionPlayer, Point goalPosition,int size, int[,] map, int[] decorationObject)
        {
            position = ChangePointsSkeleton(new Point() { X = positionPlayer.X, Y = positionPlayer.Y },new Point() { X = goalPosition.X, Y = goalPosition.Y },map,decorationObject);
        }

        private List<Point> ChangePointsSkeleton(Point startPoint, Point finishPoint,int[,] map,int[] decorationObject)
        {
            var resoult = new List<Point>();
                resoult.Add(startPoint);
                var speed = 38;
                var corner = Math.Atan2((finishPoint.Y - startPoint.Y), (finishPoint.X - startPoint.X));
                var time = 1;
                var direction = new Point() { X = 1, Y = 1 };
                while (resoult.Last() != finishPoint)
                {
                    var newX = startPoint.X + speed * Math.Cos(corner) * time * direction.X;
                    var newY = startPoint.Y + speed * Math.Sin(corner) * time * direction.Y;
                    var nextPoint = new Point() { X = (int)newX, Y = (int)newY };
                    time += 1;
                    if (IsPointIntoArea(finishPoint, nextPoint, 40))
                    {
                        resoult.Add(nextPoint);
                        nextPoint = finishPoint;
                    }
                    resoult.Add(nextPoint);
                }
                
            return resoult;
        }

        private bool IsPointIntoArea(Point areaPoint, Point objectPoint, int sizeArea)
        {
            return Math.Abs(areaPoint.X - objectPoint.X) <= sizeArea
               && Math.Abs(objectPoint.Y - objectPoint.Y) <= sizeArea;
        }
    }
}
