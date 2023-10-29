namespace Prototypes.UI;
//@author: Keith Thoong
public partial class ManagerPerformerInfoPage : ContentPage
{
    public ManagerPerformerInfoPage()
    {
        InitializeComponent();
    }
    private void ShowEditContactInfoPopup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EditContactInfoPopup());
    }

    private void ShowAddSongForPerformerPopUp(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddSongForPerformerPopUp());
    }


}