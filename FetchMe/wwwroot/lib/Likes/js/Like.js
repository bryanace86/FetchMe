class PictaLike {

    //Constructor
    constructor(options) {
        //Properties
        options = options || {};
        this.likedId = options.likedId || null;
        this.isLiked = options.isLiked || null;
        this.type = options.type || null;
        this.class = "picta-like " + options.class || "picta-like";

        
    }

    createHTML() {
        var notLoggedInAttr = "";
        var isLikedClass = 'far';
        var likebtn;
        var likable = " ";
        var likeElements;

        //console.log(this);

        if (UserCreds.isLoggedIn) {
            likable = ' likable ';
            if (this.isLiked) {
                isLikedClass = 'fas';
                //like = 'add-like';
                likeElements = 'data-imageliked="' + this.isLiked + '"';
            }
        } else {
            notLoggedInAttr = 'data-toggle="tooltip" data-placement="left" title="Log in to show some love." data-original-title="Log in to show some love."';
        }

        likebtn = '<i class="gallery-link ' + isLikedClass + ' fa-heart ' + isLikedClass + likable + this.class + '" ' + notLoggedInAttr + ' data-like-id="' + this.likedId + '" ' + likeElements + ' data-type="' + this.type +'"></i>';

        return likebtn;
    }

    //
    //Methods
    //
        
}

$(document).ready(function () {
    $('body').on('click', '.likable', function () {

        var el = $(this);
        if (el.find("i").hasClass('far fa-heart')) {
            addLike(el);
        }
        else {
            removeLike(el)
        }

    });

});

function addLike(el) {

    el.prop("disabled", true);
    el.find("i").removeClass("far fa-heart").addClass("fas fa-spinner fa-spin");
    var url = "/Likes/PostLike";
    var type = el.data("type");
    var id = el.data("like-id");
    //var method = "PostLike";

    var data = {
        Id: id,
        Type: type
    };
    var likedId = id;

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        statusCode: {
            200: function (response) {
                $('a[data-like-id="' + likedId + '"]')
                    .prop("disabled", false)
                    .find("i")
                    .removeClass('fas fa-spinner fa-spin')
                    .addClass('fas fa-heart');
            },
            500: function (response) {
                //console.log(response);
            }
        }
    });
}

function removeLike(el) {
    el.prop("disabled", true);

    el.find("i").removeClass("fas fa-heart").addClass("fas fa-spinner fa-spin");

    var url = "/Likes/DeleteLike";
    var type = el.data("type");
    var id = el.data("like-id");
    //var method = "PostLike";

    var data = {
        Id: id,
        Type: type
    };

    var likedId = id;
    
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        statusCode: {
            200: function (response) {
                $('a[data-like-id="' + likedId + '"]')
                    .prop("disabled", false)
                    .find("i")
                    .removeClass('fas fa-spinner fa-spin')
                    .addClass('far fa-heart');
            },
            500: function (response) {
                //console.log(response);
            }
        }
    });
}

function getUrl(type,action) {
    switch (true) {
        case type === "Photographer":
            return "/PhotographLikes/PostPhotographLike/"
            return
            // code block
            break;
        case type === "Photograph":
            if (action === "create") {
                return "/PhotographLikes/PostPhotographLike/"
            }
            else {
                return "/PhotographLikes/DeletePhotographLike/"
            }
            break;
        default:
        // code block
    }
    
}

