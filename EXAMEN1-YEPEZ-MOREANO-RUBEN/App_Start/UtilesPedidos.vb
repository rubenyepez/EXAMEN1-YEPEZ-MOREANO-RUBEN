Imports System.Data.SqlClient
Imports System.Web.Mvc
Imports System.Web.Helpers
Public Class UtilesPedidos
    Public Function ListaPedidosString(ByVal Anno As Integer, ByVal Servidor As String, ByVal User As String, ByVal Pass As String) As String
        Dim miBas As Conexion = New Conexion


        Dim miCon As SqlConnection = miBas.cargaConex(Servidor, User, Pass)

        Dim commandText As String = "select a.OrderId Orden, convert(varchar(10), a.orderdate,102) Fecha,"
        commandText = commandText + "  upper(b.CompanyName) RazonSocial, "
        commandText = commandText + " b.Phone Telefono from orders a "
        commandText = commandText + " LEFT JOIN Customers B ON A.CustomerID=b.CustomerID "
        commandText = commandText + "  where a.ShipVia = 1 and year(a.orderdate)=" + CStr(Anno) + " "
        commandText = commandText + " order by a.orderdate desc  "
        Dim command As New SqlCommand(commandText, miCon)
        Dim transaction As SqlTransaction

        transaction = miCon.BeginTransaction("listaPedidos")
        command.Transaction = transaction

        Dim nuevaFila As String = "<thead><th class='celdaTitulo' style='width:18px'>ORDEN</th>"
        nuevaFila = nuevaFila + "<th class='celdaTitulo' style='width: 32px'>FECHA</th>"
        nuevaFila = nuevaFila + "<th class='celdaTitulo' style='width: 32px'>RAZON SOCIAL</th>"
        nuevaFila = nuevaFila + "<th class='celdaTitulo' style='width: 32px'>TELEFONO</th>"
        nuevaFila = nuevaFila + "</tr></thead>"
        Dim rsReader As SqlDataReader = command.ExecuteReader
        Dim i As Integer = 0
        While rsReader.Read

            If (i = 0) Then
                nuevaFila = nuevaFila + "<tbody><tr><td id='idOrden' class='celdaTexto'>" + CStr(rsReader("Orden")) + "</td>"
            Else
                nuevaFila = nuevaFila + "<tr><td id='idOrden' class='celdaTexto'>" + CStr(rsReader("Orden")) + "</td>"
            End If
            nuevaFila = nuevaFila + "<td id='idFecha' class='celdaTexto'>" + CStr(rsReader("Fecha")) + "</td>"
            nuevaFila = nuevaFila + "<td id='idRazon' class='celdaTexto'>" + CStr(rsReader("RazonSocial")) + "</td>"
            nuevaFila = nuevaFila + "<td id='idTel' class='celdaTexto'>" + CStr(rsReader("Telefono")) + "</td>"
            i = i + 1
        End While
        nuevaFila = nuevaFila + " </tbody>"
        rsReader.Close()
        transaction.Commit()
        command.Dispose()
        miCon.Close()
        Return (nuevaFila)
    End Function
    Public Function AnnosEnConsulta(ByVal Servidor As String, ByVal User As String, ByVal Pass As String) As String
        Dim miBas As Conexion = New Conexion
        Dim miCon As SqlConnection = miBas.cargaConex(Servidor, User, Pass)

        Dim commandText As String = "select year(orderdate) Valor, year(orderdate) Anno "
        commandText = commandText + "from orders group by year(orderdate) order by year(OrderDate) desc"
        Dim command As New SqlCommand(commandText, miCon)
        Dim i As Integer = 0
        command.CommandType = CommandType.Text
        Dim transaction As SqlTransaction

        transaction = miCon.BeginTransaction("AnnosEnConsulta")
        command.Transaction = transaction
        Dim miFila As String = ""
        Dim rsReader As SqlDataReader = command.ExecuteReader
        While rsReader.Read
            If (i = 0) Then
                miFila = "<option value=" + CStr(rsReader("Valor")) + " selected>" + CStr(rsReader("Anno")) + "</option>"
            Else
                miFila = miFila + "<option value=" + CStr(rsReader("Valor")) + ">" + CStr(rsReader("Anno")) + "</option>"
            End If
            i = i + 1
        End While
        rsReader.Close()
        transaction.Commit()
        command.Dispose()
        miCon.Close()
        Return (miFila)
    End Function
    Public Function MaximoAnno(ByVal Servidor As String, ByVal User As String, ByVal Pass As String) As Integer
        Dim miBas As Conexion = New Conexion
        Dim miCon As SqlConnection = miBas.cargaConex(Servidor, User, Pass)

        Dim commandText As String = "select max(year(orderdate))  Anno "
        commandText = commandText + "from orders "
        Dim command As New SqlCommand(commandText, miCon)
        Dim i As Integer = 0
        command.CommandType = CommandType.Text
        Dim transaction As SqlTransaction

        transaction = miCon.BeginTransaction("MaxAnno")
        command.Transaction = transaction

        Dim rsReader As SqlDataReader = command.ExecuteReader
        While rsReader.Read

            i = CInt(rsReader("Anno"))
        End While
        rsReader.Close()
        transaction.Commit()
        command.Dispose()
        miCon.Close()
        Return (i)
    End Function
    Public Function DetallePedido(ByVal Orden As String, ByVal Servidor As String, ByVal User As String, ByVal Pass As String) As String
        Dim miBas As Conexion = New Conexion
        Dim miCon As SqlConnection = miBas.cargaConex(Servidor, User, Pass)

        Dim commandText As String = "SELECT a.ProductID Codigo,upper( b.ProductName) Descripcion, a.Quantity Cantidad, a.UnitPrice,  a.Quantity*a.UnitPrice Total "
        commandText = commandText + "  FROM  [Order Details] a "
        commandText = commandText + " left join Products b on a.ProductID=b.ProductID "
        commandText = commandText + " where a.OrderID='" + Orden + "' "
        Dim command As New SqlCommand(commandText, miCon)
        Dim transaction As SqlTransaction
        transaction = miCon.BeginTransaction("detallePedidos")
        command.Transaction = transaction

        Dim miNumero As Double = 0

        Dim nuevaFila As String = "<thead><tr><th class='celdaTitulo' style='width:18px;'>CODIGO</th>"

        nuevaFila = nuevaFila + "<th class='celdaTitulo' style='width:18px;'>DESCRIPCION</th>"
        nuevaFila = nuevaFila + "<th class='celdaTitulo' style='width: 32px;'>CANTIDAD</th>"
        nuevaFila = nuevaFila + "<th class='celdaTitulo' style='width: 32px;'>PRECIO</th>"
        nuevaFila = nuevaFila + "<th class='celdaTitulo' style='width: 32px;'>TOTAL</th>"
        Dim rsReader As SqlDataReader = command.ExecuteReader
        Dim i As Integer = 0
        Dim Total As Double = 0
        While rsReader.Read
            If (i = 0) Then
                nuevaFila = nuevaFila + "<tbody><tr><td id='miItem' class='celdaTextoBlanco'>" + CStr(rsReader("Codigo")) + "</td>"
            Else
                nuevaFila = nuevaFila + "<tr><td id='miItem' class='celdaTextoBlanco'>" + CStr(rsReader("Codigo")) + "</td>"
            End If
            nuevaFila = nuevaFila + "<td id='miItem' class='celdaTextoBlanco'>" + CStr(rsReader("Descripcion")) + "</td>"
            miNumero = CDbl(rsReader("Cantidad"))
            If miNumero = 0 Then
                nuevaFila = nuevaFila + "<td id='miItem' class='celdaTextoBlanco'> </td>"
            Else
                nuevaFila = nuevaFila + "<td id='miItem' class='celdaTextoBlanco'>" + FormateaNumero(miNumero, 12) + "</td>"
            End If

            miNumero = CDbl(rsReader("UnitPrice"))
            If miNumero = 0 Then
                nuevaFila = nuevaFila + "<td id='miItem' class='celdaTextoBlanco'> </td>"
            Else
                nuevaFila = nuevaFila + "<td id='miItem' class='celdaTextoBlanco'>" + FormateaNumero(miNumero, 12) + "</td>"
            End If

            miNumero = CDbl(rsReader("Total"))
            If miNumero = 0 Then
                nuevaFila = nuevaFila + "<td id='miItem' class='celdaTextoBlanco'> </td>"
            Else
                nuevaFila = nuevaFila + "<td id='miItem' class='celdaNumeroBlanco'>" + FormateaNumero(miNumero, 12) + "</td>"
            End If

            Total = Total + miNumero

            i = i + 1
        End While
        nuevaFila = nuevaFila + " </tbody>"
        nuevaFila = nuevaFila + "<th class='celdaTextoBlanco'></th>"
        nuevaFila = nuevaFila + "<th class='celdaTextoBlanco'></th>"
        nuevaFila = nuevaFila + "<th class='celdaTextoBlanco'></th>"
        nuevaFila = nuevaFila + "<th class='celdaNumeroBlanco'>TOTAL--</th>"
        nuevaFila = nuevaFila + "<th class='celdaNumeroBlanco'>" + FormateaNumero(Total, 12) + "</th>"
        nuevaFila = nuevaFila + "</tr></tfoot>"
        rsReader.Close()
        transaction.Commit()
        command.Dispose()
        miCon.Close()
        Return (nuevaFila)

    End Function

    Public Function grabaPedido(ByVal Orden As String, ByVal Fecha As String, ByVal Comentario As String, ByVal Graba As String, ByVal Servidor As String, ByVal User As String, ByVal Pass As String) As Integer
        Dim i As Integer = 0
        If (Graba = "S") Then
            i = 1
            Dim miBas As Conexion = New Conexion
            Dim miCon As SqlConnection = miBas.cargaConex(Servidor, User, Pass)
            Dim ComentarioGrabar As String = Left(Comentario, 60)
            ComentarioGrabar = ComentarioGrabar.ToUpper
            Dim commandText As String = "Update orders "
            commandText = commandText + "set ShipVia=2 , "
            commandText = commandText + "ShipAddress='" + ComentarioGrabar + "', "
            commandText = commandText + "ShippedDate= convert(datetime, '" + Fecha + "',102)"
            commandText = commandText + "  where  OrderID = '" + Orden + "'"

            Dim command As New SqlCommand(commandText, miCon)
            Dim transaction As SqlTransaction

            transaction = miCon.BeginTransaction("grabaPedido")
            command.Transaction = transaction

            command.ExecuteNonQuery()

            transaction.Commit()
            command.Dispose()
            miCon.Close()
        End If


        Return i
    End Function
    Public Function FormateaNumero(ByVal numero As Double, ByVal tamano As Integer) As String
        Dim strNumero As String = FormatNumber(numero, 2)
        Dim actualSize As Integer = Len(strNumero)
        tamano = 16
        If (tamano > actualSize) Then
            strNumero = Space(16) + strNumero
            strNumero = Right(strNumero, tamano)
        End If
        Return strNumero
    End Function
End Class
