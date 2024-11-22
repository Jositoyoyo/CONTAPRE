namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_CONCEPTO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_CONCEPTO()
        {
            PRE_PRESUPUESTO = new HashSet<PRE_PRESUPUESTO>();
            PRE_SUBCONCEPTO = new HashSet<PRE_SUBCONCEPTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int CON_CODIGO { get; set; }

        public byte? CON_NUMERO { get; set; }

        [StringLength(120)]
        public string CON_DESCRIPCION { get; set; }

        [Required]
        [StringLength(1)]
        public string CON_I_G { get; set; }

        public byte? ART_CODIGO { get; set; }

        public int? CUEP_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CON_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_ARTICULO PRE_ARTICULO { get; set; }

        public virtual PRE_CUENTA_PGCP PRE_CUENTA_PGCP { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PRESUPUESTO> PRE_PRESUPUESTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_SUBCONCEPTO> PRE_SUBCONCEPTO { get; set; }

        [NotMapped]
        public string CONCEPTO_LABEL { get; set; }

        [NotMapped]
        public int CON_CODIGO_AUX { get; set; }

        #endregion
    }
}