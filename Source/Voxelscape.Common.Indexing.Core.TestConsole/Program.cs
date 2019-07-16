using System;
using Voxelscape.Common.Indexing.Core.TestConsole.Enumerables;
using Voxelscape.Common.Indexing.Core.TestConsole.Rasterization;

namespace Voxelscape.Common.Indexing.Core.TestConsole
{
	/// <summary>
	/// Provides the entry point of the program.
	/// </summary>
	internal static class Program
	{
		/// <summary>
		/// The starting point of the program.
		/// </summary>
		/// <param name="args">The command line arguments.</param>
		private static void Main(string[] args)
		{
			////RasterizationPrinter.PrintCircle();
			////RasterizationPrinter.PrintCylinder();
			////RasterizationPrinter.PrintSphere();

			////RasterCircleTests.Run();

			SpiralTests.Run();

			Console.ReadKey();
		}
	}
}
