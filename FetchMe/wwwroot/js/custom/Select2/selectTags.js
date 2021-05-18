$(document).ready(function () {
    setSelect2();
});

function setSelect2() {
    $(".js-tags").each(function () {
        $(this).select2({
            tags: true,
            allowClear: true,
            tokenSeparators: [','],
            width: '100%',
            ajax: {
                url: '/api/Tags/' + $(this).data("get-data"),
                dataType: 'json'
            },
            minimumInputLength: 1
        });


    });
}