namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;

    #endregion

    public partial class PRE_DETALLE_HOJA_ARQUEO
    {
        #region Public Properties

        [Key]
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

        public virtual PRE_HOJA_ARQUEO PRE_HOJA_ARQUEO { get; set; }

        public virtual PRE_MONEDA PRE_MONEDA { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public string AmountLabel
        {
            get
            {
                return ((decimal)this.DET_IMPORTE).ToString("N");
            }
        }

        [NotMapped]
        public string DateLabel
        {
            get
            {
                return ((DateTime)this.DET_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"));
            }
        }

        [NotMapped]
        public string ORDINAL_BANCARIO { get; set; }

        [NotMapped]
        public string LIN_ORIGEN_DESCRIPCION { get; set; }

        [NotMapped]
        public string LIN_DESCRIPCION { get; set; }

        #endregion
    }
}