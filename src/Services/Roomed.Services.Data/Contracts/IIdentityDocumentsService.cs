namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Dtos.IdentityDocument;

    /// <summary>
    /// This interface is used to state and document the identity documents data service functionality.
    /// </summary>
    public interface IIdentityDocumentsService
    {
        /// <summary>
        /// This method asynchronously returns a collection of all identity documents.
        /// </summary>
        /// <returns>Returns a collection of <see cref="IdentityDocumentDto"/> objects.</returns>
        public Task<ICollection<IdentityDocumentDto>> GetAllAsync();
    }
}
