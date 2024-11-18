using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDto?>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllByExpressionAsync(x => !x.IsDeleted, cancellationToken);
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId, cancellationToken);
            //var productDto = _mapper.Map<ProductDto>(product);

            //return productDto;
        }

        public async Task AddProductAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var product = _mapper.Map<Product>(productDto);
            //await _unitOfWork.ProductRepository.CreateAsync(product, cancellationToken);
        }

        public async Task RemoveProductAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
