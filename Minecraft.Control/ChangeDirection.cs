using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class ChangeDirection
    {
        public void ChangeDirectionPlayer(Player player,ScreenPoint changingScreen,int[,] map,int[] decorationObject,Direction direction,Trials trial)
        {
            if (direction == Direction.Right &&
                new Way().IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 4, Y = 0 }))
                player.ChangePositionX(4);
            else if (direction == Direction.Left
                && new Way().IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = -4, Y = 0 }))
                player.ChangePositionX(-4);
            else if (trial != Trials.CaveTrial && direction == Direction.Up
                && player.GetCountToFall() <= 0 && !new Way().IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = 4 }))
                player.ChangeCountToFall(130);
            else if (direction == Direction.Up 
                && new Way().IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = -4 }))
                player.ChangePositionY(-4);
            else if (direction == Direction.Down 
                && new Way().IsWay(player.GetPosition(), changingScreen, map, decorationObject, new Point() { X = 0, Y = 4 }))
                player.ChangePositionY(4);
        }
    }
}
