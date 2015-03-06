'use strict';

/**
 * @ngdoc function
 * @name frontendApp.controller:GlobalCtrl
 * @description
 * # GlobalCtrl
 * Controller of the frontendApp
 */
angular.module('frontendApp')
  .controller('GlobalCtrl', function ($scope) {
      $scope.navbar = {
          'Home': '/',
          'Pizza Flavors': 'flavors',
          'Customers': 'customers',
          'Delivery Personnel': 'deliveryPersons'
      }
  });