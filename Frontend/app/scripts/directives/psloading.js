'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psLoading
 * @description
 * # psLoading
 */
angular.module('frontendApp')
  .directive('psLoading', function (StatusCodeService) {

      function link(scope) {
          scope.getStatusName = function () {
              return StatusCodeService.getStatusName(scope.status);
          }
      };

      return {
          transclude: true,
          templateUrl: 'views/psloading.html',
          restrict: 'E',
          scope: true,
          link: link
      };
  });