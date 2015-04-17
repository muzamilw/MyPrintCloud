/*
    Module with the view model for the Order.
*/
define("invoice/invoice.viewModel",
    ["jquery", "amplify", "ko", "invoice/invoice.dataservice", "invoice/invoice.model", "common/pagination", "common/confirmation.viewModel",
        "common/sharedNavigation.viewModel", "common/companySelector.viewModel", "common/phraseLibrary.viewModel", "common/stockItem.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, shared, companySelector, phraseLibrary, stockDialog) {
        var ist = window.ist || {};
        ist.invoice = {
            viewModel: (function () {
                var // the view 
                    view,
                    // #region Arrays
                    // invoices
                    invoices = ko.observableArray([]),
                    // flag colors
                    sectionFlags = ko.observableArray([]),
                    // Markups
                    markups = ko.observableArray([]),
                    // company contacts
                    companyContacts = ko.observableArray([]),
                    // Company Addresses
                    companyAddresses = ko.observableArray([]),
                    // System Users
                    systemUsers = ko.observableArray([]),
                    //
                    selectedCompanyTaxRate = ko.observable(),
                    // Errors List
                    errorList = ko.observableArray([]),
                    //Filter 
                    orderType = ko.observableArray([
                        { name: "ALL", value: "2" },
                        { name: "Direct  Order", value: "0" },
                        { name: "Online Order", value: "1" }
                    ]),
                    flagItem = function (state) {
                        return "<div style=\"height:20px;margin-right:10px;width:25px;float:left;background-color:" + $(state.element).data("color") + "\"></div><div>" + state.text + "</div>";
                    },
                    flagSelection = function (state) {
                        return "<span style=\"height:20px;width:25px;float:left;margin-right:10px;margin-top:5px;background-color:" + $(state.element).data("color") + "\"></span><span>" + state.text + "</span>";
                    },
                    orderTypeFilter = ko.observable(),
                    filterFlags = ko.observableArray([]),
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoading = ko.observable(false),
                    // Is Invoice Editor Visible
                    isDetailsVisible = ko.observable(false),
                    // Is Item Detail Visible
                    isItemDetailVisible = ko.observable(false),
                    // Is Section Detail Visible
                    isSectionDetailVisible = ko.observable(false),
                    // Is Company Base Data Loaded
                    isCompanyBaseDataLoaded = ko.observable(false),
                    // #endregion
                    // #region Observables
                    // filter
                    filterText = ko.observable(),
                    // Selected Product
                    selectedProduct = ko.observable(),
                    // Base Charge 1 Total
                    baseCharge1Total = ko.observable(0),
                    selectedMarkup1 = ko.observable(0),
                    selectedCategoryId = ko.observable(),
                    currencySymbol = ko.observable(''),
                    // Active Order
                    selectedInvoice = ko.observable(),
                    // Page Header 
                    pageHeader = ko.computed(function () {
                        return selectedInvoice() && selectedInvoice().name() ? selectedInvoice().name() : 'Invoices';
                    }),
                     defaultAddress = ko.observable(),
                    // Default Company Contact
                    defaultCompanyContact = ko.observable(),
                    // Sort On
                    sortOn = ko.observable(2),
                    // Sort Order -  true means asc, false means desc
                    sortIsAsc = ko.observable(false),
                    // Pagination
                    pager = ko.observable(new pagination.Pagination({ PageSize: 10 }, invoices)),
                    // Selected Address
                    selectedAddress = ko.computed(function () {
                        if (!selectedInvoice() || !selectedInvoice().addressId() || companyAddresses().length === 0) {
                            return defaultAddress();
                        }

                        var addressResult = companyAddresses.find(function (address) {
                            return address.id === selectedInvoice().addressId();
                        });

                        return addressResult || defaultAddress();
                    }),
                    // Selected Company Contact
                    selectedCompanyContact = ko.computed(function () {
                        if (!selectedInvoice() || !selectedInvoice().contactId() || companyContacts().length === 0) {
                            return defaultCompanyContact();
                        }

                        var contactResult = companyContacts.find(function (contact) {
                            return contact.id === selectedInvoice().contactId();
                        });

                        return contactResult || defaultCompanyContact();
                    }),


                    selecteditem = ko.observable(),

                    // Edit Order
                    editInvoice = function (data) {
                        getOrderById(data.id(), openInvoiceEditor);
                    },
                    // Open Editor
                    openInvoiceEditor = function () {
                        isDetailsVisible(true);
                    },
                    // On Close Editor
                    onCloseInvoiceEditor = function () {
                        if (selectedOrder().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(onSaveOrder);
                            confirmation.afterCancel(function () {
                                selectedOrder().reset();
                                closeOrderEditor();
                                orderCodeHeader('');
                                sectionHeader('');
                                itemCodeHeader('');
                                isSectionDetailVisible(false);
                                isItemDetailVisible(false);
                            });
                            confirmation.show();
                            return;
                        }
                        closeOrderEditor();
                    },
                    // Close Editor
                    closeOrderEditor = function () {
                        selectedOrder(model.Estimate.Create({}));
                        isOrderDetailsVisible(false);
                        errorList.removeAll();
                    },
                    // On Archive
                    onArchiveInvoice = function (order) {
                        confirmation.afterProceed(function () {
                            archiveOrder(order.id());
                        });
                        confirmation.show();
                    },
                    // Open Company Dialog
                    openCompanyDialog = function () {
                        companySelector.show(onSelectCompany, [0, 1], true);
                    },
                    // On Select Company
                    onSelectCompany = function (company) {
                        if (!company) {
                            return;
                        }

                        if (selectedOrder().companyId() === company.id) {
                            return;
                        }

                        selectedOrder().companyId(company.id);
                        selectedOrder().companyName(company.name);

                        // Get Company Address and Contacts
                        getBaseForCompany(company.id, company.storeId);
                    },
                    // Add Item
                    addItem = function () {
                        // Open Product Selector Dialog
                    },
                    // Edit Item
                    editItem = function (item) {
                        itemCodeHeader(item.code());
                        selectedProduct(item);
                        if (item.tax1() === undefined) {
                            if (selectedCompanyTaxRate() !== undefined && selectedCompanyTaxRate() !== null) {
                                item.tax1(selectedCompanyTaxRate());
                            } else if (item.defaultItemTax() !== undefined && item.defaultItemTax() !== null) {
                                item.tax1(item.defaultItemTax());
                            } else {
                                item.tax1(0);
                                item.taxRateIsDisabled(true);
                            }
                        }

                        // calculateSectionChargeTotal();
                        openItemDetail();
                        var section = selectedProduct() != undefined ? selectedProduct().itemSections()[0] : undefined;
                        editSection(section);
                    },
                    // Open Item Detail
                    openItemDetail = function () {
                        isItemDetailVisible(true);
                        view.initializeLabelPopovers();
                    },
                   
                    vatList = ko.observableArray([
                        {
                            name: "VAT Free",
                            id: 1,
                            tax: 0
                        },
                        { name: "VAT 20%", id: 2, tax: 20 },
                        { name: "VAT 10%", id: 3, tax: 10 }
                    ]),
                    // Close Item Detail
                    closeItemDetail = function () {
                        itemCodeHeader('');
                        sectionHeader('');
                        isItemDetailVisible(false);
                        isSectionDetailVisible(false);
                    },
                    // Save Product
                    saveProduct = function () {

                    },
                    // Delete Product
                    deleteProduct = function (item) {
                        selectedOrder().items.remove(item);
                    },
                    
                    // On Save Order
                    onSaveOrder = function (data, event, navigateCallback) {
                        if (!doBeforeSave()) {
                            return;
                        }
                        _.each(selectedOrder().prePayments(), function (item) {
                            item.customerId(selectedOrder().companyId());
                        });
                        saveOrder(closeOrderEditor, navigateCallback);
                    },
                    // Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedOrder().isValid()) {
                            selectedOrder().showAllErrors();
                            selectedOrder().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    // Get Order From list
                    getOrderFromList = function (id) {
                        return orders.find(function (order) {
                            return order.id() === id;
                        });
                    },
                    // Get Paper Size by id
                    getPaperSizeById = function (id) {
                        return paperSizes.find(function (paperSize) {
                            return paperSize.id === id;
                        });
                    },
                    
                    // Subscribe Section Changes for Ptv Calculation
                    subscribeSectionChanges = function () {
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
                    onChangeQty1MarkUpId = function (qty1Markup) {
                        if (selectedProduct().qty1MarkUpId1() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty1MarkUpId1();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge1Total()).toFixed(2);
                                selectedProduct().qty1NetTotal(parseFloat(markupValue) + parseFloat(baseCharge1Total()));
                            }

                        } else {
                            selectedProduct().qty1NetTotal(0);
                        }

                    },
                    // On Change Quantity 2 Markup
                    onChangeQty2MarkUpId = function (qtyMarkup) {

                        if (selectedProduct().qty2MarkUpId2() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty2MarkUpId2();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge2Total()).toFixed(2);
                                selectedProduct().qty2NetTotal(parseFloat(markupValue) + parseFloat(baseCharge2Total()));
                            }

                        } else {
                            selectedProduct().qty2NetTotal(0);
                        }
                    },
                    // On Change Quantity 3 Markup
                    onChangeQty3MarkUpId = function (qtyMarkup) {

                        if (selectedProduct().qty3MarkUpId3() !== undefined) {
                            var markup = _.find(markups(), function (item) {
                                return item.MarkUpId === selectedProduct().qty3MarkUpId3();
                            });
                            if (markup) {
                                var markupValue = ((parseFloat(markup.MarkUpRate) / 100) * baseCharge3Total()).toFixed(2);
                                selectedProduct().qty3NetTotal(parseFloat(markupValue) + parseFloat(baseCharge3Total()));
                            }

                        } else {
                            selectedProduct().qty3NetTotal(0);
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
                    deleteOrderButtonHandler = function () {
                        confirmation.messageText("Are you sure you want to delete order?");
                        confirmation.afterProceed(deleteOrder);
                        confirmation.afterCancel(function () {

                        });
                        confirmation.show();
                        return;
                    },
                    deleteOrder = function () {
                        dataservice.deleteOrder({
                            OrderId: selectedOrder().id()
                        }, {
                            success: function () {
                                toastr.success("Order successfully deleted!");
                                selectedOrder().reset();
                                closeOrderEditor();
                                orderCodeHeader('');
                                sectionHeader('');
                                itemCodeHeader('');
                                isSectionDetailVisible(false);
                                isItemDetailVisible(false);
                            },
                            error: function (response) {
                                toastr.error("Failed to delete order!" + response);
                            }
                        });
                    },
                    openInkDialog = function () {
                        if (selectedSection() != undefined && selectedSection().plateInkId() != undefined) {
                            var count = 0;
                            _.each(availableInkPlateSides(), function (item) {
                                if (item.id == selectedSection().plateInkId()) {
                                    updateSectionInkCoverageLists(item.plateInkSide1, item.plateInkSide2);
                                    selectedSection().side1Inks(item.plateInkSide1);
                                    selectedSection().side2Inks(item.plateInkSide2);
                                }
                            });
                        }
                        view.showInksDialog();
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
                            if (item.side == 1) {
                                count += 1;
                            }
                        });
                        return count;
                    },
                    getSide2Count = function () {
                        var count = 0;
                        _.each(selectedSection().sectionInkCoverageList(), function (item) {
                            if (item.side == 2) {
                                count += 1;
                            }
                        });
                        return count;
                    },
                    addNewFieldsInSectionInkCoverageList = function (addNewCount, side) {
                        var counter = 0;
                        while (counter < addNewCount) {
                            var item = new model.SectionInkCoverage();
                            item.side = side;
                            selectedSection().sectionInkCoverageList.splice(0, 0, item);
                            counter++;
                        }
                    },
                    removeFieldsInSectionInkCoverageList = function (removeItemCount, side) {
                        var counter = removeItemCount;
                        while (counter != 0) {
                            _.each(selectedSection().sectionInkCoverageList(), function (item) {
                                if (item.side == side && counter != 0) {
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
                    selectedSectionCostCenter = ko.observable(),
                    selectedQty = ko.observable(),
                    //Section Cost Center Dialog
                    openSectionCostCenterDialog = function (costCenter, qty) {
                        selectedSectionCostCenter(costCenter);
                        selectedQty(qty);
                        view.showSectionCostCenterDialogModel();
                    },
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
                    // #endregion
                    // #region ServiceCalls
                    // Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                paperSizes.removeAll();
                                inkPlateSides.removeAll();

                                if (data.SectionFlags) {
                                    mapList(sectionFlags, data.SectionFlags, model.SectionFlag);
                                }
                                if (data.SystemUsers) {
                                    mapList(systemUsers, data.SystemUsers, model.SystemUser);
                                }
                                if (data.PipeLineSources) {
                                    mapList(pipelineSources, data.PipeLineSources, model.PipeLineSource);
                                }
                                paymentMethods.removeAll();
                                if (data.PaymentMethods) {
                                    ko.utils.arrayPushAll(paymentMethods(), data.PaymentMethods);
                                    paymentMethods.valueHasMutated();
                                }
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
                                categories.removeAll();
                                if (data.StockCategories) {
                                    _.each(data.StockCategories, function (item) {
                                        categories.push(item);
                                    });
                                }
                                nominalCodes.removeAll();
                                if (data.ChartOfAccounts) {
                                    _.each(data.ChartOfAccounts, function (item) {
                                        nominalCodes.push(item);
                                    });
                                }

                                // Paper Sizes
                                if (data.PaperSizes) {
                                    mapList(paperSizes, data.PaperSizes, model.PaperSize);
                                }

                                // Ink Plate Sides
                                if (data.InkPlateSides) {
                                    mapList(inkPlateSides, data.InkPlateSides, model.InkPlateSide);
                                }

                                currencySymbol(data.CurrencySymbol);
                                view.initializeLabelPopovers();
                            },
                            error: function (response) {
                                toastr.error("Failed to load base data" + response);
                                view.initializeLabelPopovers();
                            }
                        });
                    },
                    // Get Section flag color
                    getSectionFlagColor = function (sectionFlagId) {
                        var sectionFlg = sectionFlags.find(function (sectionFlag) {
                            return sectionFlag.id == sectionFlagId;
                        });

                        if (!sectionFlg) {
                            return undefined;
                        }

                        return sectionFlg.color;
                    },
                    // Save Order
                    saveOrder = function (callback, navigateCallback) {
                        selectedOrder().statusId(view.orderstate());
                        var order = selectedOrder().convertToServerData();
                        _.each(selectedOrder().prePayments(), function (item) {
                            order.PrePayments.push(item.convertToServerData());
                        });
                        _.each(selectedOrder().deliverySchedules(), function (item) {
                            order.ShippingInformations.push(item.convertToServerData());
                        });
                        var itemsArray = [];
                        _.each(selectedOrder().items(), function (obj) {
                            var item = obj.convertToServerData(); // item converted 
                            var attArray = [];
                            _.each(item.ItemAttachment, function (att) {
                                var attchment = att.convertToServerData(); // item converted 
                                attArray.push(attchment);
                            });
                            item.ItemAttachments = attArray;
                            itemsArray.push(item);

                        });
                        order.Items = itemsArray;
                        dataservice.saveOrder(order, {
                            success: function (data) {
                                if (!selectedOrder().id()) {
                                    // Update Id
                                    selectedOrder().id(data.OrderId);

                                    // Add to top of list
                                    orders.splice(0, 0, selectedOrder());
                                } else {
                                    // Get Order
                                    var orderUpdated = getOrderFromList(selectedOrder().id());
                                    if (orderUpdated) {
                                        order.orderCode(data.OrderCode);
                                        order.orderName(data.OrderName);
                                    }
                                }

                                toastr.success("Saved Successfully.");

                                if (callback && typeof callback === "function") {
                                    callback();
                                }

                                if (navigateCallback && typeof navigateCallback === "function") {
                                    navigateCallback();
                                }
                                orderCodeHeader('');
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Order. Error: " + response);
                            }
                        });
                    },
                    // Clone Order
                    cloneOrder = function (order, callback) {
                        dataservice.cloneOrder({ OrderId: order.id() }, {
                            success: function (data) {
                                if (data) {
                                    var newOrder = model.Estimate.Create(data);
                                    // Add to top of list
                                    orders.splice(0, 0, newOrder);
                                    selectedOrder(newOrder);

                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }

                                toastr.success("Cloned Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to Clone Order. Error: " + response);
                            }
                        });
                    },
                    // archive Order
                    archiveOrder = function () {
                        dataservice.archiveOrder({
                            OrderId: selectedOrder().id()
                        }, {
                            success: function () {
                                selectedOrder().isArchived(true);
                                toastr.success("Archived Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to archive Order. Error: " + response);
                            }
                        });
                    },
                    itemAttachmentFileLoadedCallback = function (file, data) {
                        //Flag check, whether file is already exist in media libray
                        var flag = true;

                        _.each(selectedProduct().itemAttachments(), function (item) {
                            if (item.fileSourcePath() === data && item.fileName() === file.name) {
                                flag = false;
                            }
                        });

                        if (flag) {
                            var attachment = model.ItemAttachment();
                            attachment.id(undefined);
                            attachment.fileSourcePath(data);
                            attachment.fileName(file.name);
                            attachment.companyId(selectedOrder().companyId());
                            selectedProduct().itemAttachments.push(attachment);

                        }
                    },
                    //get Orders Of Current Screen
                    getOrdersOfCurrentScreen = function () {
                        getOrders(currentScreen());
                    },
                    //Get Order Tab Changed Event
                    getOrdersOnTabChange = function (currentTab) {
                        pager().reset();
                        if (isEstimateScreen()) {
                            getEstimates(currentTab);
                        } else {
                            getOrders(currentTab);
                        }

                    },
                    // Get Orders
                    getOrders = function (currentTab) {
                        isLoadingOrders(true);
                        currentScreen(currentTab);
                        dataservice.getOrders({
                            SearchString: filterText(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            Status: currentScreen(),
                            FilterFlag: selectedFilterFlag(),
                            OrderTypeFilter: orderTypeFilter(),
                            SortBy: sortOn(),
                            IsAsc: sortIsAsc()
                        }, {
                            success: function (data) {
                                orders.removeAll();
                                if (data && data.TotalCount > 0) {
                                    mapOrders(data.Orders);
                                    pager().totalCount(data.TotalCount);
                                }
                                isLoadingOrders(false);
                            },
                            error: function (response) {
                                isLoadingOrders(false);
                                toastr.error("Failed to load orders" + response);
                            }
                        });
                    },
                    // Get Order By Id
                    getInvoiceById = function (id, callback) {
                        isLoading(true);
                        isCompanyBaseDataLoaded(false);
                        dataservice.getOrder({
                            id: id
                        }, {
                            success: function (data) {
                                if (data) {
                                    selectedInvoice(model.Invoice.Create(data));
                                    //_.each(data.PrePayments, function (item) {
                                    //    selectedOrder().prePayments.push(model.PrePayment.Create(item));
                                    //});


                                    // Get Base Data For Company
                                    //if (data.CompanyId) {
                                    //    getBaseForCompany(data.CompanyId, 0);
                                    //}
                                    //if (callback && typeof callback === "function") {
                                    //    callback();
                                    //}
                                }
                                isLoading(false);
                                var code = !selectedInvoice().code() ? "INVOICE CODE" : selectedInvoice().code();
                                //orderCodeHeader(code);
                                //view.initializeLabelPopovers();
                            },
                            error: function (response) {
                                isLoading(false);
                                toastr.error("Failed to load order details" + response);
                                view.initializeLabelPopovers();
                            }
                        });
                    },
                    // Get Company Base Data
                    getBaseForCompany = function (id, storeId) {
                        isCompanyBaseDataLoaded(false);
                        dataservice.getBaseDataForCompany({
                            id: id,
                            storeId: storeId
                        }, {
                            success: function (data) {
                                companyAddresses.removeAll();
                                companyContacts.removeAll();
                                if (data) {
                                    if (data.CompanyAddresses) {
                                        mapList(companyAddresses, data.CompanyAddresses, model.Address);
                                    }
                                    if (data.CompanyContacts) {
                                        mapList(companyContacts, data.CompanyContacts, model.CompanyContact);
                                    }
                                    selectedCompanyTaxRate(data.TaxRate);
                                }
                                isCompanyBaseDataLoaded(true);
                            },
                            error: function (response) {
                                isCompanyBaseDataLoaded(true);
                                toastr.error("Failed to load details for selected company" + response);
                            }
                        });
                    },


                //Initialize method to call in every screen
                initializeScreen = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    // Get Base Data
                    getBaseData();
                },
                // Initialize the view model
                initialize = function (specifiedView) {
                    initializeScreen(specifiedView);
                    pager(new pagination.Pagination({ PageSize: 10 }, invoices, getInvoices));
                    getInvoices();
                };
                
                //#endregion
                return {
                    // #region Observables
                    selectedInvoice: selectedInvoice,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isLoading: isLoading,
                    invoices: invoices,
                    isDetailsVisible: isDetailsVisible,
                    isItemDetailVisible: isItemDetailVisible,
                    pager: pager,
                    errorList: errorList,
                    selectedAddress: selectedAddress,
                    selectedCompanyContact: selectedCompanyContact,
                    companyContacts: companyContacts,
                    companyAddresses: companyAddresses,
                    sectionFlags: sectionFlags,
                    systemUsers: systemUsers,
                    selectedProduct: selectedProduct,
                    initialize: initialize,
                    resetFilter: resetFilter,
                    filterOrders: filterOrders,
                    editInvoice: editInvoice,
                    onCloseInvoiceEditor: onCloseInvoiceEditor,
                    onArchiveInvoice: onArchiveInvoice,
                    gotoElement: gotoElement,
                    onCloneOrder: onCloneOrder,
                    openCompanyDialog: openCompanyDialog,
                    closeItemDetail: closeItemDetail,
                    openItemDetail: openItemDetail,
                    addItem: addItem,
                    editItem: editItem,
                    saveProduct: saveProduct,
                    addSection: addSection,
                    editSection: editSection,
                    closeSectionDetail: closeSectionDetail,
                    deleteProduct: deleteProduct,
                    selectJobDescription: selectJobDescription,
                    openPhraseLibrary: openPhraseLibrary,
                    currentScreen: currentScreen,
                    orderType: orderType,
                    orderTypeFilter: orderTypeFilter,
                    flagItem: flagItem,
                    flagSelection: flagSelection,
                    filterFlags: filterFlags,
                    selectedFilterFlag: selectedFilterFlag,
                    orderCodeHeader: orderCodeHeader,
                    itemCodeHeader: itemCodeHeader,
                    sectionHeader: sectionHeader,
                    onAddInventoryItem: onAddInventoryItem,
                    selecteditem: selecteditem,
                    selectedStockItem: selectedStockItem,
                    selectedStockOptionName: selectedStockOptionName,
                    selectedStockOptionSequenceNumber: selectedStockOptionSequenceNumber,
                    selectedStockOption: selectedStockOption,
                    deleteOrderButtonHandler: deleteOrderButtonHandler,
                    productQuantitiesList: productQuantitiesList,
                    side1Image: side1Image,
                    side2Image: side2Image,
                    showSide1Image: showSide1Image,
                    inks: inks,
                    inkCoverageGroup: inkCoverageGroup,
                    openSectionCostCenterDialog: openSectionCostCenterDialog,
                    selectedSectionCostCenter: selectedSectionCostCenter,
                    selectedQty: selectedQty,
                    selectedProductQuanity: selectedProductQuanity,
                    totalProductPrice: totalProductPrice,
                    //#endregion Utility Methods
                    //#region Estimate Screen
                    initializeEstimate: initializeEstimate,
                    isEstimateScreen: isEstimateScreen,
                    //#endregion
                    //#region Dialog Product Section
                    orderProductItems: orderProductItems,
                    getOrders: getOrders,
                    getOrdersOfCurrentScreen: getOrdersOfCurrentScreen,
                    getOrdersOnTabChange: getOrdersOnTabChange,
                    //#region Product From Retail Store
                    updateItemsDataOnItemSelection: updateItemsDataOnItemSelection,
                    onCreateNewProductFromRetailStore: onCreateNewProductFromRetailStore,
                    onCloseProductFromRetailStore: onCloseProductFromRetailStore,
                    getItemsByCompanyId: getItemsByCompanyId,
                    onAddCostCenter: onAddCostCenter,
                    onCloseCostCenterDialog: closeCostCenterDialog,
                    costCentres: costCentres,
                    getCostCenters: getCostCenters,
                    costCentrefilterText: costCentrefilterText,
                    resetCostCentrefilter: resetCostCentrefilter,
                    costCenterClickListner: costCenterClickLIstner,
                    selectedCostCentre: selectedCostCentre,
                    hideCostCentreQuantityDialog: hideCostCentreQuantityDialog,
                    hideCostCentreDialog: hideCostCentreDialog,
                    selectedSection: selectedSection,
                    baseCharge1Total: baseCharge1Total,
                    baseCharge2Total: baseCharge2Total,
                    baseCharge3Total: baseCharge3Total,
                    markups: markups,
                    selectedMarkup1: selectedMarkup1,
                    selectedMarkup2: selectedMarkup2,
                    selectedMarkup3: selectedMarkup3,
                    categories: categories,
                    selectedCategoryId: selectedCategoryId,
                    categoryPager: categoryPager,
                    inventorySearchFilter: inventorySearchFilter,
                    getInventoriesListItems: getInventoriesListItems,
                    onChangeQty1MarkUpId: onChangeQty1MarkUpId,
                    onChangeQty2MarkUpId: onChangeQty2MarkUpId,
                    onChangeQty3MarkUpId: onChangeQty3MarkUpId,
                    vatList: vatList,
                    //#endregion
                    //#region Pre Payment
                    paymentMethods: paymentMethods,
                    onCreateOrderPrePayment: onCreateOrderPrePayment,
                    onCancelOrderPrePayment: onCancelOrderPrePayment,
                    currencySymbol: currencySymbol,
                    selectedPrePayment: selectedPrePayment,
                    onSavePrePayment: onSavePrePayment,
                    onEditPrePayment: onEditPrePayment,
                    inventoryItems: inventoryItems,
                    flagForToShowAddTitle: flagForToShowAddTitle,
                    //#endregion
                    isCompanyBaseDataLoaded: isCompanyBaseDataLoaded,
                    side1ButtonClick: side1ButtonClick,
                    side2ButtonClick: side2ButtonClick,
                    getPtvCalculation: getPtvCalculation,
                    openInkDialog: openInkDialog,
                    //#endregion
                    //#region Delivery Schedule
                    selectDeliverySchedule: selectDeliverySchedule,
                    addDeliverySchedule: addDeliverySchedule,
                    selectedDeliverySchedule: selectedDeliverySchedule,
                    templateToUseDeliverySchedule: templateToUseDeliverySchedule,
                    onRaised: onRaised,
                    getPtvPlan: getPtvPlan,
                    getBestPress: getBestPress,
                    onDeleteDeliveryScheduleItem: onDeleteDeliveryScheduleItem,
                    //#endregion
                    //#region Section Detail
                    availableInkPlateSides: availableInkPlateSides,
                    paperSizes: paperSizes,
                    openStockItemDialog: openStockItemDialog,
                    getSectionSystemCostCenters: getSectionSystemCostCenters,
                    doBeforeRunningWizard: doBeforeRunningWizard,
                    bestPressList: bestPressList,
                    userCostCenters: userCostCenters,
                    selectBestPressFromWizard: selectBestPressFromWizard,
                    selectedBestPressFromWizard: selectedBestPressFromWizard,
                    clickOnWizardOk: clickOnWizardOk,
                    runWizard: runWizard,
                    downloadArtwork: downloadArtwork,
                    //#endregion
                    itemAttachmentFileLoadedCallback: itemAttachmentFileLoadedCallback,
                };
            })()
        };
        return ist.order.viewModel;
    });
