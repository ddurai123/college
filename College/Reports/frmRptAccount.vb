Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Imports System.Net.Mail
Imports System.IO

Public Class frmRptAccount

    Private Sub frmRptAccount_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub frmRptAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        BackgroundWorker1.RunWorkerAsync()
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        PictureBox1.Visible = True
        ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "\Reports\rptAccount.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim ds As New collegeDataSet
        Dim cm, cm1 As MySqlCommand
        cm = New MySqlCommand("PRO_MONTHBALANCE", con)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.AddWithValue("@PAccountid", PAccountid)
        cm.Parameters.AddWithValue("@Pyear", Pyear)
        cm.Parameters.AddWithValue("@PMonth", Pmonth)
        Dim da As New MySqlDataAdapter(cm)
        ds.EnforceConstraints = False
        da.Fill(ds.Tables(1))

        cm1 = New MySqlCommand("ACCOUNTSUMMARY", con)
        cm1.CommandType = CommandType.StoredProcedure
        cm1.Parameters.AddWithValue("@PAccountid", PAccountid)
        cm1.Parameters.AddWithValue("@Pyear", Pyear)
        cm1.Parameters.AddWithValue("@PMonth", Pmonth)
        da = New MySqlDataAdapter(cm1)
        da.Fill(ds.Tables(0))
        Dim Header1 As New ReportParameter("Header1", Header)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {Header1})
        Dim Op1 As New ReportParameter("Openingbalance", Openingbalance)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {Op1})
        Dim datasource As New ReportDataSource("DataSet1", ds.Tables(0))
        Dim datasource1 As New ReportDataSource("DataSet2", ds.Tables(1))
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(datasource)
        ReportViewer1.LocalReport.DataSources.Add(datasource1)
        ReportViewer1.RefreshReport()
        PictureBox1.Visible = False
    End Sub


    Private Sub btnEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmail.Click
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
        Dim a As New Attachment(s, Format(DateTime.Now, "dd/MMM/yyyy") & ".pdf")
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
    End Sub
End Class