Imports MySql.Data.MySqlClient
Public Class frmSearchPayments
    Public Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        BackgroundWorker1.RunWorkerAsync()
    End Sub
    Public Sub Datafill()
        Try
            Dim cm As MySqlCommand
            cm = New MySqlCommand("AccountSearch", con)
            cm.CommandType = CommandType.StoredProcedure
            cm.CommandTimeout = 8000
            cm.Parameters.AddWithValue("Sdate", Format(ddlFrom.Value, "yyyy-MM-dd"))
            cm.Parameters.AddWithValue("Edate", Format(ddlTo.Value, "yyyy-MM-dd"))
            Dim ad As MySqlDataAdapter
            Dim t As New DataTable
            t.Clear()
            ad = New MySqlDataAdapter(cm)
            ad.Fill(t)
            DataGridView1.DataSource = t
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
     
    End Sub
    Private Sub frmSearchPayments_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub frmSearchPayments_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            CheckForIllegalCrossThreadCalls = False
            PictureBox2.Visible = False
            btnShow.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
     
    End Sub
    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        fill()
    End Sub
    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            fill()
        End If
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
    Private Sub fill()
        Try
            If DataGridView1.CurrentRow.Cells("creditid").Value = 3 Then
                MessageBox.Show("Student Account can not be Edited")
            Else
                If DataGridView1.CurrentRow.Cells("Vtype").Value = 1 Then
                    frmPayVoucher.txtRefno.Text = DataGridView1.CurrentRow.Cells("Refno").Value
                    frmPayVoucher.ddlDate.Value = Format(DataGridView1.CurrentRow.Cells("trans_date").Value, "dd/MMM/yyyy")
                    frmPayVoucher.DataGridView1.Rows(0).Cells(0).Value = DataGridView1.CurrentRow.Cells("DebitAccount").Value
                    frmPayVoucher.DataGridView1.Rows(0).Cells(1).Value = DataGridView1.CurrentRow.Cells("Amount").Value
                    frmPayVoucher.DataGridView1.Rows(0).Cells(2).Value = DataGridView1.CurrentRow.Cells("debitid").Value
                    frmPayVoucher.DataGridView1.Rows(0).Cells(3).Value = DataGridView1.CurrentRow.Cells("Auto_id").Value
                    frmPayVoucher.txtNarration.Text = DataGridView1.CurrentRow.Cells("Narration").Value
                    frmPayVoucher.ShowDialog()
                ElseIf DataGridView1.CurrentRow.Cells("Vtype").Value = 2 Then
                    frmReceiptVoucher.txtRefno.Text = DataGridView1.CurrentRow.Cells("Refno").Value
                    frmReceiptVoucher.ddlDate.Value = Format(DataGridView1.CurrentRow.Cells("trans_date").Value, "dd/MMM/yyyy")
                    frmReceiptVoucher.DataGridView1.Rows(0).Cells(0).Value = DataGridView1.CurrentRow.Cells("CreditAccount").Value
                    frmReceiptVoucher.DataGridView1.Rows(0).Cells(1).Value = DataGridView1.CurrentRow.Cells("Amount").Value
                    frmReceiptVoucher.DataGridView1.Rows(0).Cells(2).Value = DataGridView1.CurrentRow.Cells("Creditid").Value
                    frmReceiptVoucher.DataGridView1.Rows(0).Cells(3).Value = DataGridView1.CurrentRow.Cells("Auto_id").Value
                    frmReceiptVoucher.txtNarration.Text = DataGridView1.CurrentRow.Cells("Narration").Value
                    frmReceiptVoucher.ShowDialog()
                ElseIf DataGridView1.CurrentRow.Cells("Vtype").Value = 3 Then
                    frmContraVoucher.txtRefno.Text = DataGridView1.CurrentRow.Cells("Refno").Value
                    frmContraVoucher.ddlDate.Value = Format(DataGridView1.CurrentRow.Cells("trans_date").Value, "dd/MMM/yyyy")
                    frmContraVoucher.DataGridView1.Rows(0).Cells(0).Value = DataGridView1.CurrentRow.Cells("DebitAccount").Value
                    frmContraVoucher.DataGridView1.Rows(0).Cells(1).Value = DataGridView1.CurrentRow.Cells("Amount").Value
                    frmContraVoucher.DataGridView1.Rows(0).Cells(2).Value = DataGridView1.CurrentRow.Cells("debitid").Value
                    frmContraVoucher.DataGridView1.Rows(0).Cells(3).Value = DataGridView1.CurrentRow.Cells("Auto_id").Value
                    frmContraVoucher.txtNarration.Text = DataGridView1.CurrentRow.Cells("Narration").Value
                    frmContraVoucher.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            DataGridView1.DataSource = Nothing
            PictureBox2.Visible = True
            Datafill()
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).FillWeight = 30
            DataGridView1.Columns("Amount").FillWeight = 30
            DataGridView1.Columns("Refno").FillWeight = 30
            DataGridView1.Columns(8).Visible = False
            DataGridView1.Columns("Vtype").Visible = False
            DataGridView1.Focus()
            PictureBox2.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
       
    End Sub
    Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.Black,
                                      Color.SteelBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.Horizontal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtPassword.Text = "sangroup699" Then
                btnShow.Visible = True
            End If
        End If
    End Sub
End Class