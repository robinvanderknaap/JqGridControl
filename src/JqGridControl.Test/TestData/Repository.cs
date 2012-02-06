using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoPoco.Engine;
using AutoPoco;
using AutoPoco.DataSources;
using Datasources;
using System.Linq.Expressions;

namespace JqGridControl.Test.TestData
{
    public class CustomerRepository
    {
        private IEnumerable<Customer> _customers;

        public CustomerRepository()
        {
            _customers = GetPocoFactory().CreateSession().List<Customer>(200).Get();

            for (var ctr = 1; ctr <= 200; ctr++)
            {
                _customers.ElementAt(ctr - 1).Id = ctr;
            }
        }

        public virtual Customer GetOne(int id)
        {
            return _customers.Single(x => x.Id == id);
        }
        
        public virtual Customer GetOne(List<Func<Customer, bool>> criteria)
        {
            return GetAll(criteria).SingleOrDefault();
        }

        public virtual IEnumerable<Customer> GetAll(List<Func<Customer, bool>> criteria)
        {
            var customers = _customers;

            foreach (var criterium in criteria)
            {
                customers = customers.Where(criterium);
            }

            return customers;
        }

        public IEnumerable<Customer> GetAll<TKey>(List<Func<Customer, bool>> criteria, int page, int pageSize, Func<Customer, TKey> orderBy, string orderByDirection)
        {
            return orderByDirection == "asc" ?
                GetAll(criteria).OrderBy(orderBy).Skip(page * pageSize).Take(pageSize) :
                GetAll(criteria).OrderByDescending(orderBy).Skip(page * pageSize).Take(pageSize);
        }
        
        private static IGenerationSessionFactory GetPocoFactory()
        {
            // Return factory for poco's
            return AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => c.UseDefaultConventions());
                x.AddFromAssemblyContainingType<Customer>();

                x.Include<Customer>()
                    .Setup(c => c.Firstname).Use<FirstNameSource>()
                    .Setup(c => c.Lastname).Use<LastNameSource>()
                    .Setup(c => c.Email).Use<ExtendedEmailAddressSource>("test.nl")
                    .Setup(c => c.City).Use<DutchCitySource>()
                    .Setup(c => c.DateOfBirth).Use<DateTimeSource>(DateTime.Now.AddYears(-65), DateTime.Now.AddYears(-18));
            });
        }

    }
}