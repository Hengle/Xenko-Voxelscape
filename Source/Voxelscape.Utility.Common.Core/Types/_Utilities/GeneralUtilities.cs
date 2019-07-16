using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Types
{
	/// <summary>
	/// Provides general purpose utility methods.
	/// </summary>
	public static class GeneralUtilities
	{
		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T temp = lhs;
			lhs = rhs;
			rhs = temp;
		}

		/// <summary>
		/// Deep clones the specified serializable source.
		/// </summary>
		/// <typeparam name="T">The serializable type to deep clone.</typeparam>
		/// <param name="source">The source to deep clone.</param>
		/// <returns>The deep cloned copy of the source.</returns>
		/// <remarks>
		/// This method only works on serializable types and will otherwise throw a contract exception.
		/// </remarks>
		public static T DeepCloneSerializable<T>(T source)
		{
			Contracts.Requires.That(typeof(T).IsSerializable);

			// don't serialize a null object, simply return the default for that object
			if (object.ReferenceEquals(source, null))
			{
				return default(T);
			}

			// serialize and then deserialize the source to create a deep copy of it
			IFormatter formatter = new BinaryFormatter();
			using (var stream = new MemoryStream())
			{
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}
