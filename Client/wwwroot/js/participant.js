$(document).ready(function () {
    if (window.location.pathname === "/Participant") {
        console.log("home terbuka");
        var nik = getCookie('nik');
        console.log(nik);
        getBiodata(nik);
        getProject(nik);
    }

    if (window.location.pathname === "/myproject") {
        console.log("myproject terbuka");
        var nik = getCookie('nik');
        console.log(nik);
        getProject(nik);
        dataRevisi(nik)
    }

    //$.ajax({
    //    type: "GET",
    //    url: 'https://localhost:7229/api/Employees/' + nik,
    //    success: function (result) {
    //        $('#name').html(result.data.name);
    //        $('#name1').html(result.data.name);
    //        $('#name2').html(result.data.name);
    //        var class_ = (result.data.classID) == 1 ? 'Java' : '.NET';
    //        $('#class').html(class_);
    //    },
    //})
    //$.ajax({
    //    type: "GET",
    //    url: 'https://localhost:7229/api/Participants/' + nik,
    //    success: function (result) {
    //        $('#batch').html(result.data.batch);
    //        var project = result.data.projectID
    //        if (project != null) {
    //            $.ajax({
    //                type: "GET",
    //                url: 'https://localhost:7229/api/Projects/Master/' + project,
    //                success: function (result) {
    //                    $('#info_text').html("");
    //                    $('#project_title').html(result.data[0].projectTitle);
    //                    $('#description').html(result.data[0].description);
    //                    $('#status').html(result.data[0].statusName)
    //                    $('#project_projectTitle').html(result.data[0].projectTitle);
    //                    $('#project_description').html(result.data[0].description);
    //                    $('#project_status').html(result.data[0].statusName)
    //                }
    //            })
    //        }
    //        else {
    //            $('#info_text').html("You dont have any project, please submit one.");
    //        }
    //    },
    //})

    // Use the value in JavaScript
    var data = message;
    console.log(data)
    if (data != "") {
        Swal.fire({
            text: data,
            confirmButtonText: 'Ok'
        });
    }

});

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function EditProject(id) {
    Swal.fire({
        text: "modal edit project keluar",
        confirmButtonText: 'Ok'
    });
}

function DeleteProject() {
    var nik = getCookie('nik');
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Participants/' + nik,
        success: function (result) {
            $('#batch').html(result.data.batch);
            var project = result.data.projectID;
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: 'https://localhost:7229/api/Projects?key=' + project,
                        type: 'DELETE',
                        success: function (response) {
                            Swal.fire(
                                'Deleted!',
                                'Your file has been deleted.',
                                'success'
                            );
                        },
                        error: function (response) {
                            alert("Something went wrong.");
                            console.log(response);
                        }
                    });
                }
            });
        }
    });
}


function getBiodata(nik) {
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Employees/' + nik,
        success: function (result) {
            $('#name').html(result.data.name);
            $('#name1').html(result.data.name);
            $('#name2').html(result.data.name);
            var class_ = (result.data.classID) == 1 ? 'Java' : '.NET';
            $('#class').html(class_);
        },
    })
}

function getProject(nik) {
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Participants/' + nik,
        success: function (result) {
            $('#batch').html(result.data.batch);
            var project = result.data.projectID
            if (project != null) {
                $.ajax({
                    type: "GET",
                    url: 'https://localhost:7229/api/Projects/Master/' + project,
                    success: function (result) {
                        $('#info_text').html("");
                        $('#project_title').html(result.data[0].projectTitle);
                        $('#description').html(result.data[0].description);
                        $('#status').html(result.data[0].statusName)
                        $('#project_projectTitle').html(result.data[0].projectTitle);
                        $('#project_description').html(result.data[0].description);
                        $('#project_status').html(result.data[0].statusName)
                    }
                })
            }
            else {
                $('#info_text').html("You dont have any project, please submit one.");
            }
        },
    })
}

function dataRevisi(nik) {
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Participants/' + nik,
        success: function (result) {
            $('#batch').html(result.data.batch);
            var project = result.data.projectID
            if (project != null) {
                let table = $("#table_revisi_project").DataTable({
                    ajax: {
                        url: "https://localhost:7229/api/Histories/Project/" + project,
                        dataType: "Json",
                        dataSrc: "data" //need notice, kalau misal API kalian 
                    },
                    columns: [
                        {
                            "data": "id"
                        },
                        {
                            "data": "revision"
                        },
                        {
                            "data": "message"
                        },
                        {
                            "data": "statusID"
                        },
                        {
                            "data": "time"
                        },
                    ],
                });
            }
        }
    })
}


