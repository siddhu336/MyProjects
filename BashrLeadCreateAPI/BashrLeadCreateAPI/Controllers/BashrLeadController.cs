using BashrLeadCreateAPI.Businesslogic;
using BashrLeadCreateAPI.Models;
using BashrLeadCreateAPI.Models.InputDocRequest;
using BashrLeadCreateAPI.Models.InputRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BashrLeadCreateAPI.Controllers
{
    [Authorize]
    public class BashrLeadController : ApiController
    {
        public OutputResponse SendLead(InputRequest enquiryDetails)
        {
            PushDataToCRM pushToCRM = new PushDataToCRM();
            return (OutputResponse)pushToCRM.CreateSMEApplication(enquiryDetails);
        }
        public OutputResponse UploadSMEDocumentsToLead(InputDocumentRequest enquiryDetails)
        {
            PushDataToCRM pushToCRM = new PushDataToCRM();
            return (OutputResponse)pushToCRM.UploadDocuments(enquiryDetails);
        }
    }
}
