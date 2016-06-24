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
                showAddEditClickChargeZoneMenu = function () {
                    $(".addEditClickChargeZone").contextMenu({
                        menuSelector: "#contextMenuZone"
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
                showClickChargeZoneDialog = function() {
                    $("#clickChargeZoneDialog").modal("show");
                },
                hideClickChargeZoneDialog = function () {
                    $("#clickChargeZoneDialog").modal("hide");
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
                hideCostCentreStockDialog: hideCostCentreStockDialog,
                initializeLabelPopovers: initializeLabelPopovers,
                gotoElement: gotoElement,
                showAddEditClickChargeZoneMenu: showAddEditClickChargeZoneMenu,
                showClickChargeZoneDialog: showClickChargeZoneDialog,
                hideClickChargeZoneDialog: hideClickChargeZoneDialog
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