namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_ARTICULO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_ARTICULO()
        {
            PRE_CONCEPTO = new HashSet<PRE_CONCEPTO>();
            PRE_PRESUPUESTO = new HashSet<PRE_PRESUPUESTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ART_CODIGO { get; set; }

        public byte ART_NUMERO { get; set; }

        [StringLength(100)]
        public string ART_DESCRIPCION { get; set; }

        [Required]
        [StringLength(1)]
        public string ART_I_G { get; set; }

        public byte? CAP_CODIGO { get; set; }

        public int? CUEP_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ART_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_CAPITULO PRE_CAPITULO { get; set; }

        public virtual PRE_CUENTA_PGCP PRE_CUENTA_PGCP { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CONCEPTO> PRE_CONCEPTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PRESUPUESTO> PRE_PRESUPUESTO { get; set; }

        [NotMapped]
        public string ARTICULO_LABEL { get; set; }

        [NotMapped]
        public int ART_CODIGO_AUX { get; set; }

        #endregion
    }
}