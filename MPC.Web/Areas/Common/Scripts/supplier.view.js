/*
    View for Supplier. Used to keep the viewmodel clear of UI related logic
*/
define("common/supplier.view",
    ["jquery", "common/supplier.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.supplier.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#supplierDialog")[0],
                 initializeForm = function () {
                     // Initialize Forms - For File Upload
                     $("#fileUploadForm").ajaxForm({
                         success: function () {
                             //toastr.success("Uploading completed");
                         },
                         dataType: "json",
                         error: function () {
                             //toastr.success("Uploading completed");
                         }
                     });
                 },
                saveImage = function () {
                    $("#fileUploadForm").submit();
                },

                // Show Activity the dialog
                showSupplierDialog = function () {
                    $("#supplierDialog").modal("show");
                },
                // Hide Activity the dialog
                hideSupplierDialog = function () {
                    $("#supplierDialog").modal("hide");
                };

            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showSupplierDialog: showSupplierDialog,
                hideSupplierDialog: hideSupplierDialog,
                saveImage: saveImage,
                initializeForm: initializeForm
            };
        })(ist.supplier.viewModel);

        // Initialize the view model
        if (ist.supplier.view.bindingRoot) {
            ist.supplier.viewModel.initialize(ist.supplier.view);
        }
    });

// Reads File - Print Out Section
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image;
            img.onload = function () {

                $('#companyLogo')
                    .attr('src', e.target.result);
            };
            img.src = reader.result;

        };
        reader.readAsDataURL(input.files[0]);
    }
}