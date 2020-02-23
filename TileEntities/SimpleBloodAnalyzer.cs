using BaseLibrary.Tiles.TileEntites;
using BaseLibrary.UI;
using ContainerLibrary;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ModLoader.IO;

namespace GeneticEngineering.TileEntities
{
	public class SimpleBloodAnalyzer : BaseTE, IHasUI, IItemHandler
	{
		public override Type TileType => typeof(Tiles.SimpleBloodAnalyzer);
		public Guid UUID { get; set; }
		public BaseUIPanel UI { get; set; }
		public LegacySoundStyle CloseSound { get; }
		public LegacySoundStyle OpenSound { get; }
		public ItemHandler Handler { get; }

		public int? npcType;

		public SimpleBloodAnalyzer()
		{
			UUID = Guid.NewGuid();
			Handler = new ItemHandler(27);
			npcType = null;
		}

		public override void OnKill()
		{
			Handler.DropItems(new Rectangle(Position.X * 16, Position.Y * 16, 32, 32));
		}

		public override TagCompound Save()
		{
			TagCompound save = new TagCompound
			{
				["UUID"] = UUID,
				["Items"] = Handler.Save()
			};

			if (npcType != null) save["NPC"] = npcType.Value;

			return save;
		}

		public override void Load(TagCompound tag)
		{
			UUID = tag.Get<Guid>("UUID");
			Handler.Load(tag.GetCompound("Items"));
			if (tag.ContainsKey("NPC")) npcType = tag.GetInt("NPC");
		}
	}
}