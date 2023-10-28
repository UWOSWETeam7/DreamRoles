using System.Collections.ObjectModel;

namespace Prototype.Model;

public class Performer
{
	private String id;
	private String firstName;
    private String lastName;
	private ObservableCollection<String> songs;
	private String email;
	private String phoneNumber;

    /// <summary>
    /// Creates a performer
    /// </summary>
    /// <param name="id">The Perform's specific Id that will be used to identify the Performer</param>
    /// <param name="firstName">The Perform's first name</param>
    /// /// <param name="lastName">The Perform's last name</param>
    /// <param name="songs">The songs the Performer is in</param>
    /// <param name="email">The Performer's email that can be used to contact them</param>
    /// <param name="phoneNumber">The Performer's phone number that can be used to contact them</param>
    public Performer(String id, String firstName, String lastName, ObservableCollection<String> songs, String email, String phoneNumber)
	{
		Id = id;
		FirstName = firstName;
        LastName = lastName;
        Songs = songs;
		Email = email;
		PhoneNumber = phoneNumber;
	}

    public String Id
    {
        get { return id; }
        set { id = value; }
    }
    public String FirstName 
	{
		get { return firstName; } 
		set { firstName = value; }
	}
    public String LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }
    public ObservableCollection<String> Songs 
	{
        get { return songs; }
        set { songs = value; }
    }
    public String Email
	{
        get { return email; }
        set { email = value; }
    }
    public String PhoneNumber
	{
        get { return phoneNumber; }
        set { phoneNumber = value; }
    }
}
