Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms

Public Class frmRptStudentAccount

    Private Sub frmRptStudentAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub ReportViewer1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportViewer1.Load
        ReportViewer1.LocalReport.ReportPath = System.Environment.CurrentDirectory & "\Reports\rptStudentAccount.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim ds As New collegeDataSet
        Dim cm As New MySqlCommand
        cm = New MySqlCommand("REPSTUDENTACCOUNT", con)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.AddWithValue("@PStudentid", frmStudentSearch.datagrid1.CurrentRow.Cells(0).Value)
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
    End Sub
End Class