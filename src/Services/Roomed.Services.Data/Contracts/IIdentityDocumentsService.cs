namespace Roomed.Services.Data.Contracts
{
    using Roomed.Services.Data.Common;
    using Roomed.Services.Data.Dtos.IdentityDocument;
    using Roomed.Services.Data.Dtos.Profile;
    using Roomed.Services.Data.Dtos.Reservation;

    /// <summary>
    /// This interface is used to state and document the identity documents data service functionality.
    /// </summary>
    public interface IIdentityDocumentsService
    {
        /// <summary>
        /// This method asynchronously returns the identity document with the corresponding id.
        /// </summary>
        /// <param name="id">The id of the identity document.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns the <see cref="IdentityDocumentDto"/> with the given id.</returns>
        public Task<IdentityDocumentDto> GetAsync(Guid id, QueryOptions<IdentityDocumentDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously returns a collection of all identity documents.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <returns>Returns a collection of <see cref="IdentityDocumentDto"/> objects.</returns>
        public Task<ICollection<IdentityDocumentDto>> GetAllAsync(QueryOptions<IdentityDocumentDto>? queryOptions = null);

        /// <summary>
        /// This method asynchronously creates a new identity document entity in the database.
        /// </summary>
        /// <param name="identityDocumentDto">The document to be created.</param>
        /// <returns>Returns the id of the newly created entity.</returns>
        public Task<Guid> CreateAsync(IdentityDocumentDto identityDocumentDto);

        /// <summary>
        /// This method asynchronously updates the identity document with the given id with the values of the new one.
        /// </summary>
        /// <param name="id">The id of the identity document to be updated.</param>
        /// <param name="identityDocumentDto">The new identity document.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task EditAsync(Guid id, IdentityDocumentDto identityDocumentDto);

        /// <summary>
        /// This method asynchronously deletes the identity document with the provided id.
        /// </summary>
        /// <param name="id">The id of the identity document to be deleted.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        public Task DeleteAsync(Guid id);
    }
}
