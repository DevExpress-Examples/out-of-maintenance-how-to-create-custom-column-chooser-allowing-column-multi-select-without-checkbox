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
            CustomDrawColumnHeader += new ColumnHeaderCustomDrawEventHandler(OnCustomDrawColumnHeader);
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

	public class MultiSelectCustomizationForm : CustomizationForm
	{
		public MultiSelectCustomizationForm(GridView view)
			: base(view)
		{
		}

		protected override CustomCustomizationListBoxBase CreateCustomizationListBox()
		{
			return new MultiSelectColumnCustomizationListBox(this);
		}

		protected override void CreateListBox()
		{
			base.CreateListBox();

			Panel bottomPanel = new Panel();
			bottomPanel.Parent = this;
			bottomPanel.Dock = DockStyle.Bottom;
			bottomPanel.Height = 30;
			bottomPanel.SendToBack();

			Button bAddCheckedCols = new Button();
			bAddCheckedCols.Parent = bottomPanel;
			bAddCheckedCols.Dock = DockStyle.Fill;
			bAddCheckedCols.Text = "Add checked columns to grid";

			bAddCheckedCols.Click += new EventHandler(OnButtonAddCheckedColumns_Click);
		}

		private void OnButtonAddCheckedColumns_Click(object sender, EventArgs e)
		{
			MultiSelectColumnCustomizationListBox listBox = (MultiSelectColumnCustomizationListBox)ActiveListBox;
			for ( int i = listBox.CheckedItems.Count - 1; i >= 0 ; i-- )
			{
                if (listBox.CheckedItems[i] != null)
                {
                    ((GridColumn)listBox.CheckedItems[i]).Tag = 0;
                    ((GridColumn)listBox.CheckedItems[i]).Visible = true;
                    listBox.CheckedItems.RemoveAt(i);
                    
                }
			}
		}
	}

	public class MultiSelectColumnCustomizationListBox : ColumnCustomizationListBox
	{
		private int pushedIndex = -1;
		List<object> checkedItems = new List<object>();
		object focusedItem = null;

		public MultiSelectColumnCustomizationListBox(CustomizationForm form)
			: base(form)
		{
		}

		protected override void DrawItemObject(GraphicsCache cache, int index, Rectangle bounds)
		{
            ((GridColumn)Items[index]).Tag = 0;            			

            if (index == pushedIndex)
                ((GridColumn)Items[index]).Tag = 2;

            if (checkedItems.Contains(Items[index]))
                ((GridColumn)Items[index]).Tag = 1;

            base.DrawItemObject(cache, index, bounds);                                    
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{            
			Point mousePoint = new Point(e.X, e.Y);
			object pointedItem = ItemByPoint(new Point(e.X, e.Y));
			int itemIndex = Items.IndexOf(pointedItem);
			Rectangle itemRect = GetItemRectangle(itemIndex);			

			if (e.Button == MouseButtons.Left )
			{
				pushedIndex = itemIndex;
				this.InvalidateObject(pointedItem);

				return;
			}			
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			Point mousePoint = new Point(e.X, e.Y);
			object pointedItem = ItemByPoint(new Point(e.X, e.Y));
			int itemIndex = Items.IndexOf(pointedItem);
			Rectangle itemRect = GetItemRectangle(itemIndex);			

			if ( e.Button == MouseButtons.Left )
			{
				if ( ModifierKeys == Keys.Shift )
				{
					int startIndex = Items.IndexOf(focusedItem);
					int endIndex = Items.IndexOf(pointedItem);
					bool check = !checkedItems.Contains(pointedItem);

					if ( endIndex >= startIndex )
						for ( int i = startIndex; i <= endIndex; i++ )
						{
							if ( check && !checkedItems.Contains(Items[i]) )
								checkedItems.Add(Items[i]);
							else if ( !check && checkedItems.Contains(Items[i]) )
								checkedItems.Remove(Items[i]);
						}
					else
						for ( int i = endIndex; i < startIndex; i++ )
						{
							if ( check && !checkedItems.Contains(Items[i]) )
								checkedItems.Add(Items[i]);
							else if ( !check && checkedItems.Contains(Items[i]) )
								checkedItems.Remove(Items[i]);
						}
				}
				else if ( ModifierKeys == Keys.None )
				{
					if ( checkedItems.Contains(pointedItem) )
						checkedItems.Remove(pointedItem);
					else
						checkedItems.Add(pointedItem);
				}

				focusedItem = pointedItem;
				pushedIndex = -1;
				this.InvalidateObject(pointedItem);

				return;
			}
		}
		
		public List<object> CheckedItems
		{
			get
			{
				return checkedItems;
			}
		}
	}
}