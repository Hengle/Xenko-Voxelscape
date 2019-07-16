using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Voxelscape.Stages.Management.Pact.Generation;

namespace Voxelscape.Stages.TestConsole
{
	internal class Program
	{
		private static int Main(string[] args)
		{
			try
			{
				return AsyncContext.Run(() => MainAsync(args));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex);
				return -1;
			}
		}

		private static async Task<int> MainAsync(string[] args)
		{
			await TestStageGeneration().DontMarshallContext();

			Console.WriteLine("Test Run Completed. Press 'Esc' to exit.");

			while (Console.ReadKey(true).Key != ConsoleKey.Escape)
			{
			}

			return 0;
		}

		private static async Task TestStageGeneration()
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();

			try
			{
				await NewGenerationExample.GenerateSkyIslandStage().DontMarshallContext();
				////await NewGenerationExample.GenerateNoiseStage().DontMarshallContext();

				timer.Stop();
				Console.WriteLine($"Finished generating stage in {timer.Elapsed.TotalSeconds:0.##} seconds.");
			}
			catch (StageGenerationException exception) when (exception.InnerException is OperationCanceledException)
			{
				timer.Stop();
				Console.WriteLine($"Canceled generating stage after {timer.Elapsed.TotalSeconds:0.##} seconds.");
			}
			catch (Exception exception)
			{
				timer.Stop();
				Console.WriteLine($"Exception generating stage after {timer.Elapsed.TotalSeconds:0.##} seconds.");
				Console.WriteLine($"Exception: {exception}");
			}
		}
	}
}
