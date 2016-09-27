// <copyright file="Grid.cs" company="Team4">
// Copyright(c) 2016 All Rights Reserved
// </copyright>
// <author>Alexander Kirk Jørgensen</author>
// <date>27-09-2016</date>
// <summary>Generic class the Grid structure.</summary>
namespace Grid
{
    using System;
    using System.Linq;

    /// <summary>
    /// Base generic class for the Grid structure. Contains accessors that transform from vectors to indexing. 
    /// </summary>
    /// <typeparam name="T">Any type that can be resolved to a FieldStatus or boolean.</typeparam>
    public class Grid<T>
    {
        /// <summary>
        /// Array containing all possible tile elements within the grid.
        /// </summary>
        private T[] innerGrid;

        /// <summary>
        /// Inner variable determining the Grid size.
        /// </summary>
        private int gridSideLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}" /> class. Side length is necessary to create the inner array of elements.
        /// </summary>
        /// <param name="sideLength">The side length of the grid, measured in tiles.</param>
        public Grid(int sideLength)
        {
            this.GridSideLength = sideLength;
        }

        /// <summary>
        /// Gets the side length of the grid.
        /// </summary>
        public int GridSideLength
        {
            get
            {
                return this.gridSideLength;
            }

            private set
            {
                this.gridSideLength = value;
                this.innerGrid = Enumerable.Repeat(Activator.CreateInstance<T>(), this.GridSideLength * this.GridSideLength).ToArray();
            }
        }

        /// <summary>
        /// Gets the count of innerGrid array. Should always be equal to <see cref="GridSideLength"/> squared.
        /// </summary>
        public int MaxGridCount
        {
            get { return this.GridSideLength * this.GridSideLength; }
        }
        
        /// <summary>
        /// Vector indexer for the inner array.
        /// </summary>
        /// <param name="x">X-coordinate of desired element.</param>
        /// <param name="y">Y-coordinate of desired element.</param>
        /// <returns>The element represented by the given vector.</returns>
        public T this[int x, int y]
        {
            get
            {
                return this.innerGrid[x + (y * this.GridSideLength)];
            }

            set
            {
                this.innerGrid[x + (y * this.GridSideLength)] = value;
            }
        }

        /// <summary>
        /// Direct indexer for the inner array.
        /// </summary>
        /// <param name="idx">Index of desired element.</param>
        /// <returns>The element represented by the given index.</returns>
        public T this[int idx]
        {
            get
            {
                return this.innerGrid[idx];
            }

            set
            {
                this.innerGrid[idx] = value;
            }
        }

        // Method getters and setters, if needed.
        // public T Get(int x, int y) { return innerGrid[x + y * GridSideLength]; }
        // public T Get(int idx) { return innerGrid[idx]; }
        // public void Set(bool newValue, int x, int y) { innerGrid[x + y * GridSideLength] = newValue; }
        // public void Set(bool newValue, int idx) { innerGrid[idx] = newValue; }
    }
}