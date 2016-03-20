Imports MySql.Data.MySqlClient
Public Class frmFeesIdSearch
    Dim ad As MySqlDataAdapter
    Dim t As New DataTable
    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim FeesId As String = datagrid1.CurrentRow.Cells(2).Value
            If frmFeesStructure.Visible = True Then
                frmFeesStructure.lblFeesId.Text = FeesId
                frmFeesStructure.txtSearchFees.Text = datagrid1.CurrentRow.Cells(1).Value
                Dim position As Integer = frmFeesStructure.txtSearchFees.Text.Length
                frmFeesStructure.txtSearchFees.Select(position, position)
                frmFeesStructure.txtSearchFees.Focus()
                Me.Close()
            ElseIf frmStudent.Visible = True Then
                student()
                Me.Close()
            End If
        ElseIf e.KeyCode = Keys.Down Then
            datagrid1.Focus()
            If datagrid1.Rows.Count > 1 Then
                datagrid1.CurrentCell = datagrid1(0, 1)
            Else
                datagrid1.CurrentCell = datagrid1(0, 0)
            End If
        End If
    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text = "" Then
        Else
            t.Clear()
            Dim cm As New MySqlCommand
            ad = New MySqlDataAdapter("SELECT feestruct.Batch as Fees_Structure, course.Course,feestruct.Feesid FROM course INNER JOIN feestruct ON course.Courseid = feestruct.Courseid where course.Course like '" & Trim(txtSearch.Text) & "%'", con)
            ad.Fill(t)
            datagrid1.DataSource = t
            datagrid1.Columns("Feesid").Visible = False
        End If
    End Sub

    Private Sub datagrid1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles datagrid1.CellMouseDoubleClick
        Dim FeesId As String = datagrid1.CurrentRow.Cells(2).Value
        If frmFeesStructure.Visible = True Then
            frmFeesStructure.lblFeesId.Text = FeesId
            frmFeesStructure.txtSearchFees.Text = datagrid1.CurrentRow.Cells(1).Value
            Dim position As Integer = frmFeesStructure.txtSearchFees.Text.Length
            frmFeesStructure.txtSearchFees.Select(position, position)
            frmFeesStructure.txtSearchFees.Focus()
            Me.Close()
        ElseIf frmStudent.Visible = True Then
            student()
            Me.Close()
        End If

    End Sub

    Private Sub datagrid1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles datagrid1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If frmStudent.Visible = True Then
                student()
                Me.Close()
            ElseIf frmFeesStructure.Visible = True Then
                frmFeesStructure.lblFeesId.Text = datagrid1.CurrentRow.Cells(2).Value
                frmFeesStructure.txtSearchFees.Text = datagrid1.CurrentRow.Cells(1).Value
                Dim position As Integer = frmFeesStructure.txtSearchFees.Text.Length
                frmFeesStructure.txtSearchFees.Select(position, position)
                frmFeesStructure.txtSearchFees.Focus()
                Me.Close()
            End If

        End If
    End Sub
    Private Sub dgvSearchProducts_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles datagrid1.RowPostPaint
        If datagrid1.Rows(e.RowIndex).Selected Then
            Using pen As New Pen(Color.Green)
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
    'Private Sub Fees()
    '    If t.Rows.Count > 0 Then
    '        Dim FeesId As String = datagrid1.CurrentRow.Cells(2).Value
    '        frmFeesStructure.txtFeesId.Text = FeesId
    '        txtln()
    '        Me.Dispose()
    '    Else
    '        frmFeesStructure.txtFeesId.Text = ""
    '    End If
    'End Sub
    Private Sub frmFeesIdSearch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmFeesIdSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        datagrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub
    'Private Sub txtln()
    '    Dim position As Integer = frmFeesStructure.lblFeesId.Text.Length
    '    frmFeesStructure.lblFeesId.Select(position, position)
    '    frmFeesStructure.lblFeesId.Focus()
    'End Sub
    Private Sub student()
        frmStudent.txtFeesId.Text = datagrid1.CurrentRow.Cells(2).Value
        frmStudent.txtSearchFees.Text = datagrid1.CurrentRow.Cells(1).Value
        frmStudent.txtFeesId.Focus()
    End Sub

    Private Sub datagrid1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datagrid1.CellContentClick

    End Sub

    Private Sub frmFeesIdSearch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub
End Class