namespace GestioneOrdini.Gui.Messages
{
    public class AddRigaMessage
    {
        public const string CancelMessage = "Cancel";
        public const string AddMessage = "Add";

        public string Key { get; private set; }
        public object Parameter { get; private set; }

        public AddRigaMessage(string key)
        {
            Key = key;
        }

        public AddRigaMessage(string key, object parameter)
            : this(key)
        {
            Parameter = parameter;
        }
    }
}
