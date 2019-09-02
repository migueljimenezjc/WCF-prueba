using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [FaultContract(typeof(FaultInfo))]
        string Topup(string Operador, string MobileNumber, double Amount);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        Place GetPlace(string PlaceCode);

        [OperationContract]
        [FaultContract(typeof(FaultInfo))]
        AuthorResponse GetAuthorInfo(AuthorRequest Request);

        [OperationContract]
        ResponseData Login(RequestData data);
    }

    [DataContract(Name = "Place", Namespace = "")]
    public class Place
    {
        [DataMember(Name = "PlaceCode", Order = 1, IsRequired = true)]
        public string PlaceCode;
        [DataMember(Name = "PlaceName", Order = 2)]
        public string PlaceName;
    }

    [DataContract]
    public class Author
    {
        [DataMember(Name ="FirstName", Order = 1)]
        public string FirstName;

        [DataMember(Name = "LastName", Order = 2)]
        public string LastName;
        
        [DataMember(Name ="Article", Order = 3)]
        public string Article;
    }
    
    public class RequestData
    {
        public string usuario { get; set; }
        public string password { get; set; }
    }

    [DataContract]
    public class ResponseData
    {
        [DataMember(Order = 0)]
        public string token { get; set; }
        [DataMember(Order = 1)]
        public bool authenticated { get; set; }
        [DataMember(Order = 2)]
        public string employeeId { get; set; }
        [DataMember(Order = 3)]
        public string firstname { get; set; }

        [DataMember(Order = 8)]
        public DateTime timestamp { get; set; }
        [DataMember(Order = 9)]
        public string userName { get; set; }
    }

    [MessageContract(IsWrapped = false)]
    public class AuthorRequest
    {
        [MessageHeader(Name ="AuthorIdentity")]
        public string AuthorId;
    }

    [MessageContract(IsWrapped = false)]
    public class AuthorResponse
    {
        [MessageBodyMember]
        public Author AuthorInfo;
    }

    [DataContract]
    public class FaultInfo
    {
        [DataMember]
        public string Reason = null;
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }



}
