using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dypsok
{
    public class FareZone
    {
        private string Key;

        public FareZone(string Key)
        {
            this.Key = Key;
        }
    }

    public class PaymentSchedule
    {
        public static PaymentSchedule Empty = new PaymentSchedule();
    }

    public interface IGenerateProductIds
    {
        ProductId Next();
    }

    public class ProductId
    {
        private string Key;

        public ProductId(string Key)
        {
            this.Key = Key;
        }

        public static ProductId Unavailable { get; set; }

        public static ProductId Empty { get; set; }
    }
}
