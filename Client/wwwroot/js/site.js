$(document).ready(function () {
     var data = message;
    if (data != "") {
        Swal.fire({
            text: data,
            confirmButtonText: 'Ok'
        });
    }
});