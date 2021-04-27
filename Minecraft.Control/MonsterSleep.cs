using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class MonsterSleep
    {
        public void ChangeCondition(ICreature monster,Point playerPoint)
        {
             if (new Circle().IsIntoBall(monster.GetPosition(), playerPoint, 200))
                    monster.ChangeSleep(false);
        }
    }
}
