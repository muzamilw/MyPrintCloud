// Global Variable
var ist = {
    datePattern: "DD/MM/YY",
    shortDatePattern: "dd-M-yy",
    customShortDatePattern: "dd-mm-yy",
    timePattern: "HH:mm",
    hourPattern: "HH",
    minutePattern: "mm",
    dateTimePattern: "DD/MM/YY HH:mm",
    dateTimeWithSecondsPattern: "DD/MM/YY HH:mm:ss",
    // UTC Date Format
    utcFormat: "YYYY-MM-DDTHH:mm:ss",
    //server exceptions enumeration 
    exceptionType: {
        CaresGeneralException: 'CaresGeneralException',
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
    siteUrl: ""
};

// Busy Indicator
var spinnerVisibleCounter = 0;

// Show Busy Indicator
function showProgress() {
    ++spinnerVisibleCounter;
    if (spinnerVisibleCounter > 0) {
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
                    if (errorObject.errorDetail.ExceptionType === ist.exceptionType.CaresGeneralException) {
                        error(errorObject.errorDetail.Message, ist.exceptionType.CaresGeneralException);
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

    // jquery date picker binding. Usage: <input data-bind="datepicker: myDate, datepickerOptions: { minDate: new Date() }" />. Source: http://jsfiddle.net/rniemeyer/NAgNV/
    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            //initialize datepicker with some optional options
            // ReSharper disable DuplicatingLocalDeclaration
            var options = allBindingsAccessor().datepickerOptions || {};
            // ReSharper restore DuplicatingLocalDeclaration
            $(element).datepicker(options);
            $(element).datepicker("option", "dateFormat", options.dateFormat || ist.customShortDatePattern);
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
            $(element).datetimepicker("option", "dateFormat", options.dateFormat || ist.customShortDatePattern);
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
    ko.bindingHandlers.colorPicker = {
        init: function (element, valueAccessor) {
            var value = valueAccessor();
            $(element).val(ko.utils.unwrapObservable(value));
            $(element).colorPicker();
            $(element).change(function () { value(this.value); });
        },
        update: function (element, valueAccessor) {
            $(element).val(ko.utils.unwrapObservable(valueAccessor()));
        }
    }
    // date formatting. Example <div class="date" data-bind="dateString: today, datePattern: 'dddd, MMMM dd, yyyy'">Thursday, April 05, 2012</div>
    ko.bindingHandlers.dateString = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(),
                allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            var pattern = allBindings.datePattern || ist.datePattern;
            if (valueUnwrapped !== undefined && valueUnwrapped !== null) {
                $(element).text(moment(valueUnwrapped).format(pattern));
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

    //Custom binding for handling validation messages in tooltip
    ko.bindingHandlers.tooltip = {
        update: function (element, valueAccessor) {
            var $element = $(element);
            var value = ko.utils.unwrapObservable(valueAccessor());
            value = value.replace(/:0/g, ':00');
            $element.tooltip({ title: value, html: true }); //, delay: { show: 10000, hide: 10000 }
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

//Unit length
unitLengthsGlobal = [{ Id: 1, Text: 'mm' },
    { Id: 2, Text: 'cm' },
    { Id: 3, Text: 'inch' }
];
//Currency Symbol
currencySymbolsGlobal = [{ Id: 1, Text: 'USD $' },
    { Id: 2, Text: 'GBD ' },
    { Id: 3, Text: 'AUD A$' },
    { Id: 3, Text: 'CAD C$' }
];
//Language Pack
languagePacksGlobal = [{ Id: 1, Text: 'English' },
    { Id: 2, Text: 'Frech' },
    { Id: 3, Text: 'Dutch' }
];
//Unit Weights
unitWeightsGlobal = [{ Id: 1, Text: 'lbs' },
    { Id: 2, Text: 'gsm' },
    { Id: 3, Text: 'kg' }
];
$(function () {
    // Fix for bootstrap popovers, sometimes they are left in the DOM when they shouldn't be.
    $('body').on('hidden.bs.popover', function () {
        var popovers = $('.popover').not('.in');
        if (popovers) {
            popovers.remove();
        }
    });
});

//function format(item) {
//    return $+item.FlagName;
//}