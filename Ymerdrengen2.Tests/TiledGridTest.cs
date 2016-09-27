// <copyright file="TiledGridTests.cs" company="Team4">
// Copyright(c) 2016 All Rights Reserved
// </copyright>
// <author>Alexander Kirk Jørgensen</author>
// <date>27-09-2016</date>
// <summary>Test class for the Grid structure, using Tile classes as generic class.</summary>
using System;
using System.Linq;
using Grid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ymerdrengen2.Tests
{
    [TestClass]
    public class TiledGridTest
    {
        [TestMethod]
        public void TiledGridVectorTest()
        {
            Grid<BaseTile> testGridCoords = new Grid<BaseTile>(5);
            testGridCoords[1, 1] = new BaseTile() { Value = FieldStatus.Floor };
            testGridCoords[2, 1] = new BaseTile() { Value = FieldStatus.Floor };
            testGridCoords[3, 1] = new BaseTile() { Value = FieldStatus.Floor };
            testGridCoords[2, 2] = new BaseTile() { Value = FieldStatus.Floor };

            // Check whether the coords have been set
            for (int i = 0; i < testGridCoords.GridSideLength; i++) {
                for (int j = 0; j < testGridCoords.GridSideLength; j++) {
                    if (i == 1 && j == 1) {
                        Assert.IsTrue(testGridCoords[i, j].GetValue()); continue;
                    }
                    else if (i == 2 && (j == 1 || j == 2)) {
                        Assert.IsTrue(testGridCoords[i, j].GetValue()); continue;
                    }
                    else if (i == 3 && j == 1) {
                        Assert.IsTrue(testGridCoords[i, j].GetValue()); continue;
                    }
                    else
                        Assert.IsFalse(testGridCoords[i, j].GetValue());
                }
            }
        }

        [TestMethod]
        public void TiledGridIndexerTest()
        {
            Grid<BaseTile> testGridIdx = new Grid<BaseTile>(5);
            testGridIdx[6] = new BaseTile() { Value = FieldStatus.Floor };
            testGridIdx[7] = new BaseTile() { Value = FieldStatus.Floor };
            testGridIdx[8] = new BaseTile() { Value = FieldStatus.Floor };
            testGridIdx[12] = new BaseTile() { Value = FieldStatus.Floor };

            for (int i = 0; i < testGridIdx.MaxGridCount; i++) {
                switch (i) {
                    case 6: // All following fields should be marked true.
                    case 7:
                    case 8:
                    case 12:
                        Assert.IsTrue(testGridIdx[i].GetValue()); break;
                    default:
                        Assert.IsFalse(testGridIdx[i].GetValue()); break;
                }
            }
        }
        [TestMethod]
        public void TiledGridNegativeCoordTest()
        {
            Grid<BaseTile> testGridIdx = new Grid<BaseTile>(5);
            testGridIdx[6] = new BaseTile() { Value = FieldStatus.Floor };
            testGridIdx[7] = new BaseTile() { Value = FieldStatus.Floor };
            testGridIdx[8] = new BaseTile() { Value = FieldStatus.Floor };
            testGridIdx[12] = new BaseTile() { Value = FieldStatus.Floor };

            var invalidIdxs = new int[] { -1, testGridIdx.MaxGridCount };

            int errorCount = 0;
            foreach (int idx in invalidIdxs) {
                try {
                    var test = testGridIdx[idx];
                    Assert.Fail("Object test didn't throw IndexOutOfRangeException.");
                }
                catch (IndexOutOfRangeException) {
                    errorCount++;
                }
            }

            Assert.IsTrue(errorCount == invalidIdxs.Count(), string.Format("testGridIdx[idx] did not throw IndexOutOfRangeException as expected for idx = [{0}, {1}]", invalidIdxs[0], invalidIdxs[1]));
        }

        [TestMethod]
        public void TiledGridInvalidIndexTest()
        {
            Grid<BaseTile> testGridCoords = new Grid<BaseTile>(5);
            testGridCoords[1, 1] = new BaseTile() { Value = FieldStatus.Floor };
            testGridCoords[2, 1] = new BaseTile() { Value = FieldStatus.Floor };
            testGridCoords[3, 1] = new BaseTile() { Value = FieldStatus.Floor };
            testGridCoords[2, 2] = new BaseTile() { Value = FieldStatus.Floor };

            var xCoords = new int[] { -1, testGridCoords.GridSideLength + 1 };
            var yCoords = new int[] { -1, testGridCoords.GridSideLength + 1 };

            int errorCount = 0;
            foreach (int x in xCoords) {
                foreach (int y in yCoords) {
                    try {
                        var test = testGridCoords[x, y];
                        Assert.Fail("Object test didn't throw IndexOutOfRangeException.");
                    }
                    catch (IndexOutOfRangeException) {
                        errorCount++;
                    }
                }
            }

            Assert.IsTrue(errorCount == xCoords.Count() + yCoords.Count(), string.Format("testGridCoords[x,y] did not throw IndexOutOfRangeException as expected for x = [{0}, {1}] and y = [{2}, {3}]", xCoords[0], xCoords[1], yCoords[0], yCoords[1]));
        }
    }
}
