namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_BACKUP_DOCUMENTO_APLICACION
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DOCA_CODIGO { get; set; }

        public int? DOC_CODIGO { get; set; }

        [StringLength(1)]
        public string DOCA_I_G { get; set; }

        [Column(TypeName = "money")]
        public decimal? DOCA_IMPORTE { get; set; }

        public int? CUEP_CODIGO { get; set; }

        public int? PRE_CODIGO { get; set; }

        public short? DOCA_ANO_PRESUPUESTO { get; set; }

        public bool? DOCA_MARCADO_HOJA_ARQUEO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DOCA_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }
    }
}
