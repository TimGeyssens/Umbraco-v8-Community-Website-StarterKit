(function () {
    "use strict";

    function paletteValuePickerController($scope) {

        var vm = this;

        vm.select = select;
        vm.close = close;
        vm.submit = submit;

        $scope.selectedIndex = null;

        function select($index) {
            $scope.selectedIndex = $index;
        }

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        }

        function submit() {
            if ($scope.model.submit) {
                $scope.model.submit($scope.model.config.palettes[$scope.selectedIndex]);
            }
        }

    }

    angular.module("umbraco").controller("Vizioz.PalettePicker.ValuePickerController", paletteValuePickerController);
})();