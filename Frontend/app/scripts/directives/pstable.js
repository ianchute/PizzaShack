'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psContent
 * @description
 * # psContent
 */
angular.module('frontendApp')
  .directive('psTable', function ($timeout) {

      function link(scope) {
          scope.refresh = function () {
              scope.setLoading(true);
              $timeout(function () {
                  scope.list()
                 .success(function (data, status) {
                     scope.setData(data);
                     scope.setStatus(status);
                     scope.clear();
                     scope.setLoading(false);
                  })
                  .error(function (data, status) {
                     scope.setStatus(status);
                     scope.setLoading(false);
                  });
              }, 1000);
          };

          scope.refresh();
      }

      return {
          transclude: true,
          restrict: 'E',
          templateUrl: 'views/pstable.html',
          scope: false,
          link: link
      };
  });