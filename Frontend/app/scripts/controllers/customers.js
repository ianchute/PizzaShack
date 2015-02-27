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
      $scope.loading = true;

      // Clear errors and current instance.
      $scope.clear = function () {
          $scope.outerError = null;
          $scope.innerError = null;
          $scope.instance = {};
      }

      // Load customer page.
      $scope.page = function (page) {
          $scope.clear();
          CustomerRepository.list(page)
            .success(function (data) {
                $scope.customers = data;
                $scope.loading = false;
            })
            .error(function (data, status) {
                $scope.outerError = { data: data, status: status };
                $scope.loading = false;
            });
      }

      // Add customer.
      $scope.addCustomer = function () {
          CustomerRepository.add($scope.instance)
            .success(function () {
                $scope.page(0);
                $('#addModal').modal('hide');
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }

      // Get customer.
      $scope.getCustomer = function (id) {
          $scope.clear();
          CustomerRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                $('#detailsModal').modal('show');
            })
            .error(function (data, status) {
                $scope.outerError = data;
            });
      }

      // Edit customer.
      $scope.initEditCustomer = function (id) {
          $scope.clear();
          CustomerRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                $('#editModal').modal('show');
            })
            .error(function (data, status) { $scope.outerError = data; });
      }
      $scope.editCustomer = function () {
          CustomerRepository.edit($scope.instance)
            .success(function () {
                $scope.page(0);
                $('#editModal').modal('hide');
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }

      // Delete customer.
      $scope.initDeleteCustomer = function (id) {
          $scope.clear();
          CustomerRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                $('#deleteModal').modal('show');
            })
            .error(function (data, status) {
                $scope.outerError = data;
            });
      }
      $scope.deleteCustomer = function () {
          CustomerRepository.deleteById($scope.instance.Id)
            .success(function () {
                $scope.page(0);
                $('#deleteModal').modal('hide');
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }
  });