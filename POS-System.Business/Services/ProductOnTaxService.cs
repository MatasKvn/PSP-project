using AutoMapper;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ProductOnTaxService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductOnTaxService
    {
        public async Task MarkActiveTaxLinksDeleted(int taxId, CancellationToken cancellationToken)
        {
            var productLinks = await _unitOfWork.ProductOnTaxRepository.GetAllByExpressionAsync(x => x.TaxVersionId == taxId && x.EndDate == null, cancellationToken);

            foreach (var productLink in productLinks)
            {
                productLink.EndDate = DateTime.UtcNow;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RelinkTaxToItem(int oldTaxId, int newTaxId, CancellationToken cancellationToken)
        {
            var productLinks = await _unitOfWork.ProductOnTaxRepository.GetAllByExpressionAsync(x => x.TaxVersionId == oldTaxId && x.EndDate == null, cancellationToken);

            foreach (var productLink in productLinks)
            {
                productLink.EndDate = DateTime.UtcNow;

                var newProductOnTax = new ProductOnTax
                {
                    TaxVersionId = newTaxId,
                    ProductVersionId = productLink.ProductVersionId,
                    StartDate = DateTime.UtcNow,
                    EndDate = null
                };

                await _unitOfWork.ProductOnTaxRepository.CreateAsync(newProductOnTax);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LinkTaxToProductsAsync(int taxId, int[] productIdList, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByIdAsync(taxId, cancellationToken);
            if (tax is not null && tax.IsDeleted == false)
            {
                foreach (var productId in productIdList)
                {
                    var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId, cancellationToken);
                    if (product is not null && product.IsDeleted == false)
                    {
                        var existingLink = await _unitOfWork.ProductOnTaxRepository.GetByExpressionWithIncludesAsync(x => x.TaxVersionId == taxId && x.ProductVersionId == productId);
                        if (existingLink is null)
                        {
                            var link = new ProductOnTax
                            {
                                TaxVersionId = taxId,
                                ProductVersionId = productId,
                                StartDate = DateTime.UtcNow,
                                EndDate = null
                            };

                            await _unitOfWork.ProductOnTaxRepository.CreateAsync(link, cancellationToken);
                        }
                    }
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UnlinkTaxFromProductsAsync(int taxId, int[] productIdList, CancellationToken cancellationToken)
        {
            var tax = await _unitOfWork.TaxRepository.GetByIdAsync(taxId, cancellationToken);
            if (tax is not null && tax.IsDeleted == false)
            {
                foreach (var productId in productIdList)
                {
                    var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId, cancellationToken);
                    if (product is not null && product.IsDeleted == false)
                    {
                        var existingLink = await _unitOfWork.ProductOnTaxRepository.GetByExpressionWithIncludesAsync(x => x.TaxVersionId == taxId && x.ProductVersionId == productId);
                        if (existingLink is not null)
                        {
                            _unitOfWork.ProductOnTaxRepository.Delete(existingLink);
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
