/*
    View for Phrase Library. Used to keep the viewmodel clear of UI related logic
*/
define("common/phraseLibrary.view",
    ["jquery", "common/phraseLibrary.viewModel"], function ($) {

        var ist = window.ist || {};
        // View 
        ist.phraseLibrary.view = (function (specifiedViewModel) {
            var // View model 
                viewModel = specifiedViewModel,
                // Binding root used with knockout
                bindingRoot = $("#phraseLibraryDialog")[0],
                     // Show Activity the dialog
                showPhraseLibraryDialog = function () {
                    $("#phraseLibraryDialog").modal("show");
                },
                // Hide Activity the dialog
                hidePhraseLibraryDialog = function () {
                    $("#phraseLibraryDialog").modal("hide");
                };

            return {
                bindingRoot: bindingRoot,
                viewModel: viewModel,
                showPhraseLibraryDialog: showPhraseLibraryDialog,
                hidePhraseLibraryDialog: hidePhraseLibraryDialog,
            };
        })(ist.phraseLibrary.viewModel);

        // Initialize the view model
        if (ist.phraseLibrary.view.bindingRoot) {
            ist.phraseLibrary.viewModel.initialize(ist.phraseLibrary.view);
        }
    });
