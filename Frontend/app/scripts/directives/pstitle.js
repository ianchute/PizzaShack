'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psTitle
 * @description
 * # psTitle
 */
angular.module('frontendApp')
  .directive('psTitle', function () {
      return {
          template: '<h2>{{title}}</h2>',
          restrict: 'E',
          scope: true
      };
  });