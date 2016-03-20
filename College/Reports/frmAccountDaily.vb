Imports MySql.Data.MySqlClient

Public Class frmAccountDaily

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            PAccountid = ComboBox3.SelectedValue
            PDate = ddlDate.Value
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
            Op = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        Header = UCase(ComboBox3.Text) & " REPORT ON " & Format(ddlDate.Value, "dd/MMMM/yyyy")
        frmrptDailyAccount.Show()
    End Sub

    Private Sub frmAccountDaily_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim cm As New MySqlCommand
            cm = New MySqlCommand("select AccountId,Accountname from  account_group", con)
            Dim ad As New MySqlDataAdapter
            Dim t, t2 As New DataTable
            t.Clear()
            ad = New MySqlDataAdapter(cm)
            ad.Fill(t)
            ComboBox3.DataSource = t
            ComboBox3.DisplayMember = "Accountname"
            ComboBox3.ValueMember = "AccountId"
            ComboBox3.SelectedIndex = 0
            ad = New MySqlDataAdapter("select shopname from shopdetails", con)
            t2.Clear()
            ad.Fill(t2)
            collegeH = t2.Rows(0).Item(0)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub frmAccountDaily_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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