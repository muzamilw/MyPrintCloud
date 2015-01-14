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
                },
                // Hide Activity the dialog
                hideRaveReviewDialog = function () {
                    $("#rave").modal("hide");
                },
                showCompanyTerritoryDialog = function () { //
                    $("#myTerritorySetModal").modal("show");
                },
                // Hide Activity the dialog
                hideCompanyTerritoryDialog = function () {
                    $("#myTerritorySetModal").modal("hide");
                },
                // Show Activity the dialog
                // ReSharper disable once InconsistentNaming
                showCompanyCMYKColorDialog = function () {
                    $("#myCMYKColorModal").modal("show");
                },
                // Hide Activity the dialog
                // ReSharper disable once InconsistentNaming
                hideCompanyCMYKColorDialog = function () {
                    $("#myCMYKColorModal").modal("hide");
                },
                // Show Edit Banner the dialog
                showEditBannerDialog = function () {
                    $("#myEditBannerModal").modal("show");
                },
                // Hide Edit Banner the dialog
                hideEditBannerDialog = function () {
                    $("#myEditBannerModal").modal("hide");
                },
                // Show Addressnthe dialog
                showAddressDialog = function () {
                    $("#myAddressSetModal").modal("show");
                },
                // Hide Address the dialog
                hideAddressDialog = function () {
                    $("#myAddressSetModal").modal("hide");
                },
                // Show Contact Company the dialog
                showCompanyContactDialog = function () {
                    $("#myContactProfileModal").modal("show");
                },
                // Hide Company Contact the dialog
                hideCompanyContactDialog = function () {
                    $("#myContactProfileModal").modal("hide");
                },
                // Show Add Banner Set the dialog
                showSetBannerDialog = function () {
                    $("#mybannerSetModal").modal("show");
                },
                // Hide  Add Banner Set the dialog
                hideSetBannerDialog = function () {
                    $("#mybannerSetModal").modal("hide");
                },
                // Show Secondory Page the dialog
                showSecondoryPageDialog = function () {
                    $("#secondaryPageAddDialog").modal("show");
                },
                // Hide Secondory Page the dialog
                hideSecondoryPageDialog = function () {
                    $("#secondaryPageAddDialog").modal("hide");
                },
                // Show Secondary Page Category the dialog
                showSecondaryPageCategoryDialog = function () {
                    $("#mySecondaryPageCategoryModal").modal("show");
                },
                // Hide Secondary Page Category the dialog
                hideSecondaryPageCategoryDialog = function () {
                    $("#mySecondaryPageCategoryModal").modal("hide");
                },
                // Show Email Camapaign the dialog
                showEmailCamapaignDialog = function () {
                    $("#addEditCampaignEmailModal").modal("show");
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
                // Show Product Category Dialog
                showProductCategoryDialog = function () {
                    $("#myProductCategoryModal").modal("show");
                },
                // Hide Product Category Dialog
                hideProductCategoryDialog = function () {
                    $("#myProductCategoryModal").modal("hide");
                },
                // show Payment Gateway Dialog
                showPaymentGatewayDialog = function () {
                    $("#myPaymentGatewayModal").modal("show");
                },
                // hide Payment Gateway Dialog 
                hidePaymentGatewayDialog = function () {
                    $("#myPaymentGatewayModal").modal("hide");
                },
                
                //#region Store Product Tab Functions
                // Change View - List/Grid View
                changeView = function (element) {
                    var elementId = element.currentTarget.id;
                    if (elementId === "listViewIcon") {
                        if (ist.storeProduct.viewModel.isListViewVisible()) {
                            return;
                        }

                        ist.storeProduct.viewModel.setListViewActive();
                    }
                    else if (elementId === "gridViewIcon") {
                        if (ist.storeProduct.viewModel.isGridViewVisible()) {
                            return;
                        }

                        ist.storeProduct.viewModel.setGridViewActive();
                    }
                },
                // Show Video the dialog
                showVideoDialog = function () {
                    $("#productVideoDialog").modal("show");
                },
                // Hide Video the dialog
                hideVideoDialog = function () {
                    $("#productVideoDialog").modal("hide");
                },
                // Show RelatedItem the dialog
                showRelatedItemDialog = function () {
                    $("#relatedProductDialog").modal("show");
                },
                // Hide RelatedItem the dialog
                hideRelatedItemDialog = function () {
                    $("#relatedProductDialog").modal("hide");
                },
                // Show StockItem the dialog
                showStockItemDialog = function () {
                    $("#stockDialog").modal("show");
                },
                // Hide StockItem the dialog
                hideStockItemDialog = function () {
                    $("#stockDialog").modal("hide");
                },
                // Show ItemAddonCostCentre the dialog
                showItemAddonCostCentreDialog = function () {
                    $("#itemAddonCostCentreDialog").modal("show");
                },
                // Hide ItemAddonCostCentre the dialog
                hideItemAddonCostCentreDialog = function () {
                    $("#itemAddonCostCentreDialog").modal("hide");
                },
                // Initiate Dropzone events 
                initiateDropzoneEvents = function (element, itemId, itemImageType, imageCaption, filePath) {

                    var self = element;
                    self.on("sending", function (file, xhr, formData) {
                        formData.append("itemId", itemId);
                        formData.append("imageFileType", itemImageType);
                    });

                    self.on("removedfile", function () {
                        $.ajax({
                            type: 'post',
                            url: '/Products/Home/DeleteImage?itemId=' + itemId + '&imageFileType=' + itemImageType,
                            success: function () {
                                toastr.success(imageCaption + " removed successfully!");
                            }
                        });
                    });

                    self.on("addedfile", function (file) {
                        var img = $(file.previewTemplate).find("img");
                        img[0].onload = function () {
                            var max = this.width > this.height ? this.width : this.height;
                            var ratio = 100.0 / max;

                            var width = this.width * ratio;
                            var height = this.height * ratio;

                            img.css({
                                width: width + "px",
                                height: height + "px",
                                top: ((100 - height) / 2) + "px",
                                left: ((100 - width) / 2) + "px"
                            });
                        };
                    });

                    if (!filePath) {
                        return;
                    }

                    var mockFile = { name: imageCaption, size: 12345, type: 'image/jpeg' };
                    self.emit('addedfile', mockFile);
                    self.emit('thumbnail', mockFile, filePath);
                },
                // Initialize Dropzones
                initializeDropZones = function () {

                    // Create Dropzone's
                    // "demoUpload1" is the HTML element's ID
                    $("#demoUpload1").dropzone({
                        paramName: "file", // The name that will be used to transfer the file
                        maxFilesize: 1,
                        addRemoveLinks: true,
                        dictRemoveFile: "Delete",
                        init: function () {
                            initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.thumbnail, "Thumbnail",
                                viewModel.selectedProduct().thumbnail());
                        }
                    });

                    // Image Path
                    $("#demoUpload2").dropzone({
                        paramName: "file", // The name that will be used to transfer the file
                        maxFilesize: 1,
                        addRemoveLinks: true,
                        dictRemoveFile: "Delete",
                        init: function () {
                            initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.imagePath, "Image Path",
                                viewModel.selectedProduct().imagePath());
                        }
                    });

                    // Grid Image
                    $("#demoUpload3").dropzone({
                        paramName: "file", // The name that will be used to transfer the file
                        maxFilesize: 1,
                        addRemoveLinks: true,
                        dictRemoveFile: "Delete",
                        init: function () {
                            initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.grid, "Grid",
                                viewModel.selectedProduct().gridImage());
                        }
                    });

                    // Grid Image
                    $("#demoUpload4").dropzone({
                        paramName: "file", // The name that will be used to transfer the file
                        maxFilesize: 1,
                        addRemoveLinks: true,
                        dictRemoveFile: "Delete",
                        init: function () {
                            initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.file1, "File1",
                                viewModel.selectedProduct().file1());
                        }
                    });
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
            // Initialize
            initialize = function () {
                if (!bindingRoot) {
                    return;
                }
            };
            initialize();
            return {
                bindingRoot: bindingRoot,
                showRaveReviewDialog: showRaveReviewDialog,
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
                showProductCategoryDialog: showProductCategoryDialog,
                showCkEditorDialogDialog: showCkEditorDialogDialog,
                hideProductCategoryDialog: hideProductCategoryDialog,
                hideCkEditorDialogDialog: hideCkEditorDialogDialog,
                //#region Store Product Tab Functions
                changeView: changeView,
                showVideoDialog: showVideoDialog,
                hideVideoDialog: hideVideoDialog,
                showRelatedItemDialog: showRelatedItemDialog,
                hideRelatedItemDialog: hideRelatedItemDialog,
                showStockItemDialog: showStockItemDialog,
                hideStockItemDialog: hideStockItemDialog,
                showItemAddonCostCentreDialog: showItemAddonCostCentreDialog,
                hideItemAddonCostCentreDialog: hideItemAddonCostCentreDialog,
                initializeDropZones: initializeDropZones,
                //#endregion
                initializeForm: initializeForm,
                viewModel: viewModel,
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