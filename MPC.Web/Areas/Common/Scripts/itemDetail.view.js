/*
    View for the Inventory. Used to keep the viewmodel clear of UI related logic
*/
define("common/itemDetail.view",
    ["jquery", "common/itemDetail.viewModel"], function ($, itemDetailViewModel) {

        var ist = window.ist || {};

        // View 
        ist.itemDetail.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#itemDetailSection")[0],
                // Show Sheet Plan Image the dialog
                showSheetPlanImageDialog = function () {
                    $("#sheetPlanModal").modal("show");
                },
                // Show Sheet Plan Image the dialog
                hideSheetPlanImageDialog = function () {
                    $("#sheetPlanModal").modal("show");
                },
                // Show Inks Dialog
                showInksDialog = function () {
                    $("#inkDialogModel").modal("show");
                    initializeLabelPopovers();
                },
                // Hide Inks Dialog
                hideInksDialog = function () {
                    $("#inkDialogModel").modal("hide");
                },
                // Show Cost Centers Quantity the dialog
                showCostCentersQuantityDialog = function () {
                    $("#costCentersQuanity").modal("show");
                },
               // Hide Cost Centers Quantity the dialog
                hideCostCentersQuantityDialog = function () {
                    $("#costCentersQuanity").modal("hide");
                },
                 // Show section Cost Center Dialog Model
                showSectionCostCenterDialogModel = function () {
                    $("#sectionCostCenterDialogModel").modal("show");
                },
                // hide section Cost Center Dialog Model
                hideSectionCostCenterDialogModel = function () {
                    $("#sectionCostCenterDialogModel").modal("hide");
                },
                //Show Estimate Run Wizard Modal
                showEstimateRunWizard = function () {
                    $("#estimateRunWizard").modal('show');
                },
                //Hide Estimate Run Wizard Modal
                hideEstimateRunWizard = function () {
                    $("#estimateRunWizard").modal('hide');
                },
                //Show Attachment Modal
                showAttachmentModal = function () {
                    $("#attachmentModal").modal('show');
                },
                //Hide Attachment Modal
                hideAttachmentModal = function () {
                    $("#attachmentModal").modal('hide');
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
                    //setTimeout(function () {
                    //    window.scrollTo($(element).offset().left, $(element).offset().top - 50);
                    //    // Focus on element
                    //    $(element).focus();
                    //}, 1000);
                    var target = $(element);
                    target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                    if (target.length) {
                        $('html,body').animate({
                            scrollTop: (target.offset().top - 50)
                        }, 1000);
                        return false;
                    }
                },
                goToValidationSummary= function() {
                    var target = $('#validationSummary');
                    target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                    if (target.length) {
                        $('html,body').animate({
                            scrollTop: (target.offset().top - 50)
                        }, 1000);
                        function blinker() {
                            $('#validationSummary').fadeOut(500);
                            $('#validationSummary').fadeIn(500);
                        }

                        setTimeout(blinker, 1000);
                        return false;
                    }
                },
                // Initialize Label Popovers
                initializeLabelPopovers = function () {
                    // ReSharper disable UnknownCssClass
                    $('.bs-example-tooltips a').popover();
                    // ReSharper restore UnknownCssClass
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
                viewModel: viewModel,
                showSheetPlanImageDialog: showSheetPlanImageDialog,
                hideSheetPlanImageDialog: hideSheetPlanImageDialog,
                showInksDialog: showInksDialog,
                hideInksDialog: hideInksDialog,
                showCostCentersQuantityDialog: showCostCentersQuantityDialog,
                hideCostCentersQuantityDialog: hideCostCentersQuantityDialog,
                showSectionCostCenterDialogModel: showSectionCostCenterDialogModel,
                hideSectionCostCenterDialogModel: hideSectionCostCenterDialogModel,
                gotoElement: gotoElement,
                showEstimateRunWizard: showEstimateRunWizard,
                hideEstimateRunWizard: hideEstimateRunWizard,
                showAttachmentModal: showAttachmentModal,
                hideAttachmentModal: hideAttachmentModal,
                initializeLabelPopovers: initializeLabelPopovers,
                goToValidationSummary: goToValidationSummary
            };
        })(itemDetailViewModel);

        // Initialize the view model
        if (ist.itemDetail.view.bindingRoot) {
            itemDetailViewModel.initialize(ist.itemDetail.view);
        }
        return ist.itemDetail.view;
    });