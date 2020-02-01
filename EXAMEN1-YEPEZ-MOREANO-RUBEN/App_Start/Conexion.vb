Imports System.Data.SqlClient
Public Class Conexion
    Public Function cargaConex() As SqlConnection
        Dim oConex As New SqlConnection
        Dim strConex As String

        strConex = ConfigurationManager.ConnectionStrings("SLPConnectionString").ConnectionString

        oConex = New SqlConnection(strConex)
        oConex.Open()
        Return oConex
    End Function
End Class

