$(document).ready(function () {
    $.ajax({
        url: "/Areas/ems/API/CIReport", success: function (data) {
            //console.log(data);
            dataSet = data;
            $('#cireporttable').DataTable({
                "processing": true,
                data: data,
                columns: [
                    { "data": "Title" },
                    { "data": "Npi" },
                    { "data": "Date" }
                ],
                "columnDefs": [
                    {
                        "render": function (data, type, row) {
                            return '<a  href="' + row.Url + '" target="_blank">' + data + '</a>';
                        },
                        "targets": [0]
                    }
                ],
                "bDestroy": true
            });
        }
    });
});