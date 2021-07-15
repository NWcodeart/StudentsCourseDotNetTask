
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



ShowInPopup  = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
}


$(document).ready(function () {
    //function will be called on button click having id btnsave
    $("#btnSave").click(function () {
        $.ajax(
            {
                type: "POST", //HTTP POST Method
                url: "/students/AddNewCourseToStudent", // Controller/View
                data: { //Passing data
                    courseId: $("#courseId").val(), //Reading text box values using Jquery
                },
                success: function () {
                    $("#form-modal").modal('toggle');
                    location.reload(true);
                }

            });

    });
});