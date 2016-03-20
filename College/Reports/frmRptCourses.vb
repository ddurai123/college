Imports Microsoft.Reporting.WinForms
Imports System.Net.Mail
Imports System.IO

Public Class frmRptCourses

    Private Sub frmRptCourses_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ReportViewer1.LocalReport.ReleaseSandboxAppDomain()
    End Sub

    Private Sub frmRptCourses_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.class_viewTableAdapter.Fill(Me.collegeDataSet.class_view)
        Me.shopdetailsTableAdapter.Fill(Me.collegeDataSet.shopdetails)
        Me.ReportViewer1.RefreshReport()
        Panel1.Visible = False
        frmRptCourses.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub ReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportViewer1.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Panel1.Visible = True
        Dim warnings As Warning()
        Dim streamids As String()
        Dim mimeType As String
        Dim encoding As String
        Dim filenameExtension As String
        Dim deviceInfo As String = "<DeviceInfo><OutputFormat>PDF</OutputFormat>" & _
                             "<PageWidth>8.27in</PageWidth><PageHeight>11.69in</PageHeight><MarginTop>0in</MarginTop><MarginLeft>0in</MarginLeft><MarginRight>0in</MarginRight><MarginBottom>0in</MarginBottom></DeviceInfo>"
        Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, filenameExtension, streamids, warnings)
        ReportViewer1.LocalReport.Refresh()
        'Dim fs As System.IO.FileStream = System.IO.File.Create("d:/durai1.pdf")
        'fs.Write(bytes, 0, bytes.Length)
        'fs.Close()
        Dim s As New MemoryStream(bytes)
        s.Seek(0, SeekOrigin.Begin)
        Dim a As New Attachment(s, "Courses.pdf")
        Dim Mail As New MailMessage
        Dim SMTP As New SmtpClient(Email)
        SMTP.UseDefaultCredentials = False
        SMTP.Credentials = New System.Net.NetworkCredential(Username, Password)
        Mail.Subject = "Courses Offered"
        Mail.From = New MailAddress(Username)
        Mail.Body = "Please find the attachment here with enclosed"
        Mail.To.Add(Contact)
        Mail.Attachments.Add(a)
        SMTP.EnableSsl = False
        SMTP.Send(Mail)

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Panel1.Visible = False
    End Sub
End Class