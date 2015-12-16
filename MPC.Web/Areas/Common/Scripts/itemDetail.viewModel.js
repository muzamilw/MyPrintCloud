/*
    Module with the view model for the item Detail.
*/
define("common/itemDetail.viewModel",
    ["jquery", "amplify", "ko", "common/itemDetail.dataservice", "common/itemDetail.model", "common/confirmation.viewModel", "common/pagination"
        , "common/sharedNavigation.viewModel", "common/stockItem.viewModel", "common/addCostCenter.viewModel", "common/phraseLibrary.viewModel", "common/reportManager.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVM, stockDialog, addCostCenterVm, phraseLibrary, reportManager) {
        var ist = window.ist || {};
        ist.itemDetail = {
            viewModel: (function () {
                var //View
                    view,
                    //#region Observables
                    showItemDetailsSection = ko.observable(false),

                    showSectionDetail = ko.observable(false),
                    selectedProduct = ko.observable(model.Item.Create({})),
                    selectedAttachment = ko.observable(model.ItemAttachment.Create({})),
                    // Best PressL ist
                    bestPressList = ko.observableArray([]),
                    // User Cost Center List For Run Wizard
                    userCostCenters = ko.observableArray([]),
                    // User Cost Center Copy List For Run Wizard | for search purpose
                    userCostCentersCopy = ko.observableArray([]),
                    //selected Best Press From Wizard
                    selectedBestPressFromWizard = ko.observable(),
                    // For seachring
                    searchString = ko.observable(),
                    // Errors List
                    errorList = ko.observableArray([]),
                    // Selected Section
                    selectedSection = ko.observable(),
                    // Selected Job Description
                    selectedJobDescription = ko.observable(),
                    //Inks
                    inks = ko.observableArray([]),
                    // Ink Coverage Group
                    inkCoverageGroup = ko.observableArray([]),
                    // paper sizes Methods
                    paperSizes = ko.observableArray([]),
                    // Section Paper Sizes - Sort Largest Area to lowest
                    sectionPaperSizes = ko.observableArray([]),
                    // Ink Plate Sides Methods
                    inkPlateSides = ko.observableArray([]),
                    // Markups
                    markups = ko.observableArray([]),
                    // System Users
                    systemUsers = ko.observableArray([]),
                    // Press list
                    presses = ko.observableArray([]),
                    prePressOrPostPress = ko.observable(),
                
                    // Impression Coverages
                    impressionCoverages = ko.observableArray([
                        {
                            Name: "High",
                            Id: 1
                        },
                        {
                            Name: "Medium",
                            Id: 2
                        },
                        {
                            Name: "Low",
                            Id: 3
                        }
                    ]),
                    // Job Statuses
                    jobStatuses = ko.observableArray([
                        {
                            StatusId: 11,
                            StatusName: "Need Assigning"
                        },
                        {
                            StatusId: 12,
                            StatusName: "In Studio"
                        },
                        {
                            StatusId: 13,
                            StatusName: "In Print/Press"
                        },
                        {
                            StatusId: 14,
                            StatusName: "In Post Press/Bindery"
                        },
                        {
                            StatusId: 15,
                            StatusName: "Ready for Shipping"
                        },
                        {
                            StatusId: 16,
                            StatusName: "Shipped, Not Invoiced"
                        },
                        {
                            StatusId: 17,
                            StatusName: "Not Progressed to Job"
                        }
                    ]),
                    // Stock Category 
                    stockCategory = {
                        paper: 1,
                        inks: 2,
                        films: 3,
                        plates: 4
                    },
                    // Cost Center Type
                    costCenterType = {
                        prePress: 2,
                        postPress: 3
                    },
                    itemPlan = ko.observable(),
                    side1Image = ko.observable(),
                    side2Image = ko.observable(),
                    showSide1Image = ko.observable(true),
                    // Base Charge 1 Total
                    baseCharge1Total = ko.observable(0),
                    // Base Charge 2 Total
                    baseCharge2Total = ko.observable(0),
                    // Base Charge 3 Total
                    baseCharge3Total = ko.observable(0),
                    // Is Calculating Ptv
                    isPtvCalculationInProgress = ko.observable(false),
                    // Stock Item To Create For Stock Cost Center
                    stockItemToCreate = ko.observable(),
                    //Is Inventory Dialog is opening from Order Dialog's add Product From Inventory
                    isAddProductFromInventory = ko.observable(false),
                    //Is Inventory Dialog is opening for Section Cost Center
                    isAddProductForSectionCostCenter = ko.observable(false),
                    selectedCostCentre = ko.observable(),
                    selectedSectionCostCenter = ko.observable(),
                    selectedQty = ko.observable(1),
                    selectedQtyForItem = ko.observable(),
                    selectedOrder = ko.observable(),
                    currencySymbol = ko.observable(''),
                    lengthUnit = ko.observable(),
                    weightUnit = ko.observable(),
                    loggedInUser = ko.observable(),
                    closeItemDetailSection = null,
                    saveOrderFromSection = null,
                    saveFrom = ko.observable(),
                    //Is Estimate Screen
                    isEstimateScreen = ko.observable(false),
                    // Default MarkUp Id
                    defaultMarkUpId = ko.observable(),
                    //#endregion  
                    isSectionCostCenterDialogOpen = ko.observable(false),
                    isSectionVisible = ko.observable(false),
                    // Is Side 1 Ink button clicked
                    isSide1InkButtonClicked = ko.observable(false),
                    // Default Section - From the Item Added from Retail Store
                    defaultSection = ko.observable(),
                    // defaultPhraseField
                    defaultPhraseField = ko.observable(),
                    //#region Utility Functions
                    // Update Section Cost Center Qty3 Net total on Qty3 Markup change
                    updateSectionCostCenterQty3NetTotalOnQty3MarkupChange = function(markupValue) {
                        selectedSectionCostCenter().qty3MarkUpValue(markupValue);
                        var total = parseFloat(selectedSectionCostCenter().qty3Charge()) + (selectedSectionCostCenter().qty3Charge() * (markupValue / 100));
                        selectedSectionCostCenter().qty3NetTotal(total);
                    },
                    // Update Section Cost Center Qty2 Net total on Qty2 Markup change
                    updateSectionCostCenterQty2NetTotalOnQty2MarkupChange = function(markupValue) {
                        selectedSectionCostCenter().qty2MarkUpValue(markupValue);
                        var total = parseFloat(selectedSectionCostCenter().qty2Charge()) + (selectedSectionCostCenter().qty2Charge() * (markupValue / 100));
                        selectedSectionCostCenter().qty2NetTotal(total);
                    },
                    // Update Section Cost Center Qty1 Net total on Qty1 Markup change
                    updateSectionCostCenterQty1NetTotalOnQty1MarkupChange = function(markupValue) {
                        selectedSectionCostCenter().qty1MarkUpValue(markupValue);
                        var total = parseFloat(selectedSectionCostCenter().qty1Charge()) + (selectedSectionCostCenter().qty1Charge() * (markupValue / 100));
                        selectedSectionCostCenter().qty1NetTotal(total);
                    },
                    sectionCostCenterQty1Charge = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty1Charge() || 0;
                        },
                        write: function(value) {
                            if (!selectedSectionCostCenter() || value === selectedSectionCostCenter().qty1Charge()) {
                                return;
                            }
                            selectedSectionCostCenter().qty1Charge(value || 0);
                            var markupValue = 0;
                            if (selectedQty() == 1) {
                                if (!selectedSectionCostCenter().qty1MarkUpId()) {
                                    updateSectionCostCenterQty1NetTotalOnQty1MarkupChange(markupValue);
                                } else {
                                    _.each(markups(), function(markup) {
                                        if (markup.MarkUpId == selectedSectionCostCenter().qty1MarkUpId()) {
                                            markupValue = markup.MarkUpRate || 0;
                                            updateSectionCostCenterQty1NetTotalOnQty1MarkupChange(markupValue);
                                        }
                                    });
                                }
                            }
                            calculateSectionBaseCharge1();
                        }
                    }),
                    sectionCostCenterQty2Charge = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty2Charge() || 0;
                        },
                        write: function(value) {
                            if (!selectedSectionCostCenter() || value === selectedSectionCostCenter().qty2Charge()) {
                                return;
                            }
                            selectedSectionCostCenter().qty2Charge(value || 0);
                            var markupValue = 0;
                            if (selectedQty() == 2) {
                                if (!selectedSectionCostCenter().qty2MarkUpId()) {
                                    updateSectionCostCenterQty2NetTotalOnQty2MarkupChange(markupValue);
                                } else {
                                    _.each(markups(), function(markup) {
                                        if (markup.MarkUpId == selectedSectionCostCenter().qty2MarkUpId()) {
                                            markupValue = markup.MarkUpRate || 0;
                                            updateSectionCostCenterQty2NetTotalOnQty2MarkupChange(markupValue);
                                        }
                                    });
                                }
                            }
                            calculateSectionBaseCharge2();
                        }
                    }),
                    sectionCostCenterQty3Charge = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty3Charge() || 0;
                        },
                        write: function(value) {
                            if (!selectedSectionCostCenter() || value === selectedSectionCostCenter().qty3Charge()) {
                                return;
                            }
                            selectedSectionCostCenter().qty3Charge(value || 0);
                            var markupValue = 0;
                            if (selectedQty() == 3) {
                                if (!selectedSectionCostCenter().qty3MarkUpId()) {
                                    updateSectionCostCenterQty3NetTotalOnQty3MarkupChange(markupValue);
                                } else {
                                    _.each(markups(), function(markup) {
                                        if (markup.MarkUpId == selectedSectionCostCenter().qty3MarkUpId()) {
                                            markupValue = markup.MarkUpRate || 0;
                                            updateSectionCostCenterQty3NetTotalOnQty3MarkupChange(markupValue);
                                        }
                                    });
                                }

                            }
                            calculateSectionBaseCharge3();
                        }
                    }),
                    sectionCostCenterQty3MarkUpId = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty3MarkUpId();
                        },
                        write: function(value) {
                            if (!selectedSectionCostCenter() ||
                                (selectedSectionCostCenter().qty3MarkUpId() !== undefined && value === selectedSectionCostCenter().qty3MarkUpId())) {
                                return;
                            }
                            selectedSectionCostCenter().qty3MarkUpId(value);
                            var markupValue = 0;
                            if (selectedQty() == 3) {
                                if (!selectedSectionCostCenter().qty3MarkUpId()) {
                                    updateSectionCostCenterQty3NetTotalOnQty3MarkupChange(0);
                                } else {
                                    _.each(markups(), function(markup) {
                                        if (markup.MarkUpId == selectedSectionCostCenter().qty3MarkUpId()) {
                                            markupValue = markup.MarkUpRate;
                                            updateSectionCostCenterQty3NetTotalOnQty3MarkupChange(markupValue);
                                        }
                                    });
                                }

                            }
                            calculateSectionBaseCharge3();
                        }
                    }),
                    sectionCostCenterQty2MarkUpId = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty2MarkUpId();
                        },
                        write: function(value) {
                            if (!selectedSectionCostCenter() ||
                                (selectedSectionCostCenter().qty2MarkUpId() !== undefined && value === selectedSectionCostCenter().qty2MarkUpId())) {
                                return;
                            }
                            selectedSectionCostCenter().qty2MarkUpId(value);
                            var markupValue = 0;
                            if (selectedQty() == 2) {
                                if (!selectedSectionCostCenter().qty2MarkUpId()) {
                                    updateSectionCostCenterQty2NetTotalOnQty2MarkupChange(0);
                                } else {
                                    _.each(markups(), function(markup) {
                                        if (markup.MarkUpId == selectedSectionCostCenter().qty2MarkUpId()) {
                                            markupValue = markup.MarkUpRate;
                                            updateSectionCostCenterQty2NetTotalOnQty2MarkupChange(markupValue);
                                        }
                                    });
                                }
                            }
                            calculateSectionBaseCharge2();
                        }
                    }),
                    sectionCostCenterQty1MarkUpId = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty1MarkUpId();
                        },
                        write: function(value) {
                            if (!selectedSectionCostCenter() ||
                                (selectedSectionCostCenter().qty1MarkUpId() !== undefined && value === selectedSectionCostCenter().qty1MarkUpId())) {
                                return;
                            }
                            selectedSectionCostCenter().qty1MarkUpId(value);
                            var markupValue;
                            if (selectedQty() == 1) {
                                if (!selectedSectionCostCenter().qty1MarkUpId()) {
                                    updateSectionCostCenterQty1NetTotalOnQty1MarkupChange(0);
                                } else {
                                    _.each(markups(), function(markup) {
                                        if (markup.MarkUpId == selectedSectionCostCenter().qty1MarkUpId()) {
                                            markupValue = markup.MarkUpRate || 0;
                                            updateSectionCostCenterQty1NetTotalOnQty1MarkupChange(markupValue);
                                        }
                                    });
                                }
                            }
                            calculateSectionBaseCharge1();
                        }
                    }),
                    applySectionCostCenterMarkup = function() {
                        var markupValue;
                        _.each(markups(), function(markup) {

                            if (markup.MarkUpId == selectedSectionCostCenter().qty1MarkUpId()) {
                                markupValue = markup.MarkUpRate || 0;
                                updateSectionCostCenterQty1NetTotalOnQty1MarkupChange(markupValue);
                            }
                            if (markup.MarkUpId == selectedSectionCostCenter().qty2MarkUpId()) {
                                markupValue = markup.MarkUpRate || 0;
                                updateSectionCostCenterQty2NetTotalOnQty2MarkupChange(markupValue);
                            }

                            if (markup.MarkUpId == selectedSectionCostCenter().qty3MarkUpId()) {
                                markupValue = markup.MarkUpRate || 0;
                                updateSectionCostCenterQty3NetTotalOnQty3MarkupChange(markupValue);
                            }

                        });

                        calculateSectionBaseCharge1();
                        calculateSectionBaseCharge2();
                        calculateSectionBaseCharge3();
                    },
                    sectionCostCenterQty1NetTotal = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty1NetTotal() || 0;
                        },
                        write: function(value) {
                            if (!value || value === selectedSectionCostCenter().qty1NetTotal()) {
                                return;
                            }
                            selectedSectionCostCenter().qty1NetTotal(value);
                            calculateSectionBaseCharge1();
                        }
                    }),
                    sectionCostCenterQty2NetTotal = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty2NetTotal() || 0;
                        },
                        write: function(value) {
                            if (!value || value === selectedSectionCostCenter().qty2NetTotal()) {
                                return;
                            }
                            selectedSectionCostCenter().qty2NetTotal(value);
                            calculateSectionBaseCharge2();
                        }
                    }),
                    sectionCostCenterQty3NetTotal = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty3NetTotal() || 0;
                        },
                        write: function(value) {
                            if (!value || value === selectedSectionCostCenter().qty3NetTotal()) {
                                return;
                            }
                            selectedSectionCostCenter().qty3NetTotal(value);
                            calculateSectionBaseCharge3();
                        }
                    }),
                    sectionVisibilityHandler = function() {
                        isSectionVisible(!isSectionVisible());
                    },
                    onSaveStockitemForSectionCostCenter = function() {
                        var sectionCostCenter = model.SectionCostCentre.Create({ ItemSectionId: selectedSection().id() });
                        selectedSectionCostCenter(sectionCostCenter);
                        selectedQty(1);

                        sectionCostCenter.name(stockItemToCreate().name);
                        sectionCostCenter.costCentreType('139');
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.qty1(selectedCostCentre().quantity1());
                        sectionCostCenter.qty2(selectedCostCentre().quantity2());
                        sectionCostCenter.qty3(selectedCostCentre().quantity3());
                        setWorkInstructionsForStockCostCenter(sectionCostCenter);
                        // sectionCostCenter.qty3Charge(0);
                        view.hideCostCentersQuantityDialog();

                        var sectionCostCenterDetail = model.SectionCostCenterDetail.Create({ SectionCostCentreId: selectedSectionCostCenter().id() });
                        sectionCostCenterDetail.stockName(stockItemToCreate().name);
                        sectionCostCenterDetail.stockId(stockItemToCreate().id);
                        sectionCostCenterDetail.costPrice(stockItemToCreate().actualprice);
                        sectionCostCenterDetail.qty1(selectedCostCentre().quantity1());
                        sectionCostCenterDetail.qty2(selectedCostCentre().quantity2());
                        sectionCostCenterDetail.qty3(selectedCostCentre().quantity3());
                        sectionCostCenter.sectionCostCentreDetails.splice(0, 0, sectionCostCenterDetail);
                        updateSectionCostCenter(sectionCostCenterDetail);
                        selectedSection().sectionCostCentres.splice(0, 0, sectionCostCenter);
                        sectionCostCenterQty2MarkUpId(defaultMarkUpId());
                        sectionCostCenterQty1MarkUpId(defaultMarkUpId());
                        sectionCostCenterQty3MarkUpId(defaultMarkUpId());

                        calculateSectionBaseCharge1();
                        calculateSectionBaseCharge2();
                        calculateSectionBaseCharge3();
                    },
                    //Update Section Cost Center
                    updateSectionCostCenter = function(sectionCostCenterDetail) {
                        var newCost = (sectionCostCenterDetail.costPrice() * sectionCostCenterDetail.qty1()); //selectedSectionCostCenter().qty1Charge() + 
                        selectedSectionCostCenter().qty1Charge(newCost);
                        selectedSectionCostCenter().qty1NetTotal(newCost);
                        if (isEstimateScreen()) {
                            //selectedQty(1);
                            var newCost2 = (sectionCostCenterDetail.costPrice() * sectionCostCenterDetail.qty2()); // selectedSectionCostCenter().qty2Charge() + 
                            selectedSectionCostCenter().qty2Charge(newCost2);
                            selectedSectionCostCenter().qty2NetTotal(newCost2);
                            var newCost3 = (sectionCostCenterDetail.costPrice() * sectionCostCenterDetail.qty3()); // selectedSectionCostCenter().qty2Charge() + 
                            selectedSectionCostCenter().qty3Charge(newCost3);
                            selectedSectionCostCenter().qty3NetTotal(newCost3);
                        }
                    },
                    // ReSharper disable once UnusedLocals
                    sectionCostCenterQty1Ui = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter() || !selectedSectionCostCenter().qty1()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty1();
                        },
                        write: function(value) {
                            if (selectedSectionCostCenter() != undefined) {
                                selectedSectionCostCenter().qty1(!value ? 0 : value);
                                if (selectedSectionCostCenter().sectionCostCentreDetails().length > 0) {
                                    var newCost = (selectedSectionCostCenter().sectionCostCentreDetails()[0].costPrice() * selectedSectionCostCenter().qty1()); //selectedSectionCostCenter().qty1Charge() + 
                                    selectedSectionCostCenter().qty1Charge(newCost);
                                    selectedSectionCostCenter().qty1NetTotal(newCost);
                                    var name = selectedSectionCostCenter().sectionCostCentreDetails()[0].stockName();
                                    updateWorkInstruction(selectedSectionCostCenter(), name, selectedSectionCostCenter().qty1(), selectedSectionCostCenter().qty2());
                                }
                            }
                        }
                    }),
                    sectionCostCenterQty2Ui = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter() || !selectedSectionCostCenter().qty2()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty2();
                        },
                        write: function(value) {
                            if (selectedSectionCostCenter() != undefined) {
                                selectedSectionCostCenter().qty2(!value ? 0 : value);
                                if (selectedSectionCostCenter().sectionCostCentreDetails().length > 0) {
                                    var newCost = (selectedSectionCostCenter().sectionCostCentreDetails()[0].costPrice() * selectedSectionCostCenter().qty2()); //selectedSectionCostCenter().qty2Charge() + 
                                    selectedSectionCostCenter().qty2Charge(newCost);
                                    selectedSectionCostCenter().qty2NetTotal(newCost);
                                    var name = selectedSectionCostCenter().sectionCostCentreDetails()[0].stockName();
                                    updateWorkInstruction(selectedSectionCostCenter(), name, selectedSectionCostCenter().qty1(), selectedSectionCostCenter().qty2());
                                }
                            }
                        }
                    }),
                    sectionCostCenterQty3Ui = ko.computed({
                        read: function() {
                            if (!selectedSectionCostCenter() || !selectedSectionCostCenter().qty3()) {
                                return 0;
                            }
                            return selectedSectionCostCenter().qty3();
                        },
                        write: function(value) {
                            if (selectedSectionCostCenter() != undefined) {
                                selectedSectionCostCenter().qty3(!value ? 0 : value);
                                if (selectedSectionCostCenter().sectionCostCentreDetails().length > 0) {
                                    var newCost = (selectedSectionCostCenter().sectionCostCentreDetails()[0].costPrice() * selectedSectionCostCenter().qty3());
                                    selectedSectionCostCenter().qty3Charge(newCost);
                                    selectedSectionCostCenter().qty3NetTotal(newCost);
                                    var name = selectedSectionCostCenter().sectionCostCentreDetails()[0].stockName();
                                    updateWorkInstruction(selectedSectionCostCenter(), name, selectedSectionCostCenter().qty1(), selectedSectionCostCenter().qty2());
                                }
                            }
                        }
                    }),
                    // Set Work Instructions in case of Stock Cost Center
                    setWorkInstructionsForStockCostCenter = function(sectionCostCenter) {
                        if (!isEstimateScreen()) {
                            sectionCostCenter.qty1WorkInstructions(stockItemToCreate().name + " (Quantity = " + selectedCostCentre().quantity1() + ")");
                            sectionCostCenter.qty2WorkInstructions(stockItemToCreate().name + " (Quantity = " + selectedCostCentre().quantity1() + ")");
                            sectionCostCenter.qty3WorkInstructions(stockItemToCreate().name + " (Quantity = " + selectedCostCentre().quantity1() + ")");
                        } else {
                            sectionCostCenter.qty1WorkInstructions(stockItemToCreate().name + " (Quantity = " + selectedCostCentre().quantity1() + ")");
                            sectionCostCenter.qty2WorkInstructions(stockItemToCreate().name + " (Quantity = " + selectedCostCentre().quantity2() + ")");
                            sectionCostCenter.qty3WorkInstructions(stockItemToCreate().name + " (Quantity = " + selectedCostCentre().quantity3() + ")");
                        }
                    },
                    // Set Work Instructions in case of Stock Cost Center
                    updateWorkInstruction = function(sectionCostCenter, name, qty1, qty2) {
                        if (!isEstimateScreen()) {
                            sectionCostCenter.qty1WorkInstructions(name + " (Quantity = " + qty1 + ")");
                            sectionCostCenter.qty2WorkInstructions(name + " (Quantity = " + qty1 + ")");
                            sectionCostCenter.qty3WorkInstructions(name + " (Quantity = " + qty1 + ")");
                        } else {
                            sectionCostCenter.qty1WorkInstructions(name + " (Quantity = " + qty1 + ")");
                            sectionCostCenter.qty2WorkInstructions(name + " (Quantity = " + qty2 + ")");
                        }
                    },
                    // Select Quantity Sequence for Item
                    selectQuantityForItem = function(qtySequence) {
                        if (selectedQtyForItem() === qtySequence) {
                            return;
                        }
                        selectedQtyForItem(qtySequence);
                    },
                    //Show Item Detail
                    showItemDetail = function (selectedProductParam, selectedOrderParam, closeItemDetailParam, isEstimateScreenFlag, saveOrderFromSectionParam, saveFromParam) {
                        selectedQtyForItem(1);
                        showSectionDetail(false);
                        selectedProduct(selectedProductParam);
                        showItemDetailsSection(true);
                        selectedProduct().systemUsers(systemUsers());
                        selectedOrder(selectedOrderParam);
                        selectedSection(selectedProduct().itemSections()[0]);
                        isEstimateScreen(isEstimateScreenFlag);
                        closeItemDetailSection = closeItemDetailParam;
                        saveOrderFromSection = saveOrderFromSectionParam;
                        saveFrom = saveFromParam;
                    },
                    closeItemDetail = function() {
                        showItemDetailsSection(false);
                        closeItemDetailSection();
                        selectedSection(undefined);
                    },
                    // Select Job Description
                    selectJobDescription = function(jobDescription, e) {
                        selectedJobDescription(e.currentTarget.id);
                    },
                    // Open Stock Item Dialog
                    openStockItemDialog = function() {
                        stockDialog.show(function(stockItem) {
                            selectedSection().selectStock(stockItem);
                        }, stockCategory.paper, false, currencySymbol(), selectedOrder().taxRate());
                    },
                    //Section Cost Center Dialog
                    openSectionCostCenterDialog = function(costCenter, qty) {
                        isSectionCostCenterDialogOpen(false);
                        selectedSectionCostCenter(costCenter);
                        selectedQty(qty);
                        view.showSectionCostCenterDialogModel();
                        isSectionCostCenterDialogOpen(true);
                    },
                    getPtvPlan = function() {
                        if (selectedSection().itemSizeHeight() == null || selectedSection().itemSizeWidth() == null || selectedSection().sectionSizeHeight() == null || selectedSection().sectionSizeWidth() == null) {
                            return;
                        }

                        var orient = selectedSection().printViewLayoutPortrait() >= selectedSection().printViewLayoutLandscape() ? 0 : 1;
                        dataservice.getPTV({
                                orientation: orient,
                                reversRows: 0,
                                revrseCols: 0,
                                isDoubleSided: selectedSection().isDoubleSided(),
                                isWorknTurn: selectedSection().isWorknTurn(),
                                isWorknTumble: false,
                                applyPress: false,
                                itemHeight: selectedSection().itemSizeHeight(),
                                itemWidth: selectedSection().itemSizeWidth(),
                                printHeight: selectedSection().sectionSizeHeight(),
                                printWidth: selectedSection().sectionSizeWidth(),
                                grip: 1,
                                gripDepth: 0,
                                headDepth: 0,
                                printGutter: selectedSection().itemGutterHorizontal(),
                                itemHorizentalGutter: selectedSection().itemGutterHorizontal(),
                                itemVerticalGutter: selectedSection().itemGutterHorizontal()
                            }, {
                                success: function(data) {
                                    if (data != null) {
                                        itemPlan(undefined);
                                        side1Image(undefined);
                                        side2Image(undefined);
                                        side1Image(data.Side1ImageSource);
                                        showSide1Image(true);
                                        if (data.Side2ImageSource != "") {
                                            side2Image(data.Side2ImageSource);
                                        }

                                        itemPlan(data.Side1ImageSource);
                                        view.showSheetPlanImageDialog();
                                    }
                                },
                                error: function(response) {
                                    toastr.error("Error: Failed to Load Sheet Plan. Error: " + response, "", ist.toastrOptions);
                                }
                            });
                    },
                    openInkDialog = function(data, e) {
                        if (e.currentTarget.id === "side1InkColorBtn") {
                            isSide1InkButtonClicked(true);
                        } else {
                            isSide1InkButtonClicked(false);
                        }
                        view.showInksDialog();
                    },
                    // Add Section
                    addSection = function() {
                        // Open Product Selector Dialog
                    },
                    // Close Section Detail
                    closeSectionDetail = function() {
                        sectionHeader('');
                        isSectionDetailVisible(false);

                    },
                    // Get Paper Size by id
                    getPaperSizeById = function(id) {
                        return paperSizes.find(function(paperSize) {
                            return paperSize.id === id;
                        });
                    },
                    // Subscribe Section Changes for Ptv Calculation
                    subscribeSectionChanges = function() {
                        if (selectedSection() == undefined) {
                            return;
                        }
                        // Subscribe change events for ptv calculation
                        selectedSection().isDoubleSided.subscribe(function(value) {
                            if (value !== selectedSection().isDoubleSided()) {
                                selectedSection().isDoubleSided(value);
                            }
                            if (selectedSection().printingTypeUi() === '2') {
                                return;
                            }
                            getPtvCalculation();
                        });

                        // Work n Turn
                        selectedSection().isWorknTurn.subscribe(function(value) {
                            if (value !== selectedSection().isWorknTurn()) {
                                selectedSection().isWorknTurn(value);
                            }
                            if (selectedSection().printingTypeUi() === '2') {
                                return;
                            }
                            // If is Work n Turn then set Press 2 as it is in Press 1
                            if (value) {
                                selectedSection().pressIdSide2(selectedSection().pressId());
                            }
                            getPtvCalculation();
                        });

                        // On Select Sheet Size
                        selectedSection().sectionSizeId.subscribe(function(value) {
                            if (value !== selectedSection().sectionSizeId()) {
                                selectedSection().sectionSizeId(value);
                            }

                            // Get Paper Size by id
                            var paperSize = getPaperSizeById(value);

                            // Set Sizes To Custom Fields 
                            if (paperSize) {
                                selectedSection().sectionSizeHeight(paperSize.height);
                                selectedSection().sectionSizeWidth(paperSize.width);

                                if (selectedSection().printingTypeUi() === '2') {
                                    return;
                                }

                                // Get Ptv Calculation
                                getPtvCalculation(getSectionSystemCostCenters);
                            }
                        });

                        // Section Height
                        selectedSection().sectionSizeHeight.subscribe(function(value) {
                            if (value !== selectedSection().sectionSizeHeight()) {
                                selectedSection().sectionSizeHeight(value);
                            }

                            if (!selectedSection().isSectionSizeCustom() || selectedSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // Section Width
                        selectedSection().sectionSizeWidth.subscribe(function(value) {
                            if (value !== selectedSection().sectionSizeWidth()) {
                                selectedSection().sectionSizeWidth(value);
                            }

                            if (!selectedSection().isSectionSizeCustom() || selectedSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // On Select Item Size
                        selectedSection().itemSizeId.subscribe(function(value) {
                            if (value !== selectedSection().itemSizeId()) {
                                selectedSection().itemSizeId(value);
                            }

                            // Get Paper Size by id
                            var paperSize = getPaperSizeById(value);

                            // Set Sizes To Custom Fields 
                            if (paperSize) {
                                selectedSection().itemSizeHeight(paperSize.height);
                                selectedSection().itemSizeWidth(paperSize.width);

                                if (selectedSection().printingTypeUi() === '2') {
                                    return;
                                }

                                // Get Ptv Calculation
                                getPtvCalculation(getSectionSystemCostCenters);
                            }
                        });

                        // item Height
                        selectedSection().itemSizeHeight.subscribe(function(value) {
                            if (value !== selectedSection().itemSizeHeight()) {
                                selectedSection().itemSizeHeight(value);
                            }

                            if (!selectedSection().isItemSizeCustom() || selectedSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // item Width
                        selectedSection().itemSizeWidth.subscribe(function(value) {
                            if (value !== selectedSection().itemSizeWidth()) {
                                selectedSection().itemSizeWidth(value);
                            }

                            if (!selectedSection().isItemSizeCustom() || selectedSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // Include Gutter
                        selectedSection().includeGutter.subscribe(function(value) {
                            if (value !== selectedSection().includeGutter()) {
                                selectedSection().includeGutter(value);
                            }

                            if (selectedSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });
                        // Set Gutter Value
                        selectedSection().itemGutterHorizontal.subscribe(function(value) {
                            if (value !== selectedSection().itemGutterHorizontal()) {
                                selectedSection().itemGutterHorizontal(value);
                            }

                            if (selectedSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation(getSectionSystemCostCenters);
                        });

                        // On Press Change set Section Size Width to Press Max Width
                        selectedSection().pressId.subscribe(function(value) {
                            if (value !== selectedSection().pressId()) {
                                selectedSection().pressId(value);
                            }

                            var press = getPressById(value);
                            if (!press) {
                                return;
                            }

                            selectedSection().sectionSizeWidth(press.maxSheetWidth || 0);
                            selectedSection().pressIdSide1ColourHeads(press.colourHeads || 0);
                            selectedSection().pressIdSide1IsSpotColor(press.isSpotColor || false);
                            selectedSection().passesSide1(press.passes);
                            // Update Section Ink Coverage
                            selectedSection().sectionInkCoverageList.removeAll(selectedSection().sectionInkCoveragesSide1());
                            for (var i = 0; i < press.colourHeads; i++) {
                                selectedSection().sectionInkCoverageList.push(model.SectionInkCoverage.Create({
                                    SectionId: selectedSection().id(),
                                    Side: 1,
                                    InkOrder: i + 1
                                }));
                            }
                            getSectionSystemCostCenters();
                        });

                        // On Press Side 2 Change set Section Size Width to Press Max Width
                        selectedSection().pressIdSide2.subscribe(function(value) {
                            if (value !== selectedSection().pressIdSide2()) {
                                selectedSection().pressIdSide2(value);
                            }

                            var press = getPressById(value);
                            if (!press) {
                                return;
                            }

                            selectedSection().pressIdSide2ColourHeads(press.colourHeads || 0);
                            selectedSection().pressIdSide2IsSpotColor(press.isSpotColor || false);
                            selectedSection().passesSide2(press.passes);
                            // Update Section Ink Coverage
                            selectedSection().sectionInkCoverageList.removeAll(selectedSection().sectionInkCoveragesSide2());
                            for (var i = 0; i < press.colourHeads; i++) {
                                selectedSection().sectionInkCoverageList.push(model.SectionInkCoverage.Create({
                                    SectionId: selectedSection().id(),
                                    Side: 2,
                                    InkOrder: i + 1
                                }));
                            }
                            getSectionSystemCostCenters();
                        });

                        selectedSection().qty1.subscribe(function(value) {
                            if (isNaN(value)) {
                                return;
                            }
                            var qty1Value = parseInt(value);
                            if (qty1Value !== parseInt(selectedProduct().qty1())) {
                                selectedProduct().qty1(qty1Value);
                            }
                            // If Product is of type Finished Goods then don't get cost centres
                            if (selectedProduct().productType() === 3) {
                                return;
                            }
                            getSectionSystemCostCenters();
                            
                        });

                        selectedSection().qty2.subscribe(function(value) {
                            if (isNaN(value)) {
                                return;
                            }
                            var qty2Value = parseInt(value);
                            if (qty2Value !== parseInt(selectedProduct().qty2())) {
                                selectedProduct().qty2(qty2Value);
                            }
                            // If Product is of type Finished Goods then don't get cost centres
                            if (selectedProduct().productType() === 3) {
                                return;
                            }
                            getSectionSystemCostCenters();

                        });

                        selectedSection().qty3.subscribe(function(value) {
                            if (isNaN(value)) {
                                return;
                            }
                            var qty3Value = parseInt(value);
                            if (qty3Value !== parseInt(selectedProduct().qty3())) {
                                selectedProduct().qty3(qty3Value);
                            }
                            // If Product is of type Finished Goods then don't get cost centres
                            if (selectedProduct().productType() === 3) {
                                return;
                            }
                            getSectionSystemCostCenters();

                        });

                        selectedSection().isSecondTrim.subscribe(function(value) {
                            if (value !== selectedSection().isSecondTrim()) {
                                selectedSection().isSecondTrim(value);
                            }
                            getSectionSystemCostCenters();

                        });

                        selectedSection().isPaperSupplied.subscribe(function(value) {
                            if (value !== selectedSection().isPaperSupplied()) {
                                selectedSection().isPaperSupplied(value);
                            }
                            getSectionSystemCostCenters();

                        });
                        selectedSection().impressionCoverageSide1.subscribe(function(value) {
                            if (value !== selectedSection().impressionCoverageSide1()) {
                                selectedSection().impressionCoverageSide1(value);
                            }
                            getSectionSystemCostCenters();

                        });
                        selectedSection().impressionCoverageSide2.subscribe(function(value) {
                            if (value !== selectedSection().impressionCoverageSide2()) {
                                selectedSection().impressionCoverageSide2(value);
                            }
                            getSectionSystemCostCenters();

                        });


                    },
                    // Get Press By Id
                    getPressById = function(pressId) {
                        return presses.find(function(press) {
                            return press.id === pressId;
                        });
                    },
                    // On Change Quantity 1 Markup
                    onChangeQty1MarkUpId = function() { //qty1Markup
                        calculateSectionBaseCharge1();
                    },
                    q1NetTotal = function() {
                        if (selectedSection() !== undefined && selectedSection().qty1MarkUpId() !== undefined) {
                            var markup = _.find(markups(), function(item) {
                                return item.MarkUpId === selectedSection().qty1MarkUpId();
                            });

                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * parseFloat(selectedSection().baseCharge1())).toFixed(2);
                                selectedSection().baseCharge1(parseFloat(markupValue) + parseFloat(selectedSection().baseCharge1()));
                            }

                        }
                    },
                    // On Change Quantity 2 Markup
                    onChangeQty2MarkUpId = function() { //qtyMarkup
                        calculateSectionBaseCharge2();
                    },
                    q2NetTotal = function() {
                        if (selectedSection().qty2MarkUpId() !== undefined) {
                            var markup = _.find(markups(), function(item) {
                                return item.MarkUpId === selectedSection().qty2MarkUpId();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * (parseFloat(selectedSection().baseCharge2()))).toFixed(2);
                                selectedSection().baseCharge2(parseFloat(markupValue) + parseFloat(selectedSection().baseCharge2()));
                            }

                        }
                    },
                    // On Change Quantity 3 Markup
                    onChangeQty3MarkUpId = function() { //qtyMarkup
                        calculateSectionBaseCharge3();
                    },
                    q3NetTotal = function() {
                        if (selectedSection().qty3MarkUpId() !== undefined) {
                            var markup = _.find(markups(), function(item) {
                                return item.MarkUpId === selectedSection().qty3MarkUpId();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * parseFloat(selectedSection().baseCharge3())).toFixed(2);
                                selectedSection().baseCharge3(parseFloat(markupValue) + parseFloat(selectedSection().baseCharge3()));
                            }

                        }
                    },
                    isOpenItemSection = ko.observable(false),
                    calculateQty1NetTotalForItem = ko.computed({
                        read: function() {
                            if (!selectedProduct()) {
                                return 0;
                            }
                            return selectedProduct().qty1NetTotal() || 0;
                        },
                        write: function(value) {
                            if ((value === undefined || value === null) || value === selectedProduct().qty1NetTotal()) {
                                return;
                            }
                            selectedProduct().qty1NetTotal(value);
                            qty1GrossTotalForItem();
                        }
                    }),
                    calculateQty2NetTotalForItem = ko.computed({
                        read: function() {
                            if (!selectedProduct()) {
                                return 0;
                            }
                            return selectedProduct().qty2NetTotal() || 0;
                        },
                        write: function(value) {
                            if ((value === undefined || value === null) || value === selectedProduct().qty2NetTotal()) {
                                return;
                            }
                            selectedProduct().qty2NetTotal(value);
                            qty2GrossTotalForItem();
                        }
                    }),
                    calculateQty3NetTotalForItem = ko.computed({
                        read: function() {
                            if (!selectedProduct()) {
                                return 0;
                            }
                            return selectedProduct().qty3NetTotal() || 0;
                        },
                        write: function(value) {
                            if ((value === undefined || value === null) || value === selectedProduct().qty3NetTotal()) {
                                return;
                            }
                            selectedProduct().qty3NetTotal(value);
                            qty3GrossTotalForItem();
                        }
                    }),
                    qty1NetTotalForItem = function() {
                        baseCharge1TotalForItem = 0;
                        _.each(selectedProduct().itemSections(), function(itemSection) {
                            if (itemSection.baseCharge1() === undefined || itemSection.baseCharge1() === "" || itemSection.baseCharge1() === null || isNaN(itemSection.baseCharge1())) {
                                itemSection.baseCharge1(0);
                            }
                            baseCharge1TotalForItem = parseFloat(baseCharge1TotalForItem) + parseFloat(itemSection.baseCharge1());
                        });
                        selectedProduct().qty1NetTotal(baseCharge1TotalForItem);
                        qty1GrossTotalForItem();
                    },
                    qty2NetTotalForItem = function() {
                        baseCharge2TotalForItem = 0;
                        _.each(selectedProduct().itemSections(), function(itemSection) {
                            if (itemSection.baseCharge2() === undefined || itemSection.baseCharge2() === "" || itemSection.baseCharge2() === null || isNaN(itemSection.baseCharge2())) {
                                itemSection.baseCharge2(0);
                            }
                            baseCharge2TotalForItem = parseFloat(baseCharge2TotalForItem) + parseFloat(itemSection.baseCharge2());
                        });
                        selectedProduct().qty2NetTotal(baseCharge2TotalForItem);
                        qty2GrossTotalForItem();
                    },
                    qty3NetTotalForItem = function() {
                        baseCharge3TotalForItem = 0;
                        _.each(selectedProduct().itemSections(), function(itemSection) {
                            if (itemSection.baseCharge3() === undefined || itemSection.baseCharge3() === "" || itemSection.baseCharge3() === null || isNaN(itemSection.baseCharge3())) {
                                itemSection.baseCharge3(0);
                            }
                            baseCharge3TotalForItem = parseFloat(baseCharge3TotalForItem) + parseFloat(itemSection.baseCharge3());
                        });
                        selectedProduct().qty3NetTotal(baseCharge3TotalForItem);
                        qty3GrossTotalForItem();
                    },
                    qty1GrossTotalForItem = function() {
                        var qty1NetTotal = parseFloat((selectedProduct().qty1NetTotal() !== undefined && selectedProduct().qty1NetTotal() !== null) ? selectedProduct().qty1NetTotal() : 0).toFixed(2);
                        var tax = selectedProduct().tax1() !== undefined ? selectedProduct().tax1() : 0;
                        if (selectedProduct().tax1() !== undefined && selectedProduct().tax1() !== null && selectedProduct().tax1() !== "") {
                            var taxCalculate1 = ((tax / 100) * parseFloat(qty1NetTotal)).toFixed(2);
                            var total1 = (parseFloat(taxCalculate1) + parseFloat(qty1NetTotal)).toFixed(2);
                            selectedProduct().qty1GrossTotal(total1);
                            selectedProduct().qty1Tax1Value(taxCalculate1);
                        } else {
                            selectedProduct().qty1GrossTotal(qty1NetTotal);
                            selectedProduct().qty1Tax1Value(0);
                        }
                    },
                    qty2GrossTotalForItem = function() {
                        var qty2NetTotal = parseFloat((selectedProduct().qty2NetTotal() !== undefined && selectedProduct().qty2NetTotal() !== null) ? selectedProduct().qty2NetTotal() : 0).toFixed(2);
                        var tax = selectedProduct().tax1() !== undefined ? selectedProduct().tax1() : 0;
                        if (selectedProduct().tax1() !== undefined && selectedProduct().tax1() !== null && selectedProduct().tax1() !== "") {
                            var taxCalculate2 = ((tax / 100) * (parseFloat(qty2NetTotal))).toFixed(2);
                            var total2 = (parseFloat(taxCalculate2) + parseFloat(qty2NetTotal)).toFixed(2);
                            selectedProduct().qty2GrossTotal(total2);
                            selectedProduct().qty2Tax1Value(taxCalculate2);

                        } else {
                            selectedProduct().qty2GrossTotal(qty2NetTotal);
                            selectedProduct().qty2Tax1Value(0);
                        }
                    },
                    qty3GrossTotalForItem = function() {
                        var qty3NetTotal = parseFloat((selectedProduct().qty3NetTotal() !== undefined && selectedProduct().qty3NetTotal() !== null) ? selectedProduct().qty3NetTotal() : 0).toFixed(2);

                        var tax = selectedProduct().tax1() !== undefined ? selectedProduct().tax1() : 0;
                        if (selectedProduct().tax1() !== undefined && selectedProduct().tax1() !== null && selectedProduct().tax1() !== "") {
                            var taxCalculate3 = ((tax / 100) * parseFloat(qty3NetTotal)).toFixed(2);
                            var total3 = (parseFloat(taxCalculate3) + parseFloat(qty3NetTotal)).toFixed(2);
                            selectedProduct().qty3GrossTotal(total3);
                            selectedProduct().qty3Tax1Value(taxCalculate3);

                        } else {
                            selectedProduct().qty3GrossTotal(qty3NetTotal);
                            selectedProduct().qty3Tax1Value(0);
                        }
                    },
                    // Change on Tax Rate
                    calculateTax = ko.computed(function() {
                        if (!selectedProduct()) {
                            return;
                        }
                        qty1GrossTotalForItem();
                        qty2GrossTotalForItem();
                        qty3GrossTotalForItem();
                    }),
                    // Map List
                    mapList = function(observableList, data, factory) {
                        var list = [];
                        _.each(data, function(item) {
                            list.push(factory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(observableList(), list);
                        observableList.valueHasMutated();
                        //setDeliveryScheduleAddressName();
                    },
                    // Get Base Data
                    getBaseData = function() {
                        dataservice.getBaseData({
                            success: function(data) {

                                inks.removeAll();
                                if (data.Inks) {
                                    ko.utils.arrayPushAll(inks(), data.Inks);
                                    inks.valueHasMutated();
                                }
                                inkCoverageGroup.removeAll();
                                if (data.InkCoverageGroup) {
                                    ko.utils.arrayPushAll(inkCoverageGroup(), data.InkCoverageGroup);
                                    inkCoverageGroup.valueHasMutated();
                                }
                                markups.removeAll();
                                if (data.Markups) {
                                    _.each(data.Markups, function(item) {
                                        markups.push(item);
                                    });
                                }
                                // Paper Sizes
                                paperSizes.removeAll();
                                if (data.PaperSizes) {
                                    mapList(paperSizes, data.PaperSizes, model.PaperSize);
                                    mapList(sectionPaperSizes, data.PaperSizes, model.PaperSize);
                                    // Sort Descending For Section 
                                    sectionPaperSizes.sort(function(paperA, paperB) {
                                        return paperA.area < paperB.area ? 1 : -1;
                                    });
                                }
                                // Ink Plate Sides
                                inkPlateSides.removeAll();
                                if (data.InkPlateSides) {
                                    mapList(inkPlateSides, data.InkPlateSides, model.InkPlateSide);
                                }
                                // System Users
                                systemUsers.removeAll();
                                if (data.SystemUsers) {
                                    mapList(systemUsers, data.SystemUsers, model.SystemUser);
                                }
                                // Machines
                                presses.removeAll();
                                if (data.Machines) {
                                    mapList(presses, data.Machines, model.Machine);
                                }
                                defaultMarkUpId(data.DefaultMarkUpId);
                                currencySymbol(data.CurrencySymbol);
                                lengthUnit(data.LengthUnit || '');
                                weightUnit(data.WeightUnit || '');
                                loggedInUser(data.loggedInUserId || '');
                                view.initializeLabelPopovers();
                            },
                            error: function(response) {
                                toastr.error("Failed to load base data" + response);
                                view.initializeLabelPopovers();
                            }
                        });
                    },
                    // Calculates Section Charges 
                    calculateSectionBaseCharge1 = function() {
                        baseCharge1Total(0);

                        if (selectedSection() !== undefined) {
                            _.each(selectedSection().sectionCostCentres(), function(item) {
                                if (item.qty1NetTotal() === undefined || item.qty1NetTotal() === "" || item.qty1NetTotal() === null || isNaN(item.qty1NetTotal())) {
                                    item.qty1NetTotal(0);
                                }
                                baseCharge1Total(parseFloat(baseCharge1Total()) + parseFloat(item.qty1NetTotal()));
                            });
                            selectedSection().baseCharge1(baseCharge1Total());
                        }

                        if (selectedSection() !== undefined && selectedSection().similarSections != undefined) {
                            if (parseFloat(selectedSection().similarSections()) === 0) {
                                selectedSection().similarSections(1);
                            }
                            calculateBaseCharge1BySimilarSection();
                        }

                        if (selectedSection()) {
                            qty1NetTotalForItem();
                        }


                    },
                    // Calculates Section Charges 
                    calculateSectionBaseCharge2 = function() {
                        baseCharge2Total(0);

                        if (selectedSection() !== undefined) {
                            _.each(selectedSection().sectionCostCentres(), function(item) {
                                if (item.qty2NetTotal() === undefined || item.qty2NetTotal() === "" || item.qty2NetTotal() === null || isNaN(item.qty2NetTotal())) {
                                    item.qty2NetTotal(0);
                                }
                                baseCharge2Total(parseFloat(baseCharge2Total()) + parseFloat(item.qty2NetTotal()));
                            });
                            selectedSection().baseCharge2(baseCharge2Total());
                        }

                        if (selectedSection() !== undefined && selectedSection().similarSections != undefined) {
                            if (parseFloat(selectedSection().similarSections()) === 0) {
                                selectedSection().similarSections(1);
                            }
                            calculateBaseCharge2BySimilarSection();
                        }
                        if (selectedSection()) {
                            qty2NetTotalForItem();
                        }
                    },
                    // Calculates Section Charges 
                    calculateSectionBaseCharge3 = function() {
                        baseCharge3Total(0);

                        if (selectedSection() !== undefined) {
                            _.each(selectedSection().sectionCostCentres(), function(item) {
                                if (item.qty3NetTotal() === undefined || item.qty3NetTotal() === "" || item.qty3NetTotal() === null || isNaN(item.qty3NetTotal())) {
                                    item.qty3NetTotal(0);
                                }
                                baseCharge3Total(parseFloat(baseCharge3Total()) + parseFloat(item.qty3NetTotal()));
                            });
                            selectedSection().baseCharge3(baseCharge3Total());
                        }

                        if (selectedSection() !== undefined && selectedSection().similarSections != undefined) {
                            if (parseFloat(selectedSection().similarSections()) === 0) {
                                selectedSection().similarSections(1);
                            }
                            calculateBaseCharge3BySimilarSection();
                        }
                        if (selectedSection()) {
                            qty3NetTotalForItem();
                        }

                    },
                    //Get PTV Calculation
                    getPtvCalculation = function(callback) {
                        if (isPtvCalculationInProgress()) {
                            return;
                        }
                        if (selectedSection().itemSizeHeight() == null || selectedSection().itemSizeWidth() == null || selectedSection().sectionSizeHeight() == null || selectedSection().sectionSizeWidth() == null) {
                            return;
                        }
                        var orient;
                        if (selectedSection().printViewLayoutPortrait() >= selectedSection().printViewLayoutLandscape()) {
                            orient = 0;
                            selectedSection().isPortrait(true);
                        } else {
                            orient = 1;
                            selectedSection().isPortrait(false);
                        }

                        isPtvCalculationInProgress(true);
                        dataservice.getPTVCalculation({
                                orientation: orient,
                                reversRows: 0,
                                revrseCols: 0,
                                isDoubleSided: selectedSection().isDoubleSided(),
                                isWorknTurn: selectedSection().isWorknTurn(),
                                isWorknTumble: false,
                                applyPress: false,
                                itemHeight: selectedSection().itemSizeHeight(),
                                itemWidth: selectedSection().itemSizeWidth(),
                                printHeight: selectedSection().sectionSizeHeight(),
                                printWidth: selectedSection().sectionSizeWidth(),
                                grip: 1,
                                gripDepth: 0,
                                headDepth: 0,
                                printGutter: selectedSection().itemGutterHorizontal(),
                                itemHorizentalGutter: selectedSection().itemGutterHorizontal(),
                                itemVerticalGutter: selectedSection().itemGutterHorizontal()
                            }, {
                                success: function(data) {
                                    if (data != null) {
                                        selectedSection().printViewLayoutLandscape(data.LandscapePTV || 0);
                                        selectedSection().printViewLayoutPortrait(data.PortraitPTV || 0);
                                        // selectedSection().printViewLayout = data.LandscapePTV > data.PortraitPTV ? 1 : 0;
                                    }
                                    isPtvCalculationInProgress(false);
                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                },
                                error: function(response) {
                                    isPtvCalculationInProgress(false);
                                    toastr.error("Error: Failed to Calculate Number up value. Error: " + response, "", ist.toastrOptions);
                                }
                            });
                    },
                    calculateBaseChargeBasedOnSimilarSectionsValue = ko.computed({
                        read: function() {
                            if (!selectedSection()) {
                                return 0;
                            }

                            return selectedSection().similarSections();
                        },
                        write: function(value) {
                            if ((value === null || value === undefined) || value === selectedSection().similarSections()) {
                                return;
                            }

                            selectedSection().similarSections(value);
                            calculateSectionBaseCharge1();
                            calculateSectionBaseCharge2();
                            calculateSectionBaseCharge3();
                        }
                    }),
                    calculateBaseCharge1BySimilarSection = function() {
                        var newBaseCharge1Totaol = (selectedSection().baseCharge1() !== undefined ? selectedSection().baseCharge1() : 0) * parseFloat(selectedSection().similarSections());
                        selectedSection().baseCharge1(newBaseCharge1Totaol);
                        q1NetTotal();
                    },
                    calculateBaseCharge2BySimilarSection = function() {
                        selectedSection().baseCharge2(((selectedSection().baseCharge2() !== undefined ? selectedSection().baseCharge2() : 0) * parseFloat(selectedSection().similarSections())));
                        q2NetTotal();
                    },
                    calculateBaseCharge3BySimilarSection = function() {
                        selectedSection().baseCharge3(((selectedSection().baseCharge3() !== undefined ? selectedSection().baseCharge3() : 0) * parseFloat(selectedSection().similarSections())));
                        q3NetTotal();
                    },
                    //Side 1 Button Click
                    side1ButtonClick = function() {
                        showSide1Image(true);
                    },
                    //Side 2 Button Click
                    side2ButtonClick = function() {
                        showSide1Image(false);
                    },
                    runWizard = function() {
                        errorList.removeAll();
                        if (!doBeforeRunningWizard()) {
                            selectedSection().errors.showAllMessages();
                            view.goToValidationSummary();
                            return;
                        }
                        $('#myTab a[href="#tab-recomendation"]').tab('show');
                        getBestPress();
                    },
                    // Go To Element
                    gotoElement = function(validation) {
                        view.gotoElement(validation.element);
                    },
                    doBeforeRunningWizard = function() {
                        var flag = true;
                        if (selectedSection().qty1() <= 0) {
                            errorList.push({ name: "Please set quantity greater than zero.", element: selectedSection().qty1.domElement });
                            flag = false;
                        } else if (selectedSection().sectionInkCoverageList().length == 0) {
                            errorList.push({ name: "Please select ink colors.", element: selectedSection().plateInkId.domElement });
                            flag = false;
                        } else if (selectedSection().numberUp() <= 0) {
                            errorList.push({ name: "Sheet plan cannot be zero.", element: selectedSection().numberUp.domElement });
                            flag = false;
                        } else if (selectedSection().stockItemId() == null) {
                            errorList.push({ name: "Please select stock.", element: selectedSection().stockItemId.domElement });
                            flag = false;
                        }
                        return flag;
                    },
                    getBestPress = function() {
                        showEstimateRunWizard();
                        bestPressList.removeAll();
                        userCostCenters.removeAll();
                        userCostCentersCopy.removeAll();
                        selectedBestPressFromWizard(undefined);
                        dataservice.getBestPress(selectedSection().convertToServerData(), {
                            success: function(data) {
                                if (data != null) {
                                    mapBestPressList(data.PressList);
                                    mapUserCostCentersList(data.UserCostCenters);
                                }
                            },
                            error: function(response) {
                                toastr.error("Error: Failed to Load Best Press List." + response, "", ist.toastrOptions);
                            }
                        });
                    },
                    // Map Best Press List
                    mapBestPressList = function(data) {
                        var list = [];
                        _.each(data, function(item) {
                            list.push(model.BestPress.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(bestPressList(), list);
                        bestPressList.valueHasMutated();
                        if (selectedSection().pressId() !== undefined) {
                            var bestPress = _.find(bestPressList(), function(item) {
                                // var id = item.id;
                                return item.id === selectedSection().pressId();
                            });
                            if (bestPress) {
                                selectedBestPressFromWizard(bestPress);
                            } else {
                                if (bestPressList().length > 0) {
                                    selectedBestPressFromWizard(bestPressList()[0]);
                                }
                            }
                        } else {
                            if (bestPressList().length > 0) {
                                selectedBestPressFromWizard(bestPressList()[0]);
                            }
                        }

                    },
                    // Map User Cost Centers
                    mapUserCostCentersList = function(data) {
                        var list = [];
                        _.each(data, function(item) {
                            list.push(model.UserCostCenter.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(userCostCenters(), list);
                        userCostCenters.valueHasMutated();

                    },
                    searchCostCenter = ko.computed(function() {
                        userCostCentersCopy.removeAll();
                        if (searchString()) {
                            _.each(userCostCenters(), function(item) {
                                if ((item.name().toLowerCase().indexOf(searchString().toLowerCase()) > -1))
                                    userCostCentersCopy.push(item);
                            });
                        } else {
                            ko.utils.arrayPushAll(userCostCentersCopy(), userCostCenters());
                            userCostCentersCopy.valueHasMutated();
                        }
                    }),
                    getSectionSystemCostCenters = function() {
                        if (!selectedSection().pressId()) {
                            toastr.info("Please Select Side 1 Press in order to get Cost Centers");
                            return;
                        } else if (selectedSection().isDoubleSided() && !selectedSection().pressIdSide2()) {
                            toastr.info("Please Select Side 2 Press in order to get Cost Centers");
                            return;
                        } else if (selectedSection().numberUp() <= 0) {
                            toastr.info("Sheet plan cannot be zero in order to get Cost Centers");
                            return;
                        }
                        var currSec = selectedSection().convertToServerData();
                        if (currSec.SectionInkCoverages == null || currSec.SectionInkCoverages == undefined) {
                            currSec.SectionInkCoverages = [];
                        }
                        dataservice.getUpdatedSystemCostCenters(currSec, {
                            success: function(data) {
                                if (data != null) {
                                    // Map Section Cost Centres if Any
                                    if (data.SectionCostcentres && data.SectionCostcentres.length > 0) {
                                        selectedSection().sectionCostCentres.removeAll();
                                        var sectionCostcentres = [];

                                        _.each(data.SectionCostcentres, function(sectionCostCentre) {
                                            sectionCostcentres.push(model.SectionCostCentre.Create(sectionCostCentre));
                                        });

                                        // Push to Original Item
                                        ko.utils.arrayPushAll(selectedSection().sectionCostCentres(), sectionCostcentres);
                                        selectedSection().sectionCostCentres.valueHasMutated();
                                    }

                                    hideEstimateRunWizard();
                                    _.each(userCostCenters(), function(item) {
                                        if (item.isSelected()) {
                                            var sectionCostCenterItem = model.SectionCostCentre.Create({});
                                            sectionCostCenterItem.costCentreId(item.id());
                                            sectionCostCenterItem.name(item.name());
                                            selectedSection().sectionCostCentres.push(sectionCostCenterItem);
                                        }
                                    });

                                    calculateSectionBaseCharge1();
                                    calculateSectionBaseCharge2();
                                    calculateSectionBaseCharge3();
                                }
                            },
                            error: function(response) {
                                toastr.error("Error: Failed to Load System Cost Centers." + response);
                            }
                        });
                    },
                    updateCostCentersOnQtyChange = function() {
                        //addCostCenterVm.executeCostCenter(function (costCenter) {

                            
                            
                        //});
                        _.each(selectedSection().sectionCostCentres(), function (item) {
                            if (item.calculationMethodType() != null && item.calculationMethodType() > 0) {
                                var cc = addCostCenterVm.createBlankCostCenter();
                                cc.id(item.costCentreId());
                                cc.name(item.costCentreName());
                                cc.quantity1(selectedSection().qty1());
                                cc.quantity2(selectedSection().qty2());
                                cc.quantity3(selectedSection().qty3());
                                cc.sectionId(item.itemSectionId());
                                cc.callMode("UpdateAllCostCentreOnQuantityChange");
                                cc.calculationMethodType(item.calculationMethodType());
                                selectedCostCentre(cc);
                                addCostCenterVm.onCostCenterQtyChange(updateSectionCostCenterValues, selectedCostCentre());
                            }
                        });
                        
                        
                    },
                    updateSectionCostCenterValues = function() {
                        _.each(selectedSection().sectionCostCentres(), function (item) {
                            if (item.costCentreId() == selectedCostCentre().id()) {
                                item.qty1Charge(selectedCostCentre().setupCost());
                                item.qty2Charge(selectedCostCentre().setupCost2());
                                item.qty3Charge(selectedCostCentre().setupCost3());
                            }
                        });
                    },
                    selectBestPressFromWizard = function(bestPress) {
                        selectedBestPressFromWizard(bestPress);
                        selectedSection().pressId(bestPress.id);
                    },
                    clickOnWizardOk = function() {
                        getSectionSystemCostCenters();
                    },
                    //Show Estimate Run Wizard
                    showEstimateRunWizard = function() {
                        view.showEstimateRunWizard();
                    },
                    //Hide Estimate Run Wizard
                    hideEstimateRunWizard = function() {
                        view.hideEstimateRunWizard();
                    },
                    // Open Stock Item Dialog For Adding Stock
                    openStockItemDialogForAddingStock = function() {
                        //view.showCostCentersQuantityDialog();
                        isAddProductFromInventory(false);
                        isAddProductForSectionCostCenter(true);
                        stockDialog.show(function(stockItem) {
                            onSaveStockItem(stockItem);
                        }, stockCategory.paper, false, currencySymbol(), selectedOrder().taxRate());
                    },
                    //On Save Stock Item From Item Edit Dialog
                    onSaveStockItem = function(stockItem) {
                        var costCenter = model.costCentre.Create({});
                        selectedCostCentre(costCenter);

                        stockItemToCreate(stockItem);
                        //req.selecting stock quantity default values should be from section base quantity
                        selectedCostCentre().quantity1(selectedSection().qty1());
                        selectedCostCentre().quantity2(selectedSection().qty2());
                        selectedCostCentre().quantity3(selectedSection().qty3());
                        view.showCostCentersQuantityDialog();
                        isAddProductFromInventory(false);
                        isAddProductForSectionCostCenter(true);

                    },
                    // On Proceeding to Quantity Input dialog
                    onCostCenterQuantityInputProceed = function() {
                        view.hideCostCentersQuantityDialog();
                        addCostCenterVm.executeCostCenter(function(costCenter) {
                            selectedCostCentre(costCenter);
                            createNewCostCenterProduct();
                        });
                    },
                    //Product From Cost Center
                    createNewCostCenterProduct = function() {
                        if (!selectedCostCentre()) {
                            return;
                        }
                        var sectionCostCenter = model.SectionCostCentre.Create({});
                        sectionCostCenter.qty1(selectedCostCentre().quantity1());
                        sectionCostCenter.qty2(selectedCostCentre().quantity2());
                        sectionCostCenter.qty3(selectedCostCentre().quantity3());
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.costCentreId(selectedCostCentre().id());
                        sectionCostCenter.costCentreName(selectedCostCentre().name());
                        sectionCostCenter.name(selectedCostCentre().name());
                        sectionCostCenter.qty1Charge(selectedCostCentre().setupCost());
                        sectionCostCenter.qty1NetTotal(selectedCostCentre().setupCost());

                        sectionCostCenter.qty2Charge(selectedCostCentre().setupCost2());
                        sectionCostCenter.qty2NetTotal(selectedCostCentre().setupCost2());

                        sectionCostCenter.qty3Charge(selectedCostCentre().setupCost3());
                        sectionCostCenter.qty3NetTotal(selectedCostCentre().setupCost3());
                        sectionCostCenter.calculationMethodType(selectedCostCentre().calculationMethodType());
                        selectedSectionCostCenter(sectionCostCenter);
                        selectedQty(1);
                        selectedSection().sectionCostCentres.push(sectionCostCenter);
                        selectedSection().questionQueue(selectedCostCentre().questionQueue());
                        selectedSection().inputQueue(selectedCostCentre().inputQueue());
                        selectedSection().costCenterQueue(selectedCostCentre().costCenterQueue());
                        sectionCostCenterQty2MarkUpId(defaultMarkUpId());
                        sectionCostCenterQty1MarkUpId(defaultMarkUpId());
                        sectionCostCenterQty3MarkUpId(defaultMarkUpId());

                    },
                    // Copy job Cards
                    copyJobCards = function() {
                        selectedProduct();
                        var conCatJobCards = "";
                        var title = "";
                        if (selectedProduct().jobDescription1() !== null &&
                            selectedProduct().jobDescription1() !== undefined && selectedProduct().jobDescription1().trim() !== "") {
                            title = selectedProduct().jobDescriptionTitle1() ? selectedProduct().jobDescriptionTitle1() + " : " : "";
                            conCatJobCards = title + selectedProduct().jobDescription1();
                        }
                        if (selectedProduct().jobDescription2() !== null &&
                            selectedProduct().jobDescription2() !== undefined && selectedProduct().jobDescription2().trim() !== "") {
                            title = selectedProduct().jobDescriptionTitle2() ? selectedProduct().jobDescriptionTitle2() + " : " : "";
                            if (conCatJobCards === "") {
                                conCatJobCards = title + selectedProduct().jobDescription2();
                            } else {
                                conCatJobCards = conCatJobCards + "\n" + title + selectedProduct().jobDescription2();
                            }
                        }
                        if (selectedProduct().jobDescription3() !== null &&
                            selectedProduct().jobDescription3() !== undefined && selectedProduct().jobDescription3().trim() !== "") {
                            title = selectedProduct().jobDescriptionTitle3() ? selectedProduct().jobDescriptionTitle3() + " : " : "";
                            if (conCatJobCards === "") {
                                conCatJobCards = title + selectedProduct().jobDescription3();
                            } else {
                                conCatJobCards = conCatJobCards + "\n" + title + selectedProduct().jobDescription3();
                            }
                        }
                        if (selectedProduct().jobDescription4() !== null &&
                            selectedProduct().jobDescription4() !== undefined && selectedProduct().jobDescription4().trim() !== "") {
                            title = selectedProduct().jobDescriptionTitle4() ? selectedProduct().jobDescriptionTitle4() + " : " : "";
                            if (conCatJobCards === "") {
                                conCatJobCards = title + selectedProduct().jobDescription4();
                            } else {
                                conCatJobCards = conCatJobCards + "\n" + title + selectedProduct().jobDescription4();
                            }
                        }
                        if (selectedProduct().jobDescription5() !== null &&
                            selectedProduct().jobDescription5() !== undefined && selectedProduct().jobDescription5().trim() !== "") {
                            title = selectedProduct().jobDescriptionTitle5() ? selectedProduct().jobDescriptionTitle5() + " : " : "";
                            if (conCatJobCards === "") {
                                conCatJobCards = title + selectedProduct().jobDescription5();
                            } else {
                                conCatJobCards = conCatJobCards + "\n" + title + selectedProduct().jobDescription5();
                            }
                        }
                        if (selectedProduct().jobDescription6() !== null &&
                            selectedProduct().jobDescription6() !== undefined && selectedProduct().jobDescription6().trim() !== "") {
                            title = selectedProduct().jobDescriptionTitle6() ? selectedProduct().jobDescriptionTitle6() + " : " : "";
                            if (conCatJobCards === "") {
                                conCatJobCards = title + selectedProduct().jobDescription6();
                            } else {
                                conCatJobCards = conCatJobCards + "\n" + title + selectedProduct().jobDescription6();
                            }
                        }
                        if (selectedProduct().jobDescription7() !== null &&
                            selectedProduct().jobDescription7() !== undefined && selectedProduct().jobDescription7().trim() !== "") {
                            title = selectedProduct().jobDescriptionTitle7() ? selectedProduct().jobDescriptionTitle7() + " : " : "";
                            if (conCatJobCards === "") {
                                conCatJobCards = title + selectedProduct().jobDescription7();
                            } else {
                                conCatJobCards = conCatJobCards + "\n" + title + selectedProduct().jobDescription7();
                            }
                        }
                        selectedProduct().invoiceDescription(conCatJobCards);
                    },
                    //Update Orders Data (metho fwrite to trigger computed methods)
                    updateOrderData = function(selectedOrderParam, selectedProductParam, selectedSectionCostCenterParam, selectedQtyParam, selectedSectionParam) {
                        selectedOrder(selectedOrderParam);
                        selectedProduct(selectedProductParam);
                        selectedSectionCostCenter(selectedSectionCostCenterParam);
                        selectedQty(selectedQtyParam);
                        selectedSection(selectedSectionParam);

                    },
                    showSectionDetailEditor = function(section) {
                        errorList.removeAll();
                        selectedSection(section);
                        subscribeSectionChanges();
                        showSectionDetail(true);
                        selectedQty(1);
                    },
                    closeSectionDetailEditor = function() {
                        if (!selectedSection().isValid()) {
                            selectedProduct().showAllErrors();
                            selectedProduct().setValidationSummary(errorList);
                            return;
                        }
                        saveFrom("");
                        errorList.removeAll();
                        showSectionDetail(false);
                        selectedSection(undefined);
                    },
                    // Remove Item Section
                    deleteSection = function(section) {
                        confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                        confirmation.afterProceed(function() {
                            selectedProduct().itemSections.remove(section);
                            selectedProduct().hasDeletedSections(true);
                            showSectionDetail(false);
                            selectedSection(undefined);
                            qty1NetTotalForItem();
                            qty2NetTotalForItem();
                            qty3NetTotalForItem();
                        });
                        confirmation.show();

                    },
                    // open report
                    // open job card report
                    openExternalReportsJob = function() {

                        reportManager.outputTo("preview");


                        reportManager.OpenExternalReport(ist.reportCategoryEnums.JobCards, 1, selectedProduct().id());


                    },
                    // Open Phrase Library
                    openPhraseLibrary = function() {
                        phraseLibrary.isOpenFromPhraseLibrary(false);
                        phraseLibrary.defaultOpenSectionId(ist.sectionsEnum[1].id);
                        
                        if (selectedJobDescription() === 'txtDescription1')
                            phraseLibrary.defaultOpenPhraseFieldName(ist.JobProductionPhraseFieldsEnum[0].name);
                        else if (selectedJobDescription() === 'txtDescription2')
                            phraseLibrary.defaultOpenPhraseFieldName(ist.JobProductionPhraseFieldsEnum[1].name);
                        else if (selectedJobDescription() === 'txtDescription3')
                            phraseLibrary.defaultOpenPhraseFieldName(ist.JobProductionPhraseFieldsEnum[2].name);
                        else if (selectedJobDescription() === 'txtDescription4')
                            phraseLibrary.defaultOpenPhraseFieldName(ist.JobProductionPhraseFieldsEnum[3].name);
                        else if (selectedJobDescription() === 'txtDescription5')
                            phraseLibrary.defaultOpenPhraseFieldName(ist.JobProductionPhraseFieldsEnum[4].name);
                        else if (selectedJobDescription() === 'txtDescription6')
                            phraseLibrary.defaultOpenPhraseFieldName(ist.JobProductionPhraseFieldsEnum[5].name);
                        else if (selectedJobDescription() === 'txtDescription7')
                            phraseLibrary.defaultOpenPhraseFieldName(ist.JobProductionPhraseFieldsEnum[6].name);
                       
                       
                        phraseLibrary.show(function(phrase) {
                            updateJobDescription(phrase);
                        });
                    },

                    

                    onCloseSectionCostCenter = function() {
                        view.hideSectionCostCenterDialogModel();
                    },
                    // Update Job Description
                    updateJobDescription = function(phrase) {
                        if (!phrase) {
                            return;
                        }

                        // Set Phrase to selected Job Description
                        if (selectedJobDescription() === 'txtDescription1') {
                            selectedProduct().jobDescription1(selectedProduct().jobDescription1() ? selectedProduct().jobDescription1() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription2') {
                            selectedProduct().jobDescription2(selectedProduct().jobDescription2() ? selectedProduct().jobDescription2() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription3') {
                            selectedProduct().jobDescription3(selectedProduct().jobDescription3() ? selectedProduct().jobDescription3() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription4') {
                            selectedProduct().jobDescription4(selectedProduct().jobDescription4() ? selectedProduct().jobDescription4() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription5') {
                            selectedProduct().jobDescription5(selectedProduct().jobDescription5() ? selectedProduct().jobDescription5() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription6') {
                            selectedProduct().jobDescription6(selectedProduct().jobDescription6() ? selectedProduct().jobDescription6() + ' ' + phrase : phrase);
                        } else if (selectedJobDescription() === 'txtDescription7') {
                            selectedProduct().jobDescription7(selectedProduct().jobDescription7() ? selectedProduct().jobDescription7() + ' ' + phrase : phrase);
                        }
                    },
                    counter = 0,
                    // Create new Item Section
                    createNewItemSection = function() {
                        var section = defaultSection() ? defaultSection() : {};
                        var itemSection = model.ItemSection.Create(section);
                        counter = counter - 1;
                        itemSection.id(counter);
                        itemSection.itemId(selectedProduct().id());
                        itemSection.name("Text Sheet");
                        if (selectedProduct().itemSections().length > 0) {
                            selectedProduct().itemSections.splice(-1, 0, itemSection);
                        } else {
                            selectedProduct().itemSections.push(itemSection);
                        }

                        selectedSection(itemSection);
                        selectedSection().qty1MarkUpId(defaultMarkUpId());
                        selectedSection().qty2MarkUpId(defaultMarkUpId());
                        subscribeSectionChanges();
                        showSectionDetail(true);
                    },
                    // Delete Section Cost Center
                    onDeleteSectionCostCenter = function(costCenter) {
                        // Ask for confirmation
                        confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                        confirmation.afterProceed(function() {
                            view.hideSectionCostCenterDialogModel();
                            selectedSection().sectionCostCentres.remove(costCenter);
                            selectedSection().hasDeletedSectionCostCentres(true);
                            isSectionCostCenterDialogOpen(false);
                            calculateSectionBaseCharge1();
                            calculateSectionBaseCharge2();
                            calculateSectionBaseCharge3();
                        });
                        confirmation.show();
                        return;
                    },
                    onResetButtonClick = function(costCenter) {
                        confirmation.afterProceed(function() {
                            selectedSectionCostCenter().qty1(selectedSection().qty1());
                        });
                        confirmation.show();
                        return;
                    },
                    //Select Quantity
                    selectQuantity = function(qty) {
                        selectedQty(qty);
                    },
                    // #region Pre Press / Post Press Cost Center
                    // Add Pre Press Cost Center
                    onAddPrePressCostCenter = function () {
                        prePressOrPostPress(1);
                       // onItemSectionUpdate(showPrePressCostCenters);
                        showPrePressCostCenters();
                    },
                    showPrePressCostCenters = function() {
                        addCostCenterVm.show(addCostCenter, selectedOrder().companyId(), false, currencySymbol(), null, costCenterType.prePress, true,
                            selectedProduct().id(), selectedSection().convertToServerData());
                    },
                    openJobCardsTab = function() {
                        $("#sectionTabTabs a[href=#tab-jobs]").tab('show');
                    },
                    // Add Post Press Cost Center
                    onAddPostPressCostCenter = function () {
                        prePressOrPostPress(2);
                        //onItemSectionUpdate(showPostPressCostCenter);
                        showPostPressCostCenter();
                    },
                    showPostPressCostCenter = function() {
                        addCostCenterVm.show(addCostCenter, selectedOrder().companyId(), false, currencySymbol(), null, costCenterType.postPress, true,
                            selectedProduct().id(), selectedSection().convertToServerData());
                    },
                    // After adding cost center
                    addCostCenter = function(costCenter) {
                        if (costCenter) {
                            selectedCostCentre(costCenter);
                            // Set Section Quantities to Selected Cost Center quantities
                            selectedCostCentre().quantity1(selectedSection().qty1() || 0);
                            selectedCostCentre().quantity2(selectedSection().qty2() || 0);
                            selectedCostCentre().quantity3(selectedSection().qty3() || 0);
                            selectedCostCentre().sectionId(selectedSection().id());
                        }
                        isAddProductFromInventory(false);
                        isAddProductForSectionCostCenter(false);
                        addCostCenterVm.executeCostCenter(function(costCenterCalculated) {
                            selectedCostCentre(costCenterCalculated);
                            createNewCostCenterProduct();
                        });
                    },
                    // #endregion Pre Press / Post Press Cost Center
                    //#endregion
                    itemAttachmentFileLoadedCallback = function(file, data) {
                        selectedAttachment().fileSourcePath(data);
                        selectedAttachment().fileName(file.name);
                        if (file.name.indexOf(".") > -1) {
                            selectedAttachment().fileType("." + file.name.split(".")[1]);
                        }
                        selectedAttachment().companyId(selectedOrder().companyId());
                        selectedAttachment().itemId(selectedProduct().id());
                        selectedAttachment().uploadDate(new Date());
                    },
                    // Attachment Types
                    attchmentTypes = ko.observableArray([
                        { id: "Artwork", name: "Artwork" },
                        { id: "Draft", name: "Draft" },
                        { id: "Sample", name: "Sample" },
                        { id: "Copy", name: "Copy" },
                        { id: "Final", name: "Final" }
                    ]),
                    // Upload Item Attachment
                    uploadItemAttachment = function() {
                        selectedAttachment(model.ItemAttachment()),
                        view.showAttachmentModal();
                    },
                    // Hide Attachment Dialog
                    hideAttachmentDialog = function() {
                        view.hideAttachmentModal();
                    },
                    // Save Attachment
                    saveAttachment = function() {
                        if (dobeforeSaveItemAttachment()) {
                            if (selectedAttachment().isNewOrUpdate() === "2") {
                                var attachments = [];
                                _.each(selectedProduct().itemAttachments(), function(item) {
                                    if (item.parent() === selectedAttachment().parent()) {
                                        attachments.push(item);
                                    }
                                });
                                var attachment = _.find(selectedProduct().itemAttachments(), function(item) {
                                    return item.id() === selectedAttachment().parent();
                                });
                                if (attachment !== null && attachment !== undefined) {
                                    selectedAttachment().fileTitle(attachment.fileTitle() + "_V_" + (attachments.length + 1));
                                }
                            } else {
                                selectedAttachment().parent(null);
                            }
                            selectedProduct().itemAttachments.push(selectedAttachment());
                            hideAttachmentDialog();
                        }

                    },
                    // Dobefore Save
                    dobeforeSaveItemAttachment = function() {
                        var flag = true;
                        if (!selectedAttachment().isValid()) {
                            selectedAttachment().errors.showAllMessages();
                            flag = false;
                        }
                        return flag;
                    },
                    // Parent attachment List
                    parentFilterFileList = ko.computed(function() {
                        if (selectedProduct().itemAttachments().length === 0) {
                            return [];
                        }
                        return selectedProduct().itemAttachments.filter(function(attachment) {
                            return !attachment.parent() && (attachment.id() > 0);
                        });
                    }),
                    // Delete Item attachment
                    deleteItemAttachment = function(attachment) {
                        confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                        confirmation.afterProceed(function() {
                            selectedProduct().itemAttachments.remove(attachment);
                            selectedProduct().hasDeletedAttachments(true);
                        });
                        confirmation.show();
                        return;
                    },
                    deleteItem = function() {
                        confirmation.messageText("WARNING - This item will be removed from the system and you won’t be able to recover.  There is no undo");
                        confirmation.afterProceed(function() {
                            selectedOrder().items.remove(selectedProduct());
                            selectedOrder().hasDeletedItems(true);
                            closeItemDetail();
                        });
                        confirmation.afterCancel(function() {
                        });
                        confirmation.show();
                        return;
                    },
                    onItemSectionUpdate = function (callback) {
                        //var currSec = selectedSection().convertToServerData();
                        //dataservice.updateItemSection(currSec, {
                        //    success: function(data) {
                        //        if (data != null) {
                        //            if (callback && typeof callback === "function") {
                        //                callback();
                        //            }
                        //        }
                        //    }
                        //});
                        if (selectedSection().hasChanges()) {
                            saveFrom("section");
                            saveOrderFromSection(callback);
                        } else {
                            if (prePressOrPostPress() == 1) {
                                showPrePressCostCenters();
                            } else {
                                showPostPressCostCenter();
                            }
                            
                        }
                        
                    },
                //Initialize
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    getBaseData();
                    //pager(pagination.Pagination({ PageSize: 10 }, inventories, getInventoriesListItems));
                };

                return {

                    //#region Observables
                    showItemDetailsSection: showItemDetailsSection,
                    selectedProduct: selectedProduct,
                    errorList: errorList,
                    selectedSection: selectedSection,
                    selectedJobDescription: selectedJobDescription,
                    paperSizes: paperSizes,
                    sectionPaperSizes: sectionPaperSizes,
                    inkPlateSides: inkPlateSides,
                    markups: markups,
                    inkCoverageGroup: inkCoverageGroup,
                    inks: inks,
                    sectionVisibilityHandler: sectionVisibilityHandler,
                    isEstimateScreen: isEstimateScreen,
                    side1Image: side1Image,
                    side2Image: side2Image,
                    showSide1Image: showSide1Image,
                    addSection: addSection,
                    closeSectionDetail: closeSectionDetail,
                    baseCharge1Total: baseCharge1Total,
                    baseCharge2Total: baseCharge2Total,
                    baseCharge3Total: baseCharge3Total,
                    selectedCostCentre: selectedCostCentre,
                    selectedSectionCostCenter: selectedSectionCostCenter,
                    selectedOrder: selectedOrder,
                    selectedQty: selectedQty,
                    currencySymbol: currencySymbol,
                    isSectionVisible: isSectionVisible,
                    //#endregion
                    //#region Utility Functions
                    showItemDetail: showItemDetail,
                    closeItemDetail: closeItemDetail,
                    selectJobDescription: selectJobDescription,
                    openStockItemDialog: openStockItemDialog,
                    getPtvPlan: getPtvPlan,
                    openInkDialog: openInkDialog,
                    onChangeQty1MarkUpId: onChangeQty1MarkUpId,
                    onChangeQty2MarkUpId: onChangeQty2MarkUpId,
                    onChangeQty3MarkUpId: onChangeQty3MarkUpId,
                    calculateTax: calculateTax,
                    getPtvCalculation: getPtvCalculation,
                    side1ButtonClick: side1ButtonClick,
                    side2ButtonClick: side2ButtonClick,
                    doBeforeRunningWizard: doBeforeRunningWizard,
                    runWizard: runWizard,
                    openStockItemDialogForAddingStock: openStockItemDialogForAddingStock,
                    isAddProductFromInventory: isAddProductFromInventory,
                    isAddProductForSectionCostCenter: isAddProductForSectionCostCenter,
                    onCostCenterQuantityInputProceed: onCostCenterQuantityInputProceed,
                    openSectionCostCenterDialog: openSectionCostCenterDialog,
                    onSaveStockitemForSectionCostCenter: onSaveStockitemForSectionCostCenter,
                    copyJobCards: copyJobCards,
                    updateOrderData: updateOrderData,
                    initialize: initialize,
                    gotoElement: gotoElement,
                    getBestPress: getBestPress,
                    getSectionSystemCostCenters: getSectionSystemCostCenters,
                    bestPressList: bestPressList,
                    userCostCenters: userCostCenters,
                    userCostCentersCopy: userCostCentersCopy,
                    selectBestPressFromWizard: selectBestPressFromWizard,
                    selectedBestPressFromWizard: selectedBestPressFromWizard,
                    clickOnWizardOk: clickOnWizardOk,
                    showSectionDetail: showSectionDetail,
                    showSectionDetailEditor: showSectionDetailEditor,
                    closeSectionDetailEditor: closeSectionDetailEditor,
                    createNewItemSection: createNewItemSection,
                    sectionCostCenterQty1Charge: sectionCostCenterQty1Charge,
                    sectionCostCenterQty2Charge: sectionCostCenterQty2Charge,
                    sectionCostCenterQty3Charge: sectionCostCenterQty3Charge,
                    sectionCostCenterQty1MarkUpId: sectionCostCenterQty1MarkUpId,
                    sectionCostCenterQty2MarkUpId: sectionCostCenterQty2MarkUpId,
                    sectionCostCenterQty3MarkUpId: sectionCostCenterQty3MarkUpId,
                    sectionCostCenterQty1NetTotal: sectionCostCenterQty1NetTotal,
                    sectionCostCenterQty2NetTotal: sectionCostCenterQty2NetTotal,
                    sectionCostCenterQty3NetTotal: sectionCostCenterQty3NetTotal,
                    openPhraseLibrary: openPhraseLibrary,
                    calculateBaseChargeBasedOnSimilarSectionsValue: calculateBaseChargeBasedOnSimilarSectionsValue,
                    calculateQty1NetTotalForItem: calculateQty1NetTotalForItem,
                    calculateQty2NetTotalForItem: calculateQty2NetTotalForItem,
                    calculateQty3NetTotalForItem: calculateQty3NetTotalForItem,
                    itemAttachmentFileLoadedCallback: itemAttachmentFileLoadedCallback,
                    onDeleteSectionCostCenter: onDeleteSectionCostCenter,
                    onResetButtonClick: onResetButtonClick,
                    deleteSection: deleteSection,
                    jobStatuses: jobStatuses,
                    systemUsers: systemUsers,
                    lengthUnit: lengthUnit,
                    weightUnit: weightUnit,
                    searchString: searchString,
                    presses: presses,
                    impressionCoverages: impressionCoverages,
                    selectedAttachment: selectedAttachment,
                    uploadItemAttachment: uploadItemAttachment,
                    hideAttachmentDialog: hideAttachmentDialog,
                    attchmentTypes: attchmentTypes,
                    saveAttachment: saveAttachment,
                    parentFilterFileList: parentFilterFileList,
                    isSide1InkButtonClicked: isSide1InkButtonClicked,
                    deleteItemAttachment: deleteItemAttachment,
                    deleteItem: deleteItem,
                    defaultSection: defaultSection,
                    onAddPrePressCostCenter: onAddPrePressCostCenter,
                    openJobCardsTab: openJobCardsTab,
                    onAddPostPressCostCenter: onAddPostPressCostCenter,
                    sectionCostCenterQty1Ui: sectionCostCenterQty1Ui,
                    sectionCostCenterQty2Ui: sectionCostCenterQty2Ui,
                    sectionCostCenterQty3Ui: sectionCostCenterQty3Ui,
                    defaultMarkUpId: defaultMarkUpId,
                    applySectionCostCenterMarkup: applySectionCostCenterMarkup,
                    selectQuantityForItem: selectQuantityForItem,
                    selectQuantity: selectQuantity,
                    selectedQtyForItem: selectedQtyForItem,
                    openExternalReportsJob: openExternalReportsJob,
                    onCloseSectionCostCenter: onCloseSectionCostCenter,
                    onItemSectionUpdate: onItemSectionUpdate,
                    updateCostCentersOnQtyChange: updateCostCentersOnQtyChange
                    //#endregion
                };
            })()
        };
        return ist.itemDetail.viewModel;
    });
