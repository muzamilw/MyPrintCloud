/*
    Module with the view model for the Product.
*/
define("product/product.viewModel",
    ["jquery", "amplify", "ko", "product/product.dataservice", "product/product.model", "common/pagination", "common/confirmation.viewModel",
        "common/phraseLibrary.viewModel", "common/sharedNavigation.viewModel", "common/stockItem.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, phraseLibrary, shared, stockDialog) {
        var ist = window.ist || {};
        ist.product = {
            viewModel: (function () {
                var // the view 
                    view,
                    // #region Arrays
                    // Items
                    products = ko.observableArray([]),
                    // Items to Relate
                    productsToRelate = ko.observableArray([]),
                    // Error List
                    errorList = ko.observableArray([]),
                    // Press Items
                    pressItems = ko.observableArray([]),
                    // Cost Centres
                    costCentres = ko.observableArray([]),
                    // Section Flags
                    sectionFlags = ko.observableArray([]),
                    // Countries
                    countries = ko.observableArray([]),
                    // States
                    states = ko.observableArray([]),
                    // Suppliers
                    suppliers = ko.observableArray([]),
                    // Product Categories
                    productCategories = ko.observableArray([]),
                    // Parent Product Categories
                    parentProductCategories = ko.computed(function () {
                        if (!productCategories) {
                            return [];
                        }

                        return productCategories.filter(function (productCategory) {
                            return !productCategory.parentCategoryId;
                        });
                    }),
                    // Template Categories
                    templateCategories = ko.observableArray([]),
                    // Category Regions
                    categoryRegions = ko.observableArray([]),
                    // Category Types
                    categoryTypes = ko.observableArray([]),
                    // Smart Forms
                    smartForms = ko.observableArray([]),
                    // Paper Sizes
                    paperSizes = ko.observableArray([]),
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
                    // Inks
                    inks = ko.observableArray([]),
                    // Presses
                    presses = ko.observableArray([]),
                    itemPlan = ko.observable(),
                    side1Image = ko.observable(),
                    side2Image = ko.observable(),
                    showSide1Image = ko.observable(true),
                    // Is Calculating Ptv
                    isPtvCalculationInProgress = ko.observable(false),
                    // Is Side 1 Ink button clicked
                    isSide1InkButtonClicked = ko.observable(false),
                    // Currency Unit fOr Organisation 
                    currencyUnit = ko.observable(),
                    // Length Unit fOr Organisation 
                    lengthUnit = ko.observable(),
                    weightUnit = ko.observable(),
                     isStoreTax = ko.observable(),

                    //productName = ko.observable(),

                     //productName = ko.computed(function () {
                     //    return selectedProduct() && selectedProduct().productName() ? "(" + selectedProduct().productName() + ") Select Product Categories" : 'Products';
                     //}),
                    // Selected Region Id
                    selectedRegionId = ko.observable(),
                    // Selected Category Type Id
                    selectedCategoryTypeId = ko.observable(),
                    // Selected Category
                    selectedDesignerCategory = ko.observable(),
                    // Available Template Categories
                    availableCategoriesForTemplate = ko.computed(function () {
                        if (!templateCategories) {
                            return [];
                        }

                        return templateCategories.filter(function (productCategory) {
                            return (!selectedRegionId() || productCategory.regionId === selectedRegionId()) &&
                                (!selectedCategoryTypeId() || productCategory.typeId === selectedCategoryTypeId());
                        });
                    }),
                    // #endregion Arrays
                    // True if page has errors
                    pageHasErrors = ko.observable(false),
                    // #region Busy Indicators
                    isLoadingProducts = ko.observable(false),
                    // Is List View Active
                    isListViewVisible = ko.observable(false),
                    // Is Grid View Active
                    isGridViewVisible = ko.observable(true),
                    // Is Product Editor Visible
                    isProductDetailsVisible = ko.observable(false),
                    // Is Designer Category Base Data Loaded
                    isDesignerCategoryBaseDataLoaded = ko.observable(false),
                    // #endregion Busy Indicators
                    // #region Observables
                    // Item File Types
                    itemFileTypes = {
                        thumbnail: 1,
                        grid: 2,
                        imagePath: 3,
                        file1: 4
                    },
                    // filter
                    filterText = ko.observable(),
                    // filter for Related Items
                    filterTextForRelatedItems = ko.observable(),
                    // Active Product
                    selectedProduct = ko.observable(),
                    // Page Header 
                    pageHeader = ko.computed(function () {
                        return selectedProduct() && selectedProduct().productName() ? selectedProduct().productName()  : 'Select Product Category(s)';
                    }),

                     prodName = ko.computed(function () {
                         return selectedProduct() && selectedProduct().productName() ? "Section Detail -" + selectedProduct().productName() : 'Section Detail';
                     }),
                    // Sort On
                    sortOn = ko.observable(1),
                    // Sort Order -  true means asc, false means desc
                    sortIsAsc = ko.observable(true),
                    // Pagination
                    pager = ko.observable(new pagination.Pagination({ PageSize: 10 }, products)),
                    // Pagination For Item Relater Dialog
                    itemRelaterPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, productsToRelate)),
                    // Pagination For Press Dialog
                    pressDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, pressItems)),
                    // Item Actions
                    itemActions = {
                        onSaveVideo: function () {
                            closeVideoDialog();
                        },
                        onSelectRelatedItem: function () {
                            closeRelatedItemDialog();
                        },
                        onChooseStockItem: function (stockCategoryId) {
                            openStockItemDialog(stockCategoryId);
                        },
                        onSelectStockItem: function () {
                        },
                        onUpdateItemAddonCostCentre: function () {
                            openItemAddonCostCentreDialog();
                        },
                        onSaveItemAddonCostCentre: function () {
                            closeItemAddonCostCentreDialog();
                        },
                        onCostCentreChange: function (costCentreId, activeItemAddonCostCentre) {
                            setCostCentreToActiveItemAddonCostCentre(costCentreId, activeItemAddonCostCentre);
                        },
                        onFlagChange: function (flagId, itemId) {
                            getItemPriceMatricesForItemByFlag(flagId, itemId);
                        },
                        onPreBuiltTemplateSelected: function () {
                            selectPreBuiltTemplate();
                        },
                        onSelectPressItem: function () {
                            closePressDialog();
                        },
                        onAddProductMarketBriefQuestion: function () {
                            onAddProductMarketBriefQuestion();
                        },
                        onEditProductMarketBriefQuestion: function () {
                            onEditProductMarketBriefQuestion();
                        },
                        onDeleteProductMarketBriefQuestion: function (onProceed) {
                            onDeleteProductMarketBriefQuestion(onProceed);
                        },
                        onDeleteProductMarketBriefAnswer: function (onProceed) {
                            onDeleteProductMarketBriefAnswer(onProceed);
                        },
                        onDeleteItemRelatedItem: function (onProceed) {
                            onDeleteItemRelatedItem(onProceed);
                        }
                    },
                    // Item State Tax Constructor Params
                    itemStateTaxConstructorParams = { countries: countries(), states: states() },
                    // Selected Job Description
                    selectedJobDescription = ko.observable(),
                    // press Dialog Filter
                    pressDialogFilter = ko.observable(),
                    //#endregion
                    // #region Utility Functions
                    toggleView = function (data, e) {
                        view.changeView(e);
                    },
                    // Set List View Active
                    setListViewActive = function () {
                        isListViewVisible(true);
                        isGridViewVisible(false);
                    },
                    // Set Grid View Active
                    setGridViewActive = function () {
                        isListViewVisible(false);
                        isGridViewVisible(true);
                    },
                    // Create New Product
                    createProduct = function () {
                        selectedProduct(model.Item.Create({}, itemActions, itemStateTaxConstructorParams));
                        // Set First Section Flag to Item
                        if (sectionFlags() && sectionFlags().length > 0) {
                            selectedProduct().flagId(sectionFlags()[0].id);
                        }
                        openProductEditor();
                    },
                    // Edit Product
                    editProduct = function (data) {
                        getItemById(data.id(), openProductEditor);
                    },
                    // Select Designer Category for Product
                    selectDesignerCategoryForProduct = function () {
                        // Get Designer Category From list
                        if (selectedProduct().templateTypeUi() !== '3' || !selectedProduct().designerCategoryId()) {
                            return;
                        }

                        var designerCategory = getDesignerCategoryById(selectedProduct().designerCategoryId());
                        if (!designerCategory) {
                            return;
                        }

                        selectedRegionId(designerCategory.regionId);
                        selectedCategoryTypeId(designerCategory.typeId);
                        selectedDesignerCategory(selectedProduct().designerCategoryId());
                    },
                    // Open Editor
                    openProductEditor = function () {
                        // Show Basic Details Tab
                        view.showBasicDetailsTab();
                        // Show Details
                        isProductDetailsVisible(true);

                        // WIre up tab shown event
                        view.wireUpTabShownEvent();

                        // Wire Up Navigation Control - If has Changes and user navigates to another page
                        shared.initialize(selectedProduct, function (navigateCallback) {
                            onSaveProduct(null, null, navigateCallback);
                        });

                        // Initialize Label Popovers
                        view.initializeLabelPopovers();
                    },
                    // Initialize Product Category Dialog
                    initializeProductCategoryDialog = function () {
                        // Set Product Category true/false for popup
                        productCategories.each(function (productCategory) {
                            var productCategoryItem = selectedProduct().productCategoryItems.find(function (pci) {
                                return pci.categoryId() === productCategory.id;
                            });

                            if (productCategoryItem) {
                                productCategory.isSelected(productCategoryItem.isSelected());
                            }
                            else {
                                productCategory.isSelected(false);
                            }
                        });

                        // Update Input Checked States in Bindings
                        view.updateInputCheckedStates();
                    },
                    // On Close Editor
                    onCloseProductEditor = function () {
                        if (selectedProduct().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(onSaveProduct);
                            confirmation.afterCancel(function () {
                                selectedProduct().reset();
                                closeProductEditor();
                            });
                            confirmation.show();
                            return;
                        }
                        closeProductEditor();
                    },
                    // Close Editor
                    closeProductEditor = function () {
                        selectedProduct(model.Item.Create({}, itemActions, itemStateTaxConstructorParams));
                        resetVideoCounter();
                        resetProductMarketBriefQuestionCounter();
                        isProductDetailsVisible(false);
                        selectedDesignerCategory(undefined);
                        selectedRegionId(undefined);
                        selectedCategoryTypeId(undefined);
                        errorList.removeAll();
                        shared.reset();
                    },
                    // On Archive
                    onArchiveProduct = function (item) {
                        confirmation.afterProceed(function () {
                            archiveProduct(item.id());
                        });
                        confirmation.show();
                    },
                    // Active Video Item
                    activeVideo = ko.observable(),
                    // Added Video Counter
                    videoCounter = -1,
                    // Reset Video Counter
                    resetVideoCounter = function () {
                        videoCounter = -1;
                    },
                    // On Add Video
                    onAddVideo = function () {
                        // Open Video Dialog  
                        openVideoDialog();
                        activeVideo(model.ItemVideo.Create({ VideoId: 0, ItemId: selectedProduct().id() }));
                    },
                    onEditVideo = function (itemVideo) {
                        // Open Video Dialog  
                        openVideoDialog();
                        activeVideo(itemVideo);
                    },
                    // Save Video
                    saveVideo = function () {
                        if (activeVideo().id() === 0) { // Add
                            activeVideo().id(videoCounter);
                            selectedProduct().addVideo(activeVideo());
                            videoCounter -= 1;
                            return;
                        }
                        closeVideoDialog();
                    },
                    // Open Video Dialog
                    openVideoDialog = function () {
                        // Reset Current Video Item
                        activeVideo(model.ItemVideo.Create({}));
                        view.showVideoDialog();
                    },
                    // Close Video Dialog
                    closeVideoDialog = function () {
                        view.hideVideoDialog();
                    },
                    // On Add Related Item
                    onAddRelatedItem = function () {
                        // Reset Filter & Pager
                        filterTextForRelatedItems(undefined);
                        // Reset Pager
                        itemRelaterPager().reset();
                        // Get Items 
                        getItemsToRelate(openRelatedItemDialog);
                    },
                    // Open Related Item Dialog
                    openRelatedItemDialog = function () {
                        view.showRelatedItemDialog();
                    },
                    // Close Related Item Dialog
                    closeRelatedItemDialog = function () {
                        view.hideRelatedItemDialog();
                    },
                    // Select Job Description
                    selectJobDescription = function (jobDescription, e) {
                        selectedJobDescription(e.currentTarget.id);
                    },
                    // Open Stock Item Dialog
                    openStockItemDialog = function (stockCategoryId) {
                        stockDialog.show(function (stockItem) {
                            selectedProduct().onSelectStockItem(stockItem);
                        }, stockCategoryId, false);
                    },
                    // Search Press Items
                    searchPressItems = function () {
                        pressDialogPager().reset();
                        getPressItems();
                    },
                    // Reset Stock Items
                    resetPressItems = function () {
                        // Reset Text 
                        resetPressDialogFilter();
                        // Filter Record
                        searchPressItems();
                    },
                    // Reset Press Dialog Filter
                    resetPressDialogFilter = function () {
                        // Reset Text 
                        pressDialogFilter(undefined);
                    },
                    // Open Stock Item Dialog
                    openPressDialog = function () {
                        resetPressDialogFilter();
                        view.showPressDialog();
                        searchPressItems();
                    },
                    // Close Stock Item Dialog
                    closePressDialog = function () {
                        view.hidePressDialog();
                    },
                    // Selected Section
                    selectedSection = ko.observable(),
                    // Edit Item Section
                    editSectionSignature = function (itemSection) {
                        selectedProduct().selectItemSection(itemSection);
                        selectedSection(itemSection);
                        // Subscribe Section Changes
                        subscribeSectionChanges();
                        openSignatureDialog();
                    },
                    // Open Signature Dialog
                    openSignatureDialog = function () {
                        view.showSignatureDialog();
                    },
                    // Close Signature Dialog
                    closeSignatureDialog = function () {
                        view.hideSignatureDialog();
                        selectedSection(undefined);
                    },
                    // Open Item Addon Cost Centre Dialog
                    openItemAddonCostCentreDialog = function () {
                        view.showItemAddonCostCentreDialog();
                    },
                    // Close Item Addon Cost Centre Dialog
                    closeItemAddonCostCentreDialog = function () {
                        view.hideItemAddonCostCentreDialog();
                    },
                    // Get Cost Centre By Id
                    getCostCentreById = function (id) {
                        if (costCentres().length === 0) {
                            return null;
                        }

                        return costCentres.find(function (costCentre) {
                            return costCentre.id === id;
                        });
                    },
                    // Set Cost Centre to active Item Add on cost centre
                    setCostCentreToActiveItemAddonCostCentre = function (costCentreId, activeItemAddonCostCentre) {
                        var costCentre = getCostCentreById(costCentreId);

                        if (!costCentre) {
                            return;
                        }

                        activeItemAddonCostCentre.costCentreName(costCentre.name);
                        activeItemAddonCostCentre.costCentreType(costCentre.type);
                    },
                    // open Product Category Dialog
                    openProductCategoryDialog = function () {
                        getProductCategories(selectedCompany(), function () {
                            initializeProductCategoryDialog();
                            view.showProductCategoryDialog();
                        });
                    },
                    // open Product Category Dialog
                    closeProductCategoryDialog = function () {
                        view.hideProductCategoryDialog();
                    },
                     changeIcon = function (event) {
                         if (event.target.classList.contains("fa-chevron-circle-right")) {
                             // ReSharper disable Html.TagNotResolved
                             event.target.classList.remove("fa-chevron-circle-right");

                             event.target.classList.add("fa-chevron-circle-down");
                         } else {
                             event.target.classList.remove("fa-chevron-circle-down");
                             event.target.classList.add("fa-chevron-circle-right");
                             // ReSharper restore Html.TagNotResolved
                         }
                     },
                    // Toggle Child Categories
                    toggleChildCategories = function (data, event) {
                        // If Child Categories exist then don't send call
                        changeIcon(event);
                        if (view.toggleChildCategories(event)) {
                            return;
                        }
                        var categoryId = view.getCategoryIdFromElement(event);

                        getChildCategories(categoryId, event);
                    },
                    // Update Product Categories to Selected Product
                    updateProductCategories = function () {
                        selectedProduct().updateProductCategoryItems(productCategories());
                        view.hideProductCategoryDialog();
                    },
                    // update Checked state for category
                    updateCheckedStateForCategory = function (data, event) {
                        var categoryId = view.getCategoryIdFromElement(event);
                        // get category by id
                        var productCategory = productCategories.find(function (pcat) {
                            return pcat.id === categoryId;
                        });

                        if (!productCategory) {
                            return false;
                        }

                        if ($(event.target).is(':checked')) {
                            productCategory.isSelected(true);
                        }
                        else {
                            productCategory.isSelected(false);
                        }

                        return true;
                    },
                    // Open Phrase Library
                    openPhraseLibrary = function () {
                        // ReSharper disable Html.IdNotResolved
                        $("#idheadingPhraseLibrary").html("Select a phrase");
                        // ReSharper restore Html.IdNotResolved
                        phraseLibrary.show(function (phrase) {
                            updateJobDescription(phrase);
                        });
                    },
                    // Update Job Description
                    updateJobDescription = function (phrase) {
                        if (!phrase) {
                            return;
                        }

                        // Set Phrase to selected Job Description
                        if (selectedJobDescription() === 'txtDescription1') {
                            selectedProduct().jobDescription1(selectedProduct().jobDescription1() ? selectedProduct().jobDescription1() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription2') {
                            selectedProduct().jobDescription2(selectedProduct().jobDescription2() ? selectedProduct().jobDescription2() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription3') {
                            selectedProduct().jobDescription3(selectedProduct().jobDescription3() ? selectedProduct().jobDescription3() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription4') {
                            selectedProduct().jobDescription4(selectedProduct().jobDescription4() ? selectedProduct().jobDescription4() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription5') {
                            selectedProduct().jobDescription5(selectedProduct().jobDescription5() ? selectedProduct().jobDescription5() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription6') {
                            selectedProduct().jobDescription6(selectedProduct().jobDescription6() ? selectedProduct().jobDescription6() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription7') {
                            selectedProduct().jobDescription7(selectedProduct().jobDescription7() ? selectedProduct().jobDescription7() + ' ' + phrase : phrase);
                        }
                    },
                    // ON Pre-Build Template Option Selected 
                    selectPreBuiltTemplate = function () {
                        confirmation.messageText("Do you want to keep existing Template Objects?");
                        confirmation.afterProceed(function () {
                            selectedProduct().templateTypeMode(2);
                        });
                        confirmation.afterCancel(function () {
                            selectedProduct().templateTypeMode(1);
                        });
                        confirmation.show();
                    },
                    // Get Designer Category by Id
                    getDesignerCategoryById = function (categoryId) {
                        return _.find(availableCategoriesForTemplate(), function (category) {
                            return category.id === categoryId;
                        });
                    },
                    // Can Edit Template From Editor
                    canEditTemplate = function (product) {
                        if (!product) {
                            return false;
                        }

                        return ((product.isFinishedGoodsUi() === '1') && (product.template() && product.template().id()) &&
                            (product.templateTypeUi() !== '3'));
                    },
                    // Can Edit Template
                    canEditTemplateFromEditor = ko.computed(function () {
                        if (!selectedProduct()) {
                            return false;
                        }

                        return ((selectedProduct().isFinishedGoodsUi() === '1') && (selectedProduct().template() && selectedProduct().template().id()) &&
                            (selectedProduct().templateTypeUi() !== '3'));
                    }),
                    // Edit Template
                    editTemplate = function (product) {
                        view.editTemplate(product);
                    },
                    // On Delete Product
                    onDeleteProduct = function () {
                        confirmation.afterProceed(function () {
                            deleteProduct(selectedProduct().id());
                        });
                        confirmation.show();
                    },
                    onDeleteTemplatePage = function (templatePage) {
                        confirmation.messageText("Do you want to proceed with the request?");
                        confirmation.afterProceed(function () {
                            selectedProduct().template().removeTemplatePage(templatePage);
                        });
                        confirmation.show();
                    },
                    onDeleteItemAddonCostCentre = function () {
                        confirmation.messageText("Do you want to proceed with the request?");
                        confirmation.afterProceed(function () {
                            selectedProduct().activeStockOption().removeItemAddonCostCentre();
                        });
                        confirmation.show();
                    },
                    onDeleteItemStockOption = function (itemStockOption) {
                        confirmation.messageText("Do you want to proceed with the request?");
                        confirmation.afterProceed(function () {
                            selectedProduct().removeItemStockOption(itemStockOption);
                        });
                        confirmation.show();
                    },
                    // Added ProductMarketBriefQuestion Counter
                    productMarketBriefQuestionCounter = -1,
                    // Reset ProductMarketBriefQuestion Counter
                    resetProductMarketBriefQuestionCounter = function () {
                        productMarketBriefQuestionCounter = -1;
                    },
                    // Open Market Brief Question Dialog
                    openProductMarketBriefQuestionDialog = function () {
                        view.showMarketBriefQuestionDialog();
                    },
                    // Close Market Brief Question Dialog
                    closeProductMarketBriefQuestionDialog = function () {
                        view.hideMarketBriefQuestionDialog();
                    },
                    // On Add ProductMarketBriefQuestion
                    onAddProductMarketBriefQuestion = function () {
                        // Open ProductMarketBriefQuestion Dialog  
                        openProductMarketBriefQuestionDialog();
                    },
                    onEditProductMarketBriefQuestion = function () {
                        // Open ProductMarketBriefQuestion Dialog  
                        openProductMarketBriefQuestionDialog();
                    },
                    // Save Market Brief Question
                    saveProductMarketBriefQuestion = function () {
                        if (selectedProduct().activeProductMarketQuestion().id() === 0) { // Add
                            selectedProduct().activeProductMarketQuestion().id(productMarketBriefQuestionCounter);
                            selectedProduct().addProductMarketBriefQuestion();
                            productMarketBriefQuestionCounter -= 1;
                        }
                        closeProductMarketBriefQuestionDialog();
                    },
                    // On Delete Product Market Brief Question
                    onDeleteProductMarketBriefQuestion = function (onProceed) {
                        confirmation.messageText("Do you want to delete this market brief question?");
                        confirmation.afterProceed(function () {
                            if (onProceed && typeof onProceed === "function") {
                                onProceed();
                            }
                            closeProductMarketBriefQuestionDialog();
                        });
                        confirmation.show();
                    },
                    // On Delete Product Market Brief Answer
                    onDeleteProductMarketBriefAnswer = function (onProceed) {
                        confirmation.messageText("Do you want to delete this market brief answer?");
                        confirmation.afterProceed(function () {
                            if (onProceed && typeof onProceed === "function") {
                                onProceed();
                            }
                        });
                        confirmation.show();
                    },
                    // On Delete Item Related Item
                    onDeleteItemRelatedItem = function (onProceed) {
                        confirmation.messageText("Do you want to delete this upsell product?");
                        confirmation.afterProceed(function () {
                            if (onProceed && typeof onProceed === "function") {
                                onProceed();
                            }
                        });
                        confirmation.show();
                    },
                    // Initialize the view model
                    initialize = function (specifiedView, isOnStoreScreen) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        pager(new pagination.Pagination({ PageSize: 10 }, products, getItems));

                        itemRelaterPager(new pagination.Pagination({ PageSize: 5 }, productsToRelate, getItemsToRelate));

                        pressDialogPager(new pagination.Pagination({ PageSize: 5 }, pressItems, getPressItems));

                        if (!isOnStoreScreen) {
                            // Get Base Data
                            getBaseData();

                            // Get Items
                            getItems();
                        }

                        // Set Open From Flag to false - so that popup don't show until button gets clicked
                        phraseLibrary.isOpenFromPhraseLibrary(false);

                        // Subscribe Designer Category Selection
                        selectedDesignerCategory.subscribe(function (value) {
                            if (!selectedProduct()) {
                                return;
                            }

                            if (value === selectedProduct().designerCategoryId()) {
                                return;
                            }

                            selectedProduct().designerCategoryId(value);

                            // Get Designer Category From list
                            var designerCategory = getDesignerCategoryById(value);

                            if (!designerCategory) {
                                return;
                            }

                            // Set Zoom Factor and Scalar default
                            selectedProduct().zoomFactor(1);
                            selectedProduct().scalar(designerCategory.scalarFactor);
                        });
                    },
                    // Get Press By Id
                    getPressById = function (pressId) {
                        return presses.find(function (press) {
                            return press.id === pressId;
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
                        if (selectedProduct() && selectedProduct().activeItemSection() == undefined) {
                            return;
                        }
                        // Subscribe change events for ptv calculation
                        selectedProduct().activeItemSection().isDoubleSided.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().isDoubleSided()) {
                                selectedProduct().activeItemSection().isDoubleSided(value);
                            }
                            if (selectedProduct().activeItemSection().printingTypeUi() === '2') {
                                return;
                            }
                            getPtvCalculation();
                        });

                        // Work n Turn
                        selectedProduct().activeItemSection().isWorknTurn.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().isWorknTurn()) {
                                selectedProduct().activeItemSection().isWorknTurn(value);
                            }
                            if (selectedProduct().activeItemSection().printingTypeUi() === '2') {
                                return;
                            }
                            getPtvCalculation();
                        });

                        // On Select Sheet Size
                        selectedProduct().activeItemSection().sectionSizeId.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().sectionSizeId()) {
                                selectedProduct().activeItemSection().sectionSizeId(value);
                            }

                            // Get Paper Size by id
                            var paperSize = getPaperSizeById(value);

                            // Set Sizes To Custom Fields 
                            if (paperSize) {
                                selectedProduct().activeItemSection().sectionSizeHeight(paperSize.height);
                                selectedProduct().activeItemSection().sectionSizeWidth(paperSize.width);

                                if (selectedProduct().activeItemSection().printingTypeUi() === '2') {
                                    return;
                                }

                                // Get Ptv Calculation
                                getPtvCalculation();
                            }
                        });

                        // Section Height
                        selectedProduct().activeItemSection().sectionSizeHeight.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().sectionSizeHeight()) {
                                selectedProduct().activeItemSection().sectionSizeHeight(value);
                            }

                            if (!selectedProduct().activeItemSection().isSectionSizeCustom() || selectedProduct().activeItemSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // Section Width
                        selectedProduct().activeItemSection().sectionSizeWidth.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().sectionSizeWidth()) {
                                selectedProduct().activeItemSection().sectionSizeWidth(value);
                            }

                            if (!selectedProduct().activeItemSection().isSectionSizeCustom() || selectedProduct().activeItemSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // On Select Item Size
                        selectedProduct().activeItemSection().itemSizeId.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().itemSizeId()) {
                                selectedProduct().activeItemSection().itemSizeId(value);
                            }

                            // Get Paper Size by id
                            var paperSize = getPaperSizeById(value);

                            // Set Sizes To Custom Fields 
                            if (paperSize) {
                                selectedProduct().activeItemSection().itemSizeHeight(paperSize.height);
                                selectedProduct().activeItemSection().itemSizeWidth(paperSize.width);

                                if (selectedProduct().activeItemSection().printingTypeUi() === '2') {
                                    return;
                                }

                                // Get Ptv Calculation
                                getPtvCalculation();
                            }
                        });

                        // item Height
                        selectedProduct().activeItemSection().itemSizeHeight.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().itemSizeHeight()) {
                                selectedProduct().activeItemSection().itemSizeHeight(value);
                            }

                            if (!selectedProduct().activeItemSection().isItemSizeCustom() || selectedProduct().activeItemSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });

                        // item Width
                        selectedProduct().activeItemSection().itemSizeWidth.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().itemSizeWidth()) {
                                selectedProduct().activeItemSection().itemSizeWidth(value);
                            }

                            if (!selectedProduct().activeItemSection().isItemSizeCustom() || selectedProduct().activeItemSection().printingTypeUi() === '2') {
                                return;
                            }

                            getPtvCalculation();
                        });
                        
                        // On Press Change set Section Size Width to Press Max Width
                        selectedProduct().activeItemSection().pressId.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().pressId()) {
                                selectedProduct().activeItemSection().pressId(value);
                            }

                            var press = getPressById(value);
                            if (!press) {
                                return;
                            }

                            selectedProduct().activeItemSection().sectionSizeWidth(press.maxSheetWidth || 0);
                            selectedProduct().activeItemSection().pressIdSide1ColourHeads(press.colourHeads || 0);
                            selectedProduct().activeItemSection().pressIdSide1IsSpotColor(press.isSpotColor || false);
                            // Update Section Ink Coverage
                            selectedProduct().activeItemSection().sectionInkCoverageList.removeAll(selectedProduct().activeItemSection().sectionInkCoveragesSide1());
                            for (var i = 0; i < press.colourHeads; i++) {
                                selectedProduct().activeItemSection().sectionInkCoverageList.push(model.SectionInkCoverage.Create({
                                    SectionId: selectedProduct().activeItemSection().id(),
                                    Side: 1,
                                    InkOrder: i + 1
                                }));
                            }
                        });

                        // On Press Side 2 Change set Section Size Width to Press Max Width
                        selectedProduct().activeItemSection().pressIdSide2.subscribe(function (value) {
                            if (value !== selectedProduct().activeItemSection().pressIdSide2()) {
                                selectedProduct().activeItemSection().pressIdSide2(value);
                            }

                            var press = getPressById(value);
                            if (!press) {
                                return;
                            }

                            selectedProduct().activeItemSection().pressIdSide2ColourHeads(press.colourHeads || 0);
                            selectedProduct().activeItemSection().pressIdSide2IsSpotColor(press.isSpotColor || false);
                            // Update Section Ink Coverage
                            selectedProduct().activeItemSection().sectionInkCoverageList.removeAll(selectedProduct().activeItemSection().sectionInkCoveragesSide2());
                            for (var i = 0; i < press.colourHeads; i++) {
                                selectedProduct().activeItemSection().sectionInkCoverageList.push(model.SectionInkCoverage.Create({
                                    SectionId: selectedProduct().activeItemSection().id(),
                                    Side: 2,
                                    InkOrder: i + 1
                                }));
                            }
                        });
                    },
                    // #region For Store
                    // Selected Company Id
                    selectedCompany = ko.observable(),
                    // Selected Category
                    selectedCategory = ko.observable(),
                    defaultTaxRate = ko.observable(),
                    // Select Category
                    categorySelectedEventHandler = function (category) {
                        if (category && selectedCategory() !== category) {
                            selectedCategory(category);
                            // Filter Items on This Category
                            resetFilter(true);
                        }
                    },
                    // Is Product Section Initialized
                    isProductSectionInitialized = false,
                    // Initialize the view model from Store
                    initializeForStore = function (companyId, taxRate) {
                        if (selectedCompany() !== companyId) {
                            selectedCompany(companyId);
                            defaultTaxRate(taxRate);
                            // Reset Designer load flag, to load smart forms list for this company
                            isDesignerCategoryBaseDataLoaded(false);
                        }

                        var productDetailBinding = $("#productDetailBinding")[0];
                        var productBinding = $("#productBinding")[0];
                        var productPagerBinding = $("#pagerDivForProducts")[0];
                        setTimeout(function () {
                            if (!isProductSectionInitialized) {
                                ko.cleanNode(productBinding);
                                ko.cleanNode(productDetailBinding);
                                ko.cleanNode(productPagerBinding);
                                ko.applyBindings(view.viewModel, productBinding);
                                ko.applyBindings(view.viewModel, productDetailBinding);
                                ko.applyBindings(view.viewModel, productPagerBinding);
                                isProductSectionInitialized = true;
                            }
                        }, 1000);

                        // Get Base Data
                        getBaseData();

                        // Get Items for Store
                        resetFilter();
                    },
                    // #endregion
                    // Map Products 
                    mapProducts = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Item.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(products(), itemsList);
                        products.valueHasMutated();
                    },
                    // Map Products To Relate
                    mapProductsToRelate = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            if (item.ItemId !== selectedProduct().id()) { // Can't Relate Product with itself
                                itemsList.push(model.ItemRelatedItem.Create({
                                    RelatedItemId: item.ItemId,
                                    RelatedItemName: item.ProductName,
                                    RelatedItemCode: item.ProductCode
                                }));
                            }
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(productsToRelate(), itemsList);
                        productsToRelate.valueHasMutated();
                    },
                    // Map Press Items 
                    mapPressItems = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Machine.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(pressItems(), itemsList);
                        pressItems.valueHasMutated();
                    },
                    // Filter Products to Relate
                    filterProductsToRelate = function () {
                        // Reset Pager
                        itemRelaterPager().reset();
                        // Get Items to Relate
                        getItemsToRelate();
                    },
                    // Reset Filtered Products
                    resetFilteredProducts = function () {
                        // Reset Text 
                        filterTextForRelatedItems(undefined);
                        // Filter Record
                        filterProductsToRelate();
                    },
                    // Filter Products
                    filterProducts = function (shouldKeepCategory) {
                        // Reset Pager
                        pager().reset();
                        if (!(_.isBoolean(shouldKeepCategory)) || !shouldKeepCategory) {
                            // Reset Category
                            selectedCategory(undefined);
                        }
                        // Get Items
                        getItems();
                    },
                    // Reset Filter
                    resetFilter = function (shouldKeepCategory) {
                        // Reset Text 
                        filterText(undefined);
                        // Filter Record
                        filterProducts(shouldKeepCategory);
                    },
                    // On Save Product
                    onSaveProduct = function (data, event, navigateCallback) {
                        if (!doBeforeSave()) {
                            return;
                        }

                        var callback = closeProductEditor;
                        saveProduct(callback, navigateCallback);
                    },
                    // Prompt for Designer
                    promptForDesigner = function (product) {
                        confirmation.messageText("Do you want to open designer?");
                        confirmation.afterProceed(function () {
                            editTemplate(product);
                        });
                        confirmation.afterCancel();
                        confirmation.show();
                    },
                    // Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedProduct().isValid()) {
                            selectedProduct().showAllErrors();
                            selectedProduct().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    // On Clone Product
                    onCloneProduct = function (data) {
                        confirmation.messageText("Do you want to copy product?");
                        confirmation.afterProceed(function () {
                            cloneProduct(data, openProductEditor);
                        });
                        confirmation.afterCancel();
                        confirmation.show();
                    },
                    // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    // Get Item From List
                    getItemFromList = function (id) {
                        return products.find(function (item) {
                            return item.id() === id;
                        });
                    },
                    // Map Cost Centres
                    mapCostCentres = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.CostCentre.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(costCentres(), itemsList);
                        costCentres.valueHasMutated();
                    },
                    // Map Countreis
                    mapCountries = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Country.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(countries(), itemsList);
                        countries.valueHasMutated();
                    },
                    // Map State
                    mapStates = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.State.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(states(), itemsList);
                        states.valueHasMutated();
                    },
                    // Map Section Flags
                    mapSectionFlags = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.SectionFlag.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(sectionFlags(), itemsList);
                        sectionFlags.valueHasMutated();
                    },
                    // Map Suppliers
                    mapSuppliers = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Company.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(suppliers(), itemsList);
                        suppliers.valueHasMutated();
                    },
                    // Map Product Categories
                    mapProductCategories = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.ProductCategory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(productCategories(), itemsList);
                        productCategories.valueHasMutated();
                    },
                    // Map Designer Categories
                    mapDesignerCategories = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.ProductCategoryForTemplate.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(templateCategories(), itemsList);
                        templateCategories.valueHasMutated();
                    },
                    // Map Category Regions
                    mapCategoryRegions = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.CategoryRegion.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(categoryRegions(), itemsList);
                        categoryRegions.valueHasMutated();
                    },
                    // Map Category Types
                    mapCategoryTypes = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.CategoryType.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(categoryTypes(), itemsList);
                        categoryTypes.valueHasMutated();
                    },
                    // Map Smart Forms
                    mapSmartForms = function (data) {
                        // Push to Original Array
                        ko.utils.arrayPushAll(smartForms(), data);
                        smartForms.valueHasMutated();
                    },
                    // Map Paper Sizes
                    mapPaperSizes = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.PaperSize.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(paperSizes(), itemsList);
                        paperSizes.valueHasMutated();
                    },
                    // Map Inks
                    mapInks = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(item);
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(inks(), itemsList);
                        inks.valueHasMutated();
                    },
                    // Map Presses
                    mapPresses = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Machine.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(presses(), itemsList);
                        presses.valueHasMutated();
                    },
                    // Set Item Price Matrices to Current Item against selected Flag
                    setItemPriceMatricesToItem = function (itemPriceMatrices) {
                        // Only ask for confirmation if it is not a new product
                        if ((!itemPriceMatrices || itemPriceMatrices.length === 0) && selectedProduct().id()) {
                            confirmation.messageText("There are no price items against this flag. Do you want to Add New?");
                            confirmation.afterProceed(selectedProduct().setItemPriceMatrices);
                            confirmation.afterCancel(selectedProduct().removeExistingPriceMatrices);
                            confirmation.show();
                            return;
                        }

                        // Set Price Matrix to Item against selected Flag
                        selectedProduct().setItemPriceMatrices(itemPriceMatrices);
                    },
                    // Get Item From list by id
                    getItemByIdLocal = function (id) {
                        return products.find(function (item) {
                            return item.id() === id;
                        });
                    },
                    // Add Item to List After Save
                    addToItemsList = function (data, callback) {
                        // Update Id
                        selectedProduct().id(data.ItemId);

                        // Update Min Price
                        selectedProduct().miniPrice(data.MinPrice || 0);

                        // Update Template
                        if (data.Template) {
                            selectedProduct().template().id(data.Template.ProductId);
                            selectedProduct().templateId(data.Template.ProductId);
                        }

                        // Update Company Id
                        selectedProduct().companyId(data.CompanyId);

                        // Update Organisation Id
                        selectedProduct().organisationId(data.OrganisationId);

                        if (canEditTemplate(selectedProduct())) {
                            var newCallback = callback;
                            callback = function () {
                                promptForDesigner(selectedProduct());
                                if (callback && typeof callback === "function") {
                                    newCallback();
                                }

                            };
                        }

                        // Add to top of list
                        products.splice(0, 0, selectedProduct());

                        // Return Callback
                        return callback;
                    },
                    // Update Item in the List 
                    updateItemInList = function (data, callback) {
                        // Get Item
                        var item = getItemFromList(selectedProduct().id());
                        if (item) {
                            item.isFinishedGoods(data.ProductType !== undefined && data.ProductType != null ?
                                (data.ProductType === 0 ? 0 : data.ProductType) : undefined);
                            item.productCode(data.ProductCode);
                            item.productName(data.ProductName);
                            item.isEnabled(data.IsEnabled);
                            item.isPublished(data.IsPublished);
                            item.miniPrice(data.MinPrice || 0);
                            item.templateId(data.TemplateId || undefined);
                            item.templateType(data.TemplateType || undefined);
                            item.thumbnail(data.ThumbnailImageSource || undefined);
                            item.printCropMarks(data.PrintCropMarks || false);
                            item.drawWatermarkText(data.DrawWaterMarkTxt || false);
                            // Update Template
                            if (data.Template) {
                                item.template().id(data.Template.ProductId);
                            }
                            // Prompt for Designer
                            if (canEditTemplate(item)) {
                                var anotherCallback = callback;
                                callback = function () {
                                    promptForDesigner(item);
                                    if (callback && typeof callback === "function") {
                                        anotherCallback();
                                    }

                                };
                            }
                        }

                        // Return Callback
                        return callback;
                    },
                    // Is Base Data Loaded
                    isBaseDataLoaded = false,
                    // Get Base Data
                    getBaseData = function () {
                        if (isBaseDataLoaded) {
                            return;
                        }

                        dataservice.getBaseDataForProduct({
                            success: function (data) {
                                costCentres.removeAll();
                                countries.removeAll();
                                states.removeAll();
                                sectionFlags.removeAll();
                                suppliers.removeAll();
                                templateCategories.removeAll();
                                categoryRegions.removeAll();
                                categoryTypes.removeAll();
                                paperSizes.removeAll();
                                inks.removeAll();
                                lengthUnit(undefined);
                                weightUnit(undefined);
                                presses.removeAll();
                                currencyUnit(undefined);
                                if (data) {
                                    mapCostCentres(data.CostCentres);

                                    // Map Countries
                                    mapCountries(data.Countries);

                                    // Map States
                                    mapStates(data.States);

                                    // Map Section Flags
                                    mapSectionFlags(data.SectionFlags);

                                    // Map Suppliers
                                    mapSuppliers(data.Suppliers);

                                    // Map Product Categories
                                    mapDesignerCategories(data.TemplateCategories);

                                    // Map Category Regions
                                    mapCategoryRegions(data.CategoryRegions);

                                    // Map Category Types
                                    mapCategoryTypes(data.CategoryTypes);

                                    // Map Paper Sizes
                                    mapPaperSizes(data.PaperSizes);
                                    
                                    // Map Inks
                                    if (data.Inks) {
                                        mapInks(data.Inks);
                                    }
                                    
                                    // Map Presses
                                    if (data.Machines) {
                                        mapPresses(data.Machines);
                                    }

                                    // Map Units
                                    lengthUnit(data.LengthUnit || undefined);
                                    currencyUnit(data.CurrencyUnit || undefined);
                                    weightUnit(data.WeightUnit || undefined);
                                    // Assign countries & states to StateTaxConstructorParam
                                    itemStateTaxConstructorParams.countries = countries();
                                    itemStateTaxConstructorParams.states = states();

                                    isBaseDataLoaded = true;
                                }
                            },
                            error: function (response) {
                                pageHasErrors(true);
                                toastr.error("Failed to load base data. Error: " + response, "Please Reload", ist.toastrOptions);
                            }
                        });
                    },
                    // Get Base Data For Designer Category
                    getBaseDataForDesignerCategory = function () {
                        if (isDesignerCategoryBaseDataLoaded()) {
                            // Set Designer Category if Active Product has Template Type = 3
                            // Update Category Region, Type and Designer Category
                            selectDesignerCategoryForProduct();
                            return;
                        }

                        dataservice.getBaseDataForDesignerCategory({ id: selectedCompany() || 0 }, {
                            success: function (data) {
                                templateCategories.removeAll();
                                categoryRegions.removeAll();
                                categoryTypes.removeAll();
                                smartForms.removeAll();
                                if (data) {
                                    // Map Product Categories
                                    mapDesignerCategories(data.TemplateCategories);

                                    // Map Category Regions
                                    mapCategoryRegions(data.CategoryRegions);

                                    // Map Category Types
                                    mapCategoryTypes(data.CategoryTypes);

                                    // Map Smart Forms
                                    mapSmartForms(data.SmartForms);
                                }

                                isDesignerCategoryBaseDataLoaded(true);

                                // Set Designer Category if Active Product has Template Type = 3
                                // Update Category Region, Type and Designer Category
                                selectDesignerCategoryForProduct();
                            },
                            error: function (response) {
                                toastr.error("Failed to load base data for Designer" + response);
                            }
                        });
                    },
                    // Save Product
                    saveProduct = function (callback, navigateCallback) {
                        var product = selectedProduct().convertToServerData();
                        // If opened from store
                        if (selectedCompany()) {
                            product.CompanyId = selectedCompany();
                        }

                        dataservice.saveItem(product, {
                            success: function (data) {
                                if (!selectedProduct().id()) {
                                    callback = addToItemsList(data, callback);
                                }
                                else {
                                    // Finds Item and then updates it
                                    // Prompts for Designer if it is desinger product
                                    callback = updateItemInList(data, callback);
                                }

                                toastr.success("Saved Successfully.");

                                if (callback && typeof callback === "function") {
                                    callback();
                                }

                                if (navigateCallback && typeof navigateCallback === "function") {
                                    navigateCallback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Product. Error: " + response);
                            }
                        });
                    },
                    // Clone Product
                    cloneProduct = function (item, callback) {
                        dataservice.cloneItem({ ItemId: item.id() }, {
                            success: function (data) {
                                if (data) {
                                    var newItem = model.Item.Create(data, itemActions, itemStateTaxConstructorParams);
                                    // Add to top of list
                                    products.splice(0, 0, newItem);
                                    selectedProduct(newItem);

                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }

                                toastr.success("Cloned Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to Clone Product. Error: " + response);
                            }
                        });
                    },
                    // archive Product
                    archiveProduct = function (id) {
                        dataservice.archiveItem({
                            ItemId: id
                        }, {
                            success: function () {
                                // Remove that product from list
                                var item = getItemByIdLocal(id);
                                if (item) {
                                    products.remove(item);
                                }
                                closeProductEditor();
                                toastr.success("Archived Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to archive Product. Error: " + response);
                            }
                        });
                    },
                    // Get Item Price Matrices for Item By Flag
                    getItemPriceMatricesForItemByFlag = function (flagId, itemId) {
                        // If Item is new then avoid api call to look for existing price matrices
                        // Against it
                        if (!itemId) {
                            // Set Price Matrix to Item against selected Flag
                            selectedProduct().setItemPriceMatrices();
                            return;
                        }
                        dataservice.getItemPriceMatricesForItemByFlagId({
                            FlagId: flagId,
                            ItemId: itemId
                        }, {
                            success: function (data) {
                                setItemPriceMatricesToItem(data);
                            },
                            error: function (response) {
                                toastr.error("Failed to load item price matrix" + response);
                            }
                        });
                    },
                    // Get Press Items
                    getPressItems = function () {
                        dataservice.getMachines({
                            SearchString: pressDialogFilter(),
                            PageSize: pressDialogPager().pageSize(),
                            PageNo: pressDialogPager().currentPage()
                        }, {
                            success: function (data) {
                                pressItems.removeAll();
                                if (data && data.TotalCount > 0) {
                                    mapPressItems(data.Machines);
                                    pressDialogPager().totalCount(data.TotalCount);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Press items" + response);
                            }
                        });
                    },
                    // Get Items
                    getItems = function () {
                        isLoadingProducts(true);
                        dataservice.getItems({
                            SearchString: filterText(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            CompanyId: selectedCompany(),
                            CategoryId: selectedCategory()
                        }, {
                            success: function (data) {
                                products.removeAll();
                                if (data && data.TotalCount > 0) {
                                    mapProducts(data.Items);
                                    pager().totalCount(data.TotalCount);
                                }
                                isLoadingProducts(false);
                                view.initializeProductMinMaxSlider();
                            },
                            error: function (response) {
                                isLoadingProducts(false);
                                toastr.error("Failed to load items" + response);
                                view.initializeProductMinMaxSlider();
                            }
                        });
                    },
                    // Get Items To Relate
                    getItemsToRelate = function (callback) {
                        dataservice.getItems({
                            SearchString: filterTextForRelatedItems(),
                            PageSize: itemRelaterPager().pageSize(),
                            PageNo: itemRelaterPager().currentPage()
                        }, {
                            success: function (data) {
                                productsToRelate.removeAll();
                                if (data && data.TotalCount > 0) {
                                    mapProductsToRelate(data.Items);
                                    itemRelaterPager().totalCount(data.TotalCount);
                                }
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load items" + response);
                            }
                        });
                    },
                    // Get Item By Id
                    getItemById = function (id, callback) {
                        isLoadingProducts(true);
                        dataservice.getItem({
                            id: id
                        }, {
                            success: function (data) {
                                if (data) {
                                    selectedProduct(model.Item.Create(data, itemActions, itemStateTaxConstructorParams));

                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }
                                isLoadingProducts(false);
                            },
                            error: function (response) {
                                isLoadingProducts(false);
                                toastr.error("Failed to load item details" + response);
                            }
                        });
                    },

                    // Get Category Child List Items
                getChildCategories = function (id, event) {

                    dataservice.getProductCategoryChildsForProduct({
                        id: id,
                    }, {
                        success: function (data) {
                            if (data.ProductCategories != null) {
                                // Update Product Category Items
                                selectedProduct().updateProductCategoryItems(productCategories());

                                _.each(data.ProductCategories, function (productCategory) {
                                    productCategory.ParentCategoryId = id;
                                    var category = model.ProductCategory.Create(productCategory);
                                    if (selectedProduct()) {
                                        var productCategoryItem = selectedProduct().productCategoryItems.find(function (pCatItem) {
                                            return pCatItem.categoryId() === category.id;
                                        });

                                        if (productCategoryItem) {
                                            category.isSelected(productCategoryItem.isSelected());
                                        }
                                    }
                                    productCategories.push(category);
                                    view.appendChildCategory(event, category);
                                    initializeProductCategoryDialog();
                                });
                            }
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Error: Failed To load Categories " + response);
                        }
                    });
                },
                    // Get Product Categories
                    getProductCategories = function (id, callback) {
                        dataservice.getProductCategories({
                            id: id ? id : 0,
                        }, {
                            success: function (data) {
                                productCategories.removeAll();
                                if (data != null) {
                                    // Map Product Categories
                                    mapProductCategories(data);
                                }

                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Error: Failed To load Categories " + response);
                            }
                        });
                    },
                    //Side 1 Button Click
                    side1ButtonClick = function () {
                        showSide1Image(true);
                    },
                    //Side 2 Button Click
                    side2ButtonClick = function () {
                        showSide1Image(false);
                    },
                    // Open Ink Dialog
                    openInkDialog = function (data, e) {
                        if (e.currentTarget.id === "side1InkColorBtn") {
                            isSide1InkButtonClicked(true);
                        } else {
                            isSide1InkButtonClicked(false);
                        }
                        view.showInksDialog();
                    },
                    //Get PTV Calculation
                    getPtvCalculation = function (callback) {
                        if (isPtvCalculationInProgress()) {
                            return;
                        }
                        var selectedSection = selectedProduct().activeItemSection();
                        if (selectedSection.itemSizeHeight() == null || selectedSection.itemSizeWidth() == null || selectedSection.sectionSizeHeight() == null ||
                            selectedSection.sectionSizeWidth() == null) {
                            return;
                        }
                        var orient;
                        if (selectedSection.printViewLayoutPortrait() >= selectedSection.printViewLayoutLandscape()) {
                            orient = 0;
                            selectedProduct().activeItemSection().isPortrait(true);
                        } else {
                            orient = 1;
                            selectedProduct().activeItemSection().isPortrait(false);
                        }

                        isPtvCalculationInProgress(true);
                        dataservice.getPtvCalculation({
                            orientation: orient,
                            reversRows: 0,
                            revrseCols: 0,
                            isDoubleSided: selectedSection.isDoubleSided(),
                            isWorknTurn: selectedSection.isWorknTurn(),
                            isWorknTumble: false,
                            applyPress: false,
                            itemHeight: selectedSection.itemSizeHeight(),
                            itemWidth: selectedSection.itemSizeWidth(),
                            printHeight: selectedSection.sectionSizeHeight(),
                            printWidth: selectedSection.sectionSizeWidth(),
                            grip: 1,
                            gripDepth: 0,
                            headDepth: 0,
                            printGutter: 5,
                            horizentalGutter: 5,
                            verticalGutter: 5
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedProduct().activeItemSection().printViewLayoutLandscape(data.LandscapePTV || 0);
                                    selectedProduct().activeItemSection().printViewLayoutPortrait(data.PortraitPTV || 0);
                                }
                                isPtvCalculationInProgress(false);
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            },
                            error: function (response) {
                                isPtvCalculationInProgress(false);
                                toastr.error("Error: Failed to Calculate Number up value. Error: " + response, "", ist.toastrOptions);
                            }
                        });
                    },
                    // Get Ptv Plan
                    getPtvPlan = function () {
                        if (selectedProduct().activeItemSection().itemSizeHeight() == null || selectedProduct().activeItemSection().itemSizeWidth() == null ||
                            selectedProduct().activeItemSection().sectionSizeHeight() == null || selectedProduct().activeItemSection().sectionSizeWidth() == null) {
                            return;
                        }

                        var orient = selectedProduct().activeItemSection().printViewLayoutPortrait() >=
                            selectedProduct().activeItemSection().printViewLayoutLandscape() ? 0 : 1;
                        dataservice.getPtv({
                            orientation: orient,
                            reversRows: 0,
                            revrseCols: 0,
                            isDoubleSided: selectedProduct().activeItemSection().isDoubleSided(),
                            isWorknTurn: selectedProduct().activeItemSection().isWorknTurn(),
                            isWorknTumble: false,
                            applyPress: false,
                            itemHeight: selectedProduct().activeItemSection().itemSizeHeight(),
                            itemWidth: selectedProduct().activeItemSection().itemSizeWidth(),
                            printHeight: selectedProduct().activeItemSection().sectionSizeHeight(),
                            printWidth: selectedProduct().activeItemSection().sectionSizeWidth(),
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
                    // Delete Product
                    deleteProduct = function (id) {
                        dataservice.deleteItem({ ItemId: id }, {
                            success: function () {
                                toastr.success("Product deleted successfully!");
                                var product = getItemByIdLocal(id);
                                if (product) {
                                    products.remove(product);
                                }
                                closeProductEditor();
                            },
                            error: function (response) {
                                toastr.error("Failed to delete store. Error: " + response, "", ist.toastrOptions);
                            }
                        });
                    };
                // #endregion Service Calls

                return {
                    // Observables
                    selectedProduct: selectedProduct,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isLoadingProducts: isLoadingProducts,
                    products: products,
                    isListViewVisible: isListViewVisible,
                    isGridViewVisible: isGridViewVisible,
                    isProductDetailsVisible: isProductDetailsVisible,
                    pager: pager,
                    errorList: errorList,
                    filterText: filterText,
                    pageHeader: pageHeader,
                    prodName: prodName,
                    filterTextForRelatedItems: filterTextForRelatedItems,
                    itemRelaterPager: itemRelaterPager,
                    activeVideo: activeVideo,
                    productsToRelate: productsToRelate,
                    selectedJobDescription: selectedJobDescription,
                    itemFileTypes: itemFileTypes,
                    costCentres: costCentres,
                    sectionFlags: sectionFlags,
                    suppliers: suppliers,
                    productCategories: productCategories,
                    parentProductCategories: parentProductCategories,
                    availableCategoriesForTemplate: availableCategoriesForTemplate,
                    categoryRegions: categoryRegions,
                    categoryTypes: categoryTypes,
                    selectedRegionId: selectedRegionId,
                    selectedCategoryTypeId: selectedCategoryTypeId,
                    selectedDesignerCategory: selectedDesignerCategory,
                    pressDialogFilter: pressDialogFilter,
                    pressDialogPager: pressDialogPager,
                    pressItems: pressItems,
                    paperSizes: paperSizes,
                    selectedCompany: selectedCompany,
                    currencyUnit: currencyUnit,
                    lengthUnit: lengthUnit,
                    canEditTemplate: canEditTemplate,
                    isDesignerCategoryBaseDataLoaded: isDesignerCategoryBaseDataLoaded,
                    pageHasErrors: pageHasErrors,
                    canEditTemplateFromEditor: canEditTemplateFromEditor,
                    // Utility Methods
                    initialize: initialize,
                    resetFilter: resetFilter,
                    filterProducts: filterProducts,
                    resetFilteredProducts: resetFilteredProducts,
                    filterProductsToRelate: filterProductsToRelate,
                    toggleView: toggleView,
                    setListViewActive: setListViewActive,
                    setGridViewActive: setGridViewActive,
                    editProduct: editProduct,
                    createProduct: createProduct,
                    onSaveProduct: onSaveProduct,
                    onCloseProductEditor: onCloseProductEditor,
                    onArchiveProduct: onArchiveProduct,
                    closeVideoDialog: closeVideoDialog,
                    onAddVideo: onAddVideo,
                    onEditVideo: onEditVideo,
                    onAddRelatedItem: onAddRelatedItem,
                    saveVideo: saveVideo,
                    selectJobDescription: selectJobDescription,
                    openItemAddonCostCentreDialog: openItemAddonCostCentreDialog,
                    closeItemAddonCostCentreDialog: closeItemAddonCostCentreDialog,
                    gotoElement: gotoElement,
                    toggleChildCategories: toggleChildCategories,
                    updateProductCategories: updateProductCategories,
                    openProductCategoryDialog: openProductCategoryDialog,
                    closeProductCategoryDialog: closeProductCategoryDialog,
                    updateCheckedStateForCategory: updateCheckedStateForCategory,
                    openPhraseLibrary: openPhraseLibrary,
                    getBaseDataForDesignerCategory: getBaseDataForDesignerCategory,
                    openPressDialog: openPressDialog,
                    resetPressItems: resetPressItems,
                    searchPressItems: searchPressItems,
                    closePressDialog: closePressDialog,
                    editSectionSignature: editSectionSignature,
                    closeSignatureDialog: closeSignatureDialog,
                    onCloneProduct: onCloneProduct,
                    editTemplate: editTemplate,
                    onDeleteProduct: onDeleteProduct,
                    // For Store
                    initializeForStore: initializeForStore,
                    categorySelectedEventHandler: categorySelectedEventHandler,
                    smartForms: smartForms,
                    weightUnit: weightUnit,
                    isStoreTax: isStoreTax,
                    defaultTaxRate: defaultTaxRate,
                    onDeleteTemplatePage: onDeleteTemplatePage,
                    onDeleteItemAddonCostCentre: onDeleteItemAddonCostCentre,
                    onDeleteItemStockOption: onDeleteItemStockOption,
                    saveProductMarketBriefQuestion: saveProductMarketBriefQuestion,
                    closeProductMarketBriefQuestionDialog: closeProductMarketBriefQuestionDialog,
                    impressionCoverages: impressionCoverages,
                    side1ButtonClick: side1ButtonClick,
                    side2ButtonClick: side2ButtonClick,
                    openInkDialog: openInkDialog,
                    getPtvPlan: getPtvPlan,
                    getPtvCalculation: getPtvCalculation,
                    side1Image: side1Image,
                    side2Image: side2Image,
                    showSide1Image: showSide1Image,
                    itemPlan: itemPlan,
                    presses: presses,
                    inks: inks,
                    isSide1InkButtonClicked: isSide1InkButtonClicked,
                    selectedSection: selectedSection
                    // For Store
                    // Utility Methods
                };
            })()
        };
        return ist.product.viewModel;
    });
