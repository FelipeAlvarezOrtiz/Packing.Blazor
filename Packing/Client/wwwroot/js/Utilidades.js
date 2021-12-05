export function Exito() {
    return Swal.fire({
        icon: 'success',
        title: 'La información ha sido guardada correctamente.',
        showConfirmButton: false,
        timer: 1500
    });
}

export function Error() {
    return Swal.fire({
        icon: 'error',
        title: 'Ha ocurrido un error interno.',
        showConfirmButton: false,
        timer: 1500
    });
}