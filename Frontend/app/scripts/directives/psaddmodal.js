'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psaddmodal
 * @description
 * # psaddmodal
 */
angular.module('frontendApp')
  .directive('psAddModal', function ($timeout) {

      function link(scope, element) {

          // Functions.
          scope.finalizeAdd = function () {
              scope.adding = true;
              $timeout(function () {
                  scope.add()
                  .success(function () {
                      scope.refresh();
                      angular.element('#addModal').modal('hide');
                      scope.adding = false;
                  })
                  .error(function (data) {
                      scope.errors = data;
                      scope.adding = false;
                  });
              }, 1000);
          }
      };

      return {
          templateUrl: 'views/psaddmodal.html',
          restrict: 'E',
          scope: true,
          link: link
      };
  });