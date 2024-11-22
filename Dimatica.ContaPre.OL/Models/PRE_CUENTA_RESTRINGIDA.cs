namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public partial class PRE_CUENTA_RESTRINGIDA
    {
        #region Constructors and Desctructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRE_CUENTA_RESTRINGIDA()
        {
            PRE_DOCUMENTO_CONTABLE = new HashSet<PRE_DOCUMENTO_CONTABLE>();
            PRE_EXP_EXTRAPRE = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_EXP_EXTRAPRE1 = new HashSet<PRE_EXP_EXTRAPRE>();
            PRE_EXPEDIENTE_CONTABLE = new HashSet<PRE_EXPEDIENTE_CONTABLE>();
            PRE_HOJA_ARQUEO = new HashSet<PRE_HOJA_ARQUEO>();
            PRE_TESORERIA = new HashSet<PRE_TESORERIA>();
        }

        #endregion

        #region Public Properties

        [Key]
        public int CUE_CODIGO { get; set; }

        [StringLength(1)]
        public string CUE_I_G { get; set; }

        public int? CUE_ORDINAL_PERCEPTOR { get; set; }

        [StringLength(60)]
        public string CUE_DESCRIPCION { get; set; }

        [StringLength(60)]
        public string CUE_ENTIDAD { get; set; }

        [StringLength(30)]
        public string CUE_CC { get; set; }

        [StringLength(60)]
        public string CUE_DIRECCION { get; set; }

        public int? CEN_CODIGO { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CUE_FECHA_MODIFICACION { get; set; }

        public int? USU_CODIGO { get; set; }

        public virtual PRE_CENTRO_COSTE PRE_CENTRO_COSTE { get; set; }

        public virtual User USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_DOCUMENTO_CONTABLE> PRE_DOCUMENTO_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXP_EXTRAPRE> PRE_EXP_EXTRAPRE1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_EXPEDIENTE_CONTABLE> PRE_EXPEDIENTE_CONTABLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_HOJA_ARQUEO> PRE_HOJA_ARQUEO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRE_TESORERIA> PRE_TESORERIA { get; set; }

        [NotMapped]
        public string CEN_DESCRIPCION { get; set; }

        [NotMapped]
        public string CenLabel
        {
            get
            {
                var label = string.Empty;

                if (this.CEN_CODIGO != null)
                {
                    label = $"{this.CEN_CODIGO} - {this.CEN_DESCRIPCION}";
                }

                return label;
            }
        }   
        
        [NotMapped]
        public string DisplayLabel
        {
            get
            {
                if (this.CUE_CODIGO == -1)
                {
                    return "< Seleccione >";
                }

                var label = $"{this.CUE_ORDINAL_PERCEPTOR} - {this.CUE_ENTIDAD}";

                return label;
            }
        }    

        [NotMapped]
        public string DisplayDescriptionLabel
        {
            get
            {
                if (this.CUE_CODIGO == -1)
                {
                    return "< Seleccione >";
                }

                var label = $"{this.CUE_ORDINAL_PERCEPTOR} - {this.CUE_DESCRIPCION}";

                return label;
            }
        }

        #endregion
    }
}