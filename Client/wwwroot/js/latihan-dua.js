let table;
let exportIndex = 0;

let generalExportOptions = {
    columns: [0, 1, 2, 3, 4, 5, 6],
    format: {
        body: function (data, rows, column, node) {
            if (exportIndex == table.data().count()) {
                exportIndex = 0;
            }
            return column === 0 ? ++exportIndex : data;
        }
    }
}
$(document).ready(function () {
    table = $('#employee-table').DataTable({
        "ajax": {
            "url": "https://localhost:44354/api/Employees/GetRegisteredData",
            "dataType": "json",
            "dataSrc": "data"
        },
        "columns": [
            {
                "data": "no",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "fullName"
            },
            {
                "data": "phone",
                "render": (data, type, row) => {
                    return "+62" + data.substring(1);
                }
            },
            {
                "data": "email"
            },
            {
                "data": "birthDate",
                "render": (data) => {
                    let date = new Date(Date.parse(data));
                    let tanggal = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                    let bulan = date.getMonth() < 9 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                    let dateString = tanggal + "/" + bulan + "/" + date.getFullYear();
                    return dateString;
                }
            },
            {
                "data": "gender"
            },
            {
                "data": "salary",
                "render": (data, type, row) => {
                    
                    return "Rp" + data;
                }
            },
            {
                "data": "nik",
                "render": (data) => {
                    return `<button type="button" class="btn btn-primary" onclick="updateEmployeeModal('${data}')" data-toggle="modal" data-target="#updateEmployeeModal"><i class="fas fa-user-edit"></i></button>
                            <button type="button" class="btn btn-danger" onclick="deleteEmployeeModal('${data}')" data-toggle="modal" data-target="#deleteEmployeeModal"><i class="fas fa-trash-alt"></i></button>`;
                }
            }
        ],
        "pagingType": "simple_numbers",
        columnDefs: [
            {
                searchable: false,
                orderable: false,
                targets: 0,
            },
            {
                searchable: false,
                orderable: false,
                targets: 7,
            }
        ],
        "order": [[1, 'asc']],
        "dom": 'lBfrtip',
        "buttons": {
            "name": 'primary',
            "buttons": [
                {
                    extend: 'csv',
                    className: 'ml-1 btn btn-custom rounded',
                    exportOptions: generalExportOptions
                },
                {
                    extend: 'excel',
                    className: 'ml-11 btn btn-custom rounded',
                    exportOptions: generalExportOptions
                },
                {
                    extend: 'pdf',
                    className: 'ml-11 btn btn-custom rounded',
                    exportOptions: generalExportOptions
                }
            ]
        }
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

function updateEmployeeModal(nik) {
    $("#update-nik").val(nik);
    let obj = { NIK: nik };
    $.ajax({
        type: "POST",
        url: "https://localhost:44354/api/Employees/GetEmployeeByNIK",
        context: document.body,
        dataType: 'JSON',
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
    }).done((result) => {
        console.log(result);
        $("#update-phone").val(result.data.phone);
        $("#update-firstName").val(result.data.firstName);
        $("#update-lastName").val(result.data.lastName);
        let currDate = new Date(Date.parse(result.data.birthDate)).toISOString().substr(0, 10);
        $("#update-birthDate").val(currDate);
        $("#update-salary").val(result.data.salary);
        $("#update-email").val(result.data.email);
        $("#update-gender").val(result.data.gender);
        generateUniversity(result.data.universityId);
        $("#update-degree").val(result.data.degree);
        $("#update-gpa").val(result.data.gpa);
        $("#update-education-id").val(result.data.educationId);
    }).fail((error) => {
        console.log(error);
    })
}

$("#update-form").submit((e) => {
    e.preventDefault();
    let formItems = document.querySelectorAll("#update-form .form-control:invalid");
    if (formItems.length == 0) {
        let obj = new Object();
        obj.nik = $("#update-nik").val();
        obj.firstName = $("#update-firstName").val();
        obj.lastName = $("#update-lastName").val();
        obj.phone = $("#update-phone").val();
        obj.birthDate = $("#update-birthDate").val();
        obj.salary = $("#update-salary").val();
        obj.email = $("#update-email").val();
        obj.gender = $("#update-gender").val();
        obj.account = null;
        updateEmployee(obj);
    }
    
});

function updateEmployee(updatedEmployee) {
    
    console.log(updatedEmployee);
    $.ajax({
        url: "https://localhost:44354/api/Employees",
        type: "PUT",
        data: JSON.stringify(updatedEmployee), //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        context: document.body,
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8'
    }).done((result) => {
        //buat alert pemberitahuan jika success
        $('#updateEmployeeModal').modal('toggle');
        $("#update-form")[0].reset();
        $("#update-form").removeClass("was-validated");
        console.log(result);
        table.ajax.reload();
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log(error.responseJSON.message);
        alert(error.responseJSON.message);
    });
    console.log("from func");
}

$("#update-education").submit((e) => {
    e.preventDefault();
    let formItems = document.querySelectorAll("#update-education .form-control:invalid");
    if (formItems.length == 0) {
        let obj = new Object();
        obj.university_id = $("#update-university").val();
        obj.id = $("#update-education-id").val();
        obj.degree = $("#update-degree").val();
        obj.gpa = $("#update-gpa").val();
        updateEducation(obj);
    }
    console.log("from event");

});

function updateEducation(obj) {
    $.ajax({
        url: "https://localhost:44354/api/Educations",
        type: "PUT",
        data: JSON.stringify(obj), //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        context: document.body,
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8'
    }).done((result) => {
        //buat alert pemberitahuan jika success
        $('#updateEmployeeModal').modal('toggle');
        $("#update-education")[0].reset();
        $("#update-education").removeClass("was-validated");
        console.log(result);
        table.ajax.reload();
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log(error.responseJSON.message);
        alert(error.responseJSON.message);
    });
}

let employeeNikToDelete = "";

function deleteEmployeeModal(nik) {
    employeeNikToDelete = nik;
    $("#deleteEmployeeMessage").html("Are you sure you want to delete employee with NIK : " + nik + " ?");
    console.log(nik);
}

function deleteEmployee() {
    let obj = { nik: employeeNikToDelete };
    $.ajax({
        type: "DELETE",
        url: "https://localhost:44354/api/Employees/DeleteEmployee",
        context: document.body,
        dataType: 'JSON',
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
    }).done((result) => {
        $('#deleteEmployeeModal').modal('toggle');
        console.log(result);
        table.ajax.reload();
    }).fail((error) => {
        alert(error.responseJSON.message);
        console.log(error.responseJSON.message);
    })
}

function employeeDetail(telpon) {
    console.log(telpon);
    let obj = { phone: telpon };
    $.ajax({
        type: "POST",
        url: "https://localhost:44354/api/Employees/GetEmployeeByPhone",
        context : document.body,
        dataType: 'JSON',
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
    }).done((result) => {
        console.log(result);
    }).fail((error) => {
        console.log(error);
    })
}

$("#register-form").submit((e) => {
    e.preventDefault();
    Insert();
});

function Insert() {
    let formItems = document.querySelectorAll("#register-form .form-control:invalid");
    if (formItems.length == 0) {
        var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
        //ini ngambil value dari tiap inputan di form nya
        obj.FirstName = $("#firstName").val();
        obj.LastName = $("#lastName").val();
        obj.Email = $("#email").val();
        obj.Phone = $("#phone").val();
        obj.BirthDate = $("#birthDate").val();
        obj.Salary = $("#salary").val();
        obj.Password = $("#password").val();
        obj.GPA = $("#gpa").val();
        obj.Gender = $("#gender").val();
        obj.Degree = $("#degree").val();
        obj.UniversityId = $("#university").val();
        console.log(obj);
        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
        $.ajax({
            url: "https://localhost:44354/api/Employees/register",
            type: "POST",
            data: JSON.stringify(obj), //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
            context: document.body,
            dataType: 'JSON',
            contentType: 'application/json; charset=utf-8'
        }).done((result) => {
            //buat alert pemberitahuan jika success
            $('#registerEmployeeModal').modal('toggle');
            $("#register-form")[0].reset();
            $("#register-form").removeClass("was-validated");
            console.log(result);
            table.ajax.reload();
        }).fail((error) => {
            //alert pemberitahuan jika gagal
            console.log(error.responseJSON);
            alert(error);
        });
    }
}

function generateUniversity(universityId) {
    let options = `<option selected disabled value="">Choose...</option>`;
    $.ajax({
        type: "GET",
        url: "https://localhost:44354/api/Universities/GetUniversities",
        context: document.body,
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
    }).done((result) => {
        console.log(result);
        for (let i = 0; i < result.data.length; i++) {
            options += `<option value="${result.data[i].id}">${result.data[i].name}</option>`;
        }
        $("#university").html(options);
        $("#update-university").html(options);
        if (universityId > 0) {
            $("#update-university").val(universityId);
        }
    }).fail((error) => {
        console.log(error);
    })
}

function formatNumber(data, column) {
    if (column === 0)
    return column === 0 ?
        ++exportIndex :
        data;
}