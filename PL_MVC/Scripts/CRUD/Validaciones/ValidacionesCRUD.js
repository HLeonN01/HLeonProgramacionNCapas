
function ValidarImagen() {
    var input = $('#inptImage')[0].files[0].name.split('.').pop().toLowerCase()
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
        $('#inptImage').val("")
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

function VisualizarImagen(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (elemento) {
            $('#idUsuarioImagen').attr('src', elemento.target.result)
        }
        reader.readAsDataURL(input.files[0])
    } else {
        $('#idUsuarioImagen').attr('src', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdnWgZDAdXZemvgse9Ky3sguQEMSeVUkxkcsk_ZFvu9uLsbaEAjdfBLamh7giYmG6vWZs&usqp=CAU');
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
    var reg = new RegExp("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
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

//function ValidarPassword(evt) {
//    //let inputField = evt.target;
//    const regex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
//    var ErrorMessage = document.getElementById("inptPassword");
//    ////ErrorMessage.textContent = '';
//    //var inputField = docucument.getElementById("password");
//    //var ErrorMessage = inputField.parentNode.querySelector('.error')
//    ErrorMessage.textContent = ' ';
//    //console.log(entrada);

//    if ((regex.test(evt.value) == false)) {
//        //evt.style.borderColor = 'red';
//        ErrorMessage.textContent = 'Las contraseñas deben tener un minimo 7 Letras, una mayuscula y un numero';
//        evt.style.borderColor = 'red';
//    } else {
//        //evt.style.borderColor = 'green';
//        ErrorMessage.textContent = '';
//        evt.style.borderColor = 'green';
//    }
//}


function ValidarConfirmacionPassword() {
    var password = $('#inptPassword').val().trim();
    var confirm = $('#inptPasswordConfirm').val().trim();
    var ErrorMessage = document.getElementById("message");
    var Input = document.getElementById("inptPasswordConfirm");
    ErrorMessage.textContent = '';
    console.log(password);
    console.log(confirm);
    if (password == '' && confirm == '') {
        Input.style.borderColor = 'red';
        ErrorMessage.textContent = 'Ingrese una contraseña';
    }
    else if (password === confirm) {
        Input.style.borderColor = 'green';
        ErrorMessage.textContent = '';
    } else {
        Input.style.borderColor = 'red';
        ErrorMessage.textContent = 'Las contraseñas no coinciden';
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
