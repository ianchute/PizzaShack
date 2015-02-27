'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:CustomersCtrl
 * @description
 * # CustomersCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('CustomersCtrl', function ($scope, $timeout, CustomerRepository) {
      $scope.customers = [];
      $scope.instance = {};
      $scope.outerError = null;
      $scope.innerError = null;
      $scope.loading = true;
      $scope.pageLoading = true;

      // Events.
      $("#addModal").keyup(function (event) {
          if (event.keyCode == 13) { $scope.addCustomer(); }
      });
      $("#editModal").keyup(function (event) {
          if (event.keyCode == 13) { $scope.editCustomer(); }
      });
      $("#deleteModal").keyup(function (event) {
          if (event.keyCode == 13) { $scope.deleteCustomer(); }
      });

      // Clear errors and current instance.
      $scope.clear = function () {
          $scope.outerError = null;
          $scope.innerError = null;
          $scope.instance = {};
      }

      // Load customer page.
      $scope.page = function (page) {
          $scope.loading = true;
          $timeout(function () {
              CustomerRepository.list(page)
            .success(function (data) {
                $scope.customers = data;
                $scope.loading = false;
                $scope.pageLoading = false;

                $scope.clear();
                $('#addModal').modal('hide');
                $('#editModal').modal('hide');
                $('#deleteModal').modal('hide');
            })
            .error(function (data, status) {
                $scope.outerError = { data: data, status: status };
                $scope.loading = false;
            });
          }, 1000
          );
          
      }

      // Add customer.
      $scope.addCustomer = function () {
          CustomerRepository.add($scope.instance)
            .success(function () {
                $scope.page(0);
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
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }
  });