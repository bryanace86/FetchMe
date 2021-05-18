$(document).ready(function () {
    $('.modal').each(function () {
        setModalZIndexEvents("#"+$(this).attr("id"));
    });

});

function setModalZIndexEvents(modalId) {
    console.log('setModalZIndexEvents');
    $(modalId).on('show.bs.modal', function (e) {
        setModalIndex(e);
    });

    $(modalId).on('hide.bs.modal', function (e) {
        removeModalIndex(e);
    });
}

function setModalIndex(e) {
    var index_highest = 1050;
    $(".modal.show").each(function () {
        // always use a radix when using parseInt
        var index_current = parseInt($(this).css("zIndex"), 10);

        if (index_current > index_highest) {
            index_highest = index_current;
        }

    });

    switch (index_highest) {
        case 1050:
            $(e.currentTarget).addClass("z-1");
            $(".modal-backdrop.show").addClass("z-1");
            break;
        case 1051:
            $(e.currentTarget).addClass("z-2");
            $(".modal-backdrop.show").addClass("z-2");
            break;
        case 1053:
            $(e.currentTarget).addClass("z-3");
            $(".modal-backdrop.show").addClass("z-3");
            break;
        case 1055:
            $(e.currentTarget).addClass("z-4");
            $(".modal-backdrop.show").addClass("z-4");
            break;
    }
    $('body').css('overflow', 'hidden');
}

function removeModalIndex(e) {

    if ($(e.target).hasClass('z-1')) {
        $('body').css('overflow', 'auto');
    }

    $(".modal-backdrop.show").removeClass("z-1 z-2 z-3 z-4");
}