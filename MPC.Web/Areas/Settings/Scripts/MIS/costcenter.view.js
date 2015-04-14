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
                  hideCostCentreQuestionDialog = function () {
                      $("#CostCentreQuestionModal").modal("hide");
                      
                  },
                  showAddEditQuestionMenu = function () {
                      $(".AddEditQuestion").contextMenu({
                          menuSelector: "#contextMenu"
                      });
                  },
                   showCostCentreMatrixDialog = function () {
                       $("#CostCentreMatrixModal").modal("show");
                       //   initializeLabelPopovers();
                   },
                  hideCostCentreMatrixDialog = function () {
                      $("#CostCentreMatrixModal").modal("hide");

                  },
                showAddEditMatrixMenu = function () {
                    $(".AddEditMatrix").contextMenu({
                        menuSelector: "#contextMenuMatrix"
                    });
                },
                showCostCentreStockDialog = function () {
                    $("#CostCentreStockModal").modal("show");
                    //   initializeLabelPopovers();
                },
                  hideCostCentreStockDialog = function () {
                      $("#CostCentreStockModal").modal("hide");

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
                hideCostCentreQuestionDialog:hideCostCentreQuestionDialog,
                showAddEditQuestionMenu: showAddEditQuestionMenu,
                showCostCentreMatrixDialog: showCostCentreMatrixDialog,
                hideCostCentreMatrixDialog: hideCostCentreMatrixDialog,
                showAddEditMatrixMenu: showAddEditMatrixMenu,
                showCostCentreStockDialog: showCostCentreStockDialog,
                hideCostCentreStockDialog: hideCostCentreStockDialog
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