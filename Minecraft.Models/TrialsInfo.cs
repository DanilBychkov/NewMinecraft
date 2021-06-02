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
            else if(trial==Trials.ArenaTrial)
                return new Point() { X = 500, Y = 559 };
            return new Point() { X = 100, Y = 200 };
        }

        public Point GetChangingScreen(Trials trial)
        {
            if (trial == Trials.CaveTrial)
                return new Point() { X = 0, Y =330};
            else if (trial == Trials.MainTrial)
                return new Point() { X = 0, Y = 630};
            else if (trial == Trials.ArenaTrial)
                return new Point() { X = 0, Y = 155};
            return new Point() { X = 0, Y = 559 };
        }

        public List<Block> GetStartBlocks(ScreenPoint screen,int tickCount)
        {
            var blocks = new List<Block>();
            blocks.Add(new Block(500, 670, screen, tickCount));
            blocks.Add( new Block(940, 670, screen, tickCount));
            return blocks;
        }

        public bool IsPointToNextLevel(Trials trial,Point point)
        {
            if (Trials.CaveTrial == trial && point.IsIntoBall(new Point() {X=730,Y=841},40))
                return true;
            if (Trials.MainTrial == trial && point.IsIntoBall( new Point() { X = 2640, Y = 1080 }, 150))
                return true;
            if (Trials.ArenaTrial == trial && point.IsIntoBall(new Point() { X = 1300, Y = 120 }, 150))
                return true;
            return false;
        }

        public Trials GetNextTrial(Trials trial)
        {
            if (trial == Trials.CaveTrial)
                return Trials.MainTrial;
            else if (trial == Trials.MainTrial)
                return Trials.ArenaTrial;
            else if(trial==Trials.ArenaTrial)
                return Trials.FlyingUFO;
            return Trials.CaveTrial;
        }

        public List<ICreature> GetMonstersTrial(Trials trial)
        {
            if (trial == Trials.CaveTrial)
                return monstersCaveTrial;
            else if (trial == Trials.MainTrial)
                return monstersMainTrial;
            else if(trial==Trials.ArenaTrial)
                return monstersArenaTrial;
            return new List<ICreature>();
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
        }

        private void FillMonstersArenaTrial()
        {
            monstersArenaTrial.Add(new Shooter(new Point() { X = 500, Y = 559 }, -30));
        }
    }
}
