namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_BACKUP_HOJA_ARQUEO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HOJ_CODIGO { get; set; }

        public short? HOJ_ANO { get; set; }

        public int? HOJ_NUMERO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HOJ_FECHA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HOJ_FECHA_SICAI { get; set; }

        public int? HOJ_NUMERO_SICAI { get; set; }

        public bool? HOJ_ARQUEO50 { get; set; }

        public int? CUE_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HOJ_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public int? HArqueoID { get; set; }
    }
}
