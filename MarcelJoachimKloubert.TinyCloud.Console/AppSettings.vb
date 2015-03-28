﻿Imports System.Net
Imports MarcelJoachimKloubert.TinyCloud.SDK

''' <summary>
''' Handles global application settings.
''' </summary>
Module AppSettings

#Region "Properties (6)"

    ''' <summary>
    ''' Gets or sets the host.
    ''' </summary>
    Public Property Host As String = "localhost"

    ''' <summary>
    ''' Gets or sets if connection is secure or not.
    ''' </summary>
    Public Property IsSecure As Boolean = False

    ''' <summary>
    ''' Gets or sets the type of the mode.
    ''' </summary>
    Public Property Mode As Type = GetType(ConsoleMode)

    ''' <summary>
    ''' Gets or sets the password.
    ''' </summary>
    Public Property Password As String = "admin"

    ''' <summary>
    ''' Gets or sets the port.
    ''' </summary>
    Public Property Port As Integer = 5979

    ''' <summary>
    ''' Gets or sets the suffix for the service URL.
    ''' </summary>
    Public Property Suffix As String = Nothing

    ''' <summary>
    ''' Gets or sets the username.
    ''' </summary>
    Public Property Username As String = "admin"

#End Region

#Region "Methods (4)"

    ''' <summary>
    ''' Creates a new connection from the current data.
    ''' </summary>
    Public Function CreateConnection() As CloudConnection
        Return New CloudConnection(GetBaseUrl(), GetCredentials())
    End Function

    ''' <summary>
    ''' Creates a new <see cref="IMode" /> instance from the current data (if possible).
    ''' </summary>
    ''' <returns>The new instance.</returns>
    Public Function CreateMode() As IMode
        Dim result As IMode = Nothing

        Dim m As Type = Mode
        If m IsNot Nothing Then
            result = Activator.CreateInstance(m)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Returns the base URL based on the current data.
    ''' </summary>
    ''' <returns>The base URL.</returns>
    Public Function GetBaseUrl() As Uri
        Dim h As String = Host
        If String.IsNullOrWhiteSpace(h) Then
            h = "localhost"
        Else
            h = h.Trim()
        End If

        Dim scheme As String = "http"
        If IsSecure Then
            scheme = scheme & "s"
        End If

        Dim s As String = Suffix
        If String.IsNullOrWhiteSpace(s) Then
            s = String.Empty
        Else
            s = s.Trim()
        End If

        Return New Uri(String.Format("{0}://{1}:{2}/{3}", _
                                     scheme, h, Port, s))
    End Function

    ''' <summary>
    ''' Returns the credentials based on the current data.
    ''' </summary>
    ''' <returns>The credentials.</returns>
    Public Function GetCredentials() As NetworkCredential
        Return New NetworkCredential(Username, Password)
    End Function

#End Region

End Module