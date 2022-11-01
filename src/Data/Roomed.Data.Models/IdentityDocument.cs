namespace Roomed.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Roomed.Data.Common.Models;
    using Roomed.Data.Models.Enums;

    using static Roomed.Common.DataConstants.IdentityDocument;

    /// <summary>
    /// Identity document entity model. Inherits base deletable model. Has guid id.
    /// </summary>
    public class IdentityDocument : BaseDeletableModel<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDocument"/> class with new guid as id.
        /// </summary>
        public IdentityDocument()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets owner id foreign key.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets identity document type.
        /// </summary>
        [Required]
        [EnumDataType(typeof(IdentityDocumentType))]
        public IdentityDocumentType Type { get; set; }

        /// <summary>
        /// Gets or sets name in document.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(IdentityDocumentNameMaxLength)]
        public string NameInDocument { get; set; }

        /// <summary>
        /// Gets or sets document number.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(IdentityDocumentNumberMaxLength)]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets personal number.
        /// </summary>
        [MaxLength(IdentityDocumentPersonalNumberMaxLength)]
        public string? PersonalNumber { get; set; }

        /// <summary>
        /// Gets or sets identity document country.
        /// </summary>
        [MaxLength(IdentityDocumentCountryMaxLength)]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets identity document birthdate.
        /// </summary>
        [Required]
        public DateOnly Birthdate { get; set; }

        /// <summary>
        /// Gets or sets place of birth.
        /// </summary>
        [Required]
        [MaxLength(IdentityDocumentPlaceOfBirthMaxLength)]
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Gets or sets nationality.
        /// </summary>
        [MaxLength(IdentityDocumentNationalityMaxLength)]
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets valid from.
        /// </summary>
        [Required]
        public DateOnly ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets valid until.
        /// </summary>
        [Required]
        public DateOnly ValidUntil { get; set; }


        /// <summary>
        /// Gets or sets issued by.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(IdentityDocumentIssuedByMaxLength)]
        public string IssuedBy { get; set; }

        // Navigational Properties

        /// <summary>
        /// Gets or sets owner navigational property.
        /// </summary>
        [ForeignKey(nameof(OwnerId))]
        public virtual Profile Owner { get; set; }
    }
}
