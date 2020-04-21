' Set Option Strict, really speeds up performance in vb.net and helps you create good code
Option Strict On

Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.ServiceProcess
Imports System.Environment

Public Class Webserver

    Inherits System.ServiceProcess.ServiceBase
    Private DoStop As Boolean = False
    Private ini As New IniReader(Replace(Mid(Environment.CommandLine, 1, InStrRev(Environment.CommandLine, "\") - 1) & "\simplehttp4.ini", Chr(34), ""))
#Region " Component Designer generated code "

    Public Sub New()
        MyBase.New()

        ' This call is required by the Component Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call

    End Sub

    'UserService overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ServicesToRun = New System.ServiceProcess.ServiceBase() {New Webserver}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
        Me.ServiceName = "SimpleHTTP4"
    End Sub

#End Region

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Start the webserver on a thread then exit
        Dim t As New Thread(AddressOf StartServer)

        ' Run the service thread in the background
        ' as low priority, as to not take up the processor
        t.IsBackground = True
        t.Priority = ThreadPriority.BelowNormal
        t.Start()

        ' NOTE If you want to convert this from a Service application to a standard app
        ' In your form load, new or main sub just call StartServer

    End Sub

    Protected Overrides Sub OnStop()
        ' This is called when the service is stopped.

        ' set the dostop variable so the thread exits normally
        Me.DoStop = True

    End Sub
    Public Sub StartServer()

        ' Get IP address of the adapter to run the server on
        ' All configuration settings are stored in .ini
        Dim address As IPAddress = IPAddress.Parse(ini.ReadString("SimpleHTTP", "IP"))
        ' map the end point with IP Address and Port
        Dim EndPoint As IPEndPoint = New IPEndPoint(address, Int32.Parse(ini.ReadString("SimpleHTTP", "Port")))

        ' Create a new socket and bind it to the address and port and listen.
        Dim ss As Socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        ss.Bind(EndPoint)
        ss.Listen(20)

        Dim WebRoot As String = ini.ReadString("SimpleHTTP", "Path", "c:\")

        Do While Not Me.DoStop
            ' Wait for an incoming connections
            Dim sock As Socket = ss.Accept()

            ' Connection accepted

            ' Initialise the Server class
            Dim ServerRun As New Server(sock, WebRoot)

            ' Create a new thread to handle the connection
            Dim t As Thread = New Thread(AddressOf ServerRun.HandleConnection)

            t.IsBackground = True
            t.Priority = ThreadPriority.BelowNormal
            t.Start()

            ' Loop and wait for more connections
        Loop

    End Sub
End Class
