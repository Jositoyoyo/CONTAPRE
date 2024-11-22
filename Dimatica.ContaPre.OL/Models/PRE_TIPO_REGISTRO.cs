namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_TIPO_REGISTRO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_TIPO_REGISTRO()
        {
            PRE_TESORERIA = new HashSet<PRE_TESORERIA>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int TIPR_CODIGO { get; set; }

        [StringLength(20)]
        public string TIPR_DESCRIPCION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA> PRE_TESORERIA { get; set; }

        [NotMapped]
        public string DisplayLabel
        {
            get
            {
                if (this.TIPR_CODIGO == -1)
                {
                    return "< Seleccione >";
                }

                return $"{this.TIPR_CODIGO} - {this.TIPR_DESCRIPCION}";
            }
        }

        #endregion
    }
}