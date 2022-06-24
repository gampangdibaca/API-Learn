$("#logout-btn").click((e) => {
    e.preventDefault();
    Swal.fire({
        title: 'Logging Out!!!',
        text: "Are you sure you want to log out ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.replace("/Logout");
        }
    })
})