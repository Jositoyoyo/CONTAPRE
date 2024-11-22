namespace Dimatica.ContaPre.OL.Procedures
{
    public class Rectification
    {
        #region Fields

        private string concept;

        private string cta;

        private string description;

        private string nature;

        private string origin;

        private string providerName;

        private string year;

        #endregion

        #region Public Properties

        public string Origin
        {
            get
            {
                return this.origin ?? (this.origin = string.Empty);
            }

            set
            {
                this.origin = value;
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

        public string Nature
        {
            get
            {
                return this.nature ?? (this.nature = string.Empty);
            }

            set
            {
                this.nature = value;
            }
        }

        public string Year
        {
            get
            {
                return this.year ?? (this.year = string.Empty);
            }

            set
            {
                this.year = value;
            }
        }

        public int Code { get; set; }

        public string Concept
        {
            get
            {
                return this.concept ?? (this.concept = string.Empty);
            }

            set
            {
                concept = value;
            }
        }

        public string ProviderName
        {
            get
            {
                return this.providerName ?? (this.providerName = string.Empty);
            }

            set
            {
                this.providerName = value;
            }
        }

        public string Cta
        {
            get
            {
                return this.cta ?? (this.cta = string.Empty);
            }

            set
            {
                this.cta = value;
            }
        }

        public decimal Amount { get; set; }

        //public string AmountLabel
        //{
        //    get
        //    {
        //        return this.Amount.ToString("N");
        //    }
        //}

        #endregion
    }
}