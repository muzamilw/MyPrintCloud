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
                // Show Discount Voucher Detail Dialog
                showDiscountVoucherDetailDialog = function () {
                    $("#discountVoucherDetailDialog").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Discount Voucher Detail Dialog
                hideDiscountVoucherDetailDialog = function () {
                    $("#discountVoucherDetailDialog").modal("hide");
                },
                
                 // Show CSS Dialog
                showCssDialog = function () {
                    $("#editStoreCssModal").modal("show");
                    initializeLabelPopovers();
                },
                // Hide CSS Dialog
                hideCssDialog = function () {
                    $("#editStoreCssModal").modal("hide");
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

                //#region product
                appendChildCategory = function(event, category) {
                    if (!event) {
                        return;
                    }

                    var targetElement = $(event.target).closest('li');
                    if (!targetElement) {
                        return;
                    }
                    
                    var inputElement = category.isSelected() ?
                        '<input class="bigcheckbox" style="float: right;" type="checkbox" checked="checked" data-bind="click: $root.updateCheckedStateForCategory"  />' :
                        '<input class="bigcheckbox" style="float: right;" type="checkbox" data-bind="click: $root.updateCheckedStateForCategory" />';
                    var childCategoryHtml;
                    if (category.isArchived) {
                        childCategoryHtml = '<ol class="dd-list"> ' +
                            '<li class="dd-item dd-item-list" id="liElement-' + category.id + '"> ' +
                            '<div class="dd-handle-list" ><i class="fa fa-chevron-circle-right cursorShape" data-bind="click: $root.toggleChildCategories"></i></div>' +
                            '<div class="dd-handle">' +
                            '<span>' + category.name + '</span>' + '<span style="color:red; font-weight: 700;"> (Archive) </span>'+
                            inputElement
                            + '</div></li></ol>';
                    } else {
                        childCategoryHtml = '<ol class="dd-list"> ' +
                            '<li class="dd-item dd-item-list" id="liElement-' + category.id + '"> ' +
                            '<div class="dd-handle-list" ><i class="fa fa-chevron-circle-right cursorShape" data-bind="click: $root.toggleChildCategories"></i></div>' +
                            '<div class="dd-handle">' +
                            '<span>' + category.name + '</span>' +
                            inputElement
                            + '</div></li></ol>';
                    }


                    targetElement.append(childCategoryHtml);

                    ko.applyBindings(viewModel, $("#liElement-" + category.id)[0]);
                },
                // Get Category Id From li
                getCategoryIdFromliElement = function (categoryliElement) {
                    var categoryliId = categoryliElement.id.split("-");
                    if (!categoryliId || categoryliId.length < 2) {
                        return null;
                    }

                    var categoryId = categoryliId[1];
                    if (!categoryId) {
                        return null;
                    }

                    return parseInt(categoryId);
                },
                // Get Category Id from Binding li Element
                getCategoryIdFromElement = function (event) {
                    if (!event || !event.target) {
                        return null;
                    }

                    var categoryliElement = $(event.target).closest('li')[0];
                    if (!categoryliElement) {
                        return null;
                    }

                    return getCategoryIdFromliElement(categoryliElement);
                },
                // Update Input Checked States
                updateInputCheckedStates = function() {
                    $.each($("#productCategoryDialogCategories").find("input:checkbox"), function(index, inputElement) {
                        var categoryliElement = $(inputElement).closest('li')[0];
                        if (categoryliElement) {
                            var categoryId = getCategoryIdFromliElement(categoryliElement);
                            if (categoryId) {
                                var category = viewModel.productCategories.find(function (productCategory) {
                                    return productCategory.id === categoryId;
                                });

                                if (category) {
                                    if (category.isSelected()) {
                                        $(inputElement).prop('checked', true);
                                    }
                                    else {
                                        $(inputElement).prop('checked', false);    
                                    }
                                }
                            }
                        }
                    });
                },


                 // Update Input Checked States for products
                updateInputCheckedStatesForProduct = function () {
                    $.each($("#itemsDialogProducts").find("input:checkbox"), function (index, inputElement) {
                        var categoryliElement = $(inputElement).closest('li')[0];
                        if (categoryliElement) {
                            var categoryId = getCategoryIdFromliElement(categoryliElement);
                            if (categoryId) {
                                var category = viewModel.products.find(function (productCategory) {
                                    return productCategory.id === categoryId;
                                });

                                if (category) {
                                    if (category.isSelected()) {
                                        $(inputElement).prop('checked', true);
                                    }
                                    else {
                                        $(inputElement).prop('checked', false);
                                    }
                                }
                            }
                        }
                    });
                },



                toggleChildCategories = function (event) {

                if (!event) {
                    return false;
                }

                var targetElement = $(event.target);
                if (!targetElement) {
                    return false;
                }

                var childList = targetElement.closest('li').children('ol');
                if (childList.length === 0) {
                    return false;
                }

                // Toggle Child List
                if (childList.is(':hidden')) {
                    childList.show();
                }
                else {
                    childList.hide();
                }

                return true;
                },
                // Show Product Category dialog
                showProductCategoryDialog = function () {
                    $("#productCategoryDialogForDiscountVoucher").modal("show");
                },
                // Hide Product Category dialog
                hideProductCategoryDialog = function () {
                    $("#productCategoryDialogForDiscountVoucher").modal("hide");
                },
                 // Show Product dialog for DV
                showItemDialog = function () {
                    $("#itemsDialogForDiscountVoucher").modal("show");
                },
                // Hide Product dialog for DV
                hideItemDialog = function () {
                    $("#itemsDialogForDiscountVoucher").modal("hide");
                },
                 //Show RealEstateCompaign VariableIcon Dialog
                showVariableIconDialog = function ()
                {
                    $("#editvariableIconsModal").modal("show");
                },

                //#endregion
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
                expandCategory: expandCategory,
                showDiscountVoucherDetailDialog: showDiscountVoucherDetailDialog,
                hideDiscountVoucherDetailDialog: hideDiscountVoucherDetailDialog,
                showProductCategoryDialog: showProductCategoryDialog,
                hideProductCategoryDialog: hideProductCategoryDialog,
                showItemDialog: showItemDialog,
                hideItemDialog: hideItemDialog,
                updateInputCheckedStates: updateInputCheckedStates,
                updateInputCheckedStatesForProduct: updateInputCheckedStatesForProduct,
                toggleChildCategories: toggleChildCategories,
                getCategoryIdFromliElement: getCategoryIdFromliElement,
                getCategoryIdFromElement: getCategoryIdFromElement,
                appendChildCategory: appendChildCategory,
                showCssDialog: showCssDialog,
                hideCssDialog: hideCssDialog,
                //Show RealEstateCompaign VariableIcon Dialog
                showVariableIconDialog: showVariableIconDialog
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