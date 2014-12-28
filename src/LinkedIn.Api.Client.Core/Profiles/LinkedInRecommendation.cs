/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Api.Client.Core.Profiles
{
    public class LinkedInRecommendation
    {
        public int Id
        {
            get;
            set;
        }

        public LinkedInRecommender Recommender
        {
            get;
            set;
        }

        public string RecommendationText
        {
            get;
            set;
        }

        public string RecommendationType
        {
            get;
            set;
        }
    }
}
