using BaseLibrary;
using BaseLibrary.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GeneticEngineering.Items
{
	public class Syringe : BaseItem
	{
		public override bool CloneNewInstances => false;

		public int? npcType;

		public override ModItem Clone()
		{
			Syringe clone = (Syringe)base.Clone();
			clone.npcType = npcType;
			return clone;
		}

		public override ModItem Clone(Item item)
		{
			var clone = Clone();
			clone.SetValue("item", item);
			return clone;
		}

		public override ModItem NewInstance(Item itemClone)
		{
			ModItem copy = (ModItem)Activator.CreateInstance(GetType());
			copy.SetValue("item", itemClone);
			copy.SetValue("mod", mod);
			copy.SetValue("Name", Name);
			copy.SetValue("DisplayName", DisplayName);
			copy.SetValue("Tooltip", Tooltip);
			return copy;
		}

		public override void SetDefaults()
		{
			item.damage = 2;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.knockBack = 1;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.crit = 6;
			item.useStyle = ItemUseStyleID.Stabbing;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (npcType == null) npcType = target.type;

			Main.NewText(npcType.Value);
		}

		public override bool CanRightClick() => true;

		public override void RightClick(Player player)
		{
			item.stack++;

			npcType = null;
		}

		public override TagCompound Save() => npcType != null ? new TagCompound { ["NPC"] = npcType.Value } : new TagCompound();

		public override void Load(TagCompound tag)
		{
			if (tag.ContainsKey("NPC")) npcType = tag.GetInt("NPC");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Stinger);
			recipe.AddIngredient(ItemID.Bottle);
			recipe.AddIngredient(ItemID.Wood);
			recipe.anyWood = true;
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}