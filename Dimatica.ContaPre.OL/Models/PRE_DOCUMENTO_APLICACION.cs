namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_DOCUMENTO_APLICACION
    {
        #region Public Properties

        [Key]
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

        public virtual PRE_CUENTA_PGCP PRE_CUENTA_PGCP { get; set; }

        public virtual PRE_DOCUMENTO_CONTABLE PRE_DOCUMENTO_CONTABLE { get; set; }

        public virtual PRE_PRESUPUESTO PRE_PRESUPUESTO { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public string CACS_NUMERO { get; set; }

        [NotMapped]
        public string CUEP_NUMERO { get; set; }

        [NotMapped]
        public string CACS_CODIGO { get; set; }

        [NotMapped]
        public int NUM_APLICACIONES { get; set; }

        [NotMapped]
        public decimal? AmountLabel
        {
            get
            {
                return this.DOCA_IMPORTE ?? 0;
            }
        }

        #endregion
    }
}