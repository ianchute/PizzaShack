using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class DeliveryPersonService : IDeliveryPersonService
    {
        private Repositories.Interfaces.IDeliveryPersonRepository Repository;

        public DeliveryPersonService(IDeliveryPersonRepository repo)
        {
            this.Repository = repo;
        }
        public IEnumerable<Models.DeliveryPersonViewModel> List(int page)
        {
            IEnumerable<DeliveryPersonViewModel> result = new List<DeliveryPersonViewModel>(0);

            try
            {
                var dPersonEntities = Repository.List(page);
                var dPersonModels = new List<DeliveryPersonViewModel>(dPersonEntities.Count());

                foreach (var entity in dPersonEntities)
                {
                    var customerModel = Mapper.Map<DeliveryPersonViewModel>(entity);
                    dPersonModels.Add(customerModel);
                }

                result = dPersonModels;
            }
            catch { }

            return result;
        }

        public bool Add(Models.DeliveryPersonAddModel model)
        {
            try
            {
                var entity = Mapper.Map<DeliveryPerson>(model);
                Repository.CreateNewDeliveryPerson(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Models.DeliveryPersonViewModel Get(Guid id)
        {
            DeliveryPersonViewModel result = null;

            try
            {
                var entity = Repository.GetDeliveryPersonById(id);
                result = Mapper.Map<DeliveryPersonViewModel>(entity);
            }
            catch { }

            return result;
        }

        public bool Edit(Models.DeliveryPersonEditModel model)
        {
            try
            {
                var entity = Mapper.Map<DeliveryPerson>(model);
                Repository.UpdateDeliveryPersonDetails(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Delete(Guid id)
        {
            try
            {
                Repository.DeleteDeliveryPersonById(id);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}