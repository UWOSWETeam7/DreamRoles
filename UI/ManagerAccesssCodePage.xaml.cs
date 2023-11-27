namespace Prototypes.UI;

public partial class ManagerAccesssCodePage : ContentPage
{
	public ManagerAccesssCodePage()
	{
		InitializeComponent();
        managerAccessCode.Text = MauiProgram.BusinessLogic.GetManagerAccessCode();
        choreoAccessCode.Text = MauiProgram.BusinessLogic.GetChoreoAccessCode();
        performerAccessCode.Text = MauiProgram.BusinessLogic.GetPerformerAccessCode();
    }

    private void GenerateNewAccessCode(object sender, EventArgs e)
    {
        MauiProgram.BusinessLogic.GenerateNewAccessCode();
        UpdateAccessCode();
    }

    private void UpdateAccessCode()
    {
        performerAccessCode.Text = MauiProgram.BusinessLogic.GetPerformerAccessCode();
    }
}