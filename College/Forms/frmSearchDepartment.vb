Imports MySql.Data.MySqlClient

Public Class frmSearchDepartment

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            dgasn()
        ElseIf e.KeyCode = Keys.Down Then
            datagrid1.Focus()
            If datagrid1.Rows.Count = 1 Then
                datagrid1.CurrentCell = datagrid1(0, 0)
            Else
                datagrid1.CurrentCell = datagrid1(0, 1)
            End If
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        'If txtSearch.Text = "" Then
        'Else
        Dim t As New DataTable
        Dim ad As MySqlDataAdapter
        t.Clear()
        Dim cm As New MySqlCommand
        ad = New MySqlDataAdapter("SELECT Department,DepId from department where Department like '" & Trim(txtSearch.Text) & "%'", con)
        ad.Fill(t)
        datagrid1.DataSource = t
        datagrid1.Columns(1).Visible = False
        'End If
    End Sub

    Private Sub frmSearchDepartment_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmSearchDepartment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        datagrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub datagrid1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles datagrid1.CellMouseDoubleClick
        dgasn()
    End Sub

    Private Sub datagrid1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles datagrid1.KeyDown
        If e.KeyCode = Keys.Enter Then
            dgasn()
        End If
    End Sub

    Private Sub datagrid1_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles datagrid1.RowPostPaint
        If datagrid1.Rows(e.RowIndex).Selected Then
            Using pen As New Pen(Color.Black)
                Dim penWidth As Integer = 2
                pen.Width = penWidth
                Dim x As Integer = e.RowBounds.Left + (penWidth / 3)
                Dim y As Integer = e.RowBounds.Top + (penWidth / 3)
                Dim width As Integer = e.RowBounds.Width - penWidth
                Dim height As Integer = e.RowBounds.Height - penWidth
                e.Graphics.DrawRectangle(pen, x, y, width, height)
            End Using
        End If
    End Sub
    Private Sub dgasn()
        frmCourse.txtdgv.Text = datagrid1.CurrentRow.Cells(0).Value
        frmCourse.datagrid1.CurrentRow.Cells("dgvDepId").Value = datagrid1.CurrentRow.Cells(1).Value
        Dim position As Integer = frmCourse.txtdgv.Text.Length
        frmCourse.txtdgv.Select(position, position)
        Me.Close()
        ' frmCourse.datagrid1.CurrentCell = frmCourse.datagrid1(0, datagrid1.Rows.Count - 1)
    End Sub

    Private Sub datagrid1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datagrid1.CellContentClick

    End Sub

    Private Sub datagrid1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles datagrid1.MouseDoubleClick

    End Sub
End Class