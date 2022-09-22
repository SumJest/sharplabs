namespace lab4;



public class CSVWriter
{

    private readonly string _path;

    public CSVWriter(string path)
    {
        _path = path;
    }

    public void Write(Phone[] phones)
    {
        string data = "";

        foreach (var phone in phones)
        {
            data += $"{phone.Number},{phone.UrlName},{phone.Url},{phone.Level}\n";
        }
        
        File.WriteAllText(_path, data);
    }
}