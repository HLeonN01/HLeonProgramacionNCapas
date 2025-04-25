
    ///*****************************FUNCIONES PARA LA IMAGEN
    function ValidarImagen() {
        var input = $('#Foto')[0].files[0].name.split('.').pop().toLowerCase()
        var extensionesValidas = ['png', 'jpg', 'jpeg', 'webp']
        var banderaImg = false

        for (var i = 0; i <= extensionesValidas.length; i++) {
            if (input == extensionesValidas[i]) {
                banderaImg = true
            }
        }

        if (!banderaImg) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: `Los archivos permitidos son ${extensionesValidas}!`
            });
            $('#Foto').val("")
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
        if (fileSize > 2) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "La imagen no puede superar los 2MB"
            });
            $('#Foto').val("")
        }
    }

    function eliminarImagenPrevizualizada() {
        $('#Foto').val("")
        $('#img').attr('src', "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdnWgZDAdXZemvgse9Ky3sguQEMSeVUkxkcsk_ZFvu9uLsbaEAjdfBLamh7giYmG6vWZs&usqp=CAU")
    }

    ///***********************************FUNCIONES PARA EL CURRICULUM
    function ValidarCurriculum() {
        var inputCV = $('#Curriculum')[0].files[0].name.split('.').pop().toLowerCase()
        console.log(inputCV)
        var extensionValida = ['pdf']
        var banderaCV = false
        if (inputCV == extensionValida) {
            banderaCV = true
        }
        else if (banderaCV != extensionValida) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: `El archio permitido es formato ${extensionValida}!`
            });
            $('#Curriculum').val("")
        }
    }

    function ValidarTamCV(input) {
        const fileSize = input.files[0].size / 1024 / 1024;
        if (fileSize > 2) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "El curriculum no puede superar los 2MB"
            });
            $('#Curriculum').val("")
        }
    }
    function eliminarCurriculum() {
        $('#Curriculum').val("")
    }

    ///******************* Funcion para campos

    function SoloLetras(evt) {
        var entrada = String.fromCharCode(evt.which)
        var inputField = evt.target;
        var ErrorMessage = inputField.parentNode.querySelector('.error')
        ErrorMessage.textContent = ' ';
        if (!(/[a-z A-Z]/.test(entrada))) {
            evt.preventDefault()
            inputField.style.borderColor = 'red';
            ErrorMessage.textContent = 'Solo se aceptan letras';
        }
        else {
            inputField.style.borderColor = '';
            ErrorMessage.textContent = '';
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
            inputField.style.borderColor = '';
            ErrorMessage.textContent = '';
        }
    }

    function ValidarEmail(evt) {
        var reg = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/
        console.log(reg)
        var ErrorMessage = document.getElementById("error");
        ErrorMessage.textContent = '';
        if (reg.test(evt.value) == false) {
            ErrorMessage.textContent = 'Correo no valido';
            evt.style.borderColor = 'red';
        } else {
            ErrorMessage.textContent = '';
            evt.style.borderColor = '';
        }
    }
