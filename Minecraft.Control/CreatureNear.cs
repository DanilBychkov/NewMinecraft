using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class CreatureNear
    {
        public void IsCreature(List<ICreature> monsters, Point playerPointClic, Point playerPoint)
        {
            foreach (var monster in monsters)
            {
                var IcreatureMonster = (ICreature)monster;
                if (IcreatureMonster.IsSleep() == false
                    && new Circle().IsIntoBall(playerPoint, IcreatureMonster.GetPosition(), 60)
                    && playerPointClic.X >= IcreatureMonster.GetPosition().X - 40
                    && playerPointClic.X <= IcreatureMonster.GetPosition().X + 40
                    && playerPointClic.Y >= IcreatureMonster.GetPosition().Y - 40
                    && playerPointClic.Y <= IcreatureMonster.GetPosition().Y + 60)//почему есть 20 и 60 
                {
                    IcreatureMonster.ChangeHealth(-5);
                    break;
                }
            }
        }
    }
}
