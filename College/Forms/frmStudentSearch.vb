Imports MySql.Data.MySqlClient

Public Class frmStudentSearch

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            assign()
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
        Dim ad As MySqlDataAdapter
        Dim t As New DataTable
        Dim cm As MySqlCommand
        cm = New MySqlCommand("PROSEARCHSTUDENT", con)
        Dim s As String
        s = txtSearch.Text & "%"
        If txtSearch.Text = "" Then
        Else
            cm.Parameters.AddWithValue("@PStudentname", s)
            cm.CommandType = CommandType.StoredProcedure
            t.Clear()
            ad = New MySqlDataAdapter(cm)
            ad.Fill(t)
            datagrid1.DataSource = t
            datagrid1.Columns("Feesid").Visible = False
        End If

    End Sub

    Private Sub datagrid1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datagrid1.CellDoubleClick
        'If frmStudent.Visible = True Then
        '    frmStudent.txtStudentId.Text = datagrid1.CurrentRow.Cells(0).Value
        '    frmStudent.txtStudentSearch.Text = datagrid1.CurrentRow.Cells(1).Value
        '    txtln()
        '    Me.Close()
        'ElseIf frmStudentFees.Visible = True Then
        '    frmStudentFees.txtStudentId.Text = datagrid1.CurrentRow.Cells(0).Value
        '    frmStudentFees.txtStudentSearch.Text = datagrid1.CurrentRow.Cells(1).Value
        '    Dim p As Integer
        '    p = frmStudentFees.txtStudentSearch.Text.Length
        '    frmStudentFees.txtStudentSearch.Select(p, p)
        '    Me.Close()
        'ElseIf frmDiscounts.Visible = True Then
        '    frmDiscounts.txtStudentId.Text = datagrid1.CurrentRow.Cells(0).Value
        '    frmDiscounts.txtStudentSearch.Text = datagrid1.CurrentRow.Cells(1).Value
        '    Dim p As Integer
        '    p = frmDiscounts.txtStudentSearch.Text.Length
        '    frmDiscounts.txtStudentSearch.Select(p, p)
        '    Me.Close()
        'End If
        assign()
    End Sub

    Private Sub datagrid1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles datagrid1.KeyDown
        If e.KeyCode = Keys.Enter Then
            assign()
        End If
    End Sub

    Private Sub datagrid1_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles datagrid1.RowPostPaint
        If datagrid1.Rows(e.RowIndex).Selected Then
            Using pen As New Pen(Color.Red)
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

    Private Sub frmStudentSearch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmStudentSearch_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        datagrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        datagrid1.Columns(1).Width = 250
    End Sub
    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        frmRptStudentAccount.Show()
    End Sub
    Private Sub assign()
        If frmStudent.Visible = True Then
            frmStudent.txtStudentId.Text = datagrid1.CurrentRow.Cells(0).Value
            frmStudent.txtStudentSearch.Text = datagrid1.CurrentRow.Cells(1).Value
            Dim p As Integer
            p = frmStudent.txtStudentSearch.Text.Length
            frmStudent.txtStudentSearch.Select(p, p)
            Me.Close()
        ElseIf frmStudentFees.Visible = True Then
            frmStudentFees.txtStudentId.Text = datagrid1.CurrentRow.Cells(0).Value
            frmStudentFees.txtStudentSearch.Text = datagrid1.CurrentRow.Cells(1).Value
            Dim p As Integer
            p = frmStudentFees.txtStudentSearch.Text.Length
            frmStudentFees.txtStudentSearch.Select(p, p)
            Me.Close()
        ElseIf frmDiscounts.Visible = True Then
            frmDiscounts.txtStudentId.Text = datagrid1.CurrentRow.Cells(0).Value
            frmDiscounts.txtStudentSearch.Text = datagrid1.CurrentRow.Cells(1).Value
            Dim p As Integer
            p = frmDiscounts.txtStudentSearch.Text.Length
            frmDiscounts.txtStudentSearch.Select(p, p)
            Me.Close()
        ElseIf frmEditStudentAccount.Visible = True Then
            frmEditStudentAccount.txtStudentId.Text = datagrid1.CurrentRow.Cells(0).Value
            frmEditStudentAccount.txtStudentSearch.Text = datagrid1.CurrentRow.Cells(1).Value
            Dim p As Integer
            p = frmEditStudentAccount.txtStudentSearch.Text.Length
            frmEditStudentAccount.txtStudentSearch.Select(p, p)
            Me.Close()
        End If
    End Sub
    Private Sub frmStudentSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmStudentSearch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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