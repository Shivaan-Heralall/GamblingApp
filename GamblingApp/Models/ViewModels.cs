namespace GamblingApp.Models
{
    public class ViewModels
    {
        public List<Client> Clients { get; set; } = new List<Client>();
        public int? SelectedClientId { get; set; }
        public Client? SelectedClient { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<TransactionType> TransactionTypes { get; set; } = new List<TransactionType>();
        public string? NameFilter { get; set; }
        public decimal? MinBalance { get; set; }
        public decimal? MaxBalance { get; set; }
        public string? CommentFilter { get; set; }

        public string OrderBy { get; set; } = "Name";
        public bool OrderDescending { get; set; } = false;

        public Transaction NewTransaction { get; set; } = new Transaction();
    }
}
