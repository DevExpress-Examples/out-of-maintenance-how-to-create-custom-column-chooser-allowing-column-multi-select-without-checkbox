using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.Customization;

namespace MultiSelectColumnCustomization
{
    public class MultiSelectColumnCustomizationListBox : ColumnCustomizationListBox
    {
        private int pushedIndex = -1;
        readonly List<object> checkedItems = new List<object>();
        object focusedItem = null;

        public MultiSelectColumnCustomizationListBox(CustomizationForm form)
            : base(form)
        {
        }

        protected override void DrawItemObject(GraphicsCache cache, int index, Rectangle bounds, DrawItemState itemState)
        {

            if (index == pushedIndex)
                ((GridColumn)Items[index]).Tag = 2;
            else
                ((GridColumn)Items[index]).Tag = 0;

            if (checkedItems.Contains(Items[index]))
                ((GridColumn)Items[index]).Tag = 1;

            base.DrawItemObject(cache, index, bounds, itemState);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);
            object pointedItem = ItemByPoint(new Point(e.X, e.Y));
            int itemIndex = Items.IndexOf(pointedItem);
            Rectangle itemRect = GetItemRectangle(itemIndex);

            if (e.Button == MouseButtons.Left)
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

            if (e.Button == MouseButtons.Left)
            {
                if (ModifierKeys == Keys.Shift)
                {
                    int startIndex = Items.IndexOf(focusedItem);
                    int endIndex = Items.IndexOf(pointedItem);
                    bool check = !checkedItems.Contains(pointedItem);

                    if (endIndex >= startIndex)
                        for (int i = startIndex; i <= endIndex; i++)
                        {
                            if (check && !checkedItems.Contains(Items[i]))
                                checkedItems.Add(Items[i]);
                            else if (!check && checkedItems.Contains(Items[i]))
                                checkedItems.Remove(Items[i]);
                        }
                    else
                        for (int i = endIndex; i < startIndex; i++)
                        {
                            if (check && !checkedItems.Contains(Items[i]))
                                checkedItems.Add(Items[i]);
                            else if (!check && checkedItems.Contains(Items[i]))
                                checkedItems.Remove(Items[i]);
                        }
                }
                else if (ModifierKeys == Keys.None)
                {
                    if (checkedItems.Contains(pointedItem))
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
