
namespace GestioneOrdini.Gui.Messages
{
    class AddMarcheMessage
    {
        public const string CancelMessage = "Cancel";
        public const string AddMessage = "Add";

        public string Key { get; private set; }
        public object Parameter { get; private set; }

        public AddMarcheMessage(string key)
        {
            Key = key;
        }

        public AddMarcheMessage(string key, object parameter)
            : this(key)
        {
            Parameter = parameter;
        }
    }
}
