using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraft.Models;
using Minecraft.Control;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace UnitTestProject1
{
    [TestClass]
    public class QuadraticEquatonSolverTest
    {
        private readonly int[,] map ={ { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2 },{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{ 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
            { 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{ 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{ 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},{ 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,2},
            { 2,0,0,0,0,0,0,6,6,6,6,0,0,0,0,0,0,0,1,1,2},{ 2,0,0,0,0,0,6,6,0,0,6,6,0,0,0,0,0,0,1,1,2},{ 2,0,0,0,0,0,6,0,0,0,0,6,0,0,0,0,0,0,1,1,2}};
        private readonly int[] decorationObject = { 0, 4, 5 };
        List<ICreature> creatures = new List<ICreature>();
        List<GlowingBalls> flyingObjects = new List<GlowingBalls>();
        [TestMethod]
        public void startPointIsFinishPoint()
        {
            var flyingObject = new GlowingBalls(new Point() { X = 0, Y = 0 }, new Point() { X = 0, Y = 0 }, "player", 40,map,decorationObject);
            Assert.AreEqual(flyingObject.GetPosition(), new Point() { X = 0, Y = 0 });
        }

        [TestMethod]
        public void FinsihPointInFourSquare()
        {
            var flyingObject = new GlowingBalls(new Point() { X = 100, Y = 200 }, new Point() { X = 200, Y = 250 }, "player", 40,map,decorationObject);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 200, Y = 250 }, position, 50));
        }

        [TestMethod]
        public void FinishPointInThreeSquare()
        {
            var flyingObject = new GlowingBalls(new Point() { X = 400, Y = 200 }, new Point() { X = 200, Y = 250 }, "player", 40,map,decorationObject);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 200, Y = 250}, position, 50));
        }

        [TestMethod]
        public void FinishPointInFourSquare()
        {
            var flyingObject = new GlowingBalls(new Point() { X = 400, Y = 140 }, new Point() { X = 400, Y = 200 }, "player", 40,map,decorationObject);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 400, Y = 200 }, position, 50));
        }

        [TestMethod]
        public void FinishPointIsOneSquare()
        {
            var flyingObject = new GlowingBalls(new Point() { X = 321, Y = 441 }, new Point() { X = 464, Y = 433 }, "player", 40,map,decorationObject);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 464, Y = 433 }, position, 50));
        }

        [TestMethod]
        public void TestSoleveRandom()
        {
            var flyingObject = new GlowingBalls(new Point() { X = 321, Y = 441 }, new Point() { X = 464, Y = 433 }, "player", 40,map,decorationObject);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 464, Y = 433 }, position, 50));
        }

        [TestMethod]
        public void IsAddGlowingBall()
        {
            var creatures = new List<ICreature>();
            var screen = new ScreenPoint(new Point() { X = 0, Y = 0 });
            var player = new Player(new Point() { X = 200, Y = 1020 });
            var glowingBalls = new List<GlowingBalls>();
            new Point() { X = 500, Y = 1020 }.DoAction(Trials.MainTrial, creatures, screen, player, glowingBalls, 1000, map, decorationObject);
            Assert.IsTrue(glowingBalls.Count == 1);
        }

        [TestMethod]
        public void TestSolveRandom2()
        {
            var flyingObject = new GlowingBalls(new Point() { X = 300, Y = 300 }, new Point() { X = 400, Y = 200 }, "player", 40,map,decorationObject);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 400, Y = 200 }, position, 50));
        }

        private bool IsPointIntoArea(Point areaPoint, Point objectPoint, int sizeArea)
        {
            return Math.Abs(areaPoint.X - objectPoint.X) <= sizeArea
               && Math.Abs(objectPoint.Y - objectPoint.Y) <= sizeArea;
        }
    }
}