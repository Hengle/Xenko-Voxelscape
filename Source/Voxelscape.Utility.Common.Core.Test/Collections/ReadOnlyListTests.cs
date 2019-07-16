using System.Linq;
using FluentAssertions;
using Voxelscape.Utility.Common.Core.Collections;
using Xunit;

namespace Voxelscape.Utility.Common.Core.Test.Collections
{
	/// <summary>
	///
	/// </summary>
	public static class ReadOnlyListTests
	{
		[Fact]
		public static void EmptyIsEmpty()
		{
			var subject = ReadOnlyList.Empty<int>();
			subject.Count.Should().Be(0);
		}

		[Fact]
		public static void EmptyReturnsSameInstance()
		{
			var subject1 = ReadOnlyList.Empty<int>();
			var subject2 = ReadOnlyList.Empty<int>();
			subject1.Should().BeSameAs(subject2);
		}

		[Fact]
		public static void Converter()
		{
			var source = new[] { (short)'a', (short)'b', (short)'c' };
			var subject = ReadOnlyList.Convert(source, value => (char)value);

			subject.Count.Should().Be(source.Length);
			for (int index = 0; index < subject.Count; index++)
			{
				subject[index].Should().Be((char)source[index]);
			}

			subject.SequenceEqual(source.Select(value => (char)value)).Should().BeTrue();
		}

		[Fact]
		public static void Combine()
		{
			var source1 = new[] { 10, 20, 30 };
			var source2 = new[] { 40, 50 };

			var subject = ReadOnlyList.CombineParams(source1, source2);

			subject.Count.Should().Be(source1.Length + source2.Length);
			subject.SequenceEqual(source1.Concat(source2)).Should().BeTrue();

			subject[0].Should().Be(source1[0]);
			subject[1].Should().Be(source1[1]);
			subject[2].Should().Be(source1[2]);
			subject[3].Should().Be(source2[0]);
			subject[4].Should().Be(source2[1]);
		}

		[Fact]
		public static void PartitionAtStart()
		{
			var source = new[] { 10, 20, 30, 40, 50 };

			var subject = ReadOnlyList.Partition(source, 0, 2);

			subject.Count.Should().Be(2);
			subject.SequenceEqual(source.Take(2)).Should().BeTrue();
			subject[0].Should().Be(source[0]);
			subject[1].Should().Be(source[1]);
		}

		[Fact]
		public static void PartitionInMiddle()
		{
			var source = new[] { 10, 20, 30, 40, 50 };

			var subject = ReadOnlyList.Partition(source, 1, 3);

			subject.Count.Should().Be(3);
			subject.SequenceEqual(source.Skip(1).Take(3)).Should().BeTrue();
			subject[0].Should().Be(source[1]);
			subject[1].Should().Be(source[2]);
			subject[2].Should().Be(source[3]);
		}
	}
}
