Imports MySql.Data.MySqlClient
Public Class frmReceiptVoucher
    Dim ad As MySqlDataAdapter
    Dim t As New DataTable
    Dim scAutoComplete3 As New AutoCompleteStringCollection
    Public WithEvents txt1 As TextBox

    Private Sub frmReceiptVoucher_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If frmSearchPayments.Visible = True Then
            Dim ob As New frmSearchPayments
            Call frmSearchPayments.btnShow_Click(sender, e)
        End If
        Me.Dispose()
    End Sub
    Private Sub frmReceiptVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmSearchPayments.Visible = False Then
            comboload()
            cmbLedger.SelectedIndex = 0
        Else
            comboload()
            cmbLedger.SelectedValue = frmSearchPayments.DataGridView1.CurrentRow.Cells("debitid").Value
        End If
    End Sub
    Private Sub comboload()
        Dim cm As New MySqlCommand
        cm = New MySqlCommand("select Accountname,Accountid from account_group where AccountGroup in(1,2)", con)
        Dim t As New DataTable
        Dim ad As New MySqlDataAdapter(cm)
        t.Clear()
        ad.Fill(t)
        cmbLedger.DataSource = t
        cmbLedger.DisplayMember = "Accountname"
        cmbLedger.ValueMember = "Accountid"
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            Dim cm As New MySqlCommand
            cm.Transaction = tr
            Dim i As Integer
            For i = 0 To DataGridView1.Rows.Count - 1
                If Not DataGridView1.Rows(i).Cells("dgvLedger").Value = Nothing Then
                    cm = New MySqlCommand("PRO_Voucher", con)
                    cm.CommandType = CommandType.StoredProcedure
                    cm.Parameters.AddWithValue("@PAuto_id", DataGridView1.Rows(i).Cells("dgvAutoId").Value)
                    cm.Parameters.AddWithValue("@PVoucherdate", Format(ddlDate.Value, "yyyy-MM-dd"))
                    cm.Parameters.AddWithValue("@PCreditid", DataGridView1.Rows(i).Cells("dgvAccountID").Value)
                    cm.Parameters.AddWithValue("@PDebitid", cmbLedger.SelectedValue)
                    cm.Parameters.AddWithValue("@PAmount", DataGridView1.Rows(i).Cells("dgvAmount").Value)
                cm.Parameters.AddWithValue("@Prefno", txtRefno.Text)
                cm.Parameters.AddWithValue("@PNarration", txtNarration.Text)
                cm.Parameters.AddWithValue("@PVtype", 2)
                cm.ExecuteNonQuery()
                End If
            Next
            tr.Commit()
            MessageBox.Show("Record Saved")
            clear()
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        Try
            If DataGridView1.CurrentCell.ColumnIndex = 0 Then
                If TypeOf e.Control Is TextBox Then
                    txt1 = DirectCast(e.Control, TextBox)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txt1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt1.TextChanged
        If DataGridView1.CurrentCell.ColumnIndex = 0 Then
            If txt1.Text.Length = 1 Then
                frmLedgerSearch.txtSearch.Text = txt1.Text
                frmLedgerSearch.txtSearch.Select(1, 1)
                frmLedgerSearch.ShowDialog()
            End If
        End If
    End Sub
    Private Sub clear()
        txtNarration.Text = ""
        txtRefno.Text = ""
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clear()
    End Sub

    Private Sub frmReceiptVoucher_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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