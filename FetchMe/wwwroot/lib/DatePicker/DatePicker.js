//Date Picker
$(function () {
    $('.date').each(function () {
        $(this).datetimepicker({
            defaultDate: $(this).attr("data-defaultDate") != undefined ? $(this).attr("data-defaultDate") : "",
            format: 'L'
        });
    });
    
});
/*
$(function () {
    $('.datepicker').each(function () {

        this.type = 'text';
        var el = $($(this).data("target"));
        
        if ($(this).data('multiple') === true) {
            console.log('multiple');
            
            el.datetimepicker({
                useCurrent: false,
                allowMultidate: true,
                multidateSeparator: ',',
                format: 'L'
            });

        }
        else {
            el.datetimepicker({
                viewMode: 'years',
                useCurrent: false,
                format: 'L'
            });
        }
    });
});

//Time Picker
$(function () {
    $('.timepicker').each(function () {

        this.type = 'text';

        $(this).datetimepicker({
            useCurrent: false,
            format: 'LT'
        });
    });
});


//Date Range
$(function () {

    $('.date-range-a').each(function () {
        //get end range element
        var startEl = this;
        var endEl = $('#' + $(this).data('end-date'));

        startEl.type = 'text';
        endEl.type = 'text';

        $(startEl).datetimepicker({
            format: 'L',
            useCurrent: false
        });

        $(endEl).datetimepicker({
            format: 'L',
            useCurrent: false
        });

        $(startEl).on("change.datetimepicker", function (e) {
            $(endEl).datetimepicker('minDate', e.date);
        });

        $(endEl).on("change.datetimepicker", function (e) {
            $(startEl).datetimepicker('maxDate', e.date);
        });

    });
    
});
*/