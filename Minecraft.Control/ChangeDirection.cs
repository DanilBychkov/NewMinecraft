using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class ChangeDirection
    {
        public void ChangeDirectionPlayer(Direction direction,Player player,ScreenPoint screen,int[,] map,int[] decorationObject,EnumMaps level)
        {
            if (direction == Direction.Right)
            {
                player.ChangeDirection(Direction.Right);
                if (new Way().IsWay(player.GetPosition(), screen, map, decorationObject, new Point() { X = 4, Y = 0 }))
                    player.ChangePositionX(4);
            }
            else if (direction == Direction.Left)
            {
                player.ChangeDirection(Direction.Left);
                if (new Way().IsWay(player.GetPosition(), screen, map, decorationObject, new Point() { X = -4, Y = 0 }))
                    player.ChangePositionX(-4);
            }
            if (level !=EnumMaps.CaveMap&& direction == Direction.Up &&
                player.GetCountToFall() <= 0 && !new Way().IsWay(player.GetPosition(), screen, map, decorationObject, new Point() { X = 0, Y = 4 }))
                    player.ChangeCountToFall(130);
            else if (direction == Direction.Up && new Way().IsWay(player.GetPosition(), screen, map, decorationObject, new Point() { X = 0, Y = -4 }))
                    player.ChangePositionY(-4);
            else if (direction == Direction.Down && new Way().IsWay(player.GetPosition(), screen, map, decorationObject, new Point() { X = 0, Y = 4 }))//заблокировать для всех уровней кроме 0
                    player.ChangePositionY(4);
        }
    }
}
