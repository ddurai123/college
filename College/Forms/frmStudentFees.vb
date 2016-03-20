Imports MySql.Data.MySqlClient
Imports System.Net

Public Class frmStudentFees
    Dim ad As MySqlDataAdapter
    Dim request As HttpWebRequest
    Dim response As HttpWebResponse = Nothing
    Public RefId As String
    Private Sub frmStudentFees_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If frmEditStudentAccount.Visible = True Then
            Call frmEditStudentAccount.fill()
        End If
        Me.Dispose()
    End Sub
    Private Sub frmStudentFees_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim t1 As New DataTable
        t1.Clear()
        ad = New MySqlDataAdapter("select Semid,Semname from Semesters", con)
        ad.Fill(t1)
        cmbSemesters.DataSource = t1
        cmbSemesters.DisplayMember = "Semname"
        cmbSemesters.ValueMember = "Semid"
        txtBillNo.Focus()
        If frmEditStudentAccount.Visible = True Then
            cmbSemesters.SelectedValue = frmEditStudentAccount.DataGridView1.CurrentRow.Cells("SemId").Value
        End If
    End Sub
    Private Sub save()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            Dim cm As MySqlCommand
            cm = New MySqlCommand("PRO_Voucher", con)
            cm.CommandType = CommandType.StoredProcedure
            cm.Transaction = tr
            If txtAuto_id.Text.Length = 0 Then
                cm.Parameters.AddWithValue("@PAuto_id", DBNull.Value)
            Else
                cm.Parameters.AddWithValue("@PAuto_id", txtAuto_id.Text)
            End If
            cm.Parameters.AddWithValue("@PVoucherdate", Format(ddlFeesDate.Value, "yyyy-MM-dd"))
            cm.Parameters.AddWithValue("@PDebitid", 1)
            cm.Parameters.AddWithValue("@PCreditid", 3)
            cm.Parameters.AddWithValue("@PAmount", txtAmount.Text)
            cm.Parameters.AddWithValue("@Prefno", txtBillNo.Text)
            cm.Parameters.AddWithValue("@PNarration", txtStudentName.Text)
            cm.Parameters.AddWithValue("@PVtype", 3)
            cm.ExecuteNonQuery()
            If txtAuto_id.Text.Length = 0 Then
                cm = New MySqlCommand("select max(auto_id) from account_trans", con)
                Dim t1 As New DataTable
                ad = New MySqlDataAdapter(cm)
                ad.Fill(t1)
                RefId = t1.Rows(0).Item(0).ToString
            End If
        
            cm = New MySqlCommand("PROSTUDENTFEES", con)
            cm.CommandType = CommandType.StoredProcedure
            If txtPaymentId.Text = "" Then
                cm.Parameters.AddWithValue("@PPaymentid", DBNull.Value)
            Else
                cm.Parameters.AddWithValue("@PPaymentid", CInt(txtPaymentId.Text))
            End If
            cm.Parameters.AddWithValue("@PStudentId", CInt(txtStudentId.Text))
            cm.Parameters.AddWithValue("@PAmount", txtAmount.Text)
            cm.Parameters.AddWithValue("@PFeesDate", Format(ddlFeesDate.Value, "yyyy-MM-dd"))
            cm.Parameters.AddWithValue("@PAmtid", txtAmtid.Text)
            cm.Parameters.AddWithValue("@PBillno", txtBillNo.Text)
            cm.Parameters.AddWithValue("@PRefno", RefId)
            cm.ExecuteNonQuery()
            tr.Commit()
            MessageBox.Show("Record Saved")
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PictureBox2.Visible = True
        Try
            If txtStudentId.Text.Length = 0 Then
                MessageBox.Show("Please Enter the Student ID")
                txtStudentId.Focus()
            ElseIf txtBillNo.Text.Length = 0 Then
                MessageBox.Show("Please Enter the Billno")
                txtBillNo.Focus()

            ElseIf txtAmount.Text.Length = 0 Then
                MessageBox.Show("Please Enter the Amount")
                txtAmount.Focus()
            Else
                amtidfill()
                save()
                If CheckBox1.Checked = True Then
                    Dim number As String = txtMobileNo.Text
                    Dim msg As String
                    msg = "Your Payment of Rs." & txtAmount.Text & " Has been credited in your " & cmbSemesters.Text & " Account Bill No:" & txtBillNo.Text & " Date:" & Format(ddlFeesDate.Value, "dd/MMM/yyyy")
                    Dim webAddress As String = "http://pay4sms.in/sendsms/?token=40e39808dce8491fc05c1f877c319bda&credit=1&message=" & msg & " & number=" & txtMobileNo.Text
                    request = DirectCast(WebRequest.Create(webAddress), HttpWebRequest)
                    response = DirectCast(request.GetResponse(), HttpWebResponse)
                    MessageBox.Show("Response: " & response.StatusDescription)

                End If
                clear()
            End If
            PictureBox2.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
     
    End Sub
    Private Sub clear()
        txtAmount.Text = ""
        txtPaymentId.Text = ""
        txtStudentId.Text = ""
        txtBillNo.Text = ""
        txtStudentId.Focus()
    End Sub
    Private Sub txtStudentId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStudentId.KeyDown
        If e.KeyCode = Keys.Enter Then
            stdfill()
        End If
    End Sub
    Private Sub stdfill()
        Try
            If txtStudentId.Text.Length = 0 Then
                MessageBox.Show("Please Enter Student Id")
            Else
                Dim cm As MySqlCommand
                cm = New MySqlCommand("select Studentname,StudentId,Regno,FatherMobileNo from student where studentid=" & txtStudentId.Text & "", con)
                ad = New MySqlDataAdapter(cm)
                Dim t1 As New DataTable
                t1.Clear()
                ad.Fill(t1)
                If t1.Rows.Count > 0 Then
                    txtStudentName.Text = t1.Rows(0).Item(0) & ",ID:" & t1.Rows(0).Item(1) & ",Reg No:" & t1.Rows(0).Item(2) & ""
                    txtMobileNo.Text = t1.Rows(0).Item(3)
                    txtBillNo.Focus()
                Else
                    MessageBox.Show("No Record Found")
                    txtStudentId.Focus()
                    clear()
                End If
            End If
           
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub amtidfill()
        Try
            Dim cm As New MySqlCommand("PROFEESSELECT", con)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.AddWithValue("PStudentId", txtStudentId.Text)
            cm.Parameters.AddWithValue("PSemId", cmbSemesters.SelectedValue)
            ad = New MySqlDataAdapter(cm)
            Dim t1 As New DataTable
            ad.Fill(t1)
            txtAmtid.Text = t1.Rows(0).Item("amtid")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clear()
    End Sub

    Private Sub txtStudentSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStudentSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            stdfill()
            txtBillNo.Focus()
        End If
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
    Private Sub txtBillNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBillNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbSemesters.Focus()
        End If
    End Sub
    Private Sub ddlFeesDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ddlFeesDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtAmount.Focus()
        End If
    End Sub
    Private Sub txtAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub cmbSemesters_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSemesters.KeyDown
        If e.KeyCode = Keys.Enter Then
            ddlFeesDate.Focus()
        End If
    End Sub
    Private Sub txtStudentId_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStudentId.Leave
        'If txtStudentId.Text.Length = 0 Then
        '    MessageBox.Show("Please Enter Student ID")
        '    txtStudentId.Focus()
        'End If
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

    Private Sub frmStudentFees_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.BackwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub txtBillNo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBillNo.Leave
        'If txtBillNo.Text.Length = 0 Then
        '    MessageBox.Show("Please Enter Bill No")
        '    txtBillNo.Focus()
        'End If
    End Sub

    Private Sub txtBillNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBillNo.TextChanged

    End Sub

    Private Sub frmStudentFees_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub txtMobileNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobileNo.TextChanged

    End Sub
End Class