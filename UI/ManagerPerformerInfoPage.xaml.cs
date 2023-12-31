using Prototypes.Model;

namespace Prototypes.UI;
//@author: Keith Thoong
public partial class ManagerPerformerInfoPage : ContentPage
{
    private Performer _performer; 
    private RefreshView _refreshView = new RefreshView();
    public ManagerPerformerInfoPage(Performer performer)
    {
        _performer = performer;
        BindingContext = MauiProgram.BusinessLogic.FindPerformer(_performer.Id);
        InitializeComponent();
        title.Text = _performer.FirstName + " " + _performer.LastName + "'s Info";
        _refreshView.Refreshing += ShowEditPerformerNamePopup;
    }
    private void ShowEditContactInfoPopup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EditContactInfoPopup(_performer));
        _performer = MauiProgram.BusinessLogic.FindPerformer(_performer.Id);
        BindingContext = _performer;
    }

    private void ShowAddSongForPerformerPopUp(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddSongForPerformerPopUp(_performer));
    }
    private void ShowEditPerformerNamePopup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EditPerformerNamePopup(_performer));
        //_refreshView.IsRefreshing = true;
    }

    private void ShowEditPerformerSongPopup(object sender, EventArgs e)
    {
        var label = (Image)sender;
        var song = label.BindingContext;
        Navigation.PushAsync(new EditSongPopup((Song)song));
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());

        // Remove all pages from the navigation stack except the WelcomePage
        var existingPages = Navigation.NavigationStack.ToList();

        if (existingPages.Any())
        {
            foreach (var page in existingPages.Take(existingPages.Count - 1))
            {
                Navigation.RemovePage(page);
            }
        }
    }
}