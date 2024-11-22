namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_BACKUP_MODIF_CREDITO_PRESUPUESTO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PRE_CODIGO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MOD_CODIGO { get; set; }

        [Column(TypeName = "money")]
        public decimal? MODP_IMPORTE { get; set; }

        public bool? MODP_POSITIVO { get; set; }

        [StringLength(1)]
        public string MODP_I_G { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MODP_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }
    }
}
