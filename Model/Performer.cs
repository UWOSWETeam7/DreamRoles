using Prototypes.Model.Interfaces;
using System.Collections.ObjectModel;
namespace Prototypes.Model;
using System.ComponentModel;

public class Performer : INotifyPropertyChanged
{
	private int _id;
	private String _firstName;
    private String _lastName;
    private String _email;
	private String? _phoneNumber;
    private int? _absences;
    private String _checkedInStatus;
    private String _checkedInImage;
    private ObservableCollection<ISongDB> _songs;
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Creates a performer
    /// </summary>
    /// <param name="id">The Perform's specific Id that will be used to identify the Performer</param>
    /// <param name="firstName">The Perform's first name</param>
    /// /// <param name="lastName">The Perform's last name</param>
    /// <param name="songs">The songs the Performer is in</param>
    /// <param name="email">The Performer's email that can be used to contact them</param>
    /// <param name="phoneNumber">The Performer's phone number that can be used to contact them</param>
    public Performer(int id, String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber, int absences, String isCheckedIn = "not checked in")
	{
		Id = id;
		FirstName = firstName;
        LastName = lastName;
        Songs = songs;
		Email = email;
		PhoneNumber = phoneNumber;
        Absences = absences;
        CheckedInStatus = isCheckedIn;

        if (_checkedInStatus == "checked in" )
        {
            _checkedInImage = "checkmark.svg";
        }
        else if (_checkedInStatus == "excused")
        {
            _checkedInImage = "alert.png";
        }
        else
        {
            _checkedInImage = "xmark.svg";
        }
	}

    public int Id
    {
        get { return _id; }
        set { _id = value;}
    }
    public String FirstName 
	{
		get { return _firstName; } 
		set { _firstName = value; OnPropertyChanged(nameof(FirstName));}
	}
    public String LastName
    {
        get { return _lastName; }
        set { _lastName = value; OnPropertyChanged(nameof(LastName));}
    }
    public ObservableCollection<ISongDB> Songs 
    {
        get { return _songs; }
        set { _songs = value; }
    }
    public String Email
	{
        get { return _email; }
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }
    public String? PhoneNumber
	{
        get { return _phoneNumber; }
        set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
    }

    public int? Absences
    {
        get { return _absences; }
        set { _absences = value; OnPropertyChanged(nameof(Absences)); }
    }

    public String CheckedInStatus
    {
        get { return _checkedInStatus; }
        set
        {
            _checkedInStatus = value;
            if (_checkedInStatus == "checked in")
            {
                CheckedInImage = "checkmark.svg";
            }
            else if (_checkedInStatus == "excused")
            {
                CheckedInImage = "alert.png";
            }
            else
            {
                CheckedInImage = "xmark.svg";
            }

            OnPropertyChanged(nameof(CheckedInStatus));
        }
    }

    public String CheckedInImage
    {
        get { return _checkedInImage;}
        set { _checkedInImage = value; OnPropertyChanged(nameof(CheckedInImage)); }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}