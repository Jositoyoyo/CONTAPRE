namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_BACKUP_SENALAMIENTO_DOCUMENTO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEND_CODIGO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEN_CODIGO { get; set; }

        public int? DOC_CODIGO { get; set; }

        public int? EXP_EXTRAP_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SEND_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }
    }
}
