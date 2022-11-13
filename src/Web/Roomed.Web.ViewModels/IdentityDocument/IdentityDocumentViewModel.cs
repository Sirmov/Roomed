﻿namespace Roomed.Web.ViewModels.IdentityDocument
{
    using Roomed.Data.Models.Enums;
    using Roomed.Services.Data.Dtos.IdentityDocument;
    using Roomed.Services.Mapping;

    /// <summary>
    /// This is a <see cref="Roomed.Data.Models.IdentityDocument"/> view model.
    /// </summary>
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
