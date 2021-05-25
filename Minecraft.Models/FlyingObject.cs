using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Minecraft.Models
{
    public class FlyingObject
    {
        private List<Point> position;
        private int damage = 5;
        private string type;
        private double health = 5;
        private int size=15;
        public FlyingObject(Point creatureStart,Point creatureFinsh,string newType,int size)
        {
            ChangeListPoisitons(creatureStart, creatureFinsh,size);
            type = newType;
        }

        public string GetTypeObject()
        {
            return type;
        }

        public void ChangeHealth(double damage)
        {
            health -= damage;
        }

        public Point GetPosition()
        {
            var pos = position[0];
            position.RemoveAt(0);
            return pos;
        }

        public int GetSize()
        {
            return size;
        }

        public bool IsDied(List<ICreature> icreatures,List<FlyingObject> flyingObjects,Player player,int[,] map,int[] decorationObject)
        {
            if (position.Count != 0)
                IsDiedInConflict(position[0], icreatures,player, flyingObjects, map, decorationObject);
            else
                health = 0;
            return health <= 0;
        }

        public void ChahgeHealth(double damage)
        {
            health -= damage;
        }

        private void IsDiedInConflict(Point position,List<ICreature> icreatures,Player player,List<FlyingObject> flyingObjects,int[,] map,int[]decorationObject)//сделать массив bool один возвращает deadinConflic а второй был ли это объект //нужно передавать ещё игрока
        {
            if (type == "monster" && IsBreak(player, position, player))
            {
                player.ChangeHealth(damage);
                health = 0;
            }
            else if (type == "player")
                foreach (var icreature in icreatures)
                {
                    if (IsBreak(icreature, position, player))
                    {
                        icreature.ChangeHealth(damage);
                        health = 0;
                        break;
                    }
                }
            else if (this.position.Count == 0 || !IsWay(position, map, decorationObject, size))
                health = 0;
        }

        private bool IsPointIntoArea(Point areaPoint, Point objectPoint, int sizeArea)
        {
            return areaPoint.X <= objectPoint.X && areaPoint.X + sizeArea >= objectPoint.X
               && areaPoint.Y <= objectPoint.Y && areaPoint.Y + sizeArea >= objectPoint.Y;
        }

        private bool IsBreak(ICreature icreature,Point position,Player player)
        {
            return IsPointIntoArea(icreature.GetPosition(), position, icreature.GetSize())
                || IsPointIntoArea(icreature.GetPosition(), new Point() { X = position.X, Y = position.Y + size }, icreature.GetSize())
                || IsPointIntoArea(icreature.GetPosition(), new Point() { X = position.X + size, Y = position.Y + size }, player.GetSize())
                || IsPointIntoArea(icreature.GetPosition(), new Point() { X = position.X + size, Y = position.Y }, player.GetSize());
        }

        private bool IsWay(Point pointPlayer, int[,] map, int[] decorationObject,int size)
        {
            return IsDecorationObject(new Point() { X = pointPlayer.X, Y = pointPlayer.Y }, map, decorationObject) &&
            IsDecorationObject(new Point() { X = pointPlayer.X, Y = pointPlayer.Y + size }, map, decorationObject) &&
            IsDecorationObject(new Point() { X = pointPlayer.X + size, Y = pointPlayer.Y }, map, decorationObject) &&
            IsDecorationObject(new Point() { X = pointPlayer.X + size, Y = pointPlayer.Y + size }, map, decorationObject);
        }

        private bool IsDecorationObject(Point playerPoint, int[,] map, int[] decorationObject)
        {
            return decorationObject.Contains(map[(int)(playerPoint.X) / 60, (int)(playerPoint.Y) / 60]);
        }

        private void ChangeListPoisitons(Point positionPlayer, Point goalPosition,int size)
        {
            position = ChangePointsSkeleton(new Point() { X = positionPlayer.X, Y = positionPlayer.Y },new Point() { X = goalPosition.X, Y = goalPosition.Y },size);
        }

        private List<Point> ChangePointsSkeleton(Point positionSkeleton, Point pointPlayer,int size)
        {
            var newPosition = new Point() { X = positionSkeleton.X +size/2, Y = positionSkeleton.Y+size/2 };//обратить внимание
            var resoult = new List<Point>();
            if (newPosition.X - pointPlayer.X == 0 && newPosition.Y - pointPlayer.Y == 0)
                return resoult;
            else if (newPosition.X - pointPlayer.X == 0)
                return AddPoint(1, new Point() { X = 0, Y = -(Math.Abs(newPosition.Y - pointPlayer.Y) / (newPosition.Y - pointPlayer.Y)) },
                    Math.Abs(newPosition.Y - pointPlayer.Y) + size, newPosition);
            else if (newPosition.Y - pointPlayer.Y == 0)
                return AddPoint(1, new Point() { X = -(Math.Abs(newPosition.X - pointPlayer.X) / (newPosition.X - pointPlayer.X)), Y = 0 },
                    Math.Abs(newPosition.X - pointPlayer.X) + size, newPosition);
            else
                return AddPoint( Math.Abs((newPosition.X - pointPlayer.X) * 1.0 / (newPosition.Y - pointPlayer.Y)),
                    new Point()
                    {
                        X = -(Math.Abs(newPosition.X - pointPlayer.X) / (newPosition.X - pointPlayer.X)),
                        Y = -(Math.Abs(newPosition.Y - pointPlayer.Y) / (newPosition.Y - pointPlayer.Y))
                    },
                    Math.Abs(newPosition.X - pointPlayer.X) + size, newPosition);
        }

        private List<Point> AddPoint(double corner,Point direction,int stop,Point start)
        {
            var resoult = new List<Point>();
            for (var v = 0; v * corner <stop; v += 35)
            {
                var nextPoint = new Point() { X = (int)(start.X + direction.X * v * corner), Y = (int)(start.Y + direction.Y * v) };
                resoult.Add(nextPoint);
            }
            return resoult;
        }
    }
}
