(function () {
    "use strict";

    function palettePropertyEditorController($scope, editorService) {

        var vm = this;
        var editIndex = null;

        vm.add = add;
        vm.edit = edit;
        vm.remove = remove;
        
        $scope.sortableOptions = {
            axis: "y",
            cursor: "move",
            handle: ".icon-navigation"
        };

        var dialogOptions = {
            title: "Add a colour palette",
            view: "/App_Plugins/Vizioz.PalettePicker/Views/palettepicker.editorprevaluedialog.html",
            size: "small",
            value: null,
            submit: function (model) {
                if (editIndex !== null) {
                    $scope.model.value[editIndex] = model;
                } else {
                    if (Array.isArray($scope.model.value)) {
                        $scope.model.value.push(model);
                    } else {
                        $scope.model.value = [model];
                    }
                }
                editorService.close();
            },
            close: function () {
                editorService.close();
            }
        };
        
        function add() {
            editIndex = null;
            dialogOptions.value = null;
            openEditor();
        }

        function edit($index) {
            editIndex = $index;
            dialogOptions.value = $scope.model.value[$index];
            openEditor();
        }

        function remove (index) {
            $scope.model.value.splice(index, 1);
        }

        function openEditor() {
            editorService.open(dialogOptions);
        }

    }

    angular.module("umbraco").controller("Vizioz.PalettePicker.PropertyEditorController", palettePropertyEditorController);
})();