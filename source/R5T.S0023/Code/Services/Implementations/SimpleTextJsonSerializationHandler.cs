using System;

using Newtonsoft.Json.Linq;

using R5T.D0098;
using R5T.T0064;
using R5T.T0091;
using R5T.T0091.T001;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    class SimpleTextJsonSerializationHandler : IMachineMessageTypeJsonSerializationHandler, IServiceImplementation
    {
        #region Static

        public static Type SimpleTextMachineMessageType = typeof(SimpleTextMachineMessage);
        public static string SimpleTextSerializationTypeIdentifier = typeof(SimpleTextMachineMessage).FullName;

        #endregion


        public bool CanHandle(Type machineMessageType)
        {
            var output = SimpleTextJsonSerializationHandler.SimpleTextMachineMessageType == machineMessageType;
            return output;
        }

        public bool CanHandle(string serializationTypeIdentifier)
        {
            var output = SimpleTextJsonSerializationHandler.SimpleTextSerializationTypeIdentifier == serializationTypeIdentifier;
            return output;
        }

        public IMachineMessage Deserialize(string serializationTypeIdentifier, JObject json)
        {
            var output = json.ToObject<SimpleTextMachineMessage>();
            return output;
        }

        public string GetSerializationTypeIdentitifer(Type machineMessageType)
        {
            return SimpleTextJsonSerializationHandler.SimpleTextSerializationTypeIdentifier;
        }
    }
}
