$(document).ready(function () {
    if (window.location.pathname === "/review") {
        console.log("Review kebuka");
        PageReview();
    }
    

    let table = $("#tableMProject").DataTable({
        ajax: {
            url: "https://localhost:7229/api/Projects/Master",
            dataType: "Json",
            dataSrc: "data" //need notice, kalau misal API kalian 
        },
        columns: [
            {
                "data": "projectTitle"
            },
            {
                "data": "description"
            },
            {
                "data": "nik"
            },
            {
                "data": "name"
            },
            {
                "data": "email"
            },
            {
                "data": "className",
            },
            {
                "data": "batch"
            },
            {
                "data": "statusName"
            },
            {
                "data": "id",
                render: function (data, type, row) {
                    return `<button onclick="viewUML(\'${data}\')" class="btn btn-info">UML</button> 
                            <button onclick="viewBPMN(\'${data}\')" class="btn btn-danger">BPMN</button>`
                }
            },
            {
                "data": "id",
                render: function (data, type, row) {
                    return `<button id="review_\'${data}\'" onclick="Review(\'${data}\')" class="btn btn-info">Review</button>
                            <script>
                            document.getElementById("review_\'${data}\'").addEventListener("click", function(){
                              window.open("/review?id=" + encodeURIComponent(\'${data}\'), "_blank");
                            });
                            </script>`
                }
            },
        ],
    });
});

function viewUML(id) {

    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Projects/' + id,
        success: function (data) {
            $('#modalLabel').html(data.data.projectTitle);
            //Add the image to the modal body
            document.getElementById("modal-body-image").src = "data:image/jpg;base64," + data.data.uml;
            //Show the modal
            $("#modal").modal("show");
        },
    })
}

function viewBPMN(id) {

    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Projects/' + id,
        success: function (data) {
            $('#modalLabel').html(data.data.projectTitle);
            //Add the image to the modal body
            document.getElementById("modal-body-image").src = "data:image/jpg;base64," + data.data.bpmn;
            //Show the modal
            $("#modal").modal("show");
        },
    })
}

function Review(id) {
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Projects/Master/' + id,
        success: function (data) {
            $('#re_title').html(data.data[0].projectTitle);
            $('#re_description').html(data.data[0].description);
            console.log(data.data[0].statusname);
            $('#re_currentstatus').html(data.data[0].description);
            $('#re_nik').html(data.data[0].nik);
            $('#re_name').html(data.data[0].name);
            $('#re_email').html(data.data[0].email);
            $('#re_class').html(data.data[0].className);
            $('#re_nik').html(data.data[0].nik);
        },
    });
};

function PostReview() {

    var id = GetId();
    var currentDateTime = new Date();
    console.log(currentDateTime);

    var obj = new Object();
    obj.projectID = id;
    obj.time = currentDateTime;
    obj.message = $("#message").val();
    obj.revision = $("#revision").val();
    obj.statusID = $("#status").val();

    $.ajax({
        type: "POST",
        url: 'https://localhost:7229/api/Histories/PostHistory',
        dataType: "Json",
        contentType: "application/json",
        data: JSON.stringify(obj)
    }).done((result) => {
        Swal.fire(
            'Data Berhasil Disimpan'
        );
    }).fail((error) => {
        Swal.fire(
            'Gagal Disimpan'
        );
    });

};

function PageReview() {
    var id = GetId();
    if (id) {
        console.log("ID: " + id);
    }

    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Projects/Master/' + id,
        success: function (data) {
            console.log(data.data[0]);
            document.getElementById('re_title').value = (data.data[0].projectTitle);
            document.getElementById('re_description').value = (data.data[0].description);
            document.getElementById('re_currentstatus').value = (data.data[0].statusName);
            document.getElementById('re_nik').value = (data.data[0].nik);
            document.getElementById('re_name').value = (data.data[0].name);
            document.getElementById('re_email').value = (data.data[0].email);
            document.getElementById('re_class').value = (data.data[0].className);
            document.getElementById('re_batch').value = String((data.data[0].batch));
            document.getElementById("btn_UML_").onclick = function viewUML() { };
        },
    });

}

function GetId() {
    function getQueryVariable(variable) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == variable) {
                return decodeURIComponent(pair[1]);
            }
        }
        return null;
    }
    var id = getQueryVariable("id");
    return id;
}