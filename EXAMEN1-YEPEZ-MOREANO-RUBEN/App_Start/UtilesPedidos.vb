Imports System.Data.SqlClient
Imports System.Web.Mvc
Imports System.Web.Helpers
Public Class UtilesPedidos
    Public Function ListaPedidos(ByVal Anno As Integer)
        Dim miBas As Conexion = New Conexion
        Dim miCon As SqlConnection = miBas.cargaConex()

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


        Dim lista As New List(Of PedidosPendientes)
        Dim rsReader As SqlDataReader = command.ExecuteReader

        While rsReader.Read
            Dim item As New PedidosPendientes

            item.Orden = CStr(rsReader("Orden"))

            item.Fecha = CStr(rsReader("Fecha"))
            item.RazonSocial = CStr(rsReader("RazonSocial"))
            item.Telefono = CStr(rsReader("Telefono"))

            lista.Add(item)
        End While

        rsReader.Close()
        transaction.Commit()
        command.Dispose()
        miCon.Close()
        Return (lista)

    End Function

    Public Function AnnosEnConsulta()
        Dim miBas As Conexion = New Conexion
        Dim miCon As SqlConnection = miBas.cargaConex()

        Dim commandText As String = "select year(orderdate) Valor, year(orderdate) Anno "
        commandText = commandText + "from orders group by year(orderdate) order by year(OrderDate) desc"
        Dim command As New SqlCommand(commandText, miCon)
        Dim i As Integer = 0
        command.CommandType = CommandType.Text
        Dim transaction As SqlTransaction

        transaction = miCon.BeginTransaction("AnnosEnConsulta")
        command.Transaction = transaction
        Dim lista As New List(Of modAnnoConsulta)
        Dim rsReader As SqlDataReader = command.ExecuteReader
        While rsReader.Read
            Dim item As New modAnnoConsulta
            item.Valor = CStr(rsReader("Valor"))
            item.Anno = CStr(rsReader("Anno"))
            lista.Add(item)
        End While
        rsReader.Close()
        transaction.Commit()
        command.Dispose()
        miCon.Close()
        Return (lista)
    End Function
    Public Function MaximoAnno() As Integer
        Dim miBas As Conexion = New Conexion
        Dim miCon As SqlConnection = miBas.cargaConex()

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
    Public Function DetallePedido(ByVal Orden As String, ByRef total As Double)
        Dim miBas As Conexion = New Conexion
        Dim miCon As SqlConnection = miBas.cargaConex()

        Dim commandText As String = "SELECT a.ProductID Codigo,upper( b.ProductName) Descripcion, a.Quantity Cantidad, a.UnitPrice,  a.Quantity*a.UnitPrice Total "
        commandText = commandText + "  FROM  [Order Details] a "
        commandText = commandText + " left join Products b on a.ProductID=b.ProductID "
        commandText = commandText + " where a.OrderID='" + Orden + "' "
        Dim command As New SqlCommand(commandText, miCon)
        Dim transaction As SqlTransaction
        transaction = miCon.BeginTransaction("detallePedidos")
        command.Transaction = transaction

        Dim miNumero As Double = 0
        total = 0
        Dim lista As New List(Of modDetallePedido)
        Dim rsReader As SqlDataReader = command.ExecuteReader

        While rsReader.Read
            Dim item As New modDetallePedido

            item.Codigo = CStr(rsReader("Codigo"))
            item.Descripcion = CStr(rsReader("Descripcion"))
            miNumero = CDbl(rsReader("UnitPrice"))
            If miNumero = 0 Then
                item.UnitPrice = ""
            Else
                item.UnitPrice = FormateaNumero(miNumero, 12)
            End If

            miNumero = CDbl(rsReader("Cantidad"))
            If miNumero = 0 Then
                item.Cantidad = ""
            Else
                item.Cantidad = FormateaNumero(miNumero, 12)
            End If

            miNumero = CDbl(rsReader("Total"))
            total = total + miNumero
            If miNumero = 0 Then
                item.Total = ""
            Else
                item.Total = FormateaNumero(miNumero, 12)
            End If

            lista.Add(item)
        End While

        rsReader.Close()
        transaction.Commit()
        command.Dispose()
        miCon.Close()
        Return (lista)

    End Function

    Public Function grabaPedido(ByVal Orden As String, ByVal Fecha As String, ByVal Comentario As String, ByVal Graba As String) As Integer
        Dim i As Integer = 0
        If (Graba = "S") Then
            i = 1
            Dim miBas As Conexion = New Conexion
            Dim miCon As SqlConnection = miBas.cargaConex()

            Dim commandText As String = "Update orders "
            commandText = commandText + "  set ShipVia=2 "
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
