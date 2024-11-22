namespace Dimatica.ContaPre.OL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRE_EXP_CENTRO_COSTE
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EXP_CODIGO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CEN_CODIGO { get; set; }

        public int? EXP_CEN_COD_ACTIVIDAD { get; set; }

        [Column(TypeName = "money")]
        public decimal? EXP_CEN_IMPORTE { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EXP_CEN_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_CENTRO_COSTE PRE_CENTRO_COSTE { get; set; }

        public virtual PRE_EXPEDIENTE_CONTABLE PRE_EXPEDIENTE_CONTABLE { get; set; }

        public virtual User USUARIO { get; set; }
    }
}
