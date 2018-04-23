using System;
using System.Windows.Forms;
using System.Data;

namespace MultiSelectColumnCustomization
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
			//this.carsTableAdapter.Fill(this.carsDBDataSet.Cars);
			gridControlOverride1.DataSource = CreateTable(30);
		}

		private DataTable CreateTable(int RowCount)
		{
			DataTable tbl = new DataTable();
			tbl.Columns.Add("ID", typeof(int));
			tbl.Columns.Add("Vendor", typeof(string));
			tbl.Columns.Add("Model", typeof(string));
			tbl.Columns.Add("ProductNumber", typeof(int));


			for ( int i = 0; i < RowCount; i++ )
			{
				int rnd = new Random(i).Next(1, 4);
				tbl.Rows.Add(new object[] { i, String.Format("Vendor {0}", rnd), String.Format("Model {0}", i), i * new Random().Next(1, 10000) });
			}

			return tbl;
		}

	}
}
