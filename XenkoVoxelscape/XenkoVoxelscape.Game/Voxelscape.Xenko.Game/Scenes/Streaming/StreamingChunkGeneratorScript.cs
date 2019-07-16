using System;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Xenko.Engine;
using Xenko.Rendering;

namespace Voxelscape.Xenko.Game.Scenes.Streaming
{
	public class StreamingChunkGeneratorScript : StartupScript
	{
		private readonly StreamingStageMeshFactory meshFactory;

		private StreamingChunkGenerator generator;

		public StreamingChunkGeneratorScript(IStage<ChunkKey, Entity> stage, StreamingStageMeshFactory meshFactory)
		{
			Contracts.Requires.That(stage != null);
			Contracts.Requires.That(meshFactory != null);

			this.meshFactory = meshFactory;

			stage.Activated.Subscribe(async pair =>
			{
				pair.Value.Transform.Position = pair.Key.Index.ToXenkoVector() * this.meshFactory.ChunkLength;
				this.Entity.AddChild(pair.Value);

				// Do not use DontMarshallContext here. Must stay on UI thread.
				var model = await this.generator?.GenerateModelAsync(pair.Key);
				if (model != null)
				{
					pair.Value.Add(new ModelComponent(model));
				}
			});

			stage.Deactivated.Subscribe(pair =>
			{
				this.Entity.RemoveChild(pair.Value);
				pair.Value.Get<ModelComponent>()?.Model.Dispose();
				pair.Value.Dispose();
			});
		}

		/// <inheritdoc />
		public override void Start()
		{
			base.Start();

			var material = new MaterialInstance(this.Content.Load<Material>("Voxel Mesh Material"))
			{
				IsShadowCaster = true,
				////IsShadowReceiver = true
			};

			this.generator = new StreamingChunkGenerator(this.meshFactory, material, this.GraphicsDevice);
		}

		/// <inheritdoc />
		public override void Cancel()
		{
			base.Cancel();
			this.generator?.Complete();
		}
	}
}
