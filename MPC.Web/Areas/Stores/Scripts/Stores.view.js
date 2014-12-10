/*
    View for the tariff Type. Used to keep the viewmodel clear of UI related logic
*/
define("stores/stores.view",
    ["jquery", "stores/stores.viewModel"], function ($, storesViewModel) {

        var ist = window.ist || {};

        // View 
        ist.stores.view = (function (specifiedViewModel) {
            var
                // View model 
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
                showCompanyTerritoryDialog = function () {//
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
                showCompanyTerritoryDialog: showCompanyTerritoryDialog,
                hideCompanyTerritoryDialog: hideCompanyTerritoryDialog,
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