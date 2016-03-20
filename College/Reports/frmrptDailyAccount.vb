Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Net.Mail

Public Class frmrptDailyAccount
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        PictureBox1.Visible = True
        ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "\Reports\rptDailyAccount.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim ds As New collegeDataSet
        Dim cm1 As MySqlCommand
        Dim da As New MySqlDataAdapter
        ds.EnforceConstraints = False
        If Op = 1 Then
            cm1 = New MySqlCommand("ACCOUNTSUMMARYDAY", con)
            cm1.CommandType = CommandType.StoredProcedure
            cm1.Parameters.AddWithValue("@PAccountid", PAccountid)
            cm1.Parameters.AddWithValue("@PDate", Format(PDate, "yyyy-MM-dd"))
            da = New MySqlDataAdapter(cm1)
            ds.Clear()
            da.Fill(ds.Tables(0))
        ElseIf Op = 2 Then
            cm1 = New MySqlCommand("ACCOUNTSUMMARY", con)
            cm1.CommandType = CommandType.StoredProcedure
            cm1.Parameters.AddWithValue("@PAccountid", PAccountid)
            cm1.Parameters.AddWithValue("@Pyear", Pyear)
            cm1.Parameters.AddWithValue("@PMonth", Pmonth)
            ds.Clear()
            da = New MySqlDataAdapter(cm1)
            da.Fill(ds.Tables(0))
        End If
        Dim Header2 As New ReportParameter("Header2", collegeH)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {Header2})
        Dim Header1 As New ReportParameter("Header1", Header)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {Header1})
        Dim Op1 As New ReportParameter("OpeningBalance", Openingbalance)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {Op1})
        Dim datasource As New ReportDataSource("DataSet1", ds.Tables(0))
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(datasource)
        ReportViewer1.RefreshReport()
        PictureBox1.Visible = False
    End Sub

    Private Sub frmrptDailyAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False
        BackgroundWorker1.RunWorkerAsync()
    End Sub
    Private Sub btnEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmail.Click
        BackgroundWorker2.RunWorkerAsync()
    End Sub


    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            PictureBox1.Visible = True
            Dim warnings As Warning()
            Dim streamids As String()
            Dim mimeType As String
            Dim encoding As String
            Dim filenameExtension As String
            Dim deviceInfo As String = "<DeviceInfo><OutputFormat>PDF</OutputFormat>" & _
                                 "<PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
            Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, filenameExtension, streamids, warnings)
            ReportViewer1.LocalReport.Refresh()
            Dim s As New MemoryStream(bytes)
            s.Seek(0, SeekOrigin.Begin)
            Dim a As New Attachment(s, Format(DateTime.Now, "ddMMMyyyy") & ".pdf")
            Dim Mail As New MailMessage
            Dim SMTP As New SmtpClient(Email)
            SMTP.UseDefaultCredentials = False
            SMTP.Credentials = New System.Net.NetworkCredential(Username, Password)
            Mail.Subject = Header
            Mail.From = New MailAddress(Username)
            Mail.Body = "Please find the attachment of" & Header
            Mail.To.Add(Contact)
            Mail.Attachments.Add(a)
            SMTP.EnableSsl = False
            SMTP.Send(Mail)
            PictureBox1.Visible = False
            MessageBox.Show("E-Mail Sent")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        
    End Sub

    Private Sub ReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportViewer1.Load

    End Sub
End Class