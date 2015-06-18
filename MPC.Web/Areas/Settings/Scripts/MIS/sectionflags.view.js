define("sectionflags/sectionflags.view",
    ["jquery", "sectionflags/sectionflags.viewModel"], function ($, sectionflagsViewModel) {

        var ist = window.ist || {};

        // View 
        ist.sectionflags.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#divSalesPipeline")[0],
                   // Go To Element with Validation Errors
                gotoElement = function (element) {
                    var tab = $(element).closest(".tab-pane");
                    if (!tab) {
                        return;
                    }

                    var liElement = $('a[href=#' + tab.attr('id') + ']');
                    if (!liElement) {
                        return;
                    }

                    liElement.click();

                    // Scroll to Element
                    setTimeout(function () {
                        window.scrollTo($(element).offset().left, $(element).offset().top - 50);
                        // Focus on element
                        $(element).focus();
                    }, 1000);
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
                gotoElement: gotoElement
            };
        })(sectionflagsViewModel);

        // Initialize the view model
        if (ist.sectionflags.view.bindingRoot) {
            sectionflagsViewModel.initialize(ist.sectionflags.view);
        }
        return ist.sectionflags.view;
    });