using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Procedural.Core.Noise;
using Voxelscape.Stages.Management.Core.Chunks;
using Voxelscape.Stages.Management.Core.Generation;
using Voxelscape.Stages.Management.Core.Stages;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Core.Meshing;
using Voxelscape.Stages.Voxels.Core.Templates.NoiseWorld;
using Voxelscape.Stages.Voxels.Core.Templates.SkyIsland;
using Voxelscape.Stages.Voxels.Core.Terrain;
using Voxelscape.Stages.Voxels.Core.Voxels;
using Voxelscape.Stages.Voxels.Core.Voxels.Grid;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Stages.Voxels.SQLite.Meshing;
using Voxelscape.Stages.Voxels.SQLite.Templates.SkyIsland;
using Voxelscape.Stages.Voxels.SQLite.Voxels.Grid;
using Voxelscape.Utility.Concurrency.Core.LifeCycle;
using Voxelscape.Utility.Concurrency.Dataflow.Blocks;
using Voxelscape.Utility.Data.Core.Stores;
using Voxelscape.Utility.Data.Pact.Serialization;
using Voxelscape.Utility.Data.SQLite.Entities;
using Voxelscape.Utility.Data.SQLite.Stores;

namespace Voxelscape.Stages.TestConsole
{
	/// <summary>
	///
	/// </summary>
	[SuppressMessage(
		"StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "Because.")]
	public static class NewGenerationExample
	{
		/*
select count()
from MeshChunkEntity
where length(SerializedData) > 12
		 */

		////private static Index3D StageDimensions => new Index3D(64, 32, 64);

		private static Index3D StageDimensions => new Index3D(4, 4, 4);

		private static int TreeDepth => 4;

		private static long CacheSizeInMegaBytes => 1000;

		private static long CacheSizeInBytes => CacheSizeInMegaBytes * 1000000L;

		private static double CacheStashMultiplier => 2.0;

		public static async Task GenerateSkyIslandStage()
		{
			var path = @"C:\VoxelscapeDevelopment";

			Directory.CreateDirectory(path);
			Array.ForEach(Directory.GetFiles(path), File.Delete);

			// shared
			var stageIdentity = new StageIdentity("Test Stage");
			var stageBounds = new StageBounds(new ChunkKey(0, 0, 0), StageDimensions);
			var store = SQLiteStore.Locked.AnyEntities.Partitioned.New(
				new PartitionedConfigFactory(path, "TestStage.", ".db"));
			var multiNoise = new NoiseFactory(seed: 0);

			var absoluteOptions = new BatchSerializeGenerationOptions()
			{
				Batching = Batching.Dynamic,
				////CancellationToken = new CancellationTokenSource(4000).Token
			};

			#region map absolute

			var mapChunkConfig = new SkyIslandMapChunkConfig(TreeDepth);
			var mapChunkStore = new SkyIslandMapChunkStore(store);

			var mapConfig = SkyIslandMapGenerationConfigService.CreatePreconfigured(stageBounds, mapChunkConfig).Build();
			var mapChunkPopulator = new AsyncChunkPopulator<ISkyIslandMapChunk>(
				new SkyIslandMapChunkPopulator(mapConfig, stageBounds, multiNoise));

			var mapAbsolutePhase = new SkyIslandMapAbsolutePhase(
				stageIdentity,
				stageBounds,
				mapChunkConfig,
				mapChunkPopulator,
				mapChunkStore,
				maxBufferedChunks: 1000,
				options: absoluteOptions);

			#endregion

			#region maps stats

			var mapSerializer = new SkyIslandMapChunkResourcesSerializer(
				SkyIslandMapsSerializer.Get[absoluteOptions.SerializationEndianness], mapChunkConfig);
			var mapPersister = new SkyIslandMapChunkPersister(mapSerializer);
			var mapFromStorePopulator = new SkyIslandMapChunkStorePopulator(
				mapChunkStore,
				mapPersister,
				new ExteriorSkyIslandMapChunkPopulator(default(SkyIslandMaps)));

			var mapChunkCacheBuilder = new SkyIslandMapChunkCacheBuilder(mapChunkConfig, mapFromStorePopulator);
			var mapCacheOptions = new ChunkCacheBuilderOptions<SkyIslandMapChunkResources>()
			{
				StashCapacityMultiplier = 2,
				EagerFillPool = true,
			};

			mapChunkCacheBuilder.CreateAndAssignStandardResourceStash(CacheSizeInBytes / 10, mapCacheOptions);
			mapChunkCacheBuilder.SetFiniteBounds(stageBounds, default(SkyIslandMaps), modifyingThrowsException: true);
			var mapChunkCache = mapChunkCacheBuilder.Build();

			var statsStore = new KeyValueStore<KeyValueEntity>(new TypedAsyncStore<KeyValueEntity>(store));

			var statsOptions = new GenerationOptions()
			{
				MaxDegreeOfParallelism = 3,
			};
			var statsPhase = new SkyIslandMapStatsPhase(
				stageIdentity,
				stageBounds,
				mapChunkCache.AsReadOnly,
				statsStore,
				statsOptions);

			#endregion

			#region voxel absolute

			var voxelChunkConfig = new VoxelGridChunkConfig(TreeDepth);
			var voxelChunkStore = new VoxelGridChunkStore(store);

			var voxelConfig = SkyIslandVoxelGridGenerationConfigService.CreatePreconfigured().Build();
			var voxelChunkPopulator = new SkyIslandVoxelGridChunkPopulator(
				voxelConfig, mapChunkCache.AsReadOnly, multiNoise);

			var voxelAbsolutePhase = new VoxelGridAbsolutePhase(
				stageIdentity,
				stageBounds,
				voxelChunkConfig,
				voxelChunkPopulator,
				voxelChunkStore,
				maxBufferedChunks: 1000,
				options: absoluteOptions);

			#endregion

			#region contour

			var voxelSerializer = new VoxelGridChunkResourcesSerializer(
				TerrainVoxelSerializer.Get[absoluteOptions.SerializationEndianness], voxelChunkConfig);
			var voxelPersister = new VoxelGridChunkPersister(voxelSerializer);
			var voxelFromStorePopulator = new VoxelGridChunkStorePopulator(
				voxelChunkStore,
				voxelPersister,
				new ExteriorVoxelGridChunkPopulator(SkyIslandGenerationConstants.EmptyAir));

			var voxelChunkCacheBuilder = new VoxelGridChunkCacheBuilder(voxelChunkConfig, voxelFromStorePopulator);
			var voxelCacheOptions = new ChunkCacheBuilderOptions<VoxelGridChunkResources>()
			{
				StashCapacityMultiplier = 2,
				EagerFillPool = true,
			};

			voxelChunkCacheBuilder.CreateAndAssignStandardResourceStash(CacheSizeInBytes, voxelCacheOptions);
			voxelChunkCacheBuilder.SetFiniteBounds(
				stageBounds, SkyIslandGenerationConstants.EmptyAir, modifyingThrowsException: true);
			var voxelChunkCache = voxelChunkCacheBuilder.Build();

			var meshChunkStore = new MeshChunkStore(store);
			var contourer = new TerrainDualContourer<TerrainSurfaceData>(multiNoise);

			var contourPhase = new ContourPhase<TerrainSurfaceData>(
				stageIdentity,
				stageBounds,
				voxelChunkConfig,
				voxelChunkCache.AsReadOnly,
				contourer,
				meshChunkStore,
				maxBufferedChunks: 100,
				options: absoluteOptions);

			#endregion

			// seed
			var seedConfig = new StageSeedConfig(
				stageIdentity, mapAbsolutePhase, statsPhase, voxelAbsolutePhase, contourPhase);
			var seedOptions = new StageSeedOptions()
			{
				PostGeneration = new AggregateAsyncCompletable(mapChunkCache, voxelChunkCache),
			};
			var stageSeed = new StageSeed(seedConfig, seedOptions);
			stageSeed.Progress.Sample(TimeSpan.FromSeconds(.5)).Subscribe(progress => Console.WriteLine(progress));

			// generator
			var generatorOptions = new GenerationOptions()
			{
			};
			var stageGenerator = new StageGenerator(generatorOptions);

			await stageGenerator.GenerateStageAsync(stageSeed).DontMarshallContext();
			await stageGenerator.CompleteAndAwaitAsync().DontMarshallContext();
		}

		public static async Task GenerateNoiseStage()
		{
			var path = @"A:\VoxelscapeDevelopment";

			Directory.CreateDirectory(path);
			Array.ForEach(Directory.GetFiles(path), File.Delete);

			// shared
			var stageIdentity = new StageIdentity("Test Stage");
			var stageBounds = new StageBounds(new ChunkKey(0, 0, 0), StageDimensions);
			var store = SQLiteStore.Locked.AnyEntities.Partitioned.New(
				new PartitionedConfigFactory(path, "TestStage.", ".db"));
			var multiNoise = new NoiseFactory(seed: 0);

			var absoluteOptions = new BatchSerializeGenerationOptions()
			{
				Batching = Batching.Dynamic,
				SerializationEndianness = Endian.Little,
				////CancellationToken = new CancellationTokenSource(4000).Token
			};

			#region voxel absolute

			var voxelChunkConfig = new VoxelGridChunkConfig(TreeDepth);
			var voxelChunkStore = new VoxelGridChunkStore(store);

			var voxelChunkPopulator = new NoiseWorldVoxelGridChunkPopulator(
				multiNoise.Create(0),
				noiseScaling: 100,
				numberOfOctaves: 4,
				material: TerrainMaterial.Stone);

			var voxelAbsolutePhase = new VoxelGridAbsolutePhase(
				stageIdentity,
				stageBounds,
				voxelChunkConfig,
				voxelChunkPopulator.WrapWithAsync(),
				voxelChunkStore,
				maxBufferedChunks: 1000,
				options: absoluteOptions);

			#endregion

			#region contour

			var voxelSerializer = new VoxelGridChunkResourcesSerializer(
				TerrainVoxelSerializer.Get[absoluteOptions.SerializationEndianness], voxelChunkConfig);
			var voxelPersister = new VoxelGridChunkPersister(voxelSerializer);
			var voxelFromStorePopulator = new VoxelGridChunkStorePopulator(
				voxelChunkStore,
				voxelPersister,
				new ExteriorVoxelGridChunkPopulator(SkyIslandGenerationConstants.EmptyAir));

			var voxelChunkCacheBuilder = new VoxelGridChunkCacheBuilder(voxelChunkConfig, voxelFromStorePopulator);
			var voxelCacheOptions = new ChunkCacheBuilderOptions<VoxelGridChunkResources>()
			{
				StashCapacityMultiplier = 2,
				EagerFillPool = true,
			};

			voxelChunkCacheBuilder.CreateAndAssignStandardResourceStash(CacheSizeInBytes, voxelCacheOptions);
			voxelChunkCacheBuilder.SetFiniteBounds(
				stageBounds, SkyIslandGenerationConstants.EmptyAir, modifyingThrowsException: true);
			var voxelChunkCache = voxelChunkCacheBuilder.Build();

			var meshChunkStore = new MeshChunkStore(store);
			var contourer = new TerrainDualContourer<TerrainSurfaceData>(multiNoise);

			var contourPhase = new ContourPhase<TerrainSurfaceData>(
				stageIdentity,
				stageBounds,
				voxelChunkConfig,
				voxelChunkCache.AsReadOnly,
				contourer,
				meshChunkStore,
				maxBufferedChunks: 100,
				options: absoluteOptions);

			#endregion

			// seed
			var seedConfig = new StageSeedConfig(stageIdentity, voxelAbsolutePhase, contourPhase);
			var seedOptions = new StageSeedOptions()
			{
				PostGeneration = new AggregateAsyncCompletable(voxelChunkCache),
			};
			var stageSeed = new StageSeed(seedConfig, seedOptions);
			stageSeed.Progress.Sample(TimeSpan.FromSeconds(.5)).Subscribe(progress => Console.WriteLine(progress));

			// generator
			var generatorOptions = new GenerationOptions()
			{
			};
			var stageGenerator = new StageGenerator(generatorOptions);

			await stageGenerator.GenerateStageAsync(stageSeed).DontMarshallContext();
			await stageGenerator.CompleteAndAwaitAsync().DontMarshallContext();
		}
	}
}
