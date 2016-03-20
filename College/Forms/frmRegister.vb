Imports MySql.Data.MySqlClient

Public Class frmRegister

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim tr As MySqlTransaction
        tr = con.BeginTransaction
        Try
            Dim cm As MySqlCommand
            cm = New MySqlCommand("truncate table shopdetails", con)
            cm.ExecuteNonQuery()
            cm = New MySqlCommand("insert into shopdetails(Contact1,Contact2,Shopname,Addressline1,Addressline2,Addressline3,Email) values(@Contact1,@Contact2,@Shopname,@Addressline1,@Addressline2,@Addresssline3,@Email)", con)
            cm.Parameters.AddWithValue("@Contact1", txtContact1.Text)
            cm.Parameters.AddWithValue("@Contact2", txtContact2.Text)
            cm.Parameters.AddWithValue("@Shopname", txtShopname.Text)
            cm.Parameters.AddWithValue("@Addressline1", txtAddressline1.Text)
            cm.Parameters.AddWithValue("@Addressline2", txtAddressline2.Text)
            cm.Parameters.AddWithValue("@Addresssline3", txtAddressline3.Text)
            cm.Parameters.AddWithValue("@Email", txtEmail.Text)
            cm.ExecuteNonQuery()
            tr.Commit()
            MessageBox.Show("Record Saved")
            cm.Dispose()
        Catch ex As Exception
            tr.Rollback()
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Private Sub grid()
        Try
            Dim ad As New MySqlDataAdapter
            Dim t As New DataTable
            t.Clear()
            ad = New MySqlDataAdapter("select * from shopdetails", con)
            ad.Fill(t)
            txtContact1.Text = t.Rows(0).Item("Contact1")
            txtContact2.Text = t.Rows(0).Item("Contact2")
            txtShopname.Text = t.Rows(0).Item("Shopname")
            txtAddressline1.Text = t.Rows(0).Item("Addressline1")
            txtAddressline2.Text = t.Rows(0).Item("Addressline2")
            txtAddressline3.Text = t.Rows(0).Item("Addressline3")
            txtEmail.Text = t.Rows(0).Item("Email")
            ad.Dispose()
            t.Dispose()
        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub frmRegistration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grid()
    End Sub

    Private Sub frmRegister_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim TheControl As Control = CType(sender, Control)
        Dim oRAngle As Rectangle = New Rectangle(0, 0, TheControl.Width, TheControl.Height)
        Dim oGradientBrush As Brush = New Drawing.Drawing2D.LinearGradientBrush(
                                      oRAngle, Color.WhiteSmoke,
                                      Color.Silver,
                                      Drawing.Drawing2D _
                                      .LinearGradientMode.BackwardDiagonal)
        e.Graphics.FillRectangle(oGradientBrush, oRAngle)
    End Sub
End Class