using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minecraft.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Minecraft.Control
{
    public static class AddGlowingBalls
    {
        public static void  AddBalls(this List<ICreature> creatures,Player player,int tickCount,List<GlowingBalls> flyingObjects,int[,] map,int[] decorationObject)
        {
            Action[] actions = new Action[creatures.Count];
            Action[] method = creatures.Select(i => new Action(() => i.MakeNewICreature(player.GetPosition(), "monster", tickCount, flyingObjects, map, decorationObject))).ToArray();//
            Parallel.Invoke(method);
        }
    }
}
