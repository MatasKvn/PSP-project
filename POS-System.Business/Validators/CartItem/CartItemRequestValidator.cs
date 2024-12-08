using FluentValidation;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Validators.CartItem
{
    public class CartItemRequestValidator : AbstractValidator<CartItemRequest>
    {
        public CartItemRequestValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty()
                .WithMessage(CartItemValidationMessages.CartIdRequired);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(CartItemValidationConstants.MinQuantity)
                .WithMessage(CartItemValidationMessages.QuantityInvalid);

            RuleFor(x => x)
                .Must(item => ValidateItemType(item))
                .WithMessage(CartItemValidationMessages.ItemTypeMismatch);
        }

        private static bool ValidateItemType(CartItemRequest item)
        {
            if (item.IsProduct)
            {
                return item.ProductVersionId.HasValue && !item.ServiceVersionId.HasValue;
            }
            else
            {
                return !item.ProductVersionId.HasValue && item.ServiceVersionId.HasValue;
            }
        }
    }
}