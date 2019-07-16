using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Xenko.Utility.Pact.Meshing;
using Voxelscape.Xenko.Utility.Pact.Vertices;
using Xenko.Core.Mathematics;
using Xenko.Graphics;
using Xenko.Rendering;

namespace Voxelscape.Xenko.Utility.Core.Meshing
{
	public abstract class AbstractProceduralMeshFactory<TVertex>
		where TVertex : struct
	{
		public AbstractProceduralMeshFactory(IVertexFormat<TVertex> format, GraphicsDevice graphicsDevice)
		{
			Contracts.Requires.That(format != null);
			Contracts.Requires.That(graphicsDevice != null);

			this.Format = format;
			this.GraphicsDevice = graphicsDevice;
		}

		protected IVertexFormat<TVertex> Format { get; }

		protected GraphicsDevice GraphicsDevice { get; }

		protected Mesh CreateMesh(IndexBufferBinding indexBuffer, IMeshData<TVertex> meshData)
		{
			Contracts.Requires.That(indexBuffer != null);
			Contracts.Requires.That(meshData != null);

			var vertexBuffers = new[]
			{
				new VertexBufferBinding(
					Buffer.Vertex.New(this.GraphicsDevice, meshData.Vertices, GraphicsResourceUsage.Immutable),
					this.Format.Layout,
					meshData.VerticesCount),
			};

			var meshDraw = new MeshDraw()
			{
				IndexBuffer = indexBuffer,
				VertexBuffers = vertexBuffers,
				DrawCount = meshData.IndicesCount,
				PrimitiveType = PrimitiveType.TriangleList,
			};

			// create the bounding volumes
			var boundingBox = BoundingBox.Empty;
			for (int index = 0; index < meshData.VerticesCount; index++)
			{
				var position = this.Format.GetPosition(meshData.Vertices[index]);
				BoundingBox.Merge(ref boundingBox, ref position, out boundingBox);
			}

			var boundingSphere = BoundingSphere.FromBox(boundingBox);

			return new Mesh
			{
				Draw = meshDraw,
				BoundingBox = boundingBox,
				BoundingSphere = boundingSphere,
			};
		}
	}
}
