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
      $scope.headers = ['Last Name', 'First Name', 'Address', 'Mobile Number'];
      $scope.keys = ['LastName', 'FirstName', 'Address', 'MobileNumber'];
      $scope.fields = [
          {
              label: 'First Name',
              type: 'text',
              key: 'FirstName'
          },
          {
              label: 'Last Name',
              type: 'text',
              key: 'LastName'
          },
          {
              label: 'Address',
              type: 'text',
              key: 'Address'
          },
          {
              label: 'Mobile Number',
              type: 'text',
              key: 'MobileNumber'
          }];
      $scope.instance = {};
      $scope.outerError = null;
      $scope.innerError = null;
      $scope.loading = true;
      $scope.pageLoading = true;

      // Clear errors and current instance.
      $scope.clear = function () {
          $scope.outerError = null;
          $scope.innerError = null;
          $scope.instance = {};
      };

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
                angular.element('#addModal').modal('hide');
                angular.element('#editModal').modal('hide');
                angular.element('#deleteModal').modal('hide');
            })
            .error(function (data, status) {
                $scope.outerError = { data: data, status: status };
                $scope.loading = false;
                $scope.pageLoading = false;
            });
          }, 1000
          );
      };

      // Add customer.
      $scope.addCustomer = function () {
          CustomerRepository.add($scope.instance)
            .success(function () {
                $scope.page(0);
            })
            .error(function (data) {
                $scope.innerError = data;
            });
      };

      // Get customer.
      $scope.getCustomer = function (id) {
          $scope.clear();
          CustomerRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                angular.element('#detailsModal').modal('show');
            })
            .error(function (data) {
                $scope.outerError = data;
            });
      };

      // Edit customer.
      $scope.initEditCustomer = function (id) {
          $scope.clear();
          CustomerRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                angular.element('#editModal').modal('show');
            })
            .error(function (data) { $scope.outerError = data; });
      };
      $scope.editCustomer = function () {
          CustomerRepository.edit($scope.instance)
            .success(function () {
                $scope.page(0);
            })
            .error(function (data) {
                $scope.innerError = data;
            });
      };

      // Delete customer.
      $scope.initDeleteCustomer = function (id) {
          $scope.clear();
          CustomerRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                angular.element('#deleteModal').modal('show');
            })
            .error(function (data) {
                $scope.outerError = data;
            });
      };
      $scope.deleteCustomer = function () {
          CustomerRepository.deleteById($scope.instance.Id)
            .success(function () {
                $scope.page(0);
            })
            .error(function (data) {
                $scope.innerError = data;
            });
      };

      // Events.
      
      angular.element('#editModal').keyup(function (event) {
          if (event.keyCode === 13) { $scope.editCustomer(); }
      });
      angular.element('#deleteModal').keyup(function (event) {
          if (event.keyCode === 13) { $scope.deleteCustomer(); }
      });

      // Initialize.
      $scope.page(0);
  });