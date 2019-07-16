using System;
using System.Linq;
using System.Reactive.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Xenko.Core.Mathematics;
using MonoVector3 = Microsoft.Xna.Framework.Vector3;

/// <summary>
/// Provides extension methods for converting <see cref="IObservable{T}"/>.
/// </summary>
public static class ObservableConversionExtensions
{
	public static IObservable<MonoVector3> ToMono(this IObservable<Vector3> observable)
	{
		Contracts.Requires.That(observable != null);

		return observable.Select(vector => vector.ToMono());
	}
}
