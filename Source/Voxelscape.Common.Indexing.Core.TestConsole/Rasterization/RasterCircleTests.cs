using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Rasterization;

namespace Voxelscape.Common.Indexing.Core.TestConsole.Rasterization
{
	/// <summary>
	/// Tests for the <see cref="RasterCircle"/> class.
	/// </summary>
	public static class RasterCircleTests
	{
		/// <summary>
		/// Runs the tests.
		/// </summary>
		public static void Run()
		{
			////int diameter = 4;
			////Index2D midPoint = new Index2D(2, 2);
			////int xLength = 6;
			////int yLength = 6;

			int diameter = 19;
			Index2D midPoint = new Index2D(10, 10);
			int xLength = 21;
			int yLength = 21;

			OutlineCircle(xLength, yLength, midPoint, diameter);
			Console.WriteLine();
			FillCircle(xLength, yLength, midPoint, diameter);

			Console.WriteLine();
			Console.WriteLine("Completed");
		}

		/// <summary>
		/// Outlines a circle.
		/// </summary>
		/// <param name="xLength">Length of the x-axis of the array.</param>
		/// <param name="yLength">Length of the y-axis of the array.</param>
		/// <param name="midPoint">The mid point of the circle.</param>
		/// <param name="diameter">The diameter of the circle.</param>
		private static void OutlineCircle(int xLength, int yLength, Index2D midPoint, int diameter)
		{
			int[,] array = new int[xLength, yLength];
			RasterCircle.OutlineCircle(array, midPoint, diameter, index => array[index.X, index.Y] + 1);

			Console.WriteLine("Outline");
			Console.WriteLine();
			PrintArray(array);
		}

		/// <summary>
		/// Fills a circle.
		/// </summary>
		/// <param name="xLength">Length of the x-axis of the array.</param>
		/// <param name="yLength">Length of the y-axis of the array.</param>
		/// <param name="midPoint">The mid point of the circle.</param>
		/// <param name="diameter">The diameter of the circle.</param>
		private static void FillCircle(int xLength, int yLength, Index2D midPoint, int diameter)
		{
			int[,] array = new int[xLength, yLength];
			RasterCircle.FillCircle(array, midPoint, diameter, index => array[index.X, index.Y] + 1);

			Console.WriteLine("Fill");
			Console.WriteLine();
			PrintArray(array);
		}

		/// <summary>
		/// Prints the circle.
		/// </summary>
		/// <param name="array">The array to print.</param>
		private static void PrintArray(int[,] array)
		{
			for (int iY = 0; iY < array.GetLength(1); iY++)
			{
				for (int iX = 0; iX < array.GetLength(0); iX++)
				{
					Console.Write(array[iX, iY] == 0 ? "\u00B7" : array[iX, iY].ToString());
				}

				Console.WriteLine();
			}
		}
	}
}
