using CommunityToolkit.Maui.Core.Handlers;
using CommunityToolkit.Maui.Views;
using Prototypes.Model;

namespace Prototypes.UI;

public partial class RehearsalCheckInPopup : Popup
{
	Performer _performer;
	Rehearsal _rehearsal;
	public RehearsalCheckInPopup(Performer performer, Rehearsal rehearsal)
	{
		_performer = performer;
		_rehearsal = rehearsal;

		InitializeComponent();
		LSong.Text = rehearsal.Song.Title;
		LTime.Text = rehearsal.Time.ToString("ddd MMMM d, yyyy 'At' h:mm tt");
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await CloseAsync(true);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
		await CloseAsync(false);
    }
}