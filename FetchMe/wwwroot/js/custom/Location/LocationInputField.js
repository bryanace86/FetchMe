async function setGridSearchFormMapsAutocomplete(form) {

    if (form.find('.location-autocomplete').length) {

        var autocomplete = $(form.find('.location-autocomplete').get(0));

        autocomplete.on('change cut', function () {

            form.find(".location_id").val('');
            form.find(".administrative_area_level_1").val('');
            form.find(".administrative_area_level_2").val('');
            form.find(".administrative_area_level_3").val('');
            form.find(".country").val('');
            form.find(".locality").val('');
            form.find(".neighborhood").val('');
            form.find(".political").val('');
            form.find(".postal_code").val('');
            form.find(".route").val('');
            form.find(".street_number").val('');
            form.find(".lat").val('');
            form.find(".lng").val('');
        });

        /**/
        var input = document.getElementById(autocomplete.attr('id'));;
        //var location = input.value;
        
        var options = {
            //types: ['(cities)']
            //componentRestrictions: { country: "us" }
        }

        if (autocomplete.data("city") == true) {
            options.types = ['(cities)'];
        }
        var autocomplete = await new google.maps.places.Autocomplete(input, options);

        autocomplete.addListener('place_changed', function () {

            var place = autocomplete.getPlace();
            var address_components = place.address_components;
            var components = {};
            jQuery.each(address_components, function (k, v1) { jQuery.each(v1.types, function (k2, v2) { components[v2] = v1.short_name }); });

            city = components.locality;
            state = components.administrative_area_level_1;
            lat = place.geometry.location.lat();
            lng = place.geometry.location.lng();

            form.find(".location_id").val(place.place_id);
            form.find(".administrative_area_level_1").val(components.administrative_area_level_1);
            form.find(".administrative_area_level_2").val(components.administrative_area_level_2);
            form.find(".administrative_area_level_3").val(components.administrative_area_level_3);
            form.find(".country").val(components.country);
            form.find(".locality").val(components.locality);
            form.find(".neighborhood").val(components.neighborhood);
            form.find(".political").val(components.political);
            form.find(".postal_code").val(components.postal_code);
            form.find(".route").val(components.route);
            form.find(".street_number").val(components.street_number);
            form.find(".lat").val(place.geometry.location.lat());
            form.find(".lng").val(place.geometry.location.lng());
        });

        return autocomplete;
    }


}