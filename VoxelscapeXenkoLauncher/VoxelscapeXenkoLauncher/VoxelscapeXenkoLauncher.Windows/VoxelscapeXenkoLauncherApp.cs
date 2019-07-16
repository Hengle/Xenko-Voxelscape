using Voxelscape.Xenko.Game;
using Xenko.Engine;

namespace VoxelscapeXenkoLauncher
{
    class VoxelscapeXenkoLauncherApp
    {
        static void Main(string[] args)
        {
			using (var game = new VoxelscapeGame())
			{
				game.Run();
			}

			////using (var game = new Game())
			////{
			////	game.Run();
			////}
		}
	}
}
