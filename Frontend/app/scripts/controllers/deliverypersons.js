'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:DeliverypersonCtrl
 * @description
 * # DeliverypersonCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('DeliveryPersonsCtrl', function ($scope, $controller, DeliveryPersonRepository) {

      $controller('BaseDataCtrl', { $scope: $scope });

      // Parameters.
      $scope.title = 'Delivery Personnel';
      $scope.name = 'Delivery Person';
      $scope.headers = ['Last Name', 'First Name'];
      $scope.keys = ['LastName', 'FirstName'];
      $scope.fields = [
          { label: 'First Name', type: 'text', key: 'FirstName' },
          { label: 'Last Name', type: 'text', key: 'LastName' },
      ];

      // Functions.
      $scope.list = function () { return DeliveryPersonRepository.list($scope.currentPage); };
      $scope.add = function () { return DeliveryPersonRepository.add($scope.instance); };
      $scope.get = function (id) { return DeliveryPersonRepository.getById(id); };
      $scope.edit = function () { return DeliveryPersonRepository.edit($scope.instance); };
      $scope.delete = function () { return DeliveryPersonRepository.deleteById($scope.instance.Id); };
  });
