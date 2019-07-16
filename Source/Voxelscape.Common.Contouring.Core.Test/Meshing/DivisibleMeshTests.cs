using System.Linq;
using FluentAssertions;
using Voxelscape.Common.Contouring.Core.Meshing;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Xunit;

namespace Voxelscape.Common.Contouring.Core.Test.Meshing
{
	/// <summary>
	///
	/// </summary>
	public static class DivisibleMeshTests
	{
		[Fact]
		public static void EmptyIsEmpty()
		{
			var subject = DivisibleMesh.Empty<int>();

			subject.Groups.Count.Should().Be(0);
			subject.Offsets.Count.Should().Be(0);
			subject.Vertices.Count.Should().Be(0);
		}

		[Fact]
		public static void CombineSingle()
		{
			var builder = new MutableDivisibleMesh<int>();
			builder.AddTriangle(1, 1, 1);
			builder.AddTriangle(2, 2, 2);
			builder.AddTriangle(3, 3, 3);

			var subject = DivisibleMesh.Combine(builder);

			SingleMeshAsserts(subject, 3);
		}

		[Fact]
		public static void Combine()
		{
			var builder1 = new MutableDivisibleMesh<int>();
			builder1.AddTriangle(1, 1, 1);
			builder1.AddTriangle(2, 2, 2);
			builder1.AddTriangle(3, 3, 3);

			var builder2 = new MutableDivisibleMesh<int>();
			builder2.AddTriangle(4, 4, 4);
			builder2.AddTriangle(5, 5, 5);
			builder2.AddTriangle(6, 6, 6);

			var subject = DivisibleMesh.Combine(builder1, builder2);

			SingleMeshAsserts(subject, 6);
		}

		[Fact]
		public static void Split()
		{
			var builder = new MutableDivisibleMesh<int>();
			builder.AddTriangle(1, 1, 1);
			builder.AddTriangle(2, 2, 2);
			builder.AddTriangle(3, 3, 3);
			builder.AddTriangle(4, 4, 4);

			int groupsPerMesh = 2;
			var subjects = DivisibleMesh.Split(groupsPerMesh * MeshConstants.VerticesPerTriangle, builder).ToArray();

			subjects.Length.Should().Be(2);

			subjects[0].Groups.Count.Should().Be(2);
			subjects[0].Offsets.Count.Should().Be(6);
			subjects[0].Vertices.Count.Should().Be(6);

			subjects[1].Groups.Count.Should().Be(2);
			subjects[1].Offsets.Count.Should().Be(6);
			subjects[1].Vertices.Count.Should().Be(6);
		}

		[Fact]
		public static void SplitSmallerLastMesh()
		{
			var builder = new MutableDivisibleMesh<int>();
			builder.AddTriangle(1, 1, 1);
			builder.AddTriangle(2, 2, 2);
			builder.AddTriangle(3, 3, 3);

			int groupsPerMesh = 2;
			var subjects = DivisibleMesh.Split(groupsPerMesh * MeshConstants.VerticesPerTriangle, builder).ToArray();

			subjects.Length.Should().Be(2);

			subjects[0].Groups.Count.Should().Be(2);
			subjects[0].Offsets.Count.Should().Be(6);
			subjects[0].Vertices.Count.Should().Be(6);

			subjects[1].Groups.Count.Should().Be(1);
			subjects[1].Offsets.Count.Should().Be(3);
			subjects[1].Vertices.Count.Should().Be(3);
		}

		[Fact]
		public static void CombineAndSplit()
		{
			var builder1 = new MutableDivisibleMesh<int>();
			builder1.AddTriangle(1, 1, 1);
			builder1.AddTriangle(2, 2, 2);
			builder1.AddTriangle(3, 3, 3);

			var builder2 = new MutableDivisibleMesh<int>();
			builder2.AddTriangle(4, 4, 4);
			builder2.AddTriangle(5, 5, 5);
			builder2.AddTriangle(6, 6, 6);

			int groupsPerMesh = 2;
			var subjects = DivisibleMesh.CombineAndSplit(
				groupsPerMesh * MeshConstants.VerticesPerTriangle, builder1, builder2).ToArray();

			subjects.Length.Should().Be(3);

			subjects[0].Groups.Count.Should().Be(2);
			subjects[0].Offsets.Count.Should().Be(6);
			subjects[0].Vertices.Count.Should().Be(6);

			subjects[1].Groups.Count.Should().Be(2);
			subjects[1].Offsets.Count.Should().Be(6);
			subjects[1].Vertices.Count.Should().Be(6);

			subjects[2].Groups.Count.Should().Be(2);
			subjects[2].Offsets.Count.Should().Be(6);
			subjects[2].Vertices.Count.Should().Be(6);
		}

		private static void SingleMeshAsserts(IDivisibleMesh<int> subject, int groups)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(groups >= 0);

			subject.Groups.Count.Should().Be(groups);

			foreach (var group in subject.Groups)
			{
				group.Offsets.Should().Be(3);
				group.Vertices.Should().Be(3);
				group.Triangles.Should().Be(1);
			}

			var vertex = subject.Vertices.GetEnumerator();
			var offset = subject.Offsets.GetEnumerator();

			for (int count = 1; count <= groups; count++)
			{
				vertex.MoveNext().Should().BeTrue();
				offset.MoveNext().Should().BeTrue();
				vertex.Current.Should().Be(count);
				offset.Current.Should().Be(0);

				vertex.MoveNext().Should().BeTrue();
				offset.MoveNext().Should().BeTrue();
				vertex.Current.Should().Be(count);
				offset.Current.Should().Be(1);

				vertex.MoveNext().Should().BeTrue();
				offset.MoveNext().Should().BeTrue();
				vertex.Current.Should().Be(count);
				offset.Current.Should().Be(2);
			}
		}
	}
}
