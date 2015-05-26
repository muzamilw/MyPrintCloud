/*
    View for the delivery Notes. Used to keep the viewmodel clear of UI related logic
*/
define("deliveryNotes/deliveryNotes.view",
    ["jquery", "deliveryNotes/deliveryNotes.viewModel"], function ($, deliveryNotesViewModel) {

        var ist = window.ist || {};

        // View 
        ist.deliveryNotes.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                    // Binding root used with knockout
                bindingRoot = $("#dNotesBinding")[0],

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

            };
        })(deliveryNotesViewModel);

        // Initialize the view model
        if (ist.deliveryNotes.view.bindingRoot) {
            deliveryNotesViewModel.initialize(ist.deliveryNotes.view);
        }
        return ist.deliveryNotes.view;
    });
