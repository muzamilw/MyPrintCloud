/*
    Module with the model for the Product
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
     // Item Entity
    // ReSharper disable InconsistentNaming
    Item = function (specifiedId, specifiedName, specifiedCode, specifiedProductName, specifiedProductCode, specifiedThumbnail, specifiedMinPrice,
        specifiedIsArchived, specifiedIsPublished, specifiedProductCategoryName, specifiedIsEnabled, specifiedIsFeatured, specifiedIsFinishedGoods,
        specifiedSortOrder, specifiedIsStockControl, specifiedIsVdpProduct, specifiedXeroAccessCode, specifiedWebDescription, specifiedProductSpecification,
        specifiedTipsAndHints, specifiedMetaTitle, specifiedMetaDescription, specifiedMetaKeywords, specifiedJobDescriptionTitle1, specifiedJobDescription1,
        specifiedJobDescriptionTitle2, specifiedJobDescription2, specifiedJobDescriptionTitle3, specifiedJobDescription3, specifiedJobDescriptionTitle4,
        specifiedJobDescription4, specifiedJobDescriptionTitle5, specifiedJobDescription5, specifiedJobDescriptionTitle6, specifiedJobDescription6,
        specifiedJobDescriptionTitle7, specifiedJobDescription7, specifiedJobDescriptionTitle8, specifiedJobDescription8, specifiedJobDescriptionTitle9,
        specifiedJobDescription9, specifiedJobDescriptionTitle10, specifiedJobDescription10, specifiedGridImage, specifiedImagePath, specifiedFile1,
        specifiedFlagId, specifiedIsQtyRanged, specifiedPackagingWeight, specifiedDefaultItemTax, callbacks, constructorParams) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Product Name
            productName = ko.observable(specifiedProductName || undefined),
            // Product Name For Grid
            productNameForGrid = ko.computed(function () {
                if (!productName()) {
                    return "";
                }
                return productName().length > 30 ? productName().substring(0, 29) : productName();
            }),
            // Product Code
            productCode = ko.observable(specifiedProductCode || undefined),
            // thumbnail
            thumbnail = ko.observable(specifiedThumbnail || undefined),
            // grid image
            gridImage = ko.observable(specifiedGridImage || undefined),
            // image path
            imagePath = ko.observable(specifiedImagePath || undefined),
            // file 1
            file1 = ko.observable(specifiedFile1 || undefined),
            // mini Price
            miniPrice = ko.observable(specifiedMinPrice || undefined),
            // is archived
            isArchived = ko.observable(specifiedIsArchived || undefined),
            // Is Archived Ui
            isArchivedUi = ko.computed(function () {
                return isArchived() ? "Yes" : "No";
            }),
            // is published
            isPublished = ko.observable(specifiedIsPublished || undefined),
            // Is Published Ui
            isPublishedUi = ko.computed(function () {
                return isPublished() ? "Yes" : "No";
            }),
            // product Category Name
            productCategoryName = ko.observable(specifiedProductCategoryName || undefined),
            // is featured
            isFeatured = ko.observable(specifiedIsFeatured || undefined),
            // is finished goods
            isFinishedGoods = ko.observable(specifiedIsFinishedGoods !== undefined && specifiedIsFinishedGoods != null ?
            (specifiedIsFinishedGoods === 0 ? 0 : specifiedIsFinishedGoods) : undefined),
            // is finished goods for ui
            isFinishedGoodsUi = ko.computed({
                read: function () {
                    if (isFinishedGoods() === undefined || isFinishedGoods() === null) {
                        return '1';
                    }
                    if (isFinishedGoods() === 0) {
                        return '3';
                    }
                    return '' + isFinishedGoods();
                },
                write: function (value) {
                    var finishedGoods = parseInt(value);
                    if (finishedGoods === isFinishedGoods()) {
                        return;
                    }

                    isFinishedGoods(finishedGoods);
                }
            }),
            // is vdp product
            isVdpProduct = ko.observable(specifiedIsVdpProduct || undefined),
            // is stock control
            isStockControl = ko.observable(specifiedIsStockControl || undefined),
            // is enabled
            isEnabled = ko.observable(specifiedIsEnabled || undefined),
            // sort order
            sortOrder = ko.observable(specifiedSortOrder || undefined),
            // xero access code
            xeroAccessCode = ko.observable(specifiedXeroAccessCode || undefined),
            // web Description
            webDescription = ko.observable(specifiedWebDescription || undefined),
            // product Specification
            productSpecification = ko.observable(specifiedProductSpecification || undefined),
            // tips and hints
            tipsAndHints = ko.observable(specifiedTipsAndHints || undefined),
            // meta title
            metaTitle = ko.observable(specifiedMetaTitle || undefined),
            // meta description
            metaDescription = ko.observable(specifiedMetaDescription || undefined),
            // meta keywords
            metaKeywords = ko.observable(specifiedMetaKeywords || undefined),
            // job description title1
            jobDescriptionTitle1 = ko.observable(specifiedJobDescriptionTitle1 || undefined),
            // job description title2
            jobDescriptionTitle2 = ko.observable(specifiedJobDescriptionTitle2 || undefined),
            // job description title3
            jobDescriptionTitle3 = ko.observable(specifiedJobDescriptionTitle3 || undefined),
            // job description title4
            jobDescriptionTitle4 = ko.observable(specifiedJobDescriptionTitle4 || undefined),
            // job description title5
            jobDescriptionTitle5 = ko.observable(specifiedJobDescriptionTitle5 || undefined),
            // job description title6
            jobDescriptionTitle6 = ko.observable(specifiedJobDescriptionTitle6 || undefined),
            // job description title7
            jobDescriptionTitle7 = ko.observable(specifiedJobDescriptionTitle7 || undefined),
            // job description title8
            jobDescriptionTitle8 = ko.observable(specifiedJobDescriptionTitle8 || undefined),
            // job description title9
            jobDescriptionTitle9 = ko.observable(specifiedJobDescriptionTitle9 || undefined),
            // job description title10
            jobDescriptionTitle10 = ko.observable(specifiedJobDescriptionTitle10 || undefined),
            // job description 1
            jobDescription1 = ko.observable(specifiedJobDescription1 || undefined),
            // job description 2
            jobDescription2 = ko.observable(specifiedJobDescription2 || undefined),
            // job description 3
            jobDescription3 = ko.observable(specifiedJobDescription3 || undefined),
            // job description 4
            jobDescription4 = ko.observable(specifiedJobDescription4 || undefined),
            // job description 5
            jobDescription5 = ko.observable(specifiedJobDescription5 || undefined),
            // job description 6
            jobDescription6 = ko.observable(specifiedJobDescription6 || undefined),
            // job description 7
            jobDescription7 = ko.observable(specifiedJobDescription7 || undefined),
            // job description 8
            jobDescription8 = ko.observable(specifiedJobDescription8 || undefined),
            // job description 9
            jobDescription9 = ko.observable(specifiedJobDescription9 || undefined),
            // job description 10
            jobDescription10 = ko.observable(specifiedJobDescription10 || undefined),
            // Is Template tabs Visible
            isTemplateTabsVisible = ko.computed(function () {
                return isFinishedGoodsUi() === '1';
            }),
            // Flag Id
            flagId = ko.observable(specifiedFlagId || undefined),
            // Is Qty Ranged
            isQtyRanged = ko.observable(specifiedIsQtyRanged != null && specifiedIsQtyRanged != undefined ? specifiedIsQtyRanged : undefined),
            // Is Qty Ranged for ui
            isQtyRangedUi = ko.computed({
                read: function () {
                    if (isQtyRanged() === 0) {
                        return '2';
                    }
                    return '' + isQtyRanged();
                },
                write: function (value) {
                    var qtyRanged = parseInt(value);
                    if (qtyRanged === isQtyRanged()) {
                        return;
                    }

                    isQtyRanged(qtyRanged === 2 ? 0 : qtyRanged);
                }
            }),
            // Packaging Weight
            packagingWeight = ko.observable(specifiedPackagingWeight || undefined),
            // Default Item Tax
            defaultItemTax = ko.observable(specifiedDefaultItemTax || undefined),
            // Item Vdp Prices
            itemVdpPrices = ko.observableArray([]),
            // Item Videos
            itemVideos = ko.observableArray([]),
            // Item Related Items
            itemRelatedItems = ko.observableArray([]),
            // Template 
            template = ko.observable(Template.Create({})),
            // Item Stock options
            itemStockOptions = ko.observableArray([]),
            // Item Price Matrices
            itemPriceMatrices = ko.observableArray([]),
            // Item State Taxes
            itemStateTaxes = ko.observableArray([]),
            // Active Stock Option
            activeStockOption = ko.observable(),
            // Stock Option Sequence 1
            stockOptionSequence1 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 0 || stockOption.optionSequence() === 1;
                });
            }),
            // Stock Option Sequence 2
            stockOptionSequence2 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 1 || stockOption.optionSequence() === 2;
                });
            }),
            // Stock Option Sequence 3
            stockOptionSequence3 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 2 || stockOption.optionSequence() === 3;
                });
            }),
            // Stock Option Sequence 4
            stockOptionSequence4 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 3 || stockOption.optionSequence() === 4;
                });
            }),
            // Stock Option Sequence 5
            stockOptionSequence5 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 4 || stockOption.optionSequence() === 5;
                });
            }),
            // Stock Option Sequence 6
            stockOptionSequence6 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 5 || stockOption.optionSequence() === 6;
                });
            }),
            // Stock Option Sequence 7
            stockOptionSequence7 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 6 || stockOption.optionSequence() === 7;
                });
            }),
            // Stock Option Sequence 8
            stockOptionSequence8 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 7 || stockOption.optionSequence() === 8;
                });
            }),
            // Stock Option Sequence 9
            stockOptionSequence9 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 8 || stockOption.optionSequence() === 9;
                });
            }),
            // Stock Option Sequence 10
            stockOptionSequence10 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 9 || stockOption.optionSequence() === 10;
                });
            }),
            // Stock Option Sequence 11
            stockOptionSequence11 = ko.computed(function () {
                return itemStockOptions.find(function (stockOption, index) {
                    return index === 10 || stockOption.optionSequence() === 11;
                });
            }),
            // choose stock item
            chooseStockItem = function (stockOption) {
                selectItemStockOption(stockOption);

                if (callbacks && callbacks.onChooseStockItem && typeof callbacks.onChooseStockItem === "function") {
                    callbacks.onChooseStockItem();
                }
            },
            // Select Item Stock Option
            selectItemStockOption = function (stockOption) {
                if (activeStockOption() !== stockOption) {
                    activeStockOption(stockOption);
                }
            },
            // On Select Stock Item
            onSelectStockItem = function (stockItem) {
                activeStockOption().selectStock(stockItem);
                activeStockOption(ItemStockOption.Create({}, callbacks));

                if (callbacks && callbacks.onSelectStockItem && typeof callbacks.onSelectStockItem === "function") {
                    callbacks.onSelectStockItem();
                }
            },
            // Can Add Item Vdp Price
            canAddItemVdpPrice = ko.computed(function () {
                return itemVdpPrices.length < 15;
            }),
            // Add Item Vdp Price
            addItemVdpPrice = function () {
                itemVdpPrices.push(ItemVdpPrice.Create({ ItemId: id() }));
            },
            // Remove Item Vdp Price
            removeItemVdpPrice = function (itemVdpPrice) {
                itemVdpPrices.remove(itemVdpPrice);
            },
            // Add Video
            addVideo = function (video) {
                itemVideos.splice(0, 0, video);
                if (callbacks && callbacks.onSaveVideo && typeof callbacks.onSaveVideo === "function") {
                    callbacks.onSaveVideo();
                }
            },
            // Remove Item Video
            removeItemVideo = function (itemVideo) {
                itemVideos.remove(itemVideo);
            },
            // On Select Related Item
            onSelectRelatedItem = function (relatedItem) {
                var relatedProduct = itemRelatedItems.find(function (item) {
                    return item.relatedItemId() == relatedItem.relatedItemId();
                });

                if (!relatedProduct) {
                    itemRelatedItems.splice(0, 0, ItemRelatedItem.Create({
                        ItemId: id(),
                        RelatedItemId: relatedItem.relatedItemId(),
                        RelatedItemName: relatedItem.relatedItemName()
                    }));
                }

                if (callbacks && callbacks.onSelectRelatedItem && typeof callbacks.onSelectRelatedItem === "function") {
                    callbacks.onSelectRelatedItem();
                }
            },
            // Remove Item Related Item
            removeItemRelatedItem = function (item) {
                itemRelatedItems.remove(item);
            },
            // Add Item Stock Option
            addItemStockOption = function () {
                itemStockOptions.push(ItemStockOption.Create({ ItemId: id() }, callbacks));
            },
            // Remove Item Stock Option
            removeItemStockOption = function (itemStockOption) {
                itemStockOptions.remove(itemStockOption);
            },
            // On Add Item Cost Centre
            onAddItemCostCentre = function (itemStockOption) {
                selectItemStockOption(itemStockOption);
                activeStockOption().onAddItemAddonCostCentre();

                if (callbacks && callbacks.onUpdateItemAddonCostCentre && typeof callbacks.onUpdateItemAddonCostCentre === "function") {
                    callbacks.onUpdateItemAddonCostCentre();
                }
            },
            // On Edit Item Cost Centre
            onEditItemCostCentre = function (itemStockOption, itemAddonCostCentre) {
                selectItemStockOption(itemStockOption);
                activeStockOption().onEditItemAddonCostCentre(itemAddonCostCentre);

                if (callbacks && callbacks.onUpdateItemAddonCostCentre && typeof callbacks.onUpdateItemAddonCostCentre === "function") {
                    callbacks.onUpdateItemAddonCostCentre();
                }
            },
            // On Save Item Cost Centre
            onSaveItemCostCentre = function () {
                activeStockOption().saveItemAddonCostCentre();

                if (callbacks && callbacks.onSaveItemAddonCostCentre && typeof callbacks.onSaveItemAddonCostCentre === "function") {
                    callbacks.onSaveItemAddonCostCentre();
                }
            },
            // Add Item State Tax
            addItemStateTax = function () {
                itemStateTaxes.push(ItemStateTax.Create({ ItemId: id() }, callbacks, constructorParams));
            },
            // Remove Item State Tax
            removeItemStateTax = function (itemStateTax) {
                itemStateTaxes.remove(itemStateTax);
            },
            // Selected Price Matrix Item
            selectedPriceMatrixItem = ko.observable(),
            // Select Price Matrix Item
            selectPriceMatrixItem = function(priceMatrixItem) {
                if (selectedPriceMatrixItem() === priceMatrixItem) {
                    return;
                }

                selectedPriceMatrixItem(priceMatrixItem);
            },
            // Choose Template for Price Matrix
            chooseTemplateForPriceMatrix = function(priceMatrixItem) {
                return selectedPriceMatrixItem() === priceMatrixItem ? 'editPriceMatrixTemplate' : 'itemPriceMatrixTemplate';
            },
            // Selected State Tax Item
            selectedStateTaxItem = ko.observable(),
            // Select State Tax Item
            selectStateTaxItem = function (stateTaxItem) {
                if (selectedStateTaxItem() === stateTaxItem) {
                    return;
                }

                selectedStateTaxItem(stateTaxItem);
            },
            // Choose Template for State Tax
            chooseTemplateForStateTax = function (stateTaxItem) {
                return selectedStateTaxItem() === stateTaxItem ? 'editStateTaxTemplate' : 'itemStateTaxTemplate';
            },
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && itemVdpPrices.filter(function (itemVdpPrice) {
                    return !itemVdpPrice.isValid();
                }).length === 0 &&
                itemStockOptions.filter(function (itemStockOption) {
                    return !itemStockOption.isValid();
                }).length === 0;
            }),
            // True if the product has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                code: code,
                productName: productName,
                productCode: productCode,
                isArchived: isArchived,
                isPublished: isPublished,
                isEnabled: isEnabled,
                isFeatured: isFeatured,
                isVdpProduct: isVdpProduct,
                isStockControl: isStockControl,
                sortOrder: sortOrder,
                isFinishedGoods: isFinishedGoods,
                xeroAccessCode: xeroAccessCode,
                webDescription: webDescription,
                productSpecification: productSpecification,
                tipsAndHints: tipsAndHints,
                metaTitle: metaTitle,
                metaDescription: metaDescription,
                metaKeywords: metaKeywords,
                jobDescriptionTitle1: jobDescriptionTitle1,
                jobDescriptionTitle2: jobDescriptionTitle2,
                jobDescriptionTitle3: jobDescriptionTitle3,
                jobDescriptionTitle4: jobDescriptionTitle4,
                jobDescriptionTitle5: jobDescriptionTitle5,
                jobDescriptionTitle6: jobDescriptionTitle6,
                jobDescriptionTitle7: jobDescriptionTitle7,
                jobDescriptionTitle8: jobDescriptionTitle8,
                jobDescriptionTitle9: jobDescriptionTitle9,
                jobDescriptionTitle10: jobDescriptionTitle10,
                jobDescription1: jobDescription1,
                jobDescription2: jobDescription2,
                jobDescription3: jobDescription3,
                jobDescription4: jobDescription4,
                jobDescription5: jobDescription5,
                jobDescription6: jobDescription6,
                jobDescription7: jobDescription7,
                jobDescription8: jobDescription8,
                jobDescription9: jobDescription9,
                jobDescription10: jobDescription10,
                flagId: flagId,
                isQtyRanged: isQtyRanged,
                packagingWeight: packagingWeight,
                defaultItemTax: defaultItemTax,
                itemVdpPrices: itemVdpPrices,
                itemVideos: itemVideos,
                itemRelatedItems: itemRelatedItems,
                template: template,
                itemStockOptions: itemStockOptions,
                itemPriceMatrices: itemPriceMatrices,
                itemStateTaxes: itemStateTaxes
            }),
            // Item Vdp Prices has changes
            itemVdpPriceListHasChanges = ko.computed(function () {
                return itemVdpPrices.find(function (itemVdpPrice) {
                    return itemVdpPrice.hasChanges();
                }) != null;
            }),
            // Item Videos Has Changes
            itemVideosHasChanges = ko.computed(function () {
                return itemVideos.find(function (itemVideo) {
                    return itemVideo.hasChanges();
                }) != null;
            }),
            // Item Stock Option Changes
            itemStockOptionHasChanges = ko.computed(function () {
                return itemStockOptions.find(function (itemStockOption) {
                    return itemStockOption.hasChanges();
                }) != null;
            }),
            // Item Price Matrix Changes
            itemPriceMatrixHasChanges = ko.computed(function () {
                return itemPriceMatrices.find(function (itemPriceMatrix) {
                    return itemPriceMatrix.hasChanges();
                }) != null;
            }),
            // Item State Taxes Changes
            itemStateTaxesHasChanges = ko.computed(function () {
                return itemStateTaxes.find(function (itemStateTax) {
                    return itemStateTax.hasChanges();
                }) != null;
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || itemVdpPriceListHasChanges() || itemVideosHasChanges() || template().hasChanges() || itemStockOptionHasChanges() ||
                    itemPriceMatrixHasChanges() || itemStateTaxesHasChanges();
            }),
            // Reset
            reset = function () {
                itemVdpPrices.each(function (itemVdpPrice) {
                    return itemVdpPrice.reset();
                });
                itemVideos.each(function (itemVideo) {
                    return itemVideo.reset();
                });
                itemStockOptions.each(function (itemStockOption) {
                    return itemStockOption.reset();
                });
                itemPriceMatrices.each(function (itemPriceMatrix) {
                    return itemPriceMatrix.reset();
                });
                itemStateTaxes.each(function (itemStateTax) {
                    return itemStateTax.reset();
                });
                template().reset();
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemId: id(),
                    ItemCode: code(),
                    ProductCode: productCode(),
                    ProductName: productName(),
                    IsArchived: isArchived(),
                    IsEnabled: isEnabled(),
                    IsPublished: isPublished(),
                    IsFeatured: isFeatured(),
                    IsVdpProduct: isVdpProduct(),
                    IsStockControl: isStockControl(),
                    SortOrder: sortOrder(),
                    ProductType: isFinishedGoodsUi() === '3' ? 0 : parseInt(isFinishedGoodsUi()),
                    XeroAccessCode: xeroAccessCode(),
                    WebDescription: webDescription(),
                    ProductSpecification: productSpecification(),
                    TipsAndHints: tipsAndHints(),
                    MetaTitle: metaTitle(),
                    MetaDescription: metaDescription(),
                    MetaKeywords: metaKeywords(),
                    JobDescriptionTitle1: jobDescriptionTitle1(),
                    JobDescriptionTitle2: jobDescriptionTitle2(),
                    JobDescriptionTitle3: jobDescriptionTitle3(),
                    JobDescriptionTitle4: jobDescriptionTitle4(),
                    JobDescriptionTitle5: jobDescriptionTitle5(),
                    JobDescriptionTitle6: jobDescriptionTitle6(),
                    JobDescriptionTitle7: jobDescriptionTitle7(),
                    JobDescriptionTitle8: jobDescriptionTitle8(),
                    JobDescriptionTitle9: jobDescriptionTitle9(),
                    JobDescriptionTitle10: jobDescriptionTitle10(),
                    JobDescription1: jobDescription1(),
                    JobDescription2: jobDescription2(),
                    JobDescription3: jobDescription3(),
                    JobDescription4: jobDescription4(),
                    JobDescription5: jobDescription5(),
                    JobDescription6: jobDescription6(),
                    JobDescription7: jobDescription7(),
                    JobDescription8: jobDescription8(),
                    JobDescription9: jobDescription9(),
                    JobDescription10: jobDescription10(),
                    FlagId: flagId(),
                    IsQtyRanged: isQtyRanged(),
                    PackagingWeight: packagingWeight(),
                    DefaultItemTax: defaultItemTax(),
                    ItemVdpPrices: itemVdpPrices.map(function (itemVdpPrice) {
                        return itemVdpPrice.convertToServerData();
                    }),
                    ItemVideos: itemVideos.map(function (itemVideo) {
                        return itemVideo.convertToServerData();
                    }),
                    ItemRelatedItems: itemRelatedItems.map(function (itemRelatedItem) {
                        return itemRelatedItem.convertToServerData();
                    }),
                    ItemStockOptions: itemStockOptions.map(function (itemStockOption, index) {
                        var stockOption = itemStockOption.convertToServerData();
                        stockOption.OptionSequence = index + 1;
                        return stockOption;
                    }),
                    ItemPriceMatrices: itemPriceMatrices.map(function (itemPriceMatrix) {
                        return itemPriceMatrix.convertToServerData();
                    }),
                    ItemStateTaxes: itemStateTaxes.map(function (itemStateTax) {
                        return itemStateTax.convertToServerData();
                    }),
                    Template: template().convertToServerData()
                }
            };

        return {
            id: id,
            name: name,
            code: code,
            productName: productName,
            productNameForGrid: productNameForGrid,
            productCode: productCode,
            thumbnail: thumbnail,
            gridImage: gridImage,
            imagePath: imagePath,
            file1: file1,
            miniPrice: miniPrice,
            isArchived: isArchived,
            isPublished: isPublished,
            isArchivedUi: isArchivedUi,
            isPublishedUi: isPublishedUi,
            productCategoryName: productCategoryName,
            isEnabled: isEnabled,
            isFeatured: isFeatured,
            isVdpProduct: isVdpProduct,
            isStockControl: isStockControl,
            sortOrder: sortOrder,
            isFinishedGoods: isFinishedGoods,
            isFinishedGoodsUi: isFinishedGoodsUi,
            itemVdpPrices: itemVdpPrices,
            xeroAccessCode: xeroAccessCode,
            webDescription: webDescription,
            productSpecification: productSpecification,
            tipsAndHints: tipsAndHints,
            metaTitle: metaTitle,
            metaDescription: metaDescription,
            metaKeywords: metaKeywords,
            jobDescriptionTitle1: jobDescriptionTitle1,
            jobDescriptionTitle2: jobDescriptionTitle2,
            jobDescriptionTitle3: jobDescriptionTitle3,
            jobDescriptionTitle4: jobDescriptionTitle4,
            jobDescriptionTitle5: jobDescriptionTitle5,
            jobDescriptionTitle6: jobDescriptionTitle6,
            jobDescriptionTitle7: jobDescriptionTitle7,
            jobDescriptionTitle8: jobDescriptionTitle8,
            jobDescriptionTitle9: jobDescriptionTitle9,
            jobDescriptionTitle10: jobDescriptionTitle10,
            jobDescription1: jobDescription1,
            jobDescription2: jobDescription2,
            jobDescription3: jobDescription3,
            jobDescription4: jobDescription4,
            jobDescription5: jobDescription5,
            jobDescription6: jobDescription6,
            jobDescription7: jobDescription7,
            jobDescription8: jobDescription8,
            jobDescription9: jobDescription9,
            jobDescription10: jobDescription10,
            isTemplateTabsVisible: isTemplateTabsVisible,
            flagId: flagId,
            isQtyRangedUi: isQtyRangedUi,
            isQtyRanged: isQtyRanged,
            packagingWeight: packagingWeight,
            defaultItemTax: defaultItemTax,
            itemVideos: itemVideos,
            itemRelatedItems: itemRelatedItems,
            canAddItemVdpPrice: canAddItemVdpPrice,
            addItemVdpPrice: addItemVdpPrice,
            removeItemVdpPrice: removeItemVdpPrice,
            addVideo: addVideo,
            removeItemVideo: removeItemVideo,
            onSelectRelatedItem: onSelectRelatedItem,
            removeItemRelatedItem: removeItemRelatedItem,
            template: template,
            itemStockOptions: itemStockOptions,
            stockOptionSequence1: stockOptionSequence1,
            stockOptionSequence2: stockOptionSequence2,
            stockOptionSequence3: stockOptionSequence3,
            stockOptionSequence4: stockOptionSequence4,
            stockOptionSequence5: stockOptionSequence5,
            stockOptionSequence6: stockOptionSequence6,
            stockOptionSequence7: stockOptionSequence7,
            stockOptionSequence8: stockOptionSequence8,
            stockOptionSequence9: stockOptionSequence9,
            stockOptionSequence10: stockOptionSequence10,
            stockOptionSequence11: stockOptionSequence11,
            itemStateTaxes: itemStateTaxes,
            itemPriceMatrices: itemPriceMatrices,
            addItemStockOption: addItemStockOption,
            removeItemStockOption: removeItemStockOption,
            chooseStockItem: chooseStockItem,
            activeStockOption: activeStockOption,
            onSelectStockItem: onSelectStockItem,
            onAddItemCostCentre: onAddItemCostCentre,
            onEditItemCostCentre: onEditItemCostCentre,
            onSaveItemCostCentre: onSaveItemCostCentre,
            addItemStateTax: addItemStateTax,
            removeItemStateTax: removeItemStateTax,
            chooseTemplateForPriceMatrix: chooseTemplateForPriceMatrix,
            selectedPriceMatrixItem: selectedPriceMatrixItem,
            selectPriceMatrixItem: selectPriceMatrixItem,
            chooseTemplateForStateTax: chooseTemplateForStateTax,
            selectedStateTaxItem: selectedStateTaxItem,
            selectStateTaxItem: selectStateTaxItem,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            itemVdpPriceListHasChanges: itemVdpPriceListHasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item VdpPrice Entity
    ItemVdpPrice = function (specifiedId, specifiedClickRangeTo, specifiedClickRangeFrom, specifiedPricePerClick, specifiedSetupCharge, specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // click Range To
            clickRangeTo = ko.observable(specifiedClickRangeTo || undefined),
            // Click Range From
            clickRangeFrom = ko.observable(specifiedClickRangeFrom || undefined),
            // Price Per Click
            pricePerClick = ko.observable(specifiedPricePerClick || undefined),
            // Setup Charge
            setupCharge = ko.observable(specifiedSetupCharge || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                clickRangeFrom: clickRangeFrom,
                clickRangeTo: clickRangeTo,
                pricePerClick: pricePerClick,
                setupCharge: setupCharge
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemVdpPriceId: id(),
                    ItemId: itemId(),
                    ClickRangeFrom: clickRangeFrom(),
                    ClickRangeTo: clickRangeTo(),
                    PricePerClick: pricePerClick(),
                    SetupCharge: setupCharge()
                }
            };

        return {
            id: id,
            itemId: itemId,
            clickRangeFrom: clickRangeFrom,
            clickRangeTo: clickRangeTo,
            pricePerClick: pricePerClick,
            setupCharge: setupCharge,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item Video Entity
    ItemVideo = function (specifiedId, specifiedVideoLink, specifiedCaption, specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Video link
            videoLink = ko.observable(specifiedVideoLink || undefined),
            // Caption
            caption = ko.observable(specifiedCaption || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                videoLink: videoLink,
                caption: caption
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    VideoId: id(),
                    ItemId: itemId(),
                    Caption: caption(),
                    VideoLink: videoLink()
                }
            };

        return {
            id: id,
            itemId: itemId,
            videoLink: videoLink,
            caption: caption,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item Related Item Entity
    ItemRelatedItem = function (specifiedId, specifiedRelatedItemId, specifiedRelatedItemName, specifiedRelatedItemCode, specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // RelatedItem id
            relatedItemId = ko.observable(specifiedRelatedItemId || 0),
            // RelatedItem Name
            relatedItemName = ko.observable(specifiedRelatedItemName || undefined),
            // RelatedItem Code
            relatedItemCode = ko.observable(specifiedRelatedItemCode || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    Id: id(),
                    ItemId: itemId(),
                    RelatedItemId: relatedItemId()
                }
            };

        return {
            id: id,
            itemId: itemId,
            relatedItemId: relatedItemId,
            relatedItemName: relatedItemName,
            relatedItemCode: relatedItemCode,
            errors: errors,
            isValid: isValid,
            convertToServerData: convertToServerData
        };
    },

    // Template Entity
    // ReSharper disable InconsistentNaming
    Template = function (specifiedId, specifiedPdfTemplateWidth, specifiedPdfTemplateHeight) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Pdf Template Width
            pdfTemplateWidth = ko.observable(specifiedPdfTemplateWidth || undefined),
            // Pdf Template Height
            pdfTemplateHeight = ko.observable(specifiedPdfTemplateHeight || undefined),
            // Template Pages
            templatePages = ko.observableArray([]),
            // Add Template Page
            addTemplatePage = function () {
                templatePages.push(TemplatePage.Create({ ProductId: id() }));
            },
            // Remove Template Page
            removeTemplatePage = function (templatePage) {
                templatePages.remove(templatePage);
            },
            // Move Template Page Up
            moveTemplatePageUp = function (templatePage) {
                var i = templatePages.indexOf(templatePage);
                if (i >= 1) {
                    var array = templatePages();
                    templatePages.splice(i - 1, 2, array[i], array[i - 1]);
                }
            },
            // Move Template Page Down
            moveTemplatePageDown = function (templatePage) {
                var i = templatePages.indexOf(templatePage);
                var array = templatePages();
                if (i < array.length) {
                    templatePages.splice(i, 2, array[i + 1], array[i]);
                }
            },
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 || templatePages.filter(function (templatePage) {
                    return !templatePage.isValid();
                }).length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                pdfTemplateWidth: pdfTemplateWidth,
                pdfTemplateHeight: pdfTemplateHeight,
                templatePages: templatePages
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || templatePages.find(function (templatePage) {
                    return templatePage.hasChanges();
                }) != null;
            }),
            // Reset
            reset = function () {
                // Reset Template Page State to Un-Modified
                templatePages.each(function (templatePage) {
                    return templatePage.reset();
                });
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ProductId: id(),
                    PdfTemplateWidth: pdfTemplateWidth(),
                    PdfTemplateHeight: pdfTemplateHeight(),
                    TemplatePages: templatePages.map(function (templatePage, index) {
                        var templatePageItem = templatePage.convertToServerData();
                        templatePageItem.PageNo = index + 1;
                        return templatePageItem;
                    })
                }
            };

        return {
            id: id,
            pdfTemplateWidth: pdfTemplateWidth,
            pdfTemplateHeight: pdfTemplateHeight,
            templatePages: templatePages,
            addTemplatePage: addTemplatePage,
            removeTemplatePage: removeTemplatePage,
            moveTemplatePageUp: moveTemplatePageUp,
            moveTemplatePageDown: moveTemplatePageDown,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Template Page Entity
    TemplatePage = function (specifiedId, specifiedWidth, specifiedHeight, specifiedPageName, specifiedPageNo, specifiedProductId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Width
            width = ko.observable(specifiedWidth || undefined),
            // Height
            height = ko.observable(specifiedHeight || undefined),
            // Page Name
            pageName = ko.observable(specifiedPageName || undefined),
            // Page No
            pageNo = ko.observable(specifiedPageNo || undefined),
            // Product Id
            productId = ko.observable(specifiedProductId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                width: width,
                height: height,
                pageName: pageName,
                pageNo: pageNo
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ProductPageId: id(),
                    Width: width(),
                    Height: height(),
                    PageName: pageName(),
                    PageNo: pageNo(),
                    ProductId: productId()
                }
            };

        return {
            id: id,
            productId: productId,
            width: width,
            height: height,
            pageName: pageName,
            pageNo: pageNo,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item Stock Option Entity
    ItemStockOption = function (specifiedId, specifiedStockLabel, specifiedStockId, specifiedStockItemName, specifiedStockItemDescription, specifiedImage,
        specifiedOptionSequence, specifiedItemId, callbacks) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Label
            label = ko.observable(specifiedStockLabel || undefined),
            // Stock Item Id
            stockItemId = ko.observable(specifiedStockId || undefined),
            // Stock Item Name
            stockItemName = ko.observable(specifiedStockItemName || undefined),
            // Stock Item Description
            stockItemDescription = ko.observable(specifiedStockItemDescription || undefined),
            // image
            image = ko.observable(specifiedImage || undefined),
            // Option Sequence
            optionSequence = ko.observable(specifiedOptionSequence || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || undefined),
            // Item Addon Cost Centers
            itemAddonCostCentres = ko.observableArray([]),
            // Active Video Item
            activeItemAddonCostCentre = ko.observable(ItemAddonCostCentre.Create({}, callbacks)),
            // Added ItemAddonCostCentre Counter
            itemAddonCostCentreCounter = -1,
            // On Add ItemAddonCostCentre
            onAddItemAddonCostCentre = function () {
                activeItemAddonCostCentre(ItemAddonCostCentre.Create({ ProductAddOnId: 0, ItemStockOptionId: id() }, callbacks));
            },
            onEditItemAddonCostCentre = function (itemAddonCostCentre) {
                activeItemAddonCostCentre(itemAddonCostCentre);
            },
            // Save ItemAddonCostCentre
            saveItemAddonCostCentre = function () {
                if (activeItemAddonCostCentre().id() === 0) { // Add
                    activeItemAddonCostCentre().id(itemAddonCostCentreCounter);
                    addItemAddonCostCentre(activeItemAddonCostCentre());
                    itemAddonCostCentreCounter -= 1;
                    return;
                }
            },
            // Add Item Addon Cost Center
            addItemAddonCostCentre = function (itemAddonCostCentre) {
                itemAddonCostCentres.splice(0, 0, itemAddonCostCentre);
            },
            // Remove ItemAddon Cost Centre
            removeItemAddonCostCentre = function (itemAddonCostCentre) {
                itemAddonCostCentres.remove(itemAddonCostCentre);
            },
            // Select Stock Item
            selectStock = function (stockItem) {
                if (!stockItem || stockItemId() === stockItem.id) {
                    return;
                }

                stockItemId(stockItem.id);
                stockItemName(stockItem.name);
                stockItemDescription(stockItem.description);
            },
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 || itemAddonCostCentres.filter(function (itemAddonCostCentre) {
                    return !itemAddonCostCentre.isValid();
                }).length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                stockItemId: stockItemId,
                label: label,
                itemAddonCostCentres: itemAddonCostCentres
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || itemAddonCostCentres.find(function (itemAddonCostCentre) {
                    return itemAddonCostCentre.hasChanges();
                }) != null;
            }),
            // Reset
            reset = function () {
                // Reset Item Addon Cost Centres State to Un-Modified
                itemAddonCostCentres.each(function (itemAddonCostCentre) {
                    return itemAddonCostCentre.reset();
                });
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemStockOptionId: id(),
                    StockLabel: label(),
                    StockId: stockItemId(),
                    ItemId: itemId(),
                    OptionSequence: optionSequence(),
                    ItemAddOnCostCentres: itemAddonCostCentres.map(function (itemAddonCostCentre) {
                        return itemAddonCostCentre.convertToServerData();
                    })
                }
            };

        return {
            id: id,
            stockItemId: stockItemId,
            label: label,
            stockItemName: stockItemName,
            stockItemDescription: stockItemDescription,
            itemId: itemId,
            image: image,
            optionSequence: optionSequence,
            activeItemAddonCostCentre: activeItemAddonCostCentre,
            itemAddonCostCentres: itemAddonCostCentres,
            addItemAddonCostCentre: addItemAddonCostCentre,
            removeItemAddonCostCentre: removeItemAddonCostCentre,
            onAddItemAddonCostCentre: onAddItemAddonCostCentre,
            onEditItemAddonCostCentre: onEditItemAddonCostCentre,
            saveItemAddonCostCentre: saveItemAddonCostCentre,
            selectStock: selectStock,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item Addon Cost Centre Entity
    ItemAddonCostCentre = function (specifiedId, specifiedIsMandatory, specifiedItemStockOptionId, specifiedCostCentreId, specifiedCostCentreName,
        specifiedCostCentreType, callbacks) {
        // ReSharper restore InconsistentNaming
        var
            // self reference
            self,
            // Unique key
            id = ko.observable(specifiedId),
            // is Mandatory
            isMandatory = ko.observable(specifiedIsMandatory || undefined),
            // Cost Centre Id
            internalCostCentreId = ko.observable(specifiedCostCentreId || undefined),
            // Cost Centre Name
            costCentreName = ko.observable(specifiedCostCentreName || undefined),
            // Cost Centre Type
            costCentreType = ko.observable(specifiedCostCentreType || undefined),
            // Cost Centre Id - On Change
            costCentreId = ko.computed({
                read: function () {
                    return internalCostCentreId();
                },
                write: function (value) {
                    if (!value || value === internalCostCentreId()) {
                        return;
                    }

                    internalCostCentreId(value);
                    if (callbacks && callbacks.onCostCentreChange && typeof callbacks.onCostCentreChange === "function") {
                        callbacks.onCostCentreChange(value, self);
                    }
                }
            }),
            // Item Stock Option Id
            itemStockOptionId = ko.observable(specifiedItemStockOptionId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                isMandatory: isMandatory,
                costCentreId: costCentreId
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ProductAddOnId: id(),
                    IsMandatory: isMandatory(),
                    CostCentreId: costCentreId(),
                    ItemStockOptionId: itemStockOptionId()
                }
            };

        self = {
            id: id,
            itemStockOptionId: itemStockOptionId,
            costCentreId: costCentreId,
            costCentreName: costCentreName,
            costCentreType: costCentreType,
            isMandatory: isMandatory,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };

        return self;
    },

    // Stock Item Entity        
    StockItem = function (specifiedId, specifiedName, specifiedCategoryName, specifiedLocation, specifiedWeight, specifiedDescription) {
        return {
            id: specifiedId,
            name: specifiedName,
            category: specifiedCategoryName,
            location: specifiedLocation,
            weight: specifiedWeight,
            description: specifiedDescription
        }
    },

    // Cost Centre Entity        
    CostCentre = function (specifiedId, specifiedName, specifiedType) {
        return {
            id: specifiedId,
            name: specifiedName,
            type: specifiedType
        }
    },

    // Country Entity        
    Country = function (specifiedId, specifiedName, specifiedCode) {
        return {
            id: specifiedId,
            name: specifiedName,
            type: specifiedCode
        }
    },

    // State Entity        
    State = function (specifiedId, specifiedName, specifiedCode, specifiedCountryId) {
        return {
            id: specifiedId,
            name: specifiedName,
            type: specifiedCode,
            countryId: specifiedCountryId
        }
    },

    // Section Flag Entity        
    SectionFlag = function (specifiedId, specifiedFlagName, specifiedFlagColor) {
        return {
            id: specifiedId,
            name: specifiedFlagName,
            color: specifiedFlagColor
        }
    },

    // Item Price Matrix Entity
    ItemPriceMatrix = function (specifiedId, specifiedQuantity, specifiedQtyRangedFrom, specifiedQtyRangedTo, specifiedPricePaperType1, specifiedPricePaperType2,
        specifiedPricePaperType3, specifiedPriceStockType4, specifiedPriceStockType5, specifiedPriceStockType6, specifiedPriceStockType7, specifiedPriceStockType8,
        specifiedPriceStockType9, specifiedPriceStockType10, specifiedPriceStockType11, specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Quantity
            quantity = ko.observable(specifiedQuantity || undefined),
            // Qty Ranged From
            qtyRangedFrom = ko.observable(specifiedQtyRangedFrom || undefined),
            // Qty Ranged To
            qtyRangedTo = ko.observable(specifiedQtyRangedTo || undefined),
            // Price Paper Type1
            pricePaperType1 = ko.observable(specifiedPricePaperType1 || undefined),
            // Price Paper Type1 Ui
            pricePaperType1Ui = ko.computed(function () {
                if (!pricePaperType1()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType1();
            }),
            // Price Paper Type2
            pricePaperType2 = ko.observable(specifiedPricePaperType2 || undefined),
            // Price Paper Type2 Ui
            pricePaperType2Ui = ko.computed(function () {
                if (!pricePaperType2()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType2();
            }),
            // Price Paper Type3
            pricePaperType3 = ko.observable(specifiedPricePaperType3 || undefined),
            // Price Paper Type3 Ui
            pricePaperType3Ui = ko.computed(function () {
                if (!pricePaperType3()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType3();
            }),
            // Price Stock Type4
            priceStockType4 = ko.observable(specifiedPriceStockType4 || undefined),
            // Price Stock Type4 Ui
            priceStockType4Ui = ko.computed(function () {
                if (!priceStockType4()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType4();
            }),
            // Price Stock Type5
            priceStockType5 = ko.observable(specifiedPriceStockType5 || undefined),
            // Price Stock Type5 Ui
            priceStockType5Ui = ko.computed(function () {
                if (!priceStockType5()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType5();
            }),
            // Price Stock Type6
            priceStockType6 = ko.observable(specifiedPriceStockType6 || undefined),
            // Price Stock Type6 Ui
            priceStockType6Ui = ko.computed(function () {
                if (!priceStockType6()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType6();
            }),
            // Price Stock Type7
            priceStockType7 = ko.observable(specifiedPriceStockType7 || undefined),
            // Price Stock Type7 Ui
            priceStockType7Ui = ko.computed(function () {
                if (!priceStockType7()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType7();
            }),
            // Price Stock Type8
            priceStockType8 = ko.observable(specifiedPriceStockType8 || undefined),
            // Price Stock Type8 Ui
            priceStockType8Ui = ko.computed(function () {
                if (!priceStockType8()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType8();
            }),
            // Price Stock Type9
            priceStockType9 = ko.observable(specifiedPriceStockType9 || undefined),
            // Price Stock Type9 Ui
            priceStockType9Ui = ko.computed(function () {
                if (!priceStockType4()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType9();
            }),
            // Price Stock Type10
            priceStockType10 = ko.observable(specifiedPriceStockType10 || undefined),
            // Price Stock Type10 Ui
            priceStockType10Ui = ko.computed(function () {
                if (!priceStockType10()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType10();
            }),
            // Price Stock Type11
            priceStockType11 = ko.observable(specifiedPriceStockType11 || undefined),
            // Price Stock Type11 Ui
            priceStockType11Ui = ko.computed(function () {
                if (!priceStockType11()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType11();
            }),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                quantity: quantity,
                qtyRangedFrom: qtyRangedFrom,
                qtyRangedTo: qtyRangedTo,
                pricePaperType1: pricePaperType1,
                pricePaperType2: pricePaperType2,
                pricePaperType3: pricePaperType3,
                priceStockType4: priceStockType4,
                priceStockType5: priceStockType5,
                priceStockType6: priceStockType6,
                priceStockType7: priceStockType7,
                priceStockType8: priceStockType8,
                priceStockType9: priceStockType9,
                priceStockType10: priceStockType10,
                priceStockType11: priceStockType11
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    PriceMatrixId: id(),
                    ItemId: itemId(),
                    Quantity: quantity(),
                    QtyRangedFrom: qtyRangedFrom(),
                    QtyRangedTo: qtyRangedTo(),
                    PricePaperType1: pricePaperType1(),
                    PricePaperType2: pricePaperType2(),
                    PricePaperType3: pricePaperType3(),
                    PriceStockType4: priceStockType4(),
                    PriceStockType5: priceStockType5(),
                    PriceStockType6: priceStockType6(),
                    PriceStockType7: priceStockType7(),
                    PriceStockType8: priceStockType8(),
                    PriceStockType9: priceStockType9(),
                    PriceStockType10: priceStockType10(),
                    PriceStockType11: priceStockType11()
                }
            };

        return {
            id: id,
            itemId: itemId,
            quantity: quantity,
            qtyRangedFrom: qtyRangedFrom,
            qtyRangedTo: qtyRangedTo,
            pricePaperType1: pricePaperType1,
            pricePaperType2: pricePaperType2,
            pricePaperType3: pricePaperType3,
            priceStockType4: priceStockType4,
            priceStockType5: priceStockType5,
            priceStockType6: priceStockType6,
            priceStockType7: priceStockType7,
            priceStockType8: priceStockType8,
            priceStockType9: priceStockType9,
            priceStockType10: priceStockType10,
            priceStockType11: priceStockType11,
            pricePaperType1Ui: pricePaperType1Ui,
            pricePaperType2Ui: pricePaperType2Ui,
            pricePaperType3Ui: pricePaperType3Ui,
            priceStockType4Ui: priceStockType4Ui,
            priceStockType5Ui: priceStockType5Ui,
            priceStockType6Ui: priceStockType6Ui,
            priceStockType7Ui: priceStockType7Ui,
            priceStockType8Ui: priceStockType8Ui,
            priceStockType9Ui: priceStockType9Ui,
            priceStockType10Ui: priceStockType10Ui,
            priceStockType11Ui: priceStockType11Ui,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Item State Tax Entity
    ItemStateTax = function (specifiedId, specifiedCountryId, specifiedStateId, specifiedTaxRate, specifiedCountryName, specifiedStateName, specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Country Id
            internalCountryId = ko.observable(specifiedCountryId || undefined),
            // Country Name
            countryName = ko.observable(specifiedCountryName || undefined),
            // State Name
            stateName = ko.observable(specifiedStateName || undefined),
            // State Id
            internalStateId = ko.observable(specifiedStateId || undefined),
            // Tax Rate
            taxRate = ko.observable(specifiedTaxRate || undefined),
            // Tax Rate Ui
            taxRateUi = ko.computed(function() {
                return taxRate() ? '$ ' + taxRate() : '$ 0';
            }),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Countries
            countries = ko.observableArray([]),
            // States
            states = ko.observableArray([]),
            // Country Id
            countryId = ko.computed({
                read: function () {
                    return internalCountryId();
                },
                write: function (value) {
                    if (!value || internalCountryId() === value) {
                        return;
                    }

                    internalCountryId(value);

                    var countryResult = countries.filter(function (country) {
                        return country.id === value;
                    });

                    if (!countryResult) {
                        return;
                    }

                    countryName(countryResult.name);
                }
            }),
            // State Id
            stateId = ko.computed({
                read: function () {
                    return internalStateId();
                },
                write: function (value) {
                    if (!value || internalStateId() === value) {
                        return;
                    }

                    internalStateId(value);

                    var stateResult = states.filter(function (state) {
                        return state.id === value;
                    });

                    if (!stateResult) {
                        return;
                    }

                    stateName(stateResult.name);
                }
            }),
            // Country States
            countryStates = ko.computed(function () {
                if (!countryId()) {
                    return [];
                }

                return states.filter(function (state) {
                    return state.countryid === countryId();
                });
            }),
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item State Tax has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                taxRate: taxRate,
                countryId: countryId,
                stateId: stateId
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ItemStateTaxId: id(),
                    ItemId: itemId(),
                    CountryId: countryId(),
                    StateId: stateId(),
                    TaxRate: taxRate()
                }
            };

        return {
            id: id,
            itemId: itemId,
            taxRate: taxRate,
            taxRateUi: taxRateUi,
            countryId: countryId,
            stateId: stateId,
            countryName: countryName,
            stateName: stateName,
            countries: countries,
            states: states,
            countryStates: countryStates,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    };

    // Item Vdp Price Factory
    ItemVdpPrice.Create = function (source) {
        return new ItemVdpPrice(source.ItemVdpPriceId, source.ClickRangeTo, source.ClickRangeFrom, source.PricePerClick, source.SetupCharge, source.ItemId);
    }

    // Item Video Factory
    ItemVideo.Create = function (source) {
        return new ItemVideo(source.VideoId, source.VideoLink, source.Caption, source.ItemId);
    }

    // Item Related Item Factory
    ItemRelatedItem.Create = function (source) {
        return new ItemRelatedItem(source.Id, source.RelatedItemId, source.RelatedItemName, source.RelatedItemCode, source.ItemId);
    }

    // Template Page Factory
    TemplatePage.Create = function (source) {
        return new TemplatePage(source.ProductPageId, source.Width, source.Height, source.PageName, source.PageNo, source.ProductId);
    }

    // Template Factory
    Template.Create = function (source) {
        var template = new Template(source.ProductId, source.PdfTemplateWidth, source.PdfTemplateHeight, source.ItemId);

        // Map Template Pages if any
        if (source.TemplatePages != null) {
            var templatePages = [];

            // Sort by PageNo
            source.TemplatePages.sort(function (a, b) {
                return a.PageNo > b.PageNo ? 1 : -1;
            });

            _.each(source.TemplatePages, function (templatePage) {
                templatePages.push(TemplatePage.Create(templatePage));
            });

            // Push to original array
            ko.utils.arrayPushAll(template.templatePages(), templatePages);
            template.templatePages.valueHasMutated();
        }

        // Reset template state to Un-Modified
        template.reset();

        return template;
    }

    // Item Addon Cost Centre Factory
    ItemAddonCostCentre.Create = function (source, callbacks) {
        return new ItemAddonCostCentre(source.ProductAddOnId, source.IsMandatory, source.ItemStockOptionId, source.CostCentreId, source.CostCentreName,
            source.CostCentreType, callbacks);
    }

    // Item Stock Option Factory
    ItemStockOption.Create = function (source, callbacks) {
        var itemStockOption = new ItemStockOption(source.ItemStockOptionId, source.StockLabel, source.StockId, source.StockItemName, source.StockItemDescription,
            source.ImageSource, source.ItemId, callbacks);

        // If Item Addon CostCentres exists then add
        if (source.ItemAddOnCostCentres) {
            var itemAddonCostCentres = [];

            _.each(source.ItemAddOnCostCentres, function (itemAddonCostCenter) {
                itemAddonCostCentres.push(ItemAddonCostCentre.Create(itemAddonCostCenter, callbacks));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(itemStockOption.itemAddonCostCentres(), itemAddonCostCentres);
            itemStockOption.itemAddonCostCentres.valueHasMutated();
        }

        // Reset State to Un-Modified
        itemStockOption.reset();

        return itemStockOption;
    }

    // Item State Tax Factory
    ItemStateTax.Create = function (source, callbacks, constructorParams) {
        var itemStateTax = new ItemStateTax(source.ItemStateTaxId, source.CountryId, source.StateId, source.TaxRate, source.CountryName,
            source.StateName, source.ItemId, callbacks);

        if (constructorParams) {
            itemStateTax.countries(constructorParams.countries || []);
            itemStateTax.states(constructorParams.states || []);
        }

        return itemStateTax;
    }

    // Item Price Matrix Factory
    ItemPriceMatrix.Create = function (source) {
        return new ItemPriceMatrix(source.PriceMatrixId, source.Quantity, source.QtyRangedFrom, source.QtyRangedTo, source.PricePaperType1, source.PricePaperType2,
            source.PricePaperType3, source.PriceStockType4, source.PriceStockType5, source.PriceStockType6, source.PriceStockType7, source.PriceStockType8,
            source.PriceStockType9, source.PriceStockType10, source.PriceStockType11, source.ItemId);
    }

    // Item Factory
    Item.Create = function (source, callbacks, constructorParams) {
        var item = new Item(source.ItemId, source.ItemName, source.ItemCode, source.ProductName, source.ProductCode, source.ThumbnailImageSource, source.MinPrice,
            source.IsArchived, source.IsPublished, source.ProductCategoryName, source.IsEnabled, source.IsFeatured, source.ProductType, source.SortOrder,
            source.IsStockControl, source.IsVdpProduct, source.XeroAccessCode, source.WebDescription, source.ProductSpecification, source.TipsAndHints,
            source.MetaTitle, source.MetaDescription, source.MetaKeywords, source.JobDescriptionTitle1, source.JobDescription1, source.JobDescriptionTitle2,
            source.JobDescription2, source.JobDescriptionTitle3, source.JobDescription3, source.JobDescriptionTitle4, source.JobDescription4,
            source.JobDescriptionTitle5, source.JobDescription5, source.JobDescriptionTitle6, source.JobDescription6, source.JobDescriptionTitle7,
            source.JobDescription7, source.JobDescriptionTitle8, source.JobDescription8, source.JobDescriptionTitle9, source.JobDescription9,
            source.JobDescriptionTitle10, source.JobDescription10, source.GridImageSource, source.ImagePathImageSource, source.File1BytesSource, source.FlagId,
            source.IsQtyRanged, source.PackagingWeight, source.DefaultItemTax, callbacks, constructorParams);

        // Map Item Vdp Prices if any
        if (source.ItemVdpPrices && source.ItemVdpPrices.length > 0) {
            var itemVdpPrices = [];

            _.each(source.ItemVdpPrices, function (itemVdpPrice) {
                itemVdpPrices.push(ItemVdpPrice.Create(itemVdpPrice));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemVdpPrices(), itemVdpPrices);
            item.itemVdpPrices.valueHasMutated();
        }

        // Map Item Videos if any
        if (source.ItemVideos && source.ItemVideos.length > 0) {
            var itemVideos = [];

            _.each(source.ItemVideos, function (itemVideo) {
                itemVideos.push(ItemVideo.Create(itemVideo));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemVideos(), itemVideos);
            item.itemVideos.valueHasMutated();
        }

        // Map Item Related Items if any
        if (source.ItemRelatedItems && source.ItemRelatedItems.length > 0) {
            var itemRelatedItems = [];

            _.each(source.ItemRelatedItems, function (itemRelatedItem) {
                itemRelatedItems.push(ItemRelatedItem.Create(itemRelatedItem));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemRelatedItems(), itemRelatedItems);
            item.itemRelatedItems.valueHasMutated();
        }

        // Map Template
        if (source.Template != null) {
            item.template(Template.Create(source.Template));
        }

        // Map Item Stock Options if any
        if (source.ItemStockOptions && source.ItemStockOptions.length > 0) {
            var itemStockOptions = [];

            _.each(source.ItemStockOptions, function (itemStockOption) {
                itemStockOptions.push(ItemStockOption.Create(itemStockOption, callbacks));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemStockOptions(), itemStockOptions);
            item.itemStockOptions.valueHasMutated();
        }

        // Map Item Related Items if any
        if (source.ItemPriceMatrices && source.ItemPriceMatrices.length > 0) {
            var itemPriceMatrices = [];

            _.each(source.ItemPriceMatrices, function (itemPriceMatrix) {
                itemPriceMatrices.push(ItemPriceMatrix.Create(itemPriceMatrix));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemPriceMatrices(), itemPriceMatrices);
            item.itemPriceMatrices.valueHasMutated();
        }

        // Map Item State Taxes if any
        if (source.ItemStateTaxes && source.ItemStateTaxes.length > 0) {
            var itemStateTaxes = [];

            _.each(source.ItemStateTaxes, function (itemStateTax) {
                itemStateTaxes.push(ItemStateTax.Create(itemStateTax, callbacks, constructorParams));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemStateTaxes(), itemStateTaxes);
            item.itemStateTaxes.valueHasMutated();
        }

        // Reset State to Un-Modified
        item.reset();

        return item;
    }

    // Stock Item Factory
    StockItem.Create = function (source) {
        return new StockItem(source.StockItemId, source.ItemName, source.CategoryName, source.StockLocation, source.ItemWeight, source.ItemDescription);
    }

    // Cost Centre Factory
    CostCentre.Create = function (source) {
        return new CostCentre(source.CostCentreId, source.Name, source.TypeName);
    }

    // Country Factory
    Country.Create = function (source) {
        return new Country(source.CountryId, source.CountryName, source.CountryCode);
    }

    // State Factory
    State.Create = function (source) {
        return new State(source.StateId, source.StateName, source.StateCode, source.CountryId);
    }

    // Section Flag Factory
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagColor);
    }

    return {
        // Item Constructor
        Item: Item,
        // Item Vdp Price Constructor
        ItemVdpPrice: ItemVdpPrice,
        // Item Video Constructor
        ItemVideo: ItemVideo,
        // Item Related Item Consturctor
        ItemRelatedItem: ItemRelatedItem,
        // Template Constructor
        Template: Template,
        // Template Page Constructor
        TemplatePage: TemplatePage,
        // Item Stock Option Constructor
        ItemStockOption: ItemStockOption,
        // Item Addon CostCentre Constructor
        ItemAddonCostCentre: ItemAddonCostCentre,
        // Stock Item Constructor
        StockItem: StockItem,
        // Cost Centre Constructor
        CostCentre: CostCentre,
        // Country Constructor
        Country: Country,
        // State Constructor
        State: State,
        // Section Flag Constructor
        SectionFlag: SectionFlag
    };
});