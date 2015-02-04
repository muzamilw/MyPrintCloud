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
                showCompanyTerritoryDialog = function () { 
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
                // Show Product Category dialog
                showStoreProductCategoryDialog = function () {
                    $("#myProductCategoryModal").modal("show");
                },
                // Hide Product Category dialog
                hideStoreProductCategoryDialog = function () {
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
                // show item sFor Widgets Dialog
                showItemsForWidgetsDialog = function () {
                    $("#itemsForWidgetsDialog").modal("show");
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
                showCkEditorDialogDialog: showCkEditorDialogDialog,
                hideCkEditorDialogDialog: hideCkEditorDialogDialog,
                showItemsForWidgetsDialog: showItemsForWidgetsDialog,
                hideItemsForWidgetsDialog: hideItemsForWidgetsDialog,
                showStoreProductCategoryDialog: showStoreProductCategoryDialog,
                hideStoreProductCategoryDialog: hideStoreProductCategoryDialog,
                showMediaGalleryDialog: showMediaGalleryDialog,
                hideMediaGalleryDialog: hideMediaGalleryDialog,
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