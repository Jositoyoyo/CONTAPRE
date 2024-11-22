namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_TIPO_EXTRAP
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_TIPO_EXTRAP()
        {
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
        }

        #endregion

        #region Public Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte TIP_EXTRAP_CODIGO { get; set; }

        [StringLength(60)]
        public string TIP_EXTRAP_DESCRIPCION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        [NotMapped]
        public int TIP_EXTRAP_CODIGO_AUX { get; set; }

        #endregion
    }
}