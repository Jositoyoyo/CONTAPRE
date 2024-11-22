namespace Dimatica.ContaPre.OL.Procedures
{
    public class Application
    {
        #region Fields

        private string accountNumber;

        private string article;

        private string chapter;

        private string concept;

        private string description;

        private string level;

        private string subConcept;

        #endregion

        #region Public Properties

        public int ApplicationId { get; set; }

        public string Level
        {
            get
            {
                return this.level ?? (this.level = string.Empty);
            }

            set
            {
                this.level = value;
            }
        }

        public string Chapter
        {
            get
            {
                return this.chapter ?? (this.chapter = string.Empty);
            }

            set
            {
                this.chapter = value;
            }
        }

        public string Article
        {
            get
            {
                return this.article ?? (this.article = string.Empty);
            }

            set
            {
                this.article = value;
            }
        }

        public string Concept
        {
            get
            {
                return this.concept ?? (this.concept = string.Empty);
            }

            set
            {
                this.concept = value;
            }
        }

        public string SubConcept
        {
            get
            {
                return this.subConcept ?? (this.subConcept = string.Empty);
            }

            set
            {
                this.subConcept = value;
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

        public int? AccountId { get; set; }

        public string AccountNumber
        {
            get
            {
                return this.accountNumber ?? (this.accountNumber = string.Empty);
            }

            set
            {
                this.accountNumber = value;
            }
        }

        public bool Active { get; set; }

        public string ActiveImage
        {
            get
            {
                if (!this.Level.Equals("CAP"))
                {
                    return "empty";
                }

                return this.Active ? "notObsolete" : "obsolete";
            }
        }

        public string ApplicationLabel
        {
            get
            {
                var label = string.Empty;

                switch (this.Level)
                {
                    case "CAP":
                        label = $"{this.Chapter}";

                        break;
                    case "ART":
                        label = $"{this.Chapter}{this.Article}";

                        break;
                    case "CON":
                        label = $"{this.Chapter}{this.Article}{this.Concept}";

                        break;
                    case "SUB":
                        label = $"{this.Chapter}{this.Article}{this.Concept}.{this.SubConcept}";

                        break;
                }

                return label;
            }
        }

        #endregion
    }
}