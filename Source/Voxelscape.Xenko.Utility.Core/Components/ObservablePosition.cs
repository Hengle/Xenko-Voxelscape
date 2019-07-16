using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Xenko.Core.Mathematics;
using Xenko.Engine;
using Xenko.Input;

namespace Voxelscape.Xenko.Utility.Core.Components
{
	public class ObservablePosition : SyncScript
	{
		private readonly Subject<Vector3> positionChanged = new Subject<Vector3>();

		public ObservablePosition()
		{
			this.PositionChanged = this.positionChanged.DistinctUntilChanged();
		}

		public IObservable<Vector3> PositionChanged { get; }

		public bool IsEnabled { get; set; } = true;

		public Keys ToggleEnabledKey { get; set; } = Keys.None;

		/// <inheritdoc />
		public override void Update()
		{
			if (this.Input.IsKeyPressed(this.ToggleEnabledKey))
			{
				this.IsEnabled = !this.IsEnabled;
			}

			if (this.IsEnabled)
			{
				this.positionChanged.OnNext(this.Entity.Transform.Position);
			}
		}

		public override void Cancel()
		{
			base.Cancel();
			this.positionChanged.OnCompletedAndDispose();
		}
	}
}
