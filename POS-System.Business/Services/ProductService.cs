using AutoMapper;
using POS_System.Business.Dtos.ProductDtos;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<GetProductDto?>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllByExpressionAsync(x => !x.IsDeleted, cancellationToken);
            var productDtos = _mapper.Map<List<GetProductDto>>(products);

            return productDtos;
        }

        public async Task<GetProductDto?> GetProductByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByExpressionWithIncludesAsync(
                x => x.ProductId == productId && !x.IsDeleted,
                cancellationToken
            );

            var productDto = _mapper.Map<GetProductDto>(product);

            return productDto;
        }

        public async Task<IEnumerable<GetProductDto?>> GetProductVersionsByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllByExpressionWithIncludesAsync(
                x => x.ProductId == productId,
                cancellationToken
            );

            var productDtos = _mapper.Map<List<GetProductDto>>(products);

            return productDtos;
        }

        public async Task<GetProductDto> CreateProductAsync(CreateProductDto? productDto, CancellationToken cancellationToken)
        {
            var newProductId = await _unitOfWork.ProductRepository.GetMaxProductIdAsync(cancellationToken) + 1;

            var product = _mapper.Map<Product>(productDto);

            product.ProductId = newProductId;
            product.Version = DateTime.Now;
            product.IsDeleted = false;

            await _unitOfWork.ProductRepository.CreateAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<GetProductDto>(product);
            return responseProductDto;
        }

        public async Task<GetProductDto> UpdateProductByProductIdAsync(int productId, CreateProductDto? productDto, CancellationToken cancellationToken)
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

            var responseProductDto = _mapper.Map<GetProductDto>(newProduct);
            return responseProductDto;
        }

        public async Task<GetProductDto> DeleteProductByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByExpressionWithIncludesAsync(
                x => x.ProductId == productId && !x.IsDeleted,
                cancellationToken
            );

            product.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var responseProductDto = _mapper.Map<GetProductDto>(product);
            return responseProductDto;
        }
    }
}
