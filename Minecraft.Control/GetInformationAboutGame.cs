using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;
namespace Minecraft.Control
{
    public class GetInformationAboutGame
    {
        private EnumMaps numberLevel = EnumMaps.CaveMap;

        public void ChangeInformationGame(Player player)//изменение мира зависит от перемещения игрока
        {

        }

        private bool IsExitInLevel(Point playerPoint,EnumMaps maps)
        {
            if(playerPoint.X>0&&playerPoint.X<=100&&playerPoint.Y>0&&playerPoint.Y<=100)
                return true;
            return false;
        }
    }
}
