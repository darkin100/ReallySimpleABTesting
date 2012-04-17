namespace Website.Models
{
    public class Advert : IAdvert
    {
        public Advert(string message)
        {
            Text = message;
        }

        #region IAdvert Members

        public string Text { get; set; }

        #endregion
    }

    public interface IAdvert
    {
        string Text { get; set; }
    }
}