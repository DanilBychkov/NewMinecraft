using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class CreaturesChangeState
    {
        public bool ChangeState(ref List<ICreature> creatures,int number,ref Player player)
        {
            creatures[number].ChangeSleep(player.GetPosition());
            if (creatures[number].IsSleep())
                return true;
            if (creatures[number].IsDiedInConflit())
            {
                creatures.RemoveAt(number);
                return true;
            }
            return false;
        }
    }
}
