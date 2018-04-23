using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Customization;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Customization;

namespace MultiSelectColumnCustomization
{
    public class MultiSelectCustomizationForm : CustomizationForm
    {
        public MultiSelectCustomizationForm(GridView view)
            : base(view)
        {
        }

        protected override CustomizationListBoxBase CreateCustomizationListBox()
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

            bAddCheckedCols.Click += OnButtonAddCheckedColumns_Click;
        }

        private void OnButtonAddCheckedColumns_Click(object sender, EventArgs e)
        {
            MultiSelectColumnCustomizationListBox listBox = (MultiSelectColumnCustomizationListBox)ActiveListBox;
            for (int i = listBox.CheckedItems.Count - 1; i >= 0; i--)
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
}
