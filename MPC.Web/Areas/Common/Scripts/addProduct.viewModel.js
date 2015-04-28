/*
    Module with the view model for Product
*/
define("common/addProduct.viewModel",
    ["jquery", "amplify", "ko", "common/addProduct.dataservice", "common/addProduct.model", "common/pagination"], function ($, amplify, ko, dataservice, model,
    pagination) {
        var ist = window.ist || {};
        ist.addProduct = {
            viewModel: (function () {
                var // The view 
                    view,
                    // Product Items
                    orderProductItems = ko.observableArray([]),
                    costCentresFromOrders = null,
                    productQuantitiesList = ko.observableArray([]),
                    //Selected item
                    selecteditem = ko.observable(),
                    //Selected Stock Option Sequence Number
                    selectedStockOptionSequenceNumber = ko.observable(),
                    //SelectedStockOption
                    selectedStockOption = ko.observable(),
                    // Active Cost Center
                    selectedCostCentre = ko.observable(),
                    //Selected Stock Item
                    selectedStockItem = ko.observable(),
                    //Selected Product Quanity 
                    selectedProductQuanity = ko.observable(),
                    //Selected Stock Option Name
                    selectedStockOptionName = ko.observable(),
                    //Total Product Price
                    totalProductPrice = ko.observable(0),
                    // Reset Cost center Items
                   
                    // after selection
                    afterAddCostCenter = null,
                    currencySymbol = ko.observable(),
                    
                    // Show
                show = function (afterAddCostCenterCallback, companyId, costCentresBaseData, currencySym) {
                    currencySymbol(currencySym);
                        afterAddCostCenter = afterAddCostCenterCallback;
                        costCentresFromOrders = costCentresBaseData;
                        view.showProductFromRetailStoreModal();
                        getItemsByCompanyId(companyId);
                    },
                    
                    // On Select fcosCost Center
                    onSelectCostCenter = function(costCenter) {
                        selectedCostCentre(costCenter);
                        view.showCostCentersQuantityDialog();
                    },
                    // Initialize the view model
                    initialize = function(specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);
                    },
                 
                    //Get Items By CompanyId
                    getItemsByCompanyId = function (companyId) {
                       
                        dataservice.getItemsByCompanyId({
                                CompanyId: companyId
                            }, {
                                success: function(data) {
                                    if (data != null) {
                                        orderProductItems.removeAll();
                                        _.each(data.Items, function(item) {
                                            var itemToBePushed = new model.Item.Create(item);
                                            orderProductItems.push(itemToBePushed);
                                        });
                                        //Select First Item by Default if list is not empty
                                        if (orderProductItems().length > 0) {
                                            updateItemsDataOnItemSelection(orderProductItems()[0]);
                                        }
                                    }
                                },
                                error: function(response) {
                                    toastr.error("Failed to Load Company Products . Error: " + response);
                                }
                            });
                    },                    
                    //Update Items Data On Item Selection
                    //Get Item Stock Options and Items Price Matrix against this item's id(itemId)
                    updateItemsDataOnItemSelection = function (item) {
                        var v = item;
                        dataservice.getItemsDetailsByItemId({
                                itemId: item.id()
                            }, {
                                success: function(data) {
                                    if (data != null) {
                                        item.itemStockOptions.removeAll();
                                        item.itemPriceMatrices.removeAll();
                                        item.itemSections.removeAll();
                                        productQuantitiesList.removeAll();
                                        _.each(data.ItemStockOptions, function(itemStockoption) {
                                            var itemToBePushed = new model.ItemStockOption.Create(itemStockoption);
                                            item.itemStockOptions.push(itemToBePushed);
                                        });
                                        _.each(data.ItemPriceMatrices, function(itemPriceMatrix) {
                                            var itemToBePushed = new model.ItemPriceMatrix.Create(itemPriceMatrix);
                                            item.itemPriceMatrices.push(itemToBePushed);
                                            if (item.isQtyRanged() == 2) {
                                                productQuantitiesList.push(itemToBePushed.quantity());
                                            }
                                        });
                                        if (data.ItemSection != null) {
                                            var itemSectionToBePushed = new model.ItemSection.Create(data.ItemSection);
                                            item.itemSections.push(itemSectionToBePushed);
                                        }


                                        selecteditem(item);
                                    }
                                },
                                error: function(response) {
                                    toastr.error("Failed to Load Company Products . Error: " + response);
                                }
                            });
                    },
                    createNewRetailStoreProduct = function () {
                                var item = selecteditem().convertToServerData();
                                var newItem = model.Item.Create(item);
                                newItem.id(0);
                                newItem.qty1NetTotal(totalProductPrice());
                              
                                addSelectedAddOnsAsCostCenters(newItem);
                                afterAddCostCenter(newItem);
                    },
                    onSaveRetailStoreProduct = function () {
                                createNewRetailStoreProduct();
                                onCloseProductFromRetailStore();
                    },
                      onCloseProductFromRetailStore = function () {
                          view.hideProductFromRetailStoreModal();
                      },
                     //req: In retail store case add selected addons as cost centers of new creating product
                            //and make new cost center of name 'Web order Cost Center' in any case
                    addSelectedAddOnsAsCostCenters = function(newItem) {
                                
                                //#region Add Default Web Order Cost Center
                                var sectionCostCenter = model.SectionCostCentre.Create({});
                                //sectionCostCenter.id();
                                sectionCostCenter.name('Web Order Cost Center');
                                sectionCostCenter.qty1EstimatedStockCost(0);
                                sectionCostCenter.qty2EstimatedStockCost(0);
                                sectionCostCenter.qty3EstimatedStockCost(0);
                                
                                sectionCostCenter.qty2Charge(0);
                                sectionCostCenter.qty3Charge(0);
                                sectionCostCenter.qty1(selectedProductQuanity());

                                //sectionCostCenter.costCentreType();
                                var counter = 0;
                                var price = 0;
                                if (selecteditem() != undefined) {// && selecteditem().isQtyRanged() == 2
                                    _.each(selecteditem().itemPriceMatrices(), function (priceMatrix) {
                                        counter = counter + 1;
                                        if (priceMatrix.quantity() == selectedProductQuanity()) {
                                            price = getPrice(counter - 1, selectedStockOptionSequenceNumber());
                                        }
                                    });
                                }
                                sectionCostCenter.qty1Charge(price);
                                sectionCostCenter.costCentreId(getStockCostCenterId(29));
                                
                                //sectionCostCenter.qty1NetTotal(price);


                                newItem.itemSections()[0].sectionCostCentres.push(sectionCostCenter);
                                //#endregion

                                //#region Add Selected Addons as Cost Centers
                                if (selectedStockOption() != undefined && selectedStockOption().itemAddonCostCentres().length > 0) {
                                    _.each(selectedStockOption().itemAddonCostCentres(), function (stockOption) {
                                        if (stockOption.isSelected()) {
                                            sectionCostCenter = model.SectionCostCentre.Create({});
                                            //sectionCostCenter.id();
                                            sectionCostCenter.name(stockOption.costCentreName());
                                            sectionCostCenter.qty1EstimatedStockCost(0);
                                            sectionCostCenter.qty2EstimatedStockCost(0);
                                            sectionCostCenter.qty3EstimatedStockCost(0);
                                            sectionCostCenter.qty1Charge(stockOption.totalPrice());
                                            sectionCostCenter.qty2Charge(0);
                                            sectionCostCenter.qty3Charge(0);
                                            sectionCostCenter.qty1(1);
                                           
                                            sectionCostCenter.qty1NetTotal(stockOption.totalPrice());//todo 
                                            newItem.itemSections()[0].sectionCostCentres.push(sectionCostCenter);
                                        }
                                    });
                                }
                                //#endregion
                    },
                    
                     getStockCostCenterId = function (type) {
                         var costCentreId;
                         _.each(costCentresFromOrders, function (costCenter) {
                             if (costCenter.Type == type) {
                                 costCentreId = costCenter.CostCentreId;
                             }
                         });
                         return costCentreId;
                     },
                           getPrice = function (listElementNumber, count) {
                               if (count == 1) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].pricePaperType1();
                               }
                               else if (count == 2) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].pricePaperType2();
                               }
                               else if (count == 3) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].pricePaperType3();
                               }
                               else if (count == 4) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].priceStockType4();
                               }
                               else if (count == 5) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].priceStockType5();
                               }
                               else if (count == 6) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].priceStockType6();
                               }
                               else if (count == 7) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].priceStockType7();
                               }
                               else if (count == 8) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].priceStockType8();
                               }
                               else if (count == 9) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].priceStockType9();
                               }
                               else if (count == 10) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].priceStockType10();
                               }
                               else if (count == 11) {
                                   return selecteditem().itemPriceMatrices()[listElementNumber].priceStockType11();
                               }
                           },
                      //On Product From Retail Store update Item price matrix table and Add on Table 
                            updateViewOnStockOptionChange = ko.computed(function () {
                                if (selecteditem() == undefined || selecteditem().itemStockOptions == undefined) {
                                    return;
                                }
                                var count = 0;
                                _.each(selecteditem().itemStockOptions(), function (itemStockOption) {
                                    count = count + 1;
                                    if (itemStockOption.id() == selectedStockItem()) {
                                        selectedStockOptionName(itemStockOption.label());
                                        selectedStockOptionSequenceNumber(count);
                                        selectedStockOption(itemStockOption);
                                    }
                                });
                            })
                ,  //Calculate Total Price
                        calculateTotalPrice = ko.computed(function () {
                            //selecteditem().itemStockOptions()[0].itemAddonCostCentres()
                            //selectedStockOption().itemAddonCostCentres()
                            var totalPrice = 0;
                            var counter = 0;
                            if (selecteditem() != undefined && selecteditem().isQtyRanged() == 2) {
                                _.each(selecteditem().itemPriceMatrices(), function (priceMatrix) {
                                    counter = counter + 1;
                                    if (priceMatrix.quantity() == selectedProductQuanity()) {
                                        totalPrice = getPrice(counter - 1, selectedStockOptionSequenceNumber());
                                    }
                                });
                                if (selectedStockOption() != undefined && selectedStockOption().itemAddonCostCentres().length > 0) {
                                    _.each(selectedStockOption().itemAddonCostCentres(), function (stockOption) {
                                        if (stockOption.isSelected()) {
                                            totalPrice = totalPrice + stockOption.totalPrice();
                                        }
                                    });
                                }
                                totalProductPrice(totalPrice);
                            }
                            else if (selecteditem() != undefined && selecteditem().isQtyRanged() == 1) {
                                //totalPrice = parseInt(selectedProductQuanity());
                                var qtyInLimit = false;
                                counter = 0;
                                _.each(selecteditem().itemPriceMatrices(), function (priceMatrix) {
                                    counter = counter + 1;
                                    if (priceMatrix.qtyRangedFrom() <= parseInt(selectedProductQuanity()) && priceMatrix.qtyRangedTo() >= parseInt(selectedProductQuanity())) {
                                        totalPrice = getPrice(counter - 1, selectedStockOptionSequenceNumber());
                                    }
                                });
                                if (selectedStockOption() != undefined && selectedStockOption().itemAddonCostCentres().length > 0) {
                                    _.each(selectedStockOption().itemAddonCostCentres(), function (stockOption) {
                                        if (stockOption.isSelected()) {
                                            totalPrice = totalPrice + stockOption.totalPrice();
                                        }
                                    });
                                }
                                totalProductPrice(totalPrice);
                            }
                        })


                        ;
                return {
                    //Arrays
                    onSaveRetailStoreProduct:onSaveRetailStoreProduct,
                    //Utilities
                    onSelectCostCenter: onSelectCostCenter,
                    initialize: initialize,
                    show: show,
                    selectedCostCentre: selectedCostCentre,
                    selecteditem: selecteditem,
                    orderProductItems: orderProductItems,
                    selectedStockItem: selectedStockItem,
                    selectedStockOption: selectedStockOption,
                    selectedStockOptionSequenceNumber: selectedStockOptionSequenceNumber,
                    updateItemsDataOnItemSelection: updateItemsDataOnItemSelection,
                    selectedStockOptionName: selectedStockOptionName,
                    selectedProductQuanity: selectedProductQuanity,
                    totalProductPrice: totalProductPrice,
                    productQuantitiesList: productQuantitiesList,
                    costCentresFromOrders: costCentresFromOrders,
                    currencySymbol: currencySymbol
                };
            })()
        };

        return ist.addProduct.viewModel;
    });

