Imports MySql.Data.MySqlClient

Public Class frmLedger

    Private Sub frmLedger_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If frmLedgerEdit.Visible = True Then
            Call frmLedgerEdit.txtSearch_TextChanged(sender, e)
        End If
        Me.Dispose()
    End Sub
    Private Sub frmLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim t1 As New DataTable
        t1.Clear()
        Dim ad As MySqlDataAdapter
        ad = New MySqlDataAdapter("select Account_id,Accountgroup from account", con)
        ad.Fill(t1)
        cmbAccount.DataSource = t1
        cmbAccount.DisplayMember = "Accountgroup"
        cmbAccount.ValueMember = "Account_id"
        If frmLedgerEdit.Visible = True Then
            cmbAccount.SelectedValue = frmLedgerEdit.DataGridView1.CurrentRow.Cells("Accountgroup").Value
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim cm As MySqlCommand
        cm = New MySqlCommand("PRO_ACCOUNTGROUP", con)
        cm.CommandType = CommandType.StoredProcedure
        If txtAccountId.Text.Length = 0 Then
            cm.Parameters.AddWithValue("PAccountid", DBNull.Value)
        Else
            cm.Parameters.AddWithValue("PAccountid", txtAccountId.Text)
        End If

        cm.Parameters.AddWithValue("PAccountgroup", cmbAccount.SelectedValue)
        cm.Parameters.AddWithValue("PAccountname", txtName.Text)
        If RadioButton1.Checked = True Then
            cm.Parameters.AddWithValue("POpbalance", -(txtBal.Text))
        Else
            cm.Parameters.AddWithValue("POpbalance", txtBal.Text)
        End If
        cm.ExecuteNonQuery()
        clear()
    End Sub
    Private Sub clear()
        txtAccountId.Text = ""
        cmbAccount.Text = ""
        txtName.Text = ""
    End Sub

    Private Sub frmLedger_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.BackwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub frmLedger_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
      
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class