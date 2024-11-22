namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_DOCUMENTO_CONTABLE
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_DOCUMENTO_CONTABLE()
        {
            PRE_DOCUMENTO_APLICACION = new HashSet<PRE_DOCUMENTO_APLICACION>();
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_TESORERIA_DOCUMENTO = new HashSet<PRE_TESORERIA_DOCUMENTO>();
            PRE_FACTURA_COMPRA = new HashSet<PRE_FACTURA_COMPRA>();
            PRE_SENALAMIENTO_DOCUMENTO = new HashSet<PRE_SENALAMIENTO_DOCUMENTO>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int DOC_CODIGO { get; set; }

        public int? EXP_CODIGO { get; set; }

        [StringLength(1)]
        public string DOC_I_G { get; set; }

        public DateTime? DOC_FECHA_PROPUESTA { get; set; }

        public DateTime? DOC_FECHA_ASIENTO_DIARIO { get; set; }

        [StringLength(20)]
        public string DOC_NUMERO_CHEQUE { get; set; }

        [StringLength(255)]
        public string DOC_DESCRIPCION { get; set; }

        public int? DOC_NUMERO_MOVIMIENTO_I { get; set; }

        public bool? DOC_AGRUPADO_DR_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DOC_FECHA_MOVIMIENTO_I { get; set; }

        public int? TIPD_CODIGO { get; set; }

        public byte? FOR_CODIGO { get; set; }

        public int? CUE_CODIGO { get; set; }

        public byte? TIPP_CODIGO { get; set; }

        public bool? DOC_ENLAZADO_TESORERIA { get; set; }

        public int? TES_CODIGO { get; set; }

        public int? HOJ_NUMERO { get; set; }

        public int? HOJ_NUMERO50 { get; set; }

        public short? ANO_HOJA { get; set; }

        public short? ANO_HOJA50 { get; set; }

        [StringLength(75)]
        public string DOC_FACTURA { get; set; }

        public int? DOC_CODIGO_G_DESCUENTO_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DOC_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public int? TesoreriaID { get; set; }

        public int? DocuExpedienteID { get; set; }

        public bool? doc_reparado { get; set; }

        public int? prov_codigo { get; set; }

        public virtual PRE_CUENTA_RESTRINGIDA PRE_CUENTA_RESTRINGIDA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_APLICACION> PRE_DOCUMENTO_APLICACION { get; set; }

        public virtual PRE_EXPEDIENTE_CONTABLE PRE_EXPEDIENTE_CONTABLE { get; set; }

        public virtual PRE_FORMA_PAGO PRE_FORMA_PAGO { get; set; }

        public virtual PRE_TESORERIA PRE_TESORERIA { get; set; }

        public virtual PRE_TIPO_DOCUMENTO PRE_TIPO_DOCUMENTO { get; set; }

        public virtual PRE_TIPO_PAGO PRE_TIPO_PAGO { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA_DOCUMENTO> PRE_TESORERIA_DOCUMENTO { get; set; }

        public virtual PRE_PROVEEDOR PRE_PROVEEDOR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_FACTURA_COMPRA> PRE_FACTURA_COMPRA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_SENALAMIENTO_DOCUMENTO> PRE_SENALAMIENTO_DOCUMENTO { get; set; }

        [NotMapped]
        public int? SEN_NUMERO { get; set; }

        [NotMapped]
        public int? SEN_CODIGO { get; set; }

        [NotMapped]
        public string TIPO_DOC { get; set; }

        [NotMapped]
        public string ORDINAL_PAGADOR { get; set; }

        [NotMapped]
        public string TIPP_DESCRIPCION { get; set; }

        [NotMapped]
        public string FOR_DESCRIPCION { get; set; }

        [NotMapped]
        public int ANO_PRESUPUESTO { get; set; }

        [NotMapped]
        public int? NUMERO_EXPEDIENTE { get; set; }

        [NotMapped]
        public string PROV_NOMBRE { get; set; }

        [NotMapped]
        public string DOCUMENTO_APLICACION { get; set; }

        [NotMapped]
        public int? EXP_NUM_EXP_CONTABLE_ANUAL { get; set; }

        [NotMapped]
        public decimal LIQUIDO { get; set; }

        [NotMapped]
        public string ORIGEN { get; set; }

        [NotMapped]
        public string PROCEDENCIA { get; set; }

        // [NotMapped]
        // public string AmountLabel
        // {
        // get
        // {
        // return this.LIQUIDO.ToString("N");
        // }
        // }
        [NotMapped]
        public int? CODIGO_EXP_EXTRAP { get; set; }

        [NotMapped]
        public int? PROV_CODIGO { get; set; }

        [NotMapped]
        public int? CODIGO_DOCUMENTO { get; set; }

        [NotMapped]
        public int? EXP_EXTRAP_CODIGO { get; set; }

        [NotMapped]
        public int? NUMERO_DOCUMENTO { get; set; }

        [NotMapped]
        public int? DOC_CODIGO_AUX { get; set; }

        [NotMapped]
        public int? CUE_ORDINAL_PERCEPTOR { get; set; }

        [NotMapped]
        public string TIPO_DOCUMENTO { get; set; }

        [NotMapped]
        public string CONCEPTO { get; set; }

        [NotMapped]
        public string tiene_irpf { get; set; }

        [NotMapped]
        public bool Select { get; set; }

        [NotMapped]
        public bool Repair { get; set; }

        [NotMapped]
        public bool TIPD_POSITIVO { get; set; }

        [NotMapped]
        public int? CEN_CODIGO { get; set; }

        [NotMapped]
        public string CEN_DESCRIPCION { get; set; }

        [NotMapped]
        public int? TIPD_CLAVE { get; set; }

        [NotMapped]
        public string TIPD_NOMBRE_CORTO { get; set; }

        [NotMapped]
        public string TIPD_DESCRIPCION { get; set; }

        [NotMapped]
        public bool TIPD_FASE_RC_G { get; set; }

        [NotMapped]
        public bool TIPD_FASE_AD_G { get; set; }

        [NotMapped]
        public bool TIPD_FASE_O_G { get; set; }

        [NotMapped]
        public bool TIPD_FASE_P_G { get; set; }

        [NotMapped]
        public string CEN_AREA_ORIGEN { get; set; }

        [NotMapped]
        public string PROV_NIF { get; set; }

        [NotMapped]
        public string PROV_DIRECCION { get; set; }

        [NotMapped]
        public string PROV_POBLACION { get; set; }

        [NotMapped]
        public string PROV_CODIGO_POSTAL { get; set; }

        [NotMapped]
        public string PRO_NUMERO { get; set; }

        [NotMapped]
        public short? EXP_ANO_PRESUPUESTO { get; set; }

        #endregion
    }
}