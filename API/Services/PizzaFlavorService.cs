using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Data;
using API.Models;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class PizzaFlavorService : IPizzaFlavorService
    {
        private Repositories.Interfaces.IPizzaFlavorRepository Repository { get; set; }

        public PizzaFlavorService(Repositories.Interfaces.IPizzaFlavorRepository repo)
        {
            this.Repository = repo;
        }

        public IEnumerable<Models.PizzaFlavorViewModel> List()
        {
            var result = new List<PizzaFlavorViewModel>(0);

            try
            {
                var entities = Repository.List();
                var models = new List<PizzaFlavorViewModel>(entities.Count());

                foreach (var entity in entities)
                {
                    var customerModel = Mapper.Map<PizzaFlavorViewModel>(entity);
                    models.Add(customerModel);
                }

                result = models;
            }
            catch { }

            return result;
        }

        public bool Add(Models.PizzaFlavorAddModel addModel)
        {
            try
            {
                var entity = Mapper.Map<PizzaFlavor>(addModel);
                Repository.CreateNewFlavor(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Models.PizzaFlavorViewModel Get(Guid id)
        {
            PizzaFlavorViewModel result = null;

            try
            {
                var entity = Repository.GetFlavorById(id);
                result = Mapper.Map<PizzaFlavorViewModel>(entity);
            }
            catch { }

            return result;
        }

        public bool Delete(Guid id)
        {
            try
            {
                Repository.DeleteFlavorById(id);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}