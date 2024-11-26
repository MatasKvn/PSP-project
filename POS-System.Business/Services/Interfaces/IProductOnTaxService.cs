namespace POS_System.Business.Services.Interfaces
{
    public interface IProductOnTaxService
    {
        public Task MarkActiveTaxLinksDeleted(int taxId, CancellationToken cancellationToken);
        public Task RelinkTaxToItem(int oldTaxId, int newTaxId, CancellationToken cancellationToken);
        public Task LinkTaxToProductsAsync(int taxId, int[] productIdList, CancellationToken cancellationToken);
        public Task UnlinkTaxFromProductsAsync(int taxId, int[] productIdList, CancellationToken cancellationToken);
    }
}
