Imports MySql.Data.MySqlClient
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports System.Net.Mail

Public Class frmCourse
    Dim cm As MySqlCommand
    Dim ad As MySqlDataAdapter
    Dim scAutoComplete3 As New AutoCompleteStringCollection
    Public WithEvents txtdgv As TextBox

    Private Sub frmCourse_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub frmCourse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillgrid()
        datagrid1.Focus()
        datagrid1.CurrentCell = datagrid1(0, datagrid1.Rows.Count - 1)
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message,
        ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If keyData = Keys.F1 Then
            save()
        ElseIf keyData = Keys.F2 Then
            delete()
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        save()
    End Sub
    Private Sub save()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            Dim i As Integer
            For i = 0 To datagrid1.Rows.Count - 1
                If Not datagrid1.Rows(i).Cells("dgvCourse").Value = Nothing Then
                    cm = New MySqlCommand("PROCOURSE", con)
                    cm.CommandType = CommandType.StoredProcedure
                    cm.Parameters.AddWithValue("@PROCourse", datagrid1.Rows(i).Cells("dgvCourse").Value)
                    cm.Parameters.AddWithValue("@PROdepartmentid", datagrid1.Rows(i).Cells("dgvDepId").Value)
                    cm.Parameters.AddWithValue("@PROCourseid", datagrid1.Rows(i).Cells("dgvCourseId").Value)
                    cm.ExecuteNonQuery()
                End If
            Next
            tr.Commit()
            MessageBox.Show("Record Saved")
            datagrid1.Rows.Clear()
            fillgrid()
            datagrid1.CurrentCell = datagrid1(0, datagrid1.Rows.Count - 1)
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub fillgrid()
        ad = New MySqlDataAdapter("select * from class_view", con)
        Dim t As New DataTable
        ad.Fill(t)
        Dim i As Integer
        For i = 0 To t.Rows.Count - 1
            i = datagrid1.Rows.Add()
            datagrid1.Rows(i).Cells("dgvCourse").Value = t.Rows(i).Item("Course")
            datagrid1.Rows(i).Cells("dgvDepId").Value = t.Rows(i).Item("DepartmentId")
            datagrid1.Rows(i).Cells("dgvCourseId").Value = t.Rows(i).Item("CourseId")
            datagrid1.Rows(i).Cells("dgvDepName").Value = t.Rows(i).Item("Department")
        Next
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        delete()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        '  frmRptCourses.Show()
    End Sub

    Private Sub delete()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            cm = New MySqlCommand("delete from Course where Courseid=@PCourseid", con)
            cm.Parameters.AddWithValue("@PCourseid", datagrid1.CurrentRow.Cells("dgvCourseId").Value)
            cm.ExecuteNonQuery()
            tr.Commit()
            datagrid1.Rows.Clear()
            fillgrid()
            MessageBox.Show("Record Deleted")
            datagrid1.CurrentCell = datagrid1(0, datagrid1.Rows.Count - 1)
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub datagrid1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datagrid1.CellEndEdit
        'datagrid1.CurrentCell = datagrid1(0, datagrid1.Rows.Count - 1)
        Dim t As New DataTable
        If datagrid1.CurrentCell.ColumnIndex = 1 Then
            t.Clear()
            ad = New MySqlDataAdapter("Select Depid From department where Department='" & Trim(datagrid1.CurrentRow.Cells("dgvDepName").Value) & "'", con)
            ad.Fill(t)
            If t.Rows.Count > 0 Then
                datagrid1.CurrentRow.Cells("dgvDepId").Value = t.Rows(0).Item(0)
            Else
                datagrid1.CurrentRow.Cells("dgvDepId").Value = Nothing
                datagrid1.CurrentRow.Cells("dgvCourse").Value = Nothing
                datagrid1.CurrentRow.Cells("dgvDepName").Value = Nothing
            End If
        End If
    End Sub

    Private Sub datagrid1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datagrid1.CellLeave
        datagrid1.Invalidate()
    End Sub

    Private Sub datagrid1_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles datagrid1.CellPainting
        Try
            '   Datagridview1.BeginEdit(True)
            If e.ColumnIndex = Me.datagrid1.CurrentCell.ColumnIndex AndAlso e.RowIndex = Me.datagrid1.CurrentCell.RowIndex Then
                e.Paint(e.CellBounds, DataGridViewPaintParts.All And Not DataGridViewPaintParts.Border)
                Using p As New Pen(Color.Red, 2)
                    Dim rect As Rectangle = e.CellBounds
                    rect.Width -= 1
                    rect.Height -= 1
                    e.Graphics.DrawRectangle(p, rect)
                End Using
                e.Handled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub datagrid1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles datagrid1.EditingControlShowing

        Try
            If datagrid1.CurrentCell.ColumnIndex = 1 Then
                If TypeOf e.Control Is TextBox Then
                    txtdgv = DirectCast(e.Control, TextBox)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'If datagrid1.CurrentCell.ColumnIndex = 1 Then
        '    Dim cmd11 As New MySqlCommand
        '    cmd11 = New MySqlCommand("Select Department From department where Department like '" & txtdgv.Text.Trim & "%'", con)
        '    Dim dr As MySqlDataReader
        '    dr = cmd11.ExecuteReader
        '    Do While dr.Read
        '        scAutoComplete3.Add(dr.Item("Department"))
        '    Loop
        '    dr.Close()
        'End If
        'If datagrid1.CurrentCell.ColumnIndex = 1 AndAlso TypeOf e.Control Is TextBox Then
        '    With DirectCast(e.Control, TextBox)
        '        .AutoCompleteMode = AutoCompleteMode.Suggest
        '        .AutoCompleteSource = AutoCompleteSource.CustomSource
        '        .AutoCompleteCustomSource = scAutoComplete3
        '    End With
        'Else
        '    With DirectCast(e.Control, TextBox)
        '        .AutoCompleteMode = AutoCompleteMode.None
        '        .AutoCompleteSource = AutoCompleteSource.None
        '        .AutoCompleteCustomSource = scAutoComplete3
        '    End With
        'End If
    End Sub

    Private Sub datagrid1_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles datagrid1.RowPostPaint
        'If datagrid1.Rows(e.RowIndex).Selected Then
        '    Using pen As New Pen(Color.Red)
        '        Dim penWidth As Integer = 2
        '        pen.Width = penWidth
        '        Dim x As Integer = e.RowBounds.Left + (penWidth / 3)
        '        Dim y As Integer = e.RowBounds.Top + (penWidth / 3)
        '        Dim width As Integer = e.RowBounds.Width - penWidth
        '        Dim height As Integer = e.RowBounds.Height - penWidth
        '        e.Graphics.DrawRectangle(pen, x, y, width, height)
        '    End Using
        'End If
    End Sub

    'If Datagridview1.CurrentCell.ColumnIndex = 1 Then
    '       If Len(txtdgv.Text.Trim) = 1 Then
    '           frmSearchProducts.txtSearch.Text = txtdgv.Text
    '           frmSearchProducts.txtSearch.Select(frmSearchProducts.txtSearch.Text.Length, 1)
    '           frmSearchProducts.ShowDialog()
    '       End If
    Private Sub datagrid1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datagrid1.CellContentClick

    End Sub

    Private Sub txtdgv_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdgv.TextChanged
        If datagrid1.CurrentCell.ColumnIndex = 1 Then
            If Len(txtdgv.Text.Trim) = 1 Then
                frmSearchDepartment.txtSearch.Text = txtdgv.Text
                frmSearchDepartment.txtSearch.Select(frmSearchDepartment.txtSearch.Text.Length, 1)
                frmSearchDepartment.ShowDialog()
            End If
        End If
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub frmCourse_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.BackwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub
End Class