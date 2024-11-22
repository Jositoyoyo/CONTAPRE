namespace Dimatica.ContaPre.OL.Procedures
{
    public class BudgetApplication
    {
        #region Fields

        private string cacsCode;

        private string cacsNumber;

        private string description;

        #endregion

        #region Public Properties

        public int BudgetId { get; set; }

        public int? ChapterId { get; set; }

        public int? ArticleId { get; set; }

        public int? ConceptId { get; set; }

        public int? SubConceptId { get; set; }

        public string CacsCode
        {
            get
            {
                return this.cacsCode ?? (this.cacsCode = string.Empty);
            }

            set
            {
                this.cacsCode = value;
            }
        }

        public string CacsNumber
        {
            get
            {
                return this.cacsNumber ?? (this.cacsNumber = string.Empty);
            }

            set
            {
                this.cacsNumber = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description ?? (this.description = string.Empty);
            }

            set
            {
                this.description = value;
            }
        }

        #endregion
    }
}