Imports MySql.Data.MySqlClient

Public Class frmStudentReport

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Op = cmbView.SelectedIndex
        PAccountid = cmbBatch.SelectedValue
        Batch = cmbBatch.Text
        frmRptStudent.Show()
    End Sub

    Private Sub frmStudentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbView.SelectedIndex = 0
        Dim t1, t2 As New DataTable
        t1.Clear()
        Dim ad As MySqlDataAdapter
        ad = New MySqlDataAdapter("select BatchYear,Batchid from Batch", con)
        ad.Fill(t1)
        cmbBatch.DataSource = t1
        cmbBatch.DisplayMember = "BatchYear"
        cmbBatch.ValueMember = "Batchid"
        ad = New MySqlDataAdapter("select shopname from shopdetails", con)
        t2.Clear()
        ad.Fill(t2)
        Header = t2.Rows(0).Item(0)
    End Sub

    Private Sub frmStudentReport_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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