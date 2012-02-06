using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoPoco.Engine;
using AutoPoco;
using AutoPoco.DataSources;
using Datasources;

namespace JqGridControl.Test.TestData
{
    public class Repository
    {
        public IList<Customer> Customers { get; private set; }

        public Repository()
        {
            Customers = GetPocoFactory().CreateSession().List<Customer>(200).Get();

            for (var ctr = 1; ctr <= 200; ctr++)
            {
                Customers[ctr - 1].Id = ctr;
            }
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