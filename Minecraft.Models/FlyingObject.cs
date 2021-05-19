﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Minecraft.Models
{
    public class FlyingObject
    {
        List<Point> position;
        int damage = 5;
        string type;
        double health = 5;
        int size=15;
        public FlyingObject(Point creatureStart,Point creatureFinsh,string type,int size)
        {
            ChangeListPoisitons(creatureStart, creatureFinsh,size);
            this.type = type;
        }

        public string GetTypeObject()
        {
            return type;
        }

        public void ChangeHealth(double damage)
        {
            health -= damage;
        }

        public Point GetPosition()//нужно будет учитывать кол-во точек и diedInConflict
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

        public bool IsPointIntoArea(Point areaPoint, Point objectPoint,int sizeArea)
        {
            return areaPoint.X <= objectPoint.X && areaPoint.X +sizeArea >= objectPoint.X
               && areaPoint.Y <= objectPoint.Y && areaPoint.Y + sizeArea >= objectPoint.Y;
        }

        public void ChahgeHealth(double damage)
        {
            health -= damage;
        }

        private void IsDiedInConflict(Point position,List<ICreature> icreatures,Player player,List<FlyingObject> flyingObjects,int[,] map,int[]decorationObject)//сделать массив bool один возвращает deadinConflic а второй был ли это объект //нужно передавать ещё игрока
        {
            var g = false;
            if (type == "monster")
            {
                if (IsPointIntoArea(player.GetPosition(), position,player.GetSize()) || IsPointIntoArea(player.GetPosition(), new Point() { X = position.X, Y = position.Y + size },player.GetSize())
                   || IsPointIntoArea(player.GetPosition(), new Point() { X = position.X + size, Y = position.Y + size },player.GetSize())
                   || IsPointIntoArea(player.GetPosition(), new Point() { X = position.X + size, Y = position.Y },player.GetSize()))
                {
                    player.ChangeHealth(damage);
                    health = 0;
                }
            }
            else
            {
                foreach (var icreature in icreatures)
                {
                    g = g || (IsPointIntoArea(icreature.GetPosition(), position,icreature.GetSize()) || IsPointIntoArea(icreature.GetPosition(), new Point() { X = position.X, Y = position.Y + size },icreature.GetSize())
                    || IsPointIntoArea(icreature.GetPosition(), new Point() { X = position.X + size, Y = position.Y + size },player.GetSize())
                    || IsPointIntoArea(icreature.GetPosition(), new Point() { X = position.X + size, Y = position.Y },player.GetSize()));
                    if (g == true)
                    {
                        icreature.ChangeHealth(damage);
                        health = 0;
                        break;
                    }
                }
            }
            g = !IsWay(position, map, decorationObject, size);
            if (this.position.Count == 0)
                g = true;
            if (g)
                health = 0;
        }

        public bool IsWay(Point pointPlayer, int[,] map, int[] decorationObject,int size)
        {
            var point1 = IsDecorationObject(new Point() { X = pointPlayer.X, Y = pointPlayer.Y},map, decorationObject);
            var point2 = IsDecorationObject(new Point() { X = pointPlayer.X, Y = pointPlayer.Y + size }, map, decorationObject);
            var point3 = IsDecorationObject(new Point() { X = pointPlayer.X  + size, Y = pointPlayer.Y  }, map, decorationObject);
            var point4 = IsDecorationObject(new Point() { X = pointPlayer.X + size, Y = pointPlayer.Y + size }, map, decorationObject);
            return point1 && point2 && point3 && point4;
        }

        private bool IsDecorationObject(Point playerPoint, int[,] map, int[] decorationObject)
        {
            return decorationObject.Contains(map[(int)(playerPoint.X) / 60, (int)(playerPoint.Y) / 60]);
        }

        public void ChangeListPoisitons(Point positionPlayer, Point goalPosition,int size)
        {
            position = ChangePointsSkeleton(new Point() { X = positionPlayer.X, Y = positionPlayer.Y },new Point() { X = goalPosition.X, Y = goalPosition.Y },size);
        }

        public List<Point> ChangePointsSkeleton(Point positionSkeleton, Point pointPlayer,int size)
        {
            var pos = new Point() { X = positionSkeleton.X +size/2, Y = positionSkeleton.Y+size/2 };//обратить внимание
            var resoult = new List<Point>();
            if (pos.X - pointPlayer.X == 0 && pos.Y - pointPlayer.Y == 0)
                return resoult;
            else if (pos.X - pointPlayer.X == 0)
            {
                return resoult;//доработать
            }
            else if (pos.Y - pointPlayer.Y == 0)
            {
                return resoult;//доработать
            }
            else
            {
                var atgf = Math.Abs((pos.X - pointPlayer.X) * 1.0 / (pos.Y - pointPlayer.Y));
                for (var v = 0; v*atgf < Math.Abs(pos.X - pointPlayer.X) + 16; v+=35)
                {
                    var directionSkeletin = new Point() { X = -(Math.Abs(pos.X - pointPlayer.X) / (pos.X - pointPlayer.X)), Y = -(Math.Abs(pos.Y - pointPlayer.Y) / (pos.Y - pointPlayer.Y)) };
                    var nextPoint = new Point() { X = (int)(pos.X+ directionSkeletin.X * v * atgf), Y = (int)(pos.Y+ directionSkeletin.Y *v ) };
                    resoult.Add(nextPoint);
                }
            }

            return resoult;
        }
    }
}
