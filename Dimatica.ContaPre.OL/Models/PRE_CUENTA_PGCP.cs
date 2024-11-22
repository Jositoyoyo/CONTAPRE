namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_CUENTA_PGCP
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_CUENTA_PGCP()
        {
            PRE_ARTICULO = new HashSet<PRE_ARTICULO>();
            PRE_CAPITULO = new HashSet<PRE_CAPITULO>();
            PRE_CONCEPTO = new HashSet<PRE_CONCEPTO>();
            PRE_DOCUMENTO_APLICACION = new HashSet<PRE_DOCUMENTO_APLICACION>();
            PRE_EXTRAPRESUPUESTARIA = new HashSet<PRE_EXTRAPRESUPUESTARIA>();
            PRE_SUBCONCEPTO = new HashSet<PRE_SUBCONCEPTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int CUEP_CODIGO { get; set; }

        [Required]
        [StringLength(10)]
        public string CUEP_NUMERO { get; set; }

        [StringLength(80)]
        public string CUEP_DESCRIPCION { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CUEP_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_ARTICULO> PRE_ARTICULO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CAPITULO> PRE_CAPITULO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CONCEPTO> PRE_CONCEPTO { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_APLICACION> PRE_DOCUMENTO_APLICACION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXTRAPRESUPUESTARIA> PRE_EXTRAPRESUPUESTARIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_SUBCONCEPTO> PRE_SUBCONCEPTO { get; set; }

        public string DisplayLabel
        {
            get
            {
                var label = $"{this.CUEP_NUMERO}";

                if (!string.IsNullOrWhiteSpace(this.CUEP_DESCRIPCION))
                {
                    label = $"{label} - {this.CUEP_DESCRIPCION}";
                }

                return label;
            }
        }

        #endregion
    }
}