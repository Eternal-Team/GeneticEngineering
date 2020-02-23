using BaseLibrary;
using BaseLibrary.Tiles;
using BaseLibrary.UI;
using GeneticEngineering.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GeneticEngineering.Tiles
{
	public class SimpleBloodAnalyzer : BaseTile
	{
		public override string Texture => "GeneticEngineering/Textures/Tiles/SimpleBloodAnalyzer";

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			TileEntities.SimpleBloodAnalyzer analyzer = Utility.GetTileEntity<TileEntities.SimpleBloodAnalyzer>(i, j);
			PanelUI.Instance.CloseUI(analyzer);

			Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<Items.SimpleBloodAnalyzer>());
			analyzer.Kill(i, j);
		}

		public override bool NewRightClick(int i, int j)
		{
			if (Main.LocalPlayer.HeldItem.modItem is Syringe syringe && syringe.npcType != null)
			{
				TileEntities.SimpleBloodAnalyzer analyzer = Utility.GetTileEntity<TileEntities.SimpleBloodAnalyzer>(i, j);

				if (analyzer.npcType == null)
				{
					analyzer.npcType = syringe.npcType;
					syringe.npcType = null;
				}
			}

			return true;
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			Main.specX[nextSpecialDrawIndex] = i;
			Main.specY[nextSpecialDrawIndex] = j;
			nextSpecialDrawIndex++;
		}

		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			TileEntities.SimpleBloodAnalyzer analyzer = Utility.GetTileEntity<TileEntities.SimpleBloodAnalyzer>(i, j);
			if (analyzer?.npcType == null || !Main.tile[i, j].IsTopLeft()) return;

			Vector2 position = new Point16(i, j).ToScreenCoordinates();

			spriteBatch.Draw(Main.magicPixel, new Rectangle((int)(position.X + 18), (int)(position.Y + 2), 12, 28), new Color(145, 7, 0));
		}

		public override void SetDefaults()
		{
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidBottom, 0, 0);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TileEntities.SimpleBloodAnalyzer>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			disableSmartCursor = true;

			ModTranslation name = CreateMapEntryName();
			AddMapEntry(Color.Brown, name);
		}
	}
}