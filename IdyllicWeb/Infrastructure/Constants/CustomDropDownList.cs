using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdyllicWeb.Infrastructure.Constants
{
    public static class CustomDropDownList
    {
        public static IEnumerable<SelectListItem> GetGender()
        {
            var genders = new List<SelectListItem>
            {
                new SelectListItem{ Text="Male", Value = "Male" },
                new SelectListItem{ Text="Female", Value = "Female" },
            };
            return genders;
        }
        public static IEnumerable<SelectListItem> GetMaritalStatus()
        {
            var genders = new List<SelectListItem>
            {
                new SelectListItem{ Text="Married", Value = "Married" },
                new SelectListItem{ Text="Unmarried", Value = "Unmarried" },
            };
            return genders;
        }
        public static IEnumerable<SelectListItem> GetBloodGroups()
        {
            var genders = new List<SelectListItem>
            {
                new SelectListItem{ Text="A+", Value = "A+" },
                new SelectListItem{ Text="O+", Value = "O+" },
                new SelectListItem{ Text="B+", Value = "B+" },
                new SelectListItem{ Text="AB+", Value = "AB+" },
                new SelectListItem{ Text="A-", Value = "A-" },
                new SelectListItem{ Text="O-", Value = "O-" },
                new SelectListItem{ Text="B-", Value = "B-" },
                new SelectListItem{ Text="AB-", Value = "AB-" },
            };
            return genders;
        }
        public static IEnumerable<SelectListItem> GetYesNoOpt()
        {
            var options = new List<SelectListItem>
            {
                new SelectListItem{ Text="YES", Value = "YES" },
                new SelectListItem{ Text="NO", Value = "NO" },
            };
            return options;
        }
        public static IEnumerable<SelectListItem> GetArea()
        {
            var areas = new List<SelectListItem>
            {
                new SelectListItem{ Text="NA", Value = "NA" },
                new SelectListItem{ Text="Admin", Value = "Admin" },
                new SelectListItem{ Text="Member", Value = "Member" },
                new SelectListItem{ Text="Staff", Value = "Staff" },
                new SelectListItem{ Text="Alumni", Value = "Alumni" },
            };
            return areas;
        }

        public static IEnumerable<SelectListItem> GetCastCommunity()
        {
            var community = new List<SelectListItem>
            {
                new SelectListItem{ Text="SC", Value = "SC" },
                new SelectListItem{ Text="AT", Value = "AT" },
                new SelectListItem{ Text="OBC", Value = "OBC" },
                new SelectListItem{ Text="UR", Value = "UR" },
                new SelectListItem{ Text="MBC", Value = "MBC" },
                new SelectListItem{ Text="DNC", Value = "DNC" },
                new SelectListItem{ Text="OC", Value = "OC" },

            };
            return community;
        }
    }
}