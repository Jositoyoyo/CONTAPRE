namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_EXTRAPRESUPUESTARIA
    {
        #region Fields

        private string cuepNumero;

        #endregion

        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_EXTRAPRESUPUESTARIA()
        {
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int EXTRAPRE_CODIGO { get; set; }

        public int? EXTRAPRE_NUMERO { get; set; }

        [StringLength(60)]
        public string EXTRAPRE_DESCRIPCION { get; set; }

        public int? CUEP_CODIGO { get; set; }

        public virtual PRE_CUENTA_PGCP PRE_CUENTA_PGCP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        [NotMapped]
        public string CUEP_NUMERO
        {
            get
            {
                return this.cuepNumero ?? (this.cuepNumero = string.Empty);
            }

            set
            {
                this.cuepNumero = value;
            }
        }

        [NotMapped]
        public string DisplayLabel
        {
            get
            {
                if (this.EXTRAPRE_CODIGO == -1)
                {
                    return "< Seleccione >";
                }

                if (this.EXTRAPRE_CODIGO == -2)
                {
                    return "Todas extrapresup. (Excepto especiales)";
                }

                return $"{this.EXTRAPRE_NUMERO} - {this.EXTRAPRE_DESCRIPCION}";
            }
        }

        #endregion
    }
}