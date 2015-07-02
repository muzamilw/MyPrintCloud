
// Global Variable
var ist = {
    datePattern: "DD/MM/YY",
    shortDatePattern: "dd-M-yy",
    customShortDatePattern: "dd-mm-yy",
    customDatePattern: "DD-MMM-YYYY",
    timePattern: "HH:mm",
    hourPattern: "HH",
    minutePattern: "mm",
    dateTimePattern: "DD/MM/YY HH:mm",
    dateTimeWithSecondsPattern: "DD/MM/YY HH:mm:ss",
    // UTC Date Format
    utcFormat: "YYYY-MM-DDTHH:mm:ss",
    // For Reporting 
    reportCategoryEnums: {
        CRM: 4,
        Stores: 1,
        Suppliers: 2,
        PurchaseOrders: 5,
        Delivery: 6,
        Orders: 12,
        Estimate: 3,
        Invoice: 13,
        GRN: 15,
        Inventory: 7
    },
    //server exceptions enumeration 
    exceptionType: {
        MPCGeneralException: 'MPCGeneralException',
        UnspecifiedException: 'UnspecifiedException'
    },
    //verify if the string is a valid json
    verifyValidJSON: function (str) {
        try {
            JSON.parse(str);
        } catch (exception) {
            return false;
        }
        return true;
    },
    // Validate Url
    validateUrl: function (field) {
        var regex = /^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/[^\s]*)?$/i;
        return (regex.test(field)) ? true : false;
    },
    // Resource Text
    resourceText: { showing: "Showing ", of: "of", defaultHeaderText: "Confirmation" },
    // SiteUrl
    siteUrl: "",
    // Toaster Error Options
    toastrOptions: {
        tapToDismiss: true,
        extendedTimeOut: 0,
        timeOut: 0 // Set timeOut to 0 to make it sticky
    },
    // Makes comma seperated Number
    addCommasToNumber: function (nStr) {
        nStr += '';
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    },
    numberFormat: "0,0.00",
    ordinalFormat: "0",
    lengthFormat: "0.000",
    // Sections enumeration
    sectionsEnum: [
        { indx: 0, id: 1, name: "Estimates" },
        { indx: 1, id: 2, name: "Estimate Details" },
        { indx: 2, id: 3, name: "Template Library" },
        { indx: 3, id: 4, name: "Job Production" },
        { indx: 4, id: 5, name: "Job Activity List" },
        { indx: 5, id: 6, name: "Add Job Activity" },
        { indx: 6, id: 7, name: "Purchases" },
        { indx: 7, id: 54, name: "Orders" }
    ]
};

// Busy Indicator
var spinnerVisibleCounter = 0;

// Show Busy Indicator
function showProgress() {
    ++spinnerVisibleCounter;
    if (spinnerVisibleCounter > 0) {
        $.blockUI({ message: "" });
        $("div#spinner").fadeIn("fast");
    }
};

// Hide Busy Indicator
function hideProgress() {
    --spinnerVisibleCounter;
    if (spinnerVisibleCounter <= 0) {
        spinnerVisibleCounter = 0;
        var spinner = $("div#spinner");
        spinner.stop();
        spinner.fadeOut("fast");
        $.unblockUI(spinner);

    }
};


//status decoder for parsing the exception type and message
amplify.request.decoders = {
    istStatusDecoder: function (data, status, xhr, success, error) {
        if (status === "success") {
            success(data);
        } else {
            if (status === "fail" || status === "error") {
                var errorObject = {};
                errorObject.errorType = ist.exceptionType.UnspecifiedException;
                if (ist.verifyValidJSON(xhr.responseText)) {
                    errorObject.errorDetail = JSON.parse(xhr.responseText);;
                    if (errorObject.errorDetail.ExceptionType === ist.exceptionType.MPCGeneralException) {
                        error(errorObject.errorDetail.Message, ist.exceptionType.MPCGeneralException);
                    } else {
                        error("Unspecified exception", ist.exceptionType.UnspecifiedException);
                    }
                } else {
                    error(xhr.responseText);
                }
            } else if (status === "nocontent") { // Added by ali : nocontent status is returned when no response is returned but operation is sucessful
                success(data);

            } else {
                error(xhr.responseText);
            }
        }
    }
};

// If while ajax call user shifts to another page then avoid error toasts
amplify.subscribe("request.before.ajax", function (resource, settings, ajaxSettings, ampXhr) {
    var _error = ampXhr.error;

    function error(data, status) {
        _error(data, status);
    }

    ampXhr.error = function (data, status) {
        if (ampXhr.status === 0) {
            return;
        }
        error(data, status);
    };

});

// Knockout Validation + Bindings

var ko = window["ko"];

require(["ko", "knockout-validation"], function (ko) {
    ko.utils.stringStartsWith = function (string, startsWith) {
        string = string || "";
        if (startsWith.length > string.length)
            return false;
        return string.substring(0, startsWith.length) === startsWith;
    };

    ko.bindingHandlers.select2 = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var obj = valueAccessor(),
                allBindings = allBindingsAccessor();
            //var options1 = allBindings.get('select2Options') || {};
            // $(element).select2(allBindings);
            // $(element).select2(obj, allBindings);
            $(element).select2({
                //data: ['a', 'b', 'c'],
                allowClear: true,
                placeholder: "Select flag"
            });
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                //$(element).select2('destroy');
            });
        },
        update: function (element) {
           // $(element).trigger('change');
        }
    };

    ko.bindingHandlers.valueSelect2 = {
        'after': ['options'],
        'init': function (element, valueAccessor, allBindings) {
            // kind of extend value binding
            //ko.bindingHandlers.value.init(element, valueAccessor, allBindings);

            var options1 = allBindings.get('select2Options') || {};
            $(element).select2(options1);

            var value = valueAccessor();
            // init val
            $(element).val(ko.unwrap(value)).trigger("change");

            var changeListener;
            if (ko.isObservable(value)) {
                var shouldIgnore = false;
                changeListener = value.subscribe(function (newVal) {
                    if (!shouldIgnore) {
                        shouldIgnore = true;
                        $(element).val(newVal).trigger("change");
                        shouldIgnore = false;
                    }
                });

                // this demo only works on single select.
                $(element).on("change", function (e) {
                    if (!shouldIgnore) {
                        shouldIgnore = true;
                        if (e.val == '') {
                            // select2 use empty string for unselected value
                            // it could cause problem when you really want '' as a valid option
                            value(undefined);
                        } else {
                            value(e.val);
                        }
                        shouldIgnore = false;
                    }
                });
            }

            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                if (changeListener) changeListener.dispose();
              //  $(element).select2("destory");
            });
        }
    };


    function colorHelper(col) {
        if (col.length === 4) {
            var first = col[1] + col[1];
            var second = col[2] + col[2];
            var third = col[3] + col[3];
            col = "#" + first + second + third;
        }
        return col;
    }
    var defaultoptions;
    ko.bindingHandlers.colorpicker = {
        init: function (element, valueAccessor) {
            //initialize colorpicker with some optional options
            defaultoptions = {
                showInput: true,
                className: "full-spectrum",
                showInitial: true,
                showPalette: true,
                showSelectionPalette: true,
                maxPaletteSize: 10,
                preferredFormat: "hex",
                localStorageKey: "spectrum.demo",
                change: function (color) {
                    $(element).val(color.toHexString());
                },
                palette: [
                    ["rgb(0, 0, 0)", "rgb(67, 67, 67)", "rgb(102, 102, 102)",
                        "rgb(204, 204, 204)", "rgb(217, 217, 217)", "rgb(255, 255, 255)"],
                    ["rgb(152, 0, 0)", "rgb(255, 0, 0)", "rgb(255, 153, 0)", "rgb(255, 255, 0)", "rgb(0, 255, 0)",
                        "rgb(0, 255, 255)", "rgb(74, 134, 232)", "rgb(0, 0, 255)", "rgb(153, 0, 255)", "rgb(255, 0, 255)"],
                    ["rgb(230, 184, 175)", "rgb(244, 204, 204)", "rgb(252, 229, 205)", "rgb(255, 242, 204)", "rgb(217, 234, 211)",
                        "rgb(208, 224, 227)", "rgb(201, 218, 248)", "rgb(207, 226, 243)", "rgb(217, 210, 233)", "rgb(234, 209, 220)",
                        "rgb(221, 126, 107)", "rgb(234, 153, 153)", "rgb(249, 203, 156)", "rgb(255, 229, 153)", "rgb(182, 215, 168)",
                        "rgb(162, 196, 201)", "rgb(164, 194, 244)", "rgb(159, 197, 232)", "rgb(180, 167, 214)", "rgb(213, 166, 189)",
                        "rgb(204, 65, 37)", "rgb(224, 102, 102)", "rgb(246, 178, 107)", "rgb(255, 217, 102)", "rgb(147, 196, 125)",
                        "rgb(118, 165, 175)", "rgb(109, 158, 235)", "rgb(111, 168, 220)", "rgb(142, 124, 195)", "rgb(194, 123, 160)",
                        "rgb(166, 28, 0)", "rgb(204, 0, 0)", "rgb(230, 145, 56)", "rgb(241, 194, 50)", "rgb(106, 168, 79)",
                        "rgb(69, 129, 142)", "rgb(60, 120, 216)", "rgb(61, 133, 198)", "rgb(103, 78, 167)", "rgb(166, 77, 121)",
                        "rgb(91, 15, 0)", "rgb(102, 0, 0)", "rgb(120, 63, 4)", "rgb(127, 96, 0)", "rgb(39, 78, 19)",
                        "rgb(12, 52, 61)", "rgb(28, 69, 135)", "rgb(7, 55, 99)", "rgb(32, 18, 77)", "rgb(76, 17, 48)"]
                ]
            };
            defaultoptions.color = "#FFFFFF";
            $(element).spectrum(defaultoptions);
            $(element).val("#FFF");
            var obser = valueAccessor();
            if (obser() === undefined || obser === null) {
                obser(colorHelper($(element).val()));
            }

            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();

                observable(colorHelper($(element).val()));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).spectrum("destroy");
            });

        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor()),
                current = colorHelper($(element).val());

            if (value !== current) {
                defaultoptions.color = value;
                $(element).spectrum(defaultoptions);
            }
        }
    };

    ko.bindingHandlers.fullCalendar = {
        // This method is called to initialize the node, and will also be called again if you change what the grid is bound to
        update: function (element, viewModelAccessor, allBindingsAccessor) {
            var viewModel = viewModelAccessor();
            element.innerHTML = "";
            $(element).fullCalendar({
                events: ko.utils.unwrapObservable(viewModel.events),
                //events: viewModel.events,
                header: viewModel.header,
                editable: viewModel.editable,
                selectable: true,
                cache: true,
                default: true,
                defaultView: ko.utils.unwrapObservable(viewModel.defaultView),
                eventClick: this.eventClick,
                // eventDrop: this.eventDropOrResize,
                eventDrop: viewModel.eventDropOrResize,
                eventResize: viewModel.eventDropOrResize,
                select: this.newEventAdd,
                viewDisplay: this.viewEventClick,
                //monthClick:this.dayEventClick
                //eventSources:this.dayEventClick
            });


            $(".fc-button-prev").click(function (event) {
                var view = $('#calendar').fullCalendar('getView');
                ist.calendar.viewModel.getActivitiesForNextPreTodayClick(view);
            });
            $(".fc-button-next").click(function (event) {
                var view = $('#calendar').fullCalendar('getView');
                ist.calendar.viewModel.getActivitiesForNextPreTodayClick(view);
            });
            $(".fc-button-today").click(function (event) {
                var view = $('#calendar').fullCalendar('getView');
                ist.calendar.viewModel.getActivitiesForNextPreTodayClick(view);
            });
            $(element).fullCalendar('gotoDate', ko.utils.unwrapObservable(viewModel.viewDate));;
            //});
            //$(element).on('click', '.fc-button-next span', function () {
            //    $(this).unbind('click');
            //});
            //    .on('.fc-button-next span').click(function () {

            //    alert("next");

            //})
            // $(element).fullCalendar('gotoDate', ko.utils.unwrapObservable(viewModel.viewDate));;

        }
    };


    ko.bindingHandlers.editor = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var valueUnwrapped = ko.unwrap(value);
            var allBindings = allBindingsAccessor();
            var $element = $(element);
            var droppable = allBindingsAccessor().drop;
            CKEDITOR.basePath = ist.siteUrl + "/RichTextEditor/";
            var myinstance = CKEDITOR.instances['content'];
            //check if my instance already exist
            if (myinstance !== undefined) {
                CKEDITOR.remove(myinstance);
            }
            if (allBindingsAccessor().openFrom() === "Campaign" || allBindingsAccessor().openFrom() === "SecondaryPage") {
                CKEDITOR.config.toolbar = [
                    ['Source', 'Bold', 'Italic', 'Underline', 'SpellChecker', 'TextColor', 'BGColor', 'Undo', 'Redo', 'Link', 'Unlink', '-', 'Format'],
                    '/', ['NumberedList', 'BulletedList', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'Font', 'FontSize']
                ];
            } else {
                CKEDITOR.config.toolbar = 'Full';
            }

            CKEDITOR.replace(element).setData(valueUnwrapped || $element.html());
            var instance = CKEDITOR.instances['content'];
            //CKEDITOR.instances
            //CKEDITOR.appendTo(element).setData(valueUnwrapped || $element.html());
            if (ko.isObservable(value)) {
                var isSubscriberChange = false;
                var isEditorChange = true;
                $element.html(value());
                visEditorChange = false;
                $.fn.modal.Constructor.prototype.enforceFocus = function () {
                    modal_this = this;
                    $(document).on('focusin.modal', function (e) {
                        if (modal_this.$element[0] !== e.target && !modal_this.$element.has(e.target).length
                            // add whatever conditions you need here:
                            &&
                            !$(e.target.parentNode).hasClass('cke_dialog_ui_input_select') && !$(e.target.parentNode).hasClass('cke_dialog_ui_input_text')) {
                            modal_this.$element.focus();
                        }
                    });
                };
                // Handles typing changes 
                instance.on('contentDom', function () {
                    instance.document.on('keyup', function (event) {
                        if (ist.stores.viewModel.selectedSecondaryPage() !== undefined && ist.stores.viewModel.selectedSecondaryPage() !== null) {
                            ist.stores.viewModel.selectedSecondaryPage().pageHTML(instance.getData());
                        }
                        else if (ist.stores.viewModel.selectedEmail() !== undefined && ist.stores.viewModel.selectedEmail() !== null) {
                            ist.stores.viewModel.selectedEmail().hTMLMessageA(instance.getData());
                        }
                    });
                });

                function handleAfterCommandExec(event) {
                    if (ist.stores.viewModel.selectedSecondaryPage() !== undefined && ist.stores.viewModel.selectedSecondaryPage() !== null) {
                        ist.stores.viewModel.selectedSecondaryPage().pageHTML(instance.getData());
                    }
                    else if (ist.stores.viewModel.selectedEmail() !== undefined && ist.stores.viewModel.selectedEmail() !== null) {
                        ist.stores.viewModel.selectedEmail().hTMLMessageA(instance.getData());
                    }
                }

                // Handles styling changes 
                instance.on('afterCommandExec', handleAfterCommandExec);
                // Handles styling Drop down changes like font size, font family 
                instance.on('selectionChange', handleAfterCommandExec);


                value.subscribe(function (newValue) {
                    if (!isEditorChange) {
                        isSubscriberChange = true;
                        $element.html(newValue);
                        isSubscriberChange = false;
                    }
                });
            }
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            //handle programmatic updates to the observable
            var existingEditor = CKEDITOR.instances && CKEDITOR.instances[element.id];
            if (existingEditor) {
                existingEditor.setData(ko.utils.unwrapObservable(valueAccessor()), function () {
                    this.checkDirty(); // true
                });
            }
        }
    }
    ko.bindingHandlers.drag = {
        init: function (element, valueAccessor, allBindingsAccessor,
                       viewModel, context) {
            var value = valueAccessor();

            if (value(context)) {

                $(element).draggable({
                    containment: 'window',
                    zindex: 1000,
                    distance: 20,
                    helper: function (evt) {
                        var h = $(element).clone().css({
                            width: $(element).width(),
                            height: $(element).height(),
                        });
                        if (evt.ctrlKey) {
                            $(h).css({ cursor: "copy" });
                        }
                        h.data('ko.draggable.data', value(context, evt));
                        return h;
                    },
                    scroll: false,
                    cursorAt: { left: 5, top: 5 },
                    start: function (event, ui) {
                        ui.position.top += $('body').scrollTop();
                    },

                    appendTo: 'body'
                });
            } else {
                $(element).draggable({
                    containment: 'window',
                    zindex: 1000,
                    distance: 20,
                    disabled: true
                });

            }
        }
    };

    ko.bindingHandlers.drop = {
        init: function (element, valueAccessor, allBindingsAccessor,
                       viewModel, context) {
            var value = valueAccessor();
            $(element).droppable({
                tolerance: 'pointer',
                hoverClass: 'dragHover',
                activeClass: 'dragActive',
                drop: function (evt, ui) {
                    value(ui.helper.data('ko.draggable.data'), context, evt);
                }
            });
        }
    };

    // jquery date picker binding. Usage: <input data-bind="datepicker: myDate, datepickerOptions: { minDate: new Date() }" />. Source: http://jsfiddle.net/rniemeyer/NAgNV/
    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            // ReSharper disable DuplicatingLocalDeclaration
            var options = allBindingsAccessor().datepickerOptions || {};
            // ReSharper restore DuplicatingLocalDeclaration
            $(element).datepicker(options);
            $(element).datepicker("option", "dateFormat", ist.shortDatePattern);
            $(element).datepicker("option", "changeMonth", true);
            $(element).datepicker("option", "changeYear", true);
            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();
                observable($(element).datepicker("getDate"));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).datepicker("destroy");
            });

        },
        update: function (element, valueAccessor) {
            var observable = valueAccessor();

            var value = ko.utils.unwrapObservable(valueAccessor()),
                current = $(element).datepicker("getDate");

            if (value - current !== 0) {
                $(element).datepicker("setDate", value);
            }
            //For showing highlighted field if contains invalid value
            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $(element).addClass('errorFill');
                } else {
                    $(element).removeClass('errorFill');
                }
            }
        }
    };

    // jquery date time picker binding. Usage: <input data-bind="datetimepicker: myDate, datepickerOptions: { minDate: new Date() }" />. Source: http://jsfiddle.net/rniemeyer/NAgNV/
    ko.bindingHandlers.datetimepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            // ReSharper disable DuplicatingLocalDeclaration
            var options = allBindingsAccessor().datepickerOptions || {};
            // ReSharper restore DuplicatingLocalDeclaration
            $(element).datetimepicker(options);
            $(element).datetimepicker("option", "dateFormat", ist.shortDatePattern);
            $(element).datepicker("option", "changeMonth", true);
            $(element).datepicker("option", "changeYear", true);
            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();
                observable($(element).datetimepicker("getDate"));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).datetimepicker("destroy");
            });

        },
        update: function (element, valueAccessor) {
            var observable = valueAccessor();

            var value = ko.utils.unwrapObservable(valueAccessor()),
                current = $(element).datetimepicker("getDate");

            if (value - current !== 0) {
                $(element).datetimepicker("setDate", value);
            }
            //For showing highlighted field if contains invalid value
            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $(element).addClass('errorFill');
                } else {
                    $(element).removeClass('errorFill');
                }
            }
        }
    };

    ko.bindingHandlers.validationDependsOn = {
        update: function (element, valueAccessor) {
            var observable = valueAccessor();

            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $(element).addClass('errorFill');
                } else {
                    $(element).removeClass('errorFill');
                }
            }
        }
    };
    //Slider Binding Handler
    ko.bindingHandlers.slider = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var sliderOptions = allBindingsAccessor().sliderOptions || {};
            $(element).slider(sliderOptions);
            ko.utils.registerEventHandler(element, "slidechange", function (event, ui) {
                var observable = valueAccessor();
                observable(ui.value);
            });
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).slider("destroy");
            });
            ko.utils.registerEventHandler(element, "slide", function (event, ui) {
                var observable = valueAccessor();
                observable(ui.value);
            });
        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            if (isNaN(value)) value = 0;
            $(element).slider("value", value);

        }
    };

    ko.bindingHandlers.files = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var allBindings = allBindingsAccessor();
            var loadedCallback, progressCallback, errorCallback, fileFilter, readAs, maxFileSize;
            var filesBinding = allBindings.files;

            if (typeof (ko.unwrap(filesBinding)) == "function")
            { loadedCallback = ko.unwrap(filesBinding); }
            else
            {
                loadedCallback = ko.unwrap(filesBinding.onLoaded);
                progressCallback = ko.unwrap(filesBinding.onProgress);
                errorCallback = ko.unwrap(filesBinding.onError);
                fileFilter = ko.unwrap(filesBinding.fileFilter);
                maxFileSize = ko.unwrap(filesBinding.maxFileSize);
                readAs = ko.unwrap(filesBinding.readAs);
            }

            if (typeof (loadedCallback) != "function")
            { return; }

            var readFile = function (file) {
                var reader = new FileReader();
                reader.onload = function (fileLoadedEvent) {
                    loadedCallback(file, fileLoadedEvent.target.result);
                };

                if (typeof (progressCallback) == "function") {
                    reader.onprogress = function (fileProgressEvent) {
                        progressCallback(file, fileProgressEvent.loaded, fileProgressEvent.total);
                    };
                }

                if (typeof (errorCallback) == "function") {
                    reader.onerror = function (fileErrorEvent) {
                        errorCallback(file, fileErrorEvent.target.error);
                    };
                }

                if (readAs == "text")
                { reader.readAsText(file); }
                else if (readAs == "array")
                { reader.readAsArrayBuffer(file); }
                else if (readAs == "binary")
                { reader.readAsBinaryString(file); }
                else
                { reader.readAsDataURL(file); }
            };

            var handleFileChangedEvent = function (fileChangedEvent) {
                var files = fileChangedEvent.target.files;
                for (var i = 0, f; f = files[i]; i++) {
                    if (typeof (fileFilter) != "undefined" && !f.type.match(fileFilter)) {
                        if (typeof (errorCallback) == "function")
                        { errorCallback(f, "File type does not match filter"); }
                        continue;
                    }

                    if (typeof (maxFileSize) != "undefined" && f.size >= maxFileSize) {
                        if (typeof (errorCallback) == "function")
                        { errorCallback(f, "File exceeds file size limit"); }
                        continue;
                    }

                    readFile(f);
                }
            };

            element.addEventListener('change', handleFileChangedEvent, false);
        }
    };
    // date formatting. Example <div class="date" data-bind="dateString: today, datePattern: 'dddd, MMMM dd, yyyy'">Thursday, April 05, 2012</div>
    ko.bindingHandlers.dateString = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(),
                allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            var pattern = allBindings.datePattern || ist.datePattern;
            if (valueUnwrapped !== undefined && valueUnwrapped !== null) {
                $(element).text(moment(valueUnwrapped).format(ist.customDatePattern));
            }
            else {
                $(element).text("");
            }

        }
    };


    //Custom binding for handling validation messages in tooltip
    ko.bindingHandlers.validationTooltip = {
        update: function (element, valueAccessor) {
            var observable = valueAccessor(), $element = $(element);
            if (observable.isValid) {
                if (!observable.isValid() && observable.isModified()) {
                    $element.tooltip({ title: observable.error }); //, delay: { show: 10000, hide: 10000 }
                } else {
                    $element.tooltip('destroy');
                }
            }
        }
    };

    // Knockout Extender for Element
    ko.extenders.element = function (target, element) {
        target.domElement = element;
    }

    // Custom Binding for handling validation elements
    ko.bindingHandlers.validationOnElement = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            valueAccessor().extend({ element: element });
        }
    };

    //Custom binding for handling validation messages in tooltip
    ko.bindingHandlers.tooltip = {
        update: function (element, valueAccessor) {
            var $element = $(element);
            var value = ko.utils.unwrapObservable(valueAccessor());
            value = value.replace(/:0/g, ':00');
            $element.tooltip({ title: value, html: true }); //, delay: { show: 10000, hide: 10000 }
        }
    };

    // Forcing Input to be Numeric
    ko.extenders.numberInput = function (target) {
        //create a writeable computed observable to intercept writes to our observable
        var result = ko.computed({
            read: function () {
                return target();
            },
            write: function (newValue) {
                var current = target(),
                    valueToWrite = newValue === null || newValue === undefined ? null : numeral().unformat("" + newValue);

                //only write if it changed
                if (valueToWrite !== current) {
                    target(valueToWrite);
                } else {
                    //if the rounded value is the same, but a different value was written, force a notification for the current field
                    if (newValue !== current) {
                        target.notifySubscribers(valueToWrite);
                    }
                }
            }
        });

        //initialize with current value to make sure it is rounded appropriately
        result(target());

        //return the new computed observable
        return result;
    };

    // number formatting setting the text property of an element
    ko.bindingHandlers.numberInput = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(),
                allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            var pattern = allBindings.format || ist.numberFormat;
            if (valueUnwrapped !== undefined && valueUnwrapped !== null) {
                var formattedValue = numeral(valueUnwrapped).format(pattern);
                $(element).text(formattedValue);
            }
            else {
                $(element).text("");
            }

        }
    };
    // number formatting for input fields
    ko.bindingHandlers.numberValue = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var underlyingObservable = valueAccessor(),
                allBindings = allBindingsAccessor(),
                pattern = allBindings.numberFormat || ist.numberFormat;

            var interceptor = ko.computed({
                read: function () {
                    if (underlyingObservable() === null || underlyingObservable() === undefined || underlyingObservable() === "") {
                        return "";
                    }
                    // ReSharper disable InconsistentNaming
                    return new numeral(underlyingObservable()).format(pattern);
                    // ReSharper restore InconsistentNaming
                },

                write: function (newValue) {
                    var current = underlyingObservable(),
                        valueToWrite = newValue === null || newValue === undefined || newValue === "" ? null : numeral().unformat("" + newValue);

                    if (valueToWrite !== current) {
                        underlyingObservable(valueToWrite);
                    } else {
                        if (newValue !== current.toString()) {
                            underlyingObservable('');
                            underlyingObservable(valueToWrite);
                        }
                    }
                }
            });

            ko.applyBindingsToNode(element, { value: interceptor });
        }
    };


    // KO Dirty Flag - Change Tracking
    ko.dirtyFlag = function (root, isInitiallyDirty) {
        var result = function () { },
    // ReSharper disable InconsistentNaming
            _initialState = ko.observable(ko.toJSON(root)),
            _isInitiallyDirty = ko.observable(isInitiallyDirty);
        // ReSharper restore InconsistentNaming

        result.isDirty = ko.dependentObservable(function () {
            return _isInitiallyDirty() || _initialState() !== ko.toJSON(root);
        });

        result.reset = function () {
            _initialState(ko.toJSON(root));
            _isInitiallyDirty(false);
        };

        return result;
    };
    // KO Dirty Flag - Change Tracking

    // Common View Model - Editor (Save, Cancel - Reverts changes, Select Item)
    ist.ViewModel = function (model) {

        //hold the currently selected item
        this.selectedItem = ko.observable();

        // hold the model
        this.model = model;

        //make edits to a copy
        this.itemForEditing = ko.observable();

    };

    ko.utils.extend(ist.ViewModel.prototype, {
        //select an item and make a copy of it for editing
        selectItem: function (item) {
            this.selectedItem(item);
            this.itemForEditing(this.model.CreateFromClientModel(ko.toJS(item)));
        },

        acceptItem: function (data) {

            //apply updates from the edited item to the selected item
            this.selectedItem().update(data);

            //clear selected item
            this.selectedItem(null);
            this.itemForEditing(null);
        },

        //just throw away the edited item and clear the selected observables
        revertItem: function () {
            this.itemForEditing().reset(); // Resets Changed State
            this.selectedItem(null);
            this.itemForEditing(null);
        }
    });

    // Common View Model

    // Used to show popover
    ko.bindingHandlers.bootstrapPopover = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            // ReSharper disable DuplicatingLocalDeclaration
            var options = valueAccessor();
            // ReSharper restore DuplicatingLocalDeclaration
            var node = $("#" + options.elementNode);
            var defaultOptions = { trigger: 'click', content: node.html() };
            options = $.extend(true, {}, defaultOptions, options);
            $(element).popover(options);
            $(element).click(function () {
                var popOver = $("#" + options.popoverId);
                if (popOver) {
                    popOver = popOver[0];
                }
                var childBindingContext = bindingContext.createChildContext(viewModel);
                if (!popOver) {
                    return;
                }
                ko.cleanNode(popOver);
                ko.applyBindingsToDescendants(childBindingContext, popOver);
            });
            $(window).click(function (event) {
                if (event.target != $(element)[0] && event.target.className.search(options.popoverId) < 0) {
                    $(element).popover('hide');
                }
            });
        }
    }
    //Validation Rules
    ko.validation.rules['compareWith'] = {
        getValue: function (o) {
            return (typeof o === 'function' ? o() : o);
        },
        validator: function (val, otherField) {
            return val === this.getValue(otherField);
        },
        message: 'The fields must have the same value'
    };
    //Validation Rules
    ko.validation.rules['variableTagRule'] = {
        validator: function (val) {
            var regExp = new RegExp("^{{[a-zA-Z0-9_]*}}$");
            return regExp.test(val);
        },
        message: 'Tag must start with {{ and end with }}, cannot contain spaces and special characters except "_" '
    };
    // Fix for bootstrap popovers, sometimes they are left in the DOM when they shouldn't be.
    $('body').on('hidden.bs.popover', function () {
        var popovers = $('.popover').not('.in');
        if (popovers) {
            popovers.remove();
        }
    });

    // Can be used to have a parent with one binding and children with another. Child areas should be surrounded with <!-- ko stopBinding: true --> <!-- /ko -->
    ko.bindingHandlers.stopBinding = {
        init: function () {
            return { controlsDescendantBindings: true };
        }
    };

    ko.virtualElements.allowedBindings.stopBinding = true;

    var options = { insertMessages: false, decorateElement: true, errorElementClass: 'errorFill', messagesOnModified: true, registerExtenders: true };
    ko.validation.init(options);

});


// Sorting 
// <Param>tableId - Id of the table like "productTable" </Param>
// <Param>Sort On - Observable, to identify sort column</Param>
// <Param>Sort Asc - Observable, to identify sort Order i.e. Asc, or desc</Param>
// <Param>callback - function, to call for refreshing the list</Param>
function handleSorting(tableId, sortOn, sortAsc, callback) {
    // Make Table Sortable
    $('#' + tableId + ' thead tr th span').bind('click', function (e) {
        if (!e.target || !e.target.id) {
            return;
        }
        var sortBy = e.target.id;
        var targetEl = $(e.target).children("span")[0];
        // Remove other header sorting
        _.each($('#' + tableId + ' thead tr th span'), function (item) {
            if (item.parentElement !== e.target) {
                item.className = '';
            }
        });
        // Sort Column
        if (targetEl) {
            var direction = (targetEl.className === 'icon-up') ? 'icon-up' : (targetEl.className === 'icon-down') ? 'icon-down' : 'icon-down';
            if (direction === 'icon-up') {
                targetEl.className = 'icon-down';
                sortAsc(false);
            } else {
                targetEl.className = 'icon-up';
                sortAsc(true);
            }
            sortOn(sortBy);

            // Refresh Grid
            if (callback && typeof callback === "function") {
                callback();
            }
        }
    });
}

// #region Cost Center Execution

var GlobalQuestionQueueItemsList = null; // question queues of disfferent cost centres will be added to this list 
var idsToValidate = ""; // This variable contain ids of text boxes and validate that each text box must have a correct value
var GlobalInputQueueItemsList = null;
var costCentreQueueItems = null;
var selectedCostCentreCheckBoxElement = null;
var selectedStockOptionItemAddOns = null;
var globalSelectedCostCenter = null;
var globalAfterCostCenterExecution = null;

function getBrowserHeight() {
    var intH = 0;
    var intW = 0;
    if (typeof window.innerWidth == 'number') {
        intH = window.innerHeight;
        intW = window.innerWidth;
    }
    else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        intH = document.documentElement.clientHeight;
        intW = document.documentElement.clientWidth;
    }
    else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        intH = document.body.clientHeight;
        intW = document.body.clientWidth;
    }
    return { width: parseInt(intW), height: parseInt(intH) };
}

function HideMessagePopUp() {
    var parentContainer = $("#productFromRetailStoreModal")[0].style.display === "block" ? "#productFromRetailStoreModal" :
        $("#costCenters")[0].style.display === "block" ? "#costCenters" : "";
    $(parentContainer + " #innerLayer")[0].innerHTML = "";
    $(parentContainer + " #layer")[0].style.display = "none";
    $(parentContainer + " #innerLayer")[0].style.display = "none";

}

// Validate Cost Centre Control
function ValidateCostCentreControl(costCentreId, clonedItemId, currency, itemPrice, costCentreType, taxRate, orderedQty) {

    var arrayOfIds = idsToValidate.split(",");

    var isDisplyEmptyFieldsMesg = 0;

    var isNotValidInput = 0;

    var isFormulaValidationError = 0;
    for (var i = 0; i < arrayOfIds.length; i++) {
        if (arrayOfIds[i].indexOf("formulaMatrixBox") != -1) {

            if ($("#" + arrayOfIds[i]).val() == "") {
                isFormulaValidationError = 1;
                $("#" + arrayOfIds[i]).css("border", "1px solid red");
            } else {
                $("#" + arrayOfIds[i]).css("border", "1px solid #a8a8a8");
            }

        } else {

            if ($("#" + arrayOfIds[i]).val() == undefined) {
                $("#" + arrayOfIds[i]).css("border", "1px solid #a8a8a8");
            } else {
                if ($("#" + arrayOfIds[i]).val() == "") {
                    $("#" + arrayOfIds[i]).css("border", "1px solid red");
                    isDisplyEmptyFieldsMesg = 1;
                } else if (isNaN($("#" + arrayOfIds[i]).val())) {
                    isNotValidInput = 1;
                    $("#" + arrayOfIds[i]).css("border", "1px solid red");
                } else {
                    $("#" + arrayOfIds[i]).css("border", "1px solid #a8a8a8");
                }

            }

        }


    }

    if (isDisplyEmptyFieldsMesg == 1) {
        $("#CCErrorMesgContainer").css("display", "block");
        if (isNotValidInput == 1) {
            $("#CCErrorMesgContainer").html("Please enter numbers only to proceed.");
            if (isFormulaValidationError == 1) {
                var html = $("#CCErrorMesgContainer").text() + "<br/> Please select value formula values also.";
                $("#CCErrorMesgContainer").html(html);
            }
        } else {

            $("#CCErrorMesgContainer").html("Please enter in the hightlighted fields.");
        }
        return;
    } else if (isNotValidInput == 1) {
        $("#CCErrorMesgContainer").css("display", "block");
        $("#CCErrorMesgContainer").html("Please enter numbers only to proceed.");
        if (isFormulaValidationError == 1) {
            var html = $("#CCErrorMesgContainer").text() + "<br/> Please select value formula values also.";
            $("#CCErrorMesgContainer").html(html);
        }
        return;
    } else if (isFormulaValidationError == 1) {
        $("#CCErrorMesgContainer").html("Please select value formula values ");
        return;
    } else {

        $("#btnCostCentreCalculator").prop('disabled', 'disabled');
        $("#imgCCLoader").css("display", "block");
        $("#imgCCLoader").css("float", "right");
        var desriptionOfCostCentre = "";
        $("#CCErrorMesgContainer").css("display", "none");
        // Question Queue object items
        $(".CostCentreAnswersQueue").each(function (i, val) {
            if ($(val).attr('data-id') == undefined) {
                var idofDropDown = $(val).attr('id');
                idofDropDown = "select#" + idofDropDown;
                var idOfQuestion = $(idofDropDown + ' option:selected').attr('data-id');
                $(GlobalQuestionQueueItemsList).each(function (i, QueueItem) {
                    if (QueueItem.ID == idOfQuestion) {

                        QueueItem.Qty1Answer = $(idofDropDown + ' option:selected').val();

                    }
                });

                $(GlobalInputQueueItemsList).each(function (i, QueueItem) {
                    if (QueueItem.ID == idOfQuestion) {

                        QueueItem.Qty1Answer = $(idofDropDown + ' option:selected').val();

                    }
                });
            } else {
                $(GlobalQuestionQueueItemsList).each(function (i, QueueItem) {
                    if (QueueItem.ID == $(val).attr('data-id')) {

                        QueueItem.Qty1Answer = $(val).val();

                    }
                });

                $(GlobalInputQueueItemsList).each(function (i, QueueItem) {
                    if (QueueItem.ID == $(val).attr('data-id')) {

                        QueueItem.Qty1Answer = $(val).val();

                    }
                });
            }
            if (desriptionOfCostCentre == "") {
                desriptionOfCostCentre = $(val).parent().prev().children().text() + ", Answer:" + $(val).val() + ". ";
            } else {
                desriptionOfCostCentre = desriptionOfCostCentre + "  " + $(val).parent().prev().children().text() + ", Answer:" + $(val).val() + ". ";
            }
        });

        SetGlobalCostCentreQueue(GlobalQuestionQueueItemsList, GlobalInputQueueItemsList, costCentreId, costCentreType, clonedItemId,
            selectedCostCentreCheckBoxElement, desriptionOfCostCentre, itemPrice, currency, true, taxRate, orderedQty, selectedStockOptionItemAddOns,
            globalSelectedCostCenter, null, true);

        idsToValidate = "";
    }
}

// Show Cost Center Popup
function ShowCostCentrePopup(questionQueueItems, costCentreId, clonedItemId, selectedCostCentreCheckBoxId, mode, currency, itemPrice,
    inputQueueObject, costCentreType, taxRate, workInstructions, orderedQty, itemAddOns, costCenter, afterCostCenterExecution) {

    GlobalQuestionQueueItemsList = questionQueueItems;
    GlobalInputQueueItemsList = inputQueueObject;
    selectedCostCentreCheckBoxElement = selectedCostCentreCheckBoxId;
    selectedStockOptionItemAddOns = itemAddOns;
    globalSelectedCostCenter = costCenter;
    globalAfterCostCenterExecution = afterCostCenterExecution;
    var innerHtml = "";
    var Heading = "Add " + $(selectedCostCentreCheckBoxId).next().html();
    var optionHtml;
    var matrixTable;
    if (mode == "New") { // prompt in case of newly added cost centre
        for (var i = 0; i < questionQueueItems.length; i++) {

            if (questionQueueItems[i].ItemType == 1) { // text box
                if (idsToValidate == "") {
                    idsToValidate = 'txtBox' + questionQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'txtBox' + questionQueueItems[i].ID;
                }

                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + questionQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + questionQueueItems[i].ID + ' data-id=' + questionQueueItems[i].ID + ' value=' + parseFloat(questionQueueItems[i].DefaultAnswer) + ' /></div><br/><div class="clearBoth"></div>';
            }

            if (questionQueueItems[i].ItemType == 3) { // drop down
                optionHtml = "";
                matrixTable = questionQueueItems[i].MatrixTable;
                for (var a = 0; a < questionQueueItems[i].AnswersTable.length; a++) {
                    optionHtml = optionHtml + '<option data-id=' + questionQueueItems[i].ID + ' value=' + questionQueueItems[i].AnswersTable[a].AnswerString + ' >' + questionQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                }
                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + questionQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><select id=dropdown' + questionQueueItems[i].ID + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
                    + optionHtml + '</select></div><br/><div class="clearBoth"></div>';
            }
            if (questionQueueItems[i].ItemType == 2) { // radio


                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + questionQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><input type="radio" checked="checked" name="Group2" id=radioNo' + questionQueueItems[i].ID + ' class="cost-centre-radios CostCentreAnswersQueue" /><label for=radioNo' + questionQueueItems[i].ID + ' >No</label><input type="radio" name="Group2" id=radioYes' + questionQueueItems[i].ID + ' class="cost-centre-radios CostCentreAnswersQueue" /><label for=radioYes' + questionQueueItems[i].ID + ' >Yes</label>' +
                    '</div><br/><div class="clearBoth"></div>';
            }
            if (questionQueueItems[i].ItemType == 4) { // formula matrix

                if (idsToValidate == "") {
                    idsToValidate = 'formulaMatrixBox' + questionQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'formulaMatrixBox' + questionQueueItems[i].ID;
                }

                innerHtml = innerHtml +
                    '<div class="cost-centre-left-container"><label>Super Formula Matrix</label></div>' +
                    '<div class="cost-centre-right-container"><input id=formulaMatrixBox' + questionQueueItems[i].ID + ' type="text" disabled="disabled" ' +
                    'style="float:left; margin-right:10px;"  data-id=' + questionQueueItems[i].ID + ' class="CostCentreAnswersQueue" /> ' +
                    '<input type="button" onclick="ShowFormulaMatrix(' + questionQueueItems[i].RowCount + ',' + questionQueueItems[i].ColumnCount + ',' + i + '); return false;" class="Matrix-select-button rounded_corners5 " value="Select" /></div><div class="clearBoth"></div>';
            }
        }

    } else if (mode == "Modify") { // prompt in case of added cost centre
        //Heading = "Edit " + $("#" + selectedCostCentreCheckBoxId).next().html();
        for (var i = 0; i < questionQueueItems.length; i++) {

            if (questionQueueItems[i].ItemType == 1) { // text box
                if (idsToValidate == "") {
                    idsToValidate = 'txtBox' + questionQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'txtBox' + questionQueueItems[i].ID;
                }

                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + questionQueueItems[i].VisualQuestion + '</label></div><div class="cost-centre-right-container"><input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + questionQueueItems[i].ID + ' data-id=' + questionQueueItems[i].ID + ' value=' + questionQueueItems[i].Qty1Answer + ' /></div><br/><div class="clearBoth"></div>';
            }

            if (questionQueueItems[i].ItemType == 3) { // drop down
                optionHtml = "";
                matrixTable = questionQueueItems[i].MatrixTable;
                for (var a = 0; a < questionQueueItems[i].AnswersTable.length; a++) {
                    if (questionQueueItems[i].AnswersTable[a].AnswerString == questionQueueItems[i].Qty1Answer) {
                        optionHtml = optionHtml + '<option data-id=' + questionQueueItems[i].ID + ' value=' + questionQueueItems[i].AnswersTable[a].AnswerString + ' selected>' + questionQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                    } else {
                        optionHtml = optionHtml + '<option data-id=' + questionQueueItems[i].ID + ' value=' + questionQueueItems[i].AnswersTable[a].AnswerString + ' >' + questionQueueItems[i].AnswersTable[a].AnswerString + '</option>'
                    }

                }
                innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
                    + questionQueueItems[i].VisualQuestion +
                    '</label></div><div class="cost-centre-right-container"><select id=dropdown' + questionQueueItems[i].ID + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
                    + optionHtml + '</select></div><br/><div class="clearBoth"></div>';
            }
            if (questionQueueItems[i].ItemType == 4) { // formula matrix

                if (idsToValidate == "") {
                    idsToValidate = 'formulaMatrixBox' + questionQueueItems[i].ID;
                } else {
                    idsToValidate = idsToValidate + ',' + 'formulaMatrixBox' + questionQueueItems[i].ID;
                }

                innerHtml = innerHtml +
                    '<div class="cost-centre-left-container"><label>Super Formula Matrix</label></div>' +
                    '<div class="cost-centre-right-container"><input id=formulaMatrixBox' + questionQueueItems[i].ID + ' type="text" disabled="disabled" ' +
                    'style="float:left; margin-right:10px;"  data-id=' + questionQueueItems[i].ID + ' class="CostCentreAnswersQueue" value=' + questionQueueItems[i].Qty1Answer + ' /> ' +
                    '<input type="button" onclick="ShowFormulaMatrix(' + questionQueueItems[i].RowCount + ',' + questionQueueItems[i].ColumnCount + ',' + i + '); return false;" class="Matrix-select-button rounded_corners5 " value="Select" /></div><div class="clearBoth"></div>';
            }
        }
    }

    for (var w = 0; w < workInstructions.length; w++) {

        var wOptionHtml = "";

        for (var c = 0; c < workInstructions[w].CostcentreWorkInstructionsChoices.length; c++) {
            wOptionHtml = wOptionHtml + '<option data-id=' + workInstructions[w].InstructionId + ' value=' + workInstructions[w].CostcentreWorkInstructionsChoices[c].Choice + ' >' + workInstructions[w].CostcentreWorkInstructionsChoices[c].Choice + '</option>'
        }
        innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
            + workInstructions[w].Instruction +
            '</label></div><div class="cost-centre-right-container"><select id=dropdown' + workInstructions[w].InstructionId + ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
            + wOptionHtml + '</select></div><br/><div class="clearBoth"></div>';

    }

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header">' +
        '<button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title left_align">' + Heading +
        '</h4></div><div class="modal-body left_align"><div id="CCErrorMesgContainer"></div>' + innerHtml +
        '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;">' +
        '<button  id="btnCostCentreCalculator" type="button" class="btn btn-primary float_right" ' +
        'onclick="ValidateCostCentreControl(' + costCentreId + ',' + clonedItemId + ',&#34; ' + currency +
        '&#34; ,' + itemPrice + ',' + costCentreType + ',' + taxRate + ',' + orderedQty + ');">Continue</button>' +
        '<img src="/Content/Images/costcentreLoader.gif" id="imgCCLoader" style="height: 20px;margin-right: 10px;margin-top:8px; display:none;"/></div></div></div>';


    var bws = getBrowserHeight();

    var parentContainer = $("#productFromRetailStoreModal")[0].style.display === "block" ? "#productFromRetailStoreModal" :
        $("#costCenters")[0].style.display === "block" ? "#costCenters" : "";

    $(parentContainer + " #layer")[0].style.width = bws.width + "px";
    $(parentContainer + " #layer")[0].style.height = bws.height + "px";

    var left = parseInt((bws.width - 730) / 2);

    $(parentContainer + " #innerLayer")[0].innerHTML = container;

    $(parentContainer + " #innerLayer")[0].style.left = left + "px";
    $(parentContainer + " #innerLayer")[0].style.top = "30px";

    $(parentContainer + " #innerLayer")[0].style.width = "730px";
    $(parentContainer + " #innerLayer")[0].style.position = "fixed";
    $(parentContainer + " #innerLayer")[0].style.zIndex = "9999";

    $(parentContainer + " #layer")[0].style.display = "block";
    $(parentContainer + " #innerLayer")[0].style.display = "block";
}

// Show Input Cost Center Popup
function ShowInputCostCentrePopup(inputQueueItems, costCentreId, clonedItemId, selectedCostCentreCheckBoxId, mode, currency,
    itemPrice, questionQueueObject, costCentreType, taxRate, workInstructions, orderedQty, itemAddOns, costCenter, afterCostCenterExecution) {

    GlobalInputQueueItemsList = inputQueueItems;
    GlobalQuestionQueueItemsList = questionQueueObject;
    selectedCostCentreCheckBoxElement = selectedCostCentreCheckBoxId;
    selectedStockOptionItemAddOns = itemAddOns;
    globalSelectedCostCenter = costCenter;
    globalAfterCostCenterExecution = afterCostCenterExecution;
    var innerHtml = "";
    var Heading = "Add " + $(selectedCostCentreCheckBoxId).next().html();

    if (mode == "New") { // This condition will execute when first time cost centre is prompting for values
        if (inputQueueItems) {
            for (var i = 0; i < inputQueueItems.length; i++) {

                if (inputQueueItems[i].ID != 1 && inputQueueItems[i].ID != 2) { // Id 1= setuptime , Id 2 = setup cost
                    if (idsToValidate == "") {
                        idsToValidate = 'txtBox' + inputQueueItems[i].ID;
                    } else {
                        idsToValidate = idsToValidate + ',' + 'txtBox' + inputQueueItems[i].ID;
                    }
                    if (inputQueueItems[i].Value != "") {
                        innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + inputQueueItems[i].VisualQuestion +
                            '</label></div><div class="cost-centre-right-container">' +
                            '<input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + inputQueueItems[i].ID +
                            ' data-id=' + inputQueueItems[i].ID + ' value=' + parseFloat(inputQueueItems[i].Value) + ' /></div><br/><div class="clearBoth"></div>';
                    } else {
                        innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + inputQueueItems[i].VisualQuestion +
                            '</label></div><div class="cost-centre-right-container">' +
                            '<input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + inputQueueItems[i].ID +
                            ' data-id=' + inputQueueItems[i].ID + ' /></div><br/><div class="clearBoth"></div>';
                    }

                }
            }
        }

    } else if (mode == "Modify") { // This condition will execute when cost centre is already prompted and user clicks to modify the values entered
        Heading = "Edit " + $(selectedCostCentreCheckBoxId).next().html();
        if (inputQueueItems) {
            for (var i = 0; i < inputQueueItems.length; i++) {

                if (inputQueueItems[i].ID != 1 && inputQueueItems[i].ID != 2) { // Id 1= setuptime , Id 2 = setup cost
                    if (idsToValidate == "") {
                        idsToValidate = 'txtBox' + inputQueueItems[i].ID;
                    } else {
                        idsToValidate = idsToValidate + ',' + 'txtBox' + inputQueueItems[i].ID;
                    }
                    if (inputQueueItems[i].Value != "") {
                        innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + inputQueueItems[i].VisualQuestion +
                            '</label></div><div class="cost-centre-right-container">' +
                            '<input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + inputQueueItems[i].ID +
                            ' data-id=' + inputQueueItems[i].ID + ' value=' + inputQueueItems[i].Qty1Answer + ' value=' +
                            parseFloat(inputQueueItems[i].Value) + ' /></div><br/><div class="clearBoth"></div>';
                    } else {
                        innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>' + inputQueueItems[i].VisualQuestion +
                            '</label></div><div class="cost-centre-right-container">' +
                            '<input type="text" class="cost-centre-dropdowns CostCentreAnswersQueue" id=txtBox' + inputQueueItems[i].ID +
                            ' data-id=' + inputQueueItems[i].ID + ' value=' + inputQueueItems[i].Qty1Answer + ' /></div><br/><div class="clearBoth"></div>';
                    }

                }
            }
        }

    }

    for (var w = 0; w < workInstructions.length; w++) {

        var WOptionHtml = "";

        for (var c = 0; c < workInstructions[w].CostcentreWorkInstructionsChoices.length; c++) {
            WOptionHtml = WOptionHtml + '<option data-id=' + workInstructions[w].InstructionId + ' value=' + workInstructions[w].CostcentreWorkInstructionsChoices[c].Choice + ' >' + workInstructions[w].CostcentreWorkInstructionsChoices[c].Choice + '</option>'
        }
        innerHtml = innerHtml + '<div class="cost-centre-left-container"><label>'
            + workInstructions[w].Instruction +
            '</label></div><div class="cost-centre-right-container"><select id=dropdown' + workInstructions[w].InstructionId +
            ' class="cost-centre-dropdowns CostCentreAnswersQueue">'
            + WOptionHtml + '</select></div><br/><div class="clearBoth"></div>';

    }

    var container = '<div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header">' +
        '<button class="md-close close" onclick=HideMessagePopUp(); >&times;</button><h4 class="modal-title left_align">' + Heading +
        '</h4></div><div class="modal-body left_align"><div id="CCErrorMesgContainer"></div>' + innerHtml +
        '<div class="modal-footer" style="margin-left: -20px;margin-right: -20px;">' +
        '<button id="btnCostCentreCalculator" type="button" class="btn btn-primary float_right" ' +
        'onclick="ValidateCostCentreControl(' + costCentreId + ',' + clonedItemId + ',&#34; ' + currency +
        '&#34; ,' + itemPrice + ',' + costCentreType + ',' + taxRate + ',' + orderedQty + ');">Continue</button>' +
        '<img src="/Content/Images/costcentreLoader.gif" id="imgCCLoader"  style="height: 20px; margin-right: 10px;margin-top:8px; display:none;" /></div></div></div>';


    var bws = getBrowserHeight();
    var parentContainer = $("#productFromRetailStoreModal")[0].style.display === "block" ? "#productFromRetailStoreModal" :
        $("#costCenters")[0].style.display === "block" ? "#costCenters" : "";
    $(parentContainer + " #layer")[0].style.width = bws.width + "px";
    $(parentContainer + " #layer")[0].style.height = bws.height + "px";

    var left = parseInt((bws.width - 730) / 2);

    $(parentContainer + " #innerLayer")[0].innerHTML = container;
    $(parentContainer + " #innerLayer")[0].style.left = left + "px";
    $(parentContainer + " #innerLayer")[0].style.top = "30px";
    $(parentContainer + " #innerLayer")[0].style.width = "730px";
    $(parentContainer + " #innerLayer")[0].style.position = "fixed";
    $(parentContainer + " #innerLayer")[0].style.zIndex = "9999";

    $(parentContainer + " #layer")[0].style.display = "block";
    $(parentContainer + " #innerLayer")[0].style.display = "block";
}

// Show Formula Matrix
function ShowFormulaMatrix(rows, columns, matrixIndex) {
    var matrixItems = GlobalQuestionQueueItemsList[matrixIndex].MatrixTable;
    var isFirstSetToEmpty = 0;
    var globalIndex = 0;
    var rowsHtml = "";
    var trHtml = "<tr>";

    for (var row = 0; row < rows; row++) {
        for (var col = 0; col < columns; col++) {

            if (col == 0 && isFirstSetToEmpty == 0) {
                isFirstSetToEmpty = 1;
                trHtml = trHtml + '<td></td>';
            } else {
                if (row == 0 || col == 0) {
                    trHtml = trHtml + '<td>' + matrixItems[globalIndex].Value + '</td>';

                } else {
                    trHtml = trHtml + '<td><button type="button" class="MatrixOption" onclick=SetMatrixAnswer(' + matrixItems[globalIndex].Value + ',' +
                        matrixItems[globalIndex].MatrixId + ');>' + matrixItems[globalIndex].Value + '</button></td>';
                }


                globalIndex = parseInt(globalIndex) + 1;
            }

        }
        rowsHtml = rowsHtml + trHtml + "</tr>";
        trHtml = "<tr>";
    }

    var container = '  <div class="md-modal md-effect-7" id="modal-7"><div class="md-content"><div class="modal-header"><button class="md-close close" onclick=HideFormulaPopUp();>&times;</button><h4 class="modal-title left_align">Please select a value from Matrix</h4></div><div class="modal-body left_align"><table class="cost-centre-Matrix">' + rowsHtml + '</table></div></div></div>';

    var bws = getBrowserHeight();

    var left = parseInt((bws.width - 730) / 2) + 20;

    var parentContainer = $("#productFromRetailStoreModal")[0].style.display === "block" ? "#productFromRetailStoreModal" :
        $("#costCenters")[0].style.display === "block" ? "#costCenters" : "";
    $(parentContainer + " #FormulaMatrixLayer")[0].innerHTML = container;

    $(parentContainer + " #FormulaMatrixLayer")[0].style.left = left + "px";
    $(parentContainer + " #FormulaMatrixLayer")[0].style.top = "75px";

    $(parentContainer + " #FormulaMatrixLayer")[0].style.width = "700px";
    $(parentContainer + " #FormulaMatrixLayer")[0].style.position = "fixed";
    $(parentContainer + " #FormulaMatrixLayer")[0].style.zIndex = "9999";
    $(parentContainer + " #FormulaMatrixLayer")[0].style.boxShadow = "1px 1px 5px #888888";
    $(parentContainer + " #FormulaMatrixLayer")[0].style.display = "block";
}

// Hide Formula Popup
function HideFormulaPopUp() {
    var parentContainer = $("#productFromRetailStoreModal")[0].style.display === "block" ? "#productFromRetailStoreModal" :
        $("#costCenters")[0].style.display === "block" ? "#costCenters" : "";
    $(parentContainer + " #FormulaMatrixLayer")[0].style.display = "none";
}

// Hide Cost Center Question Dialog
function HideLoader() {
    var parentContainer = $("#productFromRetailStoreModal")[0].style.display === "block" ? "#productFromRetailStoreModal" :
        $("#costCenters")[0].style.display === "block" ? "#costCenters" : "";
    $(parentContainer + " #layer")[0].style.display = "none";
    $(parentContainer + " #innerLayer")[0].style.display = "none";
}

// Sets Matrix Answer
function SetMatrixAnswer(answer, matrixId) {
    var parentContainer = $("#productFromRetailStoreModal")[0].style.display === "block" ? "#productFromRetailStoreModal" :
        $("#costCenters")[0].style.display === "block" ? "#costCenters" : "";
    $("#formulaMatrixBox" + matrixId).val(answer);
    $(parentContainer + " #FormulaMatrixLayer")[0].style.display = "none";
}

// Set Cost Center Queue Object To Save in Db
function SetCostCenterQueueObjectToSaveInDb(costCenterType, updatedGlobalQueueArray, costCentreQueueObjectToSaveInDb, costCentreId) {
    if (costCenterType == 4) { // question queue
        if (updatedGlobalQueueArray && updatedGlobalQueueArray.QuestionQueues) {
            for (var j = 0; j < updatedGlobalQueueArray.QuestionQueues.length; j++) {
                if (updatedGlobalQueueArray.QuestionQueues[j].CostCentreID == costCentreId) {
                    costCentreQueueObjectToSaveInDb.push(updatedGlobalQueueArray.QuestionQueues[j]);
                }
            }
        }

    } else { // input queue
        if (updatedGlobalQueueArray && updatedGlobalQueueArray.InputQueues) {
            for (var k = 0; k < updatedGlobalQueueArray.InputQueues.length; k++) {

                if (updatedGlobalQueueArray.InputQueues[k].CostCentreID == costCentreId) {
                    costCentreQueueObjectToSaveInDb.push(updatedGlobalQueueArray.InputQueues[k]);
                }
            }
        }

    }

}

// Update Question and Input Queue
function UpdateQuestionAndInputQueue(updatedGlobalQueueArray) {
    if (updatedGlobalQueueArray && updatedGlobalQueueArray.QuestionQueues != null) {
        var questionQueueDbObject = [];
        for (var m = 0; m < updatedGlobalQueueArray.QuestionQueues.length; m++) {

            questionQueueDbObject.push(updatedGlobalQueueArray.QuestionQueues[m]);

        }

        //if (QuestionQueueDBObject.length > 0) {
        //    $("#VMJsonAddOnsQuestionQueue").val(JSON.stringify(QuestionQueueDBObject, null, 2));
        //}
    }
    if (updatedGlobalQueueArray && updatedGlobalQueueArray.InputQueues != null) {
        var inputQueueDbObject = [];
        for (var n = 0; n < updatedGlobalQueueArray.InputQueues.length; n++) {

            inputQueueDbObject.push(updatedGlobalQueueArray.InputQueues[n]);

        }

        //if (InputQueueDBObject.length > 0) {
        //    $("#VMJsonAddOnsInputQueue").val(JSON.stringify(InputQueueDBObject, null, 2));
        //}
    }
}

// Set Global Cost Centre Queue
function SetGlobalCostCentreQueue(globalQuestionQueueItemsList, globalInputQueueItemsList, costCentreId, costCentreType,
    clonedItemId, selectedCostCentreCheckBoxId, desriptionOfQuestion, itemPrice, currencyCode, isPromptAQuestion, taxRate, orderedQty, itemAddOns, costCenter,
    afterCostCenterExecution, isCalledAfterQuestionPrompt) {

    var jsonObjectsOfGlobalQueue = null;
    var inputAndQuestionQueues;
    if (!costCentreQueueItems) {
        inputAndQuestionQueues = {
            QuestionQueues: globalQuestionQueueItemsList,
            InputQueues: globalInputQueueItemsList
        };
        jsonObjectsOfGlobalQueue = JSON.stringify(inputAndQuestionQueues, null, 2);
        costCentreQueueItems = jsonObjectsOfGlobalQueue;

    } else {
        var isUpdated = false;
        inputAndQuestionQueues = JSON.parse(costCentreQueueItems);
        if (inputAndQuestionQueues.InputQueues == null) {
            inputAndQuestionQueues.InputQueues = [];
            if (globalInputQueueItemsList) {
                for (var i = 0; i < globalInputQueueItemsList.length; i++) {
                    inputAndQuestionQueues.InputQueues.push(globalInputQueueItemsList[i]);
                }
            }

        } else {
            if (globalInputQueueItemsList && inputAndQuestionQueues) {
                for (var i = 0; i < globalInputQueueItemsList.length; i++) {
                    for (var j = 0; j < inputAndQuestionQueues.InputQueues.length; j++) {

                        if (inputAndQuestionQueues.InputQueues[j].CostCentreID == globalInputQueueItemsList[i].CostCentreID &&
                            inputAndQuestionQueues.InputQueues[j].ID == globalInputQueueItemsList[i].ID) {
                            inputAndQuestionQueues.InputQueues[j].Qty1Answer = globalInputQueueItemsList[i].Qty1Answer;
                            isUpdated = true;
                            break;
                        }
                    }

                    if (isUpdated == false) {
                        inputAndQuestionQueues.InputQueues.push(globalInputQueueItemsList[i]);
                        isUpdated = false;
                    }
                }
            }

        }

        if (globalQuestionQueueItemsList && inputAndQuestionQueues) {
            for (var i = 0; i < globalQuestionQueueItemsList.length; i++) {
                for (var j = 0; j < inputAndQuestionQueues.QuestionQueues.length; j++) {
                    if (inputAndQuestionQueues.QuestionQueues[j].CostCentreID == globalQuestionQueueItemsList[i].CostCentreID &&
                        inputAndQuestionQueues.QuestionQueues[j].ID == globalQuestionQueueItemsList[i].ID) {
                        inputAndQuestionQueues.QuestionQueues[j].Qty1Answer = globalQuestionQueueItemsList[i].Qty1Answer;
                        isUpdated = true;
                        break;

                    }
                }

                if (isUpdated == false) {
                    if (inputAndQuestionQueues.QuestionQueues == null) {
                        inputAndQuestionQueues.QuestionQueues = [];
                    }
                    inputAndQuestionQueues.QuestionQueues.push(globalQuestionQueueItemsList[i]);
                    isUpdated = false;
                }
            }
        }

        if (inputAndQuestionQueues) {
            costCentreQueueItems = JSON.stringify(inputAndQuestionQueues, null, 2);
        }
    }

    var updatedGlobalQueueArray = JSON.parse(costCentreQueueItems);
    var costCentreQueueObjectToSaveInDb = [];
    if (!isCalledAfterQuestionPrompt) {
        globalAfterCostCenterExecution = afterCostCenterExecution;
    }

    var to;
    to = "/webstoreapi/costCenter/ExecuteCostCentre?CostCentreId=" + costCentreId + "&ClonedItemId=" + clonedItemId + "&OrderedQuantity=" + orderedQty + "&CallMode=New";
    var options = {
        type: "POST",
        url: to,
        data: costCentreQueueItems,
        contentType: "application/json",
        async: true,
        success: function (response) {

            var updatedAddOns = itemAddOns;

            if (updatedAddOns() !== null) {

                for (var i = 0; i < updatedAddOns().length; i++) {

                    if (updatedAddOns()[i].costCentreId() == costCentreId) {
                        updatedAddOns()[i].totalPrice(response);

                        // Sets Cost Center Queue Object to save in db
                        SetCostCenterQueueObjectToSaveInDb(costCentreType, updatedGlobalQueueArray, costCentreQueueObjectToSaveInDb, costCentreId);

                        if (costCentreQueueObjectToSaveInDb && costCentreQueueObjectToSaveInDb.length > 0) {
                            updatedAddOns()[i].CostCentreJasonData = JSON.stringify(costCentreQueueObjectToSaveInDb, null, 2);
                        }

                        break;
                    }
                }

                UpdateQuestionAndInputQueue(updatedGlobalQueueArray);

                var jsonToReSubmit = [];

                var totalVal = 0;
                var taxAppliedValue = 0;
                // add checked cost centre values to gross total
                for (var i = 0; i < updatedAddOns().length; i++) {
                    jsonToReSubmit.push(updatedAddOns()[i]);
                }

                //displayTotalPrice(itemPrice, totalVal);
                taxAppliedValue = response;
                taxAppliedValue = taxAppliedValue + ((taxAppliedValue * taxRate) / 100);
                if (isPromptAQuestion == true) { // TODO: Modify Scenario
                    //$(selectedCostCentreCheckBoxId).next().next().html('<label>' + currencyCode + (taxAppliedValue).toFixed(2).toString() + '</label>' + '<a class="CCModifyLink" onclick="PromptQuestion(' + costCentreId + ',' + selectedCostCentreCheckBoxId + ',' + costCentreType + ', 1);" >Modify</a> ');
                } else {
                    //$(selectedCostCentreCheckBoxId).next().next().html('<label>' + currencyCode + (taxAppliedValue).toFixed(2).toString() + '</label>');
                }
                //$("#VMAddOnrice").val(totalVal);
                // $("#VMJsonAddOns").val(JSON.stringify(JsonToReSubmit));
            }
            else if (costCenter() !== null) {
                costCenter().setupCost(response);

                // Sets Cost Center Queue Object to save in db
                SetCostCenterQueueObjectToSaveInDb(costCentreType, updatedGlobalQueueArray, costCentreQueueObjectToSaveInDb, costCentreId);

                UpdateQuestionAndInputQueue(updatedGlobalQueueArray);
            }
            HideLoader();
            if (globalAfterCostCenterExecution && typeof globalAfterCostCenterExecution === "function") {
                globalAfterCostCenterExecution();
            }
        },
        error: function (msg) { toastr.error("Error occured "); console.log(msg); }
    };
    var returnText = $.ajax(options).responseText;
}


// #endregion


$(function () {
    // Fix for bootstrap popovers, sometimes they are left in the DOM when they shouldn't be.
    $('body').on('hidden.bs.popover', function () {
        var popovers = $('.popover').not('.in');
        if (popovers) {
            popovers.remove();
        }
    });
});
