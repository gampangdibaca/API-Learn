$("#login-form").submit((e) => {
    e.preventDefault();

    let formItems = document.querySelectorAll("#login-form .form-control:invalid");
    if (formItems.length <= 0) {
        let obj = new Object();
        obj.email = $("#login-email").val();
        obj.password = $("#login-password").val();

        console.log(obj);

        $.ajax({
            url: "Auth/",
            type: "POST",
            data: obj, //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
            context: document.body,
            //dataType: 'JSON',
            //contentType: 'application/json; charset=utf-8'
        }).done((result) => {
            //buat alert pemberitahuan jika success
            
            console.log(result);
            switch (result.status) {
                case 200:
                    window.location.replace("../Dashboard/")
                    break;
                default:
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: result.message,
                    })
            }
        }).fail((error) => {
            //alert pemberitahuan jika gagal
            console.log(error);
            console.log(error.responseJSON);
            Swal.fire({
                title: 'Error!',
                text: error.responseJSON.message,
                icon: 'error',
                confirmButtonText: 'Ok',
                confirmButtonColor: '#eb2f06'
            });
        });
    }

})