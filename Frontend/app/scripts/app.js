'use strict';

/**
 * @ngdoc overview
 * @name frontendApp
 * @description
 * # frontendApp
 *
 * Main module of the application.
 */
angular
  .module('frontendApp', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngRoute',
    'ngSanitize',
    'ngTouch'
  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl'
      })
      .when('/about', {
        templateUrl: 'views/about.html',
        controller: 'AboutCtrl'
      })
      .when('/pizzas', {
        templateUrl: 'views/pizzas.html',
        controller: 'PizzasCtrl'
      })
      .when('/true', {
        templateUrl: 'views/deliveries.html',
        controller: 'DeliveriesCtrl'
      })
      .when('/customers', {
        templateUrl: 'views/customers.html',
        controller: 'CustomersCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  });