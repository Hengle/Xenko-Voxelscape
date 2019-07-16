using System;
using Voxelscape.Common.Indexing.Core.Enumerables;

namespace Voxelscape.Common.Indexing.Core.TestConsole.Enumerables
{
	/// <summary>
	/// Provides tests for the <see cref="Spiral2D"/> class.
	/// </summary>
	public static class SpiralTests
	{
		/// <summary>
		/// Runs the test.
		/// </summary>
		public static void Run()
		{
			// size are spirals are the only configurable variables for this test
			int size = 7;
			int spirals = -1;

			// end of configurable variables
			int[,] array = new int[size, size];
			int count = 1;

			var options = new Spiral2D.Options()
			{
				Origin = array.GetMiddleIndex(),
				Spirals = spirals,
				IsEven = size.IsEven(),
			};

			foreach (var index in new Spiral2D(options))
			{
				if (!array.IsIndexValid(index))
				{
					break;
				}

				if (array[index.X, index.Y] != 0)
				{
					throw new Exception("Spiral returned the same value more than once!");
				}

				array[index.X, index.Y] = count;
				count++;
			}

			for (int iY = 0; iY < array.GetLength(1); iY++)
			{
				for (int iX = 0; iX < array.GetLength(0); iX++)
				{
					Console.Write(array[iX, iY].ToString("D2") + " ");
				}

				Console.WriteLine();
			}
		}
	}
}
