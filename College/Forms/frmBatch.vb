Imports MySql.Data.MySqlClient

Public Class frmBatch
    Dim cm As New MySqlCommand
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        save()
    End Sub
    Private Sub save()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            cm = New MySqlCommand("PROBATCH", con)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.AddWithValue("@PBatchYear", txtBatch.Text)
            If txtId.Text = "" Then
                cm.Parameters.AddWithValue("@PBatchid", DBNull.Value)
            Else
                cm.Parameters.AddWithValue("@PBatchid", CInt(txtId.Text))
            End If
            cm.ExecuteNonQuery()
            clear()
            tr.Commit()
            MessageBox.Show("Record Saved")
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub clear()
        txtBatch.Text = ""
        btnSave.Text = "Save(F1)"
    End Sub
    Public Sub listfind()
        Dim ad As MySqlDataAdapter
        Dim t As New DataTable
        t.Clear()
        ad = New MySqlDataAdapter("select * from Batch where BatchYear='" & Trim(lstSearch.SelectedValue) & "'", con)
        ad.Fill(t)
        If t.Rows.Count > 0 Then
            txtId.Text = t.Rows(0).Item("Batchid")
            txtBatch.Text = t.Rows(0).Item("BatchYear")
            txtBatch.Focus()

        End If
    End Sub
    Private Sub txtBatch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBatch.KeyDown
        If e.KeyCode = Keys.Enter Then
            listfind()
            txtBatch.Select(txtBatch.Text.Length, txtBatch.Text.Length)
            btnSave.Text = "Update(F1)"
        ElseIf e.KeyCode = Keys.Tab Then
            btnSave.Focus()
        ElseIf e.KeyCode = Keys.Down Then
            If lstSearch.Items.Count > 1 Then
                lstSearch.Focus()
                lstSearch.SelectedIndex += 1
            Else
                lstSearch.Focus()
            End If
        End If
    End Sub
    Private Sub txtBatch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBatch.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then e.KeyChar = ""
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        delete()
    End Sub

    Private Sub txtBatch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBatch.TextChanged
        Try
            lstSearch.DataSource = Nothing
            If txtBatch.Text = "" Then
            Else
                Dim ad As New MySqlDataAdapter
                Dim t As New DataTable
                t.Clear()
                ad = New MySqlDataAdapter("select BatchYear from Batch where BatchYear like '" & txtBatch.Text & "%'", con)
                ad.Fill(t)
                lstSearch.DisplayMember = "BatchYear"
                lstSearch.ValueMember = "BatchYear"
                lstSearch.DataSource = t
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub lstSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBatch.Text = lstSearch.SelectedValue
            txtBatch.Focus()
            SendKeys.Send("{ENTER}")
            txtBatch.Select(txtBatch.Text.Length, txtBatch.Text.Length)
        End If
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
    Private Sub delete()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            cm = New MySqlCommand("delete from batch where Batchid=@PBatchid", con)
            cm.Parameters.AddWithValue("@PBatchid", txtId.Text)
            cm.ExecuteNonQuery()
            clear()
            tr.Commit()
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub frmBatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmBatch_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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