namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_CENTRO_COSTE
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_CENTRO_COSTE()
        {
            PRE_CUENTA_RESTRINGIDA = new HashSet<PRE_CUENTA_RESTRINGIDA>();
            PRE_EXP_CENTRO_COSTE = new HashSet<PRE_EXP_CENTRO_COSTE>();
            PRE_EXPEDIENTE_CONTABLE = new HashSet<PRE_EXPEDIENTE_CONTABLE>();
        }

        #endregion

        #region Public Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CEN_CODIGO { get; set; }

        [StringLength(50)]
        public string CEN_DESCRIPCION { get; set; }

        public int? CEN_AREA_ORIGEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CUENTA_RESTRINGIDA> PRE_CUENTA_RESTRINGIDA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_CENTRO_COSTE> PRE_EXP_CENTRO_COSTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_CONTABLE> PRE_EXPEDIENTE_CONTABLE { get; set; }

        public string DisplayLabel
        {
            get
            {
                if (this.CEN_CODIGO == -1)
                {
                    return "< Seleccione >";
                }

                var label = $"{this.CEN_CODIGO}";

                if (!string.IsNullOrWhiteSpace(this.CEN_DESCRIPCION))
                {
                    label = $"{label} - {this.CEN_DESCRIPCION}";
                }

                return label;
            }
        }

        public string DisplayLabelArea
        {
            get
            {
                if (this.CEN_CODIGO == -1)
                {
                    return "< Seleccione >";
                }

                var label = string.Empty;

                if (!string.IsNullOrWhiteSpace(this.CEN_DESCRIPCION))
                {
                    label = $"{this.CEN_DESCRIPCION}";
                }

                if (this.CEN_AREA_ORIGEN != null)
                {
                    label = $"{label} - {this.CEN_AREA_ORIGEN}";
                }

                return label;
            }
        }

        #endregion
    }
}