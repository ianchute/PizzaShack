'use strict';

/**
 * @ngdoc service
 * @name frontendApp.DeliveryPersonRepository
 * @description
 * # DeliveryPersonRepository
 * Service in the frontendApp.
 */
angular.module('frontendApp')
  .service('DeliveryPersonRepository', function (ApiPath, $http) {
      var DeliveryPersonApiPath = ApiPath + 'DeliveryPersons/';
      return {
          list:         function (page)     { return $http.get(DeliveryPersonApiPath, page); },
          add:          function (dPerson)  { return $http.post(DeliveryPersonApiPath, dPerson); },
          getById:      function (id)       { return $http.get(DeliveryPersonApiPath + id); },
          edit:         function (dPerson)  { return $http.put(DeliveryPersonApiPath , dPerson); },
          deleteById:   function (id)       { return $http.delete(DeliveryPersonApiPath + id); }
      };
  });