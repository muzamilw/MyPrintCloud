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
                initialize:initialize
            };
        })(machineViewModel);

        // Initialize the view model
        if (ist.machine.view.bindingRoot) {
            machineViewModel.initialize(ist.machine.view);
        }
        return ist.machine.view;
    });