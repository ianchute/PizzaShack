'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psdetailsmodal
 * @description
 * # psdetailsmodal
 */
angular.module('frontendApp')
  .directive('psDetailsModal', function () {

      function link(scope, element) {
          //element.keyup(function (event) {
          //    if (event.keyCode === 13) {
          //        scope.get();
          //    }
          //});
      };

      return {
          templateUrl: 'views/psdetailsmodal.html',
          restrict: 'E',
          scope: false,
          link: link
      };
  });