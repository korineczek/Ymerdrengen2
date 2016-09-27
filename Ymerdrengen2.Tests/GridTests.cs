// <copyright file="GridTests.cs" company="Team4">
// Copyright(c) 2016 All Rights Reserved
// </copyright>
// <author>Alexander Kirk Jørgensen</author>
// <date>27-09-2016</date>
// <summary>Test class for the Grid structure.</summary>
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grid;

namespace Ymerdrengen.Grid.Tests
{
    /// <summary>
    /// Testing whether the grid:
    ///   01234
    /// 0 xxxxx 4
    /// 1 xfffx 9
    /// 2 xxfxx 14
    /// 3 xxxxx 19
    /// 4 xxxxx 24
    /// 
    /// Is correctly setup with all getters and setters.
    /// </summary>
    [TestClass]
    public class GridTests {
        [TestMethod]
        public void GridGettersTest() {
            Grid<bool> testGridCoords = new Grid<bool>(5);
            testGridCoords[1, 1] = true;
            testGridCoords[2, 1] = true;
            testGridCoords[3, 1] = true;
            testGridCoords[2, 2] = true;

            // Check whether the coords have been set
            for (int i = 0; i < testGridCoords.GridSideLength; i++) {
                for (int j = 0; j < testGridCoords.GridSideLength; j++) {
                    if (i == 1 && j == 1 ){
                        Assert.IsTrue(testGridCoords[i, j]); continue;
                    } else if (i == 2 && (j == 1 || j == 2)){
                        Assert.IsTrue(testGridCoords[i, j]); continue;
                    } else if (i == 3 && j == 1) {
                        Assert.IsTrue(testGridCoords[i, j]); continue;
                    } else
                        Assert.IsFalse(testGridCoords[i, j]);
                }
            }
        }

        [TestMethod]
        public void GridIndexerTest() {
            Grid<bool> testGridIdx = new Grid<bool>(5);
            testGridIdx[6] = true;
            testGridIdx[7] = true;
            testGridIdx[8] = true;
            testGridIdx[12] = true;

            for (int i = 0; i < testGridIdx.MaxGridCount; i++) {
                switch (i) {
                    case 6: // All following fields should be marked true.
                    case 7:
                    case 8:
                    case 12:
                        Assert.IsTrue(testGridIdx[i]); break;
                    default:
                        Assert.IsFalse(testGridIdx[i]); break;
                }
            }
        }

        [TestMethod]
        public void GridNegativeCoordTest()
        {
            Grid<bool> testGridIdx = new Grid<bool>(5);
            testGridIdx[6] = true;
            testGridIdx[7] = true;
            testGridIdx[8] = true;
            testGridIdx[12] = true;

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
        public void GridInvalidIndexTest()
        {
            Grid<bool> testGridCoords = new Grid<bool>(5);
            testGridCoords[1, 1] = true;
            testGridCoords[2, 1] = true;
            testGridCoords[3, 1] = true;
            testGridCoords[2, 2] = true;

            var xCoords = new int[] { -1, testGridCoords.GridSideLength + 1 };
            var yCoords = new int[] { -1, testGridCoords.GridSideLength + 1 };

            int errorCount = 0;
            foreach (int x in xCoords) {
                foreach (int y in yCoords) {
                    try {
                        var test = testGridCoords[x, y];
                        Assert.Fail("Object test didn't throw IndexOutOfRangeException.");
                    } catch (IndexOutOfRangeException) {
                        errorCount++;
                    }
                }
            }

            Assert.IsTrue(errorCount == xCoords.Count() + yCoords.Count(), string.Format("testGridCoords[x,y] did not throw IndexOutOfRangeException as expected for x = [{0}, {1}] and y = [{2}, {3}]", xCoords[0], xCoords[1], yCoords[0], yCoords[1]));
        }
    }
}
