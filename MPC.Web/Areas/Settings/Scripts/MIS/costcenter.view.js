define("costcenter/costcenter.view",
    ["jquery", "costcenter/costcenter.viewModel"], function ($, costcenterViewModel) {

        var ist = window.ist || {};

        // View 
        ist.costcenter.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#CostCentersBinding")[0],
                //showCostCenterDialog = function () {
                //    $("#CostCenterDialog").modal("show");
                //},
                //// Hide Activity the dialog
                //hideCostCenterDialog = function () {
                //    $("#CostCenterDialog").modal("hide");
                //},
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel
                //showCostCenterDialog: showCostCenterDialog,
                //hideCostCenterDialog: hideCostCenterDialog
            };
        })(costcenterViewModel);

        // Initialize the view model
        if (ist.costcenter.view.bindingRoot) {
            costcenterViewModel.initialize(ist.costcenter.view);
        }
        return ist.costcenter.view;
    });