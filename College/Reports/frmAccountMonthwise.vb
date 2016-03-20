Imports MySql.Data.MySqlClient

Public Class frmAccountMonthwise
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            PAccountid = cmbAccount.SelectedValue
            PDate = txtYear.Text & "-" & cmbMonth.SelectedValue & "-" & "01"
            Dim cm As MySqlCommand
            cm = New MySqlCommand("PRO_Openingbalance", con)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.AddWithValue("PDate", PDate)
            cm.Parameters.AddWithValue("PAccountid", PAccountid)
            Dim ad As MySqlDataAdapter
            Dim t As New DataTable
            t.Clear()
            ad = New MySqlDataAdapter(cm)
            ad.Fill(t)
            Openingbalance = t.Rows(0).Item(0)
            Pyear = txtYear.Text
            Pmonth = cmbMonth.SelectedValue
            Op = 2
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        Header = UCase(cmbAccount.Text) & " REPORT FOR THE MONTH OF " & UCase(cmbMonth.Text) & "-" & txtYear.Text
        frmrptDailyAccount.Show()
    End Sub

    Private Sub frmAccountMonthwise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tb, tb1 As New DataTable
        tb.Columns.Add("Month", GetType(String))
        tb.Columns.Add("Value", GetType(Integer))
        tb.Rows.Add("January", 1)
        tb.Rows.Add("February", 2)
        tb.Rows.Add("March", 3)
        tb.Rows.Add("April", 4)
        tb.Rows.Add("May", 5)
        tb.Rows.Add("June", 6)
        tb.Rows.Add("July", 7)
        tb.Rows.Add("August", 8)
        tb.Rows.Add("September", 9)
        tb.Rows.Add("October", 10)
        tb.Rows.Add("November", 11)
        tb.Rows.Add("December", 12)
        cmbMonth.DataSource = tb
        cmbMonth.DisplayMember = "Month"
        cmbMonth.ValueMember = "Value"
        Try
            Dim cm As New MySqlCommand
            cm = New MySqlCommand("select AccountId,Accountname from  account_group", con)
            Dim ad As New MySqlDataAdapter
            Dim t, t2 As New DataTable
            t.Clear()
            ad = New MySqlDataAdapter(cm)
            ad.Fill(t)
            cmbAccount.DataSource = t
            cmbAccount.DisplayMember = "Accountname"
            cmbAccount.ValueMember = "AccountId"
            cmbAccount.SelectedIndex = 0
            ad = New MySqlDataAdapter("select shopname from shopdetails", con)
            t2.Clear()
            ad.Fill(t2)
            collegeH = t2.Rows(0).Item(0)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        txtYear.Text = Format(DateTime.Now, "yyyy")
    End Sub

    Private Sub frmAccountMonthwise_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.CadetBlue,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.ForwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class