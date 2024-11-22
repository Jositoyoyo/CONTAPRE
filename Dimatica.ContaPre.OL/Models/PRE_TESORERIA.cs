namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_TESORERIA
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_TESORERIA()
        {
            PRE_DOCUMENTO_CONTABLE = new HashSet<PRE_DOCUMENTO_CONTABLE>();
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_TESORERIA_DOCUMENTO = new HashSet<PRE_TESORERIA_DOCUMENTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int TES_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TES_FECHA_APUNTE { get; set; }

        [Column(TypeName = "money")]
        public decimal? TES_TOTAL_IMPORTE_LIQUIDO { get; set; }

        public short? TES_ANO_PRESUPUESTO { get; set; }

        public int? CUE_CODIGO { get; set; }

        public byte? ORI_CODIGO { get; set; }

        public byte? MON_CODIGO { get; set; }

        public byte? TES_MARCA_0_1_255 { get; set; }

        [StringLength(255)]
        public string TES_DESCRIPCION { get; set; }

        [StringLength(50)]
        public string TES_NUMERO_CHEQUE { get; set; }

        public byte? FOR_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TES_FECHA_BANCO { get; set; }

        public int? TIPR_CODIGO { get; set; }

        public bool? TES_ANULADO { get; set; }

        public bool? TES_HABER { get; set; }

        [StringLength(50)]
        public string TES_APLICACION { get; set; }

        public int? LIN_NUMERO { get; set; }

        public int? HOJ_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TES_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        [StringLength(10)]
        public string TesoreriaID { get; set; }

        public int? DocuExpedienteID { get; set; }

        public virtual PRE_CUENTA_RESTRINGIDA PRE_CUENTA_RESTRINGIDA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_CONTABLE> PRE_DOCUMENTO_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        public virtual PRE_FORMA_PAGO PRE_FORMA_PAGO { get; set; }

        public virtual PRE_HOJA_ARQUEO PRE_HOJA_ARQUEO { get; set; }

        public virtual PRE_LINEA_TESORERIA PRE_LINEA_TESORERIA { get; set; }

        public virtual PRE_MONEDA PRE_MONEDA { get; set; }

        public virtual PRE_ORIGEN PRE_ORIGEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA_DOCUMENTO> PRE_TESORERIA_DOCUMENTO { get; set; }

        public virtual PRE_TIPO_REGISTRO PRE_TIPO_REGISTRO { get; set; }

        public virtual User USUARIO { get; set; }

        //[NotMapped]
        //public string AmountLabel
        //{
        //    get
        //    {
        //        return ((decimal)this.TES_TOTAL_IMPORTE_LIQUIDO).ToString("N");
        //    }
        //}

        //[NotMapped]
        //public string AmountDocumentLabel
        //{
        //    get
        //    {
        //        return ((decimal)this.TESD_IMPORTE_LIQUIDO).ToString("N");
        //    }
        //}

        [NotMapped]
        public int TESD_CODIGO { get; set; }

        [NotMapped]
        public int TESD_ANO_PRESUPUESTO { get; set; }

        [NotMapped]
        public int? TESD_NUMERO_EXPEDIENTE { get; set; }

        [NotMapped]
        public string TESD_DOCUMENTO { get; set; }

        [NotMapped]
        public decimal TESD_IMPORTE_LIQUIDO { get; set; }

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
        public string TESD_NUMERO_CHEQUE { get; set; }

        [NotMapped]
        public string TESD_DESCRIPCION { get; set; }

        [NotMapped]
        public int? DOC_CODIGO { get; set; }

        [NotMapped]
        public int? EXP_NUM_EXP_CONTABLE_ANUAL { get; set; }

        //robert 07-08-2020
        [NotMapped]
        public string ORI_DESCRIPCION { get; set; }
        [NotMapped]
        public string PROV_NOMBRE { get; set; }
        //

        [NotMapped]
        public string SingLabel
        {
            get
            {
                return (bool)this.TES_HABER ? "-" : "+";
            }
        }

        #endregion
    }
}