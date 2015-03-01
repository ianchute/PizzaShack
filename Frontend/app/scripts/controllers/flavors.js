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

      // Clear errors and current instance.
      $scope.clear = function () {
          $scope.outerError = null;
          $scope.innerError = null;
          $scope.instance = {};
      };

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
                angular.element('#addModal').modal('hide');
                angular.element('#deleteModal').modal('hide');
            })
            .error(function (data, status) {
                $scope.outerError = { data: data, status: status };
                $scope.loading = false;
                $scope.pageLoading = false;
            });
          }, 1000);
      };

      // Add flavor.
      $scope.addFlavor = function () {
          FlavorRepository.add($scope.instance)
            .success(function () {
                $scope.page();
            })
            .error(function (data) {
                $scope.innerError = data;
            });
      };

      // Get flavor.
      $scope.getFlavor = function (id) {
          $scope.clear();
          FlavorRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                angular.element('#detailsModal').modal('show');
            })
            .error(function (data) {
                $scope.outerError = data;
            });
      };

      // Delete flavor.
      $scope.initDeleteFlavor = function (id) {
          $scope.clear();
          FlavorRepository.getById(id)
            .success(function (data) {
                $scope.instance = data;
                angular.element('#deleteModal').modal('show');
            })
            .error(function (data) {
                $scope.outerError = data;
            });
      };
      $scope.deleteFlavor = function () {
          FlavorRepository.deleteById($scope.instance.Id)
            .success(function () {
                $scope.page();
            })
            .error(function (data) {
                $scope.innerError = data;
            });
      };

      // Events.
      angular.element('#addModal').keyup(function (event) {
          if (event.keyCode === 13) { $scope.addFlavor(); }
      });
      angular.element('#deleteModal').keyup(function (event) {
          if (event.keyCode === 13) { $scope.deleteFlavor(); }
      });

      // Initialize.
      $scope.page();
});