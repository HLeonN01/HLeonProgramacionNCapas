$(document).ready(function () {
    GetAll();
});
function showModal() {
    limpiarFormulario();
    if ($("#inptIdUsuario").val()) {
        GetById();       
    } else {
        GetAllEstados();
        GetAllRoles();
        $('#idModal').modal("show");
    }
}
function limpiarFormulario() {
    $("#inptIdUsuario").val(""); 
    $("#inptNombre").val("");
    $("#inptApellidoPaterno").val("");
    $("#inptApellidoMaterno").val("");
    $("#inptCURP").val("");
    $("#datepicker").val("");
    $("input[name='sexo']").prop("checked", false);
    $("#inptTelefono").val("");
    $("#inptCelular").val("");
    $("#inptCalle").val("");
    $("#inptNumeroInterior").val("");
    $("#inptNumeroExterior").val("");
    $("#selectEstado").val("");
    $("#selectMunicipio").val("");
    $("#selectColonia").val("");
    $("#inptUserName").val("");
    $("#inptCorreo").val("");
    $("#inptPassword").val("");
    $("#inptPasswordConfirm").val("");
    $("#inptEstatus").prop("checked", true);
    $("#selectRoles").val("");
    $("#inptImage").val("");
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
                        let Imagen = valor.ImagenBase64 && valor.ImagenBase64.trim() !== ""
                            ? `<img src="data:image/png;base64, ${valor.ImagenBase64}" alt="Imagen del usuario" style="width: 100px; height: 80px;">`
                            : `<img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdnWgZDAdXZemvgse9Ky3sguQEMSeVUkxkcsk_ZFvu9uLsbaEAjdfBLamh7giYmG6vWZs&usqp=CAU" alt="Imagen por defecto" style="width: 150px; height: 80px;">`;
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
function MunicipioGetByIdEstado(callback) {
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
                if (callback) {
                    callback();
                }
                ColoniaGetByIdMunicipio();
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

function ColoniaGetByIdMunicipio(callback) {
    let url = window.location.origin;
    let newurl = url + "/JavaScript/GetByIdMunicipio";
    let ddlMunicipio = $("#selectMunicipio").val();
    $.ajax({
        url: newurl + "?IdMunicipio=" + ddlMunicipio,
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
                if (callback) {
                    callback();
                }
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
$("#inptImage").change(function () {
    let file = this.files[0];
    let reader = new FileReader();
    reader.onloadend = function () {
        $("#inptImageBase64").val(reader.result.split(",")[1]); 
    };
    reader.readAsDataURL(file);
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
            sexo: $("input[name='sexo']:checked").val(),
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
            imagenBase64: $("#inptImageBase64").val()
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
                $("#intpIdDireccion").val(result.Object.Direccion.IdDireccion);
                $("#inptNombre").val(result.Object.Nombre);   
                $("#inptApellidoPaterno").val(result.Object.ApellidoMaterno);
                $("#inptApellidoMaterno").val(result.Object.ApellidoPaterno);
                $("#inptCURP").val(result.Object.CURP);
                $("#datepicker").val(result.Object.FechaNacimiento);                
                $("input[name='sexo'][value='" + result.Object.Sexo.trim() + "']").prop("checked", true);
                $("#inptTelefono").val(result.Object.Telefono);
                $("#inptCelular").val(result.Object.Celular)
                $("#inptCalle").val(result.Object.Direccion.Calle);
                $("#inptNumeroInterior").val(result.Object.Direccion.NumeroInterior);
                $("#inptNumeroExterior").val(result.Object.Direccion.NumeroExterior);                
                $("#inptUserName").val(result.Object.UserName);
                $("#inptCorreo").val(result.Object.Email);
                $("#inptPassword").val(result.Object.Password);
                $("#inptPasswordConfirm").val(result.Object.Password);
                $("#inptEstatus").prop('checked', result.Object.Estatus === true);
                let imagenUsuario = result.Object.ImagenBase64 && result.Object.ImagenBase64.trim() !== ""
                    ? `data:image/png;base64, ${result.Object.ImagenBase64}`
                    : "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdnWgZDAdXZemvgse9Ky3sguQEMSeVUkxkcsk_ZFvu9uLsbaEAjdfBLamh7giYmG6vWZs&usqp=CAU";
                $('#ImagenBase64').attr('src', imagenUsuario);
                //$('#idUsuarioImagen').attr('src', imagenUsuario);
                
                GetAllRoles(function () {
                    $("#selectRoles").val(result.Object.Rol.IdRol);
                });

                
                GetAllEstados(function () {
                    $("#selectEstado").val(result.Object.Direccion.Colonia.Municipio.Estado.IdEstado);

                    MunicipioGetByIdEstado(function () {
                        $("#selectMunicipio").val(result.Object.Direccion.Colonia.Municipio.IdMunicipio);

                        ColoniaGetByIdMunicipio(function () {
                            $("#selectColonia").val(result.Object.Direccion.Colonia.IdColonia);

                        });
                    });
                });

                $('#idModal').modal("show");
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}
function Delete(idUsuario) {
    if (confirm("¿Estás seguro de que deseas eliminar este usuario?")) {
        let url = window.location.origin;
        let newUrl = url + "/JavaScript/Delete";
        $.ajax({
            url: newUrl + "?IdUsuario=" + idUsuario,
            type: "GET",
            dataType: "JSON",
            success: function (result) {
                alert("El usuario se eliminó con éxito");
                $("#idModal").modal('hide');
                GetAll();  
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });
    } else {        
        console.log("Eliminación cancelada");
    }
}
