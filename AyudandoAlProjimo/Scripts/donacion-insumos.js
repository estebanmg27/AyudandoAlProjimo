function validardi(event) {
    event.preventDefault();
    var nombredi, cantidaddi;

    nombredi = document.getElementById("nombredi").value;
    cantidaddi = document.getElementById("cantidaddi").value;

    if (nombredi === "" || cantidaddi === "") {
        alert("Todos los campos son obligatorios");
        return false;
    }
    else if (isNaN(cantidaddi)) {
        alert("La cantidad ingresada no es un numero");
        return false;
    }
    else if (cantidaddi > 9999999999) {
        alert("La cantidad no puede ser tan grande");
        return false;
    }
    else if (cantidaddi < 0) {
        alert("La cantidad no puede ser menor a cero");
        return false;
    }

    event.target.submit();
}