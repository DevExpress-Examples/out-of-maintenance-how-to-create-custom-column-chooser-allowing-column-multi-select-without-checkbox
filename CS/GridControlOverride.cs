using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;

namespace MultiSelectColumnCustomization
{
	public class GridControlOverride : GridControl
	{
		public GridControlOverride()
			: base()
		{
		}
		protected override BaseView CreateDefaultView()
		{
			return CreateView("GridViewOverride");
		}
		protected override void RegisterAvailableViewsCore(InfoCollection collection)
		{
			base.RegisterAvailableViewsCore(collection);
			collection.Add(new GridInfoRegistratorOverride());
		}
	}

}
