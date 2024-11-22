namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_TIPO_PAGO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_TIPO_PAGO()
        {
            PRE_DOCUMENTO_CONTABLE = new HashSet<PRE_DOCUMENTO_CONTABLE>();
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
        }

        #endregion

        #region Public Properties

        [Key]
        public byte TIPP_CODIGO { get; set; }

        [StringLength(60)]
        public string TIPP_DESCRIPCION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_CONTABLE> PRE_DOCUMENTO_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        [NotMapped]
        public string DisplayLabel
        {
            get
            {
                if (this.TIPP_CODIGO_AUX == -1)
                {
                    return "< Seleccione >";
                }

                return $"{this.TIPP_CODIGO} - {this.TIPP_DESCRIPCION}";
            }
        }

        [NotMapped]
        public int TIPP_CODIGO_AUX { get; set; }

        #endregion
    }
}