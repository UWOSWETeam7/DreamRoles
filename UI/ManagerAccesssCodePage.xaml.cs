using OfficeOpenXml;

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

    private async void OnPickFileButtonClicked(object sender, EventArgs e)
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "org.openxmlformats.spreadsheetml.sheet" } }, // UTType values for xlsx
            { DevicePlatform.Android, new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } }, // MIME type for xlsx
            { DevicePlatform.WinUI, new[] { ".xlsx" } }, // file extension for xlsx
            { DevicePlatform.Tizen, new[] { "*/*" } },
            { DevicePlatform.macOS, new[] { "xlsx" } }, // UTType values for xlsx
        });

            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Please select an Excel file",
                FileTypes = customFileType,
            });

            if (result == null)
            {
                await DisplayAlert("Error", "File picking canceled or no file selected", "OK");
                return;
            }

            if (result != null)
            {
                // Get the selected file stream
                var stream = await result.OpenReadAsync();

                // Read Excel file using EPPlus
                using (var package = new ExcelPackage(stream))
                {
                    // Access the workbook and worksheet
                    var workbook = package.Workbook;
                    var worksheet = workbook.Worksheets[0];

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        // Access data in each column
                        string firstName = worksheet.Cells[row, 1].GetValue<string>();
                        string lastName = worksheet.Cells[row, 2].GetValue<string>();
                        string phoneNumber = worksheet.Cells[row, 3].GetValue<string>();
                        string email = worksheet.Cells[row, 4].GetValue<string>();

                        if (email == null)
                        {
                            email = "";

                        }
                        if (phoneNumber == null)
                        {
                            phoneNumber = "";
                        }
                        var answer = MauiProgram.BusinessLogic.AddPerformer(firstName, lastName, null, email, phoneNumber);

                        if (answer.ResultMessage == null)
                        {
                            // Iterate through song columns (assuming there are 9 pairs of song and notes)
                            for (int col = 5; col <= 21; col += 2)
                            {
                                string song = worksheet.Cells[row, col].GetValue<string>();
                                string notes = worksheet.Cells[row, col + 1].GetValue<string>();
                                int performerId = answer.PerformerId;
                                // Process song and notes data as needed
                                if (notes == null)
                                {
                                    notes = "";
                                }
                                if (song != null)
                                {
                                    MauiProgram.BusinessLogic.AddSongForPerformer(answer.PerformerId, song, notes);
                                }
                            }

                        }
                        else
                        {
                            await DisplayAlert("Error", answer + " for " + firstName + " " + lastName + ".", "Ok");
                        }

                    }
                    await DisplayAlert("Success", "It worked", "Yay");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}