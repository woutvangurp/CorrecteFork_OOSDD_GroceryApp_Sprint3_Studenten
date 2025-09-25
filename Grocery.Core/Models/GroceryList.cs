using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.Core.Models
{
    public partial class GroceryList : Model
    {
        public DateOnly Date { get; set; }
        public int ClientId { get; set; }
        [ObservableProperty]
        public string color;

        public string Name;

        public GroceryList(int id, string name, DateOnly date, string colour, int clientId) : base(id, name)
        {
            Id = id;
            Name = name;
            Date = date;
            color = colour;
            ClientId = clientId;
        }

    }
}
