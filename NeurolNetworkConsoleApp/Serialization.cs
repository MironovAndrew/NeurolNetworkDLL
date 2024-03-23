using System.Runtime.Serialization.Json;

namespace NeurolNetworkConsoleApp
{
    public class Serialization
    {
        DataContractJsonSerializer jsonSerializer;


        string serializedFilePath;
        public Serialization(string serializedFilePath = "neurol_network_serializetion.json")
        {
            this.serializedFilePath = serializedFilePath;
            jsonSerializer = new DataContractJsonSerializer(typeof(NeurolNetwork));
        }



        public void Write(object ogj)
        {
            using (FileStream stream = new FileStream(serializedFilePath, FileMode.Create))
                jsonSerializer.WriteObject(stream, ogj);
        }



        public NeurolNetwork Read()
        {
            using (FileStream stream = new FileStream(serializedFilePath, FileMode.Open))
                return jsonSerializer.ReadObject(stream) as NeurolNetwork;
        }
    }
}
