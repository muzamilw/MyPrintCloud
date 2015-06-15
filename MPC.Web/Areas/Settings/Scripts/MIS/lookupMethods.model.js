define(["ko", "underscore", "underscore-ko"], function (ko) {

    ko.bindingHandlers.autoNumeric = {
        init: function (el, valueAccessor, bindingsAccessor, viewModel) {
            var $el = $(el),
              bindings = bindingsAccessor(),
              settings = bindings.settings,
              value = valueAccessor();

            $el.autoNumeric(settings);
            $el.autoNumeric('set', parseFloat(ko.utils.unwrapObservable(value()), 10));
            $el.change(function () {
                value(parseFloat($el.autoNumeric('get'), 10));
            });
        },
        update: function (el, valueAccessor, bindingsAccessor, viewModel) {
            var $el = $(el),
              newValue = ko.utils.unwrapObservable(valueAccessor()),
              elementValue = $el.autoNumeric('get'),
              valueHasChanged = (newValue != elementValue);

            if ((newValue === 0) && (elementValue !== 0) && (elementValue !== "0")) {
                valueHasChanged = true;
            }

            if (valueHasChanged) {
                if (newValue != undefined) {
                    $el.autoNumeric('set', newValue);
                }


            }
        }
    };

    var lookupMethod = function () {
        var self
        MethodId = ko.observable(),
        Name = ko.observable(),
        Type = ko.observable(),
        LockedBy = ko.observable(),
        OrganisationId = ko.observable(),
        FlagId = ko.observable(),
        SystemSiteId = ko.observable(),
         errors = ko.validation.group({
         }),
          isValid = ko.computed(function () {
              return errors().length === 0;
          }),
          dirtyFlag = new ko.dirtyFlag({


          }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };
        self = {
            MethodId: MethodId,
            Name: Name,
            Type: Type,
            LockedBy: LockedBy,
            OrganisationId: OrganisationId,
            FlagId: FlagId,
            SystemSiteId: SystemSiteId,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset

        };
        return self;

    };

    var ClickChargeLookup = function (source) {
        var self
        if (source != undefined) {
            Id = source.Id,
            MethodId = source.MethodId,
            SheetCost = ko.observable(source.SheetCost),
            Sheets = ko.observable(source.Sheets),
            SheetPrice = ko.observable(source.SheetPrice),
            TimePerHour = ko.observable(source.TimePerHour)
        } else {
            Id = null,
            MethodId = null,
            SheetCost = ko.observable(),
            Sheets = ko.observable(),
            SheetPrice = ko.observable(),
            TimePerHour = ko.observable()
        }


        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           SheetCost: SheetCost,
           Sheets: Sheets,
           SheetPrice: SheetPrice,
           TimePerHour: TimePerHour

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id: Id,
            MethodId: MethodId,
            Sheets: Sheets,
            SheetCost: SheetCost,
            SheetPrice: SheetPrice,
            TimePerHour: TimePerHour,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    var ClickChargeZone = function (source) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
            MethodId = ko.observable(source.MethodId),
            From1 = ko.observable(source.From1),
            To1 = ko.observable(source.To1),
            Sheets1 = ko.observable(source.Sheets1),
            SheetCost1 = ko.observable(source.SheetCost1),
            SheetPrice1 = ko.observable(source.SheetPrice1),
            From2 = ko.observable(source.From2),
            To2 = ko.observable(source.To2).extend({number:true}),
            Sheets2 = ko.observable(source.Sheets2),
            SheetCost2 = ko.observable(source.SheetCost2),
            SheetPrice2 = ko.observable(source.SheetPrice2),
            From3 = ko.observable(source.From3),
            To3 = ko.observable(source.To3),
            Sheets3 = ko.observable(source.Sheets3),
            SheetCost3 = ko.observable(source.SheetCost3),
            SheetPrice3 = ko.observable(source.SheetPrice3),
            From4 = ko.observable(source.From4),
            To4 = ko.observable(source.To4),
            Sheets4 = ko.observable(source.Sheets4),
            SheetCost4 = ko.observable(source.SheetCost4),
            SheetPrice4 = ko.observable(source.SheetPrice4),
            From5 = ko.observable(source.From5),
            To5 = ko.observable(source.To5),
            Sheets5 = ko.observable(source.Sheets5),
            SheetCost5 = ko.observable(source.SheetCost5),
            SheetPrice5 = ko.observable(source.SheetPrice5),
            From6 = ko.observable(source.From6),
            To6 = ko.observable(source.To6),
            Sheets6 = ko.observable(source.Sheets6),
            SheetCost6 = ko.observable(source.SheetCost6),
            SheetPrice6 = ko.observable(source.SheetPrice6),
            From7 = ko.observable(source.From7),
            To7 = ko.observable(source.To7),
            Sheets7 = ko.observable(source.Sheets7),
            SheetCost7 = ko.observable(source.SheetCost7),
            SheetPrice7 = ko.observable(source.SheetPrice7),
            From8 = ko.observable(source.From8),
            To8 = ko.observable(source.To8),
            Sheets8 = ko.observable(source.Sheets8),
            SheetCost8 = ko.observable(source.SheetCost8),
            SheetPrice8 = ko.observable(source.SheetPrice8),
            From9 = ko.observable(source.From9),
            To9 = ko.observable(source.To9),
            Sheets9 = ko.observable(source.Sheets9),
            SheetCost9 = ko.observable(source.SheetCost9),
            SheetPrice9 = ko.observable(source.SheetPrice9),
            From10 = ko.observable(source.From10),
            To10 = ko.observable(source.To10),
            Sheets10 = ko.observable(source.Sheets10),
            SheetCost10 = ko.observable(source.SheetCost10),
            SheetPrice10 = ko.observable(source.SheetPrice10),
            From11 = ko.observable(source.From11),
            To11 = ko.observable(source.To11),
            Sheets11 = ko.observable(source.Sheets11),
            SheetCost11 = ko.observable(source.SheetCost11),
            SheetPrice11 = ko.observable(source.SheetPrice11),
            From12 = ko.observable(source.From12),
            To12 = ko.observable(source.To12),
            Sheets12 = ko.observable(source.Sheets12),
            SheetCost12 = ko.observable(source.SheetCost12),
            SheetPrice12 = ko.observable(source.SheetPrice12),
            From13 = ko.observable(source.From13),
            To13 = ko.observable(source.To13),
            Sheets13 = ko.observable(source.Sheets13),
            SheetCost13 = ko.observable(source.SheetCost13),
            SheetPrice13 = ko.observable(source.SheetPrice13),
            From14 = ko.observable(source.From14),
            To14 = ko.observable(source.To14),
            Sheets14 = ko.observable(source.Sheets14),
            SheetCost14 = ko.observable(source.SheetCost14),
            SheetPrice14 = ko.observable(source.SheetPrice14),
            From15 = ko.observable(source.From15),
            To15 = ko.observable(source.To15),
            Sheets15 = ko.observable(source.Sheets15),
            SheetCost15 = ko.observable(source.SheetCost15),
            SheetPrice15 = ko.observable(source.SheetPrice15),
            isaccumulativecharge = ko.observable(source.isaccumulativecharge),
            IsRoundUp = ko.observable(source.IsRoundUp),
            TimePerHour = ko.observable(source.TimePerHour)
        } else {
            Id = ko.observable(),
            MethodId = ko.observable(),
            From1 = ko.observable(0),
            To1 = ko.observable(),
            Sheets1 = ko.observable(),
            SheetCost1 = ko.observable(),
            SheetPrice1 = ko.observable(),
            From2 = ko.observable(),
            To2 = ko.observable(),
            Sheets2 = ko.observable(),
            SheetCost2 = ko.observable(),
            SheetPrice2 = ko.observable(),
            From3 = ko.observable(),
            To3 = ko.observable(),
            Sheets3 = ko.observable(),
            SheetCost3 = ko.observable(),
            SheetPrice3 = ko.observable(),
            From4 = ko.observable(),
            To4 = ko.observable(),
            Sheets4 = ko.observable(),
            SheetCost4 = ko.observable(),
            SheetPrice4 = ko.observable(),
            From5 = ko.observable(),
            To5 = ko.observable(),
            Sheets5 = ko.observable(),
            SheetCost5 = ko.observable(),
            SheetPrice5 = ko.observable(),
            From6 = ko.observable(),
            To6 = ko.observable(),
            Sheets6 = ko.observable(),
            SheetCost6 = ko.observable(),
            SheetPrice6 = ko.observable(),
            From7 = ko.observable(),
            To7 = ko.observable(),
            Sheets7 = ko.observable(),
            SheetCost7 = ko.observable(),
            SheetPrice7 = ko.observable(),
            From8 = ko.observable(),
            To8 = ko.observable(),
            Sheets8 = ko.observable(),
            SheetCost8 = ko.observable(),
            SheetPrice8 = ko.observable(),
            From9 = ko.observable(),
            To9 = ko.observable(),
            Sheets9 = ko.observable(),
            SheetCost9 = ko.observable(),
            SheetPrice9 = ko.observable(),
            From10 = ko.observable(),
            To10 = ko.observable(),
            Sheets10 = ko.observable(),
            SheetCost10 = ko.observable(),
            SheetPrice10 = ko.observable(),
            From11 = ko.observable(),
            To11 = ko.observable(),
            Sheets11 = ko.observable(),
            SheetCost11 = ko.observable(),
            SheetPrice11 = ko.observable(),
            From12 = ko.observable(),
            To12 = ko.observable(),
            Sheets12 = ko.observable(),
            SheetCost12 = ko.observable(),
            SheetPrice12 = ko.observable(),
            From13 = ko.observable(),
            To13 = ko.observable(),
            Sheets13 = ko.observable(),
            SheetCost13 = ko.observable(),
            SheetPrice13 = ko.observable(),
            From14 = ko.observable(),
            To14 = ko.observable(),
            Sheets14 = ko.observable(),
            SheetCost14 = ko.observable(),
            SheetPrice14 = ko.observable(),
            From15 = ko.observable(),
            To15 = ko.observable(),
            Sheets15 = ko.observable(),
            SheetCost15 = ko.observable(),
            SheetPrice15 = ko.observable(),
            isaccumulativecharge = ko.observable(),
            IsRoundUp = ko.observable(),
            TimePerHour = ko.observable()
        }


        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           Id: Id,
           MethodId: MethodId,
           From1: From1,
           To1: To1,
           Sheets1: Sheets1,
           SheetCost1: SheetCost1,
           SheetPrice1: SheetPrice1,
           From2: From2,
           To2: To2,
           Sheets2: Sheets2,
           SheetCost2: SheetCost2,
           SheetPrice2: SheetPrice2,
           From3: From3,
           To3: To3,
           Sheets3: Sheets3,
           SheetCost3: SheetCost3,
           SheetPrice3: SheetPrice3,
           From4: From4,
           To4: To4,
           Sheets4: Sheets4,
           SheetCost4: SheetCost4,
           SheetPrice4: SheetPrice4,
           From5: From5,
           To5: To5,
           Sheets5: Sheets5,
           SheetCost5: SheetCost5,
           SheetPrice5: SheetPrice5,
           From6: From6,
           To6: To6,
           Sheets6: Sheets6,
           SheetCost6: SheetCost6,
           SheetPrice6: SheetPrice6,
           From7: From7,
           To7: To7,
           Sheets7: Sheets7,
           SheetCost7: SheetCost7,
           SheetPrice7: SheetPrice7,
           From8: From8,
           To8: To8,
           Sheets8: Sheets8,
           SheetCost8: SheetCost8,
           SheetPrice8: SheetPrice8,
           From9: From9,
           To9: To9,
           Sheets9: Sheets9,
           SheetCost9: SheetCost9,
           SheetPrice9: SheetPrice9,
           From10: From10,
           To10: To10,
           Sheets10: Sheets10,
           SheetCost10: SheetCost10,
           SheetPrice10: SheetPrice10,
           From11: From11,
           To11: To11,
           Sheets11: Sheets11,
           SheetCost11: SheetCost11,
           SheetPrice11: SheetPrice11,
           From12: From12,
           To12: To12,
           Sheets12: Sheets12,
           SheetCost12: SheetCost12,
           SheetPrice12: SheetPrice12,
           From13: From13,
           To13: To13,
           Sheets13: Sheets13,
           SheetCost13: SheetCost13,
           SheetPrice13: SheetPrice13,
           From14: From14,
           To14: To14,
           Sheets14: Sheets14,
           SheetCost14: SheetCost14,
           SheetPrice14: SheetPrice14,
           From15: From15,
           To15: To15,
           Sheets15: Sheets15,
           SheetCost15: SheetCost15,
           SheetPrice15: SheetPrice15,
           isaccumulativecharge: isaccumulativecharge,
           IsRoundUp: IsRoundUp,
           TimePerHour: TimePerHour

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id: Id,
            MethodId: MethodId,
            From1: From1,
            To1: To1,
            Sheets1: Sheets1,
            SheetCost1: SheetCost1,
            SheetPrice1: SheetPrice1,
            From2: From2,
            To2: To2,
            Sheets2: Sheets2,
            SheetCost2: SheetCost2,
            SheetPrice2: SheetPrice2,
            From3: From3,
            To3: To3,
            Sheets3: Sheets3,
            SheetCost3: SheetCost3,
            SheetPrice3: SheetPrice3,
            From4: From4,
            To4: To4,
            Sheets4: Sheets4,
            SheetCost4: SheetCost4,
            SheetPrice4: SheetPrice4,
            From5: From5,
            To5: To5,
            Sheets5: Sheets5,
            SheetCost5: SheetCost5,
            SheetPrice5: SheetPrice5,
            From6: From6,
            To6: To6,
            Sheets6: Sheets6,
            SheetCost6: SheetCost6,
            SheetPrice6: SheetPrice6,
            From7: From7,
            To7: To7,
            Sheets7: Sheets7,
            SheetCost7: SheetCost7,
            SheetPrice7: SheetPrice7,
            From8: From8,
            To8: To8,
            Sheets8: Sheets8,
            SheetCost8: SheetCost8,
            SheetPrice8: SheetPrice8,
            From9: From9,
            To9: To9,
            Sheets9: Sheets9,
            SheetCost9: SheetCost9,
            SheetPrice9: SheetPrice9,
            From10: From10,
            To10: To10,
            Sheets10: Sheets10,
            SheetCost10: SheetCost10,
            SheetPrice10: SheetPrice10,
            From11: From11,
            To11: To11,
            Sheets11: Sheets11,
            SheetCost11: SheetCost11,
            SheetPrice11: SheetPrice11,
            From12: From12,
            To12: To12,
            Sheets12: Sheets12,
            SheetCost12: SheetCost12,
            SheetPrice12: SheetPrice12,
            From13: From13,
            To13: To13,
            Sheets13: Sheets13,
            SheetCost13: SheetCost13,
            SheetPrice13: SheetPrice13,
            From14: From14,
            To14: To14,
            Sheets14: Sheets14,
            SheetCost14: SheetCost14,
            SheetPrice14: SheetPrice14,
            From15: From15,
            To15: To15,
            Sheets15: Sheets15,
            SheetCost15: SheetCost15,
            SheetPrice15: SheetPrice15,
            isaccumulativecharge: isaccumulativecharge,
            IsRoundUp: IsRoundUp,
            TimePerHour: TimePerHour,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    var GuillotineCalc = function (source, sourcePTV) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
            MethodId = ko.observable(source.MethodId),
            PaperWeight1 = ko.observable(source.PaperWeight1),
            PaperThroatQty1 = ko.observable(source.PaperThroatQty1),
            PaperWeight2 = ko.observable(source.PaperWeight2),
            PaperThroatQty2 = ko.observable(source.PaperThroatQty2),
            PaperWeight3 = ko.observable(source.PaperWeight3),
            PaperThroatQty3 = ko.observable(source.PaperThroatQty3),
            PaperWeight4 = ko.observable(source.PaperWeight4),
            PaperThroatQty4 = ko.observable(source.PaperThroatQty4),
            PaperWeight5 = ko.observable(source.PaperWeight5),
            PaperThroatQty5 = ko.observable(source.PaperThroatQty5),
            GuillotinePTVList = ko.observableArray([])
            //GuillotinePTVList.removeAll();
            if (sourcePTV != null) {
                _.each(sourcePTV, function (item) {
                    GuillotinePTVList.push(GuillotineClickPTV(item));
                });

            }


        } else {
            Id = ko.observable(),
            MethodId = ko.observable(),
            PaperWeight1 = ko.observable(),
            PaperThroatQty1 = ko.observable(),
            PaperWeight2 = ko.observable(),
            PaperThroatQty2 = ko.observable(),
            PaperWeight3 = ko.observable(),
            PaperThroatQty3 = ko.observable(),
            PaperWeight4 = ko.observable(),
            PaperThroatQty4 = ko.observable(),
            PaperWeight5 = ko.observable(),
            PaperThroatQty5 = ko.observable(),
            GuillotinePTVList = ko.observableArray([])
        }

        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           Id: Id,
           MethodId: MethodId,
           PaperWeight1: PaperWeight1,
           PaperThroatQty1: PaperThroatQty1,
           PaperWeight2: PaperWeight2,
           PaperThroatQty2: PaperThroatQty2,
           PaperWeight3: PaperWeight3,
           PaperThroatQty3: PaperThroatQty3,
           PaperWeight4: PaperWeight4,
           PaperThroatQty4: PaperThroatQty4,
           PaperWeight5: PaperWeight5,
           PaperThroatQty5: PaperThroatQty5,
           GuillotinePTVList: GuillotinePTVList

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id: Id,
            MethodId: MethodId,
            PaperWeight1: PaperWeight1,
            PaperThroatQty1: PaperThroatQty1,
            PaperWeight2: PaperWeight2,
            PaperThroatQty2: PaperThroatQty2,
            PaperWeight3: PaperWeight3,
            PaperThroatQty3: PaperThroatQty3,
            PaperWeight4: PaperWeight4,
            PaperThroatQty4: PaperThroatQty4,
            PaperWeight5: PaperWeight5,
            PaperThroatQty5: PaperThroatQty5,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            GuillotinePTVList: GuillotinePTVList

        };
        return self;

    }
    var MeterPerHourLookup = function (source) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
            MethodId = ko.observable(source.MethodId),
            SheetsQty1 = ko.observable(source.SheetsQty1),
            SheetsQty2 = ko.observable(source.SheetsQty2),
            SheetsQty3 = ko.observable(source.SheetsQty3),
            SheetsQty4 = ko.observable(source.SheetsQty4),
            SheetsQty5 = ko.observable(source.SheetsQty5),
            SheetWeight1 = ko.observable(source.SheetWeight1),
            speedqty11 = ko.observable(source.speedqty11),
            speedqty12 = ko.observable(source.speedqty12),
            speedqty13 = ko.observable(source.speedqty13),
            speedqty14 = ko.observable(source.speedqty14),
            speedqty15 = ko.observable(source.speedqty15),
            SheetWeight2 = ko.observable(source.SheetWeight2),
            speedqty21 = ko.observable(source.speedqty21),
            speedqty22 = ko.observable(source.speedqty22),
            speedqty23 = ko.observable(source.speedqty23),
            speedqty24 = ko.observable(source.speedqty24),
            speedqty25 = ko.observable(source.speedqty25),
            SheetWeight3 = ko.observable(source.SheetWeight3),
            speedqty31 = ko.observable(source.speedqty31),
            speedqty32 = ko.observable(source.speedqty32),
            speedqty33 = ko.observable(source.speedqty33),
            speedqty34 = ko.observable(source.speedqty34),
            speedqty35 = ko.observable(source.speedqty35),
            hourlyCost = ko.observable(source.hourlyCost),
            hourlyPrice = ko.observable(source.hourlyPrice)
        } else {
            Id = ko.observable(),
            MethodId = ko.observable(),
            SheetsQty1 = ko.observable(),
            SheetsQty2 = ko.observable(),
            SheetsQty3 = ko.observable(),
            SheetsQty4 = ko.observable(),
            SheetsQty5 = ko.observable(),
            SheetWeight1 = ko.observable(),
            speedqty11 = ko.observable(),
            speedqty12 = ko.observable(),
            speedqty13 = ko.observable(),
            speedqty14 = ko.observable(),
            speedqty15 = ko.observable(),
            SheetWeight2 = ko.observable(),
            speedqty21 = ko.observable(),
            speedqty22 = ko.observable(),
            speedqty23 = ko.observable(),
            speedqty24 = ko.observable(),
            speedqty25 = ko.observable(),
            SheetWeight3 = ko.observable(),
            speedqty31 = ko.observable(),
            speedqty32 = ko.observable(),
            speedqty33 = ko.observable(),
            speedqty34 = ko.observable(),
            speedqty35 = ko.observable(),
            hourlyCost = ko.observable(),
            hourlyPrice = ko.observable()
        }


        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           Id: Id,
           MethodId: MethodId,
           SheetsQty1: SheetsQty1,
           SheetsQty2: SheetsQty2,
           SheetsQty3: SheetsQty3,
           SheetsQty4: SheetsQty4,
           SheetsQty5: SheetsQty5,
           SheetWeight1: SheetWeight1,
           speedqty11: speedqty11,
           speedqty12: speedqty12,
           speedqty13: speedqty13,
           speedqty14: speedqty14,
           speedqty15: speedqty15,
           SheetWeight2: SheetWeight2,
           speedqty21: speedqty21,
           speedqty22: speedqty22,
           speedqty23: speedqty23,
           speedqty24: speedqty24,
           speedqty25: speedqty25,
           SheetWeight3: SheetWeight3,
           speedqty31: speedqty31,
           speedqty32: speedqty32,
           speedqty33: speedqty33,
           speedqty34: speedqty34,
           speedqty35: speedqty35,
           hourlyCost: hourlyCost,
           hourlyPrice: hourlyPrice

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id: Id,
            MethodId: MethodId,
            SheetsQty1: SheetsQty1,
            SheetsQty2: SheetsQty2,
            SheetsQty3: SheetsQty3,
            SheetsQty4: SheetsQty4,
            SheetsQty5: SheetsQty5,
            SheetWeight1: SheetWeight1,
            speedqty11: speedqty11,
            speedqty12: speedqty12,
            speedqty13: speedqty13,
            speedqty14: speedqty14,
            speedqty15: speedqty15,
            SheetWeight2: SheetWeight2,
            speedqty21: speedqty21,
            speedqty22: speedqty22,
            speedqty23: speedqty23,
            speedqty24: speedqty24,
            speedqty25: speedqty25,
            SheetWeight3: SheetWeight3,
            speedqty31: speedqty31,
            speedqty32: speedqty32,
            speedqty33: speedqty33,
            speedqty34: speedqty34,
            speedqty35: speedqty35,
            hourlyCost: hourlyCost,
            hourlyPrice: hourlyPrice,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    var PerHourLookup = function (source) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
            MethodId = ko.observable(source.MethodId),
            SpeedCost = ko.observable(source.SpeedCost),
            Speed = ko.observable(source.Speed),
            SpeedPrice = ko.observable(source.SpeedPrice)
        } else {
            Id = ko.observable(),
            MethodId = ko.observable(),
            SpeedCost = ko.observable(),
            Speed = ko.observable(),
            SpeedPrice = ko.observable()
        }

        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           Id: Id,
           MethodId: MethodId,
           SpeedCost: SpeedCost,
           Speed: Speed,
           SpeedPrice: SpeedPrice

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id: Id,
            MethodId: MethodId,
            SpeedCost: SpeedCost,
            Speed: Speed,
            SpeedPrice: SpeedPrice,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    var SpeedWeightLookup = function (source) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
                MethodId = ko.observable(source.MethodId),
                SheetsQty1 = ko.observable(source.SheetsQty1),
                SheetsQty2 = ko.observable(source.SheetsQty2),
                SheetsQty3 = ko.observable(source.SheetsQty3),
                SheetsQty4 = ko.observable(source.SheetsQty4),
                SheetsQty5 = ko.observable(source.SheetsQty5),
                SheetWeight1 = ko.observable(source.SheetWeight1),
                speedqty11 = ko.observable(source.speedqty11),
                speedqty12 = ko.observable(source.speedqty12),
                speedqty13 = ko.observable(source.speedqty13),
                speedqty14 = ko.observable(source.speedqty14),
                speedqty15 = ko.observable(source.speedqty15),
                SheetWeight2 = ko.observable(source.SheetWeight2),
                speedqty21 = ko.observable(source.speedqty21),
                speedqty22 = ko.observable(source.speedqty22),
                speedqty23 = ko.observable(source.speedqty23),
                speedqty24 = ko.observable(source.speedqty24),
                speedqty25 = ko.observable(source.speedqty25),
                SheetWeight3 = ko.observable(source.SheetWeight3),
                speedqty31 = ko.observable(source.speedqty31),
                speedqty32 = ko.observable(source.speedqty32),
                speedqty33 = ko.observable(source.speedqty33),
                speedqty34 = ko.observable(source.speedqty34),
                speedqty35 = ko.observable(source.speedqty35),
                hourlyCost = ko.observable(source.hourlyCost),
                hourlyPrice = ko.observable(source.hourlyPrice)
        } else {
            Id = ko.observable(),
                MethodId = ko.observable(),
                SheetsQty1 = ko.observable(),
                SheetsQty2 = ko.observable(),
                SheetsQty3 = ko.observable(),
                SheetsQty4 = ko.observable(),
                SheetsQty5 = ko.observable(),
                SheetWeight1 = ko.observable(),
                speedqty11 = ko.observable(),
                speedqty12 = ko.observable(),
                speedqty13 = ko.observable(),
                speedqty14 = ko.observable(),
                speedqty15 = ko.observable(),
                SheetWeight2 = ko.observable(),
                speedqty21 = ko.observable(),
                speedqty22 = ko.observable(),
                speedqty23 = ko.observable(),
                speedqty24 = ko.observable(),
                speedqty25 = ko.observable(),
                SheetWeight3 = ko.observable(),
                speedqty31 = ko.observable(),
                speedqty32 = ko.observable(),
                speedqty33 = ko.observable(),
                speedqty34 = ko.observable(),
                speedqty35 = ko.observable(),
                hourlyCost = ko.observable(),
                hourlyPrice = ko.observable()
        }
        errors = ko.validation.group({
        }),
        // Is Valid
      isValid = ko.computed(function () {
          return errors().length === 0;
      }),
      dirtyFlag = new ko.dirtyFlag({
          Id: Id,
          MethodId: MethodId,
          SheetsQty1: SheetsQty1,
          SheetsQty2: SheetsQty2,
          SheetsQty3: SheetsQty3,
          SheetsQty4: SheetsQty4,
          SheetsQty5: SheetsQty5,
          SheetWeight1: SheetWeight1,
          speedqty11: speedqty11,
          speedqty12: speedqty12,
          speedqty13: speedqty13,
          speedqty14: speedqty14,
          speedqty15: speedqty15,
          SheetWeight2: SheetWeight2,
          speedqty21: speedqty21,
          speedqty22: speedqty22,
          speedqty23: speedqty23,
          speedqty24: speedqty24,
          speedqty25: speedqty25,
          SheetWeight3: SheetWeight3,
          speedqty31: speedqty31,
          speedqty32: speedqty32,
          speedqty33: speedqty33,
          speedqty34: speedqty34,
          speedqty35: speedqty35,
          hourlyCost: hourlyCost,
          hourlyPrice: hourlyPrice

      }),
        // Has Changes
      hasChanges = ko.computed(function () {
          return dirtyFlag.isDirty();
      }),
        // Reset
      reset = function () {
          dirtyFlag.reset();
      };

        self = {
            Id: Id,
            MethodId: MethodId,
            SheetsQty1: SheetsQty1,
            SheetsQty2: SheetsQty2,
            SheetsQty3: SheetsQty3,
            SheetsQty4: SheetsQty4,
            SheetsQty5: SheetsQty5,
            SheetWeight1: SheetWeight1,
            speedqty11: speedqty11,
            speedqty12: speedqty12,
            speedqty13: speedqty13,
            speedqty14: speedqty14,
            speedqty15: speedqty15,
            SheetWeight2: SheetWeight2,
            speedqty21: speedqty21,
            speedqty22: speedqty22,
            speedqty23: speedqty23,
            speedqty24: speedqty24,
            speedqty25: speedqty25,
            SheetWeight3: SheetWeight3,
            speedqty31: speedqty31,
            speedqty32: speedqty32,
            speedqty33: speedqty33,
            speedqty34: speedqty34,
            speedqty35: speedqty35,
            hourlyCost: hourlyCost,
            hourlyPrice: hourlyPrice,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    var GuillotineClickPTV = function (source) {
        var self
        if (source != undefined) {
            Id = ko.observable(source.Id),
            NoofSections = ko.observable(source.NoofSections),
            NoofUps = ko.observable(source.NoofUps),
            Noofcutswithoutgutters = ko.observable(source.Noofcutswithoutgutters),
            Noofcutswithgutters = ko.observable(source.Noofcutswithgutters),
            GuilotineId = ko.observable(source.GuilotineId)
        } else {
            Id = ko.observable(),
            NoofSections = ko.observable(),
            NoofUps = ko.observable(),
            Noofcutswithoutgutters = ko.observable(),
            Noofcutswithgutters = ko.observable(),
            GuilotineId = ko.observable()
        }


        errors = ko.validation.group({
        }),
        // Is Valid
       isValid = ko.computed(function () {
           return errors().length === 0;
       }),
       dirtyFlag = new ko.dirtyFlag({
           Id: Id,
           NoofSections: NoofSections,
           NoofUps: NoofUps,
           Noofcutswithoutgutters: Noofcutswithoutgutters,
           Noofcutswithgutters: Noofcutswithgutters,
           GuilotineId: GuilotineId,

       }),
        // Has Changes
       hasChanges = ko.computed(function () {
           return dirtyFlag.isDirty();
       }),
        // Reset
       reset = function () {
           dirtyFlag.reset();
       };

        self = {
            Id: Id,
            NoofSections: NoofSections,
            NoofUps: NoofUps,
            Noofcutswithoutgutters: Noofcutswithoutgutters,
            Noofcutswithgutters: Noofcutswithgutters,
            GuilotineId: GuilotineId,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,

        };
        return self;

    }
    var GuillotinePTVList = function (source) {
        var GuillotinePTV = ko.observableArray([]);
        _.each(source, function (item) {
            GuillotinePTV.push(GuillotineClickPTV(item));
         //   sharedNavigationVM.initialize(GuillotinePTVList, function (saveCallback) { saveLookup(saveCallback); });
        });
        return GuillotinePTV;
    }
    var lookupClientMapper = function (source) {
        var olookupMethod = new lookupMethod();
        olookupMethod.MethodId(source.MethodId);
        olookupMethod.Name(source.Name);
        olookupMethod.Type(source.Type);
        return olookupMethod;
    }
    var GuilotinePtvServerMapper = function (GuillotineClickChargePTV) {
        var GuilotinePtv = {};
        GuilotinePtv.Id = GuillotineClickChargePTV.Id();
        GuilotinePtv.NoofSections = GuillotineClickChargePTV.NoofSections();
        GuilotinePtv.NoofUps = GuillotineClickChargePTV.NoofUps();
        GuilotinePtv.Noofcutswithoutgutters = GuillotineClickChargePTV.Noofcutswithoutgutters();
        GuilotinePtv.Noofcutswithgutters = GuillotineClickChargePTV.Noofcutswithgutters();
        GuilotinePtv.GuilotineId = GuillotineClickChargePTV.GuilotineId();
        return GuilotinePtv;
    }

    var ClickChargeZoneServerMapper = function(ClickChargeZone)
    {
        var ClickChargeZoneLookup = {};
        if (ClickChargeZone != undefined) {
            ClickChargeZoneLookup.Id = ClickChargeZone.Id();
            ClickChargeZoneLookup.MethodId = ClickChargeZone.MethodId();
            ClickChargeZoneLookup.From1 = ClickChargeZone.From1();
            ClickChargeZoneLookup.To1 = ClickChargeZone.To1();
            ClickChargeZoneLookup.Sheets1 = ClickChargeZone.Sheets1();
            ClickChargeZoneLookup.SheetCost1 = ClickChargeZone.SheetCost1();
            ClickChargeZoneLookup.SheetPrice1 = ClickChargeZone.SheetPrice1();
            ClickChargeZoneLookup.From2 = ClickChargeZone.From2();
            ClickChargeZoneLookup.To2 = ClickChargeZone.To2();
            ClickChargeZoneLookup.Sheets2 = ClickChargeZone.Sheets2();
            ClickChargeZoneLookup.SheetCost2 = ClickChargeZone.SheetCost2();
            ClickChargeZoneLookup.SheetPrice2 = ClickChargeZone.SheetPrice2();
            ClickChargeZoneLookup.From3 = ClickChargeZone.From3();
            ClickChargeZoneLookup.To3 = ClickChargeZone.To3();
            ClickChargeZoneLookup.Sheets3 = ClickChargeZone.Sheets3();
            ClickChargeZoneLookup.SheetCost3 = ClickChargeZone.SheetCost3();
            ClickChargeZoneLookup.SheetPrice3 = ClickChargeZone.SheetPrice3();
            ClickChargeZoneLookup.From4 = ClickChargeZone.From4();
            ClickChargeZoneLookup.To4 = ClickChargeZone.To4();
            ClickChargeZoneLookup.Sheets4 = ClickChargeZone.Sheets4();
            ClickChargeZoneLookup.SheetCost4 = ClickChargeZone.SheetCost4();
            ClickChargeZoneLookup.SheetPrice4 = ClickChargeZone.SheetPrice4();
            ClickChargeZoneLookup.From5 = ClickChargeZone.From5();
            ClickChargeZoneLookup.To5 = ClickChargeZone.To5();
            ClickChargeZoneLookup.Sheets5 = ClickChargeZone.Sheets5();
            ClickChargeZoneLookup.SheetCost5 = ClickChargeZone.SheetCost5();
            ClickChargeZoneLookup.SheetPrice5 = ClickChargeZone.SheetPrice5();
            ClickChargeZoneLookup.From6 = ClickChargeZone.From6();
            ClickChargeZoneLookup.To6 = ClickChargeZone.To6();
            ClickChargeZoneLookup.Sheets6 = ClickChargeZone.Sheets6();
            ClickChargeZoneLookup.SheetCost6 = ClickChargeZone.SheetCost6();
            ClickChargeZoneLookup.SheetPrice6 = ClickChargeZone.SheetPrice6();
            ClickChargeZoneLookup.From7 = ClickChargeZone.From7();
            ClickChargeZoneLookup.To7 = ClickChargeZone.To7();
            ClickChargeZoneLookup.Sheets7 = ClickChargeZone.Sheets7();
            ClickChargeZoneLookup.SheetCost7 = ClickChargeZone.SheetCost7();
            ClickChargeZoneLookup.SheetPrice7 = ClickChargeZone.SheetPrice7();
            ClickChargeZoneLookup.From8 = ClickChargeZone.From8();
            ClickChargeZoneLookup.To8 = ClickChargeZone.To8();
            ClickChargeZoneLookup.Sheets8 = ClickChargeZone.Sheets8();
            ClickChargeZoneLookup.SheetCost8 = ClickChargeZone.SheetCost8();
            ClickChargeZoneLookup.SheetPrice8 = ClickChargeZone.SheetPrice8();
            ClickChargeZoneLookup.From9 = ClickChargeZone.From9();
            ClickChargeZoneLookup.To9 = ClickChargeZone.To9();
            ClickChargeZoneLookup.Sheets9 = ClickChargeZone.Sheets9();
            ClickChargeZoneLookup.SheetCost9 = ClickChargeZone.SheetCost9();
            ClickChargeZoneLookup.SheetPrice9 = ClickChargeZone.SheetPrice9();
            ClickChargeZoneLookup.From10 = ClickChargeZone.From10();
            ClickChargeZoneLookup.To10 = ClickChargeZone.To10();
            ClickChargeZoneLookup.Sheets10 = ClickChargeZone.Sheets10();
            ClickChargeZoneLookup.SheetCost10 = ClickChargeZone.SheetCost10();
            ClickChargeZoneLookup.SheetPrice10 = ClickChargeZone.SheetPrice10();
            ClickChargeZoneLookup.From11 = ClickChargeZone.From11();
            ClickChargeZoneLookup.To11 = ClickChargeZone.To11();
            ClickChargeZoneLookup.Sheets11 = ClickChargeZone.Sheets11();
            ClickChargeZoneLookup.SheetCost11 = ClickChargeZone.SheetCost11();
            ClickChargeZoneLookup.SheetPrice11 = ClickChargeZone.SheetPrice11();
            ClickChargeZoneLookup.From12 = ClickChargeZone.From12();
            ClickChargeZoneLookup.To12 = ClickChargeZone.To12();
            ClickChargeZoneLookup.Sheets12 = ClickChargeZone.Sheets12();
            ClickChargeZoneLookup.SheetCost12 = ClickChargeZone.SheetCost12();
            ClickChargeZoneLookup.SheetPrice12 = ClickChargeZone.SheetPrice12();
            ClickChargeZoneLookup.From13 = ClickChargeZone.From13();
            ClickChargeZoneLookup.To13 = ClickChargeZone.To13();
            ClickChargeZoneLookup.Sheets13 = ClickChargeZone.Sheets13();
            ClickChargeZoneLookup.SheetCost13 = ClickChargeZone.SheetCost13();
            ClickChargeZoneLookup.SheetPrice13 = ClickChargeZone.SheetPrice13();
            ClickChargeZoneLookup.From14 = ClickChargeZone.From14();
            ClickChargeZoneLookup.To14 = ClickChargeZone.To14();
            ClickChargeZoneLookup.Sheets14 = ClickChargeZone.Sheets14();
            ClickChargeZoneLookup.SheetCost14 = ClickChargeZone.SheetCost14();
            ClickChargeZoneLookup.SheetPrice14 = ClickChargeZone.SheetPrice14();
            ClickChargeZoneLookup.From15 = ClickChargeZone.From15();
            ClickChargeZoneLookup.To15 = ClickChargeZone.To15();
            ClickChargeZoneLookup.Sheets15 = ClickChargeZone.Sheets15();
            ClickChargeZoneLookup.SheetCost15 = ClickChargeZone.SheetCost15();
            ClickChargeZoneLookup.SheetPrice15 = ClickChargeZone.SheetPrice15();
            ClickChargeZoneLookup.isaccumulativecharge = ClickChargeZone.isaccumulativecharge();
            ClickChargeZoneLookup.IsRoundUp = ClickChargeZone.IsRoundUp();
            ClickChargeZoneLookup.TimePerHour = ClickChargeZone.TimePerHour();
        }

        return {
          
            ClickChargeZone: ClickChargeZoneLookup,
            

        }
    }

    var GuillotineZoneServerMapper = function (GuillotineClickCharge) {
        var GuillotineClickChargelookup = {};
        if (GuillotineClickCharge != undefined) {
            GuillotineClickChargelookup.Id = GuillotineClickCharge.Id();
            GuillotineClickChargelookup.MethodId = GuillotineClickCharge.MethodId();
            GuillotineClickChargelookup.PaperWeight1 = GuillotineClickCharge.PaperWeight1();
            GuillotineClickChargelookup.PaperThroatQty1 = GuillotineClickCharge.PaperThroatQty1();
            GuillotineClickChargelookup.PaperWeight2 = GuillotineClickCharge.PaperWeight2();
            GuillotineClickChargelookup.PaperThroatQty2 = GuillotineClickCharge.PaperThroatQty2();
            GuillotineClickChargelookup.PaperWeight3 = GuillotineClickCharge.PaperWeight3();
            GuillotineClickChargelookup.PaperThroatQty3 = GuillotineClickCharge.PaperThroatQty3();
            GuillotineClickChargelookup.PaperWeight4 = GuillotineClickCharge.PaperWeight4();
            GuillotineClickChargelookup.PaperThroatQty4 = GuillotineClickCharge.PaperThroatQty4();
            GuillotineClickChargelookup.PaperWeight5 = GuillotineClickCharge.PaperWeight5();
            GuillotineClickChargelookup.PaperThroatQty5 = GuillotineClickCharge.PaperThroatQty5();
            GuillotineClickChargelookup.GuillotinePtvList = [];
            _.each(GuillotineClickCharge.GuillotinePTVList(), function (itm) {
                GuillotineClickChargelookup.GuillotinePtvList.push(GuilotinePtvServerMapper(itm));
            });

            return GuillotineClickChargelookup;
        }
    };

    var lookupServerMapper = function (lookupMethodId,olookup, ClickCharge, ClickChargeZone, SpeedWeight, PerHour, MeterPerHourClickCharge, GuillotineClickCharge, GuillotineClickChargePTV) {
        var oMethodId = 0;
        oMethodId = lookupMethodId;

        //var oLookupMethod = {};
        //oLookupMethod.MethodId = olookup.MethodId();
        //oLookupMethod.Name = olookup.Name();
        //oLookupMethod.Type = olookup.Type();

        var ClickChargeLookup = {};
        if (ClickCharge != undefined) {
            ClickChargeLookup.Id = ClickCharge.Id;
            ClickChargeLookup.MethodId = ClickCharge.MethodId;
            ClickChargeLookup.SheetCost = ClickCharge.SheetCost();
            ClickChargeLookup.Sheets = ClickCharge.Sheets();
            ClickChargeLookup.SheetPrice = ClickCharge.SheetPrice();
            ClickChargeLookup.TimePerHour = ClickCharge.TimePerHour();
        }

        var ClickChargeZoneLookup = {};
        if (ClickChargeZone != undefined) {
            ClickChargeZoneLookup.Id = ClickChargeZone.Id();
            ClickChargeZoneLookup.MethodId = ClickChargeZone.MethodId();
            ClickChargeZoneLookup.From1 = ClickChargeZone.From1();
            ClickChargeZoneLookup.To1 = ClickChargeZone.To1();
            ClickChargeZoneLookup.Sheets1 = ClickChargeZone.Sheets1();
            ClickChargeZoneLookup.SheetCost1 = ClickChargeZone.SheetCost1();
            ClickChargeZoneLookup.SheetPrice1 = ClickChargeZone.SheetPrice1();
            ClickChargeZoneLookup.From2 = ClickChargeZone.From2();
            ClickChargeZoneLookup.To2 = ClickChargeZone.To2();
            ClickChargeZoneLookup.Sheets2 = ClickChargeZone.Sheets2();
            ClickChargeZoneLookup.SheetCost2 = ClickChargeZone.SheetCost2();
            ClickChargeZoneLookup.SheetPrice2 = ClickChargeZone.SheetPrice2();
            ClickChargeZoneLookup.From3 = ClickChargeZone.From3();
            ClickChargeZoneLookup.To3 = ClickChargeZone.To3();
            ClickChargeZoneLookup.Sheets3 = ClickChargeZone.Sheets3();
            ClickChargeZoneLookup.SheetCost3 = ClickChargeZone.SheetCost3();
            ClickChargeZoneLookup.SheetPrice3 = ClickChargeZone.SheetPrice3();
            ClickChargeZoneLookup.From4 = ClickChargeZone.From4();
            ClickChargeZoneLookup.To4 = ClickChargeZone.To4();
            ClickChargeZoneLookup.Sheets4 = ClickChargeZone.Sheets4();
            ClickChargeZoneLookup.SheetCost4 = ClickChargeZone.SheetCost4();
            ClickChargeZoneLookup.SheetPrice4 = ClickChargeZone.SheetPrice4();
            ClickChargeZoneLookup.From5 = ClickChargeZone.From5();
            ClickChargeZoneLookup.To5 = ClickChargeZone.To5();
            ClickChargeZoneLookup.Sheets5 = ClickChargeZone.Sheets5();
            ClickChargeZoneLookup.SheetCost5 = ClickChargeZone.SheetCost5();
            ClickChargeZoneLookup.SheetPrice5 = ClickChargeZone.SheetPrice5();
            ClickChargeZoneLookup.From6 = ClickChargeZone.From6();
            ClickChargeZoneLookup.To6 = ClickChargeZone.To6();
            ClickChargeZoneLookup.Sheets6 = ClickChargeZone.Sheets6();
            ClickChargeZoneLookup.SheetCost6 = ClickChargeZone.SheetCost6();
            ClickChargeZoneLookup.SheetPrice6 = ClickChargeZone.SheetPrice6();
            ClickChargeZoneLookup.From7 = ClickChargeZone.From7();
            ClickChargeZoneLookup.To7 = ClickChargeZone.To7();
            ClickChargeZoneLookup.Sheets7 = ClickChargeZone.Sheets7();
            ClickChargeZoneLookup.SheetCost7 = ClickChargeZone.SheetCost7();
            ClickChargeZoneLookup.SheetPrice7 = ClickChargeZone.SheetPrice7();
            ClickChargeZoneLookup.From8 = ClickChargeZone.From8();
            ClickChargeZoneLookup.To8 = ClickChargeZone.To8();
            ClickChargeZoneLookup.Sheets8 = ClickChargeZone.Sheets8();
            ClickChargeZoneLookup.SheetCost8 = ClickChargeZone.SheetCost8();
            ClickChargeZoneLookup.SheetPrice8 = ClickChargeZone.SheetPrice8();
            ClickChargeZoneLookup.From9 = ClickChargeZone.From9();
            ClickChargeZoneLookup.To9 = ClickChargeZone.To9();
            ClickChargeZoneLookup.Sheets9 = ClickChargeZone.Sheets9();
            ClickChargeZoneLookup.SheetCost9 = ClickChargeZone.SheetCost9();
            ClickChargeZoneLookup.SheetPrice9 = ClickChargeZone.SheetPrice9();
            ClickChargeZoneLookup.From10 = ClickChargeZone.From10();
            ClickChargeZoneLookup.To10 = ClickChargeZone.To10();
            ClickChargeZoneLookup.Sheets10 = ClickChargeZone.Sheets10();
            ClickChargeZoneLookup.SheetCost10 = ClickChargeZone.SheetCost10();
            ClickChargeZoneLookup.SheetPrice10 = ClickChargeZone.SheetPrice10();
            ClickChargeZoneLookup.From11 = ClickChargeZone.From11();
            ClickChargeZoneLookup.To11 = ClickChargeZone.To11();
            ClickChargeZoneLookup.Sheets11 = ClickChargeZone.Sheets11();
            ClickChargeZoneLookup.SheetCost11 = ClickChargeZone.SheetCost11();
            ClickChargeZoneLookup.SheetPrice11 = ClickChargeZone.SheetPrice11();
            ClickChargeZoneLookup.From12 = ClickChargeZone.From12();
            ClickChargeZoneLookup.To12 = ClickChargeZone.To12();
            ClickChargeZoneLookup.Sheets12 = ClickChargeZone.Sheets12();
            ClickChargeZoneLookup.SheetCost12 = ClickChargeZone.SheetCost12();
            ClickChargeZoneLookup.SheetPrice12 = ClickChargeZone.SheetPrice12();
            ClickChargeZoneLookup.From13 = ClickChargeZone.From13();
            ClickChargeZoneLookup.To13 = ClickChargeZone.To13();
            ClickChargeZoneLookup.Sheets13 = ClickChargeZone.Sheets13();
            ClickChargeZoneLookup.SheetCost13 = ClickChargeZone.SheetCost13();
            ClickChargeZoneLookup.SheetPrice13 = ClickChargeZone.SheetPrice13();
            ClickChargeZoneLookup.From14 = ClickChargeZone.From14();
            ClickChargeZoneLookup.To14 = ClickChargeZone.To14();
            ClickChargeZoneLookup.Sheets14 = ClickChargeZone.Sheets14();
            ClickChargeZoneLookup.SheetCost14 = ClickChargeZone.SheetCost14();
            ClickChargeZoneLookup.SheetPrice14 = ClickChargeZone.SheetPrice14();
            ClickChargeZoneLookup.From15 = ClickChargeZone.From15();
            ClickChargeZoneLookup.To15 = ClickChargeZone.To15();
            ClickChargeZoneLookup.Sheets15 = ClickChargeZone.Sheets15();
            ClickChargeZoneLookup.SheetCost15 = ClickChargeZone.SheetCost15();
            ClickChargeZoneLookup.SheetPrice15 = ClickChargeZone.SheetPrice15();
            ClickChargeZoneLookup.isaccumulativecharge = ClickChargeZone.isaccumulativecharge();
            ClickChargeZoneLookup.IsRoundUp = ClickChargeZone.IsRoundUp();
            ClickChargeZoneLookup.TimePerHour = ClickChargeZone.TimePerHour();
        }
        var SpeedWeightLookup = {};

        if (SpeedWeight != undefined) {
            SpeedWeightLookup.Id = SpeedWeight.Id();
            SpeedWeightLookup.MethodId = SpeedWeight.MethodId();
            SpeedWeightLookup.SheetsQty1 = SpeedWeight.SheetsQty1();
            SpeedWeightLookup.SheetsQty2 = SpeedWeight.SheetsQty2();
            SpeedWeightLookup.SheetsQty3 = SpeedWeight.SheetsQty3();
            SpeedWeightLookup.SheetsQty4 = SpeedWeight.SheetsQty4();
            SpeedWeightLookup.SheetsQty5 = SpeedWeight.SheetsQty5();
            SpeedWeightLookup.SheetWeight1 = SpeedWeight.SheetWeight1();
            SpeedWeightLookup.speedqty11 = SpeedWeight.speedqty11();
            SpeedWeightLookup.speedqty12 = SpeedWeight.speedqty12();
            SpeedWeightLookup.speedqty13 = SpeedWeight.speedqty13();
            SpeedWeightLookup.speedqty14 = SpeedWeight.speedqty14();
            SpeedWeightLookup.speedqty15 = SpeedWeight.speedqty15();
            SpeedWeightLookup.SheetWeight2 = SpeedWeight.SheetWeight2();
            SpeedWeightLookup.speedqty21 = SpeedWeight.speedqty21();
            SpeedWeightLookup.speedqty22 = SpeedWeight.speedqty22();
            SpeedWeightLookup.speedqty23 = SpeedWeight.speedqty23();
            SpeedWeightLookup.speedqty24 = SpeedWeight.speedqty24();
            SpeedWeightLookup.speedqty25 = SpeedWeight.speedqty25();
            SpeedWeightLookup.SheetWeight3 = SpeedWeight.SheetWeight3();
            SpeedWeightLookup.speedqty31 = SpeedWeight.speedqty31();
            SpeedWeightLookup.speedqty32 = SpeedWeight.speedqty32();
            SpeedWeightLookup.speedqty33 = SpeedWeight.speedqty33();
            SpeedWeightLookup.speedqty34 = SpeedWeight.speedqty34();
            SpeedWeightLookup.speedqty35 = SpeedWeight.speedqty35();
            SpeedWeightLookup.hourlyCost = SpeedWeight.hourlyCost();
            SpeedWeightLookup.hourlyPrice = SpeedWeight.hourlyPrice();
        }
        var PerHourLookup = {};
        if (PerHour != undefined) {
            PerHourLookup.Id = PerHour.Id();
            PerHourLookup.MethodId = PerHour.MethodId();
            PerHourLookup.SpeedCost = PerHour.SpeedCost();
            PerHourLookup.Speed = PerHour.Speed();
            PerHourLookup.SpeedPrice = PerHour.SpeedPrice();

        }
        var MeterPerHourClickChargelookup = {};
        if (MeterPerHourClickCharge != undefined) {
            MeterPerHourClickChargelookup.Id = MeterPerHourClickCharge.Id();
            MeterPerHourClickChargelookup.MethodId = MeterPerHourClickCharge.MethodId();
            MeterPerHourClickChargelookup.SheetsQty1 = MeterPerHourClickCharge.SheetsQty1();
            MeterPerHourClickChargelookup.SheetsQty2 = MeterPerHourClickCharge.SheetsQty2();
            MeterPerHourClickChargelookup.SheetsQty3 = MeterPerHourClickCharge.SheetsQty3();
            MeterPerHourClickChargelookup.SheetsQty4 = MeterPerHourClickCharge.SheetsQty4();
            MeterPerHourClickChargelookup.SheetsQty5 = MeterPerHourClickCharge.SheetsQty5();
            MeterPerHourClickChargelookup.SheetWeight1 = MeterPerHourClickCharge.SheetWeight1();
            MeterPerHourClickChargelookup.speedqty11 = MeterPerHourClickCharge.speedqty11();
            MeterPerHourClickChargelookup.speedqty12 = MeterPerHourClickCharge.speedqty12();
            MeterPerHourClickChargelookup.speedqty13 = MeterPerHourClickCharge.speedqty13();
            MeterPerHourClickChargelookup.speedqty14 = MeterPerHourClickCharge.speedqty14();
            MeterPerHourClickChargelookup.speedqty15 = MeterPerHourClickCharge.speedqty15();
            MeterPerHourClickChargelookup.SheetWeight2 = MeterPerHourClickCharge.SheetWeight2();
            MeterPerHourClickChargelookup.speedqty21 = MeterPerHourClickCharge.speedqty21();
            MeterPerHourClickChargelookup.speedqty22 = MeterPerHourClickCharge.speedqty22();
            MeterPerHourClickChargelookup.speedqty23 = MeterPerHourClickCharge.speedqty23();
            MeterPerHourClickChargelookup.speedqty24 = MeterPerHourClickCharge.speedqty24();
            MeterPerHourClickChargelookup.speedqty25 = MeterPerHourClickCharge.speedqty25();
            MeterPerHourClickChargelookup.SheetWeight3 = MeterPerHourClickCharge.SheetWeight3();
            MeterPerHourClickChargelookup.speedqty31 = MeterPerHourClickCharge.speedqty31();
            MeterPerHourClickChargelookup.speedqty32 = MeterPerHourClickCharge.speedqty32();
            MeterPerHourClickChargelookup.speedqty33 = MeterPerHourClickCharge.speedqty33();
            MeterPerHourClickChargelookup.speedqty34 = MeterPerHourClickCharge.speedqty34();
            MeterPerHourClickChargelookup.speedqty35 = MeterPerHourClickCharge.speedqty35();
            MeterPerHourClickChargelookup.hourlyCost = MeterPerHourClickCharge.hourlyCost();
            MeterPerHourClickChargelookup.hourlyPrice = MeterPerHourClickCharge.hourlyPrice();
        }
        
        var GuillotineClickChargelookup = {};
        if (GuillotineClickCharge != undefined) {
            GuillotineClickChargelookup.Id = GuillotineClickCharge.Id();
            GuillotineClickChargelookup.MethodId = GuillotineClickCharge.MethodId();
            GuillotineClickChargelookup.PaperWeight1 = GuillotineClickCharge.PaperWeight1();
            GuillotineClickChargelookup.PaperThroatQty1 = GuillotineClickCharge.PaperThroatQty1();
            GuillotineClickChargelookup.PaperWeight2 = GuillotineClickCharge.PaperWeight2();
            GuillotineClickChargelookup.PaperThroatQty2 = GuillotineClickCharge.PaperThroatQty2();
            GuillotineClickChargelookup.PaperWeight3 = GuillotineClickCharge.PaperWeight3();
            GuillotineClickChargelookup.PaperThroatQty3 = GuillotineClickCharge.PaperThroatQty3();
            GuillotineClickChargelookup.PaperWeight4 = GuillotineClickCharge.PaperWeight4();
            GuillotineClickChargelookup.PaperThroatQty4 = GuillotineClickCharge.PaperThroatQty4();
            GuillotineClickChargelookup.PaperWeight5 = GuillotineClickCharge.PaperWeight5();
            GuillotineClickChargelookup.PaperThroatQty5 = GuillotineClickCharge.PaperThroatQty5();


        }
        var GuillotineClickChargePTVlookup = [];
        if (GuillotineClickChargePTV != undefined) {
            _.each(GuillotineClickChargePTV, function (item) {
                var ChargePTV = GuilotinePtvServerMapper(item);
                GuillotineClickChargePTVlookup.push(ChargePTV);
            });


        }



        return {
            lookupMethodId: oMethodId,
            //LookupMethod: oLookupMethod,
            ClickChargeLookup: ClickChargeLookup,
            ClickChargeZone: ClickChargeZoneLookup,
            SpeedWeightLookup: SpeedWeightLookup,
            PerHourLookup: PerHourLookup,
            MeterPerHourLookup: MeterPerHourClickChargelookup,
            GuillotineCalc: GuillotineClickChargelookup,
            GuilotinePtv: GuillotineClickChargePTVlookup,

        }



    }
    return {
        lookupMethod: lookupMethod,
        lookupClientMapper: lookupClientMapper,
        ClickChargeLookup: ClickChargeLookup,
        lookupServerMapper: lookupServerMapper,
        ClickChargeZone: ClickChargeZone,
        GuillotineCalc: GuillotineCalc,
        MeterPerHourLookup: MeterPerHourLookup,
        PerHourLookup: PerHourLookup,
        SpeedWeightLookup: SpeedWeightLookup,
        GuilotinePtvServerMapper: GuilotinePtvServerMapper,
        GuillotineClickPTV: GuillotineClickPTV,
        GuillotinePTVList: GuillotinePTVList,
        ClickChargeZoneServerMapper: ClickChargeZoneServerMapper,
        GuillotineZoneServerMapper: GuillotineZoneServerMapper
    };
});