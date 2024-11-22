namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_BACKUP_MODIFICACION_CREDITO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MOD_CODIGO { get; set; }

        public short? MOD_ANO_PRESUPUESTO { get; set; }

        public int? MOD_NUMERO_ORDEN { get; set; }

        public int? EA_CODIGO { get; set; }

        public int? EXP_CODIGO { get; set; }

        public byte? MON_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MOD_FECHA_PROPUESTA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MOD_FECHA_ASIENTO_DIARIO { get; set; }

        [StringLength(120)]
        public string MOD_DESCRIPCION { get; set; }

        public bool? MOD_EJECUTADA { get; set; }

        public bool? MOD_CREADO_EXPEDIENTE { get; set; }

        public int? TIPM_CODIGO_I { get; set; }

        public int? TIPM_CODIGO_G { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? MOD_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }
    }
}
