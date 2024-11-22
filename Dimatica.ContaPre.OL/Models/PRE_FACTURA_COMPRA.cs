namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_FACTURA_COMPRA
    {
        #region Public Properties

        [Key]
        public int FA_CODIGO { get; set; }

        [StringLength(4)]
        public string EJERCICIO { get; set; }

        public int? EA_CODIGO { get; set; }

        public int? EXP_CODIGO { get; set; }

        public int? ANU_COD_ANUALIDAD { get; set; }

        public int? LOTE_COD_LOTE { get; set; }

        public int? PROV_COD_PROVEEDOR { get; set; }

        [StringLength(60)]
        public string PROV_NOMBRE { get; set; }

        [StringLength(15)]
        public string PROV_NIF { get; set; }

        [StringLength(20)]
        public string NCERTIFICADO { get; set; }

        [StringLength(30)]
        public string FA_NUM_FACTURA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FA_FECHA_FACTURA { get; set; }

        [Column(TypeName = "money")]
        public decimal? FA_IMPORTE_INTEGRO { get; set; }

        [Column(TypeName = "money")]
        public decimal? FA_IMPORTE_BOE { get; set; }

        [Column(TypeName = "money")]
        public decimal? FA_IMPORTE_GARANTIA { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FA_FIRMA_RO { get; set; }

        public int? DOC_CODIGO { get; set; }

        [Column(TypeName = "money")]
        public decimal? FA_BASE_IMPONIBLE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FA_IVA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FA_RETENCION { get; set; }

        [Column(TypeName = "money")]
        public decimal? FA_IMPORTE_IVA { get; set; }

        [Column(TypeName = "money")]
        public decimal? FA_IMPORTE_RETENCION { get; set; }

        public bool? MARCADO { get; set; }

        public int? CODFACTURAGEI { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FA_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        [StringLength(6)]
        public string APP_PRESUP { get; set; }

        public virtual PRE_DOCUMENTO_CONTABLE PRE_DOCUMENTO_CONTABLE { get; set; }

        public virtual PRE_EXPEDIENTE_CONTABLE PRE_EXPEDIENTE_CONTABLE { get; set; }

        [NotMapped]
        public int? EXP_NUM_EXP_CONTABLE_ANUAL { get; set; }

        [NotMapped]
        public short? EXP_ANO_PRESUPUESTO { get; set; }

        [NotMapped]
        public string BoundLabel
        {
            get
            {
                if (this.DOC_CODIGO == null || this.DOC_CODIGO == 0)
                {
                    return "No";
                }

                return "Si";
            }
        }

        [NotMapped]
        public decimal? FA_IMPORTE_RO { get; set; }

        #endregion


        [NotMapped]
        public string RecordNumber
        {
            get
            {
                return $"{this.EXP_NUM_EXP_CONTABLE_ANUAL}/{this.EXP_ANO_PRESUPUESTO}";
            }
        }

        [NotMapped]
        public string FA_IMPORTE_RO_LABEL
        {
            get
            {
                if (this.FA_IMPORTE_RO == null)
                {
                    return 0.ToString("N");
                }

                return ((decimal)this.FA_IMPORTE_RO).ToString("N");
            }
        } 
        
        [NotMapped]
        public string FA_IMPORTE_INTEGRO_LABEL
        {
            get
            {
                if (this.FA_IMPORTE_INTEGRO == null)
                {
                    return 0.ToString("N");
                }

                return ((decimal)this.FA_IMPORTE_INTEGRO).ToString("N");
            }
        }
        
        [NotMapped]
        public string FA_BASE_IMPONIBLE_LABEL
        {
            get
            {
                if (this.FA_BASE_IMPONIBLE == null)
                {
                    return 0.ToString("N");
                }

                return ((decimal)this.FA_BASE_IMPONIBLE).ToString("N");
            }
        }
        
        [NotMapped]
        public string FA_IMPORTE_IVA_LABEL
        {
            get
            {
                if (this.FA_IMPORTE_IVA == null)
                {
                    return 0.ToString("N");
                }

                return ((decimal)this.FA_IMPORTE_IVA).ToString("N");
            }
        }
        
        [NotMapped]
        public string FA_IMPORTE_RETENCION_LABEL
        {
            get
            {
                if (this.FA_IMPORTE_RETENCION == null)
                {
                    return 0.ToString("N");
                }

                return ((decimal)this.FA_IMPORTE_RETENCION).ToString("N");
            }
        }
        
        [NotMapped]
        public string FA_IMPORTE_BOE_LABEL
        {
            get
            {
                if (this.FA_IMPORTE_BOE == null)
                {
                    return 0.ToString("N");
                }

                return ((decimal)this.FA_IMPORTE_BOE).ToString("N");
            }
        }
    }
}