jQuery.fn.extend({
    FetchMeGallery: function (options) {
        
        var grid = this;
        grid.id = this.attr("id");
        grid.options = options;
        
        //create grid search
        
        setGridSearch(grid, options);
        
        //create grid
        grid.items = [];

        createGrid(grid);

        setGridEvents(grid);

        //create grid
        if (options.hasModal) {
            grid.modal = {
                id: grid.attr("id") + "-modal"
            };
            createGridModal(grid);
            setGridModalEvents(grid);
        }

        (grid.options.hasData) ? gridItemsChain(grid, grid.options.data) : getGridItems(grid);

    }
});

async function getGridItems(grid) {
    if (!grid.search.InProgress && grid.search.GetMore) {
        $('#' + grid.id + '-loader > i').removeClass('d-none');
        grid.search.InProgress == true;

        const result = await $.ajax({
            type: grid.options.itemLoading.method,
            url: grid.options.itemLoading.url,
            data: grid.search,
            success: function (data) {
                gridItemsChain(grid, data);
            }
        });
        $('#' + grid.id + '-loader > i').addClass('d-none');
    }

}

async function gridItemsChain(grid, data) {
    if (data.length != 0) {
        grid.items = grid.items.concat(data);
        switch (grid.options.search.pagingType) {
            case "dateRange":
                updateItemRange(grid, data);
                break;
            case "excludeList":
                appendItemExlusionList(grid, data);
                break;
            default:
                updateItemRange(grid, data);
        }
        
        setGetMoreBool(grid, data);

        appendGrid(data, grid);
        
        if (grid.options.hasModal) createModalCall(data, grid);

        setGridItemEvents(data);

        if (grid.options.scrollDirection != "horizontal" && grid.options.gridType === "Justified") grid.justifiedGallery("norewind");
        
        grid.search.InProgress == false;

    } else {
        grid.search.GetMore = false;
    }
}

function setHorizontalScroll(grid) {
    grid.lastScrollLeft = 0;
    grid.scroll(function () {
        $.data(this, 'scrollTimer', setTimeout(function () {
            if (grid.search.GetMore && !grid.search.InProgress && !grid.inScroll) {

                    clearTimeout($.data(this, 'scrollTimer'));

                    var newScrollLeft = grid.scrollLeft(),
                        width = grid.width(),
                        scrollWidth = grid.get(0).scrollWidth,
                        offset = 250;

                    if (scrollWidth - newScrollLeft - width < offset && grid.lastScrollLeft < newScrollLeft) {
                        grid.lastScrollLeft = newScrollLeft;
                        grid.inScroll = true;
                        getGridItems(grid).then(function () {
                            grid.inScroll = false;
                        });
                    }
            }

        }, 50));
        

    });
}

function setVerticalScroll(grid) {
    var scrollEl = grid.closest(".scroll");
    grid.lastScrollbottom = 0;
    var threashold = 350;

    scrollEl.scroll(function () {
        if (grid.search.GetMore && !grid.search.InProgress) {
            
            var currentBottom = $(this).scrollTop() + $(this).innerHeight() + threashold;
            var elBottom = $(this)[0].scrollHeight;
            
            clearTimeout($.data(this, 'scrollTimer'));

            $.data(this, 'scrollTimer', setTimeout(function () {
                
                if (currentBottom >= elBottom) {
                    
                    grid.lastScrollbottom = $(this).scrollTop() + $(this).innerHeight()
                    grid.inScroll = true;
                    getGridItems(grid).then(function () {
                        grid.inScroll = false;
                    });
                }
            }, 50));

        }
    });
}

function setWindowScroll(grid) {
    setVerticalScroll(grid);
}

function setGridEvents(grid) {
    switch (grid.options.scrollDirection) {
        case "horizontal":
            setHorizontalScroll(grid);
            break;
        case "vertical":
            setVerticalScroll(grid);
            break;
        default:
            setWindowScroll(grid);
    }
   
}

function appendGrid(data, grid) {
    
    $.each(data, function (index, obj) {
        
        if (grid.options.hasModal) {
            obj.modal = {
                id: grid.modal.id
            }
        }
        
        var html = Mustache.render($(grid.options.itemLoading.templateId).html(), obj);
        
        grid.append(html);

        if (grid.options.hasSubGrid) {
            //Create the Search
            var data = obj.photographs;

            var search = {
                //distance: $('#distance').val(),
                photographerSlug: obj.slug,
                photographTagIds: grid.search.PhotographTagIds
            };
            $("#" + obj.slug + "-image-grid").FetchMeGallery({
                scrollDirection: "horizontal",
                hasModal: true,
                modal: {
                    templateId: "#photograph-index-modal",
                    modalItemTemplateId: "#photograph-index-modal-item"
                },
                itemLoading: {
                    method: 'POST',
                    url: '/Photographs/PhotographSearch',
                    templateId: '#photograph-index-item'
                },
                search: search,
                hasData: true,
                data: data
            });
        }
        
    });
}

function setGridItemEvents() {
    setToolTip();
}

function setToolTip() {
    $('[data-toggle="tooltip"]').tooltip();
}

function createGrid(grid) {
    if (grid.options.gridType === "Justified") {
        var windowWidth = window.innerWidth;
        var rowHeight = (function () {
            switch (true) {
                case windowWidth < 425:
                    return '100px';
                case windowWidth > 425 && windowWidth < 650:
                    return '125px';
                default:
                    return '150px';
            }
        })('150px');

        grid.justifiedGallery({
            rowHeight: rowHeight,
            lastRow: 'nojustify',
            margins: 10
        }).on('jg.complete', function (e) {
        }).on('jg.resize', function (e) {
        }).on('jg.rowflush', function (e) {
        });
    }
}

function updateItemRange(grid, data) {
    updateFirstItem(grid, data);
    updateLastItem(grid, data);
}

function updateFirstItem(grid, data) {
    if (grid.search.CurrentFirstItem === null ||
        (data[0].id !== grid.search.CurrentFirstItem && data[0].created >= grid.search.CurrentFirstItemCreated)) {

        grid.search.CurrentFirstItem = data[0].id;
        grid.search.CurrentFirstItemCreated = data[0].created;

    }
}

function updateLastItem(grid, data) {
    if (grid.search.CurrentLastItem === null ||
        (data[data.length - 1].id !== grid.search.CurrentLastItem && data[data.length - 1].created <= grid.search.CurrentLastItemCreated)) {

        grid.search.CurrentLastItem = data[data.length - 1].id;
        grid.search.CurrentLastItemCreated = data[data.length - 1].created;

    }
}

function appendItemExlusionList(grid, data) {
    $.each(data, function (index, obj) {
        grid.search.exlusionList.push(obj.id)
    });
}

function setGetMoreBool(grid, data) {

    if (data.length < grid.search.PageSize) {
        grid.search.GetMore = false;
    } else {
        grid.search.GetMore = true;
    }

}

function setGridSearchFormEvents(grid) {
    var form = $("#" + grid.id + "-search");
    setSelect2();

    setModalZIndexEvents("#" + grid.id + '-search-modal');
    
    setGridSearchFormMapsAutocomplete(form);

    form.submit(function (event) {
        event.preventDefault(); 
        
        //close modal
        $("#" + grid.id + "-search-modal").modal('hide');

        //disable button
        form.find('button[type="submit"]').attr("disabled", "disabled");
        form.find('button[type="submit"] > i[data-icon="search"]').addClass("d-none");
        form.find('button[type="submit"] > i[data-icon="loading"]').removeClass("d-none");

        //set the grid search
        grid.search = formToSearch(grid.options.search, form);
        resetGridSearchGlobals(grid);
        

        //clear the grid
        clearGridItems(grid);

        //clear the modal
        $("#" + grid.id + "-modal-carousel-inner").html("");

        //create new grid
        getGridItems(grid);
        if (grid.options.scrollDirection != "horizontal" && grid.options.gridType === "Justified") createGrid(grid); //grid.justifiedGallery("norewind");


        //set the url history
        if(grid.options.form.pushToHistory) setURLHistory(form);

        //reset the buttons
        form.find('button[type="submit"]').removeAttr("disabled");
        form.find('button[type="submit"] > i[data-icon="loading"]').addClass("d-none");
        form.find('button[type="submit"] > i[data-icon="search"]').removeClass("d-none");

    });
}

function setURLHistory(form) {
    $('input[name = "AntiforgeryFieldname"]').addClass('not-included');
    var testLoc = form.find('input[name="Location.Lat"]').val();
    if (testLoc === undefined) {
        $('select[name = "Distance"]').addClass('not-included');
    }
    else {
        $('input[name = "Distance"]').removeClass('not-included');
    }
    history.pushState(null, null, '?' + $.param(form.find(':input, select').not('.not-included').serializeArray().filter(function (k) {
        return $.trim(k.value) !== "";
    })));
}

function clearGridItems(grid) {
    grid.empty()
    grid.html("");
}

function setGridSearch(grid, options) {
    //create the form
    if (options.hasForm) {
        if (grid.options.form.templateId !== null && grid.options.form.templateId !== undefined) {
            if (grid.options.form?.formData?.location) {
                switch (grid.options.form.formData.distance) {
                    case 10:
                        grid.options.form.formData.distance10 = true;
                        break;
                    case 25:
                        grid.options.form.formData.distance25 = true;
                        break;
                    case 50:
                        grid.options.form.formData.distance50 = true;
                        break;
                    case 100:
                        grid.options.form.formData.distance100 = true;
                        break;
                    case 250:
                        grid.options.form.formData.distance250 = true;
                        break;
                    default:
                    // code block
                }
                
            }
            var html = Mustache.render($(grid.options.form.templateId).html(), grid);
            //if formContainer is not defined then set it to the "grid.id-search-form"
            
            if (grid.options.form.formContainer === undefined) {
                //grid.options.form.formContainer = "#" + grid.id + "-search-form" 
                grid.options.form.formContainer = "body";
            }

            $(grid.options.form.formContainer).append(html);
            setGridSearchFormEvents(grid);
            
        }
        
    }
    
    //If it has a search set the search to that search
    //If it has a form, set the form values to the search values

    //on submit - set each property in grid.search = form.input.value

    var form = '';

    if (options.hasForm) {
        form = $("#" + grid.id + "-search");
        filterModal = $(options.form.formModal);
    }
    if (grid.options.search == null || grid.options.search == undefined) {
        grid.options.search = {};
    }

    //var search = (options.hasForm) ? formToSearch(grid.options.search, form) : {};

    grid.search = formToSearch(grid.options.search, form);
    
    resetGridSearchGlobals(grid);
}

function resetGridSearchGlobals(grid) {
    grid.search.GetMore = true;
    grid.search.exlusionList = [];
    grid.search.CurrentFirstItem = null;
    grid.search.CurrentLastItem = null;
    grid.search.CurrentFirstItemCreated = null;
    grid.search.CurrentLastItemCreated = null;
}

function formToSearch(search, form) {
    
    var windowWidth = window.innerWidth;
    var defaultPageSize = (function () {
        switch (true) {
            case windowWidth < 450:
                return 25;
            case windowWidth > 450:
                return 25;
            default:
                return 25;
        }
    })(25);


    var search = {
        //form: form,
        //__RequestVerificationToken: form.find('input[name="__RequestVerificationToken"]').val(),
        PageSize: defaultPageSize,
        //photographContainer: photographContainer,
        Distance: (form) ? form.find('.Distance').val() : search.distance || null,
        IsInsured: (form) ? form.find('.IsInsured').is(':checked') : search.isInsured || null,
        PhotographerSlug: (form) ? form.find('.PhotographerSlug').val() : search.photographerSlug || null,
        PhotographerName: (form) ? form.find('.PhotographerName').val() : search.photographerName || [],
        YearsExperience: (form) ? form.find('.YearsExperience').val() : search.yearsExperience || null,
        ShowFavorites: (form) ? form.find('.ShowFavorites').is(':checked') : search.showFavorites || null,
        PhotographTagIds: (form) ? form.find('.PhotographTagIds').val() : search.photographTagIds || [],
        CameraBody: (form) ? form.find('.CameraBody').val() : search.cameraBody || [],
        ExposureTime: (form) ? form.find('.ExposureTime').val() : search.exposureTime || [],
        FStop: (form) ? form.find('.FStop').val() : search.fStop || [],
        ISO: (form) ? form.find('.ISO').val() : search.iSO || [],
        LightSource: (form) ? form.find('.LightSource').val() : search.lightSource || [],
        Lens: (form) ? form.find('.Lens').val() : search.lens || [],
        CurrentFirstItem: search.currentFirstItem || null,
        CurrentFirstItem: search.currentFirstItem || null,
        CurrentFirstItemCreated: search.currentFirstItemCreated || null,
        CurrentLastItem: search.currentLastItem || null,
        CurrentLastItemCreated: search.currentLastItemCreated || null,
        GetMore: true,
        InProgress: false,
        ExlusionList: search.exlusionList || [],
        GalleryId: (form) ? form.find('.GalleryId').val() : search.galleryId || null,

    };

    if (form) {
        var locationSelected = form.find('.lat').val();
        
        if (locationSelected !== undefined && locationSelected !== "") {
            search.Location = {
                FormattedAddress: (form) ? form.find('.FormattedAddress').val() : search.formattedAddress || null,
                Lat: (form) ? form.find('.lat').val() : search.lat || null,
                Lng: (form) ? form.find('.lng').val() : search.lng || null
            };
            search.Distance = (form) ? form.find('.Distance').val() : search.distance || 25;
        }
        else {
            search.Distance = null;
        }
    }
    return search;

}

/*
 *
 * Grid Modal
 *
 */
function createGridModal(grid) {

    var obj = {
        modal: {
            id: grid.modal.id
        }
    }
    var html = Mustache.render($(grid.options.modal.templateId).html(), obj);

    $(document.body).append(html);

    setGridEvents(grid);
}

function createModalCall(data, grid) {
    if (grid.options.modal.hasCall) {
        $.each(data, function (index, obj) {
            //if Modal triggers call on open
        
                var callContainer = $("#" + grid.modal.id + "-" + obj.id + "-images");

            //If call has search set it up
            if (grid.options.modal.call.hasSearch) {
                grid.options.modal.call.search.photographerSlug = obj.slug;
            }
            //If form has data set it up
            //If call has search set it up
            if (grid.options.modal.call.hasData) {
                grid.options.modal.call.data = obj.photographs;
            }

                callContainer.FetchMeGallery(grid.options.modal.call);
       

        });
    }
}

function setGridModalEvents(grid) {
    modalOnShow($('#' + grid.modal.id), grid);
    modalOnHide($('#' + grid.modal.id));
}

function modalOnShow(modal, grid) {
    modal.on('show.bs.modal', function (e) {
        var trigger = $(e.relatedTarget);
        var imageId = trigger.data('carousel-id');

        var gridItem = grid.items.find(x => x.id === imageId);
        
        var modalId = trigger.data("target");

        var html = Mustache.render($("#photograph-index-modal-item").html(), gridItem);

        $(modalId + '-carousel-inner').html(html);

        attachOnImageSlideEvents(grid, imageId, modalId);

        $('#lightbox-like-tooltip').tooltip();
        $('.likable').click(function (e) {
            var isLiked = $(this).find('i').hasClass("fas")
            
            if (isLiked) {
                gridItem.isLikedByUser = false;
            } else {
                gridItem.isLikedByUser = true;
            }
        });
        setModalIndex(e);
        
    });

    modal.on('shown.bs.modal', function () {
        modal.find(".justified-gallery").justifiedGallery();
    })
}

function attachOnImageSlideEvents(grid, imageId, modalId) {
    setToolTip();
    var myElement = document.getElementById('image-carousel-content');
    
    var currentIndex = grid.items.map(function (e) { return e.id; }).indexOf(imageId);

    var hammertime = new Hammer(myElement);
    hammertime.get('swipe').set({
        direction: Hammer.DIRECTION_ALL,
        threshold: 1,
        velocity: 0.1
    });

    hammertime.on('swiperight', function (ev) {
        if (currentIndex != 0) {
            
            var newItem = grid.items[currentIndex - 1];
            var html = Mustache.render($("#photograph-index-modal-item").html(), newItem);
            $(modalId + '-footer').addClass('d-none');
            $(modalId + '-carousel-inner').addClass('spin').addClass('right');
            

            ChangeModalContent(grid, newItem.id, modalId, html);


            removeSwipeClasses(modalId);
            
        }
                
    });

    hammertime.on('swipeleft', function (ev) {
        
        var gridlength = grid.items.length - 1;

        if (currentIndex == gridlength) {
            getGridItems(grid).then(function () {
                gridlength = grid.items.length - 1;
                swipeLeft(grid, currentIndex, modalId, gridlength);
            });
        } else {
            swipeLeft(grid, currentIndex, modalId, gridlength);
        }
    });
}

function ChangeModalContent(grid, newItemId, modalId, html) {
    setTimeout(function () {
        $(modalId + '-carousel-inner').html(html);
        attachOnImageSlideEvents(grid, newItemId, modalId);
    }, 100);
}

function removeSwipeClasses(modalId) {
    setTimeout(function () {
        $(modalId + '-carousel-inner').removeClass('spin').removeClass('right').removeClass('left');
    }, 250);
}

function swipeLeft(grid, currentIndex, modalId, gridlength) {
    if (currentIndex < gridlength) {
        var newItem = grid.items[currentIndex + 1];
        var html = Mustache.render($("#photograph-index-modal-item").html(), newItem);
        $(modalId + '-footer').addClass('d-none');
        $(modalId + '-carousel-inner').addClass('spin').addClass('left');

        ChangeModalContent(grid, newItem.id, modalId, html);

        removeSwipeClasses(modalId);
    }
}

function modalOnHide(modal) {
    $('.modal').on('hide.bs.modal', function (e) {

        if ($(e.target).hasClass('z-1')) {
            $('body').css('overflow', 'auto');
        }

        removeModalIndex(e);
    });
}
