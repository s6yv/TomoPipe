using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Confluent.Kafka;
using KafkaNet.Protocol;
using Platom.Protocol.Schema.Validators;


namespace SV
{
    class Program
    {

        static void Main(string[] args)
        {

            SchemaValidator schema_status = new SchemaValidator(Platom.Protocol.Properties.Resources.schema_status);
            SchemaValidator schema_ect_potentials =
                new Platom.Protocol.Schema.Validators.SchemaValidator(Platom.Protocol.Properties.Resources.schema_ect_potentials);


            schema_status.ValidateMessage(Platom.Protocol.Properties.Resources.message_status);
            schema_ect_potentials.ValidateMessage(Platom.Protocol.Properties.Resources.message_et3_measurements);
        }
    }
}
