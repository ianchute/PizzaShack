'use strict';

/**
 * @ngdoc service
 * @name frontendApp.CustomerRepository
 * @description
 * # CustomerRepository
 * Service in the frontendApp.
 */
angular.module('frontendApp')
  .service('CustomerRepository', function ($http, ApiPath) {
      var CustomerApiPath = ApiPath + 'Customers/';
      return {
          list :        function (page)     { return $http.get(CustomerApiPath, page); },
          add:          function (customer) { return $http.post(CustomerApiPath, customer); },
          getById:      function (id)       { return $http.get(CustomerApiPath + id); },
          edit:         function (customer) { return $http.put(CustomerApiPath, customer); },
          deleteById:   function (id)       { return $http.delete(CustomerApiPath + id); }
      };
  });