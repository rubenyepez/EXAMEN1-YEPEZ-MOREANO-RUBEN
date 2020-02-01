function mensaje(miTitulo, miMensaje)
{ 
 document.getElementById('tapaTodo').style.display='block';
 document.getElementById('muestraMensaje').style.display='block';
 $('#txtMensaje').text(miMensaje);
 $('#txtTitulo').html(miTitulo);
}   

function cierraMensaje()
{  
 document.getElementById('tapaTodo').style.display='none';
 document.getElementById('muestraMensaje').style.display='none';
}
 

function pantalla(miTitulo, miMensaje) {
    document.getElementById('tapaTodo').style.display = 'block';
    document.getElementById('muestraPantalla').style.display = 'block';
    $('#txtMensaje').text(miMensaje);    $('#txtTitulo').html(miTitulo);
}
 
function cierraPantalla() {
    document.getElementById('tapaTodo').style.display = 'none';
    document.getElementById('muestraPantalla').style.display = 'none';
}
 

function cargaAnnos(data) {
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            $('.cboAnno').append('<option value=' + data[i].Valor + ' selected>' + data[i].Anno + '</option>');
        };
    };
};
 
 