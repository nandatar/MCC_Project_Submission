var form = document.getElementById("upload-form");
var fileInput = document.getElementById("image-file");

form.addEventListener("submit", function (event) {
    event.preventDefault();

    var formData = new FormData();
    formData.append("image-file", fileInput.files[0]);

    // Send the form data to the server using an HTTP POST request
    fetch("/api/upload", {
        method: "POST",
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            // Handle the server response
            console.log(data);
        })
        .catch(error => {
            console.error("Error uploading image:", error);
        });
});
