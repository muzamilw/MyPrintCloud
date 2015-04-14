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
                     
                 },
                // Initialize Label Popovers
                initializeLabelPopovers = function () {
                    // ReSharper disable UnknownCssClass
                    $('.bs-example-tooltips a').popover();
                    // ReSharper restore UnknownCssClass
                },
                saveImage = function () {
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
                gotoElement: gotoElement,
                showSupplierDialog: showSupplierDialog,
                hideSupplierDialog: hideSupplierDialog,
                saveImage: saveImage,
                initializeForm: initializeForm,
                initializeLabelPopovers: initializeLabelPopovers
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