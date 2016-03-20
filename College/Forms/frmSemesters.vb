Imports MySql.Data.MySqlClient

Public Class frmSemesters
    Dim cm As New MySqlCommand
    Dim ad As MySqlDataAdapter
   
    Private Sub save()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            Dim i As Integer
            For i = 0 To datagrid1.Rows.Count - 1
                If Not datagrid1.Rows(i).Cells("dgvSemName").Value = Nothing Then
                    cm = New MySqlCommand("PROSEMESTERS", con)
                    cm.CommandType = CommandType.StoredProcedure
                    cm.Parameters.AddWithValue("@PSemid", datagrid1.Rows(i).Cells("dgvSemId").Value)
                    cm.Parameters.AddWithValue("@PSemname", datagrid1.Rows(i).Cells("dgvSemName").Value)
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
            cm = New MySqlCommand("delete from semesters where Semid=@PSemid", con)
            cm.Parameters.AddWithValue("@PSemid", datagrid1.CurrentRow.Cells(1).Value)
            cm.ExecuteNonQuery()
            tr.Commit()
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    
    Private Sub fillgrid()
        Try
            datagrid1.Rows.Clear()
            ad = New MySqlDataAdapter("select * from semesters", con)
            Dim t As New DataTable
            ad.Fill(t)
            Dim i As Integer
            For i = 0 To t.Rows.Count - 1
                i = datagrid1.Rows.Add()
                datagrid1.Rows(i).Cells("dgvSemId").Value = t.Rows(i).Item("SemId")
                datagrid1.Rows(i).Cells("dgvSemName").Value = t.Rows(i).Item("Semname")
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub frmSemesters_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.BackwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub frmSemesters_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        fillgrid()
    End Sub

    Private Sub frmSemesters_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        save()
    End Sub

  
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        delete()
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
End Class