namespace Roomed.Services.Data.Dtos.IdentityDocument
{
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Mapping;

    public class IdentityDocumentDto : IMapFrom<Roomed.Data.Models.IdentityDocument>
    {
        public Guid Id { get; set; }

        public IdentityDocumentType Type { get; set; }

        public string NameInDocument { get; set; }

        public string DocumentNumber { get; set; }

        public DateOnly ValidFrom { get; set; }

        public DateOnly ValidUntil { get; set; }
    }
}
