'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:pseditmodal
 * @description
 * # pseditmodal
 */
angular.module('frontendApp')
  .directive('psEditModal', function ($timeout) {

      function link(scope, element) {

          // Functions.
          scope.finalizeEdit = function () {
              scope.editing = true;
              $timeout(function () {
                  scope.edit()
                  .success(function () {
                      scope.refresh();
                      angular.element('#editModal').modal('hide');
                      scope.editing = false;
                  })
                  .error(function (data) {
                      scope.setErrors(data);
                      scope.editing = false;
                  });
              }, 1000);
          };
      };

      return {
          templateUrl: 'views/pseditmodal.html',
          restrict: 'E',
          scope: false,
          link: link
      };
  });