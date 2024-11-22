namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_EXPEDIENTE_CONTABLE
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_EXPEDIENTE_CONTABLE()
        {
            PRE_DOCUMENTO_CONTABLE = new HashSet<PRE_DOCUMENTO_CONTABLE>();
            PRE_EXP_CENTRO_COSTE = new HashSet<PRE_EXP_CENTRO_COSTE>();
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_MODIFICACION_CREDITO = new HashSet<PRE_MODIFICACION_CREDITO>();
            PRE_FACTURA_COMPRA = new HashSet<PRE_FACTURA_COMPRA>();
            pre_proveedor_expediente_contable = new HashSet<pre_proveedor_expediente_contable>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int EXP_CODIGO { get; set; }

        public int? EA_CODIGO { get; set; }

        [StringLength(1)]
        public string EXP_I_G { get; set; }

        public byte? PRO_CODIGO { get; set; }

        [StringLength(255)]
        public string EXP_DESCRIPCION { get; set; }

        public bool? EXP_PLURIANUAL { get; set; }

        public byte? MON_CODIGO { get; set; }

        public bool? EXP_CUADRADO { get; set; }

        public short? EXP_ANO_PRESUPUESTO { get; set; }

        public int? EXP_NUM_EXP_CONTABLE_ANUAL { get; set; }

        public int? PROC_CODIGO { get; set; }

        public int? CEN_CODIGO { get; set; }

        public int? CUE_CODIGO { get; set; }

        public int? PROV_CODIGO { get; set; }

        public int? DOC_CODIGO_G_DESCUENTO_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EXP_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public int? ExpedienteID { get; set; }

        public int? DRConvenioId { get; set; }

        public virtual PRE_CENTRO_COSTE PRE_CENTRO_COSTE { get; set; }

        public virtual PRE_CUENTA_RESTRINGIDA PRE_CUENTA_RESTRINGIDA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_CONTABLE> PRE_DOCUMENTO_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_CENTRO_COSTE> PRE_EXP_CENTRO_COSTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        public virtual PRE_EXPEDIENTE_ADMINISTRATIVO PRE_EXPEDIENTE_ADMINISTRATIVO { get; set; }

        public virtual PRE_MONEDA PRE_MONEDA { get; set; }

        public virtual PRE_PROGRAMA PRE_PROGRAMA { get; set; }

        public virtual PRE_PROCEDENCIA PRE_PROCEDENCIA { get; set; }

        public virtual PRE_PROVEEDOR PRE_PROVEEDOR { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_MODIFICACION_CREDITO> PRE_MODIFICACION_CREDITO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_FACTURA_COMPRA> PRE_FACTURA_COMPRA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pre_proveedor_expediente_contable> pre_proveedor_expediente_contable { get; set; }

        [NotMapped]
        public int? EXP_NUMERO { get; set; }

        [NotMapped]
        public int? PRE_CODIGO { get; set; }

        [NotMapped]
        public int? CUEP_CODIGO { get; set; }

        [NotMapped]
        public bool DOC_AGRUPADO_DR_I { get; set; }

        [NotMapped]
        public bool DOC_AGRUPADO_MI_I { get; set; }

        [NotMapped]
        public bool DOC_ENLAZADO_TESORERIA { get; set; }

        [NotMapped]
        public string DOC_ENLAZADO_TESORERIA_LABEL
        {
            get
            {
                return this.DOC_ENLAZADO_TESORERIA ? "SI" : "NO";
            }
        }

        [NotMapped]
        public string EXP_CUADRADO_LABEL
        {
            get
            {
                return (bool)this.EXP_CUADRADO ? "SI" : "NO";
            }
        }

        [NotMapped]
        public string DOC_DESCRIPCION { get; set; }

        [NotMapped]
        public string PROV_NOMBRE { get; set; }

        [NotMapped]
        public string APLICACION { get; set; }

        [NotMapped]
        public string NUMERO_APLICACION { get; set; }

        [NotMapped]
        public string NOMBRE_APLICACION { get; set; }

        [NotMapped]
        public string CUENTA_PGCP { get; set; }

        [NotMapped]
        public decimal IMPORTE_DESCUENTO { get; set; }

        [NotMapped]
        public string TIPO_DOC { get; set; }

        [NotMapped]
        public int? HOJ_NUMERO { get; set; }

        [NotMapped]
        public int EJERCICIO { get; set; }

        [NotMapped]
        public string SquareImage
        {
            get
            {
                if (this.EXP_CUADRADO == null)
                {
                    return "empty";
                }

                return (bool)this.EXP_CUADRADO ? "notObsolete" : "obsolete";
            }
        }

        [NotMapped]
        public string MultiYearImage
        {
            get
            {
                if (this.EXP_PLURIANUAL == null)
                {
                    return "empty";
                }

                return (bool)this.EXP_PLURIANUAL ? "notObsolete" : "obsolete";
            }
        }

        [NotMapped]
        public decimal IMPORTE { get; set; }

        // [NotMapped]
        // public string AmountLabel
        // {
        // get
        // {
        // return this.IMPORTE.ToString("N");
        // }
        // }
        [NotMapped]
        public string CACS_NUMERO { get; set; }

        [NotMapped]
        public string PRO_NUMERO { get; set; }

        [NotMapped]
        public int? EA_NUMERO { get; set; }

        [NotMapped]
        public short? EA_ANO_EJERCICIO { get; set; }

        [NotMapped]
        public string PROC_DESCRIPCION { get; set; }

        [NotMapped]
        public string TIPD_NOMBRE_CORTO { get; set; }

        [NotMapped]
        public decimal DOCA_IMPORTE { get; set; }

        // [NotMapped]
        // public string AmountDocLabel
        // {
        // get
        // {
        // return this.DOCA_IMPORTE.ToString("N");
        // }
        // }
        [NotMapped]
        public DateTime? DOC_FECHA_MOVIMIENTO_I { get; set; }

        [NotMapped]
        public int DOC_CODIGO { get; set; }

        [NotMapped]
        public int? EXTRAPRE_NUMERO { get; set; }

        [NotMapped]
        public string EXTRAPRE_DESCRIPCION { get; set; }

        [NotMapped]
        public decimal EXP_EXTRAP_IMPORTE { get; set; }

        [NotMapped]
        public int? DOC_NUMERO_MOVIMIENTO_I { get; set; }

        [NotMapped]
        public string EA_DESCRIPCION { get; set; }

        [NotMapped]
        public int? TIPD_CLAVE { get; set; }

        [NotMapped]
        public int? DOCA_CODIGO { get; set; }

        [NotMapped]
        public bool TIPD_POSITIVO { get; set; }

        [NotMapped]
        public bool TIPD_FASE_DR_I { get; set; }

        [NotMapped]
        public bool TIPD_FASE_MI_I { get; set; }

        [NotMapped]
        public decimal MODIF_CREDITO { get; set; }

        [NotMapped]
        public decimal AUTORIZADO { get; set; }

        [NotMapped]
        public decimal Mp { get; set; }

        [NotMapped]
        public decimal OP { get; set; }

        [NotMapped]
        public decimal DrAmount { get; set; }

        [NotMapped]
        public decimal MiAmount { get; set; }

        [NotMapped]
        public bool NO_VINCULANTE { get; set; }

        [NotMapped]
        public int MES { get; set; }

        [NotMapped]
        public string REPORT_TYPE { get; set; }

        [NotMapped]
        public bool FASE_RC { get; set; }

        [NotMapped]
        public bool FASE_P { get; set; }

        [NotMapped]
        public int? DOCA_ANO_PRESUPUESTO { get; set; }

        [NotMapped]
        public DateTime? DOC_FECHA_PROPUESTA { get; set; }

        [NotMapped]
        public DateTime? DOC_FECHA_ASIENTO_DIARIO { get; set; }

        [NotMapped]
        public int TIPD_CODIGO { get; set; }

        #endregion
    }
}