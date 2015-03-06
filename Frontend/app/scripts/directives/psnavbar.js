'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psNavbar
 * @description
 * # psNavbar
 */
angular.module('frontendApp')
  .directive('psNavbar', function () {
    return {
      templateUrl: 'views/psnavbar.html',
      restrict: 'E',
      scope: true 
    };
  });
