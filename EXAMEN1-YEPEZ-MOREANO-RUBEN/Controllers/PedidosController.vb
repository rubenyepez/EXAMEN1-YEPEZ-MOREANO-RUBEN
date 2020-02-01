Public Class PedidosController
    Inherits System.Web.Mvc.Controller

    '
    ' GET: /Pedidos

    Function listaPedidoPendiente() As ActionResult
        Dim lista As New List(Of PedidosPendientes)
        Dim usrData As New UtilesPedidos
        Dim Anno As Integer = CStr(usrData.MaximoAnno)
        lista = usrData.ListaPedidos(Anno)
        ViewData("Encabezado") = "PEDIDOS PENDIENTES"
        ViewData("nombreUsuario") = "Ruben Yepez Moreano"
        ViewData("AnnoActual") = Anno
        Return View(lista)
    End Function


    Function listaCambiaAnnoPedido(ByVal Anno As Integer) As JsonResult
        Dim lista As New List(Of PedidosPendientes)
        Dim usrData As New UtilesPedidos
        lista = usrData.ListaPedidos(Anno)
      
        Return Json(lista, JsonRequestBehavior.AllowGet)
    End Function
    Function DetallePedido(ByVal Orden As String) As JsonResult
        Dim lista As New List(Of modDetallePedido)
        Dim usrData As New UtilesPedidos
        Dim total As Double = 0
        lista = usrData.DetallePedido(Orden, total)
        Dim strTotal As String = usrData.FormateaNumero(total, 12)
        ViewData("Total") = strTotal.Trim()
        lista(0).TotalGeneral = strTotal.Trim()
        Return Json(lista, JsonRequestBehavior.AllowGet)
    End Function

    Function AnnosConsulta() As JsonResult
        '    Valor As String
        'Public Property Anno As String
        Dim listaAnno = New List(Of modAnnoConsulta)
        Dim usrDocu As New UtilesPedidos
        listaAnno = usrDocu.AnnosEnConsulta

        Return Json(listaAnno, JsonRequestBehavior.AllowGet)
    End Function

    'var miUrl = '/Pedidos/grabaPedido/?Orden=' + ord+'&Fecha='+miFecha+'&Comentario='+miComentario+'&Graba='+graba;
    Function grabaPedido(ByVal Orden As String, ByVal Fecha As String, ByVal Comentario As String, ByVal Graba As String) As Integer
        '    Valor As String
        'Public Property Anno As String
        Dim i As Integer = 0

        Dim usrDocu As New UtilesPedidos
        i = usrDocu.grabaPedido(Orden, Fecha, Comentario, Graba)

        Return i
    End Function
End Class