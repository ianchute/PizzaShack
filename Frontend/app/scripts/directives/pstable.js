'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psContent
 * @description
 * # psContent
 */
angular.module('frontendApp')
  .directive('psTable', function () {
    return {
      restrict: 'E',
      templateUrl: 'views/pstable.html',
      scope: {
          headers : "=headers",
          data : "=data",
          keys : "=keys"
      }
    };
  });
