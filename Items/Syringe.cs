using BaseLibrary;
using BaseLibrary.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GeneticEngineering.Items
{
	public class Syringe : BaseItem
	{
		public override bool CloneNewInstances => false;

		public override string Texture => "GeneticEngineering/Textures/Items/Syringe";

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

		public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(60, 2));
		}

		public override void SetDefaults()
		{
			item.damage = 2;
			item.melee = true;
			item.width = 28;
			item.height = 28;
			item.useTime = 20;
			item.useAnimation = 20;
			item.knockBack = 1;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.crit = 6;
			item.useStyle = ItemUseStyleID.Stabbing;
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D texture = ModContent.GetTexture(Texture);

			spriteBatch.Draw(texture, position, npcType == null ? new Rectangle(0, 0, 28, 28) : new Rectangle(0, 28, 28, 28), drawColor, 0f, origin, scale, SpriteEffects.None, 0f);

			return false;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (npcType != null) tooltips.Add(new TooltipLine(mod, "BloodType", $"Currently holding the blood of {Utility.Cache.NPCCache[npcType.Value].GivenOrTypeName}"));
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (npcType == null) npcType = target.type;
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