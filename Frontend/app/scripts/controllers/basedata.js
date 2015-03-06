'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:BasedataCtrl
 * @description
 * # BasedataCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('BaseDataCtrl', function ($scope) {

      // Containers.
      $scope.data = [];
      $scope.instance = {};
      $scope.errors = [];

      // Status Variables.
      $scope.loading = true;
      $scope.status = 0;
      $scope.currentPage = 0;

      // Setters.
      $scope.setData = function (data) { $scope.data = data; }
      $scope.setStatus = function (status) { $scope.status = status; }
      $scope.setInstance = function (data) { $scope.instance = data; };
      $scope.setErrors = function (data) { $scope.errors = data; };
      $scope.setLoading = function (isLoading) { $scope.loading = isLoading; };
      $scope.clear = function () { $scope.setInstance({}); $scope.setErrors([]); };

  });
