using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class ChangePlayerMonstersPerTick
    {
        public void ChangeStatesPerTick(ref Player player,ref ScreenPoint screen,Point pointToClient
            ,int clientSizeWidth,int clientSizeHeight,ref int[,] map,int[] decorationObject,
            ref Trials trial,ref List<ICreature> creatures,ref List<FlyingObject> glowingBalls)
        {
            GetChangePosition(player, screen, pointToClient,
               clientSizeWidth, clientSizeHeight, map, decorationObject, trial, creatures);
            var trialInfo = new TrialsInfo();
            if (trialInfo.IsPointToNextLevel(trial, new Point() { X = player.GetPosition().X, Y = player.GetPosition().Y }))
            {
                trial = trialInfo.GetNextTrial(trial);
                map = new Maps().GetMap(trial);
                creatures = trialInfo.GetMonstersTrial(trial);
                glowingBalls = new List<FlyingObject>();
                player.ChangePoisition(new Point() { X = trialInfo.GetStartPoint(trial).X, Y = trialInfo.GetStartPoint(trial).Y });
                screen = new ScreenPoint();
            }
            else if (player.GetHealth() <= 0 || creatures.Count == 0 && trial == Trials.ArenaTrial)
            {
                trial = Trials.CaveTrial;
                map = new Maps().GetMap(trial);
                creatures = trialInfo.GetMonstersTrial(trial);
                glowingBalls = new List<FlyingObject>();
                player = new Player(new TrialsInfo().GetStartPoint(trial));
                screen = new ScreenPoint();
            }
        }

        private void GetChangePosition(Player player, ScreenPoint changingScreen, Point pointClick, int widthScreen, int heightScreen,
                                        int[,] map, int[] decorationObject, Trials trial, List<ICreature> monsters)//Все изменения проходят здесь 
        {
            var width = widthScreen / 10;
            var height = heightScreen / 10;
            var deltaPoint = new Point();
            if (pointClick.X > widthScreen - width)
                deltaPoint.X -= 5;
            if (pointClick.X < width)
                deltaPoint.X += 5;
            if (pointClick.Y > heightScreen - height)
                deltaPoint.Y -= 5;
            if (pointClick.Y < height)
                deltaPoint.Y += 5;
            changingScreen.ChangePointScreenX(-deltaPoint.X);
            changingScreen.ChangePointScreenY(-deltaPoint.Y);
            GetPointToFall(player, changingScreen, map, decorationObject, trial);
        }

        private void GetPointToFall(Player player, ScreenPoint changingScreen, int[,] map, int[] decorationObject, Trials trial)
        {
            if (trial != Trials.CaveTrial)
            {
                if (new Way().IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = 6 }, 40))
                    player.ChangePositionY(6);
                else
                {
                    var y = 5;
                    while (y > 0)
                    {
                        if (new Way().IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = y }, 40))
                        {
                            player.ChangePositionY(y);
                            break;
                        }
                        y -= 1;
                    }
                }
                if (player.GetCountToFall() > 0)
                {
                    player.ChangeCountToFall(-12);
                    if (new Way().IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = -12 }, 40))
                        player.ChangePositionY(-12);
                    else
                        player.ChangeCountToFall(-player.GetCountToFall());
                }
            }
        }
    }
}
