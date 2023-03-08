﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.Exceptions
{
    public class ExceptionCodes
    {
        public static string DefaultError = "DEFAULT_ERROR";

        public static string CorruptedToken = "CORRUPTED_TOKEN";
        public static string DatabaseError = "DATABASE_ERROR";

        public static string UserNotFound = "USER_NOT_FOUND";
        public static string LoginUsernameNotFound = "LOGIN_USERNAME_NOT_FOUND";
        public static string LoginWrongPassword = "LOGIN_WRONG_PASSWORD";
        public static string RegisterEmailFound = "REGISTER_EMAIL_FOUND";
        public static string RegisterUsernameFound = "REGISTER_USERNAME_FOUND";

        public static string CountryCodeNotFound = "COUNTRY_CODE_NOT_FOUND";
        public static string RefreshTokenWrong = "REFRESHTOKEN_WRONG";
    }
}
