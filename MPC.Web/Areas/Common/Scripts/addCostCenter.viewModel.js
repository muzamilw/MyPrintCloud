/*
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
                    // Company Id
                    selectedCompanyId = ko.observable(),
                    // Curency 
                    currencySmb = ko.observable(),
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
                    //Is Opened from Section Detail
                    isOpenedFromSectionDetail = ko.observable(false),
                    currentSection = ko.observable(),
                    // Item Id - To be passed to Execution Api
                    itemId = ko.observable(),
                    // Pagination For Press Dialog
                    costCentreDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, costCentres)),
                    // Cost Center Type
                    costCenterTypeFilter = ko.observable(),
                    // Search Stock Items
                    searchCostCenters = function () {
                        costCentreDialogPager().reset();
                        if (isCostCenterDialogForShipping()) {
                            getCostCenters();
                        } else {
                            getCostCentersForProduct();
                        }

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
                    // After Cost Center Execution
                    afterCostCenterExecution = null,
                    companyTaxRate = null,
                    // Reset Stock Dialog Filters
                    resetDialogFilters = function () {
                        // Reset Text 
                        costCenterDialogFilter(undefined);
                    },
                    // Show
                    show = function (afterAddCostCenterCallback, companyId, isCostCenterDialogForShippingFlag, currency, companyTaxRateParam, costCenterType,
                    isOpenedFromSection, productId, section) {
                        currencySmb(currency);
                        isAddProductForSectionCostCenter(false);
                        isAddProductFromInventory(false);
                        isDisplayCostCenterQuantityDialog(false);
                        isCostCenterDialogForShipping(isCostCenterDialogForShippingFlag);
                        resetCostCenters();
                        view.showDialog();
                        afterAddCostCenter = afterAddCostCenterCallback;
                        afterCostCenterExecution = null;
                        companyTaxRate = companyTaxRateParam;
                        selectedCompanyId(companyId);
                        costCenterTypeFilter(costCenterType || undefined);
                        isOpenedFromSectionDetail(isOpenedFromSection || false);
                        currentSection(section);
                        itemId(productId || "");
                        if (isCostCenterDialogForShipping()) {
                            getCostCenters();
                        } else {
                            getCostCentersForProduct();
                        }
                    },
                    // On Select Cost Center
                    onSelectCostCenter = function (costCenter) {
                        selectedCostCentre(costCenter);
                        if (afterAddCostCenter && typeof afterAddCostCenter === "function") {
                            afterAddCostCenter(selectedCostCentre());
                        }
                        if (isCostCenterDialogForShipping()) {
                            hideCostCentreDialog();
                        }
                    },
                    // #region Cost Centre Execution
                    isQueueExist = false,
                    questionQueueObject = null,
                    inputQueueObject = null,
                    workInstructions = null,
                    addOnCostCenters = ko.observable(null),
                    onCostCenterQtyChange = function(callback, currentCostCenter) {
                        afterCostCenterExecution = callback;
                        selectedCostCentre(currentCostCenter);
                        executeCostCenter(afterCostCenterExecution);
                    },
                    updateCostCenter = function() {
                        var jsonObjectsOfGlobalQueue = null;
                        var inputAndQuestionQueues;
                        var costCentreQueueItems;
                        if (!costCentreQueueItems) {
                            inputAndQuestionQueues = {
                                QuestionQueues: globalQuestionQueueItemsList,
                                InputQueues: globalInputQueueItemsList
                            };
                            jsonObjectsOfGlobalQueue = JSON.stringify(inputAndQuestionQueues, null, 2);
                            costCentreQueueItems = jsonObjectsOfGlobalQueue;

                        }
                    },
                    // Execute Cost Center
                    executeCostCenter = function (afterExecutionCallback) {
                        afterCostCenterExecution = afterExecutionCallback;
                        if (selectedCostCentre().quantity1() === undefined || selectedCostCentre().quantity1() === null) {
                            toastr.info("Please select quantity!");
                            return;
                        }
                        if (selectedCostCentre().sectionId() == undefined)
                            selectedCostCentre().sectionId(0);
                        var callMode = selectedCostCentre().callMode();
                        if (callMode == undefined)
                            callMode = "New";
                        
                        dataservice.executeCostCenterForCostCenter({
                            CostCentreId: selectedCostCentre().id(),
                            QuantityOrdered: selectedCostCentre().quantity1(),
                            ClonedItemId: itemId(),
                            CallMode: callMode,
                            Qty2: selectedCostCentre().quantity2(),
                            Qty3: selectedCostCentre().quantity3(),
                            SectionId: selectedCostCentre().sectionId()
                        }, {
                            success: function (data) {
                                questionQueueObject = data[2];
                                inputQueueObject = data[7];
                                workInstructions = data[3][0].WorkInstructions;
                                var costCenterExecutedCallback = isOpenedFromSectionDetail() ? addCostCenter : afterCostCenterExecution;
                                var selectedElement = $(selectedCostCentre().isSelected.domElement).find("td")[0];
                                if (selectedCostCentre().calculationMethodType() === 4) { // cost centres of calculation methode type 4 are formula based
                                    if (questionQueueObject != null) { // process the question queue and prompt for values
                                        //if (questionQueueObject.length > 0) {
                                            isQueueExist = true;
                                            
                                            ShowCostCentrePopup(questionQueueObject, selectedCostCentre().id(), 0, selectedElement, "New", currencySmb(),
                                                0, inputQueueObject.Items, selectedCostCentre().calculationMethodType(), companyTaxRate, workInstructions,
                                                selectedCostCentre().quantity1(), addOnCostCenters, selectedCostCentre, costCenterExecutedCallback, selectedCostCentre().quantity2(), selectedCostCentre().quantity3(), selectedCostCentre().sectionId());
                                        //}
                                        if (inputQueueObject.Items && inputQueueObject.Items.length === 3) { // do not process the queue for prompting values
                                            isQueueExist = true;
                                        }
                                    }
                                } else if (selectedCostCentre().calculationMethodType() === 3) { // if method type is not 4 then it will be 3 : per quantity or 4: per hour
                                    if (selectedCostCentre().costCentreQuantitySourceType() === 1) { // do not process the queue for prompting values else execute it as it is of variable type
                                        isQueueExist = true;
                                        SetGlobalCostCentreQueue(questionQueueObject, inputQueueObject.Items, selectedCostCentre().id(), selectedCostCentre().calculationMethodType(),
                                            "", selectedElement, "", 0, currencySmb(), false, companyTaxRate, selectedCostCentre().quantity1(),
                                            addOnCostCenters, selectedCostCentre, costCenterExecutedCallback, false, selectedCostCentre().quantity2(), selectedCostCentre().quantity3(), selectedCostCentre().sectionId(), "", currentSection());
                                    } else { // process the input queue and prompt for values
                                        isQueueExist = true;
                                        ShowInputCostCentrePopup(inputQueueObject.Items, selectedCostCentre().id(), 0, selectedElement, "New", currencySmb(),
                                            0, questionQueueObject, selectedCostCentre().calculationMethodType(), companyTaxRate, workInstructions,
                                            selectedCostCentre().quantity1(), addOnCostCenters, selectedCostCentre, costCenterExecutedCallback, selectedCostCentre().quantity2(), selectedCostCentre().quantity3(), selectedCostCentre().sectionId());
                                    }
                                } else if (selectedCostCentre().calculationMethodType() === 2) { // if method type is not 4 then it will be 3 : per quantity or 4: per hour

                                    if (selectedCostCentre().costCentreTimeSourceType() === 1) { // do not process the queue for prompting values else execute it as it is of variable type
                                        isQueueExist = true;
                                        SetGlobalCostCentreQueue(questionQueueObject, inputQueueObject.Items, selectedCostCentre().id(), selectedCostCentre().calculationMethodType(),
                                            "", selectedCostCentre().isSelected.domElement, "", 0, currencySmb(),
                                            false, companyTaxRate, selectedCostCentre().quantity1(),
                                            addOnCostCenters, selectedCostCentre, costCenterExecutedCallback, false, selectedCostCentre().quantity2(), selectedCostCentre().quantity3(), selectedCostCentre().sectionId(), "", currentSection());
                                    } else { // process the input queue and prompt for values
                                        isQueueExist = true;
                                        ShowInputCostCentrePopup(inputQueueObject.Items, selectedCostCentre().id(), 0,
                                            selectedElement, "New", currencySmb(),
                                            0, questionQueueObject, selectedCostCentre().calculationMethodType(), companyTaxRate, workInstructions,
                                            selectedCostCentre().quantity1(), addOnCostCenters, selectedCostCentre, costCenterExecutedCallback, selectedCostCentre().quantity2(), selectedCostCentre().quantity3(), selectedCostCentre().sectionId());
                                    }
                                }
                                //if (isQueueExist === false) {// queue is not populating
                                //    toastr.error("Queue is not populating.");
                                //}
                            },
                            error: function (response) {
                                toastr.error("Failed to execute cost center. Error: " + response);
                            }
                        });
                    },
                    // #endregion Cost Centre Execution
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        costCentreDialogPager(new pagination.Pagination({ PageSize: 5 }, costCentres, getCostCentersForProduct));
                    },
                    onSaveProductCostCenter = function () {
                        if ($root.isCostCenterDialogForShipping()) {
                            addCostCenter();
                            return;
                        }
                        executeCostCenter();
                        hideCostCentreQuantityDialog();
                    },
                    // add Cost Center
                    addCostCenter = function() {
                        if (afterCostCenterExecution && typeof afterCostCenterExecution === "function") {
                            afterCostCenterExecution(selectedCostCentre());
                        }
                        hideCostCentreDialog();
                        hideCostCentreQuantityDialog();
                    },
                    hideCostCentreDialog = function () {
                        view.hideDialog();
                    },
                    hideCostCentreQuantityDialog = function () {
                        view.hideCostCentersQuantityDialog();
                    },
                    getCostCenters = function () {
                        dataservice.getCostCenters({
                            CompanyId: selectedCompanyId(),
                            SearchString: costCenterDialogFilter(),
                            PageSize: costCentreDialogPager().pageSize(),
                            PageNo: costCentreDialogPager().currentPage(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    costCentres.removeAll();
                                    _.each(data.CostCentres, function (item) {
                                        item.CompanyTaxRate = companyTaxRate;
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
                    },
                    //Hide
                    hide = function () {
                        view.hideDialog();
                    },
                    createBlankCostCenter = function() {
                        return model.CostCentre.Create({});
                    },
                    // Get Cost Centers
                    getCostCentersForProduct = function () {
                        dataservice.getCostCentersForProduct({
                            CompanyId: selectedCompanyId(),
                            SearchString: costCenterDialogFilter(),
                            PageSize: costCentreDialogPager().pageSize(),
                            PageNo: costCentreDialogPager().currentPage(),
                            Type: costCenterTypeFilter()
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    costCentres.removeAll();
                                    _.each(data.CostCentres, function (item) {
                                        item.CompanyTaxRate = companyTaxRate;
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
                    selectedCostCentre: selectedCostCentre,
                    hide: hide,
                    currencySmb: currencySmb,
                    addCostCenter: addCostCenter,
                    executeCostCenter: executeCostCenter,
                    createBlankCostCenter: createBlankCostCenter,
                    onCostCenterQtyChange: onCostCenterQtyChange
                };
            })()
        };

        return ist.addCostCenter.viewModel;
    });

