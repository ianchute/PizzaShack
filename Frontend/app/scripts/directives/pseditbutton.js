'use strict';

/**
 * @ngdoc directive
 * @name frontendApp.directive:psEditButton
 * @description
 * # psEditButton
 */
angular.module('frontendApp')
  .directive('psEditButton', function () {

      function link(scope, element) {

          scope.initEdit = function () {
              var id = element.parent().data('id');
              scope.clear();
              scope.get(id)
              .success(function (data) {
                  scope.setInstance(data);
                  angular.element('#editModal').modal('show');
              })
              .error(function (data) {
                  scope.setErrors(data);
              });
          };

      };

      return {
          template: '<button class="btn btn-default" ng-click="initEdit()">' +
                        '<span class="glyphicon glyphicon-pencil"></span>' +
                    '</button>',
          restrict: 'E',
          scope: false,
          link: link
      };
  });
