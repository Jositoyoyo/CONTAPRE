namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_TESORERIA_DOCUMENTO
    {
        #region Public Properties

        [Key]
        public int TESD_CODIGO { get; set; }

        public int TES_CODIGO { get; set; }

        public int? DOC_CODIGO { get; set; }

        public int? EXP_EXTRAP_CODIGO { get; set; }

        public bool? TESD_ENLAZADO_TESORERIA { get; set; }

        [Column(TypeName = "money")]
        public decimal? TESD_IMPORTE_LIQUIDO { get; set; }

        public short? TESD_ANO_PRESUPUESTO { get; set; }

        [StringLength(50)]
        public string TESD_NUMERO_CHEQUE { get; set; }

        public byte? ORI_CODIGO { get; set; }

        [StringLength(50)]
        public string TESD_DOCUMENTO { get; set; }

        [StringLength(50)]
        public string TESD_APLICACION { get; set; }

        [StringLength(255)]
        public string TESD_DESCRIPCION { get; set; }

        public int? TESD_NUMERO_EXPEDIENTE { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TESD_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public int? TesoreriaExpedID { get; set; }

        public int? TesoreriaID { get; set; }

        public int? DocuExpedienteID { get; set; }

        public virtual PRE_DOCUMENTO_CONTABLE PRE_DOCUMENTO_CONTABLE { get; set; }

        public virtual PRE_EXP_EXTRAPRE PRE_EXP_EXTRAPRE { get; set; }

        public virtual PRE_ORIGEN PRE_ORIGEN { get; set; }

        public virtual PRE_TESORERIA PRE_TESORERIA { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public bool Finish { get; set; }

        #endregion
    }
}