Imports MySql.Data.MySqlClient

Public Class frmDiscounts
    Dim discountid As String
    Dim cm As MySqlCommand
    Dim ad As MySqlDataAdapter
    Dim scAutoComplete3 As New AutoCompleteStringCollection
    Dim WithEvents txt1 As TextBox
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        save()
    End Sub
    Private Sub save()
        If txtStudentId.TextLength = 0 Then
            MessageBox.Show("Please Enter StudentId")
        Else
            Dim tr As MySqlTransaction
            tr = con.BeginTransaction
            Try
                Dim cm As MySqlCommand
                Dim i As Integer
                For i = 0 To dgvFees.Rows.Count - 1
                    cm = New MySqlCommand("PRODISCOUNTS", con)
                    cm.CommandType = CommandType.StoredProcedure
                    If Not dgvFees.Rows(i).Cells(1).Value Is Nothing Then
                        cm.Parameters.AddWithValue("@PDiscountid", dgvFees.Rows(i).Cells("dgvAutoid").Value)
                        cm.Parameters.AddWithValue("@PStudentId", txtStudentId.Text)
                        cm.Parameters.AddWithValue("@PDiscount", dgvFees.Rows(i).Cells("dgvDiscount").Value)
                        cm.Parameters.AddWithValue("@PSemId", dgvFees.Rows(i).Cells("dgvSemAutoId").Value)
                        cm.ExecuteNonQuery()
                    End If
                Next
                tr.Commit()
                clear()
                MessageBox.Show("Record Saved")
            Catch ex As Exception
                tr.Rollback()
                MessageBox.Show(ex.ToString)
            End Try
        End If
       
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

    Private Sub clear()
        txtStudentId.Text = ""
        lblStudentName.Text = ""
        txtStudentSearch.Text = ""
        dgvFees.Rows.Clear()
    End Sub

    Private Sub frmDiscounts_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmDiscounts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmDiscounts_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.BackwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
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

    Private Sub txtStudentSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStudentSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                cm = New MySqlCommand("Discountsearch", con)
                cm.CommandType = CommandType.StoredProcedure
                cm.Parameters.AddWithValue("PStudentid", txtStudentId.Text)
                ad = New MySqlDataAdapter(cm)
                Dim t As New DataTable
                t.Clear()
                ad.Fill(t)
                If t.Rows.Count > 0 Then
                    lblStudentName.Text = t.Rows(0).Item(1) & "," & t.Rows(0).Item(2)
                    Dim i As Integer
                    For i = 0 To t.Rows.Count - 1
                        i = dgvFees.Rows.Add
                        dgvFees.Rows(i).Cells("dgvSemId").Value = t.Rows(i).Item("Semname")
                        dgvFees.Rows(i).Cells("dgvDiscount").Value = t.Rows(i).Item("Discount")
                        dgvFees.Rows(i).Cells("dgvAutoid").Value = t.Rows(i).Item("Discountid")
                        dgvFees.Rows(i).Cells("dgvSemAutoId").Value = t.Rows(i).Item("Semid")
                    Next
                Else
                    studentsearch()
                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End If

    End Sub
    Private Sub studentsearch()
        If txtStudentId.TextLength = 0 Then
            MessageBox.Show("Enter StudentId")
        Else
            cm = New MySqlCommand("select Studentname,Regno from student where studentid=" & txtStudentId.Text & "", con)
            ad = New MySqlDataAdapter(cm)
            Dim t1 As New DataTable
            t1.Clear()
            ad.Fill(t1)
            If t1.Rows.Count > 0 Then
                lblStudentName.Text = t1.Rows(0).Item(0) & "," & t1.Rows(0).Item(1)
                dgvFees.Focus()
                dgvFees.CurrentCell = dgvFees(0, 0)
            Else
                MessageBox.Show("No Record Found")
            End If
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
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clear()
    End Sub

    Private Sub txtStudentId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStudentId.KeyDown
        If e.KeyCode = Keys.Enter Then
            studentsearch()
        End If
    End Sub
End Class