using CommunityToolkit.Mvvm.Messaging.Messages;


namespace GestorNotas.Messages
{
    public class NoteSavedMessage : ValueChangedMessage<bool>
    {
        public NoteSavedMessage(bool value) : base(value)
        {
        }
    }
}
