namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_CAPITULO
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_CAPITULO()
        {
            PRE_ARTICULO = new HashSet<PRE_ARTICULO>();
            PRE_PRESUPUESTO = new HashSet<PRE_PRESUPUESTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte CAP_CODIGO { get; set; }

        public byte CAP_NUMERO { get; set; }

        [StringLength(60)]
        public string CAP_DESCRIPCION { get; set; }

        [Required]
        [StringLength(1)]
        public string CAP_I_G { get; set; }

        public byte? PRO_CODIGO { get; set; }

        public int? CUEP_CODIGO { get; set; }

        public bool? CAP_ACTIVO_PRE { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CAP_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_ARTICULO> PRE_ARTICULO { get; set; }

        public virtual PRE_CUENTA_PGCP PRE_CUENTA_PGCP { get; set; }

        public virtual PRE_PROGRAMA PRE_PROGRAMA { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PRESUPUESTO> PRE_PRESUPUESTO { get; set; }

        [NotMapped]
        public string CAPITULO_LABEL { get; set; }

        [NotMapped]
        public int CAP_CODIGO_AUX { get; set; }

        #endregion
    }
}