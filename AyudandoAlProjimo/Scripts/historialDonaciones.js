
var BuscarHistorial = function (idUsuario) {
        var cadena;
        var url = window.location.origin + '/api/ApiDonaciones/' + idUsuario;
        $.getJSON(url, function (data) {
            data.forEach(function (item) {
                var p = new Date(item.FechaDonacion);
                cadena += `<tr  class="text-center">`
                cadena += `<td>${p.getDate()}-${p.getMonth() + 1}-${p.getFullYear()}</td>`
                cadena += `<td>${item.Nombre}</td>`

                if (item.Tipo == 1) {
                    cadena += '<td>Monetaria</td>'
                }
                if (item.Tipo == 2) {
                    cadena += '<td>Insumos</td>'
                }
                if (item.Tipo == 3) {
                    cadena += '<td>Horas de trabajo</td>'
                }

                if (item.Estado == 0) {
                    cadena += `<td>Abierta</td>`
                }
                if (item.Estado == 1) {
                    cadena += `<td>Cerrada</td>`
                }


                cadena += `<td>${item.TotalRecaudado}</td>`
                cadena += `<td>${item.MiDonacion}</td>`
                cadena += `<td> <a href="/Propuestas/VerDetallePropuesta/${item.IdPropuesta}">Detalle</a> </td>`
                cadena += `</tr>`
       
            });
            $("#mostrarContenido").html(cadena);
        });
    }
