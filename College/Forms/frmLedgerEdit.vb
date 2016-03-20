Imports MySql.Data.MySqlClient
Public Class frmLedgerEdit
    Public Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim cm As MySqlCommand
        cm = New MySqlCommand("SELECT AccountId,AccountGroup, Accountname, if(Opbalance<0,-1*Opbalance,opbalance) AS OpeningBalance,if(Opbalance<0,'Cr','Dr') as Type FROM account_group where Accountname like '" & txtSearch.Text & "%'", con)
        cm.CommandTimeout = 8000
        Dim ad As MySqlDataAdapter
        Dim t As New DataTable
        t.Clear()
        ad = New MySqlDataAdapter(cm)
        ad.Fill(t)
        DataGridView1.DataSource = t
        DataGridView1.Columns("AccountId").Visible = False
        DataGridView1.Columns("AccountGroup").Visible = False
        DataGridView1.Columns("OpeningBalance").FillWeight = 40
        DataGridView1.Columns("OpeningBalance").HeaderText = "Opening Balance"
        DataGridView1.Columns("Accountname").HeaderText = "Account Name"
        DataGridView1.Columns("Type").FillWeight = 20
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        assign()
    End Sub
    Private Sub DataGridView1_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        If DataGridView1.Rows(e.RowIndex).Selected Then
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
    Private Sub frmLedgerEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub
    Private Sub assign()
        frmLedger.cmbAccount.SelectedValue = DataGridView1.CurrentRow.Cells("AccountGroup").Value
        If DataGridView1.CurrentRow.Cells("Type").Value = "Cr" Then
            frmLedger.RadioButton1.Checked = True
        Else
            frmLedger.RadioButton2.Checked = True
        End If
        frmLedger.txtBal.Text = DataGridView1.CurrentRow.Cells("Openingbalance").Value
        frmLedger.txtName.Text = DataGridView1.CurrentRow.Cells("Accountname").Value
        frmLedger.txtAccountId.Text = DataGridView1.CurrentRow.Cells("AccountId").Value
        frmLedger.txtName.Select(frmLedger.txtName.Text.Length, frmLedger.txtName.Text.Length)
        frmLedger.ShowDialog()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class