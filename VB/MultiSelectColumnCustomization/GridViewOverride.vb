Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors.Customization
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.Customization

Namespace MultiSelectColumnCustomization
	Public Class GridViewOverride
		Inherits GridView
		Public Sub New(ByVal ownerGrid As GridControl)
			MyBase.New(ownerGrid)
		End Sub
		Public Sub New()
			AddHandler CustomDrawColumnHeader, AddressOf OnCustomDrawColumnHeader
		End Sub

		Protected Overrides Function CreateCustomizationForm() As CustomizationForm
			Return New MultiSelectCustomizationForm(Me)
		End Function

		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "GridViewOverride"
			End Get
		End Property

		Private Sub OnCustomDrawColumnHeader(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs)
			If e.Column Is Nothing Then
				Return
			End If
			Select Case Convert.ToInt32(e.Column.Tag)
				Case 1
					e.Info.State = DevExpress.Utils.Drawing.ObjectState.Hot
				Case 2
					e.Info.State = DevExpress.Utils.Drawing.ObjectState.Pressed
			End Select
		End Sub
	End Class
End Namespace