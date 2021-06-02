using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class CreaturesChangeState
    {
        public bool ChangeState(List<ICreature> creatures,int number,Player player)
        {
            creatures[number].ChangeSleep(player.GetPosition());
            if (creatures[number].IsSleep())
                return true;
            else if (creatures[number].IsDiedInConflit())
            {
                creatures.RemoveAt(number);
                return true;
            }
            return false;
        }
    }
}
