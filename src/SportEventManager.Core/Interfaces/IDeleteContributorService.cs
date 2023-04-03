using Ardalis.Result;

namespace SportEventManager.Core.Interfaces;

public interface IDeleteContributorService
{
    public Task<Result> DeleteContributor(int contributorId);
}
