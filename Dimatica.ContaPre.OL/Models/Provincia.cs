namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class Provincia
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Provincia()
        {
            PRE_PROVEEDOR = new HashSet<PRE_PROVEEDOR>();
        }

        #endregion

        #region Public Properties

        [Key]
        public byte ProvID { get; set; }

        public byte CodProvincia { get; set; }

        [Column("Provincia")]
        [Required]
        [StringLength(26)]
        public string Provincia1 { get; set; }

        public int CodPaisID { get; set; }

        [StringLength(25)]
        public string ComAutonoma { get; set; }

        public virtual Pais Pais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PROVEEDOR> PRE_PROVEEDOR { get; set; }

        [NotMapped]
        public int ProvIdInt { get; set; }

        #endregion
    }
}