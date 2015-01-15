define(["ko", "underscore", "underscore-ko"], function (ko) {
    var

    //Section view Entity
    Section = function (specifiedSectionId, specifiedName, specifiedParentId) {
        var
            self,
            //Unique ID
            sectionId = ko.observable(specifiedSectionId),
            //Name
            name = ko.observable(specifiedName),
            //parent Id
            parentId = ko.observable(specifiedParentId),
            //Is Expanded
            isExpanded = ko.observable(false),
            //phrases Fields
            phrasesFields = ko.observableArray([]),

            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.SectionId = source.sectionId();
                result.PhrasesFields = [];
                return result;
            };
        self = {
            sectionId: sectionId,
            isExpanded: isExpanded,
            name: name,
            parentId: parentId,
            phrasesFields: phrasesFields,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    Section.Create = function (source) {
        return new Section(source.SectionId, source.SectionName, source.ParentId);
    }
    //Phrase Field Entity
    PhraseField = function (specifiedFieldId, specifiedFieldName, specifiedSectionId) {
        var
            self,
            //Unique ID
            fieldId = ko.observable(specifiedFieldId),
            //Field Name
            fieldName = ko.observable(specifiedFieldName),
            //Section Id
            sectionId = ko.observable(specifiedSectionId),
            //Phrases
            phrases = ko.observableArray([]),
             // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                fieldName: fieldName,
            }),
             // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
           //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.FieldId = source.fieldId();
                result.FieldName = source.fieldName();
                result.Phrases = [];
                return result;
            };
        self = {
            fieldId: fieldId,
            fieldName: fieldName,
            sectionId: sectionId,
            phrases: phrases,
            convertToServerData: convertToServerData,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
        };
        return self;
    };
    PhraseField.Create = function (source) {
        return new PhraseField(source.FieldId, source.FieldName, source.SectionId);
    }
    //Phrase Entity
    Phrase = function (specifiedPhraseId, specifiedPhrase1, specifiedFieldId) {
        var
            self,
            //Unique ID
            phraseId = ko.observable(specifiedPhraseId),
            //Field Text
            phraseText = ko.observable(specifiedPhrase1),
            //Field Id
            fieldId = ko.observable(specifiedFieldId),
            //Flag For deleted phrase
            isDeleted = ko.observable(false),
           //Is phrase checkbox is checked
           isPhraseChecked = ko.observable(false),

             // Errors
            errors = ko.validation.group({
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                phraseId: phraseId,
                phraseText: phraseText,
                fieldId: fieldId,
                isDeleted: isDeleted,
            }),
             // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),

            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.PhraseId = source.phraseId();
                result.Phrase1 = source.phraseText();
                result.FieldId = source.fieldId();
                result.IsDeleted = source.isDeleted();
                return result;
            };
        self = {
            phraseId: phraseId,
            phraseText: phraseText,
            fieldId: fieldId,
            isDeleted: isDeleted,
            convertToServerData: convertToServerData,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            isPhraseChecked: isPhraseChecked,
        };
        return self;
    };
    Phrase.Create = function (source) {
        return new Phrase(source.PhraseId, source.Phrase1, source.FieldId);
    }

    //Phrase Library Save Model Entity
    PhraseLibrarySaveModel = function () {
        var
            self,
               //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.Sections = [];
                return result;
            };
        self = {
            convertToServerData: convertToServerData,
        };
        return self;
    };

    return {
        Section: Section,
        PhraseField: PhraseField,
        Phrase: Phrase,
        PhraseLibrarySaveModel: PhraseLibrarySaveModel,
    };
});