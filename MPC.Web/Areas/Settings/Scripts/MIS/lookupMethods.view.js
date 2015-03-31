//define("lookupMethods/lookupMethods.view",
//    ["jquery", "lookupMethods/lookupMethods.viewModel"], function ($, lookupMethodViewModel) {//lookupMethodViewModel

//        var ist = window.ist || {};

//        // View 
//        ist.lookupMethods.view = (function (specifiedViewModel) {
//            var
//                // View model 
//                viewModel = specifiedViewModel,
//                // Binding root used with knockout
//                bindingRoot = $("#divlookupMethodBinding")[0],

//                 // Initialize
//                initialize = function () {
//                    if (!bindingRoot) {
//                        return;
//                    }
//                };
//            initialize();
//            return {
//                bindingRoot: bindingRoot,
//                viewModel: viewModel,
//                initialize: initialize
//            };
//        })(lookupMethodViewModel);

//        // Initialize the view model
//        if (ist.lookupMethods.view.bindingRoot) {
//            lookupMethodViewModel.initialize(ist.lookupMethods.view);
//        }
//        return ist.lookupMethods.view;
//    });


define("lookupMethods/lookupMethods.view",
    ["jquery", "lookupMethods/lookupMethods.viewModel"], function ($, lookupMethodsViewModel) {//lookupMethodsViewModel

        var ist = window.ist || {};

        // View 
        ist.lookupMethods.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#divlookupMethodBinding")[0],
                initializeLabelPopovers = function () {
                    // ReSharper disable UnknownCssClass
                    $('[data-toggle="popover"]').popover();
                    $('.bs-example-tooltips a').popover();
                    $('.bs-example-tooltips a').click(function () {
                        $('.bs-example-tooltips a').not(this).popover('hide'); //all but this
                    });
                    $("a").click(function () {
                        $('.bs-example-tooltips a').not(this).popover('hide');
                    });
                    $(".dd-handle").click(function () {
                        $('[data-toggle="popover"]').popover('hide');
                    });
                    $("button").click(function () {
                        $('.bs-example-tooltips a').not(this).popover('hide');
                    });
                    // ReSharper restore UnknownCssClass
                },
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                initialize: initialize,
                initializeLabelPopovers: initializeLabelPopovers
            };
        })(lookupMethodsViewModel);

        // Initialize the view model
        if (ist.lookupMethods.view.bindingRoot) {
            lookupMethodsViewModel.initialize(ist.lookupMethods.view);
        }
        return ist.lookupMethods.view;
    });