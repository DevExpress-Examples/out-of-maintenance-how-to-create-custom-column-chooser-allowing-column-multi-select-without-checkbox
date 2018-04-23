Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Base

Namespace MultiSelectColumnCustomization
	Public Class GridInfoRegistratorOverride
		Inherits GridInfoRegistrator
		Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
			Return New GridViewOverride(TryCast(grid, GridControl))
		End Function

		Public Overrides ReadOnly Property ViewName() As String
			Get
				Return "GridViewOverride"
			End Get
		End Property
	End Class
End Namespace
