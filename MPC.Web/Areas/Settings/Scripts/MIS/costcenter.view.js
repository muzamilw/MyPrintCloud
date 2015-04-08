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
                productCategorySelectedEvent = function (category) {
                    $.event.trigger({
                        type: "ProductCategorySelected",
                        category: category
                    });
                },
                 showCostCentreQuestionDialog = function () {
                     $("#CostCentreQuestionModal").modal("show");
                  //   initializeLabelPopovers();
                 },

                showAddEditQuestionMenu = function () {
                    $(".AddEditQuestion").contextMenu({
                        menuSelector: "#contextMenu"
                        //menuSelected: function (invokedOn, selectedMenu) {
                        //    var msg = "You selected the menu item '" + selectedMenu.text() +
                        //        "' on the value '" + invokedOn.text() + "'";
                        //    alert(msg);
                        //}
                    });
                },
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
                viewModel: viewModel,
                productCategorySelectedEvent: productCategorySelectedEvent,
                showCostCentreQuestionDialog: showCostCentreQuestionDialog,
                showAddEditQuestionMenu: showAddEditQuestionMenu
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