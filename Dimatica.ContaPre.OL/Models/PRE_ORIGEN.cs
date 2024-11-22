namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_ORIGEN
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_ORIGEN()
        {
            PRE_TESORERIA_DOCUMENTO = new HashSet<PRE_TESORERIA_DOCUMENTO>();
            PRE_TESORERIA = new HashSet<PRE_TESORERIA>();
        }

        #endregion

        #region Public Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ORI_CODIGO { get; set; }

        [StringLength(60)]
        public string ORI_DESCRIPCION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA_DOCUMENTO> PRE_TESORERIA_DOCUMENTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA> PRE_TESORERIA { get; set; }

        [NotMapped]
        public int ORI_CODIGO_AUX { get; set; }

        #endregion
    }
}