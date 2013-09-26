using System.Collections.Generic;

namespace Dypsok
{
    public interface IQueryACatalog
    {
        bool IdExists(ProductId Id);
    }

    public interface IChangeACatalog
    {
        void ProductRegistered(ProductId Id);
    }

    public class CatalogState : IQueryACatalog, IChangeACatalog
    {
        List<ProductId> RegisteredProductIds = new List<ProductId>();

        bool IQueryACatalog.IdExists(ProductId Id)
        {
            return RegisteredProductIds.Contains(Id);

        }

        void IChangeACatalog.ProductRegistered(ProductId Id)
        {
            RegisteredProductIds.Add(Id);
        }
    }
}
