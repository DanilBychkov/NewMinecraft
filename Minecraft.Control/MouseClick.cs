using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Models;
using System.Drawing;


namespace Minecraft.Control
{
    public static class MouseClick
    {
        public static  void DoAction(this Point location,Trials trial,List<ICreature> creatures,ScreenPoint screen,
            Player player,List<GlowingBalls> glowingBalls,int tickCount,int[,] map,int[] decorationObject)
        {
            if (Trials.ArenaTrial == trial || Trials.CaveTrial == trial || Trials.MainTrial == trial)
            {
                if (trial == Trials.CaveTrial)
                    IsCreature(creatures, new Point()
                    {
                        X = location.X + screen.GetPointX(),
                        Y = location.Y + screen.GetPointY()
                    }, player.GetPosition());
                else
                    player.MakeNewICreature(new Point() { X = location.X + screen.GetPointX(), Y = location.Y + screen.GetPointY() },
                        "player", tickCount, glowingBalls,map,decorationObject);
            }
        }

        private static void IsCreature(List<ICreature> monsters, Point playerPointClic, Point playerPoint)
        {
            foreach (var monster in monsters)
            {
                var IcreatureMonster = monster;
                if (IcreatureMonster.IsSleep() == false
                    && IsIntoBall(playerPoint, IcreatureMonster.GetPosition(), 60)
                    && playerPointClic.X >= IcreatureMonster.GetPosition().X - 40
                    && playerPointClic.X <= IcreatureMonster.GetPosition().X + 40
                    && playerPointClic.Y >= IcreatureMonster.GetPosition().Y - 40
                    && playerPointClic.Y <= IcreatureMonster.GetPosition().Y + 60)
                {
                    IcreatureMonster.ChangeHealth(-5);
                    break;
                }
            }
        }

        private static bool IsIntoBall(Point pointPlayer, Point objectPoint, int radious)
        {
            return Math.Sqrt((pointPlayer.X - objectPoint.X) * (pointPlayer.X - objectPoint.X)
                + (pointPlayer.Y - objectPoint.Y) * (pointPlayer.Y - objectPoint.Y)) <= radious;
        }
    }
}
