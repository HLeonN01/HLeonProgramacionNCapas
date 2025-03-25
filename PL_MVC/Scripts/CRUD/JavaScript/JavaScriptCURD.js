$(document).ready(function () {
    GetAll();
});
function showModal() {
    if ($("#inptIdUsuario").val()) {
        GetById();       
    } else {
        GetAllEstados();
        GetAllRoles();
        $('#idModal').modal("show");
    }
}
function GetAll() {
    let url = window.location.origin;
    let newUrl = url + "/JavaScript/GetAll";
    $.ajax({
        url: newUrl,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            console.log(result);
            if (result.Correct) {
                let body = $('#Idtbody');
                body.empty();  
                let seenIds = new Set();
                $.each(result.Objects, function (i, valor) {
                    if (!seenIds.has(valor.IdUsuario)) {
                        seenIds.add(valor.IdUsuario);
                        let Imagen = valor.ImagenBase64
                            ? `<img src="data:image/png;base64, ${valor.ImagenBase64}" alt="Imagen del usuario" style="width: 50px; height: 50px;">`
                            : "Sin imagen";
                        let fila = `<tr data-id="${valor.IdUsuario}">                                                    
                            <td>${valor.UserName}</td>
                            <td>${valor.Nombre} ${valor.ApellidoPaterno} ${valor.ApellidoMaterno}</td>
                            <td>${valor.Email}</td>
                            <td>${Imagen}</td>
                            <td>Calle: ${valor.Direccion.Calle} Num Interior: ${valor.Direccion.NumeroInterior} Num.Ext: ${valor.Direccion.NumeroExterior}</td>
                            <td>${valor.Rol.Nombre}</td>                        
                            <td><button type="button" class="btn btn-outline-warning form-control" onclick="GetById(${valor.IdUsuario})" >Editar</button></td>
                            <td><button type="button" class="btn btn-outline-danger form-control" onclick="Delete(${valor.IdUsuario})">Eliminar</button></td>            
                        <tr>`;
                        body.append(fila);
                    }
                });
            } else {
                console.log("Error", result.ErrorMessage);
            }
        },
        error: function (xhr, status, error) {
            console.log("Error", error)
        }
    });
}
function GetAllEstados(callback) {
    let url = window.location.origin;
    let newurl = url + "/JavaScript/GetAllEstados";
    $.ajax({
        url: newurl,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            console.log(result);
            if (result.Correct) {
                let ddlEstado = $("#selectEstado");
                ddlEstado.empty();
                let ddlMunicipio = $('#selectMunicipio');
                let ddlColonia = $('#selectColonia');
                ddlMunicipio.empty();
                ddlColonia.empty();
                let municipioDefault = "<option value='0'>Seleccione el municipio</option>";
                let coloniaDefault = "<option value='0'>Seleccione su colonia</option>";
                let ddlEstadoDefault = "<option value='0'>Seleccione su estado</option>";
                ddlEstado.append(ddlEstadoDefault);
                ddlMunicipio.append(municipioDefault);
                ddlColonia.append(coloniaDefault);
                $.each(result.Objects, function (i, valor) {
                    let option = `<option value="${valor.IdEstado}"">${valor.Nombre}</option>`;
                    ddlEstado.append(option);
                });
                if (callback) {
                    callback(result.Objects);
                }
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}
function GetAllRoles(callback) {
    let url = window.location.origin;
    let newurl = url + "/JavaScript/GetAllRoles";
    $.ajax({
        url: newurl,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            //console.log(result);
            if (result.Correct) {
                let ddlRoles = $("#selectRoles");
                ddlRoles.empty();
                let rolesDefault = "<option value='0'>Seleccione el rol</option>";
                ddlRoles.append(rolesDefault);
                $.each(result.Objects, function (i, valor) {
                    let option = `<option value="${valor.IdRol}">${valor.Nombre}</option>`;
                    ddlRoles.append(option);
                });

                if (callback) {
                    callback(result.Objects);
                }
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}
function MunicipioGetByIdEstado() {
    let url = window.location.origin;
    let newurl = url + "/JavaScript/GetByIdEstado";
    let ddlEstado = $("#selectEstado").val();
    $.ajax({
        url: newurl + "?IdEstado=" +  ddlEstado,
        type: "GET",
        dataType: "JSON",
        //data -> SOLO PARA MODELOS
        success: function (result) {
            if (result.Correct) {
                let ddlMunicipio = $('#selectMunicipio');
                let ddlColonia = $('#selectColonia');
                ddlMunicipio.empty();
                ddlColonia.empty();
                let municipioDefault = "<option value='0'>Seleccione el municipio</option>";
                let coloniaDefault = "<option value='0'>Seleccione su colonia</option>";
                ddlMunicipio.append(municipioDefault);
                ddlColonia.append(coloniaDefault);
                $.each(result.Objects, function (i, valor) {
                    let option = "<option value=" + valor.IdMunicipio + ">" + valor.Nombre + "</option>";
                    ddlMunicipio.append(option);
                });
            }
            ColoniaGetByIdMunicipio()
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function MunicipioGetByIdEstadoModal(idMunicipio) {
    let url = window.location.origin;
    let newurl = url + "/JavaScript/GetByIdEstado";
    let ddlEstado = $("#selectEstado").val();
    $.ajax({
        url: newurl + "?IdEstado=" + ddlEstado,
        type: "GET",
        dataType: "JSON",
        //data -> SOLO PARA MODELOS
        success: function (result) {
            if (result.Correct) {
                let ddlMunicipio = $('#selectMunicipio');
                let ddlColonia = $('#selectColonia');
                let municipioDefault = "<option value='0'>Seleccione el municipio</option>";
                let coloniaDefault = "<option value='0'>Seleccione su colonia</option>";
                ddlMunicipio.append(municipioDefault);
                ddlColonia.append(coloniaDefault);
                $.each(result.Objects, function (i, valor) {
                    let option = "<option value=" + valor.IdMunicipio + ">" + valor.Nombre + "</option>";
                    ddlMunicipio.append(option);
                });
            }
            ColoniaGetByIdMunicipio()
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function ColoniaGetByIdMunicipio() {
    let url = window.location.origin;
    let newurl = url + "/JavaScript/GetByIdMunicipio";
    let ddlEstado = $("#selectMunicipio").val();
    $.ajax({
        url: newurl + "?IdMunicipio=" + ddlEstado,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                let ddlColonia = $('#selectColonia');
                ddlColonia.empty();
                let optionDefault = "<option value=0>Seleccione su colonia</option>";
                ddlColonia.append(optionDefault);
                $.each(result.Objects, function (i, valor) {
                    let option = "<option value=" + valor.IdColonia + ">" + valor.Nombre + "</option>";
                    ddlColonia.append(option);
                });
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function ColoniaGetByIdMunicipioModal() {
    let url = window.location.origin;
    let newurl = url + "/JavaScript/GetByIdMunicipio";
    let ddlEstado = $("#selectMunicipio").val();
    $.ajax({
        url: newurl + "?IdMunicipio=" + ddlEstado,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                let ddlColonia = $('#selectColonia');
                let optionDefault = "<option value=0>Seleccione su colonia</option>";
                ddlColonia.append(optionDefault);
                $.each(result.Objects, function (i, valor) {
                    let option = "<option value=" + valor.IdColonia + ">" + valor.Nombre + "</option>";
                    ddlColonia.append(option);
                });
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}
$("#datepicker").datepicker({
    dateFormat: "dd-mm-yy",
    showAnim: "clip"
});
function Add() {
    let url = window.location.origin;
    let newUrl = url + "/JavaScript/Form";
    $.ajax({
        url: newUrl,
        type: "POST",
        dataType: "JSON",
        data: {
            idUsuario: $("#inptIdUsuario").val(),
            nombre: $("#inptNombre").val(),
            apellidoPaterno: $("#inptApellidoPaterno").val(),
            apellidoMaterno: $("#inptApellidoMaterno").val(),
            curp: $("#inptCURP").val(),
            fechaNacimiento: $("#datepicker").val(),
            sexo: $("#rdSexo").val(),
            telefono: $("#inptTelefono").val(),
            celular: $("#inptCelular").val(),
            direccion: {
                calle: $("#inptCalle").val(),
                numeroInterior: $("#inptNumeroInterior").val(),
                numeroExterior: $("#inptNumeroExterior").val(),
                colonia: {
                    idColonia: $("#selectColonia").val(),
                    municipio: {                   
                        idMunicipio: $("#selectMunicipio").val(),
                        estado: {                        
                            idEstado: $("#selectEstado").val(),
                        }
                    }
                }
            },            
            userName: $("#inptUserName").val(),
            email: $("#inptCorreo").val(),
            password: $("#inptPassword").val(),
            estatus: $("#inptEstatus").val(),
            rol: {
                idRol: $("#selectRoles").val(),
            },
            imagen: $("#inptImage").val()
        },
        success: function (result) {
            console.log(result);
            if (result.Correct) {
                let idUsuario = $("#inptIdUsuario").val();
                if (idUsuario !== "") {
                    $(`#Idtbody tr[data-id='${idUsuario}']`).remove();
                }
                alert(result.Correct ? (idUsuario ? 'El usuario se actualizó exitosamente' : 'El usuario se guardó exitosamente') : 'Hubo un error al procesar la solicitud');
                $("#idModal").modal('hide');
                GetAll();
            } else {
                alert("Hubo un error al procesar la solicitud");
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}
function GetById(idUsuario) {    
    let url = window.location.origin;
    let newUrl = url + "/JavaScript/GetById";
    $.ajax({
        url: newUrl+"?IdUsuario="+idUsuario,
        type: "GET",
        dataType: "JSON",                   
        success: function (result) {
            console.log(result);
            if (result.Correct) {
                $("#inptIdUsuario").val(result.Object.IdUsuario);
                $("#inptNombre").val(result.Object.Nombre);   
                $("#inptApellidoPaterno").val(result.Object.ApellidoMaterno);
                $("#inptApellidoMaterno").val(result.Object.ApellidoPaterno);
                $("#inptCURP").val(result.Object.CURP);
                $("#datepicker").val(result.Object.FechaNacimiento);
                if (result.Object.Sexo == "M") {
                    $("{-input[name='sexo'][value=" + result.Object.Sexo + "]").prop("checked", true); 
                }
                if (result.Object.Sexo == "H") {
                    $("input[name='sexo'][value=" + result.Object.Sexo + "]").prop("checked", true);
                }
                $("#inptTelefono").val(result.Object.Telefono);
                $("#inptCelular").val(result.Object.Celular)
                $("#inptCalle").val(result.Object.Direccion.Calle);
                $("#inptNumeroInterior").val(result.Object.Direccion.NumeroInterior);
                $("#inptNumeroExterior").val(result.Object.Direccion.NumeroExterior);                
                $("#inptUserName").val(result.Object.UserName);
                $("#inptCorreo").val(result.Object.Email);
                $("#inptPassword").val(result.Object.Password);
                $("#inptPasswordConfirm").val(result.Object.Password);
                $("#inptEstatus").prop('checked', result.Object.Estatus === 'true')
                
                GetAllRoles(function () {
                    $("#selectRoles").val(result.Object.Rol.IdRol);
                });

                
                GetAllEstados(function () {
                    $("#selectEstado").val(result.Object.Direccion.Colonia.Municipio.Estado.IdEstado);                    
                });

                MunicipioGetByIdEstadoModal(function () {
                    $("#selectMunicipio").val(result.Object.Direccion.Colonia.Municipio.IdMunicipio);
                });
                console.log(MunicipioGetByIdEstadoModal);

                if (result.Object.Imagen != null) {
                   //console.log($("#inptImage").attr("src", "data:image/png;base64," + result.Object.Imagen));
                } else {
                    $("#inptImage").attr("src", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdnWgZDAdXZemvgse9Ky3sguQEMSeVUkxkcsk_ZFvu9uLsbaEAjdfBLamh7giYmG6vWZs&usqp=CAU");
                }
                $('#idModal').modal("show");
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}
function Delete(idUsuario) {
    let url = window.location.origin;
    let newUrl = url + "/JavaScript/Delete";
    $.ajax({
        url: newUrl + "?IdUsuario=" + idUsuario,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            alert("El usuario se elimino con exito");
            $("#idModal").modal('hide');
            GetAll();
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}
function ValidarImagen() {
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