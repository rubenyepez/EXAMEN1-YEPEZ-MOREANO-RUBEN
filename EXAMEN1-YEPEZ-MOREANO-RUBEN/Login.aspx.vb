Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Response.RedirectToRoute(New With {Key .controller = "Pedidos", Key .action = "listaPedidoPendiente"})
        End If
    End Sub
End Class