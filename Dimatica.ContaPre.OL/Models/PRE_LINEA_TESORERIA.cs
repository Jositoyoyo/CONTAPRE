namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_LINEA_TESORERIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_LINEA_TESORERIA()
        {
            PRE_TESORERIA = new HashSet<PRE_TESORERIA>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LIN_NUMERO { get; set; }

        [StringLength(60)]
        public string LIN_DESCRIPCION { get; set; }

        [StringLength(60)]
        public string LIN_ORIGEN_DESCRIPCION { get; set; }

        public int? LIN_ORIGEN_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LIN_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA> PRE_TESORERIA { get; set; }
    }
}
