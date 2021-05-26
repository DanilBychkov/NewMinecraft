using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Minecraft.Models;
using System.Drawing;

namespace UnitTestProject1
{
    [TestClass]
    public class TestZombi
    {
        private readonly int[,] map = { { 1, 1, 1, 1, 1, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1 } };
        private readonly int[] decorationObject = { 0, 4, 5 };

        [TestMethod]
        public void TestSolve()
        {
            Assert.AreEqual(true, IsTrueWay(new Point() {X=240,Y=240},new Point() {X=120,Y=120}));
        }

        [TestMethod]
        public void TestSolve2()
        {
            Assert.AreEqual(true, IsTrueWay(new Point() { X = 120, Y = 120 }, new Point() { X = 240, Y = 240 }));
        }

        [TestMethod]
        public void TestSolve3()
        {
            Assert.AreEqual(true, IsTrueWay(new Point() { X = 240, Y = 240 }, new Point() { X = 240, Y = 240 }));
        }

        [TestMethod]
        public void TestSolverRandom()
        {
            var rnd = new Random();
            var isGoodWay = true;
            for (var i = 0; i < 10; i++)
            {
               isGoodWay=isGoodWay&& IsTrueWay(new Point() { X = rnd.Next(100,300), Y =rnd.Next(100,300) },
                   new Point() { X = rnd.Next(100,300), Y = rnd.Next(100,300) });
            }
            Assert.AreEqual(true, isGoodWay);
        }

        private bool IsTrueWay(Point playerPoint, Point creaturesPoint)
        {
            var zombi = new Zombi(creaturesPoint);
            var player = new Player(playerPoint);
            var isIntoPoint = false;
            var count = MaxCountToObject(creaturesPoint,playerPoint);
            for (var i = 0; i < count; i++)
            {
                var point = zombi.GetChangePosition(map, player, decorationObject, new ScreenPoint());
                if (IsPointIntoArea(playerPoint, point, 40))
                {
                    isIntoPoint = true;
                    break;
                }
            }
            return isIntoPoint;
        }

        private int MaxCountToObject(Point start,Point finish)
        {
            return Math.Abs(start.X - finish.X) / 5 + Math.Abs(start.Y - finish.Y) / 5 + 10;
        }

        private bool IsPointIntoArea(Point areaPoint, Point objectPoint, int sizeArea)
        {
            return Math.Abs(areaPoint.X - objectPoint.X) <= sizeArea
               && Math.Abs(objectPoint.Y - objectPoint.Y) <= sizeArea;
        }
    }
}
