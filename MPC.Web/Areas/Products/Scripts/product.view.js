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
                // Initialize Dropzones
                initializeDropZones = function() {
                    // Create Dropzone's
                    // "demoUpload1" is the HTML element's ID
                    $("#demoUpload1").dropzone({
                        paramName: "file", // The name that will be used to transfer the file
                        maxFilesize: 1,
                        addRemoveLinks: true,
                        dictRemoveFile: "Delete",
                        init: function () {
                            
                            this.on("sending", function (file, xhr, formData) {
                                formData.append("itemId", viewModel.selectedProduct().id());
                                formData.append("imageFileType", viewModel.itemFileTypes.thumbnail);
                            });

                            this.on("removedfile", function (file) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Products/Home/DeleteImage?imageId=' + viewModel.selectedProduct().id() + '&imageFileType=1',
                                    success: function () {
                                        toastr.success("Thumbnail removed successfully!");
                                    }
                                });
                            });
                              
                            var mockFile = { name: "thumbnail", size: 12345, type: 'image/jpeg' };
                            this.emit('addedfile', mockFile);
                            this.emit('thumbnail', mockFile, viewModel.selectedProduct().thumbnail());
                        }
                    });

                    $("#demoUpload2").dropzone({
                        paramName: "file", // The name that will be used to transfer the file
                        maxFilesize: 1,
                        addRemoveLinks: true,
                        dictRemoveFile: "Delete",
                        sending: function (file, xhr, formData) {
                            formData.append("itemId", viewModel.selectedProduct().id());
                            formData.append("imageFileType", viewModel.itemFileTypes.grid);
                        },
                        init: function () {
                            var mockFile = { name: "grid", size: 12345, type: 'image/jpeg' };
                            this.addFile.call(this, mockFile);
                            this.options.thumbnail.call(this, mockFile, viewModel.selectedProduct().gridImage());
                        }
                    });

                    $("#demoUpload3").dropzone({
                        paramName: "file", // The name that will be used to transfer the file
                        maxFilesize: 1,
                        addRemoveLinks: true,
                        dictRemoveFile: "Delete",
                        sending: function (file, xhr, formData) {
                            formData.append("itemId", viewModel.selectedProduct().id());
                            formData.append("imageFileType", viewModel.itemFileTypes.imagePath);
                        },
                        init: function () {
                            var mockFile = { name: "imagePath", size: 12345, type: 'image/jpeg' };
                            this.addFile.call(this, mockFile);
                            this.options.thumbnail.call(this, mockFile, viewModel.selectedProduct().imagePath());
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
                initializeDropZones: initializeDropZones
            };
        })(productViewModel);

        // Initialize the view model
        if (ist.product.view.bindingRoot) {
            productViewModel.initialize(ist.product.view);
        }
        return ist.product.view;
    });
