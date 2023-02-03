$(document).ready(function () {
    if (window.location.pathname === "/participant" || window.location.pathname === "/participant/" || window.location.pathname === "/Participant") {
        console.log("home terbuka");
        var nik = getCookie('nik');
        console.log(nik);
        getBiodata(nik);
        getProject(nik);
        setIcon(nik);
    }

    if (window.location.pathname === "/myproject" || window.location.pathname === "/MyProject/" || window.location.pathname === "/myproject/") {
        console.log("myproject terbuka");
        var nik = getCookie('nik');
        getBiodata(nik);
        console.log(nik);
        getProject(nik);
        getProjectEdit(nik);
        dataRevisi(nik);
        GetScore(nik);

    }

    if (window.location.pathname === "/submit" || window.location.pathname === "/Submit") {
        var nik = getCookie('nik');
        getBiodata(nik);
    }



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
                text: "This project will be deleted!",
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
            $('#nik').html(result.data.nik);
            console.log(result.data.nik);
            $('#user_login').html(result.data.name);
            $('#name1').html(result.data.name);
            $('#name2').html(result.data.name);
            $('#user_position').html(result.data.position);
            $('#position1').html(result.data.position);
            $('#position2').html(result.data.position);
            var class_ = (result.data.classID) == 1 ? 'Java' : '.NET';
            $('#class').html(class_);
            $('#class1').html(class_);
            $('#email').html(result.data.email);
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
                        /*$('#info_text').html("");*/
                        $('#project_title').html(result.data[0].projectTitle);
                        $('#description').html(result.data[0].description);
                        $('#status').html(result.data[0].statusName)
                        $('#project_projectTitle').html(result.data[0].projectTitle);
                        $('#project_description').html(result.data[0].description);
                        $('#project_status').html(result.data[0].statusName);
                        var status = (result.data[0].statusName);
                        console.log(status);
                        if (status == "Approved") {
                            console.log("delete atribute disabled");
                            document.getElementById("finish_btn").style.visibility = "visible";
                        }
                    }
                })
            }
        },
    })
}

function getProjectEdit(nik) {
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Participants/' + nik,
        success: function (result) {
            var project = result.data.projectID;
            if (project != null) {
                $.ajax({
                    type: "GET",
                    url: 'https://localhost:7229/api/Projects/Master/' + project,
                    success: function (result) {
                        $('#title_form').html(result.data[0].projectTitle);
                        console.log(result.data[0].projectTitle);
                        $('#description_form').html(result.data[0].description);
                        console.log(result.data[0].description);
                    }
                })
            }
            console.log("error")
        },
    })
}

function dataRevisi(nik) {
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Participants/' + nik,
        success: function (result) {
            $('#batch').html(result.data.batch);
            var project = result.data.projectID;
            if (project != null) {
                let table = $("#table_revisi_project").DataTable({
                    ajax: {
                        url: "https://localhost:7229/api/Histories/Project/" + project,
                        dataType: "Json",
                        dataSrc: "data" //need notice, kalau misal API kalian
                    },
                    columns: [
                        {
                            data: null,
                            render: function (data, type, row, meta) {
                                return meta.row + 1;
                            }
                        },
                        {
                            "data": "revision"
                        },
                        {
                            "data": "message"
                        },
                        {
                            "data": "statusID",         
                            render: function (data, type, row) {
                                if (data === 1) {
                                    return 'Waiting for Review';
                                } else if (data === 2) {
                                    return 'Have Revision';
                                } else if (data === 3) {
                                    return 'Approved';
                                } else if (data === 4) {
                                    return 'Declined';
                                } else if (data === 5) {
                                    return 'Waiting for Score';
                                } else if (data === 6) {
                                    return 'Finished';
                                }
                            }
                        },
                        {
                            "data": "time",
                            "render": function (data, type, row) {
                                var date = new Date(data);
                                return date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
                            }
                        },
                    ],
                });
                $.ajax({
                    type: "GET",
                    url: 'https://localhost:7229/api/Histories/Project/' + project,
                    success: function (result) {
                        var count = 0;
                        for (var i in result.data) {
                            count++
                        }
                        console.log(count);
                        $('#count_revisi').html(count);
                    }
                });

            }
        }
    })
}

function Finalization() {
    (function () {
        'use strict';
        window.addEventListener('load', function () {
            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.getElementsByClassName('needs-validation');
            // Loop over them and prevent submission
            var validation = Array.prototype.filter.call(forms, function (form) {
                form.addEventListener('submit', function (event) {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            });
        }, false);
    })();
    var nik = getCookie('nik');
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Participants/' + nik,
        success: function (result) {
            $('#batch').html(result.data.batch);
            var project = result.data.projectID
            if (project != null) {
                var obj = new Object();
                obj.id = project;
                obj.link = $("#link_github").val();;
                $.ajax({
                    type: "POST",
                    url: 'https://localhost:7229/api/Projects/Final',
                    dataType: "Json",
                    contentType: "application/json",
                    data: JSON.stringify(obj)
                }).done((result) => {
                    Swal.fire(
                        'Your project has been submited for scoring'
                    );
                    $('#modal').hide();
                }).fail((error) => {
                    Swal.fire(
                        'Failed to submit'
                    );
                });
            };
        }
    });
};

function GetScore(nik) {
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Participants/' + nik,
        success: function (result) {
            var project = result.data.projectID
            if (project != null) {
                $.ajax({
                    type: "GET",
                    url: 'https://localhost:7229/api/Projects/' + project,
                    success: function (result) {
                        var score = result.data.score;
                        if (score != null) {
                            console.log(score);
                            $('#score_value').html(result.data.score + '/5');
                            const stars = $('div.text-end svg');

                            for (let i = 0; i < score; i++) {
                                $(stars[i]).attr("fill", "orange");
                                $(stars[i]).attr("color", "orange");
                            }
                        }
                    }
                })
            }
        }
    })
}

function setIcon(nik) {
    $.ajax({
        type: "GET",
        url: 'https://localhost:7229/api/Employees/' + nik,
        success: function (result) {
            var class_ = (result.data.classID) == 1 ? 'Java' : '.NET';
            var icon_net = document.getElementById("icon_net");
            var icon_java = document.getElementById("icon_java");
            if (class_ == 'Java') {
                icon_net.style.visibility = "hidden";
                icon_java.style.visibility = "visible";
            }
            else {
                icon_net.style.visibility = "visible";
                icon_java.style.visibility = "hidden";
            }
        },
    })
}




