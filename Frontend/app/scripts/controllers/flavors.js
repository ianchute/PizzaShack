'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:FlavorsCtrl
 * @description
 * # FlavorsCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('FlavorsCtrl', function ($scope, $controller, FlavorRepository) {

      $controller('BaseDataCtrl', { $scope: $scope });

      // Parameters.
      $scope.title = 'Pizza Flavors';
      $scope.name = 'Flavor';
      $scope.headers = ['Flavor'];
      $scope.keys = ['FlavorName'];
      $scope.fields = [
          { label: 'Flavor Name', type: 'text', key: 'FlavorName' },
      ];

      // Functions.
      $scope.list = function () { return FlavorRepository.list($scope.currentPage); };
      $scope.add = function () { return FlavorRepository.add($scope.instance); };
      $scope.get = function (id) { return FlavorRepository.getById(id); };
      $scope.delete = function () { return FlavorRepository.deleteById($scope.instance.Id); };
});