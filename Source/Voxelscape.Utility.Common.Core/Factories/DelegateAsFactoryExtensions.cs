﻿using System;
using Voxelscape.Utility.Common.Core.Factories;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods to easily wrap delegates in factory instances.
/// </summary>
public static class DelegateAsFactoryExtensions
{
	/// <summary>
	/// Wraps a <see cref="Func{TResult}" /> in a <see cref="DelegateFactory{TResult}" />.
	/// </summary>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<TResult> AsFactory<TResult>(
		this Func<TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<TResult>(func);
	}

	/// <summary>
	/// Wraps a <see cref="Func{T, TResult}" /> in a <see cref="DelegateFactory{T, TResult}" />.
	/// </summary>
	/// <typeparam name="T">The type of the argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<T, TResult> AsFactory<T, TResult>(
		this Func<T, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<T, TResult>(func);
	}

	/// <summary>
	/// Wraps a <see cref="Func{T1, T2, TResult}" /> in a <see cref="DelegateFactory{T1, T2, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<T1, T2, TResult> AsFactory<T1, T2, TResult>(
		this Func<T1, T2, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<T1, T2, TResult>(func);
	}

	/// <summary>
	/// Wraps a <see cref="Func{T1, T2, T3, TResult}" /> in a <see cref="DelegateFactory{T1, T2, T3, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<T1, T2, T3, TResult> AsFactory<T1, T2, T3, TResult>(
		this Func<T1, T2, T3, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<T1, T2, T3, TResult>(func);
	}

	/// <summary>
	/// Wraps a <see cref="Func{T1, T2, T3, T4, TResult}" /> in a <see cref="DelegateFactory{T1, T2, T3, T4, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<T1, T2, T3, T4, TResult> AsFactory<T1, T2, T3, T4, TResult>(
		this Func<T1, T2, T3, T4, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<T1, T2, T3, T4, TResult>(func);
	}

	/// <summary>
	/// Wraps a <see cref="Func{T1, T2, T3, T4, T5, TResult}" /> in a
	/// <see cref="DelegateFactory{T1, T2, T3, T4, T5, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<T1, T2, T3, T4, T5, TResult> AsFactory<T1, T2, T3, T4, T5, TResult>(
		this Func<T1, T2, T3, T4, T5, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<T1, T2, T3, T4, T5, TResult>(func);
	}

	/// <summary>
	/// Wraps a <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}" /> in a
	/// <see cref="DelegateFactory{T1, T2, T3, T4, T5, T6, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<T1, T2, T3, T4, T5, T6, TResult> AsFactory<T1, T2, T3, T4, T5, T6, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<T1, T2, T3, T4, T5, T6, TResult>(func);
	}

	/// <summary>
	/// Wraps a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}" /> in a
	/// <see cref="DelegateFactory{T1, T2, T3, T4, T5, T6, T7, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="T7">The type of the seventh argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult> AsFactory<T1, T2, T3, T4, T5, T6, T7, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult>(func);
	}

	/// <summary>
	/// Wraps a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" /> in a
	/// <see cref="DelegateFactory{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="T7">The type of the seventh argument passed to the factory.</typeparam>
	/// <typeparam name="T8">The type of the eighth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static DelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult> AsFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new DelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func);
	}
}
