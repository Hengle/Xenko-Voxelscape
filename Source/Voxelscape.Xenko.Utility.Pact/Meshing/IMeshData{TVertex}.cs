namespace Voxelscape.Xenko.Utility.Pact.Meshing
{
	public interface IMeshData<TVertex>
		where TVertex : struct
	{
		int VerticesCount { get; }

		TVertex[] Vertices { get; }

		int IndicesCount { get; }
	}
}
