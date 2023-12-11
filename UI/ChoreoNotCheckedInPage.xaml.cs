using Prototypes.Model;
using System.Reflection;

namespace Prototypes.UI;
//author: Kaia Thern
public partial class ChoreoNotCheckedInPage : ContentPage
{
    public ChoreoNotCheckedInPage()
    {
        InitializeComponent();
        try
        {
            BindingContext = new SearchBarNotCheckedInPerformerViewModel();

        }
        catch (TargetInvocationException tie)
        {
            throw;
        }
    }
}