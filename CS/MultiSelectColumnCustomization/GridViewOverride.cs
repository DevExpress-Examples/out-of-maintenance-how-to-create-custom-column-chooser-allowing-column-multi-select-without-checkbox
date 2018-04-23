using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Customization;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Customization;

namespace MultiSelectColumnCustomization
{
	public class GridViewOverride : GridView
	{
		public GridViewOverride(GridControl ownerGrid)
			: base(ownerGrid)
		{
		}
		public GridViewOverride()
		{
            CustomDrawColumnHeader += OnCustomDrawColumnHeader;
		}

		protected override CustomizationForm CreateCustomizationForm()
		{
			return new MultiSelectCustomizationForm(this);
		}

		protected override string ViewName
		{
			get
			{
				return "GridViewOverride";
			}
		}

        private void OnCustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null)
                return;
            switch (Convert.ToInt32(e.Column.Tag))
            {
                case 1: e.Info.State = DevExpress.Utils.Drawing.ObjectState.Hot;
                    break;
                case 2: e.Info.State = DevExpress.Utils.Drawing.ObjectState.Pressed;
                    break;
            }
        }
	}
}