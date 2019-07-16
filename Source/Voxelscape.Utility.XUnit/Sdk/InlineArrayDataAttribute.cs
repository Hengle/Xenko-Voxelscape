using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Voxelscape.Utility.XUnit.Sdk
{
	/// <summary>
	/// Provides a data source for a data theory, with the data coming from inline values that are
	/// automatically wrapped into an array to be passed as the first argument to the test.
	/// </summary>
	[DataDiscoverer("Xunit.Sdk.InlineDataDiscoverer", "xunit.core")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class InlineArrayDataAttribute : DataAttribute
	{
		// Todo Steven - I would like this to support Enums if possible.
		// http://stackoverflow.com/questions/1235617/how-to-pass-objects-into-an-attribute-constructor
		// Seems to make it sound like it would be possible but not sure how do.
		// Also - the InlineDataAttribute in XUnit that is class is based off of was marked with [CLSCompliant(false)]
		// not sure what it's needed for but Visual Studio did not like me trying to add it to this class.

		/// <summary>
		/// The argument data to be passed to the test method.
		/// </summary>
		/// <remarks>
		/// The test method takes an array of arguments, each entry in the array matching one of the method's
		/// parameters. This class wraps a params array of arguments into an array to then place as the first
		/// and only argument into this array of arguments.
		/// </remarks>
		private readonly object[] data;

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params object[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params bool[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params byte[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params char[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params double[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params float[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params int[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params long[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params short[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params string[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineArrayDataAttribute"/> class.
		/// </summary>
		/// <param name="arguments">The arguments to pass to the test.</param>
		public InlineArrayDataAttribute(params Type[] arguments)
		{
			this.data = new object[] { arguments };
		}

		/// <inheritdoc/>
		public override IEnumerable<object[]> GetData(MethodInfo testMethod)
		{
			// the data (already an array of arguments that contains a single value, an another array of values)
			// is wrapped in yet another array because the DataAttribute base type actually produces a
			// sequence of test cases, not just a single one, but this implementation only provides data
			// for a single test
			return new[] { this.data };
		}
	}
}
