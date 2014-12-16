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
        specifiedJobDescription9, specifiedJobDescriptionTitle10, specifiedJobDescription10, specifiedGridImage, specifiedImagePath, specifiedFile1, callbacks) {
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
            isTemplateTabsVisible = ko.computed(function() {
                return isFinishedGoodsUi() === '1';
            }),
            // Item Vdp Prices
            itemVdpPrices = ko.observableArray([]),
            // Item Videos
            itemVideos = ko.observableArray([]),
            // Item Related Items
            itemRelatedItems = ko.observableArray([]),
            // Template 
            template = ko.observable(Template.Create({})),
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
            // Errors
            errors = ko.validation.group({
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 && itemVdpPrices.filter(function (itemVdpPrice) {
                    return !itemVdpPrice.isValid();
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
                itemVdpPrices: itemVdpPrices,
                itemVideos: itemVideos,
                itemRelatedItems: itemRelatedItems,
                template: template
            }),
            // Item Vdp Prices has changes
            itemVdpPriceListHasChanges = ko.computed(function () {
                return itemVdpPrices.find(function (itemVdpPrice) {
                    return itemVdpPrice.hasChanges();
                }) != null;
            }),
            // Item Videos Has Changes
            itemVideosHasChanges = ko.computed(function() {
                return itemVideos.find(function(itemVideo) {
                    return itemVideo.hasChanges();
                }) != null;
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty() || itemVdpPriceListHasChanges() || itemVideosHasChanges() || template().hasChanges();
            }),
            // Reset
            reset = function () {
                itemVdpPrices.each(function (itemVdpPrice) {
                    return itemVdpPrice.reset();
                });
                itemVideos.each(function (itemVideo) {
                    return itemVideo.reset();
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
                    IsFinishedGoods: isFinishedGoodsUi() === '3' ? 0 : parseInt(isFinishedGoodsUi()),
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
                    ItemVdpPrices: itemVdpPrices.map(function (itemVdpPrice) {
                        return itemVdpPrice.convertToServerData();
                    }),
                    ItemVideos: itemVideos.map(function (itemVideo) {
                        return itemVideo.convertToServerData();
                    }),
                    ItemRelatedItems: itemRelatedItems.map(function (itemRelatedItem) {
                        return itemRelatedItem.convertToServerData();
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
            moveTemplatePageUp = function(templatePage) {
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
                return errors().length === 0 || templatePages.filter(function(templatePage) {
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
                return dirtyFlag.isDirty() || templatePages.find(function(templatePage) {
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

            _.each(source.TemplatePages, function(templatePage) {
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

    // Item Factory
    Item.Create = function (source, callbacks) {
        var item = new Item(source.ItemId, source.ItemName, source.ItemCode, source.ProductName, source.ProductCode, source.ThumbnailImageSource, source.MinPrice,
            source.IsArchived, source.IsPublished, source.ProductCategoryName, source.IsEnabled, source.IsFeatured, source.IsFinishedGoods, source.SortOrder,
            source.IsStockControl, source.IsVdpProduct, source.XeroAccessCode, source.WebDescription, source.ProductSpecification, source.TipsAndHints,
            source.MetaTitle, source.MetaDescription, source.MetaKeywords, source.JobDescriptionTitle1, source.JobDescription1, source.JobDescriptionTitle2,
            source.JobDescription2, source.JobDescriptionTitle3, source.JobDescription3, source.JobDescriptionTitle4, source.JobDescription4,
            source.JobDescriptionTitle5, source.JobDescription5, source.JobDescriptionTitle6, source.JobDescription6, source.JobDescriptionTitle7,
            source.JobDescription7, source.JobDescriptionTitle8, source.JobDescription8, source.JobDescriptionTitle9, source.JobDescription9,
            source.JobDescriptionTitle10, source.JobDescription10, source.GridImageSource, source.ImagePathImageSource, source.File1BytesSource, callbacks);

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

        // Reset State to Un-Modified
        item.reset();

        return item;
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
        TemplatePage: TemplatePage
    };
});