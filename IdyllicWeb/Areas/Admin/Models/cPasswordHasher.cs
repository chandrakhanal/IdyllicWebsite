﻿using IdyllicWeb.Infrastructure.Constants;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IdyllicWeb
{
    public class cPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using (SHA512 mySHA512 = SHA512Managed.Create())
            {
                byte[] hashed = mySHA512.ComputeHash(Encoding.UTF8.GetBytes(password.ToString()));
                return BitConverter.ToString(hashed).Replace("-", string.Empty);
            }
        }
        public PasswordVerificationResult VerifyHashedPassword(
          string hashedPassword, string providedPassword)
        {
            //if (HashPassword(hashedPassword + salt) == providedPassword)
            //HashPassword(providedPassword)
            string sdd = secConst.cSalt;
            if (HashPassword(hashedPassword + sdd) == providedPassword)
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
        
    }
}