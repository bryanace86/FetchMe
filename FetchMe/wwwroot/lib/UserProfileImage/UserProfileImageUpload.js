$(document).ready(function () {

    window.URL = window.URL || window.webkitURL;

    $(".custom-file-input").on("change", function (event) {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

    $("#InputImageFile").on("change", function (event) {
        var file = event.target.files[0];
        //ImportFileandPreview(file);
        FileResize(file, 300, Number.MAX_VALUE, $("#imgBase64"), $("#imgPreview"), $("#userProfileImageForm"));
    });

    $("#userProfileImageForm").on("submit", function (e) {
        event.preventDefault();
        var form = $(e.target);
        var url = form.attr("action");
        var data = form.serialize();
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            success: function (response) {
                console.log(response);
                $(".user-profile-image").each(function () {
                    console.log($(this));
                    var baseUrl = $(this).attr("base-url");
                    console.log(response.imageUrl);
                    $(this).attr('src', baseUrl + response.imageUrl);
                    $("#profile-image-update-modal").modal('hide');
                });
            },
            failure: function (response) {
                console.log(response);
            }
        });

    });

});

async function FileResize(file, maxHeigt, maxWidth, inputEl, previewSrc, form) {

    var picReader = new FileReader();
    picReader.addEventListener("load", async function (event) {

        var picFile = event.target;
        var imageData = picFile.result;
        var img = new Image();
        img.src = imageData;
        img.onload = function () {

            if (img.width > maxWidth || img.height > maxHeigt) {

                if (img.width > maxWidth) {
                    width = maxWidth;
                    var ration = maxWidth / img.width;
                    height = Math.round(img.height * ration);
                }

                if (img.height > maxHeigt) {
                    height = maxHeigt;
                    var ration = maxHeigt / img.height;
                    width = Math.round(img.width * ration);
                }
            }

            var canvas = $("<canvas/>").get(0);
            canvas.width = width;
            canvas.height = height;
            var context = canvas.getContext('2d');
            context.drawImage(img, 0, 0, width, height);

            var quality = 1;

            imageData = canvas.toDataURL("image/jpeg", quality);

            file.imageDataType = "data:image/jpeg;base64,"
            file.imageData = imageData.replace("data:image/jpeg;base64,", "");

            inputEl.val(file.imageData);
            previewSrc.attr('src', file.imageDataType + file.imageData);
        }

    });
    //Read the image
    var dataUrl = picReader.readAsDataURL(file);
}
