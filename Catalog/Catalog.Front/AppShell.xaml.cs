using Catalog.Front.Pages;

namespace Catalog.Front
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ExistingItems), typeof(ExistingItems));
            Routing.RegisterRoute(nameof(AddItem), typeof(AddItem));
            Routing.RegisterRoute(nameof(SinglItem), typeof(SinglItem));
        }
    }
}