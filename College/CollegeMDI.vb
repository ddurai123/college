Imports System.Windows.Forms

Public Class CollegeMDI
    Private Sub CollegeMDI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        connection()
    End Sub

    Private Sub DepartmentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DepartmentsToolStripMenuItem.Click
        frmDepartment.ShowDialog()
    End Sub

    Private Sub CoursesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CoursesToolStripMenuItem.Click
        frmCourse.ShowDialog()
    End Sub

    Private Sub RegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterToolStripMenuItem.Click
        frmRegister.ShowDialog()
    End Sub

    Private Sub FeesStructureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FeesStructureToolStripMenuItem.Click
        frmFeesStructure.ShowDialog()
    End Sub

    Private Sub BatchToolrmStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatchToolStripMenuItem.Click
        frmBatch.ShowDialog()
    End Sub

    Private Sub SemesterYearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SemesterYearToolStripMenuItem.Click
        frmSemesters.ShowDialog()
    End Sub

    Private Sub FeesStructureFollwedFromToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmFeesFollowed.ShowDialog()
    End Sub

    Private Sub StudentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StudentsToolStripMenuItem.Click
        frmStudent.ShowDialog()
    End Sub

    Private Sub StudentFeesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StudentFeesToolStripMenuItem.Click
        frmStudentFees.ShowDialog()
    End Sub

    Private Sub FeesBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FeesBalanceToolStripMenuItem.Click
        frmFeesBalanceS.ShowDialog()
    End Sub

    Private Sub DiscountsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiscountsToolStripMenuItem.Click
        frmDiscounts.ShowDialog()
    End Sub

    Private Sub DailyCollectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailyCollectionToolStripMenuItem.Click
        frmDailyCollection.ShowDialog()
    End Sub

 

    Private Sub PaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentToolStripMenuItem.Click

    End Sub

    Private Sub CASHAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CASHAccountToolStripMenuItem.Click
        frmAccountMonthwise.ShowDialog()
    End Sub

    Private Sub SearchPaymentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchPaymentsToolStripMenuItem.Click
        frmSearchPayments.ShowDialog()
    End Sub

    Private Sub FeesBalanceDetailedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmFeesBalanceS.ShowDialog()
    End Sub

  

    Private Sub LedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerToolStripMenuItem.Click
        frmLedger.ShowDialog()
    End Sub

    Private Sub LedgerEditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerEditToolStripMenuItem.Click
        frmLedgerEdit.ShowDialog()
    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub MenuStrip1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MenuStrip1.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.Black,
                                      Color.Green,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub EditStudentFeesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditStudentFeesToolStripMenuItem.Click
        frmEditStudentAccount.ShowDialog()
    End Sub

    Private Sub CollegeMDI_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
       
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(3, 6, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.Silver,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub AccountDailyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountDailyToolStripMenuItem.Click
        frmAccountDaily.ShowDialog()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub


    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub PictureBox2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox2.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.IndianRed,
                                      Color.BurlyWood,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub PaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentToolStripMenuItem1.Click
        frmPayVoucher.ShowDialog()
    End Sub

    Private Sub ReceiptToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceiptToolStripMenuItem1.Click
        frmReceiptVoucher.ShowDialog()
    End Sub

    Private Sub ContraToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContraToolStripMenuItem.Click
        frmContraVoucher.ShowDialog()
    End Sub

    Private Sub StudentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StudentToolStripMenuItem.Click
        frmStudentReport.ShowDialog()
    End Sub
End Class
