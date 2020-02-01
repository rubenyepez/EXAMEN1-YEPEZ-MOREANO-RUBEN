@ModelType IEnumerable(Of EXAMEN1_YEPEZ_MOREANO_RUBEN.PedidosPendientes)

<html>
<head>
    <title>Consulta Ordenes Pendientes</title>
    <meta charset="UTF-8">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="~/css/contenido.css" type="text/css" />
    <link rel="stylesheet" href="~/css/bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="~/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="~/css/buttons.dataTables.min.css" />
    <link rel="shortcut icon" href="~/imagenes/Logo.ico">

    @*<link rel="stylesheet" href="~/css/PrintArea.css" type="text/css" />*@


    <script type='text/javascript' src="~/js/jquery-3.3.1.min.js"></script>

    <script type='text/javascript' src="~/js/jquery.dataTables.min.js"></script>

    <script type='text/javascript' src="~/js/bootstrap.js"></script>

    <script type='text/javascript' src="~/js/jszip.min.js"></script>
    <script type='text/javascript' src="~/js/dataTables.buttons.min.js"></script>


    <script type='text/javascript' src="~/js/buttons.html5.min.js"></script>
    <script type='text/javascript' src="~/js/buttons.print.min.js"></script>
    <script type='text/javascript' src="~/js/utiles.js"></script>

</head>

<body>
    <div class="contenedor">
        <div class="cabeceraConsulta" style="width:100%;">
            <div class="panelLogo"></div>
            <div class="cabeceraTitulo">
                <div style="float:right; width:100%;">
                    <label class="titH24">@ViewData("Encabezado")</label>
                </div>
                <div style="float:right; width:100%;">
                    <label class="lblLabel">USUARIO:&nbsp;</label>
                    <label id="Usuario" class="lblContenido">@ViewData("nombreUsuario")</label>
                </div>
            </div>
        </div>

      
        <div id="listaOrdenes" class="PanelImpresion" style="height:80%; ">
            <div style="margin-top: 0px; margin-left: 1px; margin-right:2px;">
                <hr />
                <label class="lblLabel" style="margin-left:8px;">PERIODO</label>
                <select id="cbAnnoOrden" class="cboAnno"> </select>
            </div>
            <div style="overflow: auto; width: 100%; height: 99%; ">
                <table id="tablaOrdenada" class="table-hover" style="width: 98%; height:80%;">
                    <thead>
                        <tr>
                            <th class="celdaTitulo" style="width:18px;">ORDEN</th>
                            <th class="celdaTitulo" style="width: 32px;">FECHA</th>
                            <th class="celdaTitulo" style="width: 32px;">RAZON SOCIAL</th>
                            <th class="celdaTitulo" style="width: 32px;">TELEFONO</th>
                        </tr>
                    </thead>
                    <tbody>
                        @For Each item In Model
                            @<tr>
                                <td id="idOrden" class="celdaTexto">@item.Orden</td>
                                <td id="idFecha" class="celdaTexto">@item.Fecha</td>
                                <td id="idRazon" class="celdaTexto">@item.RazonSocial</td>
                                <td id="idTel" class="celdaTexto">@item.Telefono</td>
                            </tr>
                        Next
                    </tbody>
                 
                </table>
            </div>
        </div>
        <div id="DatosDetallePedido" class="fondoMensaje2">
            <div id="cabMove"> </div>
            <div>
                <label id="numeroPedido" class="lblLabelMenu"></label>
                <label id="pedidoRazonSocial" class="lblLabelMenu" style="margin-left:6px;"></label>
        
            </div><br />
            <div style="overflow: auto; width:100%; height:50%">
                <table id="miTablaDetalle" class="table-hover" style="width: 100%;"></table>
            </div>
            <div style="width:100%; height:20%">
              
                <input id="miFecha" type="date" style="width: 140px; font-size: 10px; height:22px;" class="Ingreso" />
                <input type="checkbox" id="radioConforme"  value="Confir" style="padding-left:10px;"> Confirmado<br>
                <label  id="miComentario" class="lblLabelMenu">COMENTARIO </label>
                <textarea rows="4" cols="80">
                     
                </textarea>
            </div>
           
            <div style="width: 100%; margin-top: 6px; margin-left: 15px;">
                <div style="width: 35%; margin-top: 6px; margin-left: 15px; float:left">
                    <input type="button" class="botonRojoSale" id="btnCierra" value="Cancela" style="margin-left:10px;">
                </div>
                <div style="width: 35%; margin-top: 6px; margin-left: 15px; float:right">
                    <input type="button" class="botonVerde" id="btnGraba" value="Graba" style="margin-left:10px;">
                </div>
            </div>
        </div>
    </div>
    
    <div id="tapaTodo" class="overlay"></div>
    <div id="muestraMensaje" class="fondoMensaje">
        <div class="mensajeHead">
            <label id="txtTitulo"> aaaaaaaa</label>
            @*<input type="button" id="btnCierraMensaje" class="botonRojo">*@
            <hr />
        </div>
        <br /><br /><br /><br />
        <p>
            <label id="txtMensaje" class="labelMensaje"> xxxxxxxxxx </label>
        </p>
        <br /><br />
        <input type="button" class="botonVerde" id="btnConformeMensaje" value="Conforme">
    </div>
    <script>
    var currentRow = '';
    var miTotal = '';
    var AnnoCargado = 0;
    var miRazonSocial = '';

    $(document).ready(function () {
        history.pushState(null, "", "$");
        
        $('#DatosDetallePedido').hide();
        cargaTablaOrdenada();
        $.ajax({
            type: 'GET',
            cache: false,
            url: '/Pedidos/AnnosConsulta',
            dataType: 'json',
            async: false,
            success: cargaAnnos
        });
        var yyyy = '@ViewData("AnnoActual")';
        $("#cbAnnoOrden").val(yyyy);
        AnnoCargado = 1;
        
        var today = new Date(); 
        var dd = today.getDate();
        var mm = today.getMonth() + 1;         
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        $('#miFecha').val(yyyy + '-' + mm + '-' + dd);
      
    });


      function  procesaPedidoxAno(){

        var miUrl = '/Pedidos/listaCambiaAnnoPedido/?Anno=' + $('#cbAnnoOrden option:selected').text();
        $.ajax({
            type: 'GET',
            cache: false,
            url: miUrl,
            dataType: 'json',
            async: false,
            success: cambiaPeriodoPedido
        });
      }

    $('#cbAnnoOrden').change(function () {
        if (AnnoCargado == 1) {
            procesaPedidoxAno();
           
        }
    });
    function cambiaPeriodoPedido(data) {
        if ($.fn.DataTable.isDataTable("#tablaOrdenada")) {
            $('#tablaOrdenada').DataTable().clear().destroy();
        }
        $("#tablaOrdenada thead").remove(); $("#tablaOrdenada tbody").remove(); $("#tablaOrdenada tr").remove();
        var nuevaFila = '<thead><th class="celdaTitulo" style="width:18px;">ORDEN</th>';
        nuevaFila += '<th class="celdaTitulo" style="width: 32px;">FECHA</th>';
        nuevaFila += '<th class="celdaTitulo" style="width: 32px;">RAZON SOCIAL</th>';
        nuevaFila += '<th class="celdaTitulo" style="width: 32px;">TELEFONO</th>';
        nuevaFila += '</tr></thead>';
        $("#tablaOrdenada").append(nuevaFila);
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                if (i == 0) { nuevaFila = '<tbody><tr><td id="idOrden" class="celdaTexto">' + data[i].Orden + '</td>'; }
                else { nuevaFila = '<tr><td id="idOrden" class="celdaTexto">' + data[i].Orden + '</td>'; }
                nuevaFila += '<td id="idFecha" class="celdaTexto">' + data[i].Fecha + '</td>';
                nuevaFila += '<td id="idRazon" class="celdaTexto">' + data[i].RazonSocial + '</td>';
                nuevaFila += '<td id="idTel" class="celdaTexto">' + data[i].Telefono + '</td>';
                if (i == (data.length - 1)) { nuevaFila += '</tr></tbody>'; }
                else { nuevaFila += '</tr>'; }
                $("#tablaOrdenada").append(nuevaFila);
            }
            $(".celdaTexto").bind("click", function (event) {
                currentRow = $(this).closest("tr");
                procesaDetallePedido();
            });
        }; cargaTablaOrdenada();
    }

    function cargaTablaOrdenada() {
        $('#tablaOrdenada').DataTable({
            pageLength: 10,
            language: {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                },
                "lengthMenu": [15, 20, 25, 50, 75, 100]
            },
            bFilter: true,
            ordering: true,
            exportOptions: {
                columns: ':visible',
                search: 'applied',
                order: 'applied'
            },
            scrollCollapse: true,
            paging: true
        });
    }
    function datosDetallePedido(data) {
        $("#miTablaDetalle thead").remove(); $("#miTablaDetalle tbody").remove(); $("#miTablaDetalle tr").remove();
        var nuevaFila = '<thead><tr><th class="celdaTitulo" style="width:18px;">CODIGO</th>';

        nuevaFila += '<th class="celdaTitulo" style="width:18px;">DESCRIPCION</th>';
        nuevaFila += '<th class="celdaTitulo" style="width: 32px;">CANTIDAD</th>';
        nuevaFila += '<th class="celdaTitulo" style="width: 32px;">PRECIO</th>';
        nuevaFila += '<th class="celdaTitulo" style="width: 32px;">TOTAL..</th>';
        $("#miTablaDetalle").append(nuevaFila);
        miTotal = ''; 
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                if (i == 0)
                { nuevaFila = '<tbody><tr><td id="miItem" class="celdaTextoBlanco">' + data[i].Codigo + '</td>'; miTotal =data[i].TotalGeneral; }
                else { nuevaFila = '<tr><td id="miItem" class="celdaTextoBlanco">' + data[i].Codigo + '</td>'; }

                nuevaFila += '<td id="miDoc" class="celdaTextoBlanco">' + data[i].Descripcion + '</td>';
                nuevaFila += '<td id="miSaldo" class="celdaNumeroBlanco">' + data[i].Cantidad + '</td>';
                nuevaFila += '<td id="miSaldo" class="celdaNumeroBlanco">' + data[i].UnitPrice + '</td>';
                nuevaFila += '<td id="miSaldo" class="celdaNumeroBlanco">' + data[i].Total + '</td>';
                if (i == (data.length - 1)) { nuevaFila += '</tr></tbody>'; footerTablaDetallePedido(); }
                else { nuevaFila += '</tr>'; }
                $("#miTablaDetalle").append(nuevaFila);
            }
        };
        $('#DatosDetallePedido').show();
        $('#listaOrdenes').hide();
    }

    function footerTablaDetallePedido() {         
        var nuevaFila = '<tfoot><tr>';
        nuevaFila += '<th class="celdaTextoBlanco"></th>';
        nuevaFila += '<th class="celdaTextoBlanco"></th>';
        nuevaFila += '<th class="celdaTextoBlanco"></th>';
        nuevaFila += '<th class="celdaNumeroBlanco">TOTAL</th>';
        nuevaFila += '<th class="celdaNumeroBlanco">'+miTotal+'</th>';
        nuevaFila += '</tr></tfoot>';
        $("#miTablaDetalle").append(nuevaFila);
    }
    function procesaDetallePedido() {
        var col1 = currentRow.find('#idOrden');
        var ord = col1.text();

        col1 = currentRow.find('#idRazon');
        miRazonSocial = col1.text();
        $('#numeroPedido').text('CONFIRMA PEDIDO '+ord);
        $('#pedidoRazonSocial').text(' - '+miRazonSocial);
        $("#radioConforme").prop("checked", false);
        $("#miComentario").val(''); 
        var miUrl = '/Pedidos/DetallePedido/?Orden=' + ord;

        $.ajax({
            type: 'GET',
            dataType: 'json',
            cache: false,
            url: miUrl,
            async: false,
            success: datosDetallePedido
        });
    };


    $('.celdaTexto').click(function () {

        currentRow = $(this).closest("tr");
        procesaDetallePedido();
    });
    $('#btnCierra').click(function() {
        $('#DatosDetallePedido').hide();
        $('#listaOrdenes').show();
    });

    function despuesDeGrabar(data) {
        if (data == 0) {  mensaje('DATOS NO GRABADOS','Falta Confirmar'); }

       
        else {procesaPedidoxAno();}
        $('#DatosDetallePedido').hide();
        $('#listaOrdenes').show();

    }
     $('#btnGraba').click(function() {
        $('#DatosDetallePedido').hide();
        $('#listaOrdenes').show();
        var graba = 'N';
        var miFecha=$('#miFecha').val();
        var miComentario=$('#miComentario').val();
    

        if ($("#radioConforme").is(':checked')) { graba = 'S'; }
        var col1 = currentRow.find('#idOrden');
        var ord = col1.text();
        var miUrl = '/Pedidos/grabaPedido/?Orden=' + ord+'&Fecha='+miFecha+'&Comentario='+miComentario+'&Graba='+graba;

        $.ajax({
            type: 'GET',
            cache: false,
            url: miUrl,
            async: false,
            success: despuesDeGrabar
        });
     });
     $('#btnConformeMensaje').click(function () {
         cierraMensaje();
     });
    </script>
</body>
</html>
