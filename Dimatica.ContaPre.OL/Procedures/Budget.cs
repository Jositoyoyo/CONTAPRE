namespace Dimatica.ContaPre.OL.Procedures
{
    public class Budget
    {
        #region Fields

        private string article;

        private string chapter;

        private string concept;

        private string description;

        private string level;

        private string program;

        private string subConcept;

        private string type;

        #endregion

        #region Public Properties

        public int BudgetId { get; set; }

        public string Type
        {
            get
            {
                return this.type ?? (this.type = string.Empty);
            }

            set
            {
                this.type = value;
            }
        }

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

        public int? ChapterId { get; set; }

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

        public int? ArticleId { get; set; }

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

        public int? ConceptId { get; set; }

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

        public int? SubConceptId { get; set; }

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

        public bool NotBinding { get; set; }

        public decimal Amount { get; set; }

        public decimal UpdateAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public decimal DrAmount { get; set; }

        public decimal MiAmount { get; set; }

        public decimal Pending { get; set; }

        public decimal PendingAd { get; set; }

        public decimal PendingP { get; set; }

        public decimal PendingDr { get; set; }

        public decimal PendingMi { get; set; }

        public decimal RcAmount { get; set; }

        public decimal RcBalanceAmount { get; set; }

        public decimal AdAmount { get; set; }

        public decimal OAmount { get; set; }

        public decimal PAmount { get; set; }

        public int? ProgramId { get; set; }

        public string Program
        {
            get
            {
                return this.program ?? (this.program = string.Empty);
            }

            set
            {
                this.program = value;
            }
        }

        public string ProgramLabel
        {
            get
            {
                if (!this.Level.Equals("CAP"))
                {
                    return string.Empty;
                }

                return this.Program;
            }
        }

        public bool IsClose { get; set; }

        public decimal? AmountSub
        {
            get
            {
                if (!this.Level.Equals("SUB"))
                {
                    return null;
                }

                return this.Amount;
            }
        }

        public decimal? AmountCon
        {
            get
            {
                if (!this.Level.Equals("CON"))
                {
                    return null;
                }

                return this.Amount;
            }
        }

        public decimal? AmountChaArt
        {
            get
            {
                if (this.Level.Equals("CAP") || this.Level.Equals("ART"))
                {
                    return this.Amount;
                }

                return null;
            }
        }

        public decimal? UpdateAmountSub
        {
            get
            {
                if (!this.Level.Equals("SUB"))
                {
                    return null;
                }

                return this.UpdateAmount;
            }
        }

        public decimal? UpdateAmountCon
        {
            get
            {
                if (!this.Level.Equals("CON"))
                {
                    return null;
                }

                return this.UpdateAmount;
            }
        }

        public decimal? UpdateAmountChaArt
        {
            get
            {
                if (this.Level.Equals("CAP") || this.Level.Equals("ART"))
                {
                    return this.UpdateAmount;
                }

                return null;
            }
        }

        public decimal? FinalAmountSub
        {
            get
            {
                if (!this.Level.Equals("SUB"))
                {
                    return null;
                }

                return this.FinalAmount;
            }
        }

        public decimal? FinalAmountCon
        {
            get
            {
                if (!this.Level.Equals("CON"))
                {
                    return null;
                }

                return this.FinalAmount;
            }
        }

        public decimal? FinalAmountChaArt
        {
            get
            {
                if (this.Level.Equals("CAP") || this.Level.Equals("ART"))
                {
                    return this.FinalAmount;
                }

                return null;
            }
        }

        public string ApplicationLabel
        {
            get
            {
                var label = string.Empty;

                // lo dejo mientras asi para ver si esta bien
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

                // asi estaba antes, pero se cambia por peticion de sandra
                // switch (this.Type)
                // {
                // case "I":
                // label = $"{this.Chapter} {this.Article} {this.Concept} {this.SubConcept}";

                // break;

                // case "G":
                // switch (this.Level)
                // {
                // case "CAP":
                // label = $"{this.Chapter}";

                // break;
                // case "ART":
                // label = $"{this.Chapter}{this.Article}";

                // break;
                // case "CON":
                // label = $"{this.Chapter} {this.Article}{this.Concept}";

                // break;
                // case "SUB":
                // label = $"{this.Chapter} {this.Article}{this.Concept}.{this.SubConcept}";

                // break;
                // }

                // break;
                // }
                return label;
            }
        }

        public bool HasSons { get; set; }

        public string ApplicationNumber { get; set; }

        public string ApplicationName { get; set; }

        public int Year { get; set; }

        #endregion
    }
}