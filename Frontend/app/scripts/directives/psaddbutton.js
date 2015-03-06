'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psAddButton
 * @description
 * # psAddButton
 */
angular.module('frontendApp')
  .directive('psAddButton', function () {

      function link(scope) {
          scope.startAdd = function () {
              scope.clear();
              angular.element('#addModal').modal('show');
          };
      };

      return {
          templateUrl: 'views/psadd.html',
          restrict: 'E',
          scope: true,
          link: link
      };
  });