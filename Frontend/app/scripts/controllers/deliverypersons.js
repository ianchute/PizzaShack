'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:DeliverypersonCtrl
 * @description
 * # DeliverypersonCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('DeliveryPersonsCtrl', function ($scope, $timeout, DeliveryPersonRepository) {
      $scope.dPersons = [];
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
      }

      // Load delivery person page.
      $scope.page = function (page) {
          $scope.loading = true;
          $timeout(function () {
              DeliveryPersonRepository.list(page)
            .success(function (data) {
                $scope.dPersons = data;
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
                $scope.pageLoading = false;
            });
          }, 1000);
      }

      // Add delivery person.
      $scope.addDPerson = function () {
          DeliveryPersonRepository.add($scope.instance)
            .success(function () {
                $scope.page(0);
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }

      // Get delivery person.
      $scope.getDPerson = function (id) {
          $scope.clear();
          DeliveryPersonRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                $('#detailsModal').modal('show');
            })
            .error(function (data, status) {
                $scope.outerError = data;
            });
      }

      // Edit customer.
      $scope.initEditDPerson = function (id) {
          $scope.clear();

          DeliveryPersonRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                $('#editModal').modal('show');
            })
            .error(function (data, status) {
                $scope.outerError = data;
            });
      }
      $scope.editDPerson = function () {
          DeliveryPersonRepository.edit($scope.instance)
            .success(function () {
                $scope.page(0);
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }

      // Delete customer.
      $scope.initDeleteDPerson = function (id) {
          $scope.clear();
          DeliveryPersonRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                $('#deleteModal').modal('show');
            })
            .error(function (data, status) {
                $scope.outerError = data;
            });
      }
      $scope.deleteDPerson = function () {
          DeliveryPersonRepository.deleteById($scope.instance.Id)
            .success(function () {
                $scope.page(0);
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }

      // Events.
      $("#addModal").keyup(function (event) {
          if (event.keyCode == 13) { $scope.addDPerson(); }
      });
      $("#editModal").keyup(function (event) {
          if (event.keyCode == 13) { $scope.editDPerson(); }
      });
      $("#deleteModal").keyup(function (event) {
          if (event.keyCode == 13) { $scope.deleteDPerson(); }
      });

      // Initialize.
      $scope.page(0);
  });
