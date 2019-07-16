using Xenko.Core.Mathematics;
using Xenko.Graphics;

namespace Voxelscape.Xenko.Utility.Pact.Vertices
{
	public interface IVertexFormat<TVertex>
		where TVertex : struct
	{
		VertexDeclaration Layout { get; }

		Vector3 GetPosition(TVertex vertex);
	}
}
