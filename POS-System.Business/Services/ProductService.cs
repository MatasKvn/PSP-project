using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Common.Constants;
using POS_System.Common.Exceptions;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper, IManyToManyService<Product, Tax, ProductOnTax> _productOnTaxService, IManyToManyService<Product, ItemDiscount, ProductOnItemDiscount> _productOnItemDiscountService, IProductModificationService _productModification) : IProductService
    {
        public async Task<PagedResponse<ProductResponse?>> GetProductsAsync(int pageSize, int pageNumber, bool? onlyActive, CancellationToken cancellationToken)
        {
            var (products, totalCount) = await _unitOfWork.ProductRepository.GetByExpressionWithPaginationAsync(
                onlyActive is null ? null : x => x.IsDeleted != onlyActive,
                pageSize,
                pageNumber,
                cancellationToken
            );

            if (products is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var productDtos = _mapper.Map<List<ProductResponse>>(products);

            return new PagedResponse<ProductResponse?>(totalCount, pageSize, pageNumber, productDtos);
        }

        public async Task<ProductResponse?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var productDto = _mapper.Map<ProductResponse>(product);

            return productDto;
        }

        public async Task<IEnumerable<ProductResponse?>> GetProductVersionsByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllByExpressionWithIncludesAsync(
                x => x.ProductId == productId,
                cancellationToken
            );

            if (products is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            var productDtos = _mapper.Map<List<ProductResponse>>(products);

            return productDtos;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest? productDto, CancellationToken cancellationToken)
        {
            if (productDto is null)
            {
                throw new BadRequestException(ApplicationMessages.BAD_REQUEST_MESSAGE);
            }

            var product = _mapper.Map<Product>(productDto);

            product.Version = DateTime.UtcNow;
            product.IsDeleted = false;

            await _unitOfWork.ProductRepository.CreateAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<ProductResponse>(product);
            return responseProductDto;
        }

        public async Task<ProductResponse> UpdateProductByIdAsync(int id, ProductRequest? productDto, CancellationToken cancellationToken)
        {
            if (productDto is null)
            {
                throw new BadRequestException(ApplicationMessages.BAD_REQUEST_MESSAGE);
            }

            var currentProduct = await _unitOfWork.ProductRepository.GetByIdAsync(id, cancellationToken);

            if (currentProduct is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            currentProduct.IsDeleted = true;

            var newProduct = _mapper.Map<Product>(productDto);
            newProduct.ProductId = currentProduct.ProductId;
            newProduct.Version = DateTime.UtcNow;
            newProduct.IsDeleted = false;

            await _unitOfWork.ProductRepository.CreateAsync(newProduct, cancellationToken);
            await UpdateForeignKeysAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<ProductResponse>(newProduct);

            await _productOnTaxService.RelinkItemToItem(_unitOfWork.ProductOnTaxRepository, id, newProduct.Id, true, cancellationToken);
            await _productOnItemDiscountService.RelinkItemToItem(_unitOfWork.ProductOnItemDiscountRepository, id, newProduct.Id, true, cancellationToken);

            return responseProductDto;
        }

        public async Task<ProductResponse> DeleteProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByExpressionWithIncludesAsync(
                x => x.Id == id && !x.IsDeleted,
                cancellationToken
            );

            if (product is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            product.IsDeleted = true;

            await _productOnTaxService.MarkActiveLinksDeletedAsync(_unitOfWork.ProductOnTaxRepository, id, true, cancellationToken);
            await _productOnItemDiscountService.MarkActiveLinksDeletedAsync(_unitOfWork.ProductOnItemDiscountRepository, id, true, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<ProductResponse>(product);
            return responseProductDto;
        }

        private async Task UpdateForeignKeysAsync(int id, CancellationToken cancellationToken)
        {
            var prodMods = await _unitOfWork.ProductModificationRepository.GetAllByExpressionWithIncludesAsync(
                x => x.ProductVersionId == id && !x.IsDeleted,
                cancellationToken
            );

            if (prodMods is null)
            {
                throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            }

            foreach ( var prodMod in prodMods)
            {
                if (prodMods is null)
                {
                    throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
                }

                var prodModDto = _mapper.Map<ProductModificationRequest>(prodMod);

                await _productModification.UpdateProductModificationByIdAsync(prodMod.Id, prodModDto, cancellationToken);
            }
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsLinkedToTaxId(int taxId, DateTime? timeStamp, CancellationToken cancellationToken)
        {
            IEnumerable<int> productLinkIds;
            IList<Product> products = new List<Product>();

            productLinkIds = await _productOnTaxService.GetLinkIdsAsync(_unitOfWork.ProductOnTaxRepository, taxId, false, timeStamp, cancellationToken);

            foreach (var productId in productLinkIds)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId, cancellationToken);

                if (product is not null)
                    products.Add(product);
            }

            var productDtos = _mapper.Map<List<ProductResponse>>(products);
            return productDtos;
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsLinkedToItemDiscountId(int itemDiscountId, DateTime? timeStamp, CancellationToken cancellationToken)
        {
            IEnumerable<int> productLinkIds;
            IList<Product> products = new List<Product>();

            var itemDiscount = await _unitOfWork.ItemDiscountRepository.GetByIdAsync(itemDiscountId, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            productLinkIds = await _productOnItemDiscountService.GetLinkIdsAsync(_unitOfWork.ProductOnItemDiscountRepository, itemDiscountId, false, timeStamp, cancellationToken);

            foreach (var productId in productLinkIds)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId, cancellationToken);

                if (product is not null)
                {
                    if ((itemDiscount.StartDate is null && itemDiscount.EndDate is null) || (itemDiscount.StartDate <= timeStamp && itemDiscount.EndDate >= timeStamp))
                        products.Add(product);
                }
            }

            var productDtos = _mapper.Map<List<ProductResponse>>(products);
            return productDtos;
        }
    }
}
