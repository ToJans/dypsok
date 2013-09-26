using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Dypsok.Specs
{
    [TestClass]
    public class CatalogSpecs
    {
        [TestInitialize]
        public void SetupProducts()
        {
            var catalogState = new CatalogState();
            this.Queries = catalogState;
            this.Changes = catalogState;
            this.a_fake_ProductIdGenerator = new FakeProductIdGenerator();

            this.Catalog = new Catalog(catalogState, catalogState, a_fake_ProductIdGenerator);
        }

        [TestMethod]
        public void should_contain_a_product_when_it_is_registered()
        {
            Catalog.RegisterProduct(a_product_id, some_fare_zones);

            Queries.IdExists(a_product_id).ShouldBe(true);
        }

        [TestMethod, ExpectedException(typeof(DuplicateProductIdException))]
        public void should_fail_when_registering_an_existing_id()
        {
            Changes.ProductRegistered(a_product_id);

            Catalog.RegisterProduct(a_product_id, some_fare_zones);
        }

        [TestMethod, ExpectedException(typeof(TooManyFareZonesException))]
        public void should_fail_when_registering_too_many_fare_zones()
        {
            Catalog.RegisterProduct(a_product_id, too_many_fare_zones);
        }

        [TestMethod]
        public void should_succeed_when_registering_a_product_with_an_empty_product_id()
        {
            Catalog.RegisterProduct(ProductId.Empty, some_fare_zones);

            Queries.IdExists(a_fake_ProductIdGenerator.IdToReturn);
        }

        [TestMethod, ExpectedException(typeof(NoMoreProductIdsAvailableException))]
        public void should_fail_when_registering_with_an_empty_product_id_and_new_product_ids_are_unavailable()
        {
            a_fake_ProductIdGenerator.NoMoreIdsAvailable = true;

            Catalog.RegisterProduct(ProductId.Empty, some_fare_zones);
        }

        [TestMethod]
        public void should_succeed_when_registering_a_subscription()
        {
            Catalog.RegisterSubscription(a_product_id, some_fare_zones, a_payment_schedule);

            Queries.IdExists(a_product_id).ShouldBe(true);
        }

        Catalog Catalog;

        IQueryACatalog Queries;
        IChangeACatalog Changes;

        ProductId a_product_id = new ProductId("products/1");
        IEnumerable<FareZone> some_fare_zones = "AMS,NY,BRU".Split(',').Select(x => new FareZone(x));
        IEnumerable<FareZone> too_many_fare_zones = "AMS,NY,BRU,RIO".Split(',').Select(x => new FareZone(x));
        FakeProductIdGenerator a_fake_ProductIdGenerator;
        PaymentSchedule a_payment_schedule = new PaymentSchedule();

        public class FakeProductIdGenerator : IGenerateProductIds
        {
            public ProductId IdToReturn = new ProductId("products/2");
            
            public bool NoMoreIdsAvailable;

            public ProductId Next()
            {
                if (NoMoreIdsAvailable)
                    throw new NoMoreProductIdsAvailableException();
                else
                    return IdToReturn;
            }
        }


    }
}
