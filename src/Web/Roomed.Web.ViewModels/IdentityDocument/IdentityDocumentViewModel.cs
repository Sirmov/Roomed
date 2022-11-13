namespace Roomed.Web.ViewModels.IdentityDocument
{
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.IdentityDocument;
    using Roomed.Services.Mapping;

    public class IdentityDocumentViewModel : IMapFrom<IdentityDocumentDto>
    {
        public Guid Id { get; set; }

        public IdentityDocumentType Type { get; set; }

        public string NameInDocument { get; set; }

        public string DocumentNumber { get; set; }

        public DateOnly ValidFrom { get; set; }

        public DateOnly ValidUntil { get; set; }
    }
}
