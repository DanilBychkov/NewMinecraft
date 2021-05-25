using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Minecraft.Models
{
    public class TrialsInfo
    {
        private List<ICreature> monstersCaveTrial=new List<ICreature>();
        private List<ICreature> monstersMainTrial=new List<ICreature>();
        private List<ICreature> monstersArenaTrial=new List<ICreature>();

        public TrialsInfo()
        {
            FillMonstersArenaTrial();
            FillMonstersCaveTrial();
            FillMonstersMainTrial();
        }

        public Point GetStartPoint(Trials trial)
        {
            if (trial == Trials.CaveTrial)
                return new Point() { X = 181, Y = 841 };
            else if (trial == Trials.MainTrial)
                return new Point() {X=190,Y=1099};
            return new Point() { X = 500, Y = 559 };
        }

        public bool IsPointToNextLevel(Trials trial,Point point)
        {
            if (Trials.CaveTrial == trial && IsIntoBall(point,new Point() {X=730,Y=841},40))
                return true;
            if (Trials.MainTrial == trial && IsIntoBall(point, new Point() { X = 3500, Y = 1039 }, 40))
                return true;
            return false;
        }

        public Trials GetNextTrial(Trials trial)
        {
            if (trial == Trials.CaveTrial)
                return Trials.MainTrial;
            else if (trial == Trials.MainTrial)
                return Trials.ArenaTrial;
            return Trials.CaveTrial;
        }

        public List<ICreature> GetMonstersTrial(Trials trial)
        {
            if (trial == Trials.CaveTrial)
                return monstersCaveTrial;
            else if (trial == Trials.MainTrial)
                return monstersMainTrial;
            return monstersArenaTrial;
        }

        private void FillMonstersCaveTrial()
        {
            monstersCaveTrial.Add(new Zombi(new Point() { X = 540, Y = 180 }));
            monstersCaveTrial.Add(new Zombi(new Point() { X = 241, Y = 421 }));
            monstersCaveTrial.Add(new Zombi(new Point() { X = 661, Y = 480 }));
        }

        private void FillMonstersMainTrial()
        {
            monstersMainTrial.Add(new FlyingSkeleton(new Point() { X = 1100, Y = 620 }, -70));
            monstersMainTrial.Add(new FlyingSkeleton(new Point() { X = 800, Y = 420 }, -50));
            monstersMainTrial.Add(new FlyingSkeleton(new Point() { X = 1500, Y = 550 }, -60));
            monstersMainTrial.Add(new FlyingSkeleton(new Point() { X = 3300, Y = 850 }, -56));
        }

        private void FillMonstersArenaTrial()
        {
            monstersArenaTrial.Add(new Shooter(new Point() { X = 180, Y = 220 }, -30));
            monstersArenaTrial.Add(new Shooter(new Point() { X = 240, Y = 340 }, -20));
            monstersArenaTrial.Add(new Shooter(new Point() { X = 1140, Y = 220 }, -10));
            monstersArenaTrial.Add(new Shooter(new Point() { X = 1080, Y = 340 }, 0));
        }

        private bool IsIntoBall(Point pointPlayer, Point objectPoint, int radious)// убрат метод потом
        {
            return Math.Sqrt((pointPlayer.X - objectPoint.X) * (pointPlayer.X - objectPoint.X) + (pointPlayer.Y - objectPoint.Y) * (pointPlayer.Y - objectPoint.Y)) <= radious;
        }
    }
}
