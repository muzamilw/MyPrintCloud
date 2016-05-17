/*
    View for Phrase Library. Used to keep the viewmodel clear of UI related logic
*/
define("userRole/userRole.view",
    ["jquery", "userRole/userRole.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.userRole.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#userRolesBinding")[0],
                
                showRolesDialog = function () {
                    $("#roleRightsDialog").modal("show");
                },
                
                hideRolesDialog = function () {
                    $("#roleRightsDialog").modal("hide");
                };
           
            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showRolesDialog: showRolesDialog,
                hideRolesDialog: hideRolesDialog
               
            };
        })(ist.userRole.viewModel);

        // Initialize the view model
        if (ist.userRole.view.bindingRoot) {
            ist.userRole.viewModel.initialize(ist.userRole.view);
        }
    });
