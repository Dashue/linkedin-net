/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Api.Client.Core.Companies
{
    public class LinkedInCompany
    {
        public int? Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public String Industry
        {
            get;
            set;
        }

        public string CompanySize
        {
            get;
            set;
        }

        public string CompanyType
        {
            get;
            set;
        }
    }
}
