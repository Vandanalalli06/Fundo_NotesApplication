using Common_Layer.Model;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interafce
{
    public interface IUserRL
    {
        public UserEntity Register(UserRegistration userRegistration);
        public string Login(UserLoginModel userLogin);
        public string ForgotPassword(string Email);
        public bool ResetPassword(string Password, string ConfirmPassword);
    }
}


