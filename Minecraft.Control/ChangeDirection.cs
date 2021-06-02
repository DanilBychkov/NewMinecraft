using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Minecraft.Models;
using System.Windows;

namespace Minecraft.Control
{
    public static class ChangeDirection
    {
        public static void ChangeDirectionPlayer(this Player player, ScreenPoint changingScreen, int[,] map, int[] decorationObject, Direction direction, Trials trial)
        {
            var positionPlayer= player.GetPosition();
            if (direction == Direction.Right && Trials.FlyingUFO != trial &&
                    new Point() { X = positionPlayer.X + 8, Y = positionPlayer.Y }.IsWay(map, decorationObject, 40))
            {
                player.ChangePositionX(8);
                if (map.GetLength(0) * 60 >= (changingScreen.GetPointX() + 940))
                    changingScreen.ChangePointScreenX(8);
            }
            else if (direction == Direction.Left && Trials.FlyingUFO != trial &&
                new Point() { X = positionPlayer.X -8, Y = positionPlayer.Y }.IsWay( map, decorationObject,40))
            {
                player.ChangePositionX(-8);
                if (changingScreen.GetPointX() > 0)
                    changingScreen.ChangePointScreenX(-8);
            }
            else if (direction == Direction.Up && Trials.FlyingUFO != trial)
            {
                if ((Trials.CaveTrial == trial || player.IsintoSpaceSheep() == true)
                   && new Point() { X = positionPlayer.X , Y = positionPlayer.Y-8 }.IsWay(map, decorationObject, 40))
                {
                    player.ChangePositionY(-8);
                    if (changingScreen.GetPointY() > 8)
                        changingScreen.ChangePointScreenY(-8);
                }
                else if (player.GetCountToFall() <= 0)
                    player.ChangeCountToFall(150);
            }
            else if (direction == Direction.Down && Trials.FlyingUFO != trial &&
               new Point() { X = positionPlayer.X , Y = positionPlayer.Y+8 }.IsWay(map, decorationObject, 40))
            {
                player.ChangePositionY(8);
                if (map.GetLength(1) * 60 >= changingScreen.GetPointY() + 670)
                    changingScreen.ChangePointScreenY(8);
            }
            else if (Trials.FlyingUFO == trial)
            {
                if (direction == Direction.Up && player.GetPosition().Y +5>= 0)
                    player.ChangePositionY(-8);
                else if (direction == Direction.Down && player.GetPosition().Y + 8<= 670-player.GetSize()*2)
                    player.ChangePositionY(8);
            }
        }
    }
}
