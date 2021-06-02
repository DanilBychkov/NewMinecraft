using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Control;
using Minecraft.Models;
using System.Drawing;

namespace UnitTestProject1
{
    [TestClass]
    public class ChangeDirectionTest
    {
        private readonly int[,] mapWithoutBlockInto = { { 1, 1, 1, 1, 1, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1 } };

        private readonly int[,] mapWithBlockInto = { { 1, 1, 1, 1, 1, 1 }, { 1, 0, 0, 1, 0, 1 }, { 1, 0, 0, 1, 0, 1 },
            { 1, 0, 0, 1, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1 } };

        private readonly int[] decorationObject = { 0, 4, 5, 8 };
        [TestMethod]
        public void DirectionUpCaveTrial()
        {
            var player = new Player(new Point() { X = 120,Y=80});
            var screen = new ScreenPoint(new Point() { X = 0, Y = 0 });
            player.ChangeDirectionPlayer(screen, mapWithBlockInto, decorationObject, Direction.Up, Trials.CaveTrial);
            Assert.IsTrue(new Point(){X=120,Y=72}==player.GetPosition());
        }

        [TestMethod]
        public void DirectionDownCaveTrial()
        {
            var player = new Player(new Point() { X = 120, Y = 80 });
            var screen = new ScreenPoint(new Point() { X = 0, Y = 0 });
            player.ChangeDirectionPlayer(screen, mapWithBlockInto, decorationObject, Direction.Down, Trials.CaveTrial);
            Assert.IsTrue(new Point() { X = 120, Y = 88 } == player.GetPosition());
        }

        [TestMethod]
        public void DirectionLeftCaveTrial()
        {
            var player = new Player(new Point() { X = 120, Y = 80 });
            var screen = new ScreenPoint(new Point() { X = 0, Y = 0 });
            player.ChangeDirectionPlayer(screen, mapWithBlockInto, decorationObject, Direction.Left, Trials.CaveTrial);
            Assert.IsTrue(new Point() { X = 112, Y = 80 } == player.GetPosition());
        }

        [TestMethod]
        public void DirectionRightCaveTrial()
        {
            var player = new Player(new Point() { X = 120, Y = 80 });
            var screen = new ScreenPoint(new Point() { X = 0, Y = 0 });
            player.ChangeDirectionPlayer(screen, mapWithBlockInto, decorationObject, Direction.Right, Trials.CaveTrial);
            Assert.IsTrue(new Point() { X = 128, Y = 80 } == player.GetPosition());
        }

        [TestMethod]
        public void IsNotWayToDirectionUpMainTrial()
        {
            var player = new Player(new Point() { X = 120, Y = 70});
            var screen = new ScreenPoint(new Point() { X = 0, Y = 0 });
            player.ChangeDirectionPlayer(screen, mapWithoutBlockInto, decorationObject, Direction.Up, Trials.ArenaTrial);
            Assert.IsTrue(player.GetPosition().Y==70);
        }

        [TestMethod]
        public void IsWayToDirectionDownMainTrial()
        {
            var player = new Player(new Point() { X = 120, Y = 70 });
            var screen = new ScreenPoint(new Point() { X = 0, Y = 0 });
            player.ChangeDirectionPlayer(screen, mapWithoutBlockInto, decorationObject, Direction.Down, Trials.ArenaTrial);
            Assert.IsTrue(player.GetPosition().Y == 78);
        }
    }
}
