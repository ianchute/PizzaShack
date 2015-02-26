'use strict';

describe('Service: CustomerRepository', function () {

  // load the service's module
  beforeEach(module('frontendApp'));

  // instantiate service
  var CustomerRepository;
  beforeEach(inject(function (_CustomerRepository_) {
    CustomerRepository = _CustomerRepository_;
  }));

  it('should do something', function () {
    expect(!!CustomerRepository).toBe(true);
  });

});
