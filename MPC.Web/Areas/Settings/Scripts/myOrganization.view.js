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