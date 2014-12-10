/*
    View for the My Organization. Used to keep the viewmodel clear of UI related logic
*/
define("myOrganization/myOrganization.view",
    ["jquery", "myOrganization/myOrganization.viewModel"], function ($, myOrganizationViewModel) {

        var ist = window.ist || {};

        // View 
        ist.myOrganization.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#myOrganizationBinding")[0],
                 initializeForm = function () {
                     // Initialize Forms - For File Upload
                     $("#fileUploadForm").ajaxForm({
                         success: function () {
                             //status("Uploading completed");
                             //progressPercentage(uploadCompletedPercentage + "%");
                             //processingId = data.DocumentFileKey;
                             //requestProcessingStatus();
                             toastr.success("Uploading completed");
                             // viewModel.addVehicleItem().logo(undefined);
                         },
                         dataType: "json",
                         error: function () {
                             //status("Uploading failed. Try again. (Error: " + xhr.statusText + " [" + xhr.status + "])");
                             //showInputArea(true);
                             //showProgressArea(false);
                             //progressPercentage("0%");
                             //alert(status());
                             // toastr.error("Uploading failed. Try again.");
                             toastr.success("Uploading completed");
                         }
                     });
                 },
                // Initialize
                initialize = function () {
                    if (!bindingRoot) {
                        return;
                    }

                    // Handle Sorting
                    // handleSorting("tariffTypeTable", viewModel.sortOn, viewModel.sortIsAsc, viewModel.getTariffType);
                };
            initialize();
            return {
                bindingRoot: bindingRoot,
                initializeForm: initializeForm,
                viewModel: viewModel
            };
        })(myOrganizationViewModel);

        // Initialize the view model
        if (ist.myOrganization.view.bindingRoot) {
            myOrganizationViewModel.initialize(ist.myOrganization.view);
        }
        return ist.myOrganization.view;
    });

// Reads File - Print Out Section
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image;
            img.onload = function () {

                $('#orgImage')
                    .attr('src', e.target.result);
                if (ist.myOrganization.viewModel.selectedMyOrganization().id() !== undefined) {
                    $('#orgImageSubmitBtn').attr('disabled', false);
                }
            };
            img.src = reader.result;

        };
        reader.readAsDataURL(input.files[0]);
    }
}