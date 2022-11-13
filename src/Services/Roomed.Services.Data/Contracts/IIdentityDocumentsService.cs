namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Dtos.IdentityDocument;

    public interface IIdentityDocumentsService
    {
        /// <summary>
        /// This method asynchronously returns a collection of all identity documents.
        /// </summary>
        /// <returns>Returns a collection of <see cref="IdentityDocumentDto"/> objects.</returns>
        public Task<ICollection<IdentityDocumentDto>> GetAllAsync();
    }
}
