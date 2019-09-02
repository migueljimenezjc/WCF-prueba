using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;
using WCF.App_Code;

namespace WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        private const string AuthorId = "db2972";

        public AuthorResponse GetAuthorInfo(AuthorRequest Request)
        {
            if (Request.AuthorId != AuthorId)
            {
                string Error = "Invalid Author Id";
                throw new FaultException<string>(Error);
            }
            AuthorResponse Response = new AuthorResponse();
            Response.AuthorInfo = new Author();
            Response.AuthorInfo.FirstName = "Miguel Angel";
            Response.AuthorInfo.LastName = "JC";
            Response.AuthorInfo.Article = "Nuevo Articulo";
            return Response;
        }

        
        public ResponseData Login(RequestData data)  
        {
            var response = new ResponseData
            {
                token = new Token().GetToken(),
                authenticated = true,
                employeeId = "D2019",
                firstname = "Miguel",
                timestamp = DateTime.Now,
                userName = "Miguel Angel"
            };
            return response;
        }

        public Place GetPlace(string PlaceCode)
        {
            Place place = new Place();
            return place;
        }

        public string Topup(string Operador, string MobileNumber, double Amount)
        {
            if (Amount == 0.0d)
            {
                FaultInfo fi = new FaultInfo();
                fi.Reason = "Error";
                throw new FaultException<FaultInfo>(fi, new FaultReason(fi.Reason));
            }
            return "Recharge sucessfull!";
        }
    }
}
