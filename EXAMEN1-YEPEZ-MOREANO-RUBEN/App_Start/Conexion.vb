Imports System.Data.SqlClient
Public Class Conexion
    Public Function VerificaConexion(ByVal Servidor As String, ByVal User As String, ByVal Pass As String) As Integer
        Dim oConex As New SqlConnection
        Dim strConex As String
        Dim errorConexion As Integer = 0
        If (Servidor.Length() > 0) Then
            ' strConex = ConfigurationManager.ConnectionStrings("SLPConnectionString").ConnectionString
            strConex = "Data Source=" + Servidor + ";Initial Catalog=Northwind;User Id=" + User + ";Password=" + Pass + ";"

            Try
                oConex = New SqlConnection(strConex)
                oConex.Open()

                oConex.Close()




            Catch ex As SqlException


                errorConexion = 1
            End Try
        End If
        
        Return errorConexion
    End Function
    
    Public Function cargaConex(ByVal Servidor As String, ByVal User As String, ByVal Pass As String) As SqlConnection
        Dim oConex As New SqlConnection
        Dim strConex As String
        If Servidor.Length > 0 Then
            strConex = "Data Source=" + Servidor + ";Initial Catalog=Northwind;User Id=" + User + ";Password=" + Pass + ";"
        Else
            strConex = ConfigurationManager.ConnectionStrings("SLPConnectionString").ConnectionString
        End If


        oConex = New SqlConnection(strConex)
        oConex.Open()
        Return oConex
    End Function
End Class

