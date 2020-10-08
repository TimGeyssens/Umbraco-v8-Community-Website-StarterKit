(function () {
    "use strict";

    function paletteEditorPrevalueController($scope) {

        var vm = this;

        vm.submit = submit;
        vm.close = close;
        
        $scope.editModel = $scope.model.value ? angular.copy($scope.model.value) : { type: 1 };

        function submit() {
            if ($scope.model.submit) {
                if ($scope.editModel.content && $scope.editModel.valid) {
                    $scope.model.submit($scope.editModel);
                } 
            }
        }

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        }

    }

    angular.module("umbraco").controller("Vizioz.PalettePicker.EditorPrevalueController", paletteEditorPrevalueController);
})();