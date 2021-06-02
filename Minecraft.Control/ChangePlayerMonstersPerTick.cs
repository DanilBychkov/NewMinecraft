using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class ChangePlayerMonstersPerTick
    {
        public void ChangeStatesPerTick(ref Player player,ref ScreenPoint screen,ref int[,] map,ref int[] decorationObject,
            ref Trials trial,ref List<ICreature> creatures,ref List<GlowingBalls> glowingBalls,ref int tickCount,ref List<Block> blocks)
        {
            if (trial == Trials.MainTrial && IsPointIntoArea(new Point() { X = 2580, Y = 580 }, player.GetPosition(), 200, 80))
                map = new Maps().GetMainMapFall();  
            if (Trials.FlyingUFO!=trial)
            {
                GetChangePosition(player, screen, map, decorationObject, trial);
                var trialInfo = new TrialsInfo();
                if (trialInfo.IsPointToNextLevel(trial, new Point() { X = player.GetPosition().X, Y = player.GetPosition().Y }))
                {
                    trial = trialInfo.GetNextTrial(trial);
                    map = new Maps().GetMap(trial);
                    creatures = trialInfo.GetMonstersTrial(trial);
                    glowingBalls = new List<GlowingBalls>();
                    player.ChangePoisition(new Point() { X = trialInfo.GetStartPoint(trial).X, Y = trialInfo.GetStartPoint(trial).Y });
                    screen = new ScreenPoint(new TrialsInfo().GetChangingScreen(trial));
                    tickCount = 0;
                }
                else if (Trials.ArenaTrial == trial&&trialInfo.IsPointToNextLevel(trial,player.GetPosition()))
                {
                    trial = trialInfo.GetNextTrial(trial);
                }
                else if (player.GetHealth() <= 0)
                {
                    trial = Trials.CaveTrial;
                    map = new Maps().GetMap(trial);
                    creatures = trialInfo.GetMonstersTrial(trial);
                    glowingBalls = new List<GlowingBalls>();
                    player = new Player(new TrialsInfo().GetStartPoint(trial));
                    screen = new ScreenPoint(new TrialsInfo().GetChangingScreen(trial));
                    tickCount = 0;
                }
            }
            if (trial == Trials.FlyingUFO)
                new ChangeStateFlyingUfoLevel().ChangeState(screen, tickCount, blocks);
            if (trial == Trials.FlyingUFO)
                foreach (var i in blocks)
                {
                    if ((i.pointUp.X - screen.GetPointX()) <= (player.GetPosition().X) &&
                        (i.pointUp.X + i.width - screen.GetPointX()) >= (player.GetPosition().X))
                    {
                        if (player.GetPosition().Y >= 0 && player.GetPosition().Y <= i.lenghtUpPart ||
                           player.GetPosition().Y >= i.pointDown.Y && player.GetPosition().Y <= i.pointDown.Y + i.lengthDownPart)
                            player.ChangeHealth(100);

                    }

                    if ((i.pointUp.X - screen.GetPointX()) <= (player.GetPosition().X) &&
                        (i.pointUp.X + i.width - screen.GetPointX()) >= (player.GetPosition().X))
                    {
                        if (player.GetPosition().Y + player.GetSize() >= 0 && player.GetPosition().Y + player.GetSize() <= i.lenghtUpPart ||
                           player.GetPosition().Y + player.GetSize() >= i.pointDown.Y && player.GetPosition().Y + player.GetSize() <= i.pointDown.Y + i.lengthDownPart)
                            player.ChangeHealth(100);

                    }

                    if ((i.pointUp.X - screen.GetPointX()) <= (player.GetPosition().X + player.GetSize()) &&
                        (i.pointUp.X + i.width - screen.GetPointX()) >= (player.GetPosition().X + player.GetSize()))
                    {
                        if (player.GetPosition().Y >= 0 && player.GetPosition().Y <= i.lenghtUpPart ||
                           player.GetPosition().Y >= i.pointDown.Y && player.GetPosition().Y <= i.pointDown.Y + i.lengthDownPart)
                            player.ChangeHealth(100);

                    }

                    if ((i.pointUp.X - screen.GetPointX()) <= (player.GetPosition().X + player.GetSize()) &&
                        (i.pointUp.X + i.width - screen.GetPointX()) >= (player.GetPosition().X + player.GetSize()))
                    {
                        if (player.GetPosition().Y + player.GetSize() >= 0 && player.GetPosition().Y + player.GetSize() <= i.lenghtUpPart ||
                           player.GetPosition().Y + player.GetSize() >= i.pointDown.Y && player.GetPosition().Y + player.GetSize() <= i.pointDown.Y + i.lengthDownPart)
                            player.ChangeHealth(100);

                    }
                }
            if (trial == Trials.FlyingUFO && player.GetHealth()<=0)
            {
                var trialInfo = new TrialsInfo();
                trial = Trials.CaveTrial;
                map = new Maps().GetMap(trial);
                creatures = trialInfo.GetMonstersTrial(trial);
                glowingBalls = new List<GlowingBalls>();
                player = new Player(new TrialsInfo().GetStartPoint(trial));
                screen = new ScreenPoint(new TrialsInfo().GetChangingScreen(trial));
                tickCount = 0;
            }
        }

        private void GetChangePosition(Player player, ScreenPoint changingScreen,
                                        int[,] map, int[] decorationObject, Trials trial) 
        {
            if ((Trials.ArenaTrial == trial || Trials.CaveTrial == trial || Trials.MainTrial == trial)&&player.IsintoSpaceSheep()==false)
                GetPointToFall(player, changingScreen, map, decorationObject, trial);
        }

        private void GetPointToFall(Player player, ScreenPoint changingScreen, int[,] map, int[] decorationObject, Trials trial)
        {
            if (!(trial == Trials.CaveTrial||trial==Trials.FlyingUFO))
            {
                if (IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = 6 }, 40))
                {
                    changingScreen.ChangePointScreenY(6);
                    player.ChangePositionY(6);
                }
                else
                {
                    var y = 5;
                    while (y > 0)
                    {
                        if (IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = y }, 40))
                        {
                            changingScreen.ChangePointScreenY(y);
                            player.ChangePositionY(y);
                            break;
                        }
                        y -= 1;
                    }
                }
                if (player.GetCountToFall() > 0)
                {
                    player.ChangeCountToFall(-12);//
                    changingScreen.ChangePointScreenY(-6);//
                    if (IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = -12 }, 40))
                    {
                        changingScreen.ChangePointScreenY(-6);
                        player.ChangePositionY(-12);
                    }
                    else
                    {
                        changingScreen.ChangePointScreenY(-player.GetCountToFall()/2);
                        player.ChangeCountToFall(-player.GetCountToFall());
                    }
                }
            }
        }

        private bool IsWay(Point pointPlayer, ScreenPoint ChangingScreen, int[,] map, int[] decorationObject, Point delta, int size)
        {
            var point1 = IsDecorationObject(new Point() { X = pointPlayer.X + delta.X, Y = pointPlayer.Y + delta.Y }, ChangingScreen, map, decorationObject, delta);
            var point2 = IsDecorationObject(new Point() { X = pointPlayer.X + delta.X, Y = pointPlayer.Y + delta.Y + size }, ChangingScreen, map, decorationObject, delta);
            var point3 = IsDecorationObject(new Point() { X = pointPlayer.X + delta.X + size, Y = pointPlayer.Y + delta.Y }, ChangingScreen, map, decorationObject, delta);
            var point4 = IsDecorationObject(new Point() { X = pointPlayer.X + delta.X + size, Y = pointPlayer.Y + delta.Y + size }, ChangingScreen, map, decorationObject, delta);
            return point1 && point2 && point3 && point4;
        }

        private bool IsDecorationObject(Point playerPoint, ScreenPoint ChangingScreen, int[,] map, int[] decorationObject, Point delta)
        {
            return decorationObject.Contains(map[(int)(playerPoint.X) / 60, (int)(playerPoint.Y) / 60]);
        }

        private bool IsPointIntoArea(Point areaPoint, Point objectPoint, int sizeX, int sizeY)
        {
            return areaPoint.X <= objectPoint.X && areaPoint.X + sizeX >= objectPoint.X
               && areaPoint.Y <= objectPoint.Y && areaPoint.Y + sizeY >= objectPoint.Y;
        }
    }
}
