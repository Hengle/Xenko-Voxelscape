﻿using Microsoft.Xna.Framework;
using Voxelscape.Common.Indexing.Core.Indices;

/// <summary>
/// Provides extension methods for <see cref="Index3D"/>.
/// </summary>
public static class Index3DExtensions
{
	public static Vector3 ToVector(this Index3D index) => new Vector3(index.X, index.Y, index.Z);
}