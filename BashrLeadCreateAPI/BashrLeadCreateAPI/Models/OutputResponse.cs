using BashrLeadCreateAPI.Models.InputRequest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace BashrLeadCreateAPI.Models
{
    public class OutputResponse
    {
       [Required]
        public ApplicationArea applicationArea
        {
            get;
            set;
        }
        [Required]
        public DataArea dataArea
        {
            get;
            set;
        }
        [Required]
        public ResponseStatus ResponseStatus
        {
            get;
            set;
        }

    }
    public class ResponseStatus
    {
        [Required]
        public string Status
        {
            get;
            set;
        }
        [Required]
        public HttpStatusCode StatusCode
        {
            get;
            set;
        }
        [Required]
        public ErrorDetails[] errorDetails
        {
            get;
            set;
        }
    }
    public class ErrorDetails
    {
        [Required]
        public string ErrorCode
        {
            get;
            set;
        }
        [Required]
        public string ErrorDesc
        {
            get;
            set;
        }
        
        public enum ErrorType
        {
            BusinessError=0,
            TechnicalError=1,
            UnknownError=2
        }
    }
}