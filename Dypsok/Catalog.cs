using System.Collections.Generic;
using System.Linq;

namespace Dypsok
{
    public class Catalog
    {
        private IQueryACatalog Queries;
        private IChangeACatalog Changes;
        private IGenerateProductIds ProductIdGenerator;

        public Catalog(IQueryACatalog Queries, IChangeACatalog Changes,IGenerateProductIds ProductIdGenerator)
        {
            this.Queries = Queries;
            this.Changes = Changes;
            this.ProductIdGenerator = ProductIdGenerator;
        }

        public void RegisterProduct(ProductId Id, IEnumerable<FareZone> FareZones)
        {
            Guard.Against<TooManyFareZonesException>(FareZones.Count() > 3);
            Guard.Against<DuplicateProductIdException>(Queries.IdExists(Id));
            if (Id == ProductId.Empty) Id = ProductIdGenerator.Next();
            Changes.ProductRegistered(Id);
        }

        public void RegisterSubscription(ProductId Id, IEnumerable<FareZone> FareZones, PaymentSchedule Schedule)
        {
            Guard.With<PaymentScheduleRequiredException>(Schedule != PaymentSchedule.Empty);
            RegisterProduct(Id, FareZones);
        }
    }
}
