
    $("#datepicker").datepicker({
        dateFormat: "dd-mm-yy",
    showAnim: "clip"
    });
    

    function MunicipioGetByIdEstado(){
        let ddl = $('#ddlEstado').val();
        let url = window.location.origin;
        let newUrl = url + "/Usuario/GetByIdEstado?IdEstado=" + ddl;
        console.log(url);
        console.log(newUrl);
        $.ajax({
        url: newUrl,
        type: "GET",
        dataType: "JSON",
        //data -> SOLO PARA MODELOS
    success: function (result)
    {
             if (result.Correct)
    {
        //buscar donde pintare los valores
        let ddlMunicipio = $('#ddlMunicipio');
    let ddlColonia = $('#ddlColonia');
    ddlMunicipio.empty();
    ddlColonia.empty();
    let municipioDefault = "<option value=0>Seleccione el municipio</option>";
let coloniaDefault = "<option value=0>Seleccione su colonia</option>";
ddlMunicipio.append(municipioDefault);
ddlColonia.append(coloniaDefault);
$.each(result.Objects, function (i, valor) {
    let option = "<option value=" + valor.IdMunicipio + ">" + valor.Nombre + "</option>";
    ddlMunicipio.append(option);
})
             }
ColoniaGetByIdMunicipio()
         },
error: function (xhr) {
    console.log(xhr)
}
     })
    }

function ColoniaGetByIdMunicipio() {
    let ddl = $('#ddlMunicipio').val();
    let url = window.location.origin;
    let newUrl = url + "/Usuario/GetByIdMunicipio?IdMunicipio=" + ddl;
    $.ajax({
        url: newUrl,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                let ddlColonia = $('#ddlColonia');
                //ddlColonia.html("");
                ddlColonia.empty();
                let optionDefault = "<option value=0>Seleccione la colonia</option>";
                ddlColonia.append(optionDefault);
                $.each(result.Objects, function (i, valor) {
                    let option = "<option value=" + valor.IdColonia + ">" + valor.Nombre + "</option>";
                    ddlColonia.append(option);
                })
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function ValidarImagen() {
    //necesito saber la extencion de mi archivo
    //obtener la extension
    //dividir el Nombre en 2

    //variqable---id de mi input---indice0---propiedad files indice 0---dividir cuando encuentra un punto---lo convierte a minuscula
    var input = $('#inptFileImagen')[0].files[0].name.split('.').pop().toLowerCase()
    //console.log(input)
    var extensionesValidas = ['png', 'jpg', 'jpeg', 'webp']
    var banderaImg = false

    for (var i = 0; i <= extensionesValidas.length; i++) {
        if (input == extensionesValidas[i]) {
            banderaImg = true
        }
    }

    if (!banderaImg) {
        alert(`Los archivos permitidos deben ser ${extensionesValidas}`)
        //LIMPIAR EL INPUT
        $('#inptFileImagen').val("")
    }
}

function VisualizarImagen(input) {
    if (input.files) {
        var reader = new FileReader();
        reader.onload = function (elemento) {
            $('#img').attr('src', elemento.target.result)
        }
        reader.readAsDataURL(input.files[0])
    }
}

function ValidarTamanio(input) {
    const fileSize = input.files[0].size / 1024 / 1024;
    //console.log(input)
    if (fileSize > 2) {
        alert("La Imagen no puede superar los 2MB");
        $('#inptFileImagen').val("")
    }
}

function SoloLetras(evt) {
    var entrada = String.fromCharCode(evt.which)
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error')
    ErrorMessage.textContent = ' ';
    if (!(/[a-z A-Z]/.test(entrada))) {
        //console.log("no es letra")
        evt.preventDefault()
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Solo se aceptan letras';
    }
    else {
        //console.log("es letra")
        inputField.style.borderColor = 'green';
    }
}

function SoloNumeros(evt) {
    var entrada = String.fromCharCode(evt.which);
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error')
    ErrorMessage.textContent = ' ';
    if (!(/^[0-9]{1,10}$/g.test(entrada))) {
        //console.log("no es letra")
        evt.preventDefault()
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'Solo se aceptan numeros';
    }
    else {
        //console.log("es letra")
        inputField.style.borderColor = 'green';
    }
}

//Función para validar una CURP
function ValidarCurp(CURP) {
    var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
        validado = CURP.match(re);

    if (!validado)  //Coincide con el formato general?
        return false;

    //Validar que coincida el dígito verificador
    function digitoVerificador(CURP17) {
        //Fuente https://consultas.CURP.gob.mx/CurpSP/
        var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
            lngSuma = 0.0,
            lngDigito = 0.0;
        for (var i = 0; i < 17; i++)
            lngSuma = lngSuma + diccionario.indexOf(CURP17.charAt(i)) * (18 - i);
        lngDigito = 10 - lngSuma % 10;
        if (lngDigito == 10) return 0;
        return lngDigito;
    }

    if (validado[2] != digitoVerificador(validado[1]))
        return false;

    return true; //Validado
}

function validarInput(input) {
    var CURP = input.value.toUpperCase(),
        resultado = document.getElementById("resultado"),
        valido = "No válido";

    if (ValidarCurp(CURP)) {
        valido = "Válido";
        resultado.classList.add("ok");
    } else {
        resultado.classList.remove("ok");
    }

    resultado.innerText = "CURP: " + CURP + "\nFormato: " + valido;
}

function ValidarEmail(evt) {
    var reg = "/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/g";
    var ErrorMessage = document.getElementById("EmailError");
    ErrorMessage.textContent = '';
    //console.log(ErrorMessage);
    console.log(evt)
    if (reg.test(evt.value) == false) {
        //alert('Invalid Email Address');
        ErrorMessage.textContent = 'Correo no valido';
        evt.style.borderColor = 'red';
        //return false;
    } else {
        ErrorMessage.textContent = '';
        evt.style.borderColor = 'green';
    }

}

function validarFormulario() {
    let inputs = $('#Form input, #Form select');
    let formularioValido = true;

    inputs.each(function () {
        let input = $(this);
        if ((input.is('input') && input.val().trim() === '') || (input.is('select') && input.val() === '0')) {
            formularioValido = false;
            alert('El campo "' + input.attr('name') + '" está vacío. Por favor, complete todos los campos.');
            return false;
        }
    });

    if (formularioValido) {
        alert('Formulario validado correctamente.');
    }

    return formularioValido;
}

$('#Form').submit(function (event) {
    event.preventDefault();
    if (validarFormulario()) {
        this.submit();
    }
});
   