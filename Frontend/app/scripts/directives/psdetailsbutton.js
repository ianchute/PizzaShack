'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psDetailsButton
 * @description
 * # psDetailsButton
 */
angular.module('frontendApp')
  .directive('psDetailsButton', function () {

      function link(scope, element) {
          scope.finalizeGet = function () {
              var id = element.parent().data('id');
              scope.clear();
              scope.get(id)
              .success(function (data) {
                  scope.setInstance(data);
                  angular.element('#detailsModal').modal('show');
              })
              .error(function (data) {
                  scope.setErrors(data);
              });
          };
      };

      return {
          restrict: 'E',
          template: '<button class="btn btn-info" ng-click="finalizeGet()">' +
                        '<span class="glyphicon glyphicon-book"></span>' +
                    '</button>',
          scope: true,
          link: link
      };
  });