// <copyright file="ITile.cs" company="Team4">
// Copyright(c) 2016 All Rights Reserved
// </copyright>
// <author>Alexander Kirk Jørgensen</author>
// <date>27-09-2016</date>
// <summary>Interface defining grid tiles.</summary>
namespace Grid
{
    using System;
    /// <summary>
    /// Defines whether field has a floor or is special.
    /// </summary>
    [Flags]
    public enum FieldStatus
    {
        /// <summary>
        /// Field has no floor.
        /// </summary>
        None = 0,

        /// <summary>
        /// Field has floor
        /// </summary>
        Floor = 1,

        /// <summary>
        /// Field is on fire
        /// </summary>
        OnFire = 2,

        /// <summary>
        /// Field contains power-up
        /// </summary>
        PickUp = 4,

        /// <summary>
        /// Field is blocked
        /// </summary>
        Blocked = 8,

            /// <summary>
            /// Field is blocked
            /// </summary>
        NewTile = 16
    }

    /// <summary>
    /// Interface defines valid Grid tiles.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// Get the boolean value of the Tile, describing whether it has a floor or not.
        /// </summary>
        /// <returns>The current state of the tile; true means "Has floor", false means "Has no floor".</returns>
        bool HasFloor();
        bool IsPickUp();
        bool IsOnFire();
        bool IsBlocked();
        bool IsNewTile();

        void ToggleFlags(FieldStatus toggles);
        FieldStatus GetStatus();
    }
}
