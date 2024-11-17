﻿namespace POS_System.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    ICardDetailsRepository CardDetailsRepository { get; }
    ICartDiscountRepository CartDiscountRepository { get; }
    ICartRepository CartRepository { get; }
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

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}