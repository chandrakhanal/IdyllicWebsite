using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Infrastructure.Constants
{
    public class CustomConst
    {
    }
    public static class AppSettingsKeyConsts
    {
        public const string PUBLIC_ROOT_KEY = "PublicRoot";
        public const string MEDIA_ROOT_KEY = "MediaRoot";
        //public const string ErrorLogRootKey = "ErrorLogRoot";
    }
    public static class ServerRootConsts
    {
        public const string PUBLIC_ROOT = "~/writereaddata/";
        public const string MEDIA_ROOT = "~/writereaddata/media/";
        public const string USER_ROOT = "~/writereaddata/user/";
        public const string ALUMNI_ROOT = "~/writereaddata/alumni/";
        public const string SPEAKER_ROOT = "~/writereaddata/speaker/";
        public const string PRODUCT_IMAGE_ROOT = "~/writereaddata/images/products/";

        public const string PAGE_CONTENT_ROOT = "~/writereaddata/";
        public const string NEWS_CONTENT_ROOT = "~/writereaddata/";
        public const string FORUMBLOG_ROOT = "~/writereaddata/forumblog/";
        public const string ALUMNIARTICLE_ROOT = "~/writereaddata/Alumniarticle/";
        public const string ACCOMODATION_ROOT = "~/writereaddata/accomodation/";
        public const string CIRCULAR_ROOT = "~/writereaddata/circular/";
        public const string TRAINING_DOC_ROOT = "~/writereaddata/trainingdocs/";
    }
}