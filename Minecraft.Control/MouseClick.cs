using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Models;
using System.Drawing;

namespace Minecraft.Control
{
    public class MouseClick
    {
        public void DoAction(Trials trial,List<ICreature> creatures,Point mouseLocation,ScreenPoint screen,Player player,List<FlyingObject> glowingBalls,int tickCount)
        {
            if (trial == Trials.CaveTrial)
                IsCreature(creatures, new Point() { X = mouseLocation.X + screen.GetPointX(),
                    Y = mouseLocation.Y + screen.GetPointY() }, player.GetPosition());
            else
                player.MakeNewICreature(new Point() { X = mouseLocation.X + screen.GetPointX(), Y = mouseLocation.Y + screen.GetPointY() },
                    "player", tickCount, glowingBalls);
        }

        private void IsCreature(List<ICreature> monsters, Point playerPointClic, Point playerPoint)
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

        private bool IsIntoBall(Point pointPlayer, Point objectPoint, int radious)
        {
            return Math.Sqrt((pointPlayer.X - objectPoint.X) * (pointPlayer.X - objectPoint.X)
                + (pointPlayer.Y - objectPoint.Y) * (pointPlayer.Y - objectPoint.Y)) <= radious;
        }
    }
}
