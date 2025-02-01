List<string> peopleList = new List<string>()
{
    "Peter", "Mike", "Peter", "Bob", "Peter", "Peter", "Bob", "Mike", "Bob", "Peter", "Peter", "Mike", "Bob"
};

//call the publisher and subscribe
Publisher publisher = new Publisher();
publisher.ContactNotify += TextMessageSend.Send;
publisher.CountMessages(peopleList);

public class TextMessageSend
{
    public static void Send(object source, PersonEventArgs e)
    {
        Console.WriteLine(e.Name);
    }
}
public class Publisher
{
    public event MessageEventHandler ContactNotify;

    public delegate void MessageEventHandler(object sender, PersonEventArgs e);

    public void CountMessages(List<string> peopleList)
    {
        var names = new Dictionary<string, int>();

        foreach (string person in peopleList)
        {
            if (!names.ContainsKey(person))
                names[person] = 1;
            else
                names[person]++;

            if (names[person] % 3 == 0)
            {
                OnContactNotify(person);
            }
        }
    }

    protected virtual void OnContactNotify(string name)
    {
        ContactNotify?.Invoke(this, new PersonEventArgs(){Name = name});
    }
}

public class PersonEventArgs : EventArgs
{
    public string Name { get; set;}
}