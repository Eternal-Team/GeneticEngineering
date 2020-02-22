using BaseLibrary.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace GeneticEngineering.Items
{
	public class Syringe : BaseItem
	{
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Stinger);
			recipe.AddIngredient(ItemID.Bottle);
			recipe.AddIngredient(ItemID.Wood);
			recipe.anyWood = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}