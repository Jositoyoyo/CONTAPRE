namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_TIPO_MODIFICACION_CREDITO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_TIPO_MODIFICACION_CREDITO()
        {
            PRE_MODIFICACION_CREDITO = new HashSet<PRE_MODIFICACION_CREDITO>();
            PRE_MODIFICACION_CREDITO1 = new HashSet<PRE_MODIFICACION_CREDITO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int TIPM_CODIGO { get; set; }

        public int? TIPM_NUMERO { get; set; }

        [StringLength(60)]
        public string TIPM_DESCRIPCION { get; set; }

        [StringLength(1)]
        public string TIPM_I_G { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIFICACION_CREDITO> PRE_MODIFICACION_CREDITO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIFICACION_CREDITO> PRE_MODIFICACION_CREDITO1 { get; set; }

        [NotMapped]
        public string DisplayLabel
        {
            get
            {
                return $"{this.TIPM_NUMERO} - {this.TIPM_DESCRIPCION}";
            }
        }

        #endregion
    }
}