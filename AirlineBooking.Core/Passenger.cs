namespace AirlineBooking.Core;

public class Passenger
{
    private readonly string _id;
    private readonly string _fullName;
    private readonly string _email;

    public Passenger(string id, string fullName, string email)
    {
        _id = id;
        _fullName = fullName;
        _email = email;
    }

    public string GetId() => _id;

    public string GetFullName() => _fullName;

    public string GetEmail() => _email;
}
