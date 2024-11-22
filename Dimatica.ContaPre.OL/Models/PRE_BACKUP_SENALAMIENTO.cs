namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_BACKUP_SENALAMIENTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SEN_CODIGO { get; set; }

        public short? SEN_ANO { get; set; }

        public int? SEN_NUMERO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SEN_FECHA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SEN_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }
    }
}
