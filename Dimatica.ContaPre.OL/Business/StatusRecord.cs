namespace Dimatica.ContaPre.OL.Business
{
    public class StatusRecord
    {
        #region Public Properties

        public string Column { get; set; }

        public decimal Accumulated { get; set; }

        public decimal Pending { get; set; }

        public string AccumulatedLabel
        {
            get
            {
                if (this.Column.Equals("Descuentos") || this.Column.Equals("RC - P"))
                {
                    return string.Empty;
                }

                return this.Accumulated.ToString("N");
            }
        }

        public string PendingLabel
        {
            get
            {
                if (this.Column.Contains("-") && !this.Column.Equals("RC - P"))
                {
                    return string.Empty;
                }

                return this.Pending.ToString("N");
            }
        }

        #endregion
    }
}