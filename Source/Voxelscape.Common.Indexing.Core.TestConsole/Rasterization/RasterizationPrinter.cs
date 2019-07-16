namespace Voxelscape.Common.Indexing.Core.TestConsole.Rasterization
{
	using System;
	using Voxelscape.Common.Indexing.Core.Indices;
	using Voxelscape.Common.Indexing.Core.Rasterization;
	using Voxelscape.Common.Indexing.Pact.Bounds;
	using Voxelscape.Common.Indexing.Pact.Rasterization;
	using Voxelscape.Utility.Common.Pact.Diagnostics;

	/// <summary>
	/// Provides methods for testing rasterization masks.
	/// </summary>
	public static class RasterizationPrinter
	{
		/// <summary>
		/// Prints a test circle.
		/// </summary>
		public static void PrintCircle()
		{
			Print(new CircleMask<bool>(7), 1);
		}

		/// <summary>
		/// Prints a test cylinder.
		/// </summary>
		public static void PrintCylinder()
		{
			Print(new CylinderMask<bool>(diameter: 7, height: 3), 1);
		}

		/// <summary>
		/// Prints a test sphere.
		/// </summary>
		public static void PrintSphere()
		{
			Print(new SphereMask<bool>(7), 1);
		}

		/// <summary>
		/// Prints the specified mask.
		/// </summary>
		/// <param name="mask">The mask to rasterize.</param>
		/// <param name="fidelity">The fidelity to rasterize the mask at.</param>
		public static void Print(IRasterizableMask<Index2D, bool> mask, float fidelity)
		{
			Contracts.Requires.That(mask != null);

			IBoundedIndexable<Index2D, bool> indexable = mask.Rasterize(fidelity, true, false);
			Index2D lowerBound = indexable.LowerBounds;
			Index2D upperBound = indexable.UpperBounds;

			for (int iY = indexable.UpperBounds.Y; iY >= lowerBound.Y; iY--)
			{
				for (int iX = lowerBound.X; iX <= indexable.UpperBounds.X; iX++)
				{
					Console.Write(indexable[new Index2D(iX, iY)] ? "X" : "\u00B7");
				}

				Console.WriteLine();
			}
		}

		/// <summary>
		/// Prints the specified mask.
		/// </summary>
		/// <param name="mask">The mask to rasterize.</param>
		/// <param name="fidelity">The fidelity to rasterize the mask at.</param>
		public static void Print(IRasterizableMask<Index3D, bool> mask, float fidelity)
		{
			Contracts.Requires.That(mask != null);

			IBoundedIndexable<Index3D, bool> indexable = mask.Rasterize(fidelity, true, false);
			Index3D lowerBound = indexable.LowerBounds;
			Index3D upperBound = indexable.UpperBounds;

			for (int iY = indexable.UpperBounds.Y; iY >= lowerBound.Y; iY--)
			{
				for (int iZ = indexable.UpperBounds.Z; iZ >= lowerBound.Z; iZ--)
				{
					for (int iX = lowerBound.X; iX <= indexable.UpperBounds.X; iX++)
					{
						Console.Write(indexable[new Index3D(iX, iY, iZ)] ? "X" : "\u00B7");
					}

					Console.WriteLine();
				}

				Console.WriteLine();
			}
		}
	}
}
