Imports MySql.Data.MySqlClient

Public Class frmFeesStructure
    Dim cm As MySqlCommand
    Dim ad As MySqlDataAdapter
    Dim scAutoComplete3 As New AutoCompleteStringCollection
    Dim WithEvents txt1 As TextBox
    Private Sub frmFeesStructure_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim cm As New MySqlCommand
            cm = New MySqlCommand("select feesid from feestruct where courseid=" & cmbCourse.SelectedValue & " and batch=" & txtYear.Text & "", con)
            ad = New MySqlDataAdapter(cm)
            Dim t1 As New DataTable
            t1.Clear()
            ad.Fill(t1)
            If t1.Rows.Count > 0 And lblFeesId.Text = "" Then
                MessageBox.Show("Record Already Exist")
            ElseIf txtYear.Text = "" Then
                MessageBox.Show("Please Enter Year of Fees Structure")
            ElseIf txtYear.Text.Length < 4 Then
                MessageBox.Show("Year Should be in 4 Digits")
            ElseIf dgvFees.Rows(0).Cells(0).Value = Nothing And dgvFees.Rows(0).Cells(1).Value = Nothing Then
                MessageBox.Show("Please Enter Fees Structure")
            Else
                save()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
       
    End Sub
    Private Sub save()
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            cm = New MySqlCommand("PROFEES", con)
            cm.CommandType = CommandType.StoredProcedure
            If lblFeesId.Text = "" Then
                cm.Parameters.AddWithValue("@PROFeesid", DBNull.Value)
            Else
                cm.Parameters.AddWithValue("@PROFeesid", lblFeesId.Text)
            End If
            cm.Parameters.AddWithValue("@PROCourseid", cmbCourse.SelectedValue)
            cm.Parameters.AddWithValue("@PROBatch", txtYear.Text)
            cm.ExecuteNonQuery()
            Dim i As Integer
            Dim feesid As Integer
            Dim t As New DataTable
            ad = New MySqlDataAdapter("select max(feesid) from Feestruct", con)
            t.Clear()
            ad.Fill(t)
            feesid = t.Rows(0).Item(0)
            For i = 0 To dgvFees.Rows.Count - 1
                If Not dgvFees.Rows(i).Cells(1).Value Is Nothing Then
                    cm = New MySqlCommand("PROFEESSTRUCTURE", con)
                    cm.CommandType = CommandType.StoredProcedure
                    cm.Parameters.AddWithValue("@PROamtid", dgvFees.Rows(i).Cells("dgvAutoid").Value)
                    If lblFeesId.Text = "" Then
                        cm.Parameters.AddWithValue("@PROFeesid", feesid)
                    Else
                        cm.Parameters.AddWithValue("@PROFeesid", lblFeesId.Text)
                    End If
                    cm.Parameters.AddWithValue("@PROSemId", dgvFees.Rows(i).Cells("dgvSemAutoId").Value)
                    cm.Parameters.AddWithValue("@PROFees", dgvFees.Rows(i).Cells("dgvAmount").Value)
                    cm.Parameters.AddWithValue("@PROBatch", 0)
                    If i = 0 Then
                        cm.Parameters.AddWithValue("@PROCourseId", cmbCourse.SelectedValue)
                    Else
                        cm.Parameters.AddWithValue("@PROCourseId", 0)
                    End If

                    cm.ExecuteNonQuery()
                End If
            Next
            tr.Commit()
            MessageBox.Show("Record Saved")
            clear()
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub clear()
        txtYear.Text = ""
        dgvFees.Rows.Clear()
        lblFeesId.Text = ""
        txtSearchFees.Text = ""
    End Sub
    Private Sub dgvFees_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFees.CellEndEdit
        Dim t As New DataTable
        If dgvFees.CurrentCell.ColumnIndex = 0 Then
            t.Clear()
            ad = New MySqlDataAdapter("Select Semid From Semesters where SemName='" & Trim(dgvFees.CurrentRow.Cells("dgvSemId").Value) & "'", con)
            ad.Fill(t)
            If t.Rows.Count > 0 Then
                dgvFees.CurrentRow.Cells("dgvSemAutoId").Value = t.Rows(0).Item(0)
            Else
                dgvFees.CurrentRow.Cells("dgvSemAutoId").Value = Nothing
            End If
        End If
    End Sub
    Private Sub dgvFees_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvFees.EditingControlShowing
        Try
            If dgvFees.CurrentCell.ColumnIndex = 0 Then
                If TypeOf e.Control Is TextBox Then
                    txt1 = DirectCast(e.Control, TextBox)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If dgvFees.CurrentCell.ColumnIndex = 0 Then
            Dim cmd11 As New MySqlCommand
            cmd11 = New MySqlCommand("Select SemName From Semesters where SemName like '" & txt1.Text & "%'", con)
            Dim dr As MySqlDataReader
            dr = cmd11.ExecuteReader
            Do While dr.Read
                scAutoComplete3.Add(dr.Item("SemName"))
            Loop
            dr.Close()
        End If
        If dgvFees.CurrentCell.ColumnIndex = 0 AndAlso TypeOf e.Control Is TextBox Then
            With DirectCast(e.Control, TextBox)
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteSource = AutoCompleteSource.CustomSource
                .AutoCompleteCustomSource = scAutoComplete3
            End With
        Else
            With DirectCast(e.Control, TextBox)
                .AutoCompleteMode = AutoCompleteMode.None
                .AutoCompleteSource = AutoCompleteSource.None
                .AutoCompleteCustomSource = scAutoComplete3
            End With
        End If
    End Sub
    Private Sub dgvFees_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvFees.CellPainting
        Try
            '   Datagridview1.BeginEdit(True)
            If e.ColumnIndex = Me.dgvFees.CurrentCell.ColumnIndex AndAlso e.RowIndex = Me.dgvFees.CurrentCell.RowIndex Then
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

    Private Sub dgvFees_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFees.CellLeave
        dgvFees.Invalidate()
    End Sub


  

    Private Sub frmFeesStructure_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim ad As MySqlDataAdapter
        Dim t As New DataTable
        t.Clear()
        ad = New MySqlDataAdapter("select department,depId from department", con)
        ad.Fill(t)
        CmbDepartment.DataSource = t
        CmbDepartment.DisplayMember = "department"
        CmbDepartment.ValueMember = "depId"
        CmbDepartment.SelectedIndex = 0
    End Sub

    Private Sub CmbDepartment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDepartment.SelectedIndexChanged
        Try
            Dim ad As MySqlDataAdapter
            Dim t As New DataTable
            t.Clear()
            ad = New MySqlDataAdapter("select Courseid,Course from class_view where Department='" & CmbDepartment.Text & "'", con)
            ad.Fill(t)
            cmbCourse.DataSource = t
            cmbCourse.DisplayMember = "Course"
            cmbCourse.ValueMember = "CourseId"
            'If t.Rows.Count > 0 Then
            '    cmbCourse.DataSource = t
            '    cmbCourse.DisplayMember = "Course"
            '    cmbCourse.ValueMember = "CourseId"
            'Else
            '    MessageBox.Show("No Course Available for this Department")
            '    CmbDepartment.SelectedIndex = 0
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub txtYear_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtYear.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then e.KeyChar = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clear()
    End Sub

   

    Private Sub frmFeesStructure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub dgvFees_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFees.CellContentClick

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.Black,
                                      Color.SteelBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub frmFeesStructure_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub txtSearchFees_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchFees.KeyDown
        If e.KeyCode = Keys.F12 Then
            frmFeesIdSearch.ShowDialog()
        ElseIf e.KeyCode = Keys.Enter Then
            Try
                Dim ad As MySqlDataAdapter
                Dim t As New DataTable
                t.Clear()
                ad = New MySqlDataAdapter("select * from feesstructure where FeesId=" & lblFeesId.Text & "", con)
                ad.Fill(t)
                txtYear.Text = t.Rows(0).Item("Batch")
                cmbCourse.Text = t.Rows(0).Item("Course")
                CmbDepartment.Text = t.Rows(0).Item("Department")
                dgvFees.AllowUserToAddRows = False
                Dim i As Integer

                For i = 0 To t.Rows.Count - 1
                    dgvFees.Rows.Add()
                    dgvFees.Rows(i).Cells("dgvSemId").Value = t.Rows(i).Item("Semname")
                    dgvFees.Rows(i).Cells("dgvSemAutoId").Value = t.Rows(i).Item("SemId")
                    dgvFees.Rows(i).Cells("dgvAmount").Value = t.Rows(i).Item("Fees")
                    dgvFees.Rows(i).Cells("dgvAutoid").Value = t.Rows(i).Item("amtid")
                Next
                dgvFees.AllowUserToAddRows = True
                txtYear.Focus()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End If
    End Sub
    Private Sub txtSearchFees_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchFees.TextChanged
        If Len(txtSearchFees.Text.Trim) = 1 Then
            frmFeesIdSearch.txtSearch.Text = txtSearchFees.Text.Trim
            frmFeesIdSearch.txtSearch.Select(frmFeesIdSearch.txtSearch.Text.Length, 1)
            frmFeesIdSearch.ShowDialog()
        End If
    End Sub
End Class