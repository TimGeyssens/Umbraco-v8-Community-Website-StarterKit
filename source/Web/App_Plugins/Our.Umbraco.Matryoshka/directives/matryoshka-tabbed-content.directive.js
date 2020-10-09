(function () {
    'use strict';

    function tabbedContentDirective($timeout) {

        function link($scope, $element, $attrs) {

            var appRootNode = $element[0];
            $scope.currentTab = "";
            if ($scope.content.tabs[0]) {
                $scope.currentTab = $scope.content.tabs[0].label;
            }

            $scope.overflowingTabs = 0;


            var tabNavItemsWidths = [];
            // the parent is the component itself so we need to go one level higher
            var container = $element.parent().parent();

            $timeout(function(){
                $element.find(".matryoshka-tabs-list li:not(.umb-tab--expand)").each(function() {
                    tabNavItemsWidths.push($(this).outerWidth());
                });
                calculateWidth();
            });

            function calculateWidth(){
                $timeout(function(){
                    // 70 is the width of the expand menu (three dots) + 20 for the margin on umb-tabs-nav
                    var containerWidth = container.width() - 90;
                    var tabsWidth = 0;
                    $scope.overflowingSections = 0;
                    $scope.needTray = false;
                    $scope.maxTabs = tabNavItemsWidths.length;

                    // detect how many tabs we can show on the screen
                    for (var i = 0; i <= tabNavItemsWidths.length; i++) {

                        var tabWidth = tabNavItemsWidths[i];
                        tabsWidth += tabWidth;

                        if(tabsWidth >= containerWidth) {
                            $scope.needTray = true;
                            $scope.maxTabs = i;
                            $scope.overflowingTabs = $scope.maxTabs - $scope.content.tabs.length;
                            break;
                        }
                    }

                });
            }

            var ro = new ResizeObserver(function() {
                calculateWidth();
            });

            ro.observe(container[0]);
        }

        function controller($scope, $element, $attrs, $timeout) {

            var appRootNode = $element[0];

            $scope.currentTab = $scope.content.tabs[0];

            this.content = $scope.content;
            this.activeVariant = _.find(this.content.variants, variant => {
                return variant.active;
            });


            $scope.hide = function (label) {
                if ($scope.currentTab === label) {
                    return false;
                }
                return true;
            };

            $scope.changeTab = function changeTab(label) {
                $scope.currentTab = label;
                $scope.scrollTo(label, 0);
            };

            $scope.activeVariant = this.activeVariant;

            $scope.defaultVariant = _.find(this.content.variants, variant => {
                return variant.language.isDefault;
            });

            $scope.unlockInvariantValue = function (property) {
                property.unlockInvariantValue = !property.unlockInvariantValue;
            };

            $scope.$watch("tabbedContentForm.$dirty",
                function (newValue, oldValue) {
                    if (newValue === true) {
                        $scope.content.isDirty = true;
                    }
                }
            );

            $scope.needTray = false;
            $scope.showTray = false;
            $scope.overflowingSections = 0;

            $scope.toggleTray = toggleTray;
            $scope.hideTray = hideTray;

            function toggleTray() {
                $scope.showTray = !$scope.showTray;
            }

            function hideTray() {
                $scope.showTray = false;
            }


            $scope.groupSeparators = {};
            var scrollableNode = appRootNode.closest('.umb-scrollable');
            scrollableNode.addEventListener('mousewheel', cancelScrollTween);

            function getScrollPositionFor(tab, alias) {
                var offset = null;
                var groupSeparator = null;

                if (alias == 0) {
                    offset = 0;
                } else {
                    var previousTab = $scope.currentTab + "";
                    $scope.currentTab = tab;

                    groupSeparator = document.querySelector("#our-matryoshka-group-separator-" + alias);

                    if (!groupSeparator) {
                        $scope.currentTab = previousTab;
                        offset = null;
                    }
                }

                return $timeout(function () {
                    if (groupSeparator) {
                        offset = groupSeparator.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.offsetTop - 40;
                    }

                    return offset;
                });
            }

            $scope.scrollTo = function(tab, alias) {
                getScrollPositionFor(tab, alias).then(function(response) {
                    var y = response;

                    if (alias === 0 || y !== null) {
                        var viewportHeight = scrollableNode.clientHeight;
                        var from = scrollableNode.scrollTop;
                        var to = Math.min(y, scrollableNode.scrollHeight - viewportHeight);
                        var animeObject = { _y: from };
                        $scope.scrollTween = anime({
                            targets: animeObject,
                            _y: to,
                            easing: 'easeOutExpo',
                            duration: 200 + Math.min(Math.abs(to - from) / viewportHeight * 100, 400),
                            update: function update() {
                                scrollableNode.scrollTo(0, animeObject._y);
                            }
                        });
                    }
                });
            }
            function cancelScrollTween() {
                if ($scope.scrollTween) {
                    $scope.scrollTween.pause();
                }
            }

            $scope.content.tabs.map(function(tab) {
                $scope.groupSeparators[tab.label] = [];

                tab.properties.map(function(prop, i) {
                    if (prop.editor == "Our.Umbraco.Matryoshka.GroupSeparator" && prop.config.anchor == "1") {
                        $scope.groupSeparators[tab.label].push(prop);
                    }
                });
            });

            //ensure to unregister from all dom-events
            $scope.$on('$destroy', function () {
                cancelScrollTween();
            });
        }

        var directive = {
            restrict: 'E',
            replace: true,
            templateUrl: '/App_Plugins/Our.Umbraco.Matryoshka/directives/matryoshka-tabbed-content.html?umb_rnd=' + Umbraco.Sys.ServerVariables.application.cacheBuster,
            controller: controller,
            link: link,
            scope: {
                content: "="
            }
        };

        return directive;

    }

    angular.module('umbraco.directives').directive('matryoshkaTabbedContent', tabbedContentDirective);

})();
