namespace Dimatica.ContaPre.OL.Business
{
    #region NameSpaces

    using System.Collections.Generic;

    #endregion

    public class SiteDataItem
    {
        #region Fields

        private string path;

        private string text;

        #endregion

        #region Constructors and Desctructors

        public SiteDataItem(int idCtor, int parentIdCtor, string textCtor, string pathCtor = null)
        {
            this.Id = idCtor;
            this.ParentId = parentIdCtor;
            this.Text = textCtor;
            this.Path = pathCtor;
        }

        #endregion

        #region Public Properties

        public string Text
        {
            get
            {
                return this.text ?? (this.text = string.Empty);
            }
            set
            {
                this.text = value;
            }
        }

        public string Path
        {
            get
            {
                return this.path ?? (this.path = string.Empty);
            }
            set
            {
                this.path = value;
            }
        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        #endregion
    }
}