namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_SENALAMIENTO_DOCUMENTO
    {
        #region Public Properties

        [Key]
        public int SEND_CODIGO { get; set; }

        public int SEN_CODIGO { get; set; }

        public int? DOC_CODIGO { get; set; }

        public int? EXP_EXTRAP_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SEND_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_DOCUMENTO_CONTABLE PRE_DOCUMENTO_CONTABLE { get; set; }

        public virtual PRE_EXP_EXTRAPRE PRE_EXP_EXTRAPRE { get; set; }

        public virtual PRE_SENALAMIENTO PRE_SENALAMIENTO { get; set; }

        public virtual User USUARIO { get; set; }

        [NotMapped]
        public int? NUMERO_DOCUMENTO { get; set; }

        [NotMapped]
        public string TIPO_DOCUMENTO { get; set; }

        [NotMapped]
        public string CONCEPTO { get; set; }

        [NotMapped]
        public string PERCEPTOR { get; set; }

        [NotMapped]
        public decimal IMPORTE_INTEGRO { get; set; }

        [NotMapped]
        public string IMPORTE_INTEGRO_LABEL
        {
            get
            {
                return this.IMPORTE_INTEGRO.ToString("N");
            }
        }

        [NotMapped]
        public decimal IRPF { get; set; }

        [NotMapped]
        public decimal SEGURIDAD_SOCIAL { get; set; }

        [NotMapped]
        public decimal BOE { get; set; }

        [NotMapped]
        public decimal D_PASIVOS { get; set; }

        [NotMapped]
        public decimal MUFACE { get; set; }

        [NotMapped]
        public decimal ANTICIPO_HABERES { get; set; }

        [NotMapped]
        public decimal INTERESES_ANTICIPOS { get; set; }

        [NotMapped]
        public decimal IMPORTE_LIQUIDO { get; set; }

        [NotMapped]
        public string IMPORTE_LIQUIDO_LABEL
        {
            get
            {
                return this.IMPORTE_LIQUIDO.ToString("N");
            }
        }

        [NotMapped]
        public decimal TOTAL_IMPORTE_LIQUIDO { get; set; }

        [NotMapped]
        public string TOTAL_IMPORTE_LIQUIDO_LABEL
        {
            get
            {
                if (this.TOTAL_IMPORTE_LIQUIDO == -1)
                {
                    return string.Empty;
                }

                return this.TOTAL_IMPORTE_LIQUIDO.ToString("N");
            }
        }

        [NotMapped]
        public string NUMERO_CHEQUE { get; set; }

        [NotMapped]
        public DateTime? SEN_FECHA { get; set; }

        [NotMapped]
        public decimal TOTAL_DESCUENTOS
        {
            get
            {
                return this.IMPORTE_INTEGRO - this.IMPORTE_LIQUIDO;
            }
        }

        [NotMapped]
        public string TOTAL_DESCUENTOS_LABEL
        {
            get
            {
                return this.TOTAL_DESCUENTOS.ToString("N");
            }
        }

        #endregion

        [NotMapped]
        public int? SEND_CODIGO_AUX { get; set; }
        [NotMapped]
        public string DOC_CODIGO_AUX { get; set; }
        [NotMapped]
        public string PROV_CC_CE { get; set; }
        [NotMapped]
        public string PROV_CC_CO { get; set; }
        [NotMapped]
        public string PROV_CC_DC { get; set; }
        [NotMapped]
        public string PROV_CC_NC { get; set; }
        [NotMapped]
        public string PROV_IBAN { get; set; }
        [NotMapped]
        public string PROV_NOMBRE_SUCURSAL { get; set; }
        [NotMapped]
        public string PROV_DIR_SUCURSAL { get; set; }
        [NotMapped]
        public string PROV_CP_SUCURSAL { get; set; }
        [NotMapped]
        public string PROV_POBLACION_SUCURSAL { get; set; }
        [NotMapped]
        public string DOC_FACTURA { get; set; }
        [NotMapped]
        public int? PROV_CODIGO { get; set; }
    }
}