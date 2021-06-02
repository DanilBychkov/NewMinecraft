using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class ChangeStateFlyingUfoLevel
    {
        public void ChangeState(ScreenPoint screenPoint,int tickCount,List<Block> blocks)
        {
            screenPoint.ChangePointScreenX(5 * tickCount / 1000);
            new ChangeBlock().AddAndRemoveBlock(blocks, screenPoint, tickCount);
        }
    }
}
