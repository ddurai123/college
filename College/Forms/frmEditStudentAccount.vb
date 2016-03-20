Imports MySql.Data.MySqlClient
Public Class frmEditStudentAccount
    Public Sub txtStudentSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStudentSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            fill()
        End If
    End Sub

    Public Sub fill()
        Dim ad As MySqlDataAdapter
        Dim t As New DataTable
        Dim cm As MySqlCommand
        cm = New MySqlCommand("PRO_EDITSTUDENT", con)
        cm.Parameters.AddWithValue("@PStudentId", txtStudentId.Text)
        cm.CommandType = CommandType.StoredProcedure
        t.Clear()
        ad = New MySqlDataAdapter(cm)
        ad.Fill(t)
        DataGridView1.DataSource = t
        DataGridView1.Columns("debitid").Visible = False
        DataGridView1.Columns("creditid").Visible = False
        DataGridView1.Columns("amtid").Visible = False
        DataGridView1.Columns("auto_id").Visible = False
        DataGridView1.Columns("paymentid").Visible = False
        DataGridView1.Columns("Semid").Visible = False
        DataGridView1.Columns("Semname").HeaderText = "Sem/Year"
        DataGridView1.Columns("Feesdate").HeaderText = "Fees Date"
        DataGridView1.Columns("Feesdate").FillWeight = 40
    End Sub
    Private Sub txtStudentSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStudentSearch.TextChanged
        If Len(txtStudentSearch.Text) Then
            frmStudentSearch.txtSearch.Text = txtStudentSearch.Text
            frmStudentSearch.txtSearch.Select(frmStudentSearch.txtSearch.Text.Length, 1)
            If frmStudentSearch.Visible = False Then
                frmStudentSearch.ShowDialog()
            End If
        End If
    End Sub
    Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.Black,
                                      Color.SteelBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub txtStudentId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStudentId.KeyDown
        If e.KeyCode = Keys.Enter Then
            fill()
        End If
    End Sub
    Private Sub assign()
        frmStudentFees.txtBillNo.Text = DataGridView1.CurrentRow.Cells("Billno").Value
        frmStudentFees.txtStudentId.Text = DataGridView1.CurrentRow.Cells("Studentid").Value
        frmStudentFees.txtAmount.Text = DataGridView1.CurrentRow.Cells("Amount").Value
        frmStudentFees.txtStudentName.Text = DataGridView1.CurrentRow.Cells("StudentName").Value
        frmStudentFees.txtPaymentId.Text = DataGridView1.CurrentRow.Cells("Paymentid").Value
        frmStudentFees.txtAuto_id.Text = DataGridView1.CurrentRow.Cells("auto_id").Value
        frmStudentFees.ddlFeesDate.Value = DataGridView1.CurrentRow.Cells("Feesdate").Value
        frmStudentFees.RefId = DataGridView1.CurrentRow.Cells("auto_id").Value
        frmStudentFees.ShowDialog()
    End Sub
    Private Sub DataGridView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        assign()
    End Sub

    Private Sub frmEditStudentAccount_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub frmEditStudentAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtPassword.Focus()
        txtStudentId.Enabled = False
        txtStudentSearch.Enabled = False
    End Sub
    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtPassword.Text = "sangroup699" Then
                txtStudentId.Enabled = True
                txtStudentSearch.Enabled = True
                txtStudentSearch.Focus()
            Else
                MessageBox.Show("Invalid Password")
            End If
        End If
    End Sub

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

    End Sub
End Class