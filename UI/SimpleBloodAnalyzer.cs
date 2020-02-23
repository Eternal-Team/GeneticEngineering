using BaseLibrary.UI;
using ContainerLibrary;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace GeneticEngineering.UI
{
	public class SimpleBloodAnalyzer : BaseUIPanel<TileEntities.SimpleBloodAnalyzer>, IItemHandlerUI
	{
		public ItemHandler Handler => Container.Handler;
		public string GetTexture(Item item) => "Terraria/Item_" + ItemID.AlchemyTable;

		public SimpleBloodAnalyzer(TileEntities.SimpleBloodAnalyzer analyzer) : base(analyzer)
		{
			Width.Pixels = 408;
			Height.Pixels = 172;

			UITextButton buttonClose = new UITextButton("X")
			{
				Size = new Vector2(20),
				X = { Percent = 100 },
				Padding = Padding.Zero,
				RenderPanel = false
			};
			buttonClose.OnClick += args => PanelUI.Instance.CloseUI(Container);
			Add(buttonClose);

			UIText textLabel = new UIText("Simple Blood Analyzer")
			{
				Width = { Percent = 100 },
				Height = { Pixels = 20 },
				HorizontalAlignment = HorizontalAlignment.Center
			};
			Add(textLabel);
		}
	}
}