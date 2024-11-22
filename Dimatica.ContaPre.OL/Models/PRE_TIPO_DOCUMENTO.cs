namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_TIPO_DOCUMENTO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_TIPO_DOCUMENTO()
        {
            PRE_DOCUMENTO_CONTABLE = new HashSet<PRE_DOCUMENTO_CONTABLE>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int TIPD_CODIGO { get; set; }

        public int? TIPD_CLAVE { get; set; }

        [StringLength(20)]
        public string TIPD_NOMBRE_CORTO { get; set; }

        [StringLength(120)]
        public string TIPD_DESCRIPCION { get; set; }

        public bool? TIPD_POSITIVO { get; set; }

        [StringLength(1)]
        public string TIPD_I_G { get; set; }

        public bool? TIPD_FASE_RC_G { get; set; }

        public bool? TIPD_FASE_AD_G { get; set; }

        public bool? TIPD_FASE_O_G { get; set; }

        public bool? TIPD_FASE_P_G { get; set; }

        public bool? TIPD_FASE_DR_I { get; set; }

        public bool? TIPD_FASE_MI_I { get; set; }

        public bool? TIPD_FASE_MIsinDR_I { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_CONTABLE> PRE_DOCUMENTO_CONTABLE { get; set; }

        [NotMapped]
        public string SingLabel
        {
            get
            {
                return (bool)this.TIPD_POSITIVO ? "+" : "-";
            }
        }

        [NotMapped]
        public string DisplayLabel
        {
            get
            {
                return this.TIPD_CODIGO == -1 ? "< Seleccione >" : $"{this.TIPD_CLAVE} {this.SingLabel} {this.TIPD_NOMBRE_CORTO}";
            }
        }

        #endregion
    }
}