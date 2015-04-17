define("machine/machine.view",
    ["jquery", "machine/machine.viewModel"], function ($, machineViewModel) {//machineViewModel

        var ist = window.ist || {};

        // View 
        ist.machine.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#divMachineListBinding")[0],
                initializeLabelPopovers = function () {
                   // ReSharper disable UnknownCssClass
                    $('.bs-example-tooltips a').popover();
                    $('.bs-example-tooltips a').click(function () {
                        $('.bs-example-tooltips a').not(this).popover('hide'); //all but this
                    });
                    $("a").click(function () {
                        $('.bs-example-tooltips a').not(this).popover('hide');
                    });
                    $("button").click(function () {
                        $('.bs-example-tooltips a').not(this).popover('hide');
                    });
                   
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
        })(machineViewModel);

        // Initialize the view model
        if (ist.machine.view.bindingRoot) {
            machineViewModel.initialize(ist.machine.view);
        }
        return ist.machine.view;
    });