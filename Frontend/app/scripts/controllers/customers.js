'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:CustomersCtrl
 * @description
 * # CustomersCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('CustomersCtrl', function ($scope, $controller, CustomerRepository) {

      $controller('BaseDataCtrl', { $scope: $scope });

      // Parameters.
      $scope.title = 'Customers';
      $scope.name = 'Customer';
      $scope.headers = ['Last Name', 'First Name', 'Address', 'Mobile Number'];
      $scope.keys = ['LastName', 'FirstName', 'Address', 'MobileNumber'];
      $scope.fields = [
          { label: 'First Name',    type: 'text', key: 'FirstName'      },
          { label: 'Last Name',     type: 'text', key: 'LastName'       },
          { label: 'Address',       type: 'text', key: 'Address'        },
          { label: 'Mobile Number', type: 'text', key: 'MobileNumber'   }
      ];

      // Functions.
      $scope.list = function () { return CustomerRepository.list($scope.currentPage); };
      $scope.add = function () { return CustomerRepository.add($scope.instance); };
      $scope.get = function (id) { return CustomerRepository.getById(id); };
      $scope.edit = function () { return CustomerRepository.edit($scope.instance); };
      $scope.delete = function () { return CustomerRepository.deleteById($scope.instance.Id); };
  });