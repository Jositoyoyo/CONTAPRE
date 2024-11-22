namespace Dimatica.ContaPre.OL.Models
{
    #region NameSpaces

    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class BaseModel
    {
        #region Public Properties

        public bool Obsolete { get; set; }

        public int? ObsoleteBy { get; set; }

        public DateTime? ObsoleteDate { get; set; }

        [NotMapped]
        public string ObsoleteImage
        {
            get
            {
                return this.Obsolete ? "obsolete" : "notObsolete";
            }
        }

        #endregion
    }
}