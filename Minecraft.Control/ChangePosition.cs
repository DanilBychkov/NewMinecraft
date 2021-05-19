﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class ChangePosition
    {
        public void GetChangePosition(Player player, ScreenPoint changingScreen, Point pointClick, int widthScreen, int heightScreen,
                                         int[,] map,int[] decorationObject,Trials trial,List<ICreature> monsters)//Все изменения проходят здесь 
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
            //player.ChangePositionX(deltaPoint.X);//Изменить ChangePosition в один
            //player.ChangePositionY(deltaPoint.Y);//
            changingScreen.ChangePointScreenX(-deltaPoint.X);//
            changingScreen.ChangePointScreenY(-deltaPoint.Y);//
            //ChangePointCreature(deltaPoint, monsters);
            //ChangeStateICreatureMonster(monsters, map, decorationObject, player, changingScreen);
            GetPointToFall(player, changingScreen, map,decorationObject,trial);
        }

        //private void ChangeStateICreatureMonster(List<ICreature> monsters,int[,] map, int[] decorationObject,Player player,ScreenPoint changingScreen)
        //{
        //    foreach (var monster in monsters)
        //    {
        //        var iCreatureMonster = (ICreature)monster;
        //        if (!iCreatureMonster.IsSleep())
        //            new PointToPlayer().GetNextPointToPlayer(map, player.GetPosition(), iCreatureMonster, decorationObject, changingScreen,player);
        //        new MonsterSleep().ChangeCondition(iCreatureMonster, player.GetPosition());
        //    }
        //}

        //private void ChangePointCreature(Point deltaPoint, object[] monsters)
        //{
        //    foreach (var monster in monsters)
        //    {
        //        var iCreatureMonster = (ICreature)monster; //Изменить ChangePosition в один
        //        iCreatureMonster.ChangePositionX(deltaPoint.X);//
        //        iCreatureMonster.ChangePositionY(deltaPoint.Y);//
        //    }
        //}

        private void GetPointToFall(Player player, ScreenPoint changingScreen, int[,] map,int[] decorationObject, Trials trial)
        {
            if (trial != Trials.CaveTrial)//прыгает вниз
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
                if (player.GetCountToFall() > 0)//прыгает вверх
                {
                    player.ChangeCountToFall(-12);
                    if (new Way().IsWay(player.GetPosition(), changingScreen, map,decorationObject, new Point() { X = 0, Y = -12 },40))
                        player.ChangePositionY(-12);
                    else
                        player.ChangeCountToFall(-player.GetCountToFall());
                }
            }
        }
    }
}