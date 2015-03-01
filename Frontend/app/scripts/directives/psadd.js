'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psAddButton
 * @description
 * # psAddButton
 */
angular.module('frontendApp')
  .directive('psAdd', function () {
    return {
      templateUrl: 'views/psadd.html',
      restrict: 'E',
      scope: {
          addInit: '=addInit',
          add: '=add',
          name: '=name',
          errors: '=errors',
          fields: '=fields',
          instance: '=instance'
      }
    };
  });