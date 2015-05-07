/*
    Module with the view model for the item Detail.
*/
define("common/itemDetail.viewModel",
    ["jquery", "amplify", "ko", "common/itemDetail.dataservice", "common/itemDetail.model", "common/confirmation.viewModel", "common/pagination"
        , "common/sharedNavigation.viewModel", "common/stockItem.viewModel", "common/addCostCenter.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVM, stockDialog, addCostCenterVM) {
        var ist = window.ist || {};
        ist.itemDetail = {
            viewModel: (function () {
                var
                    //View
                    view,
                    //#region Observables
                    showItemDetailsSection = ko.observable(false),
                    showSectionDetail = ko.observable(false),
                    selectedProduct = ko.observable(model.Item.Create({})),
                    // Best PressL ist
                    bestPressList = ko.observableArray([]),
                    // User Cost Center List For Run Wizard
                    userCostCenters = ko.observableArray([]),
                    //selected Best Press From Wizard
                    selectedBestPressFromWizard = ko.observable(),
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
                    // Ink Plate Sides Methods
                    inkPlateSides = ko.observableArray([]),
                     // Markups
                    markups = ko.observableArray([]),
                    // Stock Category 
                    stockCategory = {
                        paper: 1,
                        inks: 2,
                        films: 3,
                        plates: 4
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
                     selectedQty = ko.observable(),
                     selectedOrder = ko.observable(),
                     currencySymbol = ko.observable(''),
                     closeItemDetailSection = null,
                    //#endregion  

                    //#region Utility Functions
                    updateSectionCostCenterDialog = ko.computed(function () {

                        if (selectedSectionCostCenter() != undefined && selectedQty() != undefined
                            //&& selectedSectionCostCenter().qty1MarkUpId() != undefined
                            // && selectedSectionCostCenter().qty2MarkUpId() != undefined && selectedSectionCostCenter().qty3MarkUpId() != undefined
                        ) {
                            var markupValue = 0;
                            if (selectedQty() == 1) {
                                _.each(markups(), function (markup) {
                                    if (markup.MarkUpId == selectedSectionCostCenter().qty1MarkUpId()) {
                                        markupValue = markup.MarkUpRate;
                                        selectedSectionCostCenter().qty1MarkUpValue(markupValue);
                                        var total = parseFloat(selectedSectionCostCenter().qty1Charge()) + (selectedSectionCostCenter().qty1Charge() * (markupValue / 100));
                                        selectedSectionCostCenter().qty1NetTotal(total);
                                    }
                                });
                            }
                            if (selectedQty() == 2) {
                                _.each(markups(), function (markup) {
                                    if (markup.MarkUpId == selectedSectionCostCenter().qty2MarkUpId()) {
                                        markupValue = markup.MarkUpRate;
                                        selectedSectionCostCenter().qty2MarkUpValue(markupValue);
                                        var total = parseFloat(selectedSectionCostCenter().qty2Charge()) + (selectedSectionCostCenter().qty2Charge() * (markupValue / 100));
                                        selectedSectionCostCenter().qty2NetTotal(total);
                                    }
                                });
                            }
                            if (selectedQty() == 3) {
                                _.each(markups(), function (markup) {
                                    if (markup.MarkUpId == selectedSectionCostCenter().qty3MarkUpId()) {
                                        markupValue = markup.MarkUpRate;
                                        selectedSectionCostCenter().qty3MarkUpValue(markupValue);
                                        var total = parseFloat(selectedSectionCostCenter().qty3Charge()) + (selectedSectionCostCenter().qty3Charge() * (markupValue / 100));
                                        selectedSectionCostCenter().qty3NetTotal(total);
                                    }
                                });
                            }
                        }
                    }),
                    onSaveStockitemForSectionCostCenter = function () {
                        var containsStockItem = false;
                        _.each(selectedSection().sectionCostCentres(), function (costCenter) {
                            if (costCenter.costCentreType() == '139') {
                                containsStockItem = true;
                                selectedSectionCostCenter(costCenter);
                            }
                        });

                        var sectionCostCenter = model.SectionCostCentre.Create({ ItemSectionId: selectedSection().id() });
                        if (!containsStockItem) {
                            selectedSectionCostCenter(sectionCostCenter);
                            selectedQty(1);
                        }

                        //sectionCostCenter.name(stockItemToCreate().name);
                        sectionCostCenter.name('Stock(s)');
                        //sectionCostCenter.qty1NetTotal(stockItemToCreate().price);
                        sectionCostCenter.costCentreType('139');
                        //sectionCostCenter.qty1NetTotal(selectedCostCentre().quantity1());
                        //sectionCostCenter.qty2NetTotal(selectedCostCentre().quantity2());
                        //sectionCostCenter.qty2NetTotal(selectedCostCentre().quantity3());
                        sectionCostCenter.qty1EstimatedStockCost(0);
                        sectionCostCenter.qty2EstimatedStockCost(0);
                        sectionCostCenter.qty3EstimatedStockCost(0);
                        sectionCostCenter.qty1Charge(stockItemToCreate().price);
                        sectionCostCenter.qty2Charge(0);
                        sectionCostCenter.qty3Charge(0);
                        view.hideCostCentersQuantityDialog();

                        var sectionCostCenterDetail = model.SectionCostCenterDetail.Create({ SectionCostCentreId: selectedSectionCostCenter().id() });
                        sectionCostCenterDetail.stockName(stockItemToCreate().name);
                        sectionCostCenterDetail.stockId(stockItemToCreate().id);
                        sectionCostCenterDetail.costPrice(stockItemToCreate().price);
                        sectionCostCenterDetail.qty1(selectedCostCentre().quantity1());
                        //sectionCostCenterDetail.qty1NetTotal(selectedCostCentre().quantity1());
                        //sectionCostCenterDetail.qty2NetTotal(selectedCostCentre().quantity2());
                        //sectionCostCenterDetail.qty2NetTotal(selectedCostCentre().quantity3());

                        sectionCostCenter.sectionCostCentreDetails.splice(0, 0, sectionCostCenterDetail);
                        if (!containsStockItem) {
                            selectedSection().sectionCostCentres.splice(0, 0, sectionCostCenter);

                        }
                        else {
                            var newCost = selectedSectionCostCenter().qty1Charge() + sectionCostCenterDetail.costPrice();
                            selectedSectionCostCenter().qty1Charge(newCost);
                            selectedSectionCostCenter().sectionCostCentreDetails.splice(0, 0, sectionCostCenterDetail);
                        }
                    },
                    //Show Item Detail
                    showItemDetail = function (selectedProductParam, selectedOrderParam, closeItemDetailParam) {
                        showSectionDetail(false);
                        showItemDetailsSection(true);
                        selectedProduct(selectedProductParam);
                        selectedOrder(selectedOrderParam);
                        selectedSection(selectedProduct().itemSections()[0]);
                        closeItemDetailSection = closeItemDetailParam;
                        subscribeSectionChanges();
                    },
                    closeItemDetail = function () {
                        showItemDetailsSection(false);
                        closeItemDetailSection();
                    },
                    // Select Job Description
                    selectJobDescription = function (jobDescription, e) {
                        selectedJobDescription(e.currentTarget.id);
                    },

                    updateSectionInkCoverageLists = function (side1Count, side2Count) {
                        if (getSide1Count() != side1Count) {
                            //If List is less then dropDown (Plate Ink)
                            if (getSide1Count() < side1Count) {
                                addNewFieldsInSectionInkCoverageList(side1Count - getSide1Count(), 1);
                            }
                                //If List is greater then dropDown (Plate Ink)
                            else if (getSide1Count() > side1Count) {
                                removeFieldsInSectionInkCoverageList(getSide1Count() - side1Count, 1);
                            }
                        }
                        if (getSide2Count() != side2Count) {
                            //If List is less then dropDown (Plate Ink)
                            if (getSide2Count() < side2Count) {
                                addNewFieldsInSectionInkCoverageList(side2Count - getSide2Count(), 2);
                            }
                                //If List is greater then dropDown (Plate Ink)
                            else if (getSide2Count() > side2Count) {
                                removeFieldsInSectionInkCoverageList(getSide2Count() - side2Count, 2);
                            }
                        }
                    },
                    getSide1Count = function () {
                        var count = 0;
                        _.each(selectedSection().sectionInkCoverageList(), function (item) {
                            if (item.side() == 1) {
                                count += 1;
                            }
                        });
                        return count;
                    },
                    getSide2Count = function () {
                        var count = 0;
                        _.each(selectedSection().sectionInkCoverageList(), function (item) {
                            if (item.side() == 2) {
                                count += 1;
                            }
                        });
                        return count;
                    },
                    addNewFieldsInSectionInkCoverageList = function (addNewCount, side) {
                        var counter = 0;
                        while (counter < addNewCount) {
                            var item = new model.SectionInkCoverage();
                            item.side(side);
                            item.sectionId(selectedSection().id());
                            selectedSection().sectionInkCoverageList.splice(0, 0, item);
                            counter++;
                        }
                    },
                    removeFieldsInSectionInkCoverageList = function (removeItemCount, side) {
                        var counter = removeItemCount;
                        while (counter != 0) {
                            _.each(selectedSection().sectionInkCoverageList(), function (item) {
                                if (item.side() == side && counter != 0) {
                                    selectedSection().sectionInkCoverageList.remove(item);
                                    counter--;
                                }
                            });
                            //selectedSection().sectionInkCoverageList.remove(selectedSection().sectionInkCoverageList()[0]);
                            //counter--;
                        }
                        //_.each(selectedSection().sectionInkCoverageList(), function (item) {

                        //        if (item.side == side && counter != 0) {
                        //            selectedSection().sectionInkCoverageList.remove(item);
                        //            counter --;
                        //        }
                        //}); 
                    },
                    //Available Ink Plate Sides
                    availableInkPlateSides = ko.computed(function () {
                        if (!selectedSection() || (selectedSection().isDoubleSided() === null || selectedSection().isDoubleSided() === undefined)) {
                            return inkPlateSides();
                        }

                        return inkPlateSides.filter(function (inkPlateSide) {
                            return inkPlateSide.isDoubleSided === selectedSection().isDoubleSided();
                        });
                    }),
                    availableInkPalteChange = function () {
                        setAvailableInkPlateChange();
                    },
                    setAvailableInkPlateChange = function () {
                        if (selectedSection() != undefined && selectedSection().plateInkId() != undefined) {
                            _.each(availableInkPlateSides(), function (item) {
                                if (item.id == selectedSection().plateInkId()) {
                                    updateSectionInkCoverageLists(item.plateInkSide1, item.plateInkSide2);
                                    selectedSection().side1Inks(item.plateInkSide1);
                                    selectedSection().side2Inks(item.plateInkSide2);
                                }
                            });
                        }
                    },
                    // Open Stock Item Dialog
                    openStockItemDialog = function () {
                        stockDialog.show(function (stockItem) {
                            selectedSection().selectStock(stockItem);
                        }, stockCategory.paper, false);
                    },
                    //Section Cost Center Dialog
                    openSectionCostCenterDialog = function (costCenter, qty) {
                        selectedSectionCostCenter(costCenter);
                        selectedQty(qty);
                        view.showSectionCostCenterDialogModel();
                    },
                    getPtvPlan = function () {
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
                            printGutter: 5,
                            horizentalGutter: 5,
                            verticalGutter: 5
                        }, {
                            success: function (data) {
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
                            error: function (response) {
                                toastr.error("Error: Failed to Load Sheet Plan. Error: " + response, "", ist.toastrOptions);
                            }
                        });
                    },
                    openInkDialog = function () {
                        //if (selectedSection() != undefined && selectedSection().plateInkId() != undefined) {
                        //    var count = 0;
                        //    _.each(availableInkPlateSides(), function (item) {
                        //        if (item.id == selectedSection().plateInkId()) {
                        //            updateSectionInkCoverageLists(item.plateInkSide1, item.plateInkSide2);
                        //            selectedSection().side1Inks(item.plateInkSide1);
                        //            selectedSection().side2Inks(item.plateInkSide2);
                        //        }
                        //    });
                        //}
                        view.showInksDialog();
                    },
                     // Add Section
                    addSection = function () {
                        // Open Product Selector Dialog
                    },

                    // Close Section Detail
                    closeSectionDetail = function () {
                        sectionHeader('');
                        isSectionDetailVisible(false);

                    },
                    // Get Paper Size by id
                    getPaperSizeById = function (id) {
                        return paperSizes.find(function (paperSize) {
                            return paperSize.id === id;
                        });
                    },
                    // Subscribe Section Changes for Ptv Calculation
                    subscribeSectionChanges = function () {
                        if (selectedSection() == undefined) {
                            return;
                        }
                        // Subscribe change events for ptv calculation
                        selectedSection().isDoubleSided.subscribe(function (value) {
                            if (value !== selectedSection().isDoubleSided()) {
                                selectedSection().isDoubleSided(value);
                            }

                            getPtvCalculation();
                        });

                        // Work n Turn
                        selectedSection().isWorknTurn.subscribe(function (value) {
                            if (value !== selectedSection().isWorknTurn()) {
                                selectedSection().isWorknTurn(value);
                            }

                            getPtvCalculation();
                        });

                        // On Select Sheet Size
                        selectedSection().sectionSizeId.subscribe(function (value) {
                            if (value !== selectedSection().sectionSizeId()) {
                                selectedSection().sectionSizeId(value);
                            }

                            // Get Paper Size by id
                            var paperSize = getPaperSizeById(value);

                            // Set Sizes To Custom Fields 
                            if (paperSize) {
                                selectedSection().sectionSizeHeight(paperSize.height);
                                selectedSection().sectionSizeWidth(paperSize.width);

                                // Get Ptv Calculation
                                getPtvCalculation();
                            }
                        });

                        // Section Height
                        selectedSection().sectionSizeHeight.subscribe(function (value) {
                            if (value !== selectedSection().sectionSizeHeight()) {
                                selectedSection().sectionSizeHeight(value);
                            }

                            if (!selectedSection().isSectionSizeCustom()) {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // Section Width
                        selectedSection().sectionSizeWidth.subscribe(function (value) {
                            if (value !== selectedSection().sectionSizeWidth()) {
                                selectedSection().sectionSizeWidth(value);
                            }

                            if (!selectedSection().isSectionSizeCustom()) {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // On Select Item Size
                        selectedSection().itemSizeId.subscribe(function (value) {
                            if (value !== selectedSection().itemSizeId()) {
                                selectedSection().itemSizeId(value);
                            }

                            // Get Paper Size by id
                            var paperSize = getPaperSizeById(value);

                            // Set Sizes To Custom Fields 
                            if (paperSize) {
                                selectedSection().itemSizeHeight(paperSize.height);
                                selectedSection().itemSizeWidth(paperSize.width);

                                // Get Ptv Calculation
                                getPtvCalculation();
                            }
                        });

                        // item Height
                        selectedSection().itemSizeHeight.subscribe(function (value) {
                            if (value !== selectedSection().itemSizeHeight()) {
                                selectedSection().itemSizeHeight(value);
                            }

                            if (!selectedSection().isItemSizeCustom()) {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // item Width
                        selectedSection().itemSizeWidth.subscribe(function (value) {
                            if (value !== selectedSection().itemSizeWidth()) {
                                selectedSection().itemSizeWidth(value);
                            }

                            if (!selectedSection().isItemSizeCustom()) {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // Include Gutter
                        selectedSection().includeGutter.subscribe(function (value) {
                            if (value !== selectedSection().includeGutter()) {
                                selectedSection().includeGutter(value);
                            }

                            getPtvCalculation();
                        });
                    },
                    // On Change Quantity 1 Markup
                    onChangeQty1MarkUpId = function () {//qty1Markup

                        q1NetTotal();
                    },
                    q1NetTotal = function () {
                        if (selectedProduct().qty1MarkUpId1() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty1MarkUpId1();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge1Total()).toFixed(2);
                                selectedProduct().qty1NetTotal(parseFloat(markupValue) + parseFloat(baseCharge1Total()));
                            }

                        } else {
                            selectedProduct().qty1NetTotal(baseCharge1Total());
                        }
                    },
                    // On Change Quantity 2 Markup
                    onChangeQty2MarkUpId = function () {//qtyMarkup
                        q2NetTotal();
                    },
                    q2NetTotal = function () {
                        if (selectedProduct().qty2MarkUpId2() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty2MarkUpId2();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge2Total()).toFixed(2);
                                selectedProduct().qty2NetTotal(parseFloat(markupValue) + parseFloat(baseCharge2Total()));
                            }

                        } else {
                            selectedProduct().qty2NetTotal(baseCharge2Total());
                        }
                    },
                    // On Change Quantity 3 Markup
                    onChangeQty3MarkUpId = function () {//qtyMarkup
                        q3NetTotal();
                    },
                    q3NetTotal = function () {
                        if (selectedProduct().qty3MarkUpId3() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty3MarkUpId3();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge3Total()).toFixed(2);
                                selectedProduct().qty3NetTotal(parseFloat(markupValue) + parseFloat(baseCharge3Total()));
                            }

                        } else {
                            selectedProduct().qty3NetTotal(baseCharge3Total());
                        }
                    },
                    // Change on Tax Rate
                    calculateTax = ko.computed(function () {
                        var qty1NetTotal = parseFloat((selectedProduct().qty1NetTotal() !== undefined && selectedProduct().qty1NetTotal() !== null) ? selectedProduct().qty1NetTotal() : 0).toFixed(2);
                        var qty2NetTotal = parseFloat((selectedProduct().qty2NetTotal() !== undefined && selectedProduct().qty2NetTotal() !== null) ? selectedProduct().qty2NetTotal() : 0).toFixed(2);
                        var qty3NetTotal = parseFloat((selectedProduct().qty3NetTotal() !== undefined && selectedProduct().qty3NetTotal() !== null) ? selectedProduct().qty3NetTotal() : 0).toFixed(2);

                        var tax = selectedProduct().tax1() !== undefined ? selectedProduct().tax1() : 0;
                        if (selectedProduct().tax1() !== undefined && selectedProduct().tax1() !== null && selectedProduct().tax1() !== "") {
                            var taxCalculate1 = ((tax / 100) * parseFloat(qty1NetTotal)).toFixed(2);
                            var total1 = (parseFloat(taxCalculate1) + parseFloat(qty1NetTotal)).toFixed(2);
                            selectedProduct().qty1GrossTotal(total1);
                            selectedProduct().qty1Tax1Value(taxCalculate1);

                            var taxCalculate2 = ((tax / 100) * (parseFloat(qty2NetTotal))).toFixed(2);
                            var total2 = (parseFloat(taxCalculate2) + parseFloat(qty2NetTotal)).toFixed(2);
                            selectedProduct().qty2GrossTotal(total2);
                            selectedProduct().qty2Tax1Value(taxCalculate2);

                            var taxCalculate3 = ((tax / 100) * parseFloat(qty3NetTotal)).toFixed(2);
                            var total3 = (parseFloat(taxCalculate3) + parseFloat(qty3NetTotal)).toFixed(2);
                            selectedProduct().qty3GrossTotal(total3);
                            selectedProduct().qty3Tax1Value(taxCalculate3);

                        } else {
                            selectedProduct().qty1GrossTotal(qty1NetTotal);
                            selectedProduct().qty2GrossTotal(qty2NetTotal);
                            selectedProduct().qty3GrossTotal(qty3NetTotal);
                            selectedProduct().qty1Tax1Value(0);
                            selectedProduct().qty2Tax1Value(0);
                            selectedProduct().qty3Tax1Value(0);
                        }
                    }),
                     // Map List
                    mapList = function (observableList, data, factory) {
                        var list = [];
                        _.each(data, function (item) {
                            list.push(factory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(observableList(), list);
                        observableList.valueHasMutated();
                        //setDeliveryScheduleAddressName();
                    },
                    // Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {

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
                                    _.each(data.Markups, function (item) {
                                        markups.push(item);
                                    });
                                }
                                // Paper Sizes
                                paperSizes.removeAll();
                                if (data.PaperSizes) {
                                    mapList(paperSizes, data.PaperSizes, model.PaperSize);
                                }

                                // Ink Plate Sides
                                inkPlateSides.removeAll();
                                if (data.InkPlateSides) {
                                    mapList(inkPlateSides, data.InkPlateSides, model.InkPlateSide);
                                }

                                view.initializeLabelPopovers();
                            },
                            error: function (response) {
                                toastr.error("Failed to load base data" + response);
                                view.initializeLabelPopovers();
                            }
                        });
                    },

                    // Calculates Section Charges 
                    calculateSectionChargeTotal = ko.computed(function () {
                        baseCharge1Total(0);
                        baseCharge2Total(0);
                        baseCharge3Total(0);

                        //if (selectedProduct().itemSections().length > 0) {

                        //    _.each(selectedProduct().itemSections(), function (item) {
                        //        if (item.qty1Profit() === undefined || item.qty1Profit() === "") {
                        //            item.qty1Profit(0);
                        //        }
                        //        if (item.qty2Profit() === undefined || item.qty2Profit() === "") {
                        //            item.qty2Profit(0);
                        //        }
                        //        if (item.qty3Profit() === undefined || item.qty3Profit() === "") {
                        //            item.qty3Profit(0);
                        //        }
                        //        var basCharge1 = parseFloat(((item.baseCharge1() !== undefined && item.baseCharge1() !== "") ? item.baseCharge1() : 0));
                        //        baseCharge1Total(parseFloat(baseCharge1Total()) + basCharge1 + parseFloat(item.qty1Profit()));
                        //        baseCharge2Total(parseFloat(baseCharge2Total()) + parseFloat(((item.baseCharge2() !== undefined && item.baseCharge2() !== "") ? item.baseCharge2() : 0)) + parseFloat(item.qty2Profit()));
                        //        baseCharge3Total(parseFloat(baseCharge3Total()) + parseFloat(((item.baseCharge3() !== undefined && item.baseCharge3() !== "") ? item.baseCharge3() : 0)) + parseFloat(item.qty3Profit()));
                        //    });
                        //}
                        if (selectedSection() !== undefined && selectedSection().sectionCostCentres().length > 0) {
                            _.each(selectedSection().sectionCostCentres(), function (item) {
                                if (item.qty1NetTotal() === undefined || item.qty1NetTotal() === "" || item.qty1NetTotal() === null || isNaN(item.qty1NetTotal())) {
                                    item.qty1NetTotal(0);
                                }
                                if (item.qty2NetTotal() === undefined || item.qty2NetTotal() === "" || item.qty2NetTotal() === null || isNaN(item.qty2NetTotal())) {
                                    item.qty2NetTotal(0);
                                }
                                if (item.qty3NetTotal() === undefined || item.qty3NetTotal() === "" || item.qty3NetTotal() === null || isNaN(item.qty3NetTotal())) {
                                    item.qty3NetTotal(0);
                                }
                                baseCharge1Total(parseFloat(baseCharge1Total()) + parseFloat(item.qty1NetTotal()));
                                baseCharge2Total(parseFloat(baseCharge2Total()) + parseFloat(item.qty2NetTotal()));
                                baseCharge3Total(parseFloat(baseCharge3Total()) + parseFloat(item.qty3NetTotal()));
                            });
                        }

                        if (selectedSection() !== undefined && selectedSection().similarSections != undefined && selectedSection().similarSections() !== undefined) {
                            if (parseFloat(selectedSection().similarSections()) === 0) {
                                selectedSection().similarSections(1);
                            }
                            calculateBaseChargeBasedOnSimilarSectionsValue();
                        }


                    }),
                    //Get PTV Calculation
                        getPtvCalculation = function () {
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
                            }
                            else {
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
                                printGutter: 5,
                                horizentalGutter: 5,
                                verticalGutter: 5
                            }, {
                                success: function (data) {
                                    if (data != null) {
                                        selectedSection().printViewLayoutLandscape(data.LandscapePTV || 0);
                                        selectedSection().printViewLayoutPortrait(data.PortraitPTV || 0);
                                        // selectedSection().printViewLayout = data.LandscapePTV > data.PortraitPTV ? 1 : 0;
                                    }
                                    isPtvCalculationInProgress(false);
                                },
                                error: function (response) {
                                    isPtvCalculationInProgress(false);
                                    toastr.error("Error: Failed to Calculate Number up value. Error: " + response, "", ist.toastrOptions);
                                }
                            });
                        },
                    calculateBaseChargeBasedOnSimilarSectionsValue = function () {
                        var newBaseCharge1Totaol = (baseCharge1Total() !== undefined ? baseCharge1Total() : 0) * parseFloat(selectedSection().similarSections());
                        baseCharge1Total(newBaseCharge1Totaol);
                        baseCharge2Total((baseCharge2Total() !== undefined ? baseCharge2Total() : 0) * parseFloat(selectedSection().similarSections()));
                        baseCharge3Total((baseCharge3Total() !== undefined ? baseCharge3Total() : 0) * parseFloat(selectedSection().similarSections()));
                        q1NetTotal();
                        q2NetTotal();
                        q3NetTotal();
                    },
                     //Side 1 Button Click
                        side1ButtonClick = function () {
                            showSide1Image(true);
                        },
                    //Side 2 Button Click
                    side2ButtonClick = function () {
                        showSide1Image(false);
                    },
                    runWizard = function () {
                        errorList.removeAll();
                        if (!doBeforeRunningWizard()) {
                            selectedSection().errors.showAllMessages();
                            return;
                        }
                        $('#myTab a[href="#tab-recomendation"]').tab('show');
                        getBestPress();
                    },
                    // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    doBeforeRunningWizard = function () {
                        var flag = true;
                        if (selectedSection().qty1() <= 0) {
                            errorList.push({ name: "Please set quantity greater than zero.", element: selectedSection().qty1.domElement });
                            flag = false;
                        }
                        else if (selectedSection().sectionInkCoverageList().length == 0) {
                            errorList.push({ name: "Please select ink colors.", element: selectedSection().plateInkId.domElement });
                            flag = false;
                        }
                        else if (selectedSection().numberUp() <= 0) {
                            errorList.push({ name: "Sheet plan cannot be zero.", element: selectedSection().numberUp.domElement });
                            flag = false;
                        }
                        else if (selectedSection().stockItemId() == null) {
                            errorList.push({ name: "Please select stock.", element: selectedSection().stockItemName.domElement });
                            flag = false;
                        }
                        return flag;
                    },
                    getBestPress = function () {
                        showEstimateRunWizard();
                        bestPressList.removeAll();
                        userCostCenters.removeAll();
                        selectedBestPressFromWizard(undefined);
                        dataservice.getBestPress(selectedSection().convertToServerData(), {
                            success: function (data) {
                                if (data != null) {
                                    mapBestPressList(data.PressList);
                                    mapUserCostCentersList(data.UserCostCenters);
                                }
                            },
                            error: function (response) {
                                toastr.error("Error: Failed to Load Best Press List." + response, "", ist.toastrOptions);
                            }
                        });
                    },

                    // Map Best Press List
                        mapBestPressList = function (data) {
                            var list = [];
                            _.each(data, function (item) {
                                list.push(model.BestPress.Create(item));
                            });

                            // Push to Original Array
                            ko.utils.arrayPushAll(bestPressList(), list);
                            bestPressList.valueHasMutated();
                            if (selectedSection().pressId() !== undefined) {
                                var bestPress = _.find(bestPressList(), function (item) {
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
                        mapUserCostCentersList = function (data) {
                            var list = [];
                            _.each(data, function (item) {
                                list.push(model.UserCostCenter.Create(item));
                            });

                            // Push to Original Array
                            ko.utils.arrayPushAll(userCostCenters(), list);
                            userCostCenters.valueHasMutated();
                        },
                        getSectionSystemCostCenters = function () {
                            if (!selectedBestPressFromWizard()) {
                                return;
                            }
                            var currSec = selectedSection().convertToServerData();
                            currSec.PressId = selectedBestPressFromWizard().id;
                            dataservice.getUpdatedSystemCostCenters(currSec, {
                                success: function (data) {
                                    if (data != null) {
                                        //selectedSection(model.ItemSection.Create(data));

                                        // Map Section Cost Centres if Any
                                        if (data.SectionCostcentres && data.SectionCostcentres.length > 0) {
                                            selectedSection().sectionCostCentres.removeAll();
                                            var sectionCostcentres = [];

                                            _.each(data.SectionCostcentres, function (sectionCostCentre) {
                                                sectionCostcentres.push(model.SectionCostCentre.Create(sectionCostCentre));
                                            });

                                            // Push to Original Item
                                            ko.utils.arrayPushAll(selectedSection().sectionCostCentres(), sectionCostcentres);
                                            selectedSection().sectionCostCentres.valueHasMutated();
                                        }

                                        hideEstimateRunWizard();
                                        _.each(userCostCenters(), function (item) {
                                            if (item.isSelected()) {
                                                var sectionCostCenterItem = model.SectionCostCentre.Create({});
                                                sectionCostCenterItem.costCentreId(item.id());
                                                sectionCostCenterItem.name(item.name());
                                                selectedSection().sectionCostCentres.push(sectionCostCenterItem);
                                            }
                                        });


                                        var charge1 = setDecimalPlaceValue(selectedSection().baseCharge1());
                                        var charge2 = setDecimalPlaceValue(selectedSection().baseCharge2());
                                        var charge3 = setDecimalPlaceValue(selectedSection().baseCharge3());
                                        baseCharge1Total(charge1);
                                        baseCharge2Total(charge2);
                                        baseCharge3Total(charge3);

                                    }
                                },
                                error: function (response) {
                                    toastr.error("Error: Failed to Load System Cost Centers." + response);
                                }
                            });
                        },
                        setDecimalPlaceValue = function (chargevalue) {
                            if (chargevalue) {
                                var val = parseFloat(chargevalue);
                                var calc;
                                if (!isNaN(val)) {
                                    calc = (val.toFixed(2));
                                    return calc;
                                }
                                else {
                                    calc = 0.00;
                                    return calc;
                                }
                            }
                            else {
                                return 0.00;
                            }
                        },
                        selectBestPressFromWizard = function (bestPress) {
                            selectedBestPressFromWizard(bestPress);
                            selectedSection().pressId(bestPress.id);
                        },
                        clickOnWizardOk = function () {
                            getSectionSystemCostCenters();
                        },
                    //Show Estimate Run Wizard
                        showEstimateRunWizard = function () {
                            view.showEstimateRunWizard();
                        },
                    //Hide Estimate Run Wizard
                        hideEstimateRunWizard = function () {
                            view.hideEstimateRunWizard();
                        },
                    // Open Stock Item Dialog For Adding Stock
                    openStockItemDialogForAddingStock = function () {
                        //view.showCostCentersQuantityDialog();
                        isAddProductFromInventory(false);
                        isAddProductForSectionCostCenter(true);
                        stockDialog.show(function (stockItem) {
                            onSaveStockItem(stockItem);
                        }, stockCategory.paper, false);
                    },
                    //On Save Stock Item From Item Edit Dialog
                    onSaveStockItem = function (stockItem) {
                        var costCenter = model.costCentre.Create({});
                        selectedCostCentre(costCenter);

                        stockItemToCreate(stockItem);

                        view.showCostCentersQuantityDialog();
                        isAddProductFromInventory(false);
                        isAddProductForSectionCostCenter(true);

                    },
                    onSaveProductCostCenter = function () {
                        createNewCostCenterProduct();
                        hideCostCentreDialog();
                        hideCostCentreQuantityDialog();
                    },
                    //Product From Cost Center
                    createNewCostCenterProduct = function (costCenter) {
                        selectedCostCentre(costCenter);
                        var item = model.Item.Create({ EstimateId: selectedOrder().id() });
                        selectedProduct(item);
                        item.productName(selectedCostCentre().name());
                        item.qty1(selectedCostCentre().quantity1());
                        item.qty1NetTotal(selectedCostCentre().setupCost());

                        var itemSection = model.ItemSection.Create({});

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

                        //sectionCostCenter.qty1NetTotal(selectedCostCentre().setupCost());
                        sectionCostCenter.qty1Charge(selectedCostCentre().setupCost());

                        selectedSectionCostCenter(sectionCostCenter);
                        selectedQty(1);


                        itemSection.sectionCostCentres.push(sectionCostCenter);
                        item.itemSections.push(itemSection);

                        if (isCostCenterDialogForShipping()) {
                            item.itemType(2); // Delivery Item
                            var deliveryItem = _.find(selectedOrder().items(), function (itemWithType2) {
                                return itemWithType2.itemType() === 2;
                            });
                            if (deliveryItem !== undefined) {
                                selectedOrder().items.remove(deliveryItem);
                            }

                        }

                        selectedOrder().items.splice(0, 0, item);

                        selectedSection(itemSection);

                    },
                    // Copy job Cards
                    copyJobCards = function () {
                        selectedProduct();
                        var conCatJobCards = "";
                        if (selectedProduct().jobDescription1() !== undefined && selectedProduct().jobDescription1().trim() !== "") {
                            conCatJobCards = selectedProduct().jobDescription1();
                        }
                        if (selectedProduct().jobDescription2() !== undefined && selectedProduct().jobDescription2().trim() !== "") {
                            if (conCatJobCards === "") {
                                conCatJobCards = selectedProduct().jobDescription2();
                            } else {
                                conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription2();
                            }
                        }
                        if (selectedProduct().jobDescription3() !== undefined && selectedProduct().jobDescription3().trim() !== "") {
                            if (conCatJobCards === "") {
                                conCatJobCards = selectedProduct().jobDescription3();
                            } else {
                                conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription3();
                            }
                        }
                        if (selectedProduct().jobDescription4() !== undefined && selectedProduct().jobDescription4().trim() !== "") {
                            if (conCatJobCards === "") {
                                conCatJobCards = selectedProduct().jobDescription4();
                            } else {
                                conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription4();
                            }
                        }
                        if (selectedProduct().jobDescription5() !== undefined && selectedProduct().jobDescription5().trim() !== "") {
                            if (conCatJobCards === "") {
                                conCatJobCards = selectedProduct().jobDescription5();
                            } else {
                                conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription5();
                            }
                        }
                        if (selectedProduct().jobDescription6() !== undefined && selectedProduct().jobDescription6().trim() !== "") {
                            if (conCatJobCards === "") {
                                conCatJobCards = selectedProduct().jobDescription6();
                            } else {
                                conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription6();
                            }
                        }
                        if (selectedProduct().jobDescription7() !== undefined && selectedProduct().jobDescription7().trim() !== "") {
                            if (conCatJobCards === "") {
                                conCatJobCards = selectedProduct().jobDescription7();
                            } else {
                                conCatJobCards = conCatJobCards + "," + selectedProduct().jobDescription7();
                            }
                        }
                        selectedProduct().invoiceDescription(conCatJobCards);
                    },

                    //Update Orders Data (metho fwrite to trigger computed methods)
                    updateOrderData = function (selectedOrderParam, selectedProductParam, selectedSectionCostCenterParam, selectedQtyParam, selectedSectionParam) {
                        selectedOrder(selectedOrderParam);
                        selectedProduct(selectedProductParam);
                        selectedSectionCostCenter(selectedSectionCostCenterParam);
                        selectedQty(selectedQtyParam);
                        selectedSection(selectedSectionParam);

                    },
                    showSectionDetailEditor = function (section) {
                        selectedSection(section);
                        showSectionDetail(true);
                    },
                    closeSectionDetailEditor = function () {
                        showSectionDetail(false);
                    },
                    //#endregion
                    //Initialize
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                        getBaseData();
                        subscribeSectionChanges();
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
                    inkPlateSides: inkPlateSides,
                    markups: markups,
                    inkCoverageGroup: inkCoverageGroup,
                    inks: inks,
                    availableInkPlateSides: availableInkPlateSides,
                    availableInkPalteChange: availableInkPalteChange,
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
                    calculateSectionChargeTotal: calculateSectionChargeTotal,
                    getPtvCalculation: getPtvCalculation,
                    side1ButtonClick: side1ButtonClick,
                    side2ButtonClick: side2ButtonClick,
                    doBeforeRunningWizard: doBeforeRunningWizard,
                    runWizard: runWizard,
                    openStockItemDialogForAddingStock: openStockItemDialogForAddingStock,
                    isAddProductFromInventory: isAddProductFromInventory,
                    isAddProductForSectionCostCenter: isAddProductForSectionCostCenter,
                    onSaveProductCostCenter: onSaveProductCostCenter,
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
                    selectBestPressFromWizard: selectBestPressFromWizard,
                    selectedBestPressFromWizard: selectedBestPressFromWizard,
                    clickOnWizardOk: clickOnWizardOk,
                    showSectionDetail: showSectionDetail,
                    showSectionDetailEditor: showSectionDetailEditor,
                    closeSectionDetailEditor:closeSectionDetailEditor

                    //#endregion
                };
            })()
        };
        return ist.itemDetail.viewModel;
    });
