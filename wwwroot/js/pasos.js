﻿function manejarClickAgregarPaso() {

    const indice = tareaEditarVM.pasos().findIndex(p => p.esNuevo());

    if (indice !== -1) return;

    tareaEditarVM.pasos.push(new pasoViewModel({ modoEdicion: true, realizado: false }));
    $("[name=txtPasoDescripcion]:visible").focus();
}

function manejarClickCancelarPaso(paso) {
    if (paso.esNuevo()) {
        tareaEditarVM.pasos.pop();
    } else {
        paso.modoEdicion(false);
        paso.descripcion(paso.descripcionAnterior);
    }
}

async function manejarClickGuardarPaso(paso) {
    paso.modoEdicion(false);
    const esNuevo = paso.esNuevo();
    const idTarea = tareaEditarVM.id;
    const data = obtenerCuerpoPeticionPaso(paso);

    const descripcion = paso.descripcion();

    if (!descripcion) {
        paso.descripcion(paso.descripcionAnterior);

        if (esNuevo) {
            tareaEditarVM.pasos.pop();
        }
        return;
    }

    if (esNuevo) {
        await insertarPaso(paso, data, idTarea);
    } else {
        actualizarPaso(data, paso.id());
    }
}

async function insertarPaso(paso, data, idTarea) {
    const respuesta = await fetch(`${urlPasos}/${idTarea}`, {
        body: data,
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (respuesta.ok) {
        const json = await respuesta.json();
        paso.id(json.id);
    } else {
        manejarErrorApi(respuesta);
    }
}

function obtenerCuerpoPeticionPaso(paso) {
    return JSON.stringify({
        descripcion: paso.descripcion(),
        realizado: paso.realizado()
    });
}

function manejarClickDescripcionPaso(paso) {
    paso.modoEdicion(true);
    paso.descripcionAnterior = paso.descripcion();
    $("[name=txtPasoDescripcion]:visible").focus();
}

function manejarClickCheckBoxPaso(paso) {

    if (paso.esNuevo()) {
        return true;
    }

    const data = obtenerCuerpoPeticionPaso(paso);
    actualizarPaso(data, paso.id());

    return true;
}

async function actualizarPaso(data, id) {
    const respuesta = await fetch(`${urlPasos}/${id}`, {
        body: data,
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (!respuesta.ok) {
        manejarErrorApi(respuesta);
    }
}

function manejarClickBorrarPaso(paso) {
    modalEditarTareaBootstrap.hide();
    confirmarAccion({
        callBackAceptar: () => {
            borrarPaso(paso);
            modalEditarTareaBootstrap.show();
        },
        callBackCancelar: () => {
            modalEditarTareaBootstrap.show();
        },
        titulo: `¿Desea borrar este paso?`
    })
}

async function borrarPaso(paso) {
    const respuesta = await fetch(`${urlPasos}/${paso.id()}`, {
        method: 'DELETE'
    });

    if (!respuesta.ok) {
        manejarErrorApi(respuesta);
        return;
    }

    tareaEditarVM.pasos.remove(function (item) { return item.id() == paso.id() });
}