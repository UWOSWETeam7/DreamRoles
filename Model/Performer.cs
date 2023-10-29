using System.Collections.ObjectModel;

namespace Prototype.Model;

public class Performer
{
	private int _id;
	private String _firstName;
    private String _lastName;
	private ObservableCollection<String> _songs;
	private String _email;
	private int _phoneNumber;

    /// <summary>
    /// Creates a performer
    /// </summary>
    /// <param name="id">The Perform's specific Id that will be used to identify the Performer</param>
    /// <param name="firstName">The Perform's first name</param>
    /// /// <param name="lastName">The Perform's last name</param>
    /// <param name="songs">The songs the Performer is in</param>
    /// <param name="email">The Performer's email that can be used to contact them</param>
    /// <param name="phoneNumber">The Performer's phone number that can be used to contact them</param>
    public Performer(int id, String firstName, String lastName, ObservableCollection<String> songs, String email, int phoneNumber)
	{
		Id = id;
		FirstName = firstName;
        LastName = lastName;
        Songs = songs;
		Email = email;
		PhoneNumber = phoneNumber;
	}

    public int Id
    {
        get { return _id; }
        set { _id = value; }
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
    public ObservableCollection<String> Songs 
	{
        get { return _songs; }
        set { _songs = value; }
    }
    public String Email
	{
        get { return _email; }
        set { _email = value; }
    }
    public int PhoneNumber
	{
        get { return _phoneNumber; }
        set { _phoneNumber = value; }
    }
}