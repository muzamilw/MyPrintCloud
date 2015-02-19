/*
    Module with the model for the Order
*/
define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    // Estimate Entity
    // ReSharper disable InconsistentNaming
    Estimate = function (specifiedId, specifiedCode, specifiedName, specifiedCompanyId, specifiedCompanyName, specifiedNumberOfItems, specifiedCreationDate,
        specifiedFlagColor, specifiedSectionFlagId, specifiedOrderCode, specifiedIsEstimate, specifiedContactId, specifiedAddressId, specifiedIsDirectSale,
        specifiedIsOfficialOrder, specifiedIsCreditApproved, specifiedOrderDate, specifiedStartDeliveryDate, specifiedFinishDeliveryDate,
        specifiedHeadNotes, specifiedArtworkByDate, specifiedDataByDate, specifiedPaperByDate, specifiedTargetBindDate, specifiedXeroAccessCode,
        specifiedTargetPrintDate, specifiedOrderCreationDateTime, specifiedOrderManagerId, specifiedSalesPersonId, specifiedSourceId, 
        specifiedCreditLimitForJob, specifiedCreditLimitSetBy, specifiedCreditLimitSetOnDateTime, specifiedIsJobAllowedWOCreditCheck,
        specifiedAllowJobWOCreditCheckSetOnDateTime, specifiedAllowJobWOCreditCheckSetBy, specifiedCustomerPo, specifiedOfficialOrderSetBy,
        specifiedOfficialOrderSetOnDateTime, specifiedFootNotes) {
        // ReSharper restore InconsistentNaming
        var // Unique key
            id = ko.observable(specifiedId || 0),
            // Name
            name = ko.observable(specifiedName || undefined).extend({ required: true }),
            // Code
            code = ko.observable(specifiedCode || undefined),
            // Company Id
            companyId = ko.observable(specifiedCompanyId || undefined).extend({ required: true }),
            // Company Name
            companyName = ko.observable(specifiedCompanyName || undefined),
            // Number Of items
            numberOfItems = ko.observable(specifiedNumberOfItems || 0),
            // Number of Items UI
            noOfItemsUi = ko.computed(function() {
                return "( " + numberOfItems() + " ) Items";
            }),
            // Creation Date
            creationDate = ko.observable(specifiedCreationDate || undefined),
            // Flag Color
            flagColor = ko.observable(specifiedFlagColor || undefined),
            // Flag Id
            sectionFlagId = ko.observable(specifiedSectionFlagId || undefined),
            // Order Code
            orderCode = ko.observable(specifiedOrderCode || undefined),
            // Is Estimate
            isEstimate = ko.observable(specifiedIsEstimate || false),
            // Contact Id
            contactId = ko.observable(specifiedContactId || undefined),
            // Address Id
            addressId = ko.observable(specifiedAddressId || undefined),
            // Is Direct Sale
            isDirectSale = ko.observable(specifiedIsDirectSale || true),
            // Is Direct Sale Ui
            isDirectSaleUi = ko.computed(function() {
                return isDirectSale() ? "Direct Order" : "Online Order";
            }),
            // Is Official Order
            isOfficialOrder = ko.observable(specifiedIsOfficialOrder || false),
            // Is Credit Approved
            isCreditApproved = ko.observable(specifiedIsCreditApproved || false),
            // Order Date
            orderDate = ko.observable(specifiedOrderDate || undefined),
            // Start Delivery Date
            startDeliveryDate = ko.observable(specifiedStartDeliveryDate || undefined),
            // Finish Delivery Date
            finishDeliveryDate = ko.observable(specifiedFinishDeliveryDate || undefined),
            // Head Notes
            headNotes = ko.observable(specifiedHeadNotes || undefined),
            // Artwork By Date
            artworkByDate = ko.observable(specifiedArtworkByDate || undefined),
            // Data By Date
            dataByDate = ko.observable(specifiedDataByDate || undefined),
            // Paper By Date
            paperByDate = ko.observable(specifiedPaperByDate || undefined),
            // Target Bind Date
            targetBindDate = ko.observable(specifiedTargetBindDate || undefined),
            // Xero Access Code
            xeroAccessCode = ko.observable(specifiedXeroAccessCode || undefined),
            // Target Print Date
            targetPrintDate = ko.observable(specifiedTargetPrintDate || undefined),
            // Order Creation Date Time
            orderCreationDateTime = ko.observable(specifiedOrderCreationDateTime || undefined),
            // Order Manager Id
            orderManagerId = ko.observable(specifiedOrderManagerId || undefined),
            // Sales Person Id
            salesPersonId = ko.observable(specifiedSalesPersonId || undefined),
            // Source Id
            sourceId = ko.observable(specifiedSourceId || undefined),
            // Credit Limit For Job
            creditLimitForJob = ko.observable(specifiedCreditLimitForJob || undefined),
            // Credit Limit Set By
            creditLimitSetBy = ko.observable(specifiedCreditLimitSetBy || undefined),
            // Credit Limit Set on Date Time
            creditLimitSetOnDateTime = ko.observable(specifiedCreditLimitSetOnDateTime || undefined),
            // Is JobAllowedWOCreditCheck
            isJobAllowedWoCreditCheck = ko.observable(specifiedIsJobAllowedWOCreditCheck || undefined),
            // Allow Job WOCreditCheckSetOnDateTime
            allowJobWoCreditCheckSetOnDateTime = ko.observable(specifiedAllowJobWOCreditCheckSetOnDateTime || undefined),
            // Allow JobWOCreditCheckSetBy
            allowJobWoCreditCheckSetBy = ko.observable(specifiedAllowJobWOCreditCheckSetBy || undefined),
            // Customer Po
            customerPo = ko.observable(specifiedCustomerPo || undefined),
            // Official Order Set By
            officialOrderSetBy = ko.observable(specifiedOfficialOrderSetBy || undefined),
            // Official Order Set on Date Time
            officialOrderSetOnDateTime = ko.observable(specifiedOfficialOrderSetOnDateTime || undefined),
            // Foot Notes
            footNotes = ko.observable(specifiedFootNotes || undefined),
            // Items
            items = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                name: name,
                companyId: companyId
            }),
            // Is Valid
            isValid = ko.computed(function() {
                return errors().length === 0;
            }),
            // Show All Error Messages
            showAllErrors = function() {
                // Show Item Errors
                errors.showAllMessages();
            },
            // Set Validation Summary
            setValidationSummary = function(validationSummaryList) {
                validationSummaryList.removeAll();
            },
            // True if the order has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                code: code,
                companyId: companyId,
                contactId: contactId,
                addressId: addressId,
                isDirectSale: isDirectSale,
                isOfficialOrder: isOfficialOrder,
                isCreditApproved: isCreditApproved,
                orderDate: orderDate,
                startDeliveryDate: startDeliveryDate,
                finishDeliveryDate: finishDeliveryDate,
                headNotes: headNotes,
                artworkByDate: artworkByDate,
                dataByDate: dataByDate,
                paperByDate: paperByDate,
                targetBindDate: targetBindDate,
                xeroAccessCode: xeroAccessCode,
                targetPrintDate: targetPrintDate,
                orderCreationDateTime: orderCreationDateTime,
                orderManagerId: orderManagerId,
                salesPersonId: salesPersonId,
                sourceId: sourceId,
                creditLimitForJob: creditLimitForJob,
                creditLimitSetBy: creditLimitSetBy,
                creditLimitSetOnDateTime: creditLimitSetOnDateTime,
                isJobAllowedWoCreditCheck: isJobAllowedWoCreditCheck,
                allowJobWoCreditCheckSetOnDateTime: allowJobWoCreditCheckSetOnDateTime,
                allowJobWoCreditCheckSetBy: allowJobWoCreditCheckSetBy,
                customerPo: customerPo,
                officialOrderSetBy: officialOrderSetBy,
                officialOrderSetOnDateTime: officialOrderSetOnDateTime,
                footNotes: footNotes,
                sectionFlagId: sectionFlagId
            }),
            // Has Changes
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),
            // Reset
            reset = function() {
                dirtyFlag.reset();
            },
            // Convert To Server Data
            convertToServerData = function() {
                return {
                    EstimateId: id(),
                    EstimateCode: code(),
                    EstimateName: name(),
                    CompanyId: companyId(),
                    ContactId: contactId(),
                    AddressId: addressId(),
                    SectionFlagId: sectionFlagId(),
                    IsDirectSale: isDirectSale(),
                    IsOfficialOrder: isOfficialOrder(),
                    IsCreditApproved: isCreditApproved(),
                    OrderDate: orderDate(),
                    StartDeliveryDate: startDeliveryDate(),
                    FinishDeliveryDate: finishDeliveryDate(),
                    HeadNotes: headNotes(),
                    FootNotes: footNotes(),
                    ArtworkByDate: artworkByDate(),
                    DataByDate: dataByDate(),
                    PaperByDate: paperByDate(),
                    TargetBindDate: targetBindDate(),
                    XeroAccessCode: xeroAccessCode(),
                    TargetPrintDate: targetPrintDate(),
                    OrderCreationDateTime: orderCreationDateTime(),
                    OrderManagerId: orderManagerId(),
                    SalesPersonId: salesPersonId(),
                    SourceId: sourceId(),
                    CreditLimitForJob: creditLimitForJob(),
                    CreditLimitSetBy: creditLimitSetBy(),
                    CreditLimitSetOnDateTime: creditLimitSetOnDateTime(),
                    IsJobAllowedWoCreditCheck: isJobAllowedWoCreditCheck(),
                    AllowJobWoCreditCheckSetOnDateTime: allowJobWoCreditCheckSetOnDateTime(),
                    AllowJobWoCreditCheckSetBy: allowJobWoCreditCheckSetBy(),
                    CustomerPo: customerPo(),
                    OfficialOrderSetBy: officialOrderSetBy(),
                    OfficialOrderSetOnDateTime: officialOrderSetOnDateTime()
                };
            };

        return {
            id: id,
            name: name,
            code: code,
            noOfItemsUi: noOfItemsUi,
            creationDate: creationDate,
            flagColor: flagColor,
            orderCode: orderCode,
            isEstimate: isEstimate,
            companyId: companyId,
            companyName: companyName,
            contactId: contactId,
            addressId: addressId,
            sectionFlagId: sectionFlagId,
            isDirectSale: isDirectSale,
            isDirectSaleUi: isDirectSaleUi,
            isOfficialOrder: isOfficialOrder,
            isCreditApproved: isCreditApproved,
            orderDate: orderDate,
            startDeliveryDate: startDeliveryDate,
            finishDeliveryDate: finishDeliveryDate,
            headNotes: headNotes,
            footNotes: footNotes,
            artworkByDate: artworkByDate,
            dataByDate: dataByDate,
            paperByDate: paperByDate,
            targetBindDate: targetBindDate,
            xeroAccessCode: xeroAccessCode,
            targetPrintDate: targetPrintDate,
            orderCreationDateTime: orderCreationDateTime,
            orderManagerId: orderManagerId,
            salesPersonId: salesPersonId,
            sourceId: sourceId,
            creditLimitForJob: creditLimitForJob,
            creditLimitSetBy: creditLimitSetBy,
            creditLimitSetOnDateTime: creditLimitSetOnDateTime,
            isJobAllowedWoCreditCheck: isJobAllowedWoCreditCheck,
            allowJobWoCreditCheckSetOnDateTime: allowJobWoCreditCheckSetOnDateTime,
            allowJobWoCreditCheckSetBy: allowJobWoCreditCheckSetBy,
            customerPo: customerPo,
            officialOrderSetBy: officialOrderSetBy,
            officialOrderSetOnDateTime: officialOrderSetOnDateTime,
            items: items,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            setValidationSummary: setValidationSummary,
            convertToServerData: convertToServerData
        };
    },

    // Item Entity
    Item = function (specifiedId, specifiedName, specifiedCode, specifiedProductName, specifiedProductCode, specifiedProductCategoryName,
        specifiedIsEnabled, specifiedIsFeatured, specifiedIsFinishedGoods, specifiedSortOrder, specifiedIsStockControl, specifiedIsVdpProduct,
        specifiedXeroAccessCode, specifiedWebDescription, specifiedProductSpecification, specifiedTipsAndHints, specifiedMetaTitle,
        specifiedMetaDescription, specifiedMetaKeywords, specifiedJobDescriptionTitle1, specifiedJobDescription1,
        specifiedJobDescriptionTitle2, specifiedJobDescription2, specifiedJobDescriptionTitle3, specifiedJobDescription3, specifiedJobDescriptionTitle4,
        specifiedJobDescription4, specifiedJobDescriptionTitle5, specifiedJobDescription5, specifiedJobDescriptionTitle6, specifiedJobDescription6,
        specifiedJobDescriptionTitle7, specifiedJobDescription7, specifiedJobDescriptionTitle8, specifiedJobDescription8, specifiedJobDescriptionTitle9,
        specifiedJobDescription9, specifiedJobDescriptionTitle10, specifiedJobDescription10, specifiedFlagId, specifiedIsQtyRanged, specifiedPackagingWeight,
        specifiedDefaultItemTax, specifiedSupplierId, specifiedSupplierId2, specifiedEstimateProductionTime, specifiedItemProductDetail,
        specifiedIsTemplateDesignMode, specifiedDesignerCategoryId, specifiedScalar, specifiedZoomFactor, specifiedIsCMYK, specifiedTemplateType,
        callbacks) {
        // ReSharper restore InconsistentNaming
        var // Unique key
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
            // file 
            file2 = ko.observable(specifiedFile2 || undefined),
            // file2 File Name
            file2FileName = ko.observable(),
            // file2 File Source
            file2FileSource = ko.observable(),
            // file 3
            file3 = ko.observable(specifiedFile3 || undefined),
            // file3 File Name
            file3FileName = ko.observable(),
            // file3 File Source
            file3FileSource = ko.observable(),
            // file 4
            file4 = ko.observable(specifiedFile4 || undefined),
            // file4 File Name
            file4FileName = ko.observable(),
            // file4 File Source
            file4FileSource = ko.observable(),
            // file 5
            file5 = ko.observable(specifiedFile5 || undefined),
            // file5 File Name
            file5FileName = ko.observable(),
            // file5 File Source
            file5FileSource = ko.observable(),
            // mini Price
            miniPrice = ko.observable(specifiedMinPrice || 0),
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
            packagingWeight = ko.observable(specifiedPackagingWeight || undefined),
            // Default Item Tax
            defaultItemTax = ko.observable(specifiedDefaultItemTax || undefined),
            // supplier id
            internalSupplierId = ko.observable(specifiedSupplierId || undefined),
            // supplier id 2
            internalSupplierId2 = ko.observable(specifiedSupplierId2 || undefined),
            // Estimate Production Time
            estimateProductionTime = ko.observable(specifiedEstimateProductionTime || undefined),
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
            scalar = ko.observable(specifiedScalar || undefined),
            // Zoom Factor
            zoomFactor = ko.observable(specifiedZoomFactor || undefined),
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
                            template().isCreatedManual(true);
                        }
                        else if (tempType === 2) {
                            template().isCreatedManual(false);
                        }
                        else {
                            template().isCreatedManual(undefined);
                        }
                    }
                }
            }),
            // Can Start Designer Empty
            canStartDesignerEmpty = ko.computed(function () {
                return templateType() === 3;
            }),
            // Item Product Detail
            itemProductDetail = ko.observable(ItemProductDetail.Create(specifiedItemProductDetail || { ItemId: id() })),
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
            // Available Product Category items
            availableProductCategoryItems = ko.computed(function () {
                if (productCategoryItems().length === 0) {
                    return "";
                }

                var categories = "";
                productCategoryItems.each(function (pci, index) {
                    var pcname = pci.categoryName();
                    if (index < productCategoryItems().length - 1) {
                        pcname = pcname + " || ";
                    }
                    categories += pcname;
                });

                return categories;
            }),
            // Item Sections
            itemSections = ko.observableArray([]),
            // Can Add Item Section
            canAddItemSection = ko.computed(function () {
                return itemProductDetail().isPrintItemUi() === '1' || itemSections().length < 5;
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
                        itemSections.push(ItemSection.Create({ ItemId: id() }));
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
            // Select Stock Item For Stock Option
            selectStockItemForStockOption = function (stockItem) {
                activeStockOption().selectStock(stockItem);
                activeStockOption(ItemStockOption.Create({}, callbacks));
            },
            // Choose Stock Item For Section
            chooseStockItemForSection = function () {
                if (callbacks && callbacks.onChooseStockItem && typeof callbacks.onChooseStockItem === "function") {
                    callbacks.onChooseStockItem(stockCategory.paper);
                }
            },
            // Active Item Section
            activeItemSection = ko.observable(ItemSection.Create({})),
            // Select Item Section
            selectItemSection = function (itemSection) {
                if (activeItemSection() !== itemSection) {
                    activeItemSection(itemSection);
                }

                // Set Stock Item Selection Callback
                selectStockItemCallback = selectStockItemForSection;
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
                            itemPriceMatrixItems.push(ItemPriceMatrix.Create(priceItem));
                        });
                        ko.utils.arrayPushAll(itemPriceMatrices(), itemPriceMatrixItems);
                        // Remove Existing ones
                        removeExistingPriceMatrices();
                        itemPriceMatrices.valueHasMutated();
                    }
                    else { // Add New
                        for (var i = 0; i < 15; i++) {
                            itemPriceMatrixItems.push(ItemPriceMatrix.Create({ ItemId: id(), FlagId: flagId() }));
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
                        itemPriceMatrixItems.push(ItemPriceMatrix.Create(itemPriceMatrix));
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
                                return productCategoryItem.categoryId() === productCategory.id && productCategoryItem.isSelected();
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
                itemSections.push(ItemSection.Create({ ItemId: id() }));
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
                        if (!file1FileSource()) {
                            file1(data);
                            file1FileSource(data);
                            file1FileName(file.name);
                        }
                        else if (!file2FileSource()) {
                            file2(data);
                            file2FileSource(data);
                            file2FileName(file.name);
                        }
                        else if (!file3FileSource()) {
                            file3(data);
                            file3FileSource(data);
                            file3FileName(file.name);
                        }
                        else if (!file4FileSource()) {
                            file4(data);
                            file4FileSource(data);
                            file4FileName(file.name);
                        }
                        else if (!file5FileSource()) {
                            file5(data);
                            file5FileSource(data);
                            file5FileName(file.name);
                        }
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
            },
            // Errors
            errors = ko.validation.group({
                productCode: productCode,
                productName: productName,
                internalFlagId: internalFlagId
            }),
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
                template().isValid();
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
                }
                // Show Item Section Errors
                var itemSectionInvalid = itemSections.find(function (itemSection) {
                    return !itemSection.isValid();
                });
                if (itemSectionInvalid) {
                    if (itemSectionInvalid.name.error || itemSectionInvalid.pressId.error || itemSectionInvalid.stockItemId.error) {
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
                        validationSummaryList.push({ name: errorName, element: nameElement });
                    }
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
                itemProductDetail: itemProductDetail,
                itemVdpPrices: itemVdpPrices,
                itemVideos: itemVideos,
                itemRelatedItems: itemRelatedItems,
                template: template,
                itemStockOptions: itemStockOptions,
                itemPriceMatrices: itemPriceMatrices,
                itemStateTaxes: itemStateTaxes,
                productCategoryItems: productCategoryItems,
                itemSections: itemSections
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
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || itemVdpPriceListHasChanges() || itemVideosHasChanges() || template().hasChanges() || itemStockOptionHasChanges() ||
                    itemPriceMatrixHasChanges() || itemStateTaxesHasChanges() || itemProductDetail().hasChanges() || itemSectionHasChanges();
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
                    IsQtyRanged: isQtyRanged() === 2 ? false : true,
                    PackagingWeight: packagingWeight(),
                    DefaultItemTax: defaultItemTax(),
                    SupplierId: supplierId(),
                    SupplierId2: supplierId2(),
                    EstimateProductionTime: estimateProductionTime(),
                    IsTemplateDesignMode: isTemplateDesignMode(),
                    IsCmyk: isCmyk() === 1,
                    Scalar: scalar(),
                    ZoomFactor: zoomFactor(),
                    DesignerCategoryId: designerCategoryId(),
                    TemplateType: templateType(),
                    TemplateTypeMode: templateTypeMode(),
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
            templateTypeMode: templateTypeMode,
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
            itemSections: itemSections,
            itemPriceMatricesForCurrentFlag: itemPriceMatricesForCurrentFlag,
            itemPriceMatricesForSupplierId1: itemPriceMatricesForSupplierId1,
            itemPriceMatricesForSupplierId2: itemPriceMatricesForSupplierId2,
            addItemStockOption: addItemStockOption,
            removeItemStockOption: removeItemStockOption,
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
            resetFiles: resetFiles,
            errors: errors,
            isValid: isValid,
            showAllErrors: showAllErrors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            itemVdpPriceListHasChanges: itemVdpPriceListHasChanges,
            reset: reset,
            convertToServerData: convertToServerData
        };
    },

    // Section Flag Entity        
    SectionFlag = function(specifiedId, specifiedFlagName, specifiedFlagColor) {
            return {
                id: specifiedId,
                name: specifiedFlagName,
                color: specifiedFlagColor
            };
    },
   
    // System User Entity        
    SystemUser = function (specifiedId, specifiedName) {
        return {
            id: specifiedId,
            name: specifiedName
        };
    },
    
    // Pipeline Source Entity        
    PipeLineSource = function (specifiedId, specifiedDescription) {
        return {
            id: specifiedId,
            name: specifiedDescription
        };
    },

    // Address Entity
    Address = function(specifiedId, specifiedName, specifiedAddress1, specifiedAddress2, specifiedTelephone1) {
        return {            
            id: specifiedId,
            name: specifiedName,
            address1: specifiedAddress1 || "",
            address2: specifiedAddress2 || "",
            telephone1: specifiedTelephone1 || ""
        };
    },
    
    // Company Contact Entity
    CompanyContact = function (specifiedId, specifiedName, specifiedEmail) {
        return {            
            id: specifiedId,
            name: specifiedName,
            email: specifiedEmail || ""
        };
    };
    
    // Estimate Factory
    Estimate.Create = function(source) {
        var estimate = new Estimate(source.EstimateId, source.EstimateCode, source.EstimateName, source.CompanyId, source.CompanyName, source.ItemsCount,
        source.CreationDate, source.FlagColor, source.SectionFlagId, source.OrderCode, source.IsEstimate, source.ContactId, source.AddressId, source.IsDirectSale,
        source.IsOfficialOrder, source.IsCreditApproved, source.OrderDate, source.StartDeliveryDate, source.FinishDeliveryDate, source.HeadNotes,
        source.ArtworkByDate, source.DataByDate, source.PaperByDate, source.TargetBindDate, source.XeroAccessCode, source.TargetPrintDate,
        source.OrderCreationDateTime, source.SalesAndOrderManagerId, source.SalesPersonId, source.SourceId, source.CreditLimitForJob, source.CreditLimitSetBy,
        source.CreditLimitSetOnDateTime, source.IsJobAllowedWOCreditCheck, source.AllowJobWOCreditCheckSetOnDateTime, source.AllowJobWOCreditCheckSetBy,
        source.CustomerPo, source.OfficialOrderSetBy, source.OfficialOrderSetOnDateTime);

        // Return item with dirty state if New
        if (!estimate.id()) {
            return estimate;
        }

        // Reset State to Un-Modified
        estimate.reset();

        return estimate;
    };
    
    // Section Flag Factory
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName, source.FlagColor);
    };

    // Address Factory
    Address.Create = function (source) {
        return new Address(source.AddressId, source.AddressName, source.Address1, source.Address2, source.Tel1);
    };
    
    // Company Contact Factory
    CompanyContact.Create = function (source) {
        return new CompanyContact(source.ContactId, source.Name, source.Email);
    };
    
    // System User Factory
    SystemUser.Create = function (source) {
        return new SystemUser(source.SystemUserId, source.UserName);
    };
    
    // Pipeline Source Factory
    PipeLineSource.Create = function (source) {
        return new PipeLineSource(source.SourceId, source.Description);
    };

    return {
        // Estimate Constructor
        Estimate: Estimate,
        // sectionflag constructor
        SectionFlag: SectionFlag,
        // Address Constructor
        Address: Address,
        // Company Contact Constructor
        CompanyContact: CompanyContact,
        // System User Constructor
        SystemUser: SystemUser,
        // PipeLine Source Constructor
        PipeLineSource: PipeLineSource
    };
});