/*
    Module with the view model for the My Organization.
*/
define("costcenter/costcenter.viewModel",
    ["jquery", "amplify", "ko", "costcenter/costcenter.dataservice", "costcenter/costcenter.model", "common/confirmation.viewModel", "common/pagination", "common/sharedNavigation.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVM) {
        var ist = window.ist || {};
        ist.costcenter = {
            viewModel: (function () {
                var // the view 
                    view,
                    // Active
                    costCentersList = ko.observableArray([]),
                    errorList = ko.observableArray([]),
                    // Cost Center Types
                    costCenterTypes = ko.observableArray([]),
                    // System Users as Resources
                    costCenterResources = ko.observableArray([]),
                    deliveryCarriers = ko.observableArray([]),
                    // Nominal Codes
                    nominalCodes = ko.observableArray([]),
                    costCenterVariables = ko.observableArray([]),
                    // Markups
                    markups = ko.observableArray([]),
                    // Cost Center Categories
                    costCenterCategories = ko.observableArray([]),
                    workInstructions = ko.observableArray([]),
                    variablesTreePrent = ko.observableArray([{ Id: 1, Text: 'Cost Centers' },
                                                { Id: 2, Text: 'Variables' },
                                                { Id: 3, Text: 'Resources' },
                                                { Id: 4, Text: 'Questions' },
                                                { Id: 5, Text: 'Matrices' },
                                                { Id: 6, Text: 'Lookup' },
                                                { Id: 7, Text: 'Stock Items' }
                    ]),

                    getVariableTreeChildListItems = function (dataRecieved, event) {
                        var id = $(event.target).closest('li')[0].id;
                        if ($(event.target).closest('li').children('ol').length > 0) {
                            if ($(event.target).closest('li').children('ol').is(':hidden')) {
                                $(event.target).closest('li').children('ol').show();
                            } else {
                                $(event.target).closest('li').children('ol').hide();
                            }
                            return;
                        }
                        dataservice.getProductCategoryChilds({
                            id: id,
                        }, {
                            success: function (data) {
                                if (data.ProductCategories != null) {
                                    _.each(data.ProductCategories, function (productCategory) {
                                        $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectChildProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + productCategory.ProductCategoryId + '> <div class="dd-handle-list" data-bind="click: $root.getCategoryChildListItems"><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + productCategory.CategoryName + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>');
                                        ko.applyBindings(view.viewModel, $("#" + productCategory.ProductCategoryId)[0]);
                                        var category = {
                                            productCategoryId: productCategory.ProductCategoryId,
                                            categoryName: productCategory.CategoryName,
                                            parentCategoryId: id
                                        };
                                        parentCategories.push(category);
                                    });
                                }
                                isLoadingStores(false);
                                $("#categoryTabItems li a").first().trigger("click");
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Error: Failed To load Categories " + response, "", ist.toastrOptions);
                            }
                        });
                    },
                    // #region Busy Indicators
                    isLoadingCostCenter = ko.observable(false),
                    
                    // #endregion Busy Indicators
                    sortOn = ko.observable(1),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    isEditorVisible = ko.observable(),
                    selectedCostCenter = ko.observable(),
                    templateToUse = function(ocostCenter) {
                        return (ocostCenter === selectedCostCenter() ? 'editCostCenterTemplate' : 'itemCostCenterTemplate');
                    },
                    makeEditable = ko.observable(false),
                    //Delete Cost Center
                    deleteCostCenter = function(oCostCenter) {
                        dataservice.deleteCostCenter({
                            CostCentreId: oCostCenter.CostCentreId(),
                        }, {
                            success: function(data) {
                                if (data != null) {
                                    costCentersList.remove(oCostCenter);
                                    toastr.success(" Deleted Successfully !");
                                }
                            },
                            error: function(response) {
                                toastr.error("Failed to Delete . Error: " + response);
                            }
                        });
                    },
                    onDeleteCostCenter = function(oCostCenter) {
                        if (!oCostCenter.CostCentreId()) {
                            costCentersList.remove(oCostCenter);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function() {
                            deleteCostCenter(oCostCenter);
                        });
                        confirmation.show();
                    },
                    getCostCenters = function() {
                        isLoadingCostCenter(true);
                        
                        dataservice.getCostCentersList({
                            CostCenterFilterText: searchFilter(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc(),
                            CostCenterType: CostCenterType
                        }, {
                            success: function(data) {
                                costCentersList.removeAll();
                                if (data != null) {
                                    pager().totalCount(data.RowCount);
                                    _.each(data.CostCenters, function(item) {
                                        var module = model.costCenterListView.Create(item);
                                        costCentersList.push(module);
                                    });
                                }
                                isLoadingCostCenter(false);
                            },
                            error: function(response) {
                                isLoadingCostCenter(false);
                                toastr.error("Error: Failed to Load Cost Centers Data." + response);
                            }
                        });
                    },
                    //Do Before Save
                    doBeforeSave = function() {
                        var flag = true;
                        if (!selectedCostCenter().isValid()) {
                            selectedCostCenter().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    //Save Cost Center
                    saveCostCenter = function (callback) {
                        errorList.removeAll();
                        if (doBeforeSave()) {
                            
                            if (selectedCostCenter().costCentreId() > 0) {
                                saveEdittedCostCenter(callback);
                            } else {
                                saveNewCostCenter(callback);
                            }
                        }
                    },
                    //Save NEW Cost Center
                    saveNewCostCenter = function (callback) {
                        dataservice.saveNewCostCenter(model.costCenterServerMapper(selectedCostCenter()), {
                            success: function(data) {
                                selectedCostCenter().costCentreId(data.CostCentreId);
                                costCentersList.splice(0, 0, selectedCostCenter());
                                selectedCostCenter().reset();
                                getCostCenters();
                                toastr.success("Successfully saved.");
                            },
                            error: function(response) {
                                toastr.error("Failed to save." + response);
                            }
                        });
                    },
                    //Save EDIT Cost Center
                    saveEdittedCostCenter = function (callback) {
                        dataservice.saveCostCenter(model.costCenterServerMapper(selectedCostCenter()), {
                            success: function (data) {
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                                selectedCostCenter().reset();
                                getCostCenters();
                                toastr.success("Successfully saved.");
                            },
                            error: function (exceptionMessage, exceptionType) {

                                if (exceptionType === ist.exceptionType.MPCGeneralException) {

                                    toastr.error(exceptionMessage);

                                } else {

                                    toastr.error("Failed to save.");

                                }

                            }
                        });
                    },
                    createCostCenter = function () {
                        errorList.removeAll();
                        var cc = new model.CostCenter();
                        setDataForNewCostCenter(cc);
                        selectedCostCenter(cc);
                        getCostCentersBaseData();
                        showCostCenterDetail();
                        sharedNavigationVM.initialize(selectedCostCenter, function (saveCallback) { saveCostCenter(saveCallback); });
                    },
                    setDataForNewCostCenter = function (newcostcenter) {
                        newcostcenter.costPerUnitQuantity('0');
                        newcostcenter.unitQuantity('0');
                        newcostcenter.name('New Cost Center');
                        newcostcenter.pricePerUnitQuantity('0');
                        newcostcenter.setupCost('0');
                        newcostcenter.setupSpoilage('0');
                        newcostcenter.setupTime('0');
                        newcostcenter.minimumCost('0');
                        newcostcenter.timePerUnitQuantity('0');
                        newcostcenter.runningSpoilage('0');
                        newcostcenter.priority('0');
                        newcostcenter.isDirectCost('1');
                        newcostcenter.isPrintOnJobCard('1');
                        newcostcenter.isDisabled('0');
                        newcostcenter.isScheduleable('1');
                        newcostcenter.sequence('1');
                       // newcostcenter.creationDate(moment().toDate().format(ist.utcFormat) + 'Z');
                        newcostcenter.costDefaultValue('0');
                        newcostcenter.priceDefaultValue('0');
                        newcostcenter.quantitySourceType('1');
                        newcostcenter.calculationMethodType('2');
                    },
                    
                    //On Edit Click Of Cost Center
                    onEditItem = function (oCostCenter) {
                        errorList.removeAll();
                        getCostCentersBaseData();
                        dataservice.getCostCentreById({
                            id: oCostCenter.costCenterId(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedCostCenter(model.costCenterClientMapper(data));
                                    selectedCostCenter().reset();
                                    showCostCenterDetail();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Detail . Error: ");
                            }
                        });
                    },
                    openEditDialog = function() {
                        view.showCostCenterDialog();
                    },
                    closeEditDialog = function () {
                        if (selectedCostCenter() != undefined) {
                            if (selectedCostCenter().costCenterId() > 0) {
                                isEditorVisible(false);
                                view.hideCostCenterDialog();
                            } else {
                                isEditorVisible(false);
                                view.hideCostCenterDialog();
                                costCentersList.remove(selectedCostCenter());
                            }
                            editorViewModel.revertItem();
                        }
                    },
                    // close CostCenter Editor
                    closeCostCenterDetail = function () {
                        isEditorVisible(false);
                    },
                    // Show CostCenter Editor
                    showCostCenterDetail = function () {
                        isEditorVisible(true);
                    },
                    // Get Base
                    getCostCentersBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                //costCenter Calculation Types
                                costCenterTypes.removeAll();
                                ko.utils.arrayPushAll(costCenterTypes(), data.CalculationTypes);
                                costCenterTypes.valueHasMutated();
                                //Cost Center Categories
                                costCenterCategories.removeAll();
                                ko.utils.arrayPushAll(costCenterCategories(), data.CostCenterCategories);
                                costCenterCategories.valueHasMutated();
                                //Nominal Codes
                                nominalCodes.removeAll();
                                ko.utils.arrayPushAll(nominalCodes(), data.NominalCodes);
                                nominalCodes.valueHasMutated();
                                //Markups
                                markups.removeAll();
                                ko.utils.arrayPushAll(markups(), data.Markups);
                                markups.valueHasMutated();
                                //Variables
                                costCenterVariables.removeAll();
                                ko.utils.arrayPushAll(costCenterVariables(), data.CostCentreVariables);
                                costCenterVariables.valueHasMutated();
                                //
                                //Cost Center Resources
                                costCenterResources.removeAll();
                                ko.utils.arrayPushAll(costCenterResources(), data.CostCenterResources);
                                costCenterResources.valueHasMutated();
                                //Delivery Carriers
                                deliveryCarriers.removeAll();
                                ko.utils.arrayPushAll(deliveryCarriers(), data.DeliveryCarriers);
                                deliveryCarriers.valueHasMutated();
                            },
                            error: function () {
                                toastr.error("Failed to base data.");
                            }
                        });
                    },
                    updateSelectedResources = function () {
                        _.each(selectedCostCenter().costCenterResource(), function (resource) {
                            var selectedResource;
                            selectedResource = _.find(costCenterResources(), function (resourceItem) {
                                return resourceItem.costCentreId() === resource.costCentreId();
                            });
                            selectedCostCenter.isSelected(true);
                        });
                    },
                    // #region Observables
                    // Initialize the view model
                    initialize = function(specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        pager(pagination.Pagination({ PageSize: 10 }, costCentersList, getCostCenters));
                        getCostCenters();
                       // getCostCentersBaseData();
                    };
                    
                return {
                    // Observables
                    costCentersList: costCentersList,
                    selectedCostCenter: selectedCostCenter,
                    isLoadingCostCenter: isLoadingCostCenter,
                    deleteCostCenter: deleteCostCenter,
                    onDeleteCostCenter: onDeleteCostCenter,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    getCostCenters: getCostCenters,
                    doBeforeSave: doBeforeSave,
                    saveCostCenter: saveCostCenter,
                    saveNewCostCenter: saveNewCostCenter,
                    saveEdittedCostCenter: saveEdittedCostCenter,
                    openEditDialog: openEditDialog,
                    closeEditDialog: closeEditDialog,
                    searchFilter: searchFilter,
                    onEditItem: onEditItem,
                    initialize: initialize,
                    isEditorVisible: isEditorVisible,
                    closeCostCenterDetail: closeCostCenterDetail,
                    showCostCenterDetail: showCostCenterDetail,
                    getCostCentersBaseData: getCostCentersBaseData,
                    costCenterTypes: costCenterTypes,
                    costCenterCategories: costCenterCategories,
                    nominalCodes: nominalCodes,
                    markups: markups,
                    costCenterResources: costCenterResources,
                    costCenterVariables: costCenterVariables,
                    deliveryCarriers: deliveryCarriers,
                    variablesTreePrent: variablesTreePrent,
                    createCostCenter: createCostCenter,
                    setDataForNewCostCenter: setDataForNewCostCenter
                };
            })()
        };
        return ist.costcenter.viewModel;
    });
