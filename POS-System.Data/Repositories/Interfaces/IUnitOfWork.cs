using POS_System.Domain.Entities;

namespace POS_System.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    ICartDiscountRepository CartDiscountRepository { get; }
    ICartRepository CartRepository { get; }
    ICartItemRepository CartItemRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IGiftCardDetailsRepository GiftCardDetailsRepository { get; }
    IGiftCardRepository GiftCardRepository { get; }
    IItemDiscountRepository ItemDiscountRepository { get; }
    IProductModificationRepository ProductModificationRepository { get; }
    IProductRepository ProductRepository { get; }
    IServiceRepository ServiceRepository { get; }
    IServiceReservationRepository ServiceReservationRepository { get; }
    ITaxRepository TaxRepository { get; }
    ITimeSlotRepository TimeSlotRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    IBusinessDetailRepository BusinessDetailRepository { get; }
    IGenericManyToManyRepository<Product, Tax, ProductOnTax> ProductOnTaxRepository { get; }
    IGenericManyToManyRepository<Service, Tax, ServiceOnTax> ServiceOnTaxRepository { get; }
    IGenericManyToManyRepository<Product, ItemDiscount, ProductOnItemDiscount> ProductOnItemDiscountRepository { get; }
    IGenericManyToManyRepository<Service, ItemDiscount, ServiceOnItemDiscount> ServiceOnItemDiscountRepository { get; }
    IGenericManyToManyRepository<ProductModification, CartItem, ProductModificationOnCartItem> ProductModificationOnCartItemRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
