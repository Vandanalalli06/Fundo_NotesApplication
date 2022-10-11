using Common_Layer.Model;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IUserBL
    {
        public UserEntity Register(UserRegistration userRegistration);
        public string Login(UserLoginModel userLogin);
        public string ForgotPassword(string Email);
        public bool ResetPassword(string Password, string ConfirmPassword);
    }
}
