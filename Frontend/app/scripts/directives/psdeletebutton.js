'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psDeleteButton
 * @description
 * # psDeleteButton
 */
angular.module('frontendApp')
  .directive('psDeleteButton', function () {

      function link(scope, element) {
          scope.initDelete = function () {
              var id = element.parent().data('id');
              scope.clear();
              scope.get(id)
              .success(function (data) {
                  scope.setInstance(data);
                  angular.element('#deleteModal').modal('show');
              })
              .error(function (data) {
                  scope.setErrors(data);
              });
          };
      };

      return {
          template: '<button class="btn btn-danger" ng-click="initDelete()">' +
                        '<span class="glyphicon glyphicon-trash"></span>' +
                    '</button>',
          restrict: 'E',
          scope: false,
          link: link
      };
  });