using Voxelscape.Xenko.Game.Scenes.Streaming;
using Xenko.Engine;

namespace Voxelscape.Xenko.Game
{
	/// <summary>
	///
	/// </summary>
	public class VoxelscapeGame : global::Xenko.Engine.Game
	{
		/// <inheritdoc />
		protected override void BeginRun()
		{
			base.BeginRun();

			var sceneController = new Entity(nameof(StreamingSceneController));
			sceneController.Add(new StreamingSceneController(this));
			this.SceneSystem.SceneInstance.RootScene.Entities.Add(sceneController);
		}
	}
}
