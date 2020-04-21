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
Imports System.Text
Imports System.Configuration
Imports System.Environment

Public Class Server

    Private sMyWebServerRoot As String
    Private mySocket As Socket
    Private ini As New IniReader(Replace(Mid(Environment.CommandLine, 1, InStrRev(Environment.CommandLine, "\") - 1) & "\simplehttp4.ini", Chr(34), ""))

    Public Sub New(ByVal s As Socket, ByVal Location As String)
        Try
            mySocket = s
            If Microsoft.VisualBasic.Right(Location, 1) = "\" Then
                Location = Mid(Location, 1, Len(Location) - 1)
            End If
            sMyWebServerRoot = Location & "\"

        Catch ex As Exception
            'something went wrong
        End Try
    End Sub

    Public Function GetMimeType(ByVal filename As String) As String
        Try
            Dim ext As String
            If InStr(filename, ".") > 0 And InStr(filename, ".") < Len(filename) Then
                ext = Mid(filename, InStrRev(filename, ".") + 1)
            Else
                ext = ""
            End If
            ext = LCase(ext)
            Select Case ext

                Case Is = "gif"
                    Return "image/gif"

                Case Is = "jpg"
                    Return "image/jpeg"
                Case Is = "jpe"
                    Return "image/jpeg"
                Case Is = "jpeg"
                    Return "image/jpeg"

                Case Is = "png"
                    Return "image/png"

                Case Is = "htm"
                    Return "text/html"
                Case Is = "html"
                    Return "text/html"
                Case Is = "asp"
                    Return "text/html"
                Case Is = "jsp"
                    Return "text/html"

                Case Is = "js"
                    Return "application/javascript"

                Case Is = "swf"
                    Return "application/x-shockwave-flash"

                Case Is = "ico"
                    Return "application/x-icon"

                Case Is = "txt"
                    Return "text/plain"

                Case Else
                    Return "text/unknown"
            End Select

        Catch ex As Exception
            'something went wrong
        End Try
    End Function

    Public Sub HandleConnection()
        Try
            Dim iStartPos As Integer = 0
            Dim sRequest, sDirName, ssRequestedFile, sErrorMessage, sLocalDir As String

            Dim sPhysicalFilePath As String = ""
            Dim bReceive As Byte()
            ReDim bReceive(1024)

            ' Receive the request into a byte array
            Dim i As Integer = mySocket.Receive(bReceive, bReceive.Length, 0)

            ' Convert the byte array to a string
            Dim sbuffer As String = Encoding.ASCII.GetString(bReceive)

            iStartPos = sbuffer.IndexOf("HTTP", 1)

            ' Get the HTTP text and version e.g. it will return "HTTP/1.1"
            Dim sHttpVersion As String = sbuffer.Substring(iStartPos, 8)

            ' the web server only accepts get requests.
            If Mid(LCase(sbuffer), 1, 3) <> "get" Then
                'if not GET request then close socket and exit
                sErrorMessage = "."
                SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found")
                SendToBrowser(sErrorMessage)
                mySocket.Close()
                Return
            End If

            ' Extract path and filename from request
            sRequest = sbuffer.Substring(0, iStartPos - 1)
            sRequest.Replace("\\", "/")
            If ((sRequest.IndexOf(".") < 1) AndAlso (Not sRequest.EndsWith("/"))) Then
                sRequest = sRequest & "/"
            End If

            iStartPos = sRequest.LastIndexOf("/") + 1

            ' Get the filename
            ssRequestedFile = sRequest.Substring(iStartPos)

            ' Get the relative path
            sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/") - 3)

            ' Web server root path
            sLocalDir = sMyWebServerRoot

            ' Allow these files to be accessed
            If InStr(sRequest, "robots.txt") <> 0 Then
                ssRequestedFile = "robots.txt"
            ElseIf InStr(sRequest, "favicon.ico") <> 0 Then
                ssRequestedFile = "favicon.ico"

                ' Detect JavaScript reference and replace with error suppressor
            ElseIf InStr(sRequest, ".js") <> 0 Then
                If InStr(sRequest, ".jsp") <> 0 Then
                    ssRequestedFile = "default.htm"
                ElseIf InStr(sRequest, ".js?") <> 0 Then
                    ssRequestedFile = "default.htm"
                Else
                    ssRequestedFile = "default.js"
                End If

                ' Detect up to 5 local file variants
            ElseIf InStr(sRequest, "default001.gif") <> 0 Then
                ssRequestedFile = "default001.gif"
            ElseIf InStr(sRequest, "default002.gif") <> 0 Then
                ssRequestedFile = "default002.gif"
            ElseIf InStr(sRequest, "default003.gif") <> 0 Then
                ssRequestedFile = "default003.gif"
            ElseIf InStr(sRequest, "default004.gif") <> 0 Then
                ssRequestedFile = "default004.gif"
            ElseIf InStr(sRequest, "default005.gif") <> 0 Then
                ssRequestedFile = "default005.gif"
            ElseIf InStr(sRequest, "default001.jpg") <> 0 Then
                ssRequestedFile = "default001.jpg"
            ElseIf InStr(sRequest, "default002.jpg") <> 0 Then
                ssRequestedFile = "default002.jpg"
            ElseIf InStr(sRequest, "default003.jpg") <> 0 Then
                ssRequestedFile = "default003.jpg"
            ElseIf InStr(sRequest, "default004.jpg") <> 0 Then
                ssRequestedFile = "default004.jpg"
            ElseIf InStr(sRequest, "default005.jpg") <> 0 Then
                ssRequestedFile = "default005.jpg"
            ElseIf InStr(sRequest, "default001.swf") <> 0 Then
                ssRequestedFile = "default001.swf"
            ElseIf InStr(sRequest, "default002.swf") <> 0 Then
                ssRequestedFile = "default002.swf"
            ElseIf InStr(sRequest, "default003.swf") <> 0 Then
                ssRequestedFile = "default003.swf"
            ElseIf InStr(sRequest, "default004.swf") <> 0 Then
                ssRequestedFile = "default004.swf"
            ElseIf InStr(sRequest, "default005.swf") <> 0 Then
                ssRequestedFile = "default005.swf"
            ElseIf InStr(sRequest, "default001.htm") <> 0 Then
                ssRequestedFile = "default001.htm"
            ElseIf InStr(sRequest, "default002.htm") <> 0 Then
                ssRequestedFile = "default002.htm"
            ElseIf InStr(sRequest, "default003.htm") <> 0 Then
                ssRequestedFile = "default003.htm"
            ElseIf InStr(sRequest, "default004.htm") <> 0 Then
                ssRequestedFile = "default004.htm"
            ElseIf InStr(sRequest, "default005.htm") <> 0 Then
                ssRequestedFile = "default005.htm"

                ' Detect image and replace with same image type
            ElseIf InStr(sRequest, ".gif") <> 0 Then
                ssRequestedFile = "default.gif"
            ElseIf InStr(sRequest, ".jpg") <> 0 Then
                ssRequestedFile = "default.jpg"
            ElseIf InStr(sRequest, ".jpe") <> 0 Then
                ssRequestedFile = "default.jpg"
            ElseIf InStr(sRequest, ".jpeg") <> 0 Then
                ssRequestedFile = "default.jpg"
            ElseIf InStr(sRequest, ".png") <> 0 Then
                ssRequestedFile = "default.png"
            ElseIf InStr(sRequest, ".swf") <> 0 Then
                ssRequestedFile = "default.swf"

                ' Detect advertisements to replace with a blank image
            ElseIf InStr(sRequest, "image") <> 0 Then
                ssRequestedFile = "default.gif"
            ElseIf InStr(sRequest, "ad") <> 0 Then
                ssRequestedFile = "default.gif"
            ElseIf InStr(sRequest, "adimg") <> 0 Then
                ssRequestedFile = "default.gif"
            ElseIf InStr(sRequest, "adserv") <> 0 Then
                ssRequestedFile = "default.gif"
            ElseIf InStr(sRequest, "advert") <> 0 Then
                ssRequestedFile = "default.gif"
            ElseIf InStr(sRequest, "realmedia") <> 0 Then
                ssRequestedFile = "default.gif"

                ' Anything else redirect to default document
            Else
                ssRequestedFile = "default.htm"
            End If

            ' get the mime type for the requested file
            Dim sMimeType As String = GetMimeType(ssRequestedFile)
            If sMimeType = "text/unknown" Then
                ' unknown type
                sErrorMessage = "."
                SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found")
                SendToBrowser(sErrorMessage)
                mySocket.Close()
                Return
            End If

            ' Build the complete path to the files
            sPhysicalFilePath = sLocalDir & ssRequestedFile

            ' Create File Stream of filename
            Dim fs As FileStream = New FileStream(sPhysicalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)
            ' create reader
            Dim reader As BinaryReader = New BinaryReader(fs)

            ' Create byte array buffer
            Dim bytes As Byte()
            ReDim bytes(CInt(fs.Length))

            ' Read file into byte array
            bytes = reader.ReadBytes(CInt(fs.Length))

            ' Total length of file
            Dim totbytes As Integer = CInt(fs.Length)

            ' close the reader and file stream
            reader.Close()
            fs.Close()

            ' Send HTTP header
            SendHeader(sHttpVersion, sMimeType, totbytes, " 200 OK")

            ' Send File
            SendToBrowser(bytes)

            ' All done for this connection!
            mySocket.Close()

        Catch ex As Exception
            'something went wrong
        Finally
            mySocket.Close()
        End Try
    End Sub

    Private Sub SendHeader(ByVal sHttpVersion As String, ByVal sMIMEHeader As String, ByVal iTotBytes As Integer, ByVal sStatusCode As String)
        Try
            Dim sBuffer As String = ""

            ' if Mime type is not provided set default
            If (sMIMEHeader.Length = 0) Then sMIMEHeader = "text/html"

            sBuffer = sBuffer & sHttpVersion & sStatusCode & vbNewLine
            sBuffer = sBuffer & "Date: Mon, 1 Jan 1999 11:11:11 GMT" & vbNewLine
            sBuffer = sBuffer & "Server: SimpleHTTP/3.0.0.3" & vbNewLine
            sBuffer = sBuffer & "Content-Length: " & iTotBytes & vbNewLine
            sBuffer = sBuffer & "Content-Type: " & sMIMEHeader & vbNewLine
            sBuffer = sBuffer & "Cache-Control: no-store, no-cache, must-revalidate" & vbNewLine & vbNewLine
            Dim bSendData As Byte() = Encoding.ASCII.GetBytes(sBuffer)
            SendToBrowser(bSendData)

        Catch ex As Exception
            'something went wrong
        End Try
    End Sub

    Private Sub SendToBrowser(ByVal sData As String)
        Try
            SendToBrowser(Encoding.ASCII.GetBytes(sData))

        Catch ex As Exception
            'something went wrong
        End Try
    End Sub

    Private Sub SendToBrowser(ByVal bSendData As Byte())
        Try
            If (mySocket.Connected) Then
                Dim numbytes As Integer
                numbytes = mySocket.Send(bSendData, bSendData.Length, 0)
            Else
                'do nothing
            End If

        Catch ex As Exception
            'something went wrong
        End Try
    End Sub

End Class