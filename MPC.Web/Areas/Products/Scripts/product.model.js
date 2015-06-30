/*
    Module with the model for the Product
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var
    // Item File Types
    itemFileTypes = {
        thumbnail: 1,
        grid: 2,
        imagePath: 3,
        file: 4
    },

    // Stock Category 
    stockCategory = {
        paper: 1,
        inks: 2,
        films: 3,
        plates: 4
    },

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
        specifiedFile2, specifiedFile3, specifiedFile4, specifiedFile5, specifiedFlagId, specifiedIsQtyRanged, specifiedPackagingWeight,
        specifiedDefaultItemTax, specifiedSupplierId, specifiedSupplierId2, specifiedEstimateProductionTime, specifiedItemProductDetail,
        specifiedIsTemplateDesignMode, specifiedDesignerCategoryId, specifiedScalar, specifiedZoomFactor, specifiedIsCMYK, specifiedTemplateType,
        specifiedProductDisplayOptions, specifiedIsRealStateProduct, specifiedIsUploadImage, specifiedIsDigitalDownload, specifiedPrintCropMarks,
        specifiedDrawWatermarkText, specifiedOrganisationId, specifiedCompanyId, specifiedIsAddCropMarks, specifiedDrawBleedArea, specifiedAllowPdfDownload,
        specifiedIsMultiPagePdf, specifiedAllowImageDownload, specifiedItemLength, specifiedItemWidth, specifiedItemHeight, specifiedItemWeight,
        specifiedTemplateId, specifiedSmartFormId, callbacks, constructorParams) {
        // ReSharper restore InconsistentNaming
        var
            // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Product Name
            productName = ko.observable(specifiedProductName || undefined).extend({ required: true }),
            // Product Name For Grid
            productNameForGrid = ko.computed(function () {
                if (!productName()) {
                    return "";
                }
                return productName().length > 30 ? productName().substring(0, 29) : productName();
            }),
            // Product Code
            productCode = ko.observable(specifiedProductCode || undefined).extend({ required: true }),
            // thumbnail
            thumbnail = ko.observable(specifiedThumbnail || undefined),
            // thumbnail File Name
            thumbnailFileName = ko.observable(),
            // thumbnail File Source
            thumbnailFileSource = ko.observable(),
            // grid image
            gridImage = ko.observable(specifiedGridImage || undefined),
            // grid Image File Name
            gridImageFileName = ko.observable(),
            // gridImage File Source
            gridImageFileSource = ko.observable(),
            // image path
            imagePath = ko.observable(specifiedImagePath || undefined),
            // imagePath File Name
            imagePathFileName = ko.observable(),
            // imagePath File Source
            imagePathFileSource = ko.observable(),
            // file 1
            file1 = ko.observable(specifiedFile1 || undefined),
            // file1 File Name
            file1FileName = ko.observable(),
            // file1 File Source
            file1FileSource = ko.observable(),
            // File 1 Deleted
            file1Deleted = ko.observable(false),
            // file 
            file2 = ko.observable(specifiedFile2 || undefined),
            // file2 File Name
            file2FileName = ko.observable(),
            // file2 File Source
            file2FileSource = ko.observable(),
            // File 2 Deleted
            file2Deleted = ko.observable(false),
            // file 3
            file3 = ko.observable(specifiedFile3 || undefined),
            // file3 File Name
            file3FileName = ko.observable(),
            // file3 File Source
            file3FileSource = ko.observable(),
            // File 3 Deleted
            file3Deleted = ko.observable(false),
            // file 4
            file4 = ko.observable(specifiedFile4 || undefined),
            // file4 File Name
            file4FileName = ko.observable(),
            // file4 File Source
            file4FileSource = ko.observable(),
            // File 4 Deleted
            file4Deleted = ko.observable(false),
            // file 5
            file5 = ko.observable(specifiedFile5 || undefined),
            // file5 File Name
            file5FileName = ko.observable(),
            // file5 File Source
            file5FileSource = ko.observable(),
            // File 5 Deleted
            file5Deleted = ko.observable(false),
            // mini Price
            miniPrice = ko.observable(specifiedMinPrice || 0),
            // is archived
            isArchived = ko.observable(specifiedIsArchived || undefined),
            // Is Archived Ui
            isArchivedUi = ko.computed(function () {
                return isArchived() ? "Yes" : "No";
            }),
            // is published
            isPublished = ko.observable(specifiedIsPublished !== undefined && specifiedIsPublished !== null ? specifiedIsPublished : true),
            // Is Published Ui
            isPublishedUi = ko.computed(function () {
                return isPublished() ? "Yes" : "No";
            }),
            isStoreTax = ko.observable(specifiedDefaultItemTax == undefined || specifiedDefaultItemTax == null ? 2 : 1),
            isStoreTaxUi = ko.computed({
                read: function () {

                    return '' + isStoreTax();
                },
                write: function (value) {
                    var storeTax = parseInt(value);
                    if (storeTax === isStoreTax()) {
                        return;
                    }
                    isStoreTax(storeTax);
                    defaultItemTax(0);
                }
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
                    if (finishedGoods !== 1) {
                        if (template()) {
                            template().isCreatedManual(undefined);
                        }
                    }
                    else {
                        if (templateType() === 1) {
                            template().isCreatedManual(true);
                        }
                    }
                }
            }),
            // is vdp product
            isVdpProduct = ko.observable(specifiedIsVdpProduct || undefined),
            // is stock control
            isStockControl = ko.observable(specifiedIsStockControl || undefined),
            // is enabled
            isEnabled = ko.observable(specifiedIsEnabled !== undefined && specifiedIsEnabled !== null ? specifiedIsEnabled : true),
            // sort order
            sortOrder = ko.observable(specifiedSortOrder || 1),
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
            jobDescriptionTitle1 = ko.observable(specifiedJobDescriptionTitle1 || "Origination"),
            // job description title2
            jobDescriptionTitle2 = ko.observable(specifiedJobDescriptionTitle2 || "Artwork"),
            // job description title3
            jobDescriptionTitle3 = ko.observable(specifiedJobDescriptionTitle3 || "Color"),
            // job description title4
            jobDescriptionTitle4 = ko.observable(specifiedJobDescriptionTitle4 || "Stock"),
            // job description title5
            jobDescriptionTitle5 = ko.observable(specifiedJobDescriptionTitle5 || "Size"),
            // job description title6
            jobDescriptionTitle6 = ko.observable(specifiedJobDescriptionTitle6 || "Special Instr."),
            // job description title7
            jobDescriptionTitle7 = ko.observable(specifiedJobDescriptionTitle7 || "Shipping"),
            // job description title8
            jobDescriptionTitle8 = ko.observable(specifiedJobDescriptionTitle8 || "Finishing"),
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
            internalFlagId = ko.observable(specifiedFlagId || undefined).extend({ required: true }),
            // Item Price Matrices For Existing flag
            itemPriceMatricesForExistingFlag = ko.observableArray([]),
            // Flag Id
            flagId = ko.computed({
                read: function () {
                    return internalFlagId();
                },
                write: function (value) {
                    if (!value || value === internalFlagId()) {
                        return;
                    }

                    // Keep track of item Price Matrices that are against existing flag
                    var itemPriceMatricesForFlag = itemPriceMatrices.filter(function (itemPriceMatrix) {
                        return itemPriceMatrix.flagId() === internalFlagId() && itemPriceMatrix.itemId() === id() && !itemPriceMatrix.supplierId();
                    });
                    if (itemPriceMatricesForFlag.length > 0) {
                        itemPriceMatricesForExistingFlag.removeAll();
                        ko.utils.arrayPushAll(itemPriceMatricesForExistingFlag(), itemPriceMatricesForFlag);
                        itemPriceMatricesForExistingFlag.valueHasMutated();
                    }

                    internalFlagId(value);

                    if (callbacks && callbacks.onFlagChange && typeof callbacks.onFlagChange === "function") {
                        callbacks.onFlagChange(value, id());
                    }
                }
            }),
            // Is Qty Ranged
            isQtyRanged = ko.observable(!specifiedIsQtyRanged ? 2 : 1),
            // Is Qty Ranged for ui
            isQtyRangedUi = ko.computed({
                read: function () {
                    return '' + isQtyRanged();
                },
                write: function (value) {
                    var qtyRanged = parseInt(value);
                    if (qtyRanged === isQtyRanged()) {
                        return;
                    }

                    isQtyRanged(qtyRanged);
                }
            }),
            // Packaging Weight
            packagingWeight = ko.observable(specifiedPackagingWeight || 0.1),
            // Default Item Tax
            defaultItemTax = ko.observable(specifiedDefaultItemTax || undefined),
            // supplier id
            internalSupplierId = ko.observable(specifiedSupplierId || undefined),
            // supplier id 2
            internalSupplierId2 = ko.observable(specifiedSupplierId2 || undefined),
            // Estimate Production Time
            estimateProductionTime = ko.observable(specifiedEstimateProductionTime || 3),
            // Is Template Design Mode
            isTemplateDesignMode = ko.observable(specifiedIsTemplateDesignMode || 1),
            // Is TemplateDesignMode for ui
            isTemplateDesignModeUi = ko.computed({
                read: function () {
                    return '' + isTemplateDesignMode();
                },
                write: function (value) {
                    if (!value) {
                        return;
                    }

                    var templateDesignMode = parseInt(value);
                    if (templateDesignMode === isTemplateDesignMode()) {
                        return;
                    }

                    isTemplateDesignMode(templateDesignMode);
                }
            }),
            // Is Template Design Mode
            isCmyk = ko.observable(specifiedIsCMYK ? 1 : 2),
            // Is Cmyk for ui
            isCmykUi = ko.computed({
                read: function () {
                    return '' + isCmyk();
                },
                write: function (value) {
                    if (!value) {
                        return;
                    }

                    var cmyk = parseInt(value);
                    if (cmyk === isCmyk()) {
                        return;
                    }

                    if (template()) {
                        if (cmyk === 2) {
                            template().isSpotTemplate(true);
                        }
                        else if (cmyk === 1) {
                            template().isSpotTemplate(false);
                        }
                    }

                    isCmyk(cmyk);
                }
            }),
            // Scalar
            scalar = ko.observable(specifiedScalar || 1).extend({ number: true }),
            // Zoom Factor
            zoomFactor = ko.observable(specifiedZoomFactor || undefined).extend({ number: true }),
            // Designer Category Id
            designerCategoryId = ko.observable(specifiedDesignerCategoryId || undefined),
            // Template Type
            templateType = ko.observable(specifiedTemplateType || 1),
            // Template Type Mode 
            templateTypeMode = ko.observable(),
            // Template Type Ui
            templateTypeUi = ko.computed({
                read: function () {
                    return '' + templateType();
                },
                write: function (value) {
                    if (!value) {
                        return;
                    }

                    var tempType = parseInt(value);

                    if (tempType === templateType()) {
                        return;
                    }

                    if (tempType === 2) {
                        // Changing from option 1 to 2
                        // Ask if want to keep old template objects or not
                        if (template() && template().id() && (specifiedTemplateType === 1)) {
                            // Set Mode to 1 if yes else set to 2 // Mode will be passed to generateTemplateFromPDF function                                
                            // that will decide whether to delete old template or not
                            if (callbacks && callbacks.onPreBuiltTemplateSelected && typeof callbacks.onPreBuiltTemplateSelected === "function") {
                                callbacks.onPreBuiltTemplateSelected();
                            }
                        }
                    }

                    templateType(tempType);
                    if (template()) {
                        if (tempType === 1) {
                            template().isCreatedManualUi(true);
                        }
                        else if (tempType === 2) {
                            template().isCreatedManualUi(false);
                            template().fileSource(undefined);
                        }
                        else {
                            template().isCreatedManualUi(undefined);
                        }
                    }
                }
            }),
            // Can Start Designer Empty
            canStartDesignerEmpty = ko.computed(function () {
                return templateType() === 3;
            }),
            // Product Display Options
            productDisplayOptions = ko.observable(specifiedProductDisplayOptions || 2),
            // Product DisplayOptions for ui
            productDisplayOptionsUi = ko.computed({
                read: function () {
                    return '' + productDisplayOptions();
                },
                write: function (value) {
                    if (!value) {
                        return;
                    }

                    var productDisplayOptionValue = parseInt(value);
                    if (productDisplayOptionValue === productDisplayOptions()) {
                        return;
                    }

                    productDisplayOptions(productDisplayOptionValue);
                }
            }),
            // Is Real State Product
            isRealStateProduct = ko.observable(specifiedIsRealStateProduct || false),
            // Is Upload Image
            isUploadImage = ko.observable(specifiedIsUploadImage || false),
            // Is Digital Download
            isDigitalDownload = ko.observable(specifiedIsDigitalDownload || false),
            // Print Crop Marks
            printCropMarks = ko.observable(specifiedPrintCropMarks !== undefined && specifiedPrintCropMarks !== null ? specifiedPrintCropMarks : true),
            // Draw Water Mark
            drawWatermarkText = ko.observable(specifiedDrawWatermarkText !== undefined && specifiedDrawWatermarkText !== null ? specifiedDrawWatermarkText : true),
            // Is Add Crop Marks
            isAddCropMarks = ko.observable(specifiedIsAddCropMarks || false),
            // Draw bleed area
            drawBleedArea = ko.observable(specifiedDrawBleedArea !== undefined && specifiedDrawBleedArea !== null ? specifiedDrawBleedArea : true),
            // Is Multipage Pdf
            isMultiPagePdf = ko.observable(specifiedIsMultiPagePdf || false),
            // Allow Pdf Download
            allowPdfDownload = ko.observable(specifiedAllowPdfDownload || false),
            // Allow Image Download
            allowImageDownload = ko.observable(specifiedAllowImageDownload !== undefined && specifiedAllowImageDownload !== null ? specifiedAllowImageDownload : true),
            // Item Length
            itemLength = ko.observable(specifiedItemLength || 0),
            // Item Height
            itemHeight = ko.observable(specifiedItemHeight || 0),
            // Item Width
            itemWidth = ko.observable(specifiedItemWidth || 0),
            // Item Weight
            itemWeight = ko.observable(specifiedItemWeight || 0),
            // Organisation Id
            organisationId = ko.observable(specifiedOrganisationId || undefined),
            // Company Id
            companyId = ko.observable(specifiedCompanyId || undefined),
            // Template Id
            templateId = ko.observable(specifiedTemplateId || undefined),
            // Smart Form Id
            smartFormId = ko.observable(specifiedSmartFormId || undefined),
            // Item Product Detail
            itemProductDetail = ko.observable(ItemProductDetail.Create(specifiedItemProductDetail || { ItemId: id() })),
            // Self Reference
            self = {
                productType: isFinishedGoodsUi
            },
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
            // Product Category Items
            productCategoryItems = ko.observableArray([]),
            // Product Category Element
            productCategoryElement = ko.observable(),
            // Item Images
            itemImages = ko.observableArray([]),
            // Product Market Brief Questions
            productMarketBriefQuestions = ko.observableArray([]),
            // Available Product Category items
            availableProductCategoryItems = ko.computed(function () {
                if (productCategoryItems().length === 0) {
                    return "";
                }

                var categories = "";
                productCategoryItems.each(function (pci, index) {
                    if (pci.isSelected()) {
                        var pcname = pci.categoryName();
                        if (index < productCategoryItems().length - 1) {
                            pcname = pcname + "<br/>";
                        }
                        categories += pcname;
                    }
                });

                return categories;
            }),
            // Item Sections
            itemSections = ko.observableArray([]),
            // Can Add Item Section
            canAddItemSection = ko.computed(function () {
                return itemProductDetail().isPrintItemUi() === '1' && itemSections().length < 5;
            }),
            // Can Remove Item Section
            canRemoveItemSection = ko.computed(function () {
                return itemSections().length > 1;
            }),
            // Update Item Section on Print Item Flag Change
            updateItemSectionOnPrintItemToggle = ko.computed(function () {
                if (itemProductDetail().isPrintItemUi() === '2') {
                    if (itemSections().length > 0 && !id()) {
                        // There shouldn't be any section in case of nonprint
                        itemSections.removeAll();
                    }
                    else if (itemSections().length > 1 && id()) {
                        // There should be one section
                        itemSections.splice(1, 4);
                    }
                }
                if (itemProductDetail().isPrintItemUi() === '1') {
                    if (itemSections().length === 0 && !id()) {
                        // There shouldn be atleast one section in case of print
                        itemSections.push(ItemSection.Create({ ItemId: id(), SectionName: "Cover Sheet" }, self));
                    }
                }

                return;
            }),
            // Item Price Matrices for Current Flag
            itemPriceMatricesForCurrentFlag = ko.computed(function () {
                if (!flagId()) {
                    return [];
                }

                return itemPriceMatrices.filter(function (itemPriceMatrix) {
                    return itemPriceMatrix.flagId() === flagId() && (!id() || itemPriceMatrix.itemId() === id()) && !itemPriceMatrix.supplierId();
                });
            }),
            // Item Price Matrices for Supplier Id 1
            itemPriceMatricesForSupplierId1 = ko.computed(function () {
                if (!internalSupplierId()) {
                    return [];
                }

                return itemPriceMatrices.filter(function (itemPriceMatrix) {
                    return (!id() || itemPriceMatrix.itemId() === id()) && itemPriceMatrix.supplierId() === internalSupplierId() &&
                        itemPriceMatrix.supplierSequence() === 1;
                });
            }),
            // Item Price Matrices for Supplier Id 2
            itemPriceMatricesForSupplierId2 = ko.computed(function () {
                if (!internalSupplierId2()) {
                    return [];
                }

                return itemPriceMatrices.filter(function (itemPriceMatrix) {
                    return (!id() || itemPriceMatrix.itemId() === id()) && itemPriceMatrix.supplierId() === internalSupplierId2() &&
                        itemPriceMatrix.supplierSequence() === 2;
                });
            }),
            // Item Price Matrices for Supplier Sequence 1
            itemPriceMatricesForSupplierSequence1 = ko.computed(function () {
                if (!internalSupplierId()) {
                    return [];
                }

                return itemPriceMatrices.filter(function (itemPriceMatrix) {
                    return (!id() || itemPriceMatrix.itemId() === id()) && itemPriceMatrix.supplierId() &&
                        itemPriceMatrix.supplierSequence() === 1;
                });
            }),
            // Item Price Matrices for Supplier Sequence 2
            itemPriceMatricesForSupplierSequence2 = ko.computed(function () {
                if (!internalSupplierId2()) {
                    return [];
                }

                return itemPriceMatrices.filter(function (itemPriceMatrix) {
                    return (!id() || itemPriceMatrix.itemId() === id()) && itemPriceMatrix.supplierId() &&
                        itemPriceMatrix.supplierSequence() === 2;
                });
            }),
            // Supplier Id
            supplierId = ko.computed({
                read: function () {
                    return internalSupplierId();
                },
                write: function (value) {
                    if (!value || value === internalSupplierId()) {
                        return;
                    }

                    // Update Supplier Prices for Sequence 1
                    updateSupplierForSequence(value, 1, itemPriceMatricesForSupplierId1, itemPriceMatricesForSupplierSequence1);

                    // Update SupplierId1
                    internalSupplierId(value);
                }
            }),
            // Supplier Id2
            supplierId2 = ko.computed({
                read: function () {
                    return internalSupplierId2();
                },
                write: function (value) {
                    if (!value || value === internalSupplierId2()) {
                        return;
                    }

                    // Update Supplier Prices for Sequence 2
                    updateSupplierForSequence(value, 2, itemPriceMatricesForSupplierId2, itemPriceMatricesForSupplierSequence2);

                    // Update SupplierId2
                    internalSupplierId2(value);
                }
            }),
            // Update Supplier in Item Price Matrix
            updateSupplierForSequence = function (suppId, suppSequence, itemPriceMatricesForSupplier, itemPriceMatricesForSupplierSequence) {
                if (itemPriceMatricesForSupplier().length > 0) {
                    _.each(itemPriceMatricesForSupplier(), function (itemPriceMatrix) {
                        itemPriceMatrix.supplierId(suppId);
                    });
                }
                else if (itemPriceMatricesForSupplierSequence().length > 0) {
                    _.each(itemPriceMatricesForSupplierSequence(), function (itemPriceMatrix) {
                        itemPriceMatrix.supplierId(suppId);
                    });
                }
                else {
                    // Copy From Current Flag & Add New
                    addPricesForSupplier(suppId, suppSequence);
                }
            },
            // Add Supplier 
            addPricesForSupplier = function (suppId, suppSequence) {
                // Copy 15 from Current Flag
                if (itemPriceMatricesForCurrentFlag().length > 0) {
                    _.each(itemPriceMatricesForCurrentFlag(), function (itemPriceMatrix) {
                        var itemMatrix = itemPriceMatrix.convertToServerData();
                        itemMatrix.PriceMatrixId = 0;
                        itemMatrix.SupplierId = suppId;
                        itemMatrix.SupplierSequence = suppSequence;
                        itemPriceMatrices.push(ItemPriceMatrix.Create(itemMatrix));
                    });
                }
                else {
                    // Add 15 New
                    for (var i = 0; i < 15; i++) {
                        itemPriceMatrices.push(ItemPriceMatrix.Create({
                            SupplierId: suppId,
                            SupplierSequence: suppSequence,
                            ItemId: id()
                        }));
                    }
                }
            },
            // Sync Suppliers Price Matrix Quantities with Item Price Quantities 
            updateQuantitiesForSupplier = function (itemPriceMatrix) {
                if (!itemPriceMatrix) {
                    return;
                }
                var itemPriceIndex = undefined;
                if (itemPriceMatricesForCurrentFlag() && itemPriceMatricesForCurrentFlag().length > 0) {
                    itemPriceIndex = itemPriceMatricesForCurrentFlag().indexOf(itemPriceMatrix);
                }
                if (itemPriceIndex === null || itemPriceIndex === undefined) {
                    return;
                }
                if (itemPriceMatricesForSupplierId1() && (itemPriceMatricesForSupplierId1().length > 0 && itemPriceMatricesForSupplierId1().length >= itemPriceIndex)) {
                    var supplierPriceMatrixItem = itemPriceMatricesForSupplierId1()[itemPriceIndex];
                    if (supplierPriceMatrixItem) {
                        updateSupplierQuantity(supplierPriceMatrixItem, itemPriceMatrix);
                    }
                }
                if (itemPriceMatricesForSupplierId2() && (itemPriceMatricesForSupplierId2().length > 0 && itemPriceMatricesForSupplierId2().length >= itemPriceIndex)) {
                    var supplier2PriceMatrixItem = itemPriceMatricesForSupplierId2()[itemPriceIndex];
                    if (supplier2PriceMatrixItem) {
                        updateSupplierQuantity(supplier2PriceMatrixItem, itemPriceMatrix);
                    }
                }
            },
            // Update supplier quantity
            updateSupplierQuantity = function (supplierPriceMatrixItem, itemPriceMatrix) {
                if (isQtyRangedUi() === '2') {
                    supplierPriceMatrixItem.quantity(itemPriceMatrix.quantity() || 0);
                }
                else {
                    supplierPriceMatrixItem.qtyRangedFrom(itemPriceMatrix.qtyRangedFrom() || 0);
                    supplierPriceMatrixItem.qtyRangedTo(itemPriceMatrix.qtyRangedTo() || 0);
                }
            },
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
            // Select Stock Call back
            selectStockItemCallback = null,
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

                // Set Stock Item Selection Callback
                selectStockItemCallback = selectStockItemForStockOption;
            },
            // On Select Stock Item
            onSelectStockItem = function (stockItem) {
                if (selectStockItemCallback && typeof selectStockItemCallback === "function") {
                    selectStockItemCallback(stockItem);
                }

                if (callbacks && callbacks.onSelectStockItem && typeof callbacks.onSelectStockItem === "function") {
                    callbacks.onSelectStockItem();
                }
            },
            // Set Stock Item For Stock Option in Case of New Product
            selectStockItemForStockOptionForNewProduct = function (stockItem) {
                if (itemStockOptions().length === 0) {
                    return;
                }
                activeStockOption(itemStockOptions()[0]);
                selectStockItemForStockOption(stockItem);
            },
            // Select Stock Item For Stock Option
            selectStockItemForStockOption = function (stockItem) {
                activeStockOption().selectStock(stockItem);
                activeStockOption(ItemStockOption.Create({}, callbacks, self));
            },
            // Choose Stock Item For Section
            chooseStockItemForSection = function () {
                if (callbacks && callbacks.onChooseStockItem && typeof callbacks.onChooseStockItem === "function") {
                    callbacks.onChooseStockItem(stockCategory.paper);
                }
            },
            // Active Item Section
            activeItemSection = ko.observable(ItemSection.Create({}, self)),
            // Select Item Section
            selectItemSection = function (itemSection) {
                if (activeItemSection() !== itemSection) {
                    activeItemSection(itemSection);
                }

                // Set Stock Item Selection Callback
                selectStockItemCallback = selectStockItemForSection;
            },
            // Select Stock Item for Section for New Product
            selectStockItemForSectionForNewProduct = function (stockItem) {
                if (itemSections().length === 0) {
                    return;
                }
                activeItemSection(itemSections()[0]);
                selectStockItemForSection(stockItem);
            },
            // On Select Stock Item
            selectStockItemForSection = function (stockItem) {
                activeItemSection().selectStock(stockItem);
            },
            // On Select Press Item
            selectPressItemForSection = function (press) {
                activeItemSection().selectPress(press);
            },
            // On Select Press Item
            onSelectPressItem = function (pressItem) {
                selectPressItemForSection(pressItem);

                if (callbacks && callbacks.onSelectPressItem && typeof callbacks.onSelectPressItem === "function") {
                    callbacks.onSelectPressItem();
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
            // On Remove Item Related Item
            onRemoveItemRelatedItem = function (item) {
                if (callbacks && callbacks.onDeleteItemRelatedItem && typeof callbacks.onDeleteItemRelatedItem === "function") {
                    callbacks.onDeleteItemRelatedItem(function () {
                        removeItemRelatedItem(item);
                    });
                }
            },
            // Remove Item Related Item
            removeItemRelatedItem = function (item) {
                itemRelatedItems.remove(item);
            },
            // Validate Category Only while Save
            shouldValidateCategory = ko.observable(false),
            // Set New Stock Option Tab Active
            setNewStockOptionTabActive = function (element) {
                var active = $('#stockLabelTabListItem').find('> .active');
                if (active) {
                    active.removeClass('active');
                }
                var tabListItem = $('#stockLabelTabListItem a[href=#' + element.id + ']');
                if (tabListItem) {
                    tabListItem.tab('show');
                }
                var activeContentItem = $('#stockLabelTabListItemDetails').find('> .active');
                if (activeContentItem) {
                    activeContentItem.removeClass('in active');
                }
                var tabContentItem = $('#stockLabelTabListItemDetails #' + element.id);
                if (tabContentItem) {
                    tabContentItem.addClass('in active');
                }
            },
            // Wireup Item Stock Option Tab Events
            wireupItemStockOptionTabEvents = function (stockOption) {
                setTimeout(function () {
                    var index = itemStockOptions.indexOf(stockOption);
                    if (index >= 0) {
                        var stockOptionItem = itemStockOptions()[index];
                        if (stockOptionItem) {
                            if (stockOptionItem.stockItemName && stockOptionItem.stockItemName.domElement) {
                                $(stockOptionItem.stockItemName.domElement).on('click', function () {
                                    setNewStockOptionTabActive(stockOptionItem.stockItemName.domElement);
                                });
                                setNewStockOptionTabActive(stockOptionItem.stockItemName.domElement);
                            }
                        }
                    }
                }, 500);
            },
            // Only Allow 11 stock labels
            canAddItemStockOption = ko.computed(function() {
                return itemStockOptions().length < 11;
            }),
            // Add Item Stock Option
            addItemStockOption = function () {
                var stockOption = ItemStockOption.Create({ ItemId: id() }, callbacks, self);
                itemStockOptions.push(stockOption);
                wireupItemStockOptionTabEvents(stockOption);
            },
            // Remove Item Stock Option
            removeItemStockOption = function (itemStockOption) {
                itemStockOptions.remove(itemStockOption);
                if (!$('#stockLabelTabListItem li a').last()[0]) {
                    return;
                }
                setNewStockOptionTabActive($('#stockLabelTabListItem li a').last()[0]);
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
                var itemStateTax = ItemStateTax.Create({ ItemId: id() }, constructorParams);
                itemStateTaxes.push(itemStateTax);
                selectStateTaxItem(itemStateTax);
            },
            // Remove Item State Tax
            removeItemStateTax = function (itemStateTax) {
                itemStateTaxes.remove(itemStateTax);
            },
            // Selected Price Matrix Item
            selectedPriceMatrixItem = ko.observable(),
            // Select Price Matrix Item
            selectPriceMatrixItem = function (priceMatrixItem) {
                if (selectedPriceMatrixItem() === priceMatrixItem) {
                    return;
                }

                selectedPriceMatrixItem(priceMatrixItem);
            },
            // Choose Template for Price Matrix
            chooseTemplateForPriceMatrix = function () {
                return 'editPriceMatrixTemplate';
            },
            // Selected Price Matrix Item For Supplier 1
            selectedPriceMatrixItemForSupplier1 = ko.observable(),
            // Select Price Matrix Item
            selectPriceMatrixItemForSupplier1 = function (priceMatrixItem) {
                if (selectedPriceMatrixItemForSupplier1() === priceMatrixItem) {
                    return;
                }

                selectedPriceMatrixItemForSupplier1(priceMatrixItem);
            },
            // Choose Template for Price Matrix
            chooseTemplateForSupplier1PriceMatrix = function () {
                return 'editPriceMatrixTemplate';
            },
            // Selected Price Matrix Item For Supplier 2
            selectedPriceMatrixItemForSupplier2 = ko.observable(),
            // Select Price Matrix Item
            selectPriceMatrixItemForSupplier2 = function (priceMatrixItem) {
                if (selectedPriceMatrixItemForSupplier2() === priceMatrixItem) {
                    return;
                }

                selectedPriceMatrixItemForSupplier2(priceMatrixItem);
            },
            // Choose Template for Price Matrix
            chooseTemplateForSupplier2PriceMatrix = function () {
                return 'editPriceMatrixTemplate';
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
            // Set Default Qty and Price for 1st Five items in Price Matrix while Creating New
            setDefaultQtyAndPriceForFirstFive = function (index, itemPriceMatrix) {
                if (index === 0) {
                    itemPriceMatrix.quantity(100);
                    itemPriceMatrix.pricePaperType1(10);
                }
                else if (index === 1) {
                    itemPriceMatrix.quantity(200);
                    itemPriceMatrix.pricePaperType1(20);
                }
                else if (index === 2) {
                    itemPriceMatrix.quantity(300);
                    itemPriceMatrix.pricePaperType1(30);
                }
                else if (index === 3) {
                    itemPriceMatrix.quantity(500);
                    itemPriceMatrix.pricePaperType1(50);
                }
                else if (index === 4) {
                    itemPriceMatrix.quantity(1000);
                    itemPriceMatrix.pricePaperType1(100);
                }
                return itemPriceMatrix;
            },
            // Set Item Price Matrices for Selected Flag
            setItemPriceMatrices = function (itemPriceMatrixList) {
                // If no list then create new
                var itemPriceMatrixItems = [];
                if (!itemPriceMatrixList || itemPriceMatrixList.length === 0) {
                    // Look for Existing Price Matrices and make a copy
                    if (itemPriceMatricesForExistingFlag().length > 0) {
                        itemPriceMatricesForExistingFlag.each(function (itemPriceMatrix) {
                            var priceItem = itemPriceMatrix.convertToServerData();
                            priceItem.PriceMatrixId = 0;
                            priceItem.FlagId = flagId();
                            priceItem.ItemId = id();
                            itemPriceMatrixItems.push(ItemPriceMatrix.Create(priceItem, { onPriceMatrixQuantityChange: updateQuantitiesForSupplier }));
                        });
                        ko.utils.arrayPushAll(itemPriceMatrices(), itemPriceMatrixItems);
                        // Remove Existing ones
                        removeExistingPriceMatrices();
                        itemPriceMatrices.valueHasMutated();
                    }
                    else { // Add New
                        var itemPriceMatrixNew;
                        for (var i = 0; i < 15; i++) {
                            itemPriceMatrixNew = ItemPriceMatrix.Create({ ItemId: id(), FlagId: flagId() },
                                { onPriceMatrixQuantityChange: updateQuantitiesForSupplier });
                            itemPriceMatrixNew = setDefaultQtyAndPriceForFirstFive(i, itemPriceMatrixNew);
                            itemPriceMatrixItems.push(itemPriceMatrixNew);
                        }
                        ko.utils.arrayPushAll(itemPriceMatrices(), itemPriceMatrixItems);
                        itemPriceMatrices.valueHasMutated();
                    }
                    return;
                }

                // Set Already existing Items For Current Flag
                _.each(itemPriceMatrixList, function (itemPriceMatrix) {
                    var itemMatrix = itemPriceMatrices.find(function (itemMatrixItem) {
                        return itemMatrixItem.id() === itemPriceMatrix.PriceMatrixId;
                    });
                    if (!itemMatrix) {
                        itemPriceMatrixItems.push(ItemPriceMatrix.Create(itemPriceMatrix, { onPriceMatrixQuantityChange: updateQuantitiesForSupplier }));
                    }
                });
                ko.utils.arrayPushAll(itemPriceMatrices(), itemPriceMatrixItems);
                removeExistingPriceMatrices();
                itemPriceMatrices.valueHasMutated();
            },
            // Remove Existing Item Price Matrices For Selected Flag
            removeExistingPriceMatrices = function () {
                if (itemPriceMatricesForExistingFlag().length > 0 && !id) {
                    // Remove Existing ones
                    itemPriceMatrices.removeAll(itemPriceMatricesForExistingFlag());
                }
            },
            // Update Product Category Items
            updateProductCategoryItems = function (productCategories) {
                if (productCategories || productCategories.length > 0) {
                    // Add Selected to Product Category Item List
                    var selectedCategories = _.filter(productCategories, function (productCategory) {
                        return productCategory.isSelected();
                    });

                    // Update UnSelected to Product Category Item List
                    var unselectedCategories = _.filter(productCategories, function (productCategory) {
                        return !productCategory.isSelected();
                    });

                    // Add Selected
                    if (selectedCategories.length > 0) {
                        _.each(selectedCategories, function (productCategory) {
                            var productCategoryItemObj = productCategoryItems.find(function (productCategoryItem) {
                                return productCategoryItem.categoryId() === productCategory.id;
                            });

                            // Exists Already
                            if (productCategoryItemObj) {
                                if (!productCategoryItemObj.isSelected()) {
                                    // set it to true
                                    productCategoryItemObj.isSelected(true);
                                }
                            }
                            else {
                                // Add New
                                productCategoryItems.push(ProductCategoryItem.Create({
                                    CategoryId: productCategory.id,
                                    CategoryName: productCategory.name,
                                    ItemId: id(),
                                    IsSelected: true
                                }));
                            }
                        });
                    }

                    // Update Un-Selected
                    if (unselectedCategories.length > 0) {
                        _.each(unselectedCategories, function (productCategory) {
                            var productCategoryItemObj = productCategoryItems.find(function (productCategoryItem) {
                                return productCategoryItem.categoryId() === productCategory.id;
                            });

                            // Exists Already
                            if (productCategoryItemObj) {
                                if (!productCategoryItemObj.id()) { // If New Product Category Item
                                    productCategoryItems.remove(productCategoryItemObj);
                                }
                                else {
                                    // set it to false
                                    productCategoryItemObj.isSelected(false);
                                }
                            }
                        });
                    }
                }
            },
            // Remove Item Section
            removeItemSection = function () {
                if (!canRemoveItemSection()) {
                    return;
                }

                itemSections.pop();
            },
            // Add Item Section
            addItemSection = function () {
                itemSections.push(ItemSection.Create({ ItemId: id(), SectionName: "Text Sheet" }, self));
            },
            // Get Available slot for Template Layout File
            getAvailableSlotForTemplateLayoutFile = function () {
                if (!file1()) {
                    return 1;
                }
                else if (!file2()) {
                    return 2;
                }
                else if (!file3()) {
                    return 3;
                }
                else if (!file4()) {
                    return 4;
                }
                else if (!file5()) {
                    return 5;
                }
                return 0;
            },
            // On Select File
            onSelectImage = function (file, data, fileType) {
                switch (fileType) {
                    case itemFileTypes.thumbnail:
                        thumbnail(data);
                        thumbnailFileSource(data);
                        thumbnailFileName(file.name);
                        break;
                    case itemFileTypes.grid:
                        gridImage(data);
                        gridImageFileSource(data);
                        gridImageFileName(file.name);
                        break;
                    case itemFileTypes.imagePath:
                        imagePath(data);
                        imagePathFileSource(data);
                        imagePathFileName(file.name);
                        break;
                    case itemFileTypes.file:
                        var availableSlot = getAvailableSlotForTemplateLayoutFile();
                        switch (availableSlot) {
                            case 1:
                                file1(data);
                                file1FileSource(data);
                                file1FileName(file.name);
                                file1Deleted(false);
                                break;
                            case 2:
                                file2(data);
                                file2FileSource(data);
                                file2FileName(file.name);
                                file2Deleted(false);
                                break;
                            case 3:
                                file3(data);
                                file3FileSource(data);
                                file3FileName(file.name);
                                file3Deleted(false);
                                break;
                            case 4:
                                file4(data);
                                file4FileSource(data);
                                file4FileName(file.name);
                                file4Deleted(false);
                                break;
                            case 5:
                                file5(data);
                                file5FileSource(data);
                                file5FileName(file.name);
                                file5Deleted(false);
                                break;
                        }
                        break;
                }
            },
            // Add Item Image
            onSelectItemImage = function (file, data) {
                var itemImage = ItemImage.Create({ ItemId: id() });
                itemImage.onSelectImage(file, data);
                itemImages.push(itemImage);
            },
            // Delete Template Layout File
            removeTemplateLayoutFile = function (fileSequenceNo) {
                switch (fileSequenceNo) {
                    case 1:
                        file1(undefined);
                        file1FileSource(undefined);
                        file1FileName(undefined);
                        file1Deleted(true);
                        break;
                    case 2:
                        file2(undefined);
                        file2FileSource(undefined);
                        file2FileName(undefined);
                        file2Deleted(true);
                        break;
                    case 3:
                        file3(undefined);
                        file3FileSource(undefined);
                        file3FileName(undefined);
                        file3Deleted(true);
                        break;
                    case 4:
                        file4(undefined);
                        file4FileSource(undefined);
                        file4FileName(undefined);
                        file4Deleted(true);
                        break;
                    case 5:
                        file5(undefined);
                        file5FileSource(undefined);
                        file5FileName(undefined);
                        file5Deleted(true);
                        break;
                }
            },
            // Reset Files
            resetFiles = function () {
                file1(undefined);
                file1FileSource(undefined);
                file1FileName(undefined);
                file2(undefined);
                file2FileSource(undefined);
                file2FileName(undefined);
                file3(undefined);
                file3FileSource(undefined);
                file3FileName(undefined);
                file4(undefined);
                file4FileSource(undefined);
                file4FileName(undefined);
                file5(undefined);
                file5FileSource(undefined);
                file5FileName(undefined);
                file1Deleted(true);
                file2Deleted(true);
                file3Deleted(true);
                file4Deleted(true);
                file5Deleted(true);
            },
            // Remove Item Image
            removeItemImage = function(itemImage) {
                itemImages.remove(itemImage);
            },
            // Move ProductMarketBriefQuestion Page Up
            moveProductMarketBriefQuestionUp = function (productMarketBriefQuestion, e) {
                var i = productMarketBriefQuestions.indexOf(productMarketBriefQuestion);
                if (i >= 1) {
                    var array = productMarketBriefQuestions();
                    productMarketBriefQuestions.splice(i - 1, 2, array[i], array[i - 1]);
                }
                e.stopImmediatePropagation();
            },
            // Move ProductMarketBriefQuestion Page Down
            moveProductMarketBriefQuestionDown = function (productMarketBriefQuestion, e) {
                var i = productMarketBriefQuestions.indexOf(productMarketBriefQuestion);
                var array = productMarketBriefQuestions();
                if (i < array.length) {
                    productMarketBriefQuestions.splice(i, 2, array[i + 1], array[i]);
                }
                e.stopImmediatePropagation();
            },
            // Active Product Market Question
            activeProductMarketQuestion = ko.observable(ProductMarketBriefQuestion.Create({ ItemId: id() }, callbacks)),
            // On Add Product Market Brief Question
            onAddProductMarketBriefQuestion = function () {
                activeProductMarketQuestion(ProductMarketBriefQuestion.Create({ ItemId: id() }, callbacks));
                if (callbacks && callbacks.onAddProductMarketBriefQuestion && typeof callbacks.onAddProductMarketBriefQuestion === "function") {
                    callbacks.onAddProductMarketBriefQuestion();
                }
            },
            // Add ProductMarketBriefQuestion
            addProductMarketBriefQuestion = function () {
                productMarketBriefQuestions.push(activeProductMarketQuestion());
                activeProductMarketQuestion().reset();
            },
            // On Edit Product Market Brief Question
            onEditProductMarketBriefQuestion = function (productMarketBriefQuestion) {
                activeProductMarketQuestion(productMarketBriefQuestion);
                activeProductMarketQuestion().reset();
                if (callbacks && callbacks.onEditProductMarketBriefQuestion && typeof callbacks.onEditProductMarketBriefQuestion === "function") {
                    callbacks.onEditProductMarketBriefQuestion();
                }
            },
            // On Delete Product Market Brief Question
            onDeleteProductMarketBriefQuestion = function (productMarketBriefQuestion) {
                if (callbacks && callbacks.onDeleteProductMarketBriefQuestion && typeof callbacks.onDeleteProductMarketBriefQuestion === "function") {
                    callbacks.onDeleteProductMarketBriefQuestion(function () {
                        removeProductMarketBriefQuestion(productMarketBriefQuestion);
                    });
                }
            },
            // Remove ProductMarketBriefQuestion
            removeProductMarketBriefQuestion = function (productMarketBriefQuestion) {
                productMarketBriefQuestions.remove(productMarketBriefQuestion);
                activeProductMarketQuestion(ProductMarketBriefQuestion.Create({ ItemId: id() }, callbacks));
            },
            // Errors
            errors = ko.validation.group({
                productCode: productCode,
                productName: productName,
                internalFlagId: internalFlagId
            }),
            // Has Valid Template
            hasTemplatePagesForManual = function () {
                if ((isFinishedGoodsUi() !== '1') || (templateTypeUi() !== '1')) {
                    return true;
                }

                return (isFinishedGoodsUi() === '1') && (templateTypeUi() === '1') && (template() && template().templatePages().length > 0);
            },
            // Has a category
            hasInvalidCategroy = function () {
                return !availableProductCategoryItems();
            },
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && itemVdpPrices.filter(function (itemVdpPrice) {
                    return !itemVdpPrice.isValid();
                }).length === 0 &&
                itemStockOptions.filter(function (itemStockOption) {
                    return !itemStockOption.isValid();
                }).length === 0 &&
                itemSections.filter(function (itemSection) {
                    return !itemSection.isValid();
                }).length === 0 &&
                template().isValid() &&
                !hasInvalidCategroy() &&
                hasTemplatePagesForManual();
            }),
            // Show All Error Messages
            showAllErrors = function () {
                // Show Item Errors
                errors.showAllMessages();
                // Show Item Stock Option Errors
                var itemStockOptionErrors = itemStockOptions.filter(function (itemStockOption) {
                    return !itemStockOption.isValid();
                });
                if (itemStockOptionErrors.length > 0) {
                    _.each(itemStockOptionErrors, function (itemStockOption) {
                        itemStockOption.errors.showAllMessages();
                    });
                }
                // Show Template Errors
                if (!template().isValid()) {
                    template().errors.showAllMessages();
                    // Show Template Page Errors
                    var templatePageErrors = template().templatePages.filter(function (templatePage) {
                        return !templatePage.isValid();
                    });
                    if (templatePageErrors.length > 0) {
                        _.each(templatePageErrors, function (templatePage) {
                            templatePage.errors.showAllMessages();
                        });
                    }
                }
                // Show Item Section Errors
                var itemSectionErrors = itemSections.filter(function (itemSection) {
                    return !itemSection.isValid();
                });
                if (itemSectionErrors.length > 0) {
                    _.each(itemSectionErrors, function (itemSection) {
                        itemSection.errors.showAllMessages();
                    });
                }
            },
            // Set Validation Summary
            setValidationSummary = function (validationSummaryList) {
                validationSummaryList.removeAll();
                if (productName.error) {
                    validationSummaryList.push({ name: productName.domElement.name, element: productName.domElement });
                }
                if (productCode.error) {
                    validationSummaryList.push({ name: productCode.domElement.name, element: productCode.domElement });
                }
                if (internalFlagId.error) {
                    validationSummaryList.push({ name: internalFlagId.domElement.name, element: internalFlagId.domElement });
                }
                // Show Item Stock Option Errors
                var itemStockOptionInvalid = itemStockOptions.find(function (itemStockOption) {
                    return !itemStockOption.isValid();
                });
                if (itemStockOptionInvalid) {
                    if (itemStockOptionInvalid.label.error) {
                        var labelElement = itemStockOptionInvalid.label.domElement;
                        validationSummaryList.push({ name: labelElement.name, element: labelElement });
                    }
                }
                // Show Template Errors
                if (!template().isValid()) {
                    if (template().fileSource.error) {
                        var templateFileElement = template().fileSource.domElement;
                        validationSummaryList.push({ name: "Pre-Built Template", element: templateFileElement });
                    }
                    if (template().pdfTemplateWidth.error) {
                        var templatePdfWidthElement = template().pdfTemplateWidth.domElement;
                        validationSummaryList.push({ name: "Width is required in case of Blank Template", element: templatePdfWidthElement });
                    }
                    if (template().pdfTemplateHeight.error) {
                        var templatePdfHeightElement = template().pdfTemplateHeight.domElement;
                        validationSummaryList.push({ name: "Height is required in case of Blank Template", element: templatePdfHeightElement });
                    }
                    // Show Template Page Errors
                    var templatePageInvalid = template().templatePages.find(function (templatePage) {
                        return !templatePage.isValid();
                    });
                    if (templatePageInvalid) {
                        if (templatePageInvalid.pageName.error) {
                            validationSummaryList.push({ name: "Template Page Name", element: templatePageInvalid.pageName.domElement });
                        }
                    }
                }
                // If Print Item and don't has Template Pages for Blank Template
                if (!hasTemplatePagesForManual()) {
                    validationSummaryList.push({
                        name: "Atleast one Template Page is required in case of Blank Template",
                        element: template().isCreatedManual.domElement
                    });
                }
                // Show Item Section Errors
                var itemSectionInvalid = itemSections.find(function (itemSection) {
                    return !itemSection.isValid();
                });
                if (itemSectionInvalid) {
                    if (itemSectionInvalid.name.error || itemSectionInvalid.pressId.error || itemSectionInvalid.stockItemId.error ||
                        itemSectionInvalid.itemGutterHorizontal.error) {
                        var nameElement = itemSectionInvalid.name.domElement;
                        var errorName = "";
                        if (itemSectionInvalid.name.error) {
                            errorName = "Section Name";
                        }
                        else if (itemSectionInvalid.pressId.error) {
                            errorName = "Section Press";
                        }
                        else if (itemSectionInvalid.stockItemId.error) {
                            errorName = "Section Stock Item";
                        }
                        else if (itemSectionInvalid.itemGutterHorizontal.error) {
                            errorName = "Gutter value must be a number from 0 to 99";
                        }
                        validationSummaryList.push({ name: errorName, element: nameElement });
                    }
                }
                // Check if a category is selected
                if (!availableProductCategoryItems()) {
                    validationSummaryList.push({ name: "Product should have atleast one category", element: productCategoryElement.domElement });
                    shouldValidateCategory(true);
                }
                else {
                    shouldValidateCategory(false);
                }
            },
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
                supplierId: supplierId,
                supplierId2: supplierId2,
                estimateProductionTime: estimateProductionTime,
                thumbnail: thumbnail,
                gridImage: gridImage,
                imagePath: imagePath,
                file1: file1,
                file2: file2,
                file3: file3,
                file4: file4,
                file5: file5,
                isTemplateDesignMode: isTemplateDesignMode,
                isCmyk: isCmyk,
                scalar: scalar,
                zoomFactor: zoomFactor,
                designerCategoryId: designerCategoryId,
                templateType: templateType,
                productDisplayOptions: productDisplayOptions,
                isRealStateProduct: isRealStateProduct,
                isUploadImage: isUploadImage,
                isDigitalDownload: isDigitalDownload,
                isAddCropMarks: isAddCropMarks,
                printCropMarks: printCropMarks,
                drawWatermarkText: drawWatermarkText,
                drawBleedArea: drawBleedArea,
                isMultiPagePdf: isMultiPagePdf,
                allowPdfDownload: allowPdfDownload,
                allowImageDownload: allowImageDownload,
                itemLength: itemLength,
                itemWeight: itemWeight,
                itemHeight: itemHeight,
                itemWidth: itemWidth,
                smartFormId: smartFormId,
                itemProductDetail: itemProductDetail,
                itemVdpPrices: itemVdpPrices,
                itemVideos: itemVideos,
                itemRelatedItems: itemRelatedItems,
                template: template,
                itemStockOptions: itemStockOptions,
                itemPriceMatrices: itemPriceMatrices,
                itemStateTaxes: itemStateTaxes,
                productCategoryItems: productCategoryItems,
                itemSections: itemSections,
                itemImages: itemImages,
                productMarketBriefQuestions: productMarketBriefQuestions
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
            // Item Section Changes
            itemSectionHasChanges = ko.computed(function () {
                return itemSections.find(function (itemSection) {
                    return itemSection.hasChanges();
                }) != null;
            }),
            // Item Image Changes
            itemImageHasChanges = ko.computed(function () {
                return itemImages.find(function (itemImage) {
                    return itemImage.hasChanges();
                }) != null;
            }),
            // Product Market Brief Question Changes
            productMarketBriefQuestionHasChanges = ko.computed(function () {
                return productMarketBriefQuestions.find(function (productMarketBriefQuestion) {
                    return productMarketBriefQuestion.hasChanges();
                }) != null;
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || itemVdpPriceListHasChanges() || itemVideosHasChanges() || template().hasChanges() || itemStockOptionHasChanges() ||
                    itemPriceMatrixHasChanges() || itemStateTaxesHasChanges() || itemProductDetail().hasChanges() || itemSectionHasChanges() || itemImageHasChanges() ||
                    productMarketBriefQuestionHasChanges();
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
                itemSections.each(function (itemSection) {
                    return itemSection.reset();
                });
                itemImages.each(function (itemImage) {
                    return itemImage.reset();
                });
                productMarketBriefQuestions.each(function (question) {
                    return question.reset();
                });
                template().reset();
                itemProductDetail().reset();
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
                    ProductType: parseInt(isFinishedGoodsUi()),
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
                    IsQtyRanged: isQtyRanged() === 2 ? false : true,
                    PackagingWeight: packagingWeight(),
                    DefaultItemTax: defaultItemTax(),
                    SupplierId: supplierId(),
                    SupplierId2: supplierId2(),
                    EstimateProductionTime: estimateProductionTime(),
                    IsTemplateDesignMode: isTemplateDesignMode(),
                    IsCmyk: isCmyk() === 1,
                    Scalar: scalar(),
                    ZoomFactor: 1,
                    DesignerCategoryId: designerCategoryId(),
                    TemplateType: templateType(),
                    TemplateTypeMode: templateTypeMode(),
                    ProductDisplayOptions: productDisplayOptions(),
                    IsRealStateProduct: isRealStateProduct(),
                    IsUploadImage: isUploadImage(),
                    IsDigitalDownload: isDigitalDownload(),
                    IsAddCropMarks: isAddCropMarks(),
                    PrintCropMarks: printCropMarks(),
                    DrawWaterMarkTxt: drawWatermarkText(),
                    DrawBleedArea: drawBleedArea(),
                    IsMultipagePdf: isMultiPagePdf(),
                    AllowPdfDownload: allowPdfDownload(),
                    AllowImageDownload: allowImageDownload(),
                    ItemLength: itemLength(),
                    ItemWeight: itemWeight(),
                    ItemHeight: itemHeight(),
                    ItemWidth: itemWidth(),
                    SmartFormId: smartFormId(),
                    ThumbnailImageName: thumbnailFileName(),
                    ThumbnailImageByte: thumbnailFileSource(),
                    GridImageSourceName: gridImageFileName(),
                    GridImageSourceByte: gridImageFileSource(),
                    ImagePathImageName: imagePathFileName(),
                    ImagePathImageByte: imagePathFileSource(),
                    File1Name: file1FileName(),
                    File1Byte: file1FileSource(),
                    File2Name: file2FileName(),
                    File2Byte: file2FileSource(),
                    File3Name: file3FileName(),
                    File3Byte: file3FileSource(),
                    File4Name: file4FileName(),
                    File4Byte: file4FileSource(),
                    File5Name: file5FileName(),
                    File5Byte: file5FileSource(),
                    File1Deleted: file1Deleted(),
                    File2Deleted: file2Deleted(),
                    File3Deleted: file3Deleted(),
                    File4Deleted: file4Deleted(),
                    File5Deleted: file5Deleted(),
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
                    Template: template().convertToServerData(),
                    ItemProductDetail: itemProductDetail().convertToServerData(),
                    ProductCategoryItems: productCategoryItems.map(function (productCategoryItem) {
                        return productCategoryItem.convertToServerData();
                    }),
                    ItemSections: itemSections.map(function (itemSection, index) {
                        var section = itemSection.convertToServerData();
                        section.SectionNo = index + 1;
                        return section;
                    }),
                    ItemImages: itemImages.map(function (itemImage) {
                        return itemImage.convertToServerData();
                    }),
                    ProductMarketBriefQuestions: productMarketBriefQuestions.map(function (productMarketBriefQuestion, index) {
                        var question = productMarketBriefQuestion.convertToServerData();
                        question.SortOrder = index + 1;
                        return question;
                    })
                };
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
            file2: file2,
            file3: file3,
            file4: file4,
            file5: file5,
            file1FileSource: file1FileSource,
            file2FileSource: file2FileSource,
            file3FileSource: file3FileSource,
            file4FileSource: file4FileSource,
            file5FileSource: file5FileSource,
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
            internalFlagId: internalFlagId,
            isQtyRangedUi: isQtyRangedUi,
            isQtyRanged: isQtyRanged,
            packagingWeight: packagingWeight,
            defaultItemTax: defaultItemTax,
            supplierId: supplierId,
            supplierId2: supplierId2,
            estimateProductionTime: estimateProductionTime,
            isTemplateDesignModeUi: isTemplateDesignModeUi,
            isCmykUi: isCmykUi,
            scalar: scalar,
            zoomFactor: zoomFactor,
            designerCategoryId: designerCategoryId,
            templateTypeUi: templateTypeUi,
            templateType: templateType,
            templateTypeMode: templateTypeMode,
            productDisplayOptionsUi: productDisplayOptionsUi,
            productDisplayOptions: productDisplayOptions,
            isRealStateProduct: isRealStateProduct,
            isUploadImage: isUploadImage,
            isDigitalDownload: isDigitalDownload,
            printCropMarks: printCropMarks,
            drawWatermarkText: drawWatermarkText,
            isAddCropMarks: isAddCropMarks,
            drawBleedArea: drawBleedArea,
            isMultiPagePdf: isMultiPagePdf,
            allowPdfDownload: allowPdfDownload,
            allowImageDownload: allowImageDownload,
            itemLength: itemLength,
            itemWeight: itemWeight,
            itemHeight: itemHeight,
            itemWidth: itemWidth,
            organisationId: organisationId,
            companyId: companyId,
            templateId: templateId,
            smartFormId: smartFormId,
            canStartDesignerEmpty: canStartDesignerEmpty,
            itemProductDetail: itemProductDetail,
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
            productCategoryItems: productCategoryItems,
            availableProductCategoryItems: availableProductCategoryItems,
            productCategoryElement: productCategoryElement,
            itemSections: itemSections,
            itemPriceMatricesForCurrentFlag: itemPriceMatricesForCurrentFlag,
            itemPriceMatricesForSupplierId1: itemPriceMatricesForSupplierId1,
            itemPriceMatricesForSupplierId2: itemPriceMatricesForSupplierId2,
            canAddItemStockOption: canAddItemStockOption,
            addItemStockOption: addItemStockOption,
            removeItemStockOption: removeItemStockOption,
            wireupItemStockOptionTabEvents: wireupItemStockOptionTabEvents,
            chooseStockItem: chooseStockItem,
            activeStockOption: activeStockOption,
            activeItemSection: activeItemSection,
            onSelectStockItem: onSelectStockItem,
            onAddItemCostCentre: onAddItemCostCentre,
            onEditItemCostCentre: onEditItemCostCentre,
            onSaveItemCostCentre: onSaveItemCostCentre,
            addItemStateTax: addItemStateTax,
            removeItemStateTax: removeItemStateTax,
            chooseTemplateForPriceMatrix: chooseTemplateForPriceMatrix,
            selectedPriceMatrixItem: selectedPriceMatrixItem,
            selectPriceMatrixItem: selectPriceMatrixItem,
            chooseTemplateForSupplier1PriceMatrix: chooseTemplateForSupplier1PriceMatrix,
            selectedPriceMatrixItemForSupplier1: selectedPriceMatrixItemForSupplier1,
            selectPriceMatrixItemForSupplier1: selectPriceMatrixItemForSupplier1,
            chooseTemplateForSupplier2PriceMatrix: chooseTemplateForSupplier2PriceMatrix,
            selectedPriceMatrixItemForSupplier2: selectedPriceMatrixItemForSupplier2,
            selectPriceMatrixItemForSupplier2: selectPriceMatrixItemForSupplier2,
            chooseTemplateForStateTax: chooseTemplateForStateTax,
            selectedStateTaxItem: selectedStateTaxItem,
            selectStateTaxItem: selectStateTaxItem,
            setItemPriceMatrices: setItemPriceMatrices,
            removeExistingPriceMatrices: removeExistingPriceMatrices,
            setValidationSummary: setValidationSummary,
            updateProductCategoryItems: updateProductCategoryItems,
            canAddItemSection: canAddItemSection,
            canRemoveItemSection: canRemoveItemSection,
            updateItemSectionOnPrintItemToggle: updateItemSectionOnPrintItemToggle,
            addItemSection: addItemSection,
            removeItemSection: removeItemSection,
            chooseStockItemForSection: chooseStockItemForSection,
            selectItemSection: selectItemSection,
            onSelectPressItem: onSelectPressItem,
            onSelectImage: onSelectImage,
            itemImages: itemImages,
            onSelectItemImage: onSelectItemImage,
            isStoreTaxUi: isStoreTaxUi,
            resetFiles: resetFiles,
            removeTemplateLayoutFile: removeTemplateLayoutFile,
            removeItemImage: removeItemImage,
            self: self,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            itemVdpPriceListHasChanges: itemVdpPriceListHasChanges,
            updateQuantitiesForSupplier: updateQuantitiesForSupplier,
            productMarketBriefQuestions: productMarketBriefQuestions,
            moveProductMarketBriefQuestionUp: moveProductMarketBriefQuestionUp,
            moveProductMarketBriefQuestionDown: moveProductMarketBriefQuestionDown,
            addProductMarketBriefQuestion: addProductMarketBriefQuestion,
            removeProductMarketBriefQuestion: removeProductMarketBriefQuestion,
            onDeleteProductMarketBriefQuestion: onDeleteProductMarketBriefQuestion,
            activeProductMarketQuestion: activeProductMarketQuestion,
            onAddProductMarketBriefQuestion: onAddProductMarketBriefQuestion,
            onEditProductMarketBriefQuestion: onEditProductMarketBriefQuestion,
            onRemoveItemRelatedItem: onRemoveItemRelatedItem,
            selectStockItemForStockOption: selectStockItemForStockOption,
            selectStockItemForSection: selectStockItemForSection,
            selectStockItemForStockOptionForNewProduct: selectStockItemForStockOptionForNewProduct,
            selectStockItemForSectionForNewProduct: selectStockItemForSectionForNewProduct,
            shouldValidateCategory: shouldValidateCategory,
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
                };
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
                };
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
                };
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
    Template = function (specifiedId, specifiedPdfTemplateWidth, specifiedPdfTemplateHeight, specifiedIsCreatedManual, specifiedIsSpotTemplate, specifiedFileSource) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Is Created Manual
            isCreatedManual = ko.observable(specifiedIsCreatedManual !== null && specifiedIsCreatedManual !== undefined ? specifiedIsCreatedManual :
                (!specifiedId ? true : undefined)),
            // Is Created Manual Changed
            isCreatedManualUi = ko.computed({
                read: function () {
                    return isCreatedManual();
                },
                write: function (value) {
                    if (value === isCreatedManual()) {
                        return;
                    }

                    isCreatedManual(value);
                    if (specifiedIsCreatedManual === false) {
                        specifiedIsCreatedManual = value;
                    }
                }
            }),
            // Pdf Template Width
            pdfTemplateWidth = ko.observable(specifiedPdfTemplateWidth || 95).extend({
                required: {
                    onlyIf: function () {
                        return isCreatedManual() === true;
                    }
                }
            }),
            // Pdf Template Height
            pdfTemplateHeight = ko.observable(specifiedPdfTemplateHeight || 65).extend({
                required: {
                    onlyIf: function () {
                        return isCreatedManual() === true;
                    }
                }
            }),
            // Is Spot Template
            isSpotTemplate = ko.observable(specifiedIsSpotTemplate !== null && specifiedIsSpotTemplate !== undefined ? specifiedIsSpotTemplate :
                (!specifiedId ? true : undefined)),
            // File Source
            fileSource = ko.observable(specifiedFileSource).extend({
                required: {
                    onlyIf: function () {
                        return specifiedIsCreatedManual !== false && isCreatedManual() === false;
                    }
                }
            }),
            // File Name
            fileName = ko.observable(),
            // Template Pages
            templatePages = ko.observableArray([]),
            // Can add Template Pages
            canAddTemplatePages = ko.computed(function () {
                return isCreatedManual() || (specifiedIsCreatedManual !== false && isCreatedManual() === false && !fileSource());
            }),
            // Add Template Page
            addTemplatePage = function () {
                templatePages.push(TemplatePage.Create({ ProductId: id(), Width: pdfTemplateWidth(), Height: pdfTemplateHeight() }));
            },
            // Add Default Template Page For New Product
            addDefaultTemplatePage = function () {
                templatePages.push(TemplatePage.Create({ ProductId: id(), Width: pdfTemplateWidth(), Height: pdfTemplateHeight(), PageName: "Front" }));
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
            // On Select File
            onSelectFile = function (file, data) {
                fileSource(data);
                fileName(file.name);
            },
            // Errors
            errors = ko.validation.group({
                fileSource: fileSource,
                pdfTemplateWidth: pdfTemplateWidth,
                pdfTemplateHeight: pdfTemplateHeight
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && templatePages.filter(function (templatePage) {
                    return !templatePage.isValid();
                }).length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                pdfTemplateWidth: pdfTemplateWidth,
                pdfTemplateHeight: pdfTemplateHeight,
                isCreatedManual: isCreatedManual,
                isSpotTemplate: isSpotTemplate,
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
                    IsCreatedManual: isCreatedManual(),
                    IsSpotTemplate: isSpotTemplate(),
                    FileName: fileName(),
                    FileSource: fileSource(),
                    TemplatePages: templatePages.map(function (templatePage, index) {
                        var templatePageItem = templatePage.convertToServerData();
                        templatePageItem.PageNo = index + 1;
                        return templatePageItem;
                    })
                };
            };

        return {
            id: id,
            pdfTemplateWidth: pdfTemplateWidth,
            pdfTemplateHeight: pdfTemplateHeight,
            isCreatedManual: isCreatedManual,
            isCreatedManualUi: isCreatedManualUi,
            isSpotTemplate: isSpotTemplate,
            canAddTemplatePages: canAddTemplatePages,
            fileSource: fileSource,
            fileName: fileName,
            onSelectFile: onSelectFile,
            templatePages: templatePages,
            addTemplatePage: addTemplatePage,
            addDefaultTemplatePage: addDefaultTemplatePage,
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
    TemplatePage = function (specifiedId, specifiedWidth, specifiedHeight, specifiedPageName, specifiedPageNo, specifiedOrientation, specifiedProductId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Width
            width = ko.observable(specifiedWidth || undefined),
            // Height
            height = ko.observable(specifiedHeight || undefined),
            // Page Name
            pageName = ko.observable(specifiedPageName || undefined).extend({ required: true }),
            // Page No
            pageNo = ko.observable(specifiedPageNo || undefined),
            // Orientation
            orientation = ko.observable(specifiedOrientation || 1),
            // Product Id
            productId = ko.observable(specifiedProductId || 0),
            // Errors
            errors = ko.validation.group({
                pageName: pageName
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
                    Orientation: orientation(),
                    ProductId: productId()
                };
            };

        return {
            id: id,
            productId: productId,
            width: width,
            height: height,
            pageName: pageName,
            pageNo: pageNo,
            orientation: orientation,
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
        specifiedOptionSequence, specifiedItemId, callbacks, specifiedInStock, specifiedAllocated, specifiedItem) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Label
            label = ko.observable(specifiedStockLabel || undefined).extend({
                required: {
                    onlyIf: function () {
                        return (specifiedItem && parseInt(specifiedItem.productType()) !== 2);
                    }
                }
            }),
            // in stock
            inStock = ko.observable(specifiedInStock || undefined),
            // allocated
            allocated = ko.observable(specifiedAllocated || undefined),
            // Stock Item Id
            stockItemId = ko.observable(specifiedStockId || undefined),
            // Stock Item Name
            stockItemName = ko.observable(specifiedStockItemName || undefined),
            // Stock Item Description
            stockItemDescription = ko.observable(specifiedStockItemDescription || undefined),
            // image
            image = ko.observable(specifiedImage || undefined),
            // file source
            fileSource = ko.observable(),
            // file name
            fileName = ko.observable(),
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
                //To Enable Dirty Flag
                activeItemAddonCostCentre().isMandatory(false);
            },
            onEditItemAddonCostCentre = function (itemAddonCostCentre) {
                activeItemAddonCostCentre(itemAddonCostCentre);
                activeItemAddonCostCentre().reset();
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
            removeItemAddonCostCentre = function () {
                itemAddonCostCentres.remove(activeItemAddonCostCentre());
            },
            // Select Stock Item
            selectStock = function (stockItem) {
                if (!stockItem || stockItemId() === stockItem.id) {
                    return;
                }

                stockItemId(stockItem.id);
                stockItemName(stockItem.name);
                stockItemDescription(stockItem.description);
                label(stockItem.name);
                inStock(stockItem.inStock);
                allocated(stockItem.allocated);
            },
            // On Select File
            onSelectImage = function (file, data) {
                image(data);
                fileSource(data);
                fileName(file.name);
            },
            // Errors
            errors = ko.validation.group({
                label: label
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && itemAddonCostCentres.filter(function (itemAddonCostCentre) {
                    return !itemAddonCostCentre.isValid();
                }).length === 0;
            }),
            // True if the Item Vdp Price has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                stockItemId: stockItemId,
                label: label,
                image: image,
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
                    FileSource: fileSource(),
                    FileName: fileName(),
                    OptionSequence: optionSequence(),
                    ItemAddOnCostCentres: itemAddonCostCentres.map(function (itemAddonCostCentre) {
                        return itemAddonCostCentre.convertToServerData();
                    })
                };
            };

        return {
            id: id,
            stockItemId: stockItemId,
            label: label,
            inStock: inStock,
            allocated: allocated,
            stockItemName: stockItemName,
            stockItemDescription: stockItemDescription,
            itemId: itemId,
            image: image,
            fileSource: fileSource,
            fileName: fileName,
            optionSequence: optionSequence,
            activeItemAddonCostCentre: activeItemAddonCostCentre,
            itemAddonCostCentres: itemAddonCostCentres,
            addItemAddonCostCentre: addItemAddonCostCentre,
            removeItemAddonCostCentre: removeItemAddonCostCentre,
            onAddItemAddonCostCentre: onAddItemAddonCostCentre,
            onEditItemAddonCostCentre: onEditItemAddonCostCentre,
            saveItemAddonCostCentre: saveItemAddonCostCentre,
            selectStock: selectStock,
            onSelectImage: onSelectImage,
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
                };
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

    // Item Section Entity
    ItemSection = function (specifiedId, specifiedSectionNo, specifiedSectionName, specifiedSectionSizeId, specifiedItemSizeId, specifiedIsSectionSizeCustom,
        specifiedSectionSizeHeight, specifiedSectionSizeWidth, specifiedIsItemSizeCustom, specifiedItemSizeHeight, specifiedItemSizeWidth,
        specifiedPressId, specifiedStockItemId, specifiedStockItemName, specifiedPressName, specifiedItemId, specifiedIsDoubleSided, specifiedIsWorknTurn,
        specifiedPrintViewLayoutPortrait, specifiedPrintViewLayoutLandscape, specifiedIsPortrait, specifiedPressIdSide2, specifiedImpressionCoverageSide1,
        specifiedImpressionCoverageSide2, specifiedPrintingType, specifiedPressSide1ColourHeads, specifiedPressSide1IsSpotColor,
        specifiedPressSide2ColourHeads, specifiedPressSide2IsSpotColor, specifiedStockItemPackageQty, specifiedItemGutterHorizontal, specifiedItem) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // name
            name = ko.observable(specifiedSectionName || undefined).extend({
                required:
                {
                    onlyIf: function () {
                        return (specifiedItem && (parseInt(specifiedItem.productType()) !== 2 && parseInt(specifiedItem.productType()) !== 3));
                    }
                }
            }),
            // Stock Item Id
            stockItemId = ko.observable(specifiedStockItemId || undefined).extend({
                required: {
                    onlyIf: function () {
                        return (specifiedItem && (parseInt(specifiedItem.productType()) !== 2 && parseInt(specifiedItem.productType()) !== 3));
                    }
                }
            }),
            // Stock Item Name
            stockItemName = ko.observable(specifiedStockItemName || undefined),
            // Stock Item Package Qty
            stockItemPackageQty = ko.observable(specifiedStockItemPackageQty || undefined),
            // Press Id
            pressId = ko.observable(specifiedPressId || undefined),
            // Press Name
            pressName = ko.observable(specifiedPressName || undefined),
            // Press Id Side 2
            pressIdSide2 = ko.observable(specifiedPressIdSide2 || undefined),
            // section size id
            sectionSizeId = ko.observable(specifiedSectionSizeId || undefined),
            // Item size id
            itemSizeId = ko.observable(specifiedItemSizeId || undefined),
            // Section No
            sectionNo = ko.observable(specifiedSectionNo || undefined),
            // Is Section Size Custom
            isSectionSizeCustom = ko.observable(specifiedIsSectionSizeCustom || undefined),
            // Is Item Size Custom
            isItemSizeCustom = ko.observable(specifiedIsItemSizeCustom || undefined),
            // Section Size Height
            sectionSizeHeight = ko.observable(specifiedSectionSizeHeight || undefined),
            // Section Size Width
            sectionSizeWidth = ko.observable(specifiedSectionSizeWidth || undefined),
            // Item Size Height
            itemSizeHeight = ko.observable(specifiedItemSizeHeight || undefined),
            // Item Size Width
            itemSizeWidth = ko.observable(specifiedItemSizeWidth || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || undefined),
            // Is Double Sided
            isDoubleSided = ko.observable(specifiedIsDoubleSided || false),
            // Is Work N Turn
            isWorknTurn = ko.observable(specifiedIsWorknTurn || false),
            // Is Double Sided Ui
            isDoubleSidedUi = ko.computed({
                read: function () {
                    return isDoubleSided();
                },
                write: function (value) {
                    if (value === isDoubleSided()) {
                        return;
                    }

                    // Single Side
                    if (!value) {
                        isWorknTurn(false);
                    }

                    isDoubleSided(value);
                }
            }),
            isPortrait = ko.observable(specifiedIsPortrait),
            printViewLayout = ko.observable(),
            // PrintViewLayoutPortrait
            printViewLayoutPortrait = ko.observable(specifiedPrintViewLayoutPortrait || 0),
            // PrintViewLayoutLandscape
            printViewLayoutLandscape = ko.observable(specifiedPrintViewLayoutLandscape || 0),
            // Number Up
            numberUp = ko.computed(function () {
                if (printViewLayoutPortrait() >= printViewLayoutLandscape()) {
                    printViewLayout(0);
                    return printViewLayoutPortrait();
                } else if (printViewLayoutPortrait() <= printViewLayoutLandscape()) {
                    printViewLayout(1);
                    return printViewLayoutLandscape();
                }

                return 0;
            }),
            // Printing Type
            printingType = ko.observable(specifiedPrintingType || 1),
            // Printing Type Ui
            printingTypeUi = ko.computed({
                read: function () {
                    return '' + printingType();
                },
                write: function (value) {
                    if (!value) {
                        return;
                    }
                    var printingValue = parseInt(value);
                    if (printingValue === printingType()) {
                        return;
                    }
                    printingType(printingValue);
                    if (printingValue === 2) { // Hide Number Up and set it as 1
                        printViewLayoutPortrait(0);
                        printViewLayoutLandscape(1);
                        // If Initialized
                        if (isDoubleSidedUi) {
                            isDoubleSidedUi(false);
                        }
                    }
                }
            }),
            // Section Ink Coverage List
            sectionInkCoverageList = ko.observableArray([]),
            // section Ink Coverage Side1
            sectionInkCoveragesSide1 = ko.computed(function () {
                if (sectionInkCoverageList().length === 0) {
                    return [];
                }
                return sectionInkCoverageList.filter(function (sectionInkCoverage) {
                    return sectionInkCoverage.side() === 1;
                });
            }),
            // section Ink Coverage Side2
            sectionInkCoveragesSide2 = ko.computed(function () {
                if (sectionInkCoverageList().length === 0) {
                    return [];
                }
                return sectionInkCoverageList.filter(function (sectionInkCoverage) {
                    return sectionInkCoverage.side() === 2;
                });
            }),
            // Impression Coverage Side 1
            impressionCoverageSide1 = ko.observable(specifiedImpressionCoverageSide1 || 3),
            // Impression Coverage Side 2
            impressionCoverageSide2 = ko.observable(specifiedImpressionCoverageSide2),
            // Press Id Side 1 Colour Heads
            pressIdSide1ColourHeads = ko.observable(specifiedPressSide1ColourHeads || 0),
            // Press Id Side 2 Colour Heads
            pressIdSide2ColourHeads = ko.observable(specifiedPressSide2ColourHeads || 0),
            // Press Id Side 1 Is Spot Color
            pressIdSide1IsSpotColor = ko.observable(specifiedPressSide1IsSpotColor || false),
            // Press Id Side 2 Is Spot Color
            pressIdSide2IsSpotColor = ko.observable(specifiedPressSide2IsSpotColor || false),
            // Item Gutter Horizontal
            itemGutterHorizontal = ko.observable(specifiedItemGutterHorizontal || 0).extend({ number: true, min: 0, max: 99 }),
            // Select Stock Item
            selectStock = function (stockItem) {
                if (!stockItem || stockItemId() === stockItem.id) {
                    return;
                }

                stockItemId(stockItem.id);
                stockItemName(stockItem.name);
                stockItemPackageQty(stockItem.packageQty);
            },
            // Select Press
            selectPress = function (press) {
                if (!press || pressId() === press.id) {
                    return;
                }

                pressId(press.id);
                pressName(press.name);
            },
            // Swap Section Height and Width
            swapSectionHeightWidth = function () {
                var sectionHeight = sectionSizeHeight();
                sectionSizeHeight(sectionSizeWidth());
                sectionSizeWidth(sectionHeight);
            },
            // Swap Item Size Height and Width
            swapItemHeightWidth = function () {
                var itemHeight = itemSizeHeight();
                itemSizeHeight(itemSizeWidth());
                itemSizeWidth(itemHeight);
            },
            // Errors
            errors = ko.validation.group({
                name: name,
                stockItemId: stockItemId,
                itemGutterHorizontal: itemGutterHorizontal
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
            }),
            // True if the Item Section has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                stockItemId: stockItemId,
                pressId: pressId,
                name: name,
                sectionSizeId: sectionSizeId,
                itemSizeId: itemSizeId,
                isSectionSizeCustom: isSectionSizeCustom,
                isItemSizeCustom: isItemSizeCustom,
                sectionSizeHeight: sectionSizeHeight,
                sectionSizeWidth: sectionSizeWidth,
                itemSizeHeight: itemSizeHeight,
                itemSizeWidth: itemSizeWidth,
                isDoubleSided: isDoubleSided,
                isWorknTurn: isWorknTurn,
                printViewLayoutPortrait: printViewLayoutPortrait,
                printViewLayoutLandscape: printViewLayoutLandscape,
                pressIdSide2: pressIdSide2,
                impressionCoverageSide1: impressionCoverageSide1,
                impressionCoverageSide2: impressionCoverageSide2,
                printingType: printingType,
                itemGutterHorizontal: itemGutterHorizontal
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
                    ItemSectionId: id(),
                    SectionName: name(),
                    SectionNo: sectionNo(),
                    StockItemID1: stockItemId(),
                    PressId: pressId(),
                    ItemId: itemId(),
                    SectionSizeId: sectionSizeId(),
                    ItemSizeId: itemSizeId(),
                    IsSectionSizeCustom: isSectionSizeCustom(),
                    IsItemSizeCustom: isItemSizeCustom(),
                    SectionSizeHeight: sectionSizeHeight(),
                    SectionSizeWidth: sectionSizeWidth(),
                    ItemSizeHeight: itemSizeHeight(),
                    ItemSizeWidth: itemSizeWidth(),
                    IsDoubleSided: isDoubleSided(),
                    IsWorknTurn: isWorknTurn(),
                    IsPortrait: isPortrait(),
                    PrintViewLayout: printViewLayout(),
                    PrintViewLayoutPortrait: printViewLayoutPortrait(),
                    PrintViewLayoutLandscape: printViewLayoutLandscape(),
                    PressIdSide2: pressIdSide2(),
                    ImpressionCoverageSide1: impressionCoverageSide1(),
                    ImpressionCoverageSide2: impressionCoverageSide2(),
                    PrintingType: printingType(),
                    ItemGutterHorizontal: itemGutterHorizontal(),
                    SectionInkCoverages: sectionInkCoverageList.map(function (sic) {
                        var inkCoverage = sic.convertToServerData();
                        return inkCoverage;
                    })
                };
            };

        return {
            id: id,
            stockItemId: stockItemId,
            pressId: pressId,
            stockItemName: stockItemName,
            stockItemPackageQty: stockItemPackageQty,
            pressName: pressName,
            name: name,
            itemId: itemId,
            sectionNo: sectionNo,
            sectionSizeId: sectionSizeId,
            itemSizeId: itemSizeId,
            isSectionSizeCustom: isSectionSizeCustom,
            isItemSizeCustom: isItemSizeCustom,
            sectionSizeHeight: sectionSizeHeight,
            sectionSizeWidth: sectionSizeWidth,
            itemSizeHeight: itemSizeHeight,
            itemSizeWidth: itemSizeWidth,
            isDoubleSided: isDoubleSided,
            isDoubleSidedUi: isDoubleSidedUi,
            isWorknTurn: isWorknTurn,
            printViewLayoutPortrait: printViewLayoutPortrait,
            printViewLayoutLandscape: printViewLayoutLandscape,
            isPortrait: isPortrait,
            pressIdSide2: pressIdSide2,
            impressionCoverageSide1: impressionCoverageSide1,
            impressionCoverageSide2: impressionCoverageSide2,
            printingType: printingType,
            swapSectionHeightWidth: swapSectionHeightWidth,
            swapItemHeightWidth: swapItemHeightWidth,
            numberUp: numberUp,
            printingTypeUi: printingTypeUi,
            itemGutterHorizontal: itemGutterHorizontal,
            pressIdSide1ColourHeads: pressIdSide1ColourHeads,
            pressIdSide2ColourHeads: pressIdSide2ColourHeads,
            pressIdSide1IsSpotColor: pressIdSide1IsSpotColor,
            pressIdSide2IsSpotColor: pressIdSide2IsSpotColor,
            sectionInkCoveragesSide1: sectionInkCoveragesSide1,
            sectionInkCoveragesSide2: sectionInkCoveragesSide2,
            sectionInkCoverageList: sectionInkCoverageList,
            selectStock: selectStock,
            selectPress: selectPress,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Stock Item Entity        
    StockItem = function (specifiedId, specifiedName, specifiedCategoryName, specifiedLocation, specifiedWeight, specifiedDescription, specifiedPackageQty,
        specifiedAllocated, specifiedInStock) {
        return {
            id: specifiedId,
            name: specifiedName,
            category: specifiedCategoryName,
            location: specifiedLocation,
            weight: specifiedWeight,
            description: specifiedDescription,
            packageQty: specifiedPackageQty,
            allocated: specifiedAllocated,
            inStock: specifiedInStock
        };
    },

    // Machine Entity        
    Machine = function (specifiedId, specifiedName, specifiedDefaultPageId, specifiedMaxSheetHeight, specifiedMaxSheetWeight, specifiedMaxSheetWidth,
        specifiedMinSheetHeight, specifiedMinSheetWidth, specifiedMachineCatId, specifiedColourHeads, specifiedIsSpotColor) {
        return {
            id: specifiedId,
            name: specifiedName,
            categoryId: specifiedMachineCatId,
            defaultPageId: specifiedDefaultPageId,
            maxSheetHeight: specifiedMaxSheetHeight,
            maxSheetWeight: specifiedMaxSheetWeight,
            maxSheetWidth: specifiedMaxSheetWidth,
            minSheetWidth: specifiedMinSheetWidth,
            minSheetHeight: specifiedMaxSheetHeight,
            colourHeads: specifiedColourHeads,
            isSpotColor: specifiedIsSpotColor
        };
    },

    // Paper Size Entity        
    PaperSize = function (specifiedId, specifiedName, specifiedHeight, specifiedWidth) {
        return {
            id: specifiedId,
            name: specifiedName,
            height: specifiedHeight,
            width: specifiedWidth
        };
    },

    // Cost Centre Entity        
    CostCentre = function (specifiedId, specifiedName, specifiedType) {
        return {
            id: specifiedId,
            name: specifiedName,
            type: specifiedType
        };
    },

    // Country Entity        
    Country = function (specifiedId, specifiedName, specifiedCode) {
        return {
            id: specifiedId,
            name: specifiedName,
            type: specifiedCode
        };
    },

    // State Entity        
    State = function (specifiedId, specifiedName, specifiedCode, specifiedCountryId) {
        return {
            id: specifiedId,
            name: specifiedName,
            type: specifiedCode,
            countryId: specifiedCountryId
        };
    },

    // Section Flag Entity        
    SectionFlag = function (specifiedId, specifiedFlagName, specifiedFlagColor) {
        return {
            id: specifiedId,
            name: specifiedFlagName,
            color: specifiedFlagColor
        };
    },

    // Company Entity        
    Company = function (specifiedId, specifiedName) {
        return {
            id: specifiedId,
            name: specifiedName
        };
    },

    // Product Category For Template Entity        
    ProductCategoryForTemplate = function (specifiedId, specifiedName, specifiedRegionId, specifiedCategoryTypeId, specifiedZoomFactor,
        specifiedScalarFactor) {
        return {
            id: specifiedId,
            name: specifiedName,
            regionId: specifiedRegionId,
            typeId: specifiedCategoryTypeId,
            zoomFactor: specifiedZoomFactor,
            scalarFactor: specifiedScalarFactor
        };
    },

    // Category Region Entity        
    CategoryRegion = function (specifiedId, specifiedName) {
        return {
            id: specifiedId,
            name: specifiedName
        };
    },

    // Company Type Entity        
    CategoryType = function (specifiedId, specifiedName) {
        return {
            id: specifiedId,
            name: specifiedName
        };
    },

    // Item Price Matrix Entity
    ItemPriceMatrix = function (specifiedId, specifiedQuantity, specifiedQtyRangedFrom, specifiedQtyRangedTo, specifiedPricePaperType1, specifiedPricePaperType2,
        specifiedPricePaperType3, specifiedPriceStockType4, specifiedPriceStockType5, specifiedPriceStockType6, specifiedPriceStockType7, specifiedPriceStockType8,
        specifiedPriceStockType9, specifiedPriceStockType10, specifiedPriceStockType11, specifiedFlagId, specifiedSupplierId, specifiedSupplierSequence,
        specifiedItemId, callbacks) {
        // ReSharper restore InconsistentNaming
        var
            // Self Reference
            self,
            // Unique key
            id = ko.observable(specifiedId || 0),
            // Quantity
            quantity = ko.observable(specifiedQuantity || 0).extend({ number: true }),
            // Qty Ranged From
            qtyRangedFrom = ko.observable(specifiedQtyRangedFrom || 0).extend({ number: true }),
            // Qty Ranged To
            qtyRangedTo = ko.observable(specifiedQtyRangedTo || 0).extend({ number: true }),
            // Qty Change 
            quantityUi = ko.computed({
                read: function () {
                    return quantity();
                },
                write: function (value) {
                    if ((value === null || value === undefined) || value === quantity()) {
                        return;
                    }

                    quantity(value);
                    if (!supplierId() && callbacks && callbacks.onPriceMatrixQuantityChange && typeof callbacks.onPriceMatrixQuantityChange === "function") {
                        callbacks.onPriceMatrixQuantityChange(self);
                    }
                }
            }),
            // Qty Ranged From Change 
            quantityRangedFromUi = ko.computed({
                read: function () {
                    return qtyRangedFrom();
                },
                write: function (value) {
                    if ((value === null || value === undefined) || value === qtyRangedFrom()) {
                        return;
                    }

                    qtyRangedFrom(value);
                    if (!supplierId() && callbacks && callbacks.onPriceMatrixQuantityChange && typeof callbacks.onPriceMatrixQuantityChange === "function") {
                        callbacks.onPriceMatrixQuantityChange(self);
                    }
                }
            }),
            // Qty Ranged To Change 
            quantityRangedToUi = ko.computed({
                read: function () {
                    return qtyRangedTo();
                },
                write: function (value) {
                    if ((value === null || value === undefined) || value === qtyRangedTo()) {
                        return;
                    }

                    qtyRangedTo(value);
                    if (!supplierId() && callbacks && callbacks.onPriceMatrixQuantityChange && typeof callbacks.onPriceMatrixQuantityChange === "function") {
                        callbacks.onPriceMatrixQuantityChange(self);
                    }
                }
            }),
            // Flag Id
            flagId = ko.observable(specifiedFlagId || undefined),
            // Supplier Id
            supplierId = ko.observable(specifiedSupplierId || undefined),
            // Supplier Sequence
            supplierSequence = ko.observable(specifiedSupplierSequence || undefined),
            // Price Paper Type1
            pricePaperType1 = ko.observable(specifiedPricePaperType1 || 0),
            // Price Paper Type1 Ui
            pricePaperType1Ui = ko.computed(function () {
                if (!pricePaperType1()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType1();
            }),
            // Price Paper Type2
            pricePaperType2 = ko.observable(specifiedPricePaperType2 || 0),
            // Price Paper Type2 Ui
            pricePaperType2Ui = ko.computed(function () {
                if (!pricePaperType2()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType2();
            }),
            // Price Paper Type3
            pricePaperType3 = ko.observable(specifiedPricePaperType3 || 0),
            // Price Paper Type3 Ui
            pricePaperType3Ui = ko.computed(function () {
                if (!pricePaperType3()) {
                    return '$ 0.00';
                }

                return '$ ' + pricePaperType3();
            }),
            // Price Stock Type4
            priceStockType4 = ko.observable(specifiedPriceStockType4 || 0),
            // Price Stock Type4 Ui
            priceStockType4Ui = ko.computed(function () {
                if (!priceStockType4()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType4();
            }),
            // Price Stock Type5
            priceStockType5 = ko.observable(specifiedPriceStockType5 || 0),
            // Price Stock Type5 Ui
            priceStockType5Ui = ko.computed(function () {
                if (!priceStockType5()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType5();
            }),
            // Price Stock Type6
            priceStockType6 = ko.observable(specifiedPriceStockType6 || 0),
            // Price Stock Type6 Ui
            priceStockType6Ui = ko.computed(function () {
                if (!priceStockType6()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType6();
            }),
            // Price Stock Type7
            priceStockType7 = ko.observable(specifiedPriceStockType7 || 0),
            // Price Stock Type7 Ui
            priceStockType7Ui = ko.computed(function () {
                if (!priceStockType7()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType7();
            }),
            // Price Stock Type8
            priceStockType8 = ko.observable(specifiedPriceStockType8 || 0),
            // Price Stock Type8 Ui
            priceStockType8Ui = ko.computed(function () {
                if (!priceStockType8()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType8();
            }),
            // Price Stock Type9
            priceStockType9 = ko.observable(specifiedPriceStockType9 || 0),
            // Price Stock Type9 Ui
            priceStockType9Ui = ko.computed(function () {
                if (!priceStockType4()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType9();
            }),
            // Price Stock Type10
            priceStockType10 = ko.observable(specifiedPriceStockType10 || 0),
            // Price Stock Type10 Ui
            priceStockType10Ui = ko.computed(function () {
                if (!priceStockType10()) {
                    return '$ 0.00';
                }

                return '$ ' + priceStockType10();
            }),
            // Price Stock Type11
            priceStockType11 = ko.observable(specifiedPriceStockType11 || 0),
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
                flagId: flagId,
                supplierId: supplierId,
                supplierSequence: supplierSequence,
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
                    QtyRangeFrom: qtyRangedFrom(),
                    QtyRangeTo: qtyRangedTo(),
                    FlagId: flagId(),
                    SupplierId: supplierId(),
                    SupplierSequence: supplierSequence(),
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
                };
            };

        self = {
            id: id,
            itemId: itemId,
            quantity: quantity,
            qtyRangedFrom: qtyRangedFrom,
            qtyRangedTo: qtyRangedTo,
            quantityUi: quantityUi,
            quantityRangedFromUi: quantityRangedFromUi,
            quantityRangedToUi: quantityRangedToUi,
            flagId: flagId,
            supplierId: supplierId,
            supplierSequence: supplierSequence,
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

        return self;
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
            taxRateUi = ko.computed(function () {
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

                    var countryResult = countries.find(function (country) {
                        return country.id === value;
                    });

                    if (!countryResult) {
                        return;
                    }

                    countryName(countryResult.name);
                }
            }),
            // Country States
            countryStates = ko.computed(function () {
                if (!countryId()) {
                    return [];
                }

                return states.filter(function (state) {
                    return state.countryId === countryId();
                });
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

                    var stateResult = _.find(countryStates(), function (state) {
                        return state.id === value;
                    });

                    if (!stateResult) {
                        return;
                    }

                    stateName(stateResult.name);
                }
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
                };
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
    },

    // Item Product Detail Entity
    ItemProductDetail = function (specifiedId, specifiedIsInternalActivity, specifiedIsAutoCreateSupplierPO, specifiedIsQtyLimit, specifiedQtyLimit,
        specifiedDeliveryTimeSupplier1, specifiedDeliveryTimeSupplier2, specifiedIsPrintItem, specifiedIsAllowMarketBriefAttachment, specifiedMarketBriefSuccessMessage,
        specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Is Internal Activity
            isInternalActivity = ko.observable(specifiedIsInternalActivity || false),
            // Is Auto Create Supplier PO
            isAutoCreateSupplierPo = ko.observable(specifiedIsAutoCreateSupplierPO || true),
            // Is Qty Limit
            isQtyLimit = ko.observable(specifiedIsQtyLimit || true),
            // Qty Limit
            qtyLimit = ko.observable(specifiedQtyLimit || undefined),
            // Delivery Time Supplier1
            deliveryTimeSupplier1 = ko.observable(specifiedDeliveryTimeSupplier1 || undefined),
            // Delivery Time Supplier2
            deliveryTimeSupplier2 = ko.observable(specifiedDeliveryTimeSupplier2 || undefined),
            // Is Internal Activity Ui
            isInternalActivityUi = ko.computed({
                read: function () {
                    return '' + isInternalActivity();
                },
                write: function (value) {
                    if (!value || value === isInternalActivity()) {
                        return;
                    }

                    isInternalActivity(value);
                }
            }),
            // Is Print Item
            isPrintItem = ko.observable(specifiedIsPrintItem !== undefined && specifiedIsPrintItem !== null ? (specifiedIsPrintItem === true ? 1 : 2) : 1),
            // Is Print Item Ui
            isPrintItemUi = ko.computed({
                read: function () {
                    return '' + isPrintItem();
                },
                write: function (value) {
                    if (!value || value === isPrintItem()) {
                        return;
                    }

                    isPrintItem(value);
                }
            }),
            // Is Allow Market Brief Attachment
            isAllowMarketBriefAttachment = ko.observable(specifiedIsAllowMarketBriefAttachment || false),
            // Market Brief Success Message
            marketBriefSuccessMessage = ko.observable(specifiedMarketBriefSuccessMessage || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
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
                isInternalActivity: isInternalActivity,
                isAutoCreateSupplierPo: isAutoCreateSupplierPo,
                isQtyLimit: isQtyLimit,
                qtyLimit: qtyLimit,
                deliveryTimeSupplier1: deliveryTimeSupplier1,
                deliveryTimeSupplier2: deliveryTimeSupplier2,
                isPrintItem: isPrintItem
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
                    ItemDetailId: id(),
                    ItemId: itemId(),
                    IsInternalActivity: isInternalActivity(),
                    IsAutoCreateSupplierPO: isAutoCreateSupplierPo(),
                    IsQtyLimit: isQtyLimit(),
                    QtyLimit: qtyLimit(),
                    IsPrintItem: isPrintItemUi() === '1',
                    DeliveryTimeSupplier1: deliveryTimeSupplier1(),
                    DeliveryTimeSupplier2: deliveryTimeSupplier2(),
                    IsAllowMarketBriefAttachment: isAllowMarketBriefAttachment(),
                    MarketBriefSuccessMessage: marketBriefSuccessMessage()
                };
            };

        return {
            id: id,
            itemId: itemId,
            isInternalActivity: isInternalActivity,
            isInternalActivityUi: isInternalActivityUi,
            isAutoCreateSupplierPo: isAutoCreateSupplierPo,
            isQtyLimit: isQtyLimit,
            qtyLimit: qtyLimit,
            deliveryTimeSupplier1: deliveryTimeSupplier1,
            deliveryTimeSupplier2: deliveryTimeSupplier2,
            isPrintItem: isPrintItem,
            isPrintItemUi: isPrintItemUi,
            marketBriefSuccessMessage: marketBriefSuccessMessage,
            isAllowMarketBriefAttachment: isAllowMarketBriefAttachment,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Product Category Entity
    ProductCategory = function (specifiedId, specifiedName, specifiedIsSelected, specifiedParentCategoryId, specifiedIsArchived) {
        // True If Selected
        var isSelected = ko.observable(specifiedIsSelected || undefined);

        return {
            id: specifiedId,
            name: specifiedName,
            isSelected: isSelected,
            parentCategoryId: specifiedParentCategoryId,
            isArchived: specifiedIsArchived
        };
    },

    // Product Category Item Entity
    ProductCategoryItem = function (specifiedId, specifiedCategoryId, specifiedIsSelected, specifiedCategoryName, specifiedItemId) {
        var
            // Unique Id
            id = ko.observable(specifiedId || 0),
            // Category Id
            categoryId = ko.observable(specifiedCategoryId || 0),
            // Category Name
            categoryName = ko.observable(specifiedCategoryName || ""),
            // True if Selected
            isSelected = ko.observable(specifiedIsSelected || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ProductCategoryItemId: id(),
                    CategoryId: categoryId(),
                    ItemId: itemId(),
                    IsSelected: isSelected()
                };
            };

        return {
            id: id,
            categoryId: categoryId,
            categoryName: categoryName,
            isSelected: isSelected,
            itemId: itemId,
            convertToServerData: convertToServerData
        };
    },

    // Item Image Entity
    ItemImage = function (specifiedId, specifiedImage, specifiedItemId) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // image
            image = ko.observable(specifiedImage || undefined),
            // file source
            fileSource = ko.observable(),
            // file name
            fileName = ko.observable(),
            // Item Id
            itemId = ko.observable(specifiedItemId || undefined),
            // On Select File
            onSelectImage = function (file, data) {
                image(data);
                fileSource(data);
                fileName(file.name);
            },
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
                image: image
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function () {
                // Reset State to Un-Modified
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    ProductImageId: id(),
                    ItemId: itemId(),
                    FileSource: fileSource(),
                    FileName: fileName()
                };
            };

        return {
            id: id,
            itemId: itemId,
            image: image,
            fileSource: fileSource,
            fileName: fileName,
            onSelectImage: onSelectImage,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Product Market Brief Question Entity
    ProductMarketBriefQuestion = function (specifiedId, specifiedQuestionDetail, specifiedIsMultipleSelection, specifiedSortOrder, specifiedItemId, callbacks) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Question Detail
            questionDetail = ko.observable(specifiedQuestionDetail || undefined).extend({ required: true }),
            // is Multiple Selection
            isMultipleSelection = ko.observable(specifiedIsMultipleSelection || false),
            // Sort Order
            sortOrder = ko.observable(specifiedSortOrder || undefined),
            // Item Id
            itemId = ko.observable(specifiedItemId || 0),
            // Product Market Brief Answers
            productMarketBriefAnswers = ko.observableArray([]),
            // Add productMarketBriefAnswer
            addProductMarketBriefAnswer = function () {
                productMarketBriefAnswers.push(ProductMarketBriefAnswer.Create({ MarketBriefQuestionId: id() }, callbacks));
            },
            // On Delete Product Market Brief Answer
            onDeleteProductMarketBriefAnswer = function (productMarketBriefAnswer) {
                if (callbacks && callbacks.onDeleteProductMarketBriefAnswer && typeof callbacks.onDeleteProductMarketBriefAnswer === "function") {
                    callbacks.onDeleteProductMarketBriefAnswer(function () {
                        removeProductMarketBriefAnswer(productMarketBriefAnswer);
                    });
                }
            },
            // Remove productMarketBriefAnswer
            removeProductMarketBriefAnswer = function (productMarketBriefAnswer) {
                productMarketBriefAnswers.remove(productMarketBriefAnswer);
            },
            // Errors
            errors = ko.validation.group({
                questionDetail: questionDetail
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && productMarketBriefAnswers.filter(function (productMarketBriefAnswer) {
                    return !productMarketBriefAnswer.isValid();
                }).length === 0;
            }),
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                isMultipleSelection: isMultipleSelection,
                questionDetail: questionDetail,
                sortOrder: sortOrder,
                productMarketBriefAnswers: productMarketBriefAnswers
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || productMarketBriefAnswers.find(function (productMarketBriefAnswer) {
                    return productMarketBriefAnswer.hasChanges();
                }) != null;
            }),
            // Reset
            reset = function () {
                // Reset productMarketBriefAnswers State to Un-Modified
                productMarketBriefAnswers.each(function (productMarketBriefAnswer) {
                    return productMarketBriefAnswer.reset();
                });
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function () {
                return {
                    MarketBriefQuestionId: id(),
                    QuestionDetail: questionDetail(),
                    SortOrder: sortOrder(),
                    IsMultipleSelection: isMultipleSelection(),
                    ItemId: itemId(),
                    ProductMarketBriefAnswers: productMarketBriefAnswers.map(function (answer) {
                        var answerServer = answer.convertToServerData();
                        answerServer.MarketBriefQuestionId = id();
                        return answerServer;
                    })
                };
            };

        return {
            id: id,
            isMultipleSelection: isMultipleSelection,
            questionDetail: questionDetail,
            sortOrder: sortOrder,
            itemId: itemId,
            productMarketBriefAnswers: productMarketBriefAnswers,
            addProductMarketBriefAnswer: addProductMarketBriefAnswer,
            removeProductMarketBriefAnswer: removeProductMarketBriefAnswer,
            onDeleteProductMarketBriefAnswer: onDeleteProductMarketBriefAnswer,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Product Market Brief Answer Entity
    ProductMarketBriefAnswer = function (specifiedId, specifiedMarketBriefQuestionId, specifiedAnswerDetail) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // Answer Detail
            detail = ko.observable(specifiedAnswerDetail || undefined),
            // Product Id
            marketingBriefQuestionId = ko.observable(specifiedMarketBriefQuestionId || 0),
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
                detail: detail
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
                    MarketBriefAnswerId: id(),
                    MarketBriefQuestionId: marketingBriefQuestionId(),
                    AnswerDetail: detail()
                };
            };

        return {
            id: id,
            marketingBriefQuestionId: marketingBriefQuestionId,
            detail: detail,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Section Ink Coverage Entity
    SectionInkCoverage = function (specifiedId, specifiedSectionId, specifiedInkOrder, specifiedInkId, specifiedCoverageGroupId, specifiedSide) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId),
            // section Id
            sectionId = ko.observable(specifiedSectionId),
            // ink Order
            inkOrder = ko.observable(specifiedInkOrder),
            //Ink Id
            inkId = ko.observable(specifiedInkId),
            // Coverage Group Id
            coverageGroupId = ko.observable(specifiedCoverageGroupId),
            //Side
            side = ko.observable(specifiedSide),
            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0;
                // ReSharper disable InconsistentNaming
            }),
            dirtyFlag = new ko.dirtyFlag({
                // ReSharper restore InconsistentNaming
                id: id,
                sectionId: sectionId,
                inkOrder: inkOrder,
                inkId: inkId,
                coverageGroupId: coverageGroupId,
                side: side
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
                    Id: id(),
                    SectionId: sectionId(),
                    InkOrder: inkOrder(),
                    InkId: inkId(),
                    CoverageGroupId: coverageGroupId(),
                    Side: side()
                };
            };

        return {
            id: id,
            sectionId: sectionId,
            inkOrder: inkOrder,
            inkId: inkId,
            coverageGroupId: coverageGroupId,
            side: side,
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
    };

    // Item Video Factory
    ItemVideo.Create = function (source) {
        return new ItemVideo(source.VideoId, source.VideoLink, source.Caption, source.ItemId);
    };

    // Item Related Item Factory
    ItemRelatedItem.Create = function (source) {
        return new ItemRelatedItem(source.Id, source.RelatedItemId, source.RelatedItemName, source.RelatedItemCode, source.ItemId);
    };

    // Template Page Factory
    TemplatePage.Create = function (source) {
        return new TemplatePage(source.ProductPageId, source.Width, source.Height, source.PageName, source.PageNo, source.Orientation, source.ProductId);
    };

    // Template Factory
    Template.Create = function (source) {
        var template = new Template(source.ProductId, source.PdfTemplateWidth, source.PdfTemplateHeight, source.IsCreatedManual, source.IsSpotTemplate,
        source.FileOriginalSource);

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
    };

    // Item Addon Cost Centre Factory
    ItemAddonCostCentre.Create = function (source, callbacks) {
        return new ItemAddonCostCentre(source.ProductAddOnId, source.IsMandatory, source.ItemStockOptionId, source.CostCentreId, source.CostCentreName,
            source.CostCentreTypeName, callbacks);
    };

    // Item Stock Option Factory
    ItemStockOption.Create = function (source, callbacks, parent) {
        var itemStockOption = new ItemStockOption(source.ItemStockOptionId, source.StockLabel, source.StockId, source.StockItemName, source.StockItemDescription,
            source.ImageUrlSource, source.OptionSequence, source.ItemId, callbacks, source.inStock, source.Allocated, parent);

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
    };

    // Item State Tax Factory
    ItemStateTax.Create = function (source, constructorParams) {
        var itemStateTax = new ItemStateTax(source.ItemStateTaxId, source.CountryId, source.StateId, source.TaxRate, source.CountryName,
            source.StateName, source.ItemId);

        if (constructorParams) {
            itemStateTax.countries(constructorParams.countries || []);
            itemStateTax.states(constructorParams.states || []);
        }

        return itemStateTax;
    };

    // Item Price Matrix Factory
    ItemPriceMatrix.Create = function (source, callbacks) {
        return new ItemPriceMatrix(source.PriceMatrixId, source.Quantity, source.QtyRangeFrom, source.QtyRangeTo, source.PricePaperType1, source.PricePaperType2,
            source.PricePaperType3, source.PriceStockType4, source.PriceStockType5, source.PriceStockType6, source.PriceStockType7, source.PriceStockType8,
            source.PriceStockType9, source.PriceStockType10, source.PriceStockType11, source.FlagId, source.SupplierId, source.SupplierSequence, source.ItemId,
            callbacks);
    };

    // Item Product Detail Factory
    ItemProductDetail.Create = function (source) {
        var itemProductDetail = new ItemProductDetail(source.ItemDetailId, source.IsInternalActivity, source.IsAutoCreateSupplierPO, source.IsQtyLimit, source.QtyLimit,
            source.DeliveryTimeSupplier1, source.DeliveryTimeSupplier2, source.IsPrintItem, source.IsAllowMarketBriefAttachment, source.MarketBriefSuccessMessage,
            source.ItemId);

        return itemProductDetail;
    };

    // Product Category Item Factory
    ProductCategoryItem.Create = function (source) {
        var productCategoryItem = new ProductCategoryItem(source.ProductCategoryItemId, source.CategoryId, source.IsSelected, source.CategoryName, source.ItemId);

        return productCategoryItem;
    };

    // Item Section Factory
    ItemSection.Create = function (source, parent) {
        var itemSection = new ItemSection(source.ItemSectionId, source.SectionNo, source.SectionName, source.SectionSizeId, source.ItemSizeId,
            source.IsSectionSizeCustom, source.SectionSizeHeight, source.SectionSizeWidth, source.IsItemSizeCustom, source.ItemSizeHeight,
            source.ItemSizeWidth, source.PressId, source.StockItemId1, source.StockItem1Name, source.PressName, source.ItemId, source.IsDoubleSided, source.IsWorknTurn,
            source.PrintViewLayoutPortrait, source.PrintViewLayoutLandScape, source.IsPortrait,
            source.PressIdSide2, source.ImpressionCoverageSide1, source.ImpressionCoverageSide2, source.PrintingType,
            source.PressSide1ColourHeads, source.PressSide1IsSpotColor, source.PressSide2ColourHeads, source.PressSide2IsSpotColor, source.StockItemPackageQty,
            source.ItemGutterHorizontal, parent);

        // Map Section Ink Coverage if Any
        if (source.SectionInkCoverages && source.SectionInkCoverages.length > 0) {
            var sectioninkcoverages = [];

            _.each(source.SectionInkCoverages, function (sectionink) {
                sectioninkcoverages.push(SectionInkCoverage.Create(sectionink));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(itemSection.sectionInkCoverageList(), sectioninkcoverages);
            itemSection.sectionInkCoverageList.valueHasMutated();
        }

        // Return item with dirty state if New
        if (!itemSection.id()) {
            return itemSection;
        }

        // Reset State to Un-Modified
        itemSection.reset();

        return itemSection;
    };

    // Item Image Factory
    ItemImage.Create = function (source) {
        return new ItemImage(source.ProductImageId, source.ImageUrlSource, source.ItemId);
    };

    // Item Factory
    Item.Create = function (source, callbacks, constructorParams) {
        var item = new Item(source.ItemId, source.ItemName, source.ItemCode, source.ProductName, source.ProductCode, source.ThumbnailImageSource, source.MinPrice,
            source.IsArchived, source.IsPublished, source.ProductCategoryName, source.IsEnabled, source.IsFeatured, source.ProductType, source.SortOrder,
            source.IsStockControl, source.IsVdpProduct, source.XeroAccessCode, source.WebDescription, source.ProductSpecification, source.TipsAndHints,
            source.MetaTitle, source.MetaDescription, source.MetaKeywords, source.JobDescriptionTitle1, source.JobDescription1, source.JobDescriptionTitle2,
            source.JobDescription2, source.JobDescriptionTitle3, source.JobDescription3, source.JobDescriptionTitle4, source.JobDescription4,
            source.JobDescriptionTitle5, source.JobDescription5, source.JobDescriptionTitle6, source.JobDescription6, source.JobDescriptionTitle7,
            source.JobDescription7, source.JobDescriptionTitle8, source.JobDescription8, source.JobDescriptionTitle9, source.JobDescription9,
            source.JobDescriptionTitle10, source.JobDescription10, source.GridImageSource, source.ImagePathImageSource, source.File1BytesSource, source.File2BytesSource,
            source.File3BytesSource, source.File4BytesSource, source.File5BytesSource, source.FlagId, source.IsQtyRanged, source.PackagingWeight, source.DefaultItemTax,
            source.SupplierId, source.SupplierId2, source.EstimateProductionTime, source.ItemProductDetail, source.IsTemplateDesignMode, source.DesignerCategoryId,
            source.Scalar, source.ZoomFactor, source.IsCmyk, source.TemplateType, source.ProductDisplayOptions, source.IsRealStateProduct, source.IsUploadImage,
            source.IsDigitalDownload, source.PrintCropMarks, source.DrawWaterMarkTxt, source.OrganisationId, source.CompanyId, source.IsAddCropMarks,
            source.DrawBleedArea, source.AllowPdfDownload, source.IsMultipagePdf, source.AllowImageDownload, source.ItemLength, source.ItemWidth, source.ItemHeight,
            source.ItemWeight, source.TemplateId, source.SmartFormId, callbacks, constructorParams);

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
            // If Designer Product then set its Created Manual to undefined
            if (item.templateTypeUi() === "1") {
                item.template().isCreatedManualUi(true);
            } else if (item.templateTypeUi() === "2") {
                item.template().isCreatedManualUi(false);
            } else if (item.templateTypeUi() === "3") {
                item.template().isCreatedManualUi(undefined);
            }
        }

        // If Not a print product
        if (item.isFinishedGoodsUi() !== '1') {
            item.template().isCreatedManualUi(undefined);
        }

        // Map Item Stock Options if any
        if (source.ItemStockOptions && source.ItemStockOptions.length > 0) {
            _.each(source.ItemStockOptions, function (itemStockOption) {
                var stockOption = ItemStockOption.Create(itemStockOption, callbacks, item.self);
                item.itemStockOptions.push(stockOption);
                item.wireupItemStockOptionTabEvents(stockOption);
            });
        }
        else {
            // Add one atleast if Newly Created Product
            if (!item.id()) {
                item.addItemStockOption();
            }
        }

        // Map Item Related Items if any
        if (source.ItemPriceMatrices && source.ItemPriceMatrices.length > 0) {
            var itemPriceMatrices = [];

            _.each(source.ItemPriceMatrices, function (itemPriceMatrix) {
                itemPriceMatrices.push(ItemPriceMatrix.Create(itemPriceMatrix, { onPriceMatrixQuantityChange: item.updateQuantitiesForSupplier }));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemPriceMatrices(), itemPriceMatrices);
            item.itemPriceMatrices.valueHasMutated();
        }

        // Map Item State Taxes if any
        if (source.ItemStateTaxes && source.ItemStateTaxes.length > 0) {
            var itemStateTaxes = [];

            _.each(source.ItemStateTaxes, function (itemStateTax) {
                itemStateTaxes.push(ItemStateTax.Create(itemStateTax, constructorParams));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemStateTaxes(), itemStateTaxes);
            item.itemStateTaxes.valueHasMutated();
        }

        // Map Product Category Items if any
        if (source.ProductCategoryItems && source.ProductCategoryItems.length > 0) {
            var productCategoryItems = [];

            _.each(source.ProductCategoryItems, function (productCategoryItem) {
                productCategoryItem.IsSelected = true;
                productCategoryItems.push(ProductCategoryItem.Create(productCategoryItem));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.productCategoryItems(), productCategoryItems);
            item.productCategoryItems.valueHasMutated();
        }

        // Map Item Sections if any
        if (source.ItemSections && source.ItemSections.length > 0) {
            var itemSections = [];

            _.each(source.ItemSections, function (itemSection) {
                itemSections.push(ItemSection.Create(itemSection, item.self));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemSections(), itemSections);
            item.itemSections.valueHasMutated();
        }

        // Map Item Images if any
        if (source.ItemImages && source.ItemImages.length > 0) {
            var itemImages = [];

            _.each(source.ItemImages, function (itemImage) {
                itemImages.push(ItemImage.Create(itemImage));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.itemImages(), itemImages);
            item.itemImages.valueHasMutated();
        }

        // Map Product Market Brief Question if any
        if (source.ProductMarketBriefQuestions && source.ProductMarketBriefQuestions.length > 0) {
            var productMarketBriefQuestions = [];

            _.each(source.ProductMarketBriefQuestions, function (productMarketBriefQuestion) {
                productMarketBriefQuestions.push(ProductMarketBriefQuestion.Create(productMarketBriefQuestion, callbacks));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(item.productMarketBriefQuestions(), productMarketBriefQuestions);
            item.productMarketBriefQuestions.valueHasMutated();
        }

        // Return item with dirty state if New
        if (!item.id()) {
            return item;
        }

        // Reset State to Un-Modified
        item.reset();

        return item;
    };

    // Stock Item Factory
    StockItem.Create = function (source) {
        return new StockItem(source.StockItemId, source.ItemName, source.CategoryName, source.StockLocation, source.ItemWeight, source.ItemDescription,
        source.PackageQty, source.Allocated, source.InStock);
    };

    // Cost Centre Factory
    CostCentre.Create = function (source) {
        return new CostCentre(source.CostCentreId, source.Name, source.TypeName);
    };

    // Country Factory
    Country.Create = function (source) {
        return new Country(source.CountryId, source.CountryName, source.CountryCode);
    };

    // State Factory
    State.Create = function (source) {
        return new State(source.StateId, source.StateName, source.StateCode, source.CountryId);
    };

    // Section Flag Factory
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagColor);
    };

    // Company Factory
    Company.Create = function (source) {
        return new Company(source.SupplierId, source.Name);
    };

    // Product Category Factory
    ProductCategory.Create = function (source) {
        var productCategory = new ProductCategory(source.ProductCategoryId, source.CategoryName, source.IsSelected, source.ParentCategoryId, source.IsArchived);

        return productCategory;
    };

    // Category Region Factory
    CategoryRegion.Create = function (source) {
        return new CategoryRegion(source.RegionId, source.RegionName);
    };

    // Category Type Factory
    CategoryType.Create = function (source) {
        return new CategoryType(source.TypeId, source.TypeName);
    };

    // Product Category For Template Factory
    ProductCategoryForTemplate.Create = function (source) {
        var productCategory = new ProductCategoryForTemplate(source.ProductCategoryId, source.CategoryName, source.RegionId, source.CategoryTypeId,
        source.ZoomFactor, source.ScalarFactor);

        return productCategory;
    };

    // Machine Factory
    Machine.Create = function (source) {
        return new Machine(source.MachineId, source.MachineName, source.DefaultPageId, source.Maximumsheetheight, source.Maximumsheetweight,
        source.Maximumsheetwidth, source.Minimumsheetheight, source.Minimumsheetweight, source.MachineCatId, source.ColourHeads, source.IsSpotColor);
    };

    // Paper Size Factory
    PaperSize.Create = function (source) {
        return new PaperSize(source.PaperSizeId, source.Name, source.Height, source.Width);
    };

    // Product Market Brief Question Factory
    ProductMarketBriefQuestion.Create = function (source, callbacks) {
        var marketBriefQuestion =
            new ProductMarketBriefQuestion(source.MarketBriefQuestionId, source.QuestionDetail, source.IsMultipleSelection, source.SortOrder, source.ItemId,
            callbacks);

        // Map Product Market Brief Answer if any
        if (source.ProductMarketBriefAnswers && source.ProductMarketBriefAnswers.length > 0) {
            var productMarketBriefAnswers = [];

            _.each(source.ProductMarketBriefAnswers, function (productMarketBriefAnswer) {
                productMarketBriefAnswers.push(ProductMarketBriefAnswer.Create(productMarketBriefAnswer, callbacks));
            });

            // Push to Original Item
            ko.utils.arrayPushAll(marketBriefQuestion.productMarketBriefAnswers(), productMarketBriefAnswers);
            marketBriefQuestion.productMarketBriefAnswers.valueHasMutated();
        }

        return marketBriefQuestion;
    };

    // Product Market Brief Answer Factory
    ProductMarketBriefAnswer.Create = function (source, callbacks) {
        return new ProductMarketBriefAnswer(source.MarketBriefAnswerId, source.MarketBriefQuestionId, source.AnswerDetail, callbacks);
    };

    // Section Ink Coverage Factory
    SectionInkCoverage.Create = function (source) {
        return new SectionInkCoverage(source.Id, source.SectionId, source.InkOrder, source.InkId, source.CoverageGroupId, source.Side);
    };

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
        SectionFlag: SectionFlag,
        // Company Constructor
        Company: Company,
        // Item Product Detail Constructor
        ItemProductDetail: ItemProductDetail,
        // Product Category Constructor
        ProductCategory: ProductCategory,
        // Product Category Item Constructor
        ProductCategoryItem: ProductCategoryItem,
        // Product Category For Template Constructor
        ProductCategoryForTemplate: ProductCategoryForTemplate,
        // Category Region Constructor
        CategoryRegion: CategoryRegion,
        // Category Type Constructor
        CategoryType: CategoryType,
        // Machine Constructor
        Machine: Machine,
        // Paper Size Constructor
        PaperSize: PaperSize,
        // Product Market Brief Question Constructor
        ProductMarketBriefQuestion: ProductMarketBriefQuestion,
        // Product Market Brief Answer Constructor
        ProductMarketBriefAnswer: ProductMarketBriefAnswer,
        // Section Ink Coverage Constructor
        SectionInkCoverage: SectionInkCoverage
    };
});