/*
    View for the Item Job Status. Used to keep the viewmodel clear of UI related logic
*/
define("itemJobStatus/itemJobStatus.view",
    ["jquery", "itemJobStatus/itemJobStatus.viewModel"], function ($, itemJobStatusViewModel) {

        var ist = window.ist || {};

        // View 
        ist.itemJobStatus.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                    // Binding root used with knockout
                bindingRoot = $("#itemJobStatusBinding")[0],
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


                //#endregion
                // Initialize Label Popovers
                initializeLabelPopovers = function () {
                    // ReSharper disable UnknownCssClass
                    $('.bs-example-tooltips a').popover();
                    // ReSharper restore UnknownCssClass
                    window.scrollTo(0, 0);
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
                gotoElement: gotoElement,
                initializeLabelPopovers: initializeLabelPopovers,

            };
        })(itemJobStatusViewModel);

        // Initialize the view model
        if (ist.itemJobStatus.view.bindingRoot) {
            itemJobStatusViewModel.initialize(ist.itemJobStatus.view);
        }
        return ist.itemJobStatus.view;
    });
