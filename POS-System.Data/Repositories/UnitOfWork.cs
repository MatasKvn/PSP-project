﻿using POS_System.Data.Database;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Data.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext,
                        ICartDiscountRepository cartDiscountRepository,
                        ICartRepository cartRepository,
                        ICartItemRepository cartItemRepository,
                        IEmployeeRepository employeeRepository,
                        IGiftCardRepository giftCardRepository,
                        IItemDiscountRepository itemDiscountRepository,
                        IProductModificationRepository productModificationRepository,
                        IProductRepository productRepository,
                        IServiceRepository serviceRepository,
                        IServiceReservationRepository serviceReservationRepository,
                        ITaxRepository taxRepository,
                        ITimeSlotRepository timeSlotRepository,
                        ITransactionRepository transactionRepository,
                        IBusinessDetailRepository businessDetailRepository,
                        IGenericManyToManyRepository<Product, Tax, ProductOnTax> productOnTaxRepository,
                        IGenericManyToManyRepository<Service, Tax, ServiceOnTax> serviceOnTaxRepository,
                        IGenericManyToManyRepository<Product, ItemDiscount, ProductOnItemDiscount> productOnItemDiscountRepository,
                        IGenericManyToManyRepository<Service, ItemDiscount, ServiceOnItemDiscount> serviceOnItemDiscountRepository,
                        IGenericManyToManyRepository<ProductModification, CartItem, ProductModificationOnCartItem> productModificationOnCartItemRepository) : IUnitOfWork
{
    public ICartDiscountRepository CartDiscountRepository { get; } = cartDiscountRepository;
    public ICartRepository CartRepository { get; } = cartRepository;
    public ICartItemRepository CartItemRepository { get; } = cartItemRepository;
    public IEmployeeRepository EmployeeRepository { get; } = employeeRepository;
    public IGiftCardRepository GiftCardRepository { get; } = giftCardRepository;
    public IItemDiscountRepository ItemDiscountRepository { get; } = itemDiscountRepository;
    public IProductModificationRepository ProductModificationRepository { get; } = productModificationRepository;
    public IProductRepository ProductRepository { get; } = productRepository;
    public IServiceRepository ServiceRepository { get; } = serviceRepository;
    public IServiceReservationRepository ServiceReservationRepository { get; } = serviceReservationRepository;
    public ITaxRepository TaxRepository { get; } = taxRepository;
    public ITimeSlotRepository TimeSlotRepository { get; } = timeSlotRepository;
    public ITransactionRepository TransactionRepository { get; } = transactionRepository;
    public IBusinessDetailRepository BusinessDetailRepository { get; } = businessDetailRepository;
    public IGenericManyToManyRepository<Product, Tax, ProductOnTax> ProductOnTaxRepository { get; } = productOnTaxRepository;
    public IGenericManyToManyRepository<Service, Tax, ServiceOnTax> ServiceOnTaxRepository { get; } = serviceOnTaxRepository;
    public IGenericManyToManyRepository<Product, ItemDiscount, ProductOnItemDiscount> ProductOnItemDiscountRepository { get; } = productOnItemDiscountRepository;
    public IGenericManyToManyRepository<Service, ItemDiscount, ServiceOnItemDiscount> ServiceOnItemDiscountRepository { get; } = serviceOnItemDiscountRepository;
    public IGenericManyToManyRepository<ProductModification, CartItem, ProductModificationOnCartItem> ProductModificationOnCartItemRepository { get; } = productModificationOnCartItemRepository;
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}