using Catalog.Front.Pages;

namespace Catalog.Front
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnItemsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(ExistingItems));
        }
        private void OnAddItemClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(AddItem));
        }
    }
}