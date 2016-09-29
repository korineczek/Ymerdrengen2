using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grid;

namespace Ymerdrengen2.Tests
{
    [TestClass]
    public class TileTests
    {
        /// <summary>
        /// Tests whether a tile is always correctly interpreted to have a floor.
        /// </summary>
        [TestMethod]
        public void TileHasFloorTest()
        {
            BaseTile[] testTilesNone = new BaseTile[] {
                new BaseTile() { Value = FieldStatus.None },
                new BaseTile() { Value = FieldStatus.OnFire },
                new BaseTile() { Value = FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.OnFire | FieldStatus.PickUp }
            };

            BaseTile[] testTilesFloor = new BaseTile[] {
                new BaseTile() { Value = FieldStatus.Floor },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.OnFire },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.OnFire | FieldStatus.PickUp }
            };

            foreach (BaseTile bT in testTilesNone) {
                Assert.IsFalse(bT.HasFloor());
            }
            foreach (BaseTile bT in testTilesFloor) {
                Assert.IsTrue(bT.HasFloor());
            }
        }

        [TestMethod]
        public void TileOnFireTest()
        {
            BaseTile[] testTilesNone = new BaseTile[] {
                new BaseTile() { Value = FieldStatus.None },
                new BaseTile() { Value = FieldStatus.Floor },
                new BaseTile() { Value = FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.PickUp },
            };

            BaseTile[] testTilesOnFire = new BaseTile[] {
                new BaseTile() { Value = FieldStatus.OnFire },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.OnFire },
                new BaseTile() { Value = FieldStatus.OnFire | FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.OnFire | FieldStatus.PickUp }
            };

            foreach (BaseTile bT in testTilesNone) {
                Assert.IsFalse(bT.IsOnFire());
            }
            foreach (BaseTile bT in testTilesOnFire) {
                Assert.IsTrue(bT.IsOnFire());
            }
        }

        [TestMethod]
        public void TileHasPickup()
        {
            BaseTile[] testTilesNone = new BaseTile[] {
                new BaseTile() { Value = FieldStatus.None },
                new BaseTile() { Value = FieldStatus.Floor },
                new BaseTile() { Value = FieldStatus.OnFire },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.OnFire },
            };

            BaseTile[] testTilesHasPickup = new BaseTile[] {
                new BaseTile() { Value = FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.OnFire | FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.OnFire | FieldStatus.PickUp }
            };

            foreach (BaseTile bT in testTilesNone) {
                Assert.IsFalse(bT.IsPickUp());
            }
            foreach (BaseTile bT in testTilesHasPickup) {
                Assert.IsTrue(bT.IsPickUp());
            }
        }

        [TestMethod]
        public void TileChangeTest()
        {
            BaseTile[] testTiles = new BaseTile[] {
                new BaseTile() { Value = FieldStatus.None },
                new BaseTile() { Value = FieldStatus.Floor },
                new BaseTile() { Value = FieldStatus.OnFire },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.OnFire },
                new BaseTile() { Value = FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.OnFire | FieldStatus.PickUp },
                new BaseTile() { Value = FieldStatus.Floor | FieldStatus.OnFire | FieldStatus.PickUp }
            };

            for (int i = 0; i < testTiles.Length; i++) {
                testTiles[i].ToggleFlags(FieldStatus.Floor | FieldStatus.OnFire | FieldStatus.PickUp); // After converting BaseTile to Class instead of Struct.
                //testTiles[i] = ToggleFlags(testTiles[i], FieldStatus.Floor | FieldStatus.OnFire | FieldStatus.PickUp);
                // DEPRECATED(ToggleFlags should be located in the GridManager.)
            }

            FieldStatus[] expectedResults = new FieldStatus[] {
                (FieldStatus)7, // FieldStatus.Floor | FieldStatus.OnFire | FieldStatus.PickUp OR 1 + 2 + 4
                (FieldStatus)6, // FieldStatus.OnFire | FieldStatus.PickUp OR 2 + 4
                (FieldStatus)5, // FieldStatus.Floor | FieldStatus.PickUp OR 1 + 4
                (FieldStatus)4, // FieldStatus.PickUp OR 4
                (FieldStatus)3, // FieldStatus.Floor | FieldStatus.Onfire OR 1 + 2
                (FieldStatus)2, // FieldStatus.OnFire OR 2
                (FieldStatus)1, // FieldStatus.Floor OR 1
                (FieldStatus)0  // FieldStatus.None OR 0
            }; // The results are precalculated to fit the expected value after a toggle operation.

            for (int i = 0; i < testTiles.Length; i++) {
                Assert.AreEqual(expectedResults[i], testTiles[i].Value);
            }

            // Deprecated functionality, use ITile.ToggleFlags(FieldStatus).
            BaseTile pickupTest = new BaseTile() { Value = FieldStatus.Floor | FieldStatus.PickUp };
            pickupTest.ToggleFlags(FieldStatus.PickUp);
            //pickupTest = ToggleFlags(pickupTest, FieldStatus.PickUp); // Actually toggles flags.
            //ToggleFlags(pickupTest, FieldStatus.PickUp); // Fails to toggle flags.

            Assert.AreEqual(FieldStatus.Floor, pickupTest.Value); 
        }

        public BaseTile ToggleFlags(BaseTile tile, FieldStatus flags)
        {
            return new BaseTile() { Value = tile.Value ^ flags }; // '^' ís a bitwise XOR operator.
        }
    }
}
