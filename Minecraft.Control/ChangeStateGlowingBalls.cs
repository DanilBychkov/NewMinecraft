using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public static class ChangeStateGlowingBalls
    {
        public static void ChangeState(this List<GlowingBalls> flyingObjects,List<ICreature> creatures,
            Player player,int[,] map,int[] decorationObject)
        {
            for (var i = 0; i < flyingObjects.Count; i++)
                if (flyingObjects[i].IsDied(creatures, flyingObjects, player, map, decorationObject))
                {
                    flyingObjects.RemoveAt(i);
                    continue;
                }
        }
    }
}
