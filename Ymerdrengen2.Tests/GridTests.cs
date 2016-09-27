// <copyright file="GridTests.cs" company="Team4">
// Copyright(c) 2016 All Rights Reserved
// </copyright>
// <author>Alexander Kirk Jørgensen</author>
// <date>27-09-2016</date>
// <summary>Test class for the Grid structure.</summary>
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
    }
}
