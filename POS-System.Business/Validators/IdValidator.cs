using POS_System.Common.Exceptions;

namespace POS_System.Business.Validators;

public static class IdValidator
{
    public static void ValidateId(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException($"Invalid ID {id}.");
        }
    }
}
