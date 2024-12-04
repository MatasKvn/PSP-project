using AutoMapper;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResponse?>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllByExpressionAsync(x => !x.IsDeleted, cancellationToken);
            var productDtos = _mapper.Map<List<ProductResponse>>(products);

            return productDtos;
        }

        public async Task<ProductResponse?> GetProductByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByExpressionWithIncludesAsync(
                x => x.ProductId == productId && !x.IsDeleted,
                cancellationToken
            );

            var productDto = _mapper.Map<ProductResponse>(product);

            return productDto;
        }

        public async Task<IEnumerable<ProductResponse?>> GetProductVersionsByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllByExpressionWithIncludesAsync(
                x => x.ProductId == productId,
                cancellationToken
            );

            var productDtos = _mapper.Map<List<ProductResponse>>(products);

            return productDtos;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest? productDto, CancellationToken cancellationToken)
        {
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
            var currentProduct = await _unitOfWork.ProductRepository.GetByExpressionWithIncludesAsync(
                x => x.ProductId == productId && !x.IsDeleted,
                cancellationToken
            );

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

            product.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<ProductResponse>(product);
            return responseProductDto;
        }
    }
}
