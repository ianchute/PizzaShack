'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psdeletemodal
 * @description
 * # psdeletemodal
 */
angular.module('frontendApp')
  .directive('psDeleteModal', function ($timeout) {

      function link(scope, element) {

          // Functions.
          scope.finalizeDelete = function () {
              scope.deleting = true;
              $timeout(function () {
                  scope.delete()
                  .success(function () {
                      scope.deleting = false;
                      scope.refresh();
                      angular.element('#deleteModal').modal('hide');
                  })
                  .error(function (data) {
                      scope.deleting = false;
                      scope.setErrors(data);
                  });
              }, 1000);
          }
      }

      return {
          templateUrl: 'views/psdeletemodal.html',
          restrict: 'E',
          scope: false,
          link: link
      };
  });