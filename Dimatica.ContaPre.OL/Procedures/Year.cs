namespace Dimatica.ContaPre.OL.Procedures
{
    public class Year
    {
        #region Fields

        private string value;

        #endregion

        #region Public Properties

        public string Value
        {
            get
            {
                return this.value ?? (this.value = string.Empty);
            }

            set
            {
                this.value = value;
            }
        }

        #endregion
    }
}