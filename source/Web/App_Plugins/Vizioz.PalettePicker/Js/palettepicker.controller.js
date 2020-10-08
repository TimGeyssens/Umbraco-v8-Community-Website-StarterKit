(function () {
    "use strict";

    function paletteController($scope, editorService) {

        var vm = this;
        
        vm.remove = remove;
        vm.edit = edit;

        function remove () {
            $scope.model.value = null;
        }

        function setPaletteValue(item) {
            $scope.model.value = item;
        }

        function edit() {
            var dialogOptions = {
                title: "Select a colour palette",
                view: "/App_Plugins/Vizioz.PalettePicker/Views/palettepicker.valuepicker.html",
                size: "small",
                value: $scope.model.value,
                config: $scope.model.config,
                submit: function (model) {
                    setPaletteValue(model);
                    editorService.close();
                },
                close: function () {
                    editorService.close();
                }
            };

            editorService.open(dialogOptions);
        }
    }

    angular.module("umbraco").controller("Vizioz.PalettePicker.Controller", paletteController);
})();