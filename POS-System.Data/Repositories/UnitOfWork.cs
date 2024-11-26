using POS_System.Data.Database;
using POS_System.Data.Repositories.Interfaces;

namespace POS_System.Data.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext,
                        ICardDetailsRepository cardDetailsRepository,
                        ICartDiscountRepository cartDiscountRepository,
                        ICartRepository cartRepository,
                        IEmployeeRepository employeeRepository,
                        IGiftCardDetailsRepository giftCardDetailsRepository,
                        IGiftCardRepository giftCardRepository,
                        IItemDiscountRepository itemDiscountRepository,
                        IProductModificationRepository productModificationRepository,
                        IProductRepository productRepository,
                        IServiceRepository serviceRepository,
                        IServiceReservationRepository serviceReservationRepository,
                        ITaxRepository taxRepository,
                        ITimeSlotRepository timeSlotRepository,
                        ITransactionRepository transactionRepository,
                        IProductOnTaxRepository productOnTaxRepository,
                        IServiceOnTaxRepository serviceOnTaxRepository) : IUnitOfWork
{
    public ICardDetailsRepository CardDetailsRepository { get; } = cardDetailsRepository;
    public ICartDiscountRepository CartDiscountRepository { get; } = cartDiscountRepository;
    public ICartRepository CartRepository { get; } = cartRepository;
    public IEmployeeRepository EmployeeRepository { get; } = employeeRepository;
    public IGiftCardDetailsRepository GiftCardDetailsRepository { get; } = giftCardDetailsRepository;
    public IGiftCardRepository GiftCardRepository { get; } = giftCardRepository;
    public IItemDiscountRepository ItemDiscountRepository { get; } = itemDiscountRepository;
    public IProductModificationRepository ProductModificationRepository { get; } = productModificationRepository;
    public IProductRepository ProductRepository { get; } = productRepository;
    public IServiceRepository ServiceRepository { get; } = serviceRepository;
    public IServiceReservationRepository ServiceReservationRepository { get; } = serviceReservationRepository;
    public ITaxRepository TaxRepository { get; } = taxRepository;
    public ITimeSlotRepository TimeSlotRepository { get; } = timeSlotRepository;
    public ITransactionRepository TransactionRepository { get; } = transactionRepository;
    public IProductOnTaxRepository ProductOnTaxRepository { get; } = productOnTaxRepository;
    public IServiceOnTaxRepository ServiceOnTaxRepository { get; } = serviceOnTaxRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}