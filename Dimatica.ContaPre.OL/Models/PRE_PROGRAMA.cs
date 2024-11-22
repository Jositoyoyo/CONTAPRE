namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_PROGRAMA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_PROGRAMA()
        {
            PRE_CAPITULO = new HashSet<PRE_CAPITULO>();
            PRE_EXPEDIENTE_CONTABLE = new HashSet<PRE_EXPEDIENTE_CONTABLE>();
            PRE_PRESUPUESTO = new HashSet<PRE_PRESUPUESTO>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PRO_CODIGO { get; set; }

        [Required]
        [StringLength(10)]
        public string PRO_NUMERO { get; set; }

        [StringLength(60)]
        public string PRO_DESCRIPCION { get; set; }

        public bool? PRO_POR_DEFECTO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PRO_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_CAPITULO> PRE_CAPITULO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_CONTABLE> PRE_EXPEDIENTE_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_PRESUPUESTO> PRE_PRESUPUESTO { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public int PRO_CODIGO_AUX { get; set; }
    }
}
