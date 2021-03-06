﻿using System;

namespace Voxelscape.Common.Indexing.Core.Enums
{
	/// <summary>
	/// An enumeration of the orthogonal axes in two dimensional space
	/// used to specify a combination of axes.
	/// </summary>
	[Flags]
	public enum Axes2D
	{
		/// <summary>
		/// No axis.
		/// </summary>
		None = 0,

		/// <summary>
		/// The x axis.
		/// </summary>
		X = 1 << 0,

		/// <summary>
		/// The y axis.
		/// </summary>
		Y = 1 << 1,

		/// <summary>
		/// The x and y axes.
		/// </summary>
		All = X | Y,
	}
}
