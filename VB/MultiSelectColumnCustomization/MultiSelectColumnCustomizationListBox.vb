Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid.Customization

Namespace MultiSelectColumnCustomization
	Public Class MultiSelectColumnCustomizationListBox
		Inherits ColumnCustomizationListBox
		Private pushedIndex As Integer = -1
		Private ReadOnly checkedItems_Renamed As New List(Of Object)()
		Private focusedItem As Object = Nothing

		Public Sub New(ByVal form As CustomizationForm)
			MyBase.New(form)
		End Sub

		Protected Overrides Sub DrawItemObject(ByVal cache As GraphicsCache, ByVal index As Integer, ByVal bounds As Rectangle, ByVal itemState As DrawItemState)

			If index = pushedIndex Then
				CType(Items(index), GridColumn).Tag = 2
			Else
				CType(Items(index), GridColumn).Tag = 0
			End If

			If checkedItems_Renamed.Contains(Items(index)) Then
				CType(Items(index), GridColumn).Tag = 1
			End If

			MyBase.DrawItemObject(cache, index, bounds, itemState)
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
