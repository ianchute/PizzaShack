'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:CustomersCtrl
 * @description
 * # CustomersCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('CustomersCtrl', function ($scope, CustomerRepository) {
      $scope.customers = [];
      $scope.instance = {};
      $scope.outerError = null;
      $scope.innerError = null;

      $scope.page = function (page) {
          CustomerRepository.list(page)
            .success(function (data) { $scope.customers = data; })
            .error(function (data, status) { $scope.outerError = { data : data, status : status }; });
      }

      $scope.addCustomer = function () {
          CustomerRepository.add($scope.instance)
            .success(function () { $('#addModal').modal('hide'); $scope.page(0); })
            .error(function (data, status) { $scope.innerError = data; });
      }

      $scope.getCustomer = function (id) {
          CustomerRepository.getById(id)
            .success(function (data) { $scope.instance = data; $('#detailsModal').modal('show'); })
            .error(function (data, status) { $scope.outerError = data; });
      }

      $scope.initEditCustomer = function (id) {
          CustomerRepository.getById(id)
            .success(function (data) { $scope.instance = data; $('#editModal').modal('show'); })
            .error(function (data, status) { $scope.outerError = data; });
      }
      $scope.editCustomer = function () {
          CustomerRepository.edit($scope.instance)
            .success(function () { $('#editModal').modal('hide'); $scope.page(0); })
            .error(function (data, status) { $scope.innerError = data; });
      }

      $scope.initDeleteCustomer = function (id) {
          CustomerRepository.getById(id)
            .success(function (data) { $scope.instance = data; $('#deleteModal').modal('show'); })
            .error(function (data, status) { $scope.outerError = data; });
      }
      $scope.deleteCustomer = function () {
          CustomerRepository.deleteById($scope.instance.Id)
            .success(function () { $('#deleteModal').modal('hide'); $scope.page(0); })
            .error(function (data, status) { $scope.innerError = data; });
      }
  });