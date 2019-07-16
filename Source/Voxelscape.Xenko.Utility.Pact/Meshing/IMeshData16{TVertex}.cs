namespace Voxelscape.Xenko.Utility.Pact.Meshing
{
	public interface IMeshData16<TVertex> : IMeshData<TVertex>
		where TVertex : struct
	{
		ushort[] Indices16 { get; }
	}
}
