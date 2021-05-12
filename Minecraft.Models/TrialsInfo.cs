using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class TrialsInfo
    {
        public TrialsInfo()
        { 
        
        }

        public Point GetStartPoint(Trials trial)
        {

            if (trial == Trials.CaveTrial)
                return new Point() { X = 181, Y = 841 };
            else if (trial == Trials.MainTrial)
                return new Point() {X=190,Y=1099};
            return new Point() { X = 500, Y = 559 };//
        }

        public bool IsIntoBall(Point pointPlayer, Point objectPoint, int radious)// убрат метод потом
        {
            return Math.Sqrt((pointPlayer.X - objectPoint.X) * (pointPlayer.X - objectPoint.X) + (pointPlayer.Y - objectPoint.Y) * (pointPlayer.Y - objectPoint.Y)) <= radious;
        }

        public bool IsPointToNextLevel(Trials trial,Point point)// еще нужен screen point? либо передаём точку с уже изменёнными координатами
        {
            if (Trials.CaveTrial == trial && IsIntoBall(point,new Point() {X=730,Y=841},40))
                return true;
            if (Trials.MainTrial == trial && IsIntoBall(point, new Point() { X = 3500, Y = 1039 }, 40))
                return true;
            return false;
        }

        public Trials GetNextTrial(Trials trial)// возвращает null если нет следующего уровня если поставить поставить Trials? можно вернуть null
        {
            if (trial == Trials.CaveTrial)
                return Trials.MainTrial;
            else if (trial == Trials.MainTrial)
                return Trials.ArenaTrial;
            return Trials.CaveTrial;
        }

        public List<ICreature> GetMonstersTrial(Trials trial)
        {
            List<ICreature> monstersCaveTrial = new List<ICreature>();
            monstersCaveTrial.Add(new Zombi(new Point() { X = 540, Y = 180 }));
            monstersCaveTrial.Add( new Zombi(new Point() { X = 241, Y = 421 }));
            monstersCaveTrial.Add( new Zombi(new Point() { X = 661, Y = 480 }));
            List<ICreature> monstersMainTrial = new List<ICreature>();
            monstersMainTrial.Add(new FlyingSkeleton(new Point() {X=1100,Y=620}));
            monstersMainTrial.Add(new FlyingSkeleton(new Point() { X = 800, Y = 420 }));
            monstersMainTrial.Add(new FlyingSkeleton(new Point() { X = 1500, Y = 550 }));
            monstersMainTrial.Add(new FlyingSkeleton(new Point() { X = 3300, Y = 850 }));
            List<ICreature> monstersArenaTrial = new List<ICreature>();
            monstersArenaTrial.Add(new Shooter(new Point() {X=400,Y=560}));
            monstersArenaTrial.Add(new Shooter(new Point() { X = 750, Y = 560 }));
            if (trial == Trials.CaveTrial)
                return monstersCaveTrial;
            if (trial == Trials.MainTrial)
                return monstersMainTrial;
           return monstersArenaTrial;
        }
    }
}
