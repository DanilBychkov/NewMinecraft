using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraft.Models;
using Minecraft.Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace UnitTestProject1
{
    [TestClass]
    public class TestRemoveAndAddBlock
    {
        [TestMethod]
        public void NeedToRemoveAndAdd()
        {
            var blocks = new List<Block>();
            var screen = new ScreenPoint(new Point() {X=0,Y=0});
            var tick = 1000;
            blocks.Add(new Block(670, 940, screen, 1000));
            for (var i = 0; i < 1000; i++)
            {
                new ChangeStateFlyingUfoLevel().ChangeState(screen, tick, blocks);
                new ChangeBlock().AddAndRemoveBlock(blocks, screen, tick);
            }
            Assert.IsTrue(blocks.Count==1);
        }

        [TestMethod]
        public void NeedToRemoveAndAddTwoBlocks()
        {
            var blocks = new List<Block>();
            var screen = new ScreenPoint(new Point() { X = 0, Y = 0 });
            var tick = 1000;
            blocks.Add(new Block(670, 940, screen, 1000));
            blocks.Add(new Block(370, 940, screen, 1000));
            for (var i = 0; i < 1000; i++)
            {
                new ChangeStateFlyingUfoLevel().ChangeState(screen, tick, blocks);
                new ChangeBlock().AddAndRemoveBlock(blocks, screen, tick);
            }
            Assert.IsTrue(blocks.Count == 2);
        }
    }
}
