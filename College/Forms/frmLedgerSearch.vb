Imports MySql.Data.MySqlClient

Public Class frmLedgerSearch

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            assign()
        ElseIf e.KeyCode = Keys.Down Then
            datagrid1.Focus()
            If datagrid1.Rows.Count = 1 Then
                datagrid1.CurrentCell = datagrid1(1, 0)
            Else
                datagrid1.CurrentCell = datagrid1(1, 1)
            End If
        End If
    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If frmPayVoucher.Visible = True Then
            Dim t As New DataTable
            Dim ad As MySqlDataAdapter
            t.Clear()
            Dim cm As New MySqlCommand
            ad = New MySqlDataAdapter("SELECT Accountid,Accountname from account_group where AccountGroup not in(1,2,3) and Accountname like '" & Trim(txtSearch.Text) & "%'", con)
            ad.Fill(t)
            datagrid1.DataSource = t
            datagrid1.Columns(0).Visible = False
        ElseIf frmReceiptVoucher.Visible = True Then
            Dim t As New DataTable
            Dim ad As MySqlDataAdapter
            t.Clear()
            Dim cm As New MySqlCommand
            ad = New MySqlDataAdapter("SELECT Accountid,Accountname from account_group where AccountGroup not in(1,2,4) and Accountname like '" & Trim(txtSearch.Text) & "%'", con)
            ad.Fill(t)
            datagrid1.DataSource = t
            datagrid1.Columns(0).Visible = False
        ElseIf frmContraVoucher.Visible = True Then
            Dim t As New DataTable
            Dim ad As MySqlDataAdapter
            t.Clear()
            Dim cm As New MySqlCommand
            ad = New MySqlDataAdapter("SELECT Accountid,Accountname from account_group where AccountGroup in(1,2) and Accountname like '" & Trim(txtSearch.Text) & "%'", con)
            ad.Fill(t)
            datagrid1.DataSource = t
            datagrid1.Columns(0).Visible = False
        End If
    End Sub
    Private Sub assign()
        Try
            If frmPayVoucher.Visible = True Then
                frmPayVoucher.txt1.Text = datagrid1.CurrentRow.Cells("Accountname").Value
                frmPayVoucher.DataGridView1.CurrentRow.Cells(2).Value = datagrid1.CurrentRow.Cells("Accountid").Value
                Me.Close()
            ElseIf frmReceiptVoucher.Visible = True Then
                frmReceiptVoucher.txt1.Text = datagrid1.CurrentRow.Cells("Accountname").Value
                frmReceiptVoucher.DataGridView1.CurrentRow.Cells(2).Value = datagrid1.CurrentRow.Cells("Accountid").Value
                Me.Close()
            ElseIf frmContraVoucher.Visible = True Then
                frmContraVoucher.txt1.Text = datagrid1.CurrentRow.Cells("Accountname").Value
                frmContraVoucher.DataGridView1.CurrentRow.Cells(2).Value = datagrid1.CurrentRow.Cells("Accountid").Value
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub frmLedgerSearch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.Black,
                                      Color.SteelBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub datagrid1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles datagrid1.KeyDown
        If e.KeyCode = Keys.Enter Then
            assign()
        End If
    End Sub


    Private Sub datagrid1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles datagrid1.MouseDoubleClick
        assign()
    End Sub
End Class