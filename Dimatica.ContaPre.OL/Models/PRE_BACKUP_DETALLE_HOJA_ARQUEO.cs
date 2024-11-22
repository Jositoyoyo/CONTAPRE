namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_BACKUP_DETALLE_HOJA_ARQUEO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DET_CODIGO { get; set; }

        public int? HOJ_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DET_FECHA_APUNTE { get; set; }

        [Column(TypeName = "money")]
        public decimal? DET_IMPORTE { get; set; }

        public int? DOC_CODIGO { get; set; }

        public int? EXP_EXTRAP_CODIGO { get; set; }

        public int? DET_NUMERO_EXPEDIENTE { get; set; }

        public short? EXP_ANO_PRESUPUESTO { get; set; }

        public int? LIN_NUMERO { get; set; }

        public byte? MON_CODIGO { get; set; }

        public bool? DET_ASIGNADO { get; set; }

        public bool? DET_ASIGNADO50 { get; set; }

        public bool? MARCADO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DET_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public int? HArqueoDetalleID { get; set; }
    }
}
