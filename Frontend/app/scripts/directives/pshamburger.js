'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psHamburger
 * @description
 * # psHamburger
 */
angular.module('frontendApp')
  .directive('psHamburger', function () {
    return {
      templateUrl: 'views/pshamburger.html',
      restrict: 'E',
    };
  });
