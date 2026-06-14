using LibrebooksBlazor.Core.Operations;
using LibrebooksBlazor.Models.Entity.GeneralSpace;

namespace LibrebooksBlazor.Providers.Services;

public interface IVerificationService
{
	Task<(VerificationRequest? Request, string? Code)> AddAsync (VerificationRequest request);
	Task<TransactionResult<VerificationRequest>> VerifyAsync (string subject, string reason, string code);
}
