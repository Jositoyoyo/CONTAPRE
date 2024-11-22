namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_SUBCONCEPTO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_SUBCONCEPTO()
        {
            PRE_PRESUPUESTO = new HashSet<PRE_PRESUPUESTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte SUB_CODIGO { get; set; }

        [Required]
        [StringLength(2)]
        public string SUB_NUMERO { get; set; }

        [StringLength(100)]
        public string SUB_DESCRIPCION { get; set; }

        [Required]
        [StringLength(1)]
        public string SUB_I_G { get; set; }

        public int? CON_CODIGO { get; set; }

        public int? CUEP_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SUB_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_CONCEPTO PRE_CONCEPTO { get; set; }

        public virtual PRE_CUENTA_PGCP PRE_CUENTA_PGCP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PRESUPUESTO> PRE_PRESUPUESTO { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public string SUBCONCEPTO_LABEL { get; set; }

        [NotMapped]
        public int SUB_CODIGO_AUX { get; set; }

        #endregion
    }
}