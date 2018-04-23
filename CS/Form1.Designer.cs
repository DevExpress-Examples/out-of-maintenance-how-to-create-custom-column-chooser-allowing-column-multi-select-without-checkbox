namespace MultiSelectColumnCustomization
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gridControlOverride1 = new MultiSelectColumnCustomization.GridControlOverride();
            this.gridViewOverride1 = new MultiSelectColumnCustomization.GridViewOverride();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOverride1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOverride1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlOverride1
            // 
            this.gridControlOverride1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOverride1.Location = new System.Drawing.Point(0, 0);
            this.gridControlOverride1.MainView = this.gridViewOverride1;
            this.gridControlOverride1.Name = "gridControlOverride1";
            this.gridControlOverride1.Size = new System.Drawing.Size(537, 357);
            this.gridControlOverride1.TabIndex = 0;
            this.gridControlOverride1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOverride1});
            // 
            // gridViewOverride1
            // 
            this.gridViewOverride1.GridControl = this.gridControlOverride1;
            this.gridViewOverride1.Name = "gridViewOverride1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 357);
            this.Controls.Add(this.gridControlOverride1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOverride1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOverride1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private GridControlOverride gridControlOverride1;
		private GridViewOverride gridViewOverride1;
	}
}

