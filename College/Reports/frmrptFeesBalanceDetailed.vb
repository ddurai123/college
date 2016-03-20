Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms

Public Class frmrptFeesBalanceDetailed
    Public bid As String
    Public Sfrom As String
    Public Sto As String
    Private Sub ReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportViewer1.Load

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "\Reports\rptFeesBalanceDetailed.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim ds As New collegeDataSet
        Dim cm As New MySqlCommand
        cm = New MySqlCommand("REPDETAILEDFEES", con)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.AddWithValue("@PBatchid", bid)
        cm.Parameters.AddWithValue("@PSemfrom", Sfrom)
        cm.Parameters.AddWithValue("@PSemto", Sto)
        Dim da As New MySqlDataAdapter(cm)
        ds.EnforceConstraints = False
        da.Fill(ds.Tables(0))
        da = New MySqlDataAdapter("select * from shopdetails", con)
        ds.EnforceConstraints = False
        da.Fill(ds.Tables(1))
        Dim datasource As New ReportDataSource("DataSet1", ds.Tables(0))
        Dim datasource1 As New ReportDataSource("DataSet2", ds.Tables(1))
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(datasource)
        ReportViewer1.LocalReport.DataSources.Add(datasource1)
        ReportViewer1.RefreshReport()
        PictureBox1.Visible = False
    End Sub

    Private Sub frmrptFeesBalanceDetailed_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckForIllegalCrossThreadCalls = False
        BackgroundWorker1.RunWorkerAsync()
    End Sub
End Class