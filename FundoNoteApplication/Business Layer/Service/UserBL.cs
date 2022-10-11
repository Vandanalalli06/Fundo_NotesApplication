using Business_Layer.Interface;
using Common_Layer.Model;
using Repository_Layer.Entity;
using Repository_Layer.Interafce;
using Repository_Layer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserEntity Register(UserRegistration userRegistration)
        {
            try
            {
                return userRL.Register(userRegistration);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLoginModel userLogin)
        {
            try
            {
                return userRL.Login(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgotPassword(string Email)
        {
            try
            {
                return userRL.ForgotPassword(Email);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string Password, string ConfirmPassword)
        {
            try
            {
                return userRL.ResetPassword(Password, ConfirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
    

  


