/*
    View for the Product. Used to keep the viewmodel clear of UI related logic
*/
define("product/product.view",
    ["jquery", "product/product.viewModel"], function ($, productViewModel) {

        var ist = window.ist || {};

        // View 
        ist.product.view = (function (specifiedViewModel) {
            var
                // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#productBinding")[0],
                // Change View - List/Grid View
                changeView = function (element) {
                    var elementId = element.currentTarget.id;
                    if (elementId === "listViewIcon") {
                        if (viewModel.isListViewVisible()) {
                            return;
                        }

                        viewModel.setListViewActive();
                    }
                    else if (elementId === "gridViewIcon") {
                        if (viewModel.isGridViewVisible()) {
                            return;
                        }

                        viewModel.setGridViewActive();
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

                    self.on("addedfile", function(file) {
                        var img = $(file.previewTemplate).find("img");
                        img[0].onload = function() {
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
                        init: function() {
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
                        init: function() {
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
                        init: function() {
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
                        init: function() {
                            initiateDropzoneEvents(this, viewModel.selectedProduct().id(), viewModel.itemFileTypes.file1, "File1",
                                viewModel.selectedProduct().file1());
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
                viewModel: viewModel,
                changeView: changeView,
                showVideoDialog: showVideoDialog,
                hideVideoDialog: hideVideoDialog,
                showRelatedItemDialog: showRelatedItemDialog,
                hideRelatedItemDialog: hideRelatedItemDialog,
                showStockItemDialog: showStockItemDialog,
                hideStockItemDialog: hideStockItemDialog,
                showItemAddonCostCentreDialog: showItemAddonCostCentreDialog,
                hideItemAddonCostCentreDialog: hideItemAddonCostCentreDialog,
                initializeDropZones: initializeDropZones
            };
        })(productViewModel);

        // Initialize the view model
        if (ist.product.view.bindingRoot) {
            productViewModel.initialize(ist.product.view);
        }
        return ist.product.view;
    });
