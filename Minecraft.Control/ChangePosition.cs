using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class ChangePosition
    {
        public void GetChangePosition(Player player, ScreenPoint screen, Point pointClick, int WidthScreen, int HeightScreen,
                                         int[,] map,int[] decorationObject, EnumMaps level,Zombi zombi)
        {
            var width = WidthScreen / 10;
            var height = HeightScreen / 10;
            if (pointClick.X > WidthScreen - width)
            {
                player.ChangePositionX (-5);
                zombi.ChangePositionX(-5);
                screen.ChangePointScreenX(5);
            }
            if (pointClick.X < width)
            {
                player.ChangePositionX(5);
                zombi.ChangePositionX(5);
                screen.ChangePointScreenX(-5);
            }
            if (pointClick.Y > HeightScreen - height)
            {
                player.ChangePositionY( -5);
                zombi.ChangePositionY(-5);
                screen.ChangePointScreenY(5);
            }
            if (pointClick.Y < height)
            {
                player.ChangePositionY(5);
                zombi.ChangePositionY(5);
                screen.ChangePointScreenY(-5);
            }
            new PointToPlayer().GetNextPointToPlayer(map,player.GetPosition(),zombi,decorationObject,screen);
            GetPointToFall(player, screen, map, decorationObject,level);
        }

        private void GetPointToFall(Player player, ScreenPoint changingScreen, int[,] map,int[] decorationObject, EnumMaps level)
        {
            if (level != EnumMaps.CaveMap)
            {
                if (new Way().IsWay(player.GetPosition(), changingScreen, map,decorationObject, new Point() { X = 0, Y = 1}))
                    player.ChangePositionY(6);
                if (player.GetCountToFall() > 0)
                {
                    player.ChangeCountToFall(-12);
                    if (new Way().IsWay(player.GetPosition(), changingScreen, map,decorationObject, new Point() { X = 0, Y = -12 }))
                        player.ChangePositionY(-12);
                    else
                        player.ChangeCountToFall(-player.GetCountToFall());
                }
            }
        }
    }
}
