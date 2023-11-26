namespace passwordulatorServer;

public class UserData
{
    private string? _name;
    private string? _data;
    

    public string Name {
        get {return _name;}
        set {
            _name = value;
            //base91charset
            if (value != null) {
                var allowedChars = "!#$%&()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}~";
                if (value.Except(allowedChars).Any()) {_name = null;}
                if (value.Length > 100 ) {_name = null;}
            }
        }
    }
    public string Data {
        get {return _data;} 
        set {
            _data = value;
            //base91charset. allowed data must be ascii string in base91 encoded encrypted file
            if (value != null) {
                var allowedChars = "!#$%&()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}~";
                if (value.Except(allowedChars).Any()) {_data = null;}
                if (value.Length > 2000000 ) {_data = null;}
            }
        }
    }

}
