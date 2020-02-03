Public Class PedidosController
    Inherits System.Web.Mvc.Controller

    '
    ' GET: /Pedidos
    Function Login() As ActionResult
        ViewData("Sistema") = "PEDIDOS PENDIENTES"
        ViewData("Modulo") = "INGRESO SISTEMA"
        Return View()
    End Function
    Function VerificaConexion(ByVal Servidor As String, ByVal User As String, ByVal Pass As String) As Integer
        Dim resultado As Integer
        Dim miBas As Conexion = New Conexion
        resultado = miBas.VerificaConexion(Servidor, User, Pass)
        Session("Servidor") = Servidor
        Session("User") = User
        Session("Pass") = Pass
        Return resultado
    End Function
    Function listaPedidoPendiente() As ActionResult
        'Dim lista As New List(Of PedidosPendientes)
        Dim usrData As New UtilesPedidos
        Dim Anno As Integer = CStr(usrData.MaximoAnno(CStr(Session("Servidor")), CStr(Session("User")), CStr(Session("Pass"))))
        'lista = usrData.ListaPedidos(Anno)
        ViewData("Encabezado") = "PEDIDOS PENDIENTES"
        ViewData("nombreUsuario") = "Ruben Yepez Moreano"
        ViewData("AnnoActual") = Anno

        Return View()
    End Function


    Function listaCambiaAnnoPedido(ByVal Anno As Integer) As String
        Dim miStr As String
        Dim usrData As New UtilesPedidos
        miStr = usrData.ListaPedidosString(Anno, CStr(Session("Servidor")), CStr(Session("User")), CStr(Session("Pass")))
        Return miStr
    End Function
    Function DetallePedido(ByVal Orden As String) As String
        Dim miString As String
        Dim usrData As New UtilesPedidos
        miString = usrData.DetallePedido(Orden, CStr(Session("Servidor")), CStr(Session("User")), CStr(Session("Pass")))

        Return miString
    End Function

    Function AnnosConsulta() As String
        '    Valor As String
        'Public Property Anno As String
        Dim listaAnno = New List(Of modAnnoConsulta)
        Dim usrDocu As New UtilesPedidos
        Dim miString As String = usrDocu.AnnosEnConsulta(CStr(Session("Servidor")), CStr(Session("User")), CStr(Session("Pass")))

        Return (miString)
    End Function

    'var miUrl = '/Pedidos/grabaPedido/?Orden=' + ord+'&Fecha='+miFecha+'&Comentario='+miComentario+'&Graba='+graba;
    Function grabaPedido(ByVal Orden As String, ByVal Fecha As String, ByVal Comentario As String, ByVal Graba As String) As Integer
        '    Valor As String
        'Public Property Anno As String
        Dim i As Integer = 0

        Dim usrDocu As New UtilesPedidos
        i = usrDocu.grabaPedido(Orden, Fecha, Comentario, Graba, CStr(Session("Servidor")), CStr(Session("User")), CStr(Session("Pass")))

        Return i
    End Function
End Class