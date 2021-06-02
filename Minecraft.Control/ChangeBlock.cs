using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Models;

namespace Minecraft.Control
{
    public class ChangeBlock
    {
        public void AddAndRemoveBlock(List<Block> blocks, ScreenPoint screen, int tickCount)
        {
            var block = blocks[0];
            if (block.pointUp.X+block.width+10 - screen.GetPointX() < 0)
            {
                blocks.RemoveAt(0);
                blocks.Add(new Block(940, 670, screen, tickCount));
            }
        }
    }
}
