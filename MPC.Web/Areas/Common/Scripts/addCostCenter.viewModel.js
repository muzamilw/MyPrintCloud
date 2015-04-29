﻿/*
    Module with the view model for Cost Center
*/
define("common/addCostCenter.viewModel",
    ["jquery", "amplify", "ko", "common/addCostCenter.dataservice", "common/addCostCenter.model", "common/pagination"], function ($, amplify, ko, dataservice, model,
    pagination) {
        var ist = window.ist || {};
        ist.addCostCenter = {
            viewModel: (function () {
                var // The view 
                    view,
                    // Cost Centres
                    costCentres = ko.observableArray([]),
                    // Cost Center Dialog Filter
                    costCenterDialogFilter = ko.observable(),
                    // Active Cost Center
                    selectedCostCentre = ko.observable(),
                    // Is Display Flag for Cost Center Quantity Dialog 
                    isDisplayCostCenterQuantityDialog = ko.observable(false),
                    //Is Inventory Dialog is opening from Order Dialog's add Product From Inventory
                    isAddProductFromInventory = ko.observable(false),
                    //Is Inventory Dialog is opening for Section Cost Center
                    isAddProductForSectionCostCenter = ko.observable(false),
                    //Is Cost Center dialog open for shipping
                    isCostCenterDialogForShipping = ko.observable(false),
                         // Pagination For Press Dialog
                    costCentreDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, costCentres)),
                    // Search Stock Items
                    searchCostCenters = function () {
                        costCentreDialogPager().reset();
                        getCostCenters();
                    },
                    // Reset Cost center Items
                    resetCostCenters = function () {
                        // Reset Text 
                        resetDialogFilters();
                        // Reset Callback
                        afterAddCostCenter = null;
                        // Reset List
                        costCentres.removeAll();
                        // Reset Pager
                        costCentreDialogPager().reset();
                    },
                    // after selection
                    afterAddCostCenter = null,
                    // Reset Stock Dialog Filters
                    resetDialogFilters = function () {
                        // Reset Text 
                        costCenterDialogFilter(undefined);

                    },
                    // Show
                    show = function (afterAddCostCenterCallback, companyId) {
                        isAddProductForSectionCostCenter(false);
                        isAddProductFromInventory(false);
                        isDisplayCostCenterQuantityDialog(false);
                        resetCostCenters();
                        view.showDialog();
                        afterAddCostCenter = afterAddCostCenterCallback;
                        getCostCenters(companyId);
                    },
                    // On Select Cost Center
                    onSelectCostCenter = function (costCenter) {
                        selectedCostCentre(costCenter);
                        view.showCostCentersQuantityDialog();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        costCentreDialogPager(new pagination.Pagination({ PageSize: 5 }, costCentres, getCostCenters));
                    },
                     onSaveProductCostCenter = function () {
                         if (afterAddCostCenter && typeof afterAddCostCenter === "function") {
                             afterAddCostCenter(selectedCostCentre());
                         }
                         hideCostCentreDialog();
                         hideCostCentreQuantityDialog();
                         //toastr.success("test");
                     },
                      hideCostCentreDialog = function () {
                          view.hideDialog();
                      },
                       hideCostCentreQuantityDialog = function () {
                           view.hideCostCentersQuantityDialog();
                       },
                    // Get Cost Centers
                    getCostCenters = function (companyId) {
                        dataservice.getCostCentersForProduct({
                            CompanyId: companyId,
                            SearchString: costCenterDialogFilter(),
                            PageSize: costCentreDialogPager().pageSize(),
                            PageNo: costCentreDialogPager().currentPage(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    costCentres.removeAll();
                                    _.each(data.CostCentres, function (item) {
                                        var costCentre = new model.CostCentre.Create(item);
                                        costCentres.push(costCentre);
                                    });
                                    costCentreDialogPager().totalCount(data.RowCount);
                                }
                            },
                            error: function (response) {
                                costCentres.removeAll();
                                toastr.error("Failed to Load Cost Centres. Error: " + response);
                            }
                        });
                    };

                return {
                    //Arrays
                    costCenterDialogFilter: costCenterDialogFilter,
                    searchCostCenters: searchCostCenters,
                    resetCostCenters: resetCostCenters,
                    costCentreDialogPager: costCentreDialogPager,
                    costCentres: costCentres,
                    //Utilities
                    onSelectCostCenter: onSelectCostCenter,
                    initialize: initialize,
                    show: show,
                    isDisplayCostCenterQuantityDialog: isDisplayCostCenterQuantityDialog,
                    isAddProductFromInventory: isAddProductFromInventory,
                    isAddProductForSectionCostCenter: isAddProductForSectionCostCenter,
                    isCostCenterDialogForShipping: isCostCenterDialogForShipping,
                    onSaveProductCostCenter: onSaveProductCostCenter,
                    selectedCostCentre: selectedCostCentre
                };
            })()
        };

        return ist.addCostCenter.viewModel;
    });
