//Open Add To Image Gallery
function openAddToImageGallery(el) {
    var imageId = $(el).attr('image-id');
    if ($("#addToGalleryImageId").val() !== imageId) {
        $("#addToGalleryImageId").val(imageId);
        $("#imageAvailableGalleries").html("");
        $("#getImageGallerieSpinner").removeClass("d-none");
        $.ajax({
            type: "GET",
            url: "/Galleries/GetAddToGalleryForm/" + imageId,
            success: function (response) {
                $("#getImageGallerieSpinner").addClass("d-none");
                $("#imageAvailableGalleries").append(response);
                $('i[data-toggle="tooltip"]').tooltip();
            },
            failure: function (response) {
                $("#getImageGallerieSpinner").addClass("d-none");
                alert("There was an issue creating your gallery, please try again.");
            }
        });
    }
}

function openCreateImageGallery(el) {
    $("#ImageGalleryCreateImageId").val($("#addToGalleryImageId").val());
    $("#createImageGallerySpinner").removeClass("d-none");

    $.ajax({
        type: "GET",
        url: "/Galleries/GetCreateGalleryForm/" + $("#addToGalleryImageId").val(),
        success: function (response) {
            $("#createImageGallerySpinner").addClass("d-none");
            $("#createGalleryFormBody").html(response);
            GalleryCreateSubmitListener();
            $('i[data-toggle="tooltip"]').tooltip();
        },
        failure: function (response) {
            $("#createImageGallerySpinner").addClass("d-none");
            alert("There was an generating the form to create a gallery, please try again.");
        }
    });
}

//add to gallery - for when a user clicks on the add to gallery button on an image
function ImageInGalleryToggle(element) {
    //var el = $(element).parent();
    var el = $(element);
    //var parentId = el.parent().attr("id");
    var imageId = $("#addToGalleryImageId").val();
    var galleryId = el.attr("gallery-id");
    var antiForge = $('input[name="AntiforgeryFieldname"]').val();

    var parent = el.parent();
    parent.hide();

    console.log(el.hasClass("in-gallery"));

    if (el.hasClass("in-gallery")) {
        RemoveImageFromGallery(el, imageId, galleryId, antiForge);
    }
    else {
        addImageToGallery(el, imageId, galleryId, antiForge);
    }
}

function addImageToGallery(el, imageId, galleryId, antiForge) {
    var parent = el.parent();
    //parent.hide('normal');

    var form = $(this);
    var url = "/api/GalleriesAPI/AddImageToGallery";
    var method = "Post";
    var data = { "galleryId": galleryId, "photographId": imageId, "AntiforgeryFieldname": antiForge };

    $.ajax({
        type: method,
        url: url,
        data: data,
        success: function (response) {
            parent.detach().prependTo($("#galleriesImageIsIn"));
            el.removeClass("btn-success").addClass("btn-danger").addClass("in-gallery").html("Remove");
            parent.show('normal');
        },
        failure: function (response) {
            parent.show('normal');
            alert("There was an issue creating your gallery, please try again.");
        }
    });

    //el.removeAttr("disabled");
}

function RemoveImageFromGallery(el, imageId, galleryId, antiForge) {
    var parent = el.parent();
    //parent.hide('normal');

    var form = $(this);
    var url = "/api/GalleriesAPI/RemoveImageFromGallery";
    var method = "Post";
    var data = { "galleryId": galleryId, "photographId": imageId, "AntiforgeryFieldname": antiForge };

    $.ajax({
        type: method,
        url: url,
        data: data,
        success: function (response) {
            parent.detach().prependTo($("#galleriesImageNotIn"));
            el.removeClass("btn-danger").addClass("btn-success").removeClass("in-gallery").html("Add");
            parent.show('normal');
        },
        failure: function (response) {
            parent.show('normal');
            alert("There was an issue creating your gallery, please try again.");
        }
    });
}

function GalleryCreateSubmitListener() {
    $("#createGalleryForm").on("submit", function (e) {
        e.preventDefault();

        var form = $(this);
        var url = form.attr("action");
        var method = form.attr("method");
        var data = form.serializeArray();

        $.ajax({
            type: method,
            url: url,
            data: data,
            success: function (response) {
                $("#create-new-gallery-modal").modal('toggle');
                $(response).hide()
                    .prependTo($("#galleriesImageIsIn"))
                    .show('normal');
            },
            failure: function (response) {
                alert("There was an issue creating your gallery, please try again.");
            }
        });

    });
}