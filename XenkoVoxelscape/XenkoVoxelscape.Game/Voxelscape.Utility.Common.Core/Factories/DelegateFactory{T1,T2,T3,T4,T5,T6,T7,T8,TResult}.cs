﻿using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Factories
{
	/// <summary>
	/// Allows a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" /> to be wrapped
	/// in the <see cref="IFactory{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" /> interface.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument.</typeparam>
	/// <typeparam name="T2">The type of the second argument.</typeparam>
	/// <typeparam name="T3">The type of the third argument.</typeparam>
	/// <typeparam name="T4">The type of the forth argument.</typeparam>
	/// <typeparam name="T5">The type of the fifth input argument.</typeparam>
	/// <typeparam name="T6">The type of the sixth input argument.</typeparam>
	/// <typeparam name="T7">The type of the seventh input argument.</typeparam>
	/// <typeparam name="T8">The type of the eighth input argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public class DelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult> : IFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult>
	{
		/// <summary>
		/// The instance creation delegate.
		/// </summary>
		private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> create;

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateFactory{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> class.
		/// </summary>
		/// <param name="instanceCreator">The instance creator.</param>
		public DelegateFactory(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> instanceCreator)
		{
			Contracts.Requires.That(instanceCreator != null);

			this.create = instanceCreator;
		}

		#region IFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Members

		/// <inheritdoc />
		public TResult Create(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			return this.create(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		#endregion
	}
}
