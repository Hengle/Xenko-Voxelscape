using Voxelscape.Xenko.Game;
using Xenko.Engine;

namespace XenkoVoxelscape
{
    class XenkoVoxelscapeApp
    {
        static void Main(string[] args)
        {
			using (var game = new VoxelscapeGame())
			{
				game.Run();
			}

			////using (var game = new Game())
			////{
			////    game.Run();
			////}
		}
    }
}
