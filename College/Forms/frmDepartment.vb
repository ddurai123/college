Imports MySql.Data.MySqlClient
Public Class frmDepartment
    Dim cm As MySqlCommand
    Dim ad As MySqlDataAdapter
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        save()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            cm = New MySqlCommand("delete from department where Depid=@PDepid", con)
            cm.Parameters.AddWithValue("@PDepid", datagrid1.CurrentRow.Cells(1).Value)
            cm.ExecuteNonQuery()
            tr.Commit()
            fillgrid()
            MessageBox.Show("Record Deleted")
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub frmDepartment_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmDepartment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillgrid()
    End Sub
    Private Sub save()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            Dim i As Integer
            For i = 0 To datagrid1.Rows.Count - 1
                If Not datagrid1.Rows(i).Cells("dgvDepName").Value = Nothing Then
                    cm = New MySqlCommand("PRODEPARTMENT", con)
                    cm.CommandType = CommandType.StoredProcedure
                    cm.Parameters.AddWithValue("@PROdepid", datagrid1.Rows(i).Cells("dgvDepId").Value)
                    cm.Parameters.AddWithValue("@PROdepartment", datagrid1.Rows(i).Cells("dgvDepName").Value)
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
        Try
            datagrid1.Rows.Clear()
            ad = New MySqlDataAdapter("select * from department", con)
            Dim t As New DataTable
            ad.Fill(t)
            Dim i As Integer
            For i = 0 To t.Rows.Count - 1
                i = datagrid1.Rows.Add()
                datagrid1.Rows(i).Cells("dgvDepId").Value = t.Rows(i).Item("DepId")
                datagrid1.Rows(i).Cells("dgvDepName").Value = t.Rows(i).Item("Department")
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
      
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

    Private Sub frmDepartment_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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