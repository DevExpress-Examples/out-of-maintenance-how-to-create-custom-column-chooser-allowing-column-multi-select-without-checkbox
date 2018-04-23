Imports Microsoft.VisualBasic
Imports System
Namespace MultiSelectColumnCustomization
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControlOverride1 = New MultiSelectColumnCustomization.GridControlOverride()
			Me.gridViewOverride1 = New MultiSelectColumnCustomization.GridViewOverride()
			CType(Me.gridControlOverride1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridViewOverride1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControlOverride1
			' 
			Me.gridControlOverride1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControlOverride1.Location = New System.Drawing.Point(0, 0)
			Me.gridControlOverride1.MainView = Me.gridViewOverride1
			Me.gridControlOverride1.Name = "gridControlOverride1"
			Me.gridControlOverride1.Size = New System.Drawing.Size(537, 357)
			Me.gridControlOverride1.TabIndex = 0
			Me.gridControlOverride1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridViewOverride1})
			' 
			' gridViewOverride1
			' 
			Me.gridViewOverride1.GridControl = Me.gridControlOverride1
			Me.gridViewOverride1.Name = "gridViewOverride1"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(537, 357)
			Me.Controls.Add(Me.gridControlOverride1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControlOverride1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridViewOverride1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private gridControlOverride1 As GridControlOverride
		Private gridViewOverride1 As GridViewOverride
	End Class
End Namespace

