﻿using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Factories
{
	/// <summary>
	/// Allows a <see cref="Func{T1, T2, TResult}" /> to be wrapped
	/// in the <see cref="IFactory{T1, T2, TResult}" /> interface.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument.</typeparam>
	/// <typeparam name="T2">The type of the second argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public class DelegateFactory<T1, T2, TResult> : IFactory<T1, T2, TResult>
	{
		/// <summary>
		/// The instance creation delegate.
		/// </summary>
		private readonly Func<T1, T2, TResult> create;

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateFactory{T1, T2, TResult}"/> class.
		/// </summary>
		/// <param name="instanceCreator">The instance creator.</param>
		public DelegateFactory(Func<T1, T2, TResult> instanceCreator)
		{
			Contracts.Requires.That(instanceCreator != null);

			this.create = instanceCreator;
		}

		#region IFactory<T1, T2, TResult> Members

		/// <inheritdoc />
		public TResult Create(T1 arg1, T2 arg2)
		{
			return this.create(arg1, arg2);
		}

		#endregion
	}
}
