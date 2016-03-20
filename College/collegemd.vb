Imports MySql.Data.MySqlClient

Module collegemd
    Public con As New MySqlConnection
    Public Email As String
    Public Username As String
    Public Password As String
    Public Contact As String
    Public Sub connection()
        con = New MySqlConnection("server=localhost;user id=root;database=college;allowuservariables=True")
        ' con = New MySqlConnection("server=muthupettaidargha.com;user id=muthupettai;Password=thillai123;check parameters=false;database=muthupet_billing")
        con.Open()
        Email = "mail.cctvinstallation.co.in"
        Password = "thillai123"
        Username = "durai@cctvinstallation.co.in"
        Contact = "ddurai123@yahoo.co.in"
    End Sub
    Private _SDate As String
    Public Property SDate() As String
        Get
            Return _SDate
        End Get
        Set(ByVal value As String)
            _SDate = value
        End Set
    End Property
    Private _EDate As String
    Public Property EDate() As String
        Get
            Return _EDate
        End Get
        Set(ByVal value As String)
            _EDate = value
        End Set
    End Property
    Private _PDate As String
    Public Property PDate() As Date
        Get
            Return _PDate
        End Get
        Set(ByVal value As Date)
            _PDate = value
        End Set
    End Property
    Private _Pyear As String
    Public Property Pyear() As String
        Get
            Return _Pyear
        End Get
        Set(ByVal value As String)
            _Pyear = value
        End Set
    End Property
    Private _Pmonth As String
    Public Property Pmonth() As String
        Get
            Return _Pmonth
        End Get
        Set(ByVal value As String)
            _Pmonth = value
        End Set
    End Property
    Private _PAccountid As String
    Public Property PAccountid() As String
        Get
            Return _PAccountid
        End Get
        Set(ByVal value As String)
            _PAccountid = value
        End Set
    End Property
    Private _Header As String
    Public Property Header() As String
        Get
            Return _Header
        End Get
        Set(ByVal value As String)
            _Header = value
        End Set
    End Property
    Private _collegeH As String
    Public Property collegeH() As String
        Get
            Return _collegeH
        End Get
        Set(ByVal value As String)
            _collegeH = value
        End Set
    End Property
    Private _Openingbalance As Integer
    Public Property Openingbalance() As Integer
        Get
            Return _Openingbalance
        End Get
        Set(ByVal value As Integer)
            _Openingbalance = value
        End Set
    End Property
    Private _Op As Integer
    Public Property Op() As Integer
        Get
            Return _Op
        End Get
        Set(ByVal value As Integer)
            _Op = value
        End Set
    End Property
    Private _Batch As Integer
    Public Property Batch() As Integer
        Get
            Return _Batch
        End Get
        Set(ByVal value As Integer)
            _Batch = value
        End Set
    End Property
End Module
