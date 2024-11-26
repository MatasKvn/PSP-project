namespace POS_System.Business.Services.Interfaces
{
    public interface IServiceOnTaxService
    {
        public Task MarkActiveTaxLinksDeleted(int taxId, CancellationToken cancellationToken);
        public Task RelinkTaxToItem(int oldTaxId, int newTaxId, CancellationToken cancellationToken);
        public Task LinkTaxToServicesAsync(int taxId, int[] serviceIdList, CancellationToken cancellationToken);
        public Task UnlinkTaxFromServicesAsync(int taxId, int[] serviceIdList, CancellationToken cancellationToken);
    }
}
