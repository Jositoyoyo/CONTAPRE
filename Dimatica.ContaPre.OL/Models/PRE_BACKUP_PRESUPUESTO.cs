namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_BACKUP_PRESUPUESTO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PRE_CODIGO { get; set; }

        public byte? CAP_CODIGO { get; set; }

        public byte? ART_CODIGO { get; set; }

        public int? CON_CODIGO { get; set; }

        public byte? SUB_CODIGO { get; set; }

        public short? PRE_ANO { get; set; }

        [StringLength(1)]
        public string PRE_I_G { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool PRE_NO_VINCULANTE { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool PRE_CERRADO { get; set; }

        [Column(TypeName = "money")]
        public decimal? PRE_IMPORTE { get; set; }

        [Column(TypeName = "money")]
        public decimal? PRE_IMPORTE_MODIFICACIONES { get; set; }

        public byte? PRO_CODIGO { get; set; }

        public byte? MON_CODIGO { get; set; }

        public bool? PRE_OCULTO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PRE_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }
    }
}
