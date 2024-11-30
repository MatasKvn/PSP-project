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
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
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
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
            }

            var productDtos = _mapper.Map<List<ProductResponse>>(products);

            return new PagedResponse<ProductResponse?>(totalCount, pageSize, pageNumber, productDtos);
        }

        public async Task<ProductResponse?> GetProductByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByExpressionWithIncludesAsync(
                x => x.ProductId == productId && !x.IsDeleted,
                cancellationToken
            );

            if (product is null)
            {
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
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
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
            }

            var productDtos = _mapper.Map<List<ProductResponse>>(products);

            return productDtos;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest? productDto, CancellationToken cancellationToken)
        {
            if (productDto is null)
            {
                throw new BadRequestException(ApplicationMesssages.BAD_REQUEST_MESSAGE);
            }

            var product = _mapper.Map<Product>(productDto);

            product.Version = DateTime.Now;
            product.IsDeleted = false;

            await _unitOfWork.ProductRepository.CreateAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<ProductResponse>(product);
            return responseProductDto;
        }

        public async Task<ProductResponse> UpdateProductByProductIdAsync(int productId, ProductRequest? productDto, CancellationToken cancellationToken)
        {
            if (productDto is null)
            {
                throw new BadRequestException(ApplicationMesssages.BAD_REQUEST_MESSAGE);
            }

            var currentProduct = await _unitOfWork.ProductRepository.GetByExpressionWithIncludesAsync(
                x => x.ProductId == productId && !x.IsDeleted,
                cancellationToken
            );

            if (currentProduct is null)
            {
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
            }

            currentProduct.IsDeleted = true;

            var newProduct = new Product
            {
                ProductId = productId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                ImageURL = productDto.ImageURL,
                Stock = productDto.Stock,
                Version = DateTime.Now,
                IsDeleted = false
            };

            await _unitOfWork.ProductRepository.CreateAsync(newProduct, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<ProductResponse>(newProduct);
            return responseProductDto;
        }

        public async Task<ProductResponse> DeleteProductByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByExpressionWithIncludesAsync(
                x => x.ProductId == productId && !x.IsDeleted,
                cancellationToken
            );

            if (product is null)
            {
                throw new NotFoundException(ApplicationMesssages.NOT_FOUND_ERROR);
            }

            product.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<ProductResponse>(product);
            return responseProductDto;
        }
    }
}
