
Imports System
Imports System.Text
Imports System.Collections
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Public Class IniReader
   Private Declare Ansi Function GetPrivateProfileInt Lib "kernel32.dll" Alias "GetPrivateProfileIntA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal nDefault As Integer, ByVal lpFileName As String) As Integer
   Private Declare Ansi Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
   Private Declare Ansi Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
   Private Declare Ansi Function GetPrivateProfileSectionNames Lib "kernel32" Alias "GetPrivateProfileSectionNamesA" (ByVal lpszReturnBuffer() As Byte, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
   Private Declare Ansi Function WritePrivateProfileSection Lib "kernel32.dll" Alias "WritePrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
   Public Sub New(ByVal file As String)
        Filename = file
    End Sub
    '/// <summary>Gets orelse sets the full path to the INI file.</summary>
    '/// <value>A String representing the full path to the INI file.</value>
    Public Property Filename() As String
        Get
            Return m_Filename
        End Get
        Set(ByVal Value As String)
            m_Filename = Value
        End Set
    End Property
    '/// <summary>Gets orelse sets the section you're working in. (aka 'the active section')</summary>
    '/// <value>A String representing the section you're working in.</value>
    Public Property Section() As String
        Get
            Return m_Section
        End Get
        Set(ByVal Value As String)
            m_Section = Value
        End Set
    End Property
    '/// <summary>Reads an Integer from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <param name="defVal">The value to return if the specified key isn't found.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns the default value if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadInteger(ByVal section As String, ByVal key As String, ByVal defVal As Integer) As Integer
        Return GetPrivateProfileInt(section, key, defVal, Filename)
    End Function
    '/// <summary>Reads an Integer from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns 0 if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadInteger(ByVal section As String, ByVal key As String) As Integer
        Return ReadInteger(section, key, 0)
    End Function
    '/// <summary>Reads an Integer from the specified key of the active section.</summary>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <param name="defVal">The section to search in.</param>
    '/// <returns>Returns the value of the specified Key, orelse returns the default value if the specified Key isn't found in the active section of the INI file.</returns>
    Public Function ReadInteger(ByVal key As String, ByVal defVal As Integer) As Integer
        Return ReadInteger(Section, key, defVal)
    End Function
    '/// <summary>Reads an Integer from the specified key of the active section.</summary>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified key, orelse returns 0 if the specified key isn't found in the active section of the INI file.</returns>
    Public Function ReadInteger(ByVal key As String) As Integer
        Return ReadInteger(key, 0)
    End Function
    '/// <summary>Reads a String from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <param name="defVal">The value to return if the specified key isn't found.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns the default value if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadString(ByVal section As String, ByVal key As String, ByVal defVal As String) As String
        Dim sb As New StringBuilder(MAX_ENTRY)
        Dim Ret As Integer = GetPrivateProfileString(section, key, defVal, sb, MAX_ENTRY, Filename)
        Return sb.ToString()
    End Function
    '/// <summary>Reads a String from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns an empty String if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadString(ByVal section As String, ByVal key As String) As String
        Return ReadString(section, key, "")
    End Function
    '/// <summary>Reads a String from the specified key of the active section.</summary>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified key, orelse returns an empty String if the specified key isn't found in the active section of the INI file.</returns>
    Public Function ReadString(ByVal key As String) As String
        Return ReadString(Section, key)
    End Function
    '/// <summary>Reads a Long from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <param name="defVal">The value to return if the specified key isn't found.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns the default value if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadLong(ByVal section As String, ByVal key As String, ByVal defVal As Long) As Long
        Return Long.Parse(ReadString(section, key, defVal.ToString()))
    End Function
    '/// <summary>Reads a Long from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns 0 if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadLong(ByVal section As String, ByVal key As String) As Long
        Return ReadLong(section, key, 0)
    End Function
    '/// <summary>Reads a Long from the specified key of the active section.</summary>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <param name="defVal">The section to search in.</param>
    '/// <returns>Returns the value of the specified key, orelse returns the default value if the specified key isn't found in the active section of the INI file.</returns>
    Public Function ReadLong(ByVal key As String, ByVal defVal As Long) As Long
        Return ReadLong(Section, key, defVal)
    End Function
    '/// <summary>Reads a Long from the specified key of the active section.</summary>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified Key, orelse returns 0 if the specified Key isn't found in the active section of the INI file.</returns>
    Public Function ReadLong(ByVal key As String) As Long
        Return ReadLong(key, 0)
    End Function
    '/// <summary>Reads a Byte array from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns null (Nothing in VB.NET) if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadByteArray(ByVal section As String, ByVal key As String) As Byte()
        Try
            Return Convert.FromBase64String(ReadString(section, key))
        Catch
        End Try
        Return Nothing
    End Function
    '/// <summary>Reads a Byte array from the specified key of the active section.</summary>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified key, orelse returns null (Nothing in VB.NET) if the specified key pair isn't found in the active section of the INI file.</returns>
    Public Function ReadByteArray(ByVal key As String) As Byte()
        Return ReadByteArray(Section, key)
    End Function
    '/// <summary>Reads a Boolean from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <param name="defVal">The value to return if the specified key isn't found.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns the default value if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadBoolean(ByVal section As String, ByVal key As String, ByVal defVal As Boolean) As Boolean
        Return Boolean.Parse(ReadString(section, key, defVal.ToString()))
    End Function
    '/// <summary>Reads a Boolean from the specified key of the specified section.</summary>
    '/// <param name="section">The section to search in.</param>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified section/key pair, orelse returns false if the specified section/key pair isn't found in the INI file.</returns>
    Public Function ReadBoolean(ByVal section As String, ByVal key As String) As Boolean
        Return ReadBoolean(section, key, False)
    End Function
    '/// <summary>Reads a Boolean from the specified key of the specified section.</summary>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <param name="defVal">The value to return if the specified key isn't found.</param>
    '/// <returns>Returns the value of the specified key pair, orelse returns the default value if the specified key isn't found in the active section of the INI file.</returns>
    Public Function ReadBoolean(ByVal key As String, ByVal defVal As Boolean) As Boolean
        Return ReadBoolean(Section, key, defVal)
    End Function
    '/// <summary>Reads a Boolean from the specified key of the specified section.</summary>
    '/// <param name="key">The key from which to return the value.</param>
    '/// <returns>Returns the value of the specified key, orelse returns false if the specified key isn't found in the active section of the INI file.</returns>
    Public Function ReadBoolean(ByVal key As String) As Boolean
        Return ReadBoolean(Section, key)
    End Function
    '/// <summary>Writes an Integer to the specified key in the specified section.</summary>
    '/// <param name="section">The section to write in.</param>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal section As String, ByVal key As String, ByVal value As Integer) As Boolean
        Return Write(section, key, value.ToString())
    End Function
    '/// <summary>Writes an Integer to the specified key in the active section.</summary>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal key As String, ByVal value As Integer) As Boolean
        Return Write(Section, key, value)
    End Function
    '/// <summary>Writes a String to the specified key in the specified section.</summary>
    '/// <param name="section">Specifies the section to write in.</param>
    '/// <param name="key">Specifies the key to write to.</param>
    '/// <param name="value">Specifies the value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal section As String, ByVal key As String, ByVal value As String) As Boolean
        Return (WritePrivateProfileString(section, key, value, Filename) <> 0)
    End Function
    '/// <summary>Writes a String to the specified key in the active section.</summary>
    '///	<param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal key As String, ByVal value As String) As Boolean
        Return Write(Section, key, value)
    End Function
    '/// <summary>Writes a Long to the specified key in the specified section.</summary>
    '/// <param name="section">The section to write in.</param>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal section As String, ByVal key As String, ByVal value As Long) As Boolean
        Return Write(section, key, value.ToString())
    End Function
    '/// <summary>Writes a Long to the specified key in the active section.</summary>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal key As String, ByVal value As Long) As Boolean
        Return Write(Section, key, value)
    End Function
    '/// <summary>Writes a Byte array to the specified key in the specified section.</summary>
    '/// <param name="section">The section to write in.</param>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal section As String, ByVal key As String, ByVal value() As Byte) As Boolean
        If value Is Nothing Then
            Return Write(section, key, CType(Nothing, String))
        Else
            Return Write(section, key, value, 0, value.Length)
        End If
    End Function
    '/// <summary>Writes a Byte array to the specified key in the active section.</summary>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal key As String, ByVal value() As Byte) As Boolean
        Return Write(Section, key, value)
    End Function
    '/// <summary>Writes a Byte array to the specified key in the specified section.</summary>
    '/// <param name="section">The section to write in.</param>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <param name="offset">An offset in <i>value</i>.</param>
    '/// <param name="length">The number of elements of <i>value</i> to convert.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal section As String, ByVal key As String, ByVal value() As Byte, ByVal offset As Integer, ByVal length As Integer) As Boolean
        If value Is Nothing Then
            Return Write(section, key, CType(Nothing, String))
        Else
            Return Write(section, key, Convert.ToBase64String(value, offset, length))
        End If
    End Function
    '/// <summary>Writes a Boolean to the specified key in the specified section.</summary>
    '/// <param name="section">The section to write in.</param>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal section As String, ByVal key As String, ByVal value As Boolean) As Boolean
        Return Write(section, key, value.ToString())
    End Function
    '/// <summary>Writes a Boolean to the specified key in the active section.</summary>
    '/// <param name="key">The key to write to.</param>
    '/// <param name="value">The value to write.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function Write(ByVal key As String, ByVal value As Boolean) As Boolean
        Return Write(Section, key, value)
    End Function
    '/// <summary>Deletes a key from the specified section.</summary>
    '/// <param name="section">The section to delete from.</param>
    '/// <param name="key">The key to delete.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function DeleteKey(ByVal section As String, ByVal key As String) As Boolean
        Return (WritePrivateProfileString(section, key, Nothing, Filename) <> 0)
    End Function
    '/// <summary>Deletes a key from the active section.</summary>
    '/// <param name="key">The key to delete.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function DeleteKey(ByVal key As String) As Boolean
        Return (WritePrivateProfileString(Section, key, Nothing, Filename) <> 0)
    End Function
    '/// <summary>Deletes a section from an INI file.</summary>
    '/// <param name="section">The section to delete.</param>
    '/// <returns>Returns true if the function succeeds, false otherwise.</returns>
    Public Function DeleteSection(ByVal section As String) As Boolean
        Return WritePrivateProfileSection(section, Nothing, Filename) <> 0
    End Function
    '/// <summary>Retrieves a list of all available sections in the INI file.</summary>
    '/// <returns>Returns an ArrayList with all available sections.</returns>
    Public Function GetSectionNames() As ArrayList
        Try
            Dim buffer(MAX_ENTRY) As Byte
            GetPrivateProfileSectionNames(buffer, MAX_ENTRY, Filename)
            Dim parts() As String = Encoding.ASCII.GetString(buffer).Trim(ControlChars.NullChar).Split(ControlChars.NullChar)
            Return New ArrayList(parts)
        Catch
        End Try
        Return Nothing
    End Function
    'Private variables and constants
    '/// <summary>
    '/// Holds the full path to the INI file.
    '/// </summary>
    Private m_Filename As String
    '/// <summary>
    '/// Holds the active section name
    '/// </summary>
    Private m_Section As String
    '/// <summary>
    '/// The maximum number of bytes in a section buffer.
    '/// </summary>
    Private Const MAX_ENTRY As Integer = 32768
End Class

