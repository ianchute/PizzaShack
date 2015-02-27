'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:FlavorsCtrl
 * @description
 * # FlavorsCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('FlavorsCtrl', function ($scope, $timeout, FlavorRepository) {
      $scope.flavors = [];
      $scope.instance = {};
      $scope.outerError = null;
      $scope.innerError = null;
      $scope.loading = true;
      $scope.pageLoading = true;

      // Events.
      $("#addModal").keyup(function (event) {
          if (event.keyCode == 13) { $scope.addFlavor(); }
      });
      $("#deleteModal").keyup(function (event) {
          if (event.keyCode == 13) { $scope.deleteFlavor(); }
      });

      // Clear errors and current instance.
      $scope.clear = function () {
          $scope.outerError = null;
          $scope.innerError = null;
          $scope.instance = {};
      }

      // Load flavor page.
      $scope.page = function () {
          $scope.loading = true;
          $timeout(function () {
              FlavorRepository.list()
            .success(function (data) {
                $scope.flavors = data;
                $scope.loading = false;
                $scope.pageLoading = false;

                $scope.clear();
                $('#addModal').modal('hide');
                $('#deleteModal').modal('hide');
            })
            .error(function (data, status) {
                $scope.outerError = { data: data, status: status };
                $scope.loading = false;
            });
          }, 1000);
      }


      // Add flavor.
      $scope.addFlavor = function () {
          FlavorRepository.add($scope.instance)
            .success(function () {
                $scope.page();
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }

      // Get flavor.
      $scope.getFlavor = function (id) {
          $scope.clear();
          FlavorRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                $('#detailsModal').modal('show');
            })
            .error(function (data, status) {
                $scope.outerError = data;
            });
      }

      // Delete flavor.
      $scope.initDeleteFlavor = function (id) {
          $scope.clear();
          FlavorRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                $('#deleteModal').modal('show');
            })
            .error(function (data, status) {
                $scope.outerError = data;
            });
      }
      $scope.deleteFlavor = function () {
          FlavorRepository.deleteById($scope.instance.Id)
            .success(function () {
                $scope.page();
            })
            .error(function (data, status) {
                $scope.innerError = data;
            });
      }
});