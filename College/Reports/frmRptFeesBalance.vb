Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Public Class frmRptFeesBalance
    Public bid As String
    Public Sfrom As String
    Public Sto As String
    Private Sub frmRptFeesBalance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "\Reports\rptFeesBalance.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim ds As New collegeDataSet
        Dim cm As New MySqlCommand
        cm = New MySqlCommand("REPFEESBALANCE", con)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.AddWithValue("@PBatchId", bid)
        cm.Parameters.AddWithValue("@PSemfrom", Sfrom)
        cm.Parameters.AddWithValue("@PSemto", Sto)
        Dim da As New MySqlDataAdapter(cm)
        ds.EnforceConstraints = False
        da.Fill(ds.Tables(0))
        Dim datasource As New ReportDataSource("DataSet1", ds.Tables(0))
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(datasource)
        ReportViewer1.RefreshReport()
    End Sub

    Private Sub ReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportViewer1.Load

    End Sub
End Class