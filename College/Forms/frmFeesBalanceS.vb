Imports MySql.Data.MySqlClient

Public Class frmFeesBalanceS


    Private Sub frmFeesBalanceS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim t1, t2, t3 As New DataTable
        t1.Clear()
        Dim ad As MySqlDataAdapter
        ad = New MySqlDataAdapter("select BatchYear,Batchid from Batch", con)
        ad.Fill(t1)
        cmbBatch.DataSource = t1
        cmbBatch.DisplayMember = "BatchYear"
        cmbBatch.ValueMember = "Batchid"
        ad = New MySqlDataAdapter("select Semname from Semesters order by semname", con)
        t2.Clear()
        t3.Clear()
        ad.Fill(t2)
        ad.Fill(t3)
        cmbFrom.DataSource = t2
        cmbTo.DataSource = t3
        cmbFrom.DisplayMember = "Semname"
        cmbTo.DisplayMember = "Semname"
        cmbView.SelectedIndex = 0
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If cmbView.SelectedIndex = 0 Then
            frmRptFeesBalance.bid = cmbBatch.SelectedValue
            frmRptFeesBalance.Sfrom = cmbFrom.Text.Trim
            frmRptFeesBalance.Sto = cmbTo.Text.Trim
            frmRptFeesBalance.Show()
        ElseIf cmbView.SelectedIndex = 1 Then
            frmrptFeesBalanceDetailed.bid = cmbBatch.SelectedValue
            frmrptFeesBalanceDetailed.Sfrom = cmbFrom.Text.Trim
            frmrptFeesBalanceDetailed.Sto = cmbTo.Text.Trim
            frmrptFeesBalanceDetailed.Show()
        End If

    End Sub

    Private Sub frmFeesBalanceS_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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