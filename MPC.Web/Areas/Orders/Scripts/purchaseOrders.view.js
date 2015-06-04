/*
    View for the purchase Orders. Used to keep the viewmodel clear of UI related logic
*/
define("purchaseOrders/purchaseOrders.view",
    ["jquery", "purchaseOrders/purchaseOrders.viewModel"], function ($, deliveryNotesViewModel) {

        var ist = window.ist || {};

        // View 
        ist.purchaseOrders.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                    // Binding root used with knockout
                bindingRoot = $("#purchaseOrderBinding")[0],
                // Go To Element with Validation Errors
                gotoElement = function (element) {
                    //var tab = $(element).closest(".tab-pane");
                    //if (!tab) {
                    //    return;
                    //}

                    //var liElement = $('a[href=#' + tab.attr('id') + ']');
                    //if (!liElement) {
                    //    return;
                    //}

                    //liElement.click();

                    var target = $(element);
                    target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                    if (target.length) {
                        $('html,body').animate({
                            scrollTop: (target.offset().top - 50)
                        }, 1000);
                        return false;
                    }
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
                gotoElement:gotoElement

            };
        })(deliveryNotesViewModel);

        // Initialize the view model
        if (ist.purchaseOrders.view.bindingRoot) {
            deliveryNotesViewModel.initialize(ist.purchaseOrders.view);
        }
        return ist.purchaseOrders.view;
    });
