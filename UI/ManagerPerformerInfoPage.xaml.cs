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
        BindingContext = MauiProgram.StageManagerBL.FindPerformer(_performer.Id);
        InitializeComponent();

        _refreshView.Refreshing += ShowEditPerformerNamePopup;
    }
    private void ShowEditContactInfoPopup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EditContactInfoPopup(_performer));
        _performer = MauiProgram.StageManagerBL.FindPerformer(_performer.Id);
        BindingContext = _performer;
    }

    private void ShowAddSongForPerformerPopUp(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddSongForPerformerPopUp());

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
}