<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptCourses
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Me.class_viewBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.collegeDataSet = New College.collegeDataSet()
        Me.shopdetailsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.class_viewTableAdapter = New College.collegeDataSetTableAdapters.class_viewTableAdapter()
        Me.shopdetailsTableAdapter = New College.collegeDataSetTableAdapters.shopdetailsTableAdapter()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.class_viewBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.collegeDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.shopdetailsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'class_viewBindingSource
        '
        Me.class_viewBindingSource.DataMember = "class_view"
        Me.class_viewBindingSource.DataSource = Me.collegeDataSet
        '
        'collegeDataSet
        '
        Me.collegeDataSet.DataSetName = "collegeDataSet"
        Me.collegeDataSet.EnforceConstraints = False
        Me.collegeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'shopdetailsBindingSource
        '
        Me.shopdetailsBindingSource.DataMember = "shopdetails"
        Me.shopdetailsBindingSource.DataSource = Me.collegeDataSet
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "DataSet1"
        ReportDataSource1.Value = Me.class_viewBindingSource
        ReportDataSource2.Name = "DataSet2"
        ReportDataSource2.Value = Me.shopdetailsBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "College.Report1.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ShowFindControls = False
        Me.ReportViewer1.Size = New System.Drawing.Size(1240, 484)
        Me.ReportViewer1.TabIndex = 0
        '
        'class_viewTableAdapter
        '
        Me.class_viewTableAdapter.ClearBeforeFill = True
        '
        'shopdetailsTableAdapter
        '
        Me.shopdetailsTableAdapter.ClearBeforeFill = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button1.Image = Global.College.My.Resources.Resources.gmail
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(467, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(111, 24)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Send E-Mail"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.College.My.Resources.Resources.wait
        Me.PictureBox1.Location = New System.Drawing.Point(19, 21)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(394, 147)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'BackgroundWorker1
        '
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(358, 169)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(428, 186)
        Me.Panel1.TabIndex = 3
        '
        'frmRptCourses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1240, 484)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "frmRptCourses"
        Me.Text = "frmRptCourses"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.class_viewBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.collegeDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.shopdetailsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents class_viewBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents collegeDataSet As College.collegeDataSet
    Friend WithEvents shopdetailsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents class_viewTableAdapter As College.collegeDataSetTableAdapters.class_viewTableAdapter
    Friend WithEvents shopdetailsTableAdapter As College.collegeDataSetTableAdapters.shopdetailsTableAdapter
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
