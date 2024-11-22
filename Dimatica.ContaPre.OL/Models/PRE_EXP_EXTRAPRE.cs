namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_EXP_EXTRAPRE
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_EXP_EXTRAPRE()
        {
            PRE_TESORERIA_DOCUMENTO = new HashSet<PRE_TESORERIA_DOCUMENTO>();
            PRE_EXP_EXTRAPRE1 = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_SENALAMIENTO_DOCUMENTO = new HashSet<PRE_SENALAMIENTO_DOCUMENTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int EXP_EXTRAP_CODIGO { get; set; }

        public int EXTRAPRE_CODIGO { get; set; }

        public byte? TIP_EXTRAP_CODIGO { get; set; }

        public short? EXP_EXTRAP_ANO_PRESUPUESTO { get; set; }

        public int? EXP_EXTRAP_NUMERO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EXP_EXTRAP_FECHA { get; set; }

        [Column(TypeName = "money")]
        public decimal? EXP_EXTRAP_IMPORTE { get; set; }

        [StringLength(255)]
        public string EXP_EXTRAP_TEXTO { get; set; }

        public int? EXP_CODIGO { get; set; }

        public int? DOC_CODIGO { get; set; }

        public int? EXP_NUM_EXP_CONTABLE_ANUAL { get; set; }

        public int? PROV_CODIGO_PROVEEDOR { get; set; }

        public bool? EXP_ENLAZADO_TESORERIA { get; set; }

        public int? TES_CODIGO { get; set; }

        public int? CUE_CODIGO { get; set; }

        public byte? TIPP_CODIGO { get; set; }

        public byte? FOR_CODIGO { get; set; }

        public int? CUE_CODIGO_PAGADOR { get; set; }

        public int? PROV_CODIGO_TERCERO { get; set; }

        public int? HOJ_NUMERO { get; set; }

        public int? HOJ_NUMERO50 { get; set; }

        public short? ANO_HOJA { get; set; }

        public short? ANO_HOJA50 { get; set; }

        public byte? MON_CODIGO { get; set; }

        public bool? EXP_EXTRAP_PAGADO { get; set; }

        public int? EXP_EXTRAP_NUMERO_PMP { get; set; }

        [StringLength(60)]
        public string EXP_EXTRAP_NUMERO_CHEQUE { get; set; }

        [StringLength(10)]
        public string CUEP_NUMERO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EXP_EXTRAP_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public int? TesoreriaID { get; set; }

        public int? ExtraPExpedienteID { get; set; }

        public int? EXP_NUM_EXP_EXTRAPRE { get; set; }

        public bool? exp_extrap_reparado { get; set; }

        public int? EXP_EXTRAP_CODIGO_ENLAZADO { get; set; }

        public virtual PRE_CUENTA_RESTRINGIDA PRE_CUENTA_RESTRINGIDA { get; set; }

        public virtual PRE_CUENTA_RESTRINGIDA PRE_CUENTA_RESTRINGIDA1 { get; set; }

        public virtual PRE_DOCUMENTO_CONTABLE PRE_DOCUMENTO_CONTABLE { get; set; }

        public virtual PRE_EXPEDIENTE_CONTABLE PRE_EXPEDIENTE_CONTABLE { get; set; }

        public virtual PRE_EXTRAPRESUPUESTARIA PRE_EXTRAPRESUPUESTARIA { get; set; }

        public virtual PRE_FORMA_PAGO PRE_FORMA_PAGO { get; set; }

        public virtual PRE_MONEDA PRE_MONEDA { get; set; }

        public virtual PRE_PROVEEDOR PRE_PROVEEDOR { get; set; }

        public virtual PRE_PROVEEDOR PRE_PROVEEDOR1 { get; set; }

        public virtual PRE_TESORERIA PRE_TESORERIA { get; set; }

        public virtual PRE_TIPO_EXTRAP PRE_TIPO_EXTRAP { get; set; }

        public virtual PRE_TIPO_PAGO PRE_TIPO_PAGO { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA_DOCUMENTO> PRE_TESORERIA_DOCUMENTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE1 { get; set; }

        public virtual PRE_EXP_EXTRAPRE PRE_EXP_EXTRAPRE2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_SENALAMIENTO_DOCUMENTO> PRE_SENALAMIENTO_DOCUMENTO { get; set; }

        [NotMapped]
        public int? EXTRAPRE_NUMERO { get; set; }

        [NotMapped]
        public int? NUM_ORDINAL_PAGADOR { get; set; }

        [NotMapped]
        public int? SEN_NUMERO { get; set; }

        [NotMapped]
        public int? CUE_ORDINAL_PERCEPTOR { get; set; }

        [NotMapped]
        public string EXTRAPRE_DESCRIPCION { get; set; }

        [NotMapped]
        public string PROV_NIF { get; set; }

        [NotMapped]
        public string EXTRAPRE_NOMBRE { get; set; }

        [NotMapped]
        public string TIPO_DOC { get; set; }

        [NotMapped]
        public string INTERESADO { get; set; }

        [NotMapped]
        public string CODIGO_DESCUENTO { get; set; }

        [NotMapped]
        public string TERCERO { get; set; }

        [NotMapped]
        public string EXP_NUM_EXP_CONTABLE_ANUAL_LABEL { get; set; }

        [NotMapped]
        public string ORDINAL_PAGADOR { get; set; }

        [NotMapped]
        public string EXTRAPRE_LABEL
        {
            get
            {
                if (this.EXTRAPRE_NUMERO == null)
                {
                    return string.Empty;
                }

                return $"{this.EXTRAPRE_NUMERO} - {this.EXTRAPRE_DESCRIPCION}";
            }
        }

        //[NotMapped]
        //public string AmountLabel
        //{
        //    get
        //    {
        //        return ((decimal)this.EXP_EXTRAP_IMPORTE).ToString("N");
        //    }
        //}

        #endregion
    }
}