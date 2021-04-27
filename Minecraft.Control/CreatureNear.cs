using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class CreatureNear
    {
        public void IsCreature(object[] monsters, Point playerPointClic, Point playerPoint)
        {
            foreach (var monster in monsters)
            {
                var IcreatureMonster = (ICreature)monster;
                if (IcreatureMonster.IsDeadInConflict() == false && IcreatureMonster.IsSleep() == false 
                    && new Circle().IsIntoBall(playerPoint, IcreatureMonster.GetPosition(), 60)
                    &&playerPointClic.X >= IcreatureMonster.GetPosition().X - 20 
                    && playerPointClic.X <= IcreatureMonster.GetPosition().X + 60 
                    &&playerPointClic.Y >= IcreatureMonster.GetPosition().Y - 20
                    && playerPointClic.Y <= IcreatureMonster.GetPosition().Y + 60)
                {
                            IcreatureMonster.ChangeHealth(-5);
                            break;
                }
            }
        }
    }
}
