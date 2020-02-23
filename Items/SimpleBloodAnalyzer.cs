using BaseLibrary.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace GeneticEngineering.Items
{
	public class SimpleBloodAnalyzer : BaseItem
	{
		public override string Texture => "Terraria/Item_" + ItemID.AlchemyTable;

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = ModContent.TileType<Tiles.SimpleBloodAnalyzer>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenTable);
			recipe.AddIngredient(ItemID.Bottle, 4);
			recipe.AddIngredient(ItemID.IronBar, 6);
			recipe.anyIronBar = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}