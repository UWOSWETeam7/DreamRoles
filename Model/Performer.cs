using Prototypes.Model.Interfaces;
using System.Collections.ObjectModel;
namespace Prototypes.Model;
using System.ComponentModel;

public class Performer
{
	private int _id;
	private String _firstName;
    private String _lastName;
    private String _email;
	private String? _phoneNumber;
    private int? _absences;
    private ObservableCollection<ISongDB> _songs;

    /// <summary>
    /// Creates a performer
    /// </summary>
    /// <param name="id">The Perform's specific Id that will be used to identify the Performer</param>
    /// <param name="firstName">The Perform's first name</param>
    /// /// <param name="lastName">The Perform's last name</param>
    /// <param name="songs">The songs the Performer is in</param>
    /// <param name="email">The Performer's email that can be used to contact them</param>
    /// <param name="phoneNumber">The Performer's phone number that can be used to contact them</param>
    public Performer(int id, String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber, int absences)
	{
		Id = id;
		FirstName = firstName;
        LastName = lastName;
        Songs = songs;
		Email = email;
		PhoneNumber = phoneNumber;
        Absences = absences;
	}

    public int Id
    {
        get { return _id; }
        set { _id = value;}
    }
    public String FirstName 
	{
		get { return _firstName; } 
		set { _firstName = value; }
	}
    public String LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }
    public ObservableCollection<ISongDB> Songs 
    {
        get { return _songs; }
        set { _songs = value; }
    }
    public String Email
	{
        get { return _email; }
        set { _email = value; }
    }
    public String? PhoneNumber
	{
        get { return _phoneNumber; }
        set { _phoneNumber = value; }
    }

    public int? Absences
    {
        get { return _absences; }
        set { _absences = value; }
    }
}