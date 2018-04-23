using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;

namespace MultiSelectColumnCustomization
{
	public class GridInfoRegistratorOverride : GridInfoRegistrator
	{
		public override BaseView CreateView(GridControl grid)
		{
			return new GridViewOverride(grid as GridControl);
		}

		public override string ViewName
		{
			get
			{
				return "GridViewOverride";
			}
		}
	}
}
