using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraft.Models;
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
		List<FlyingObject> flyingObjects = new List<FlyingObject>(); 
		[TestMethod]
		public void TestSolve()
		{
			var flyingObject = new FlyingObject(new Point() {X=0,Y=0}, new Point() { X = 0, Y = 0 }, "player",40);
			Assert.AreEqual(flyingObject.GetPosition(),new Point() {X=0,Y=0});
		}

		[TestMethod]
		public void TestSolve2()
		{
			var flyingObject = new FlyingObject(new Point() { X =100, Y = 200}, new Point() { X = 200, Y = 250 }, "player", 40);
			var player = new Player(new Point() { X = 0, Y = 0});
			var position = new Point() { X = 0, Y = 0 };
			while(!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
			{
				position = flyingObject.GetPosition();
			}
			Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 200, Y = 100 }, position, 50));
		}

		[TestMethod] 
		public void TestSolve3()
		{
			var flyingObject = new FlyingObject(new Point() { X = 400, Y = 200 }, new Point() { X = 200, Y = 250 }, "player", 40);
			var player = new Player(new Point() { X = 0, Y = 0 });
			var position = new Point() { X = 0, Y = 0 };
			while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
			{
				position = flyingObject.GetPosition();
			}
			Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 200, Y = 100 }, position, 50));
		}

        [TestMethod]
        public void TestSolve4()
        {
            var flyingObject = new FlyingObject(new Point() { X = 400, Y = 200 }, new Point() { X = 200, Y = 200 }, "player", 40);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 200, Y = 100 }, position, 50));
        }

        [TestMethod]
        public void TestSolve5()
        {
            var flyingObject = new FlyingObject(new Point() { X = 400, Y = 140 }, new Point() { X = 400, Y = 200 }, "player", 40);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 400, Y = 200 }, position, 50));
        }

        [TestMethod]
        public void TestSolve6()
        {
            var flyingObject = new FlyingObject(new Point() { X = 321, Y = 441 }, new Point() { X = 464, Y = 433 }, "player", 40);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 464, Y = 433 }, position, 50));
        }

        [TestMethod]
        public void TestSolve7()
        {
			var flyingObject = new FlyingObject(new Point() { X = 321, Y = 441 }, new Point() { X = 464, Y = 433 }, "player", 40);
			var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 464, Y = 433 }, position, 50));
        }

        [TestMethod]
        public void TestSolve8()
        {
            var flyingObject = new FlyingObject(new Point() { X = 300, Y = 300 }, new Point() { X = 400, Y = 200 }, "player", 40);
            var player = new Player(new Point() { X = 0, Y = 0 });
            var position = new Point() { X = 0, Y = 0 };
            while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
            {
                position = flyingObject.GetPosition();
            }
            Assert.AreEqual(true, IsPointIntoArea(new Point() { X = 400, Y = 200 }, position, 50));
        }

        [TestMethod]
        public void TestRandom()
        {
            var rnd = new Random();
            for (var i = 0; i < 10; i++)
            {
                var finishPoint = new Point() { X = rnd.Next(200, 500), Y = rnd.Next(200, 500) };
                var startPoint = new Point() { X = rnd.Next(200, 500), Y = rnd.Next(200, 500) };
                var flyingObject = new FlyingObject(startPoint, finishPoint, "player", 40);
                var player = new Player(new Point() { X = 0, Y = 0 });
                var position = new Point() { X = 0, Y = 0 };
                while (!flyingObject.IsDied(creatures, flyingObjects, player, map, decorationObject))
                {
                    position = flyingObject.GetPosition();
                }
                Assert.AreEqual(true, IsPointIntoArea(finishPoint, position, 50));
            }
        }

        private bool IsPointIntoArea(Point areaPoint, Point objectPoint, int sizeArea)
		{
			return Math.Abs(areaPoint.X - objectPoint.X)<=sizeArea
			   && Math.Abs(objectPoint.Y-objectPoint.Y)<=sizeArea;
		}
	}
}
