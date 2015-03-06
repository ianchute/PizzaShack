'use strict';
          //    keys: "=keys"

describe('Service: DeliveryPersonRepository', function () {

  // load the service's module
  beforeEach(module('frontendApp'));

  // instantiate service
  var DeliveryPersonRepository;
  beforeEach(inject(function (_DeliveryPersonRepository_) {
    DeliveryPersonRepository = _DeliveryPersonRepository_;
  }));

  it('should do something', function () {
    expect(!!DeliveryPersonRepository).toBe(true);
  });

});
