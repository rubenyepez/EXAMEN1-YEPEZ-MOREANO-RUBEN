<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Sistema")</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="~/css/estilo1.css" type="text/css" />
    <script type='text/javascript' src="~/js/jquery-3.3.1.min.js"></script>
    <script type='text/javascript' src="~/js/utiles.js"></script>
    <link rel="shortcut icon" href="~/imagenes/Logo.ico">
</head>
<body>
    <div class="contenedor">
        <div id="loginDiv">
            <div class="loginHead">
                @ViewData("Modulo")
                <input type="button" id="btnRojo" class="botonRojo">
                <hr />
            </div>
            <div id="loginBody">
                <table style="padding-top:7px; padding-left: 20px; table-layout: fixed; line-height: 2; ">
                    <tr>
                        <td class="loginLabel">Servidor</td>
                        <td class="loginData">
                            <input id="miServidor" required autofocus="autofocus" type="text" maxlength="50" size="15">
                        </td>
                    </tr>
                    <tr>
                        <td class="loginLabel">Usuario</td>
                        <td class="loginData">
                            <input id="miUsuario" required autofocus="autofocus" type="text" maxlength="30" size="15">
                        </td>
                    </tr>
                    <tr>
                        <td class="loginLabel">Contrase&ntilde;a </td>
                        <td class="loginData">
                            <input id="miPass" required type="password" maxlength="30" size="15">
                        </td>
                    </tr>
                </table>
                <br />
                <input type="button" class="botonVerde" id="btnIngresa" value="Ingresar">
            </div>
        </div>
    </div>

    <div id="tapaTodo" class="overlay"></div>
    <div id="muestraMensaje" class="fondoMensaje">
        <div class="mensajeHead">
            <label id="txtTitulo"> aaaaaaaa</label>
            <input type="button" id="btnCierraMensaje" class="botonRojo">
            <br /><br />
            <hr />
        </div>
        <br /><br /><br />
        <p>
            <label id="txtMensaje" class="labelMensaje"> xxxxxxxxxx </label>
        </p>
        <br /><br />
        <input type="button" class="botonVerde" id="btnConforme" value="Conforme">
    </div>
    <script>
        $(document).ready(function () {
            history.pushState(null, "", "$");
        });
        $("#miServidor").keypress(function (event) {
            if (event.which == 13) { if (validaServidor()) { $("#miUsuario").focus(); } }
            else
            {
                if (event.keyCode < 48 || event.keyCode > 57) { event.returnValue = false; }
            }
        });

        function validaServidor() {
            var miServidor = $("#miServidor").val();
            var bolOkey = true;
            miServidor = miServidor.trim();

            if (miServidor.length == 0) {
                mensaje('VERIFIQUE', "No Ingreso Servidor-Revise WEB.CONFIG");

            }
            return bolOkey;
        }

        $("#miUsuario").keypress(function (event) {
            if (event.which == 13) { if (validaUsuario()) { $("#miPass").focus(); } }
            else
            {
                if (event.keyCode < 48 || event.keyCode > 57) { event.returnValue = false; }
            }
        });

        function validaUsuario() {
            var miServidor = $("#miServidor").val();

            miServidor = miServidor.trim();
            var miUser = $("#miUsuario").val();
            var bolOkey = true;
            miUser = miUser.trim();

            if ((miUser.length == 0) && (miServidor.length > 0)) {
                mensaje('VERIFIQUE', "Usuario NO PUEDE estar en blanco.");
                $('#miUsuario').focus();
                bolOkey = false;
            }
            return bolOkey;
        }
        $("#miPass").keypress(function (event) {
            if (event.which == 13) {
                if (validaPass()) { $("#btnIngresa").focus(); }
            }
        });
        function validaPass() {
            var miPass = $("#miPass").val();
            var miServidor = $("#miServidor").val();

            miServidor = miServidor.trim();
            var bolOkey = true;
            miPass = miPass.trim();
            if ((miPass.length === 0) && (miServidor.length > 0)) {
                mensaje('VERIFIQUE', "Ingrese Passwoord.");
                $('#miPass').focus();
                bolOkey = false;
            }
            return bolOkey;
        }

        $('#btnRojo').click(function () {
            var miUrl = 'http://www.google.com';
            window.open(miUrl, '_top');

        });

        function resultadoLogeo(resultado) {           
            if (resultado == 1) { mensaje('VERIFIQUE', 'NO existe Servidor, Usuario o Password Incorrecto'); $('#miServidor').focus(); }
            else {
                  var miURL = '@Url.Action("listaPedidoPendiente", "Pedidos")'; window.open(miURL, '_top');
                }
        }


        function ingresoAlSistema() {
            var Servidor = $("#miServidor").val();
            var User = $("#miUsuario").val();
            var Pass = $("#miPass").val();

            var miURL = "/Pedidos/VerificaConexion/?Servidor=" + Servidor + '&User=' + User + '&Pass=' + Pass; 
            $.ajax({

                type: 'GET',
                cache: false,
                url: miURL,
                async: false,
                success: resultadoLogeo
            });
        }

        $('#btnIngresa').click(function () {
            if (validaServidor()) {

                if (validaUsuario()) {

                    if (validaPass()) {
                        ingresoAlSistema();

                    } else {
                        mensaje('VERIFIQUE', 'Password Incorrecto o en Blanco');
                    }
                } else {
                    mensaje('USUARIO', 'FALTA INGRESAR EL CODIGO DE USUARIO');
                }
            }
        });

        $('#btnConforme').click(function () {
            cierraMensaje();
        });

        $('#btnCierraMensaje').click(function () {
            cierraMensaje();
        });
    </script>
</body>
</html>
