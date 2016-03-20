Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms

Public Class frmRptStudent

    Private Sub frmRptStudent_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub frmRptStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        BackgroundWorker1.RunWorkerAsync()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        PictureBox1.Visible = True
        ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "\Reports\rptStudents.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim ds As New collegeDataSet
        Dim cm As New MySqlCommand
        cm = New MySqlCommand("REPSTUDENT", con)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.AddWithValue("@PBatch", PAccountid)
        cm.Parameters.AddWithValue("@PStatus", Op)
        Dim da As New MySqlDataAdapter(cm)
        ds.EnforceConstraints = False
        da.Fill(ds.Tables(0))
        Dim datasource As New ReportDataSource("DataSet1", ds.Tables(0))
        Dim Header1 As New ReportParameter("Header1", Header)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {Header1})
        Dim Batch1 As New ReportParameter("Batch1", Batch)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {Batch1})
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(datasource)
        ReportViewer1.RefreshReport()
        PictureBox1.Visible = False
    End Sub

    Private Sub ReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportViewer1.Load

    End Sub
End Class