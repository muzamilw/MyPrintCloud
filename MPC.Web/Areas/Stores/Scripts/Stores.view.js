/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("stores/stores.view",
    ["jquery", "stores/stores.viewModel"], function ($, storesViewModel) {

        var ist = window.ist || {};

        // View 
        ist.stores.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#storesBinding")[0],
                // Show Activity the dialog
                showRaveReviewDialog = function () {
                    $("#rave").modal("show");
                    initializeLabelPopovers();
                },
               
                // Hide Activity the dialog
                hideRaveReviewDialog = function () {
                    $("#rave").modal("hide");
                },
                showCompanyTerritoryDialog = function () {
                    $("#myTerritorySetModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Activity the dialog
                hideCompanyTerritoryDialog = function () {
                    $("#myTerritorySetModal").modal("hide");
                },
                // Show Activity the dialog
                // ReSharper disable once InconsistentNaming
                showCompanyCMYKColorDialog = function () {
                    $("#myCMYKColorModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Activity the dialog
                // ReSharper disable once InconsistentNaming
                hideCompanyCMYKColorDialog = function () {
                    $("#myCMYKColorModal").modal("hide");
                },
                // Show Edit Banner the dialog
                showEditBannerDialog = function () {
                    $("#myEditBannerModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Edit Banner the dialog
                hideEditBannerDialog = function () {
                    $("#myEditBannerModal").modal("hide");
                },
                // Show Addressnthe dialog
                showAddressDialog = function () {
                    $("#myAddressSetModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Address the dialog
                hideAddressDialog = function () {
                    $("#myAddressSetModal").modal("hide");
                },
                // Show Contact Company the dialog
                showCompanyContactDialog = function () {
                    $("#myContactProfileModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Company Contact the dialog
                hideCompanyContactDialog = function () {
                    $("#myContactProfileModal").modal("hide");
                },
                // Show Add Banner Set the dialog
                showSetBannerDialog = function () {
                    $("#mybannerSetModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide  Add Banner Set the dialog
                hideSetBannerDialog = function () {
                    $("#mybannerSetModal").modal("hide");
                },
                // Show Secondory Page the dialog
                showSecondoryPageDialog = function () {
                    $("#secondaryPageAddDialog").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Secondory Page the dialog
                hideSecondoryPageDialog = function () {
                    $("#secondaryPageAddDialog").modal("hide");
                },
                // Show Secondary Page Category the dialog
                showSecondaryPageCategoryDialog = function () {
                    $("#mySecondaryPageCategoryModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Secondary Page Category the dialog
                hideSecondaryPageCategoryDialog = function () {
                    $("#mySecondaryPageCategoryModal").modal("hide");
                },
                // Show Email Camapaign the dialog
                showEmailCamapaignDialog = function () {
                    $("#addEditCampaignEmailModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Email Camapaign the dialog
                hideEmailCamapaignDialog = function () {
                    $("#addEditCampaignEmailModal").modal("hide");
                },
                 // Show Ck Editor Dialog
                showCkEditorDialogDialog = function () {
                    $("#ckEditorDialog").modal("show");
                },
                // Hide Ck Editor Dialog
                hideCkEditorDialogDialog = function () {
                    $("#ckEditorDialog").modal("hide");
                },
                // Show Product Category dialog
                showStoreProductCategoryDialog = function () {
                    $("#myProductCategoryModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Product Category dialog
                hideStoreProductCategoryDialog = function () {
                    $("#myProductCategoryModal").modal("hide");
                },
                // show Payment Gateway Dialog
                showPaymentGatewayDialog = function () {
                    $("#myPaymentGatewayModal").modal("show");
                    initializeLabelPopovers();
                },
                // hide Payment Gateway Dialog 
                hidePaymentGatewayDialog = function () {
                    $("#myPaymentGatewayModal").modal("hide");
                },
                 // show Media Lib Image Dialog
                showMediaLibImageDialog = function () {
                    $("#mediaLibImageModel").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Media Lib Image Dialog
                hideMediaLibImageDialog = function () {
                    $("#mediaLibImageModel").modal("hide");
                },
                // show item sFor Widgets Dialog
                showItemsForWidgetsDialog = function () {
                    $("#itemsForWidgetsDialog").modal("show");
                    initializeLabelPopovers();
                },
                // hide items For Widgets Dialog
                hideItemsForWidgetsDialog = function () {
                    $("#itemsForWidgetsDialog").modal("hide");
                },
                // Show Media Gallery the dialog
                showMediaGalleryDialog = function () {
                    $("#myMediaGalleryModal").modal("show");
                },
                // Hide Media Gallery the dialog
                hideMediaGalleryDialog = function () {
                    $("#myMediaGalleryModal").modal("hide");
                },
                 // Show Veriable Defination the dialog
                showVeriableDefinationDialog = function () {
                    $("#veriableDefinationModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Veriable Defination the dialog
                hideVeriableDefinationDialog = function () {
                    $("#veriableDefinationModal").modal("hide");
                },
                // Show Smart Form Dialog
                showSmartFormDialog = function () {
                    $("#smartFormDialog").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Smart Form Dialog
                hideSmartFormDialog = function () {
                    $("#smartFormDialog").modal("hide");
                },

                //#endregion

               initializeForm = function () {
                   // Initialize Forms - For File Upload
                   $("#fileUploadForm").ajaxForm({
                       success: function () {
                           toastr.success("Uploading completed");
                       },
                       dataType: "json",
                       error: function () {
                           toastr.success("Uploading completed");
                       }
                   });
               },
               // Go To Element with Validation Errors
                gotoElement = function (element) {
                    var tab = $(element).closest(".tab-pane");
                    if (!tab) {
                        return;
                    }

                    var liElement = $('a[href=#' + tab.attr('id') + ']');
                    if (!liElement) {
                        return;
                    }

                    liElement.click();

                    // Scroll to Element
                    setTimeout(function () {
                        window.scrollTo($(element).offset().left, $(element).offset().top - 50);
                        // Focus on element
                        $(element).focus();
                    }, 1000);
                },
                // Initialize Label Popovers
                initializeLabelPopovers = function () {
                    // ReSharper disable UnknownCssClass
                    $('.bs-example-tooltips a').popover();
                    // ReSharper restore UnknownCssClass
                },
                // Product Category Selected Event 
                productCategorySelectedEvent = function(category) {
                    $.event.trigger({
                        type: "ProductCategorySelected",
                        category: category
                    });
                },
                // Sub Categories Loaded Event 
                subCategoriesLoadedEvent = function (categories) {
                    $.event.trigger({
                        type: "SubCategoriesLoaded",
                        categories: categories
                    });
                },
                // Wire Up Theme List Click event 
                wireupThemeListClick = function () {
                    $(document).ready(function () {
                        var themeListOpen = false;

                        $("#ops_theme_dropdown").click(function () {
                            if (themeListOpen == true) {
                                $("#ops_theme_dropdown #ops_theme_list ul").hide();
                                themeListOpen = false;
                            } else {

                                $("#ops_theme_dropdown #ops_theme_list ul").show();
                                themeListOpen = true;
                            }
                            return false;
                        });

                        $("#ops_theme_dropdown #ops_theme_list").click(function () {
                            if (themeListOpen == true) {
                                $("#ops_theme_dropdown #ops_theme_list ul").hide();
                                themeListOpen = false;
                            } else {

                                $("#ops_theme_dropdown #ops_theme_list ul").show();
                                themeListOpen = true;
                            }
                            return false;
                        });
                        $("#ops_theme_dropdown #ops_theme_list #ops_theme_select").click(function () {
                            if (themeListOpen == true) {
                                $("#ops_theme_dropdown #ops_theme_list ul").hide();
                                themeListOpen = false;
                            } else {

                                $("#ops_theme_dropdown #ops_theme_list ul").show();
                                themeListOpen = true;
                            }
                            return false;
                        });
                        $("body").click(function () {
                            if (themeListOpen == true) {
                                $("#ops_theme_dropdown #ops_theme_list ul").hide();
                                themeListOpen = false;
                            } 
                            return true;
                        });
                    });
                },
                // Close Theme List
                closeThemeList = function() {
                    $("#ops_theme_dropdown #ops_theme_list ul").hide();
                },
                // Expand Category to get childs
                expandCategory = function (categoryTreeNode, productCategoryId, isCategorySelectedFromViewModel) {
                    if (isCategorySelectedFromViewModel) {
                        // Get the Sub Category Tree Node Element
                        categoryTreeNode = $("#" + productCategoryId);
                        if (!categoryTreeNode) {
                            return;
                        }
                    }
                    var categoryNodeExpandOptions = categoryTreeNode.children(".dd-handle-list");
                    var categoryTreeNodeExpander = categoryNodeExpandOptions.children("i.fa-chevron-circle-right");
                    if (categoryTreeNodeExpander && categoryTreeNodeExpander[0]) {
                        // Get Child Categories
                        categoryTreeNodeExpander[0].click();
                    }
                    else {
                        categoryTreeNodeExpander = categoryNodeExpandOptions.children("i.fa-chevron-circle-down");
                        if (categoryTreeNodeExpander && categoryTreeNodeExpander[0]) {
                            // Get Child Categories
                            categoryTreeNodeExpander[0].click();
                        }
                    }
                },
                // Sub Category Selected Event Handler
                subCategorySelectedEventHandler = function (event) {
                    if (event.category && event.category.productCategoryId) {
                        var productCategoryId = ko.isObservable(event.category.productCategoryId) ?
                            event.category.productCategoryId() : event.category.productCategoryId;
                        // Get the Sub Category Tree Node Element
                        var categoryTreeNode = $("#" + productCategoryId);
                        if (!categoryTreeNode) {
                            return;
                        }
                        // Expand Category and get childs
                        expandCategory(categoryTreeNode);
                        // Get Products of Category
                        categoryTreeNode.click();
                    }
                },
                // Sub Category Edit Event Handler
                subCategoryEditEventHandler = function (event) {
                    if (event.category && event.category.productCategoryId) {
                        var productCategoryId = ko.isObservable(event.category.productCategoryId) ?
                            event.category.productCategoryId() : event.category.productCategoryId;
                        // Get the Sub Category Tree Node Element
                        var categoryTreeNode = $("#" + productCategoryId);
                        if (!categoryTreeNode) {
                            return;
                        }
                        var categoryTreeNodeEditor = categoryTreeNode.find("a");
                        if (categoryTreeNodeEditor && categoryTreeNodeEditor[0]) {
                            // Edit Category
                            categoryTreeNodeEditor[0].click();
                        }
                    }
                },
            // Initialize
            initialize = function () {
                if (!bindingRoot) {
                    return;
                }
                
                // subscribe to events
                $(document).on("SubCategorySelectedFromProduct", subCategorySelectedEventHandler);
                $(document).on("SubCategoryEdit", subCategoryEditEventHandler);
            };
            initialize();
            return {
                bindingRoot: bindingRoot,
                showRaveReviewDialog: showRaveReviewDialog,
                showVeriableDefinationDialog: showVeriableDefinationDialog,
                hideVeriableDefinationDialog: hideVeriableDefinationDialog,
                hideRaveReviewDialog: hideRaveReviewDialog,
                showCompanyCMYKColorDialog: showCompanyCMYKColorDialog,
                hideCompanyCMYKColorDialog: hideCompanyCMYKColorDialog,
                showEditBannerDialog: showEditBannerDialog,
                hideEditBannerDialog: hideEditBannerDialog,
                showCompanyTerritoryDialog: showCompanyTerritoryDialog,
                hideCompanyTerritoryDialog: hideCompanyTerritoryDialog,
                hideSetBannerDialog: hideSetBannerDialog,
                showSetBannerDialog: showSetBannerDialog,
                showAddressDialog: showAddressDialog,
                hideAddressDialog: hideAddressDialog,
                showSecondoryPageDialog: showSecondoryPageDialog,
                hideSecondoryPageDialog: hideSecondoryPageDialog,
                showCompanyContactDialog: showCompanyContactDialog,
                hideCompanyContactDialog: hideCompanyContactDialog,
                showSecondaryPageCategoryDialog: showSecondaryPageCategoryDialog,
                hideSecondaryPageCategoryDialog: hideSecondaryPageCategoryDialog,
                showEmailCamapaignDialog: showEmailCamapaignDialog,
                hideEmailCamapaignDialog: hideEmailCamapaignDialog,
                showPaymentGatewayDialog: showPaymentGatewayDialog,
                hidePaymentGatewayDialog: hidePaymentGatewayDialog,
                showCkEditorDialogDialog: showCkEditorDialogDialog,
                hideCkEditorDialogDialog: hideCkEditorDialogDialog,
                showItemsForWidgetsDialog: showItemsForWidgetsDialog,
                hideItemsForWidgetsDialog: hideItemsForWidgetsDialog,
                showStoreProductCategoryDialog: showStoreProductCategoryDialog,
                hideStoreProductCategoryDialog: hideStoreProductCategoryDialog,
                showMediaGalleryDialog: showMediaGalleryDialog,
                hideMediaGalleryDialog: hideMediaGalleryDialog,
                showSmartFormDialog: showSmartFormDialog,
                hideSmartFormDialog: hideSmartFormDialog,
                initializeForm: initializeForm,
                gotoElement: gotoElement,
                viewModel: viewModel,
                initializeLabelPopovers: initializeLabelPopovers,
                productCategorySelectedEvent: productCategorySelectedEvent,
                subCategoriesLoadedEvent: subCategoriesLoadedEvent,
                wireupThemeListClick: wireupThemeListClick,
                closeThemeList: closeThemeList,
                showMediaLibImageDialog: showMediaLibImageDialog,
                hideMediaLibImageDialog: hideMediaLibImageDialog,
                subCategorySelectedEventHandler: subCategorySelectedEventHandler,
                expandCategory: expandCategory
            };
        })(storesViewModel);

        // Initialize the view model
        if (ist.stores.view.bindingRoot) {
            storesViewModel.initialize(ist.stores.view);
        }
        return ist.stores.view;
    });
// Reads File - Print Out Section
function readURL(input) {
    $("input[id='my_file']").click();
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image;
            img.onload = function () {

                $('#companyImage').attr('src', e.target.result);
                if (ist.stores.viewModel.selectedStore().companyId() !== undefined) {
                    $('#orgImageSubmitBtn').attr('disabled', false);
                }
            };
            img.src = reader.result;

        };
        reader.readAsDataURL(input.files[0]);
    }
}