using BashrLeadCreateAPI.Models.InputRequest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BashrLeadCreateAPI.Models.InputDocRequest
{
    public class InputDocumentRequest
    {
        [Required]
        public ApplicationArea ApplicationArea
        {
            get;
            set;
        }
        [Required]
        public DocumentDataArea DataArea
        {
            get;
            set;
        }
    }
    public class DocumentDataArea
    {
        [Required]
        public string ReferenceNumber
        {
            get;
            set;
        }
        [Required]
        public Attachments[] Attachments
        {
            get;
            set;
        }

    }
    public class Attachments
    {
        public string DocumentType
        {
            get;
            set;
        }
        public string MimeType
        {
            get;
            set;
        }
        public string FileContent
        {
            get;
            set;
        }
        public string FileName
        {
            get;
            set;
        }
    }
   
}