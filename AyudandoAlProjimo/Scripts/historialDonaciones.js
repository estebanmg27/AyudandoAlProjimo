
var BuscarHistorial = function (idUsuario) {
        var cadena;
        var url = window.location.origin + '/api/ApiDonaciones/' + idUsuario;
        $.getJSON(url, function (data) {
            data.forEach(function (item) {
                var p = new Date(item.FechaDonacion);
                cadena += `<tr  class="text-center">`
                cadena += `<td>${p.getDate()}-${p.getMonth() + 1}-${p.getFullYear()}</td>`
                cadena += `<td>${item.Nombre}</td>`
                if (item.TipoDonacion == 1) {
                    cadena += `<td class="${item.TipoDonacion}">Monetaria</td>`
                }
                else if (item.TipoDonacion == 2) {
                    cadena += `<td class="${item.TipoDonacion}">Insumos</td>`
                }
                else if (item.TipoDonacion == 3) {
                    cadena += `<td class="${item.TipoDonacion}">Horas de Trabajo</td>`
                }
                if (item.Estado == 0) {
                    cadena += `<td>Abierto</td>`
                }
                if (item.Estado == 1) {
                    cadena += `<td>Cerrado</td>`
                }
                cadena += `<td>${item.TotalRecaudado}</td>`
                cadena += `<td>${item.MiDonacion}</td>`
                cadena += `<td> <a href="/Propuestas/DetallePropuesta/${item.IdPropuesta}">Ir a Propuesta</a> </td>`
                cadena += `</tr>`
            });
            $("#mostrarContenido").html(cadena);
        });
    }
