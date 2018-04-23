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

	Public Class MultiSelectCustomizationForm
		Inherits CustomizationForm
		Public Sub New(ByVal view As GridView)
			MyBase.New(view)
		End Sub

		Protected Overrides Function CreateCustomizationListBox() As CustomCustomizationListBoxBase
			Return New MultiSelectColumnCustomizationListBox(Me)
		End Function

		Protected Overrides Sub CreateListBox()
			MyBase.CreateListBox()

			Dim bottomPanel As New Panel()
			bottomPanel.Parent = Me
			bottomPanel.Dock = DockStyle.Bottom
			bottomPanel.Height = 30
			bottomPanel.SendToBack()

			Dim bAddCheckedCols As New Button()
			bAddCheckedCols.Parent = bottomPanel
			bAddCheckedCols.Dock = DockStyle.Fill
			bAddCheckedCols.Text = "Add checked columns to grid"

			AddHandler bAddCheckedCols.Click, AddressOf OnButtonAddCheckedColumns_Click
		End Sub

		Private Sub OnButtonAddCheckedColumns_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim listBox As MultiSelectColumnCustomizationListBox = CType(ActiveListBox, MultiSelectColumnCustomizationListBox)
			For i As Integer = listBox.CheckedItems.Count - 1 To 0 Step -1
				If listBox.CheckedItems(i) IsNot Nothing Then
					CType(listBox.CheckedItems(i), GridColumn).Tag = 0
					CType(listBox.CheckedItems(i), GridColumn).Visible = True
					listBox.CheckedItems.RemoveAt(i)

				End If
			Next i
		End Sub
	End Class

	Public Class MultiSelectColumnCustomizationListBox
		Inherits ColumnCustomizationListBox
		Private pushedIndex As Integer = -1
		Private checkedItems_Renamed As New List(Of Object)()
		Private focusedItem As Object = Nothing

		Public Sub New(ByVal form As CustomizationForm)
			MyBase.New(form)
		End Sub

		Protected Overrides Sub DrawItemObject(ByVal cache As GraphicsCache, ByVal index As Integer, ByVal bounds As Rectangle)
			CType(Items(index), GridColumn).Tag = 0

			If index = pushedIndex Then
				CType(Items(index), GridColumn).Tag = 2
			End If

			If checkedItems_Renamed.Contains(Items(index)) Then
				CType(Items(index), GridColumn).Tag = 1
			End If

			MyBase.DrawItemObject(cache, index, bounds)
		End Sub

		Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
			Dim mousePoint As New Point(e.X, e.Y)
			Dim pointedItem As Object = ItemByPoint(New Point(e.X, e.Y))
			Dim itemIndex As Integer = Items.IndexOf(pointedItem)
			Dim itemRect As Rectangle = GetItemRectangle(itemIndex)

			If e.Button = MouseButtons.Left Then
				pushedIndex = itemIndex
				Me.InvalidateObject(pointedItem)

				Return
			End If
		End Sub

		Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
			Dim mousePoint As New Point(e.X, e.Y)
			Dim pointedItem As Object = ItemByPoint(New Point(e.X, e.Y))
			Dim itemIndex As Integer = Items.IndexOf(pointedItem)
			Dim itemRect As Rectangle = GetItemRectangle(itemIndex)

			If e.Button = MouseButtons.Left Then
				If ModifierKeys = Keys.Shift Then
					Dim startIndex As Integer = Items.IndexOf(focusedItem)
					Dim endIndex As Integer = Items.IndexOf(pointedItem)
					Dim check As Boolean = Not checkedItems_Renamed.Contains(pointedItem)

					If endIndex >= startIndex Then
						For i As Integer = startIndex To endIndex
							If check AndAlso (Not checkedItems_Renamed.Contains(Items(i))) Then
								checkedItems_Renamed.Add(Items(i))
							ElseIf (Not check) AndAlso checkedItems_Renamed.Contains(Items(i)) Then
								checkedItems_Renamed.Remove(Items(i))
							End If
						Next i
					Else
						For i As Integer = endIndex To startIndex - 1
							If check AndAlso (Not checkedItems_Renamed.Contains(Items(i))) Then
								checkedItems_Renamed.Add(Items(i))
							ElseIf (Not check) AndAlso checkedItems_Renamed.Contains(Items(i)) Then
								checkedItems_Renamed.Remove(Items(i))
							End If
						Next i
					End If
				ElseIf ModifierKeys = Keys.None Then
					If checkedItems_Renamed.Contains(pointedItem) Then
						checkedItems_Renamed.Remove(pointedItem)
					Else
						checkedItems_Renamed.Add(pointedItem)
					End If
				End If

				focusedItem = pointedItem
				pushedIndex = -1
				Me.InvalidateObject(pointedItem)

				Return
			End If
		End Sub

		Public ReadOnly Property CheckedItems() As List(Of Object)
			Get
				Return checkedItems_Renamed
			End Get
		End Property
	End Class
End Namespace