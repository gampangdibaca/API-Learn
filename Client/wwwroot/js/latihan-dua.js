$(document).ready(function () {
    let table = $('#employee-table').DataTable({
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
                "data": "gender"
            },
            {
                "data": "salary",
                "render": (data, type, row) => {
                    let s = "";
                    for (var i = 1; i < data.length; i++) {
                        console.log("www");
                        s = data.charAt(data.length - i) + s;
                        if (i % 3 == 0 && i != data.length) {
                            s = "." + s;
                        }
                        console.log(s);
                    }
                    return "Rp" + data;
                }
            }
        ],
        "pagingType": "simple_numbers"
    });
    table.on('order.dt search.dt', function () {
        let i = 1;

        table.cells(null, 0, { search: 'applied', order: 'applied' }).every(function (cell) {
            this.data(i++);
        });
    }).draw();
});