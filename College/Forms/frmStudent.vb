Imports MySql.Data.MySqlClient

Public Class frmStudent
    Dim cm As MySqlCommand
    Dim ad As MySqlDataAdapter
    Dim t As New DataTable
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        save()
        txtName.Focus()
    End Sub
    Private Sub save()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            If txtFeesId.Text.Length = 0 Then
                MessageBox.Show("Please Select Course")
                txtSearchFees.Focus()
                tr.Rollback()
            ElseIf txtName.Text.Length = 0 Then
                MessageBox.Show("Please Enter Student Name")
                tr.Rollback()
            ElseIf txtSearchFees.Text.Length = 0 Then
                MessageBox.Show("Please Select Course")
                txtSearchFees.Focus()
                tr.Rollback()
            Else
                cm = New MySqlCommand("PROSTUDENT", con)
                cm.CommandType = CommandType.StoredProcedure
                If txtStudentId.Text = "" Then
                    cm.Parameters.AddWithValue("@PStudentId", DBNull.Value)
                Else
                    cm.Parameters.AddWithValue("@PStudentId", CInt(txtStudentId.Text))
                End If
                cm.Parameters.AddWithValue("@PStudentName", txtName.Text)
                cm.Parameters.AddWithValue("@PRegno", txtRegno.Text)
                cm.Parameters.AddWithValue("@PMobileNo", txtMobileNo.Text)
                cm.Parameters.AddWithValue("@PBatch", cmbBatch.SelectedValue)
                cm.Parameters.AddWithValue("@PGender", cmbGender.Text)
                cm.Parameters.AddWithValue("@PDateofBirth", Format(ddlDOB.Value, "yyyy-MM-dd"))
                cm.Parameters.AddWithValue("@PDateofJoining", Format(ddlDOJ.Value, "yyyy-MM-dd"))
                cm.Parameters.AddWithValue("@PMothersname", txtMothersName.Text)
                cm.Parameters.AddWithValue("@PFathersName", txtFatherName.Text)
                cm.Parameters.AddWithValue("@PFathersOccupation", txtFatherOccupation.Text)
                cm.Parameters.AddWithValue("@PFatherMobileno", txtFatherMobileno.Text)
                cm.Parameters.AddWithValue("@PEmailid", txtEmailId.Text)
                cm.Parameters.AddWithValue("@PStreet", txtStreet.Text)
                cm.Parameters.AddWithValue("@PCity", txtCity.Text)
                cm.Parameters.AddWithValue("@PTaluk", txtTaluk.Text)
                cm.Parameters.AddWithValue("@PDistrict", txtDistrict.Text)
                cm.Parameters.AddWithValue("@PPincode", txtPincode.Text)
                cm.Parameters.AddWithValue("@PState", txtState.Text)
                cm.Parameters.AddWithValue("@PReligion", cmbReligion.Text)
                cm.Parameters.AddWithValue("@PCommunity", cmbCommunity.Text)
                cm.Parameters.AddWithValue("@PSubcaste", txtSubCaste.Text)
                cm.Parameters.AddWithValue("@PHome", cmbHome.Text)
                cm.Parameters.AddWithValue("@PFeesId", txtFeesId.Text)
                cm.Parameters.AddWithValue("@PDocumentsSubmitted", txtDocumentsSubmitted.Text)
                cm.Parameters.AddWithValue("@PAatharNo", txtAatharNo.Text)
                cm.Parameters.AddWithValue("@PBankName", txtBankName.Text)
                cm.Parameters.AddWithValue("@PBankAcno", txtBankAc.Text)
                cm.Parameters.AddWithValue("@PBranch", txtBranch.Text)
                cm.Parameters.AddWithValue("@PIFSCCode", txtIFSCCode.Text)
                cm.Parameters.AddWithValue("@PReference", txtReference.Text)
                If cmbStatus.SelectedIndex = 0 Then
                    cm.Parameters.AddWithValue("@PStatus", 0)
                Else
                    cm.Parameters.AddWithValue("@PStatus", 1)
                End If
                cm.ExecuteNonQuery()
                tr.Commit()
                clear()
                MessageBox.Show("Record Saved")
            End If
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub clear()
        txtName.Text = ""
        txtRegno.Text = ""
        txtMobileNo.Text = ""
        txtMothersName.Text = ""
        txtFatherName.Text = ""
        txtFatherMobileno.Text = ""
        txtEmailId.Text = ""
        txtStreet.Text = ""
        txtCity.Text = ""
        txtTaluk.Text = ""
        txtDistrict.Text = ""
        txtPincode.Text = ""
        txtState.Text = ""
        txtRegno.Text = ""
        txtSubCaste.Text = ""
        txtDocumentsSubmitted.Text = ""
        txtAatharNo.Text = ""
        txtBankName.Text = ""
        txtBankAc.Text = ""
        txtBranch.Text = ""
        txtIFSCCode.Text = ""
        txtReference.Text = ""
        txtStudentId.Text = ""
        txtFeesId.Text = ""
        txtFatherOccupation.Text = ""
        txtStudentId.ReadOnly = False
    End Sub

    Private Sub txtSearchFees_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchFees.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtName.Focus()
        End If
    End Sub
    Private Sub txtSearchFees_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchFees.TextChanged
        If Len(txtSearchFees.Text.Trim) = 1 Then
            frmFeesIdSearch.txtSearch.Text = txtSearchFees.Text.Trim
            frmFeesIdSearch.txtSearch.Select(frmFeesIdSearch.txtSearch.Text.Length, 1)
            frmFeesIdSearch.ShowDialog()
        End If
    End Sub

    Private Sub txtStudentId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStudentId.KeyDown
        If e.KeyCode = Keys.Enter Then
            studentfill()
        End If
    End Sub
    Private Sub studentfill()
        Try
            t.Clear()
            cm = New MySqlCommand("Studentsearch", con)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.AddWithValue("PStudentid", txtStudentId.Text.Trim)
            ad = New MySqlDataAdapter(cm)
            ad.Fill(t)
            txtName.Text = t.Rows(0).Item("StudentName").ToString
            txtRegno.Text = t.Rows(0).Item("Regno").ToString
            txtMobileNo.Text = t.Rows(0).Item("MobileNo").ToString
            cmbBatch.SelectedValue = t.Rows(0).Item("Batch")
            cmbGender.Text = t.Rows(0).Item("Gender").ToString
            txtMothersName.Text = t.Rows(0).Item("Mothersname")
            txtFatherName.Text = t.Rows(0).Item("FathersName")
            txtFatherMobileno.Text = t.Rows(0).Item("FatherMobileno")
            txtFatherOccupation.Text = t.Rows(0).Item("FathersOccupation").ToString
            txtEmailId.Text = t.Rows(0).Item("Emailid")
            txtStreet.Text = t.Rows(0).Item("Street")
            txtCity.Text = t.Rows(0).Item("City")
            txtTaluk.Text = t.Rows(0).Item("Taluk")
            txtDistrict.Text = t.Rows(0).Item("District")
            txtPincode.Text = t.Rows(0).Item("Pincode")
            txtState.Text = t.Rows(0).Item("State")
            cmbReligion.Text = t.Rows(0).Item("Religion")
            cmbCommunity.Text = t.Rows(0).Item("Community")
            txtSubCaste.Text = t.Rows(0).Item("Subcaste")
            txtFeesId.Text = t.Rows(0).Item("FeesId")
            cmbHome.Text = t.Rows(0).Item("Home")
            txtDocumentsSubmitted.Text = t.Rows(0).Item("DocumentsSubmitted")
            txtAatharNo.Text = t.Rows(0).Item("AatharNo")
            txtBankName.Text = t.Rows(0).Item("BankName")
            txtBankAc.Text = t.Rows(0).Item("BankAcno")
            txtBranch.Text = t.Rows(0).Item("Branch")
            txtIFSCCode.Text = t.Rows(0).Item("IFSCCode")
            txtReference.Text = t.Rows(0).Item("Reference")
            txtSearchFees.Text = t.Rows(0).Item("Course")
            cmbStatus.SelectedIndex = t.Rows(0).Item("Status")
            txtStudentId.ReadOnly = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
       
    End Sub
    Private Sub txtName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRegno.Focus()
        End If
    End Sub

    Private Sub txtRegno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbGender.Focus()
        End If
    End Sub

    Private Sub cmbGender_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbGender.KeyDown
        If e.KeyCode = Keys.Enter Then
            ddlDOB.Focus()
        End If
    End Sub

    Private Sub ddlDOB_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ddlDOB.KeyDown
        If e.KeyCode = Keys.Enter Then
            ddlDOJ.Focus()
        End If
    End Sub

    Private Sub ddlDOJ_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ddlDOJ.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMothersName.Focus()
        End If
    End Sub

    Private Sub txtMothersName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMothersName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFatherName.Focus()
        End If
    End Sub

    Private Sub txtFatherName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFatherName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFatherOccupation.Focus()
        End If
    End Sub

    Private Sub txtFatherOccupation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFatherOccupation.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMobileNo.Focus()
        End If
    End Sub

    Private Sub txtMobileNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMobileNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFatherMobileno.Focus()
        End If
    End Sub

    Private Sub txtFatherMobileno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFatherMobileno.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtEmailId.Focus()
        End If
    End Sub

    Private Sub txtEmailId_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmailId.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtStreet.Focus()
        End If
    End Sub

    Private Sub txtStreet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStreet.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCity.Focus()
        End If
    End Sub

    Private Sub txtCity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCity.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtTaluk.Focus()
        End If
    End Sub

    Private Sub txtTaluk_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTaluk.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtDistrict.Focus()
        End If
    End Sub

    Private Sub txtDistrict_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDistrict.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPincode.Focus()
        End If
    End Sub

    Private Sub txtPincode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPincode.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtState.Focus()
        End If
    End Sub

    Private Sub txtState_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtState.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbReligion.Focus()
        End If
    End Sub

    Private Sub cmbReligion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbReligion.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbCommunity.Focus()
        End If
    End Sub

    Private Sub cmbCommunity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCommunity.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtSubCaste.Focus()
        End If
    End Sub

    Private Sub txtSubCaste_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSubCaste.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbBatch.Focus()
        End If
    End Sub

    Private Sub cmbBatch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbBatch.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbHome.Focus()
        End If
    End Sub

    Private Sub cmbHome_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbHome.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtDocumentsSubmitted.Focus()
        End If
    End Sub

    Private Sub txtDocumentsSubmitted_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDocumentsSubmitted.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtAatharNo.Focus()
        End If
    End Sub

    Private Sub txtAatharNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAatharNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBankName.Focus()
        End If
    End Sub

    Private Sub txtBankName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBankName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBankAc.Focus()
        End If
    End Sub

    Private Sub txtBankAc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBankAc.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBranch.Focus()
        End If
    End Sub

    Private Sub txtBranch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBranch.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtIFSCCode.Focus()
        End If
    End Sub

    Private Sub txtIFSCCode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtIFSCCode.KeyDown

        If e.KeyCode = Keys.Enter Then
            txtReference.Focus()
        End If
    End Sub

    Private Sub txtReference_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReference.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbStatus.Focus()
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clear()
        txtFeesId.Text = ""
        txtSearchFees.Text = ""
    End Sub
    Private Sub txtStudentSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStudentSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtStudentId.Text = "" Then
                txtSearchFees.Focus()
            Else
                studentfill()
                txtName.Focus()
            End If
        End If

    End Sub

    Private Sub txtStudentSearch_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStudentSearch.Leave
        If frmStudentSearch.Visible = False Then
            txtStudentSearch.Text = ""
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

    Private Sub frmStudent_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub frmStudent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        cmbReligion.SelectedIndex = 0
        cmbGender.SelectedIndex = 0
        cmbCommunity.SelectedIndex = 0
        Dim t1 As New DataTable
        t1.Clear()
        ad = New MySqlDataAdapter("select BatchYear,Batchid from Batch", con)
        ad.Fill(t1)
        cmbBatch.DataSource = t1
        cmbBatch.DisplayMember = "BatchYear"
        cmbBatch.ValueMember = "Batchid"
        txtSearchFees.Focus()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            If txtStudentId.Text.Length = 0 Then
                MessageBox.Show("Please Enter StudentId")
            Else
                cm = New MySqlCommand("delete from student where studentid=" & txtStudentId.Text & "", con)
                cm.ExecuteNonQuery()
                tr.Commit()
                clear()
                MessageBox.Show("Record Deleted")
            End If
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    
    End Sub
    Private Sub frmStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbHome.SelectedIndex = 0
        cmbStatus.SelectedIndex = 0
    End Sub
    Private Sub Panel1_Paint(
    ByVal sender As Object,
    ByVal e As System.Windows.Forms.PaintEventArgs) _
Handles Panel1.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.Black,
                                      Color.SteelBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub
    Private Sub cmbStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbStatus.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSave.Focus()
        End If
    End Sub

    Private Sub txtStudentId_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStudentId.TextChanged

    End Sub
End Class