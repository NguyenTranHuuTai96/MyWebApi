using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO.Pipelines;
using System.Threading;
using ViewModels;
using ViewModels.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Services
{
    public class UserIdentityServices : IUserIdentityServices
    {
        public IMapper _mapper;
        public IUnitOfWork _unitOfWork;
        public IEmailExtend _emailExtend;

        public UserIdentityServices(IMapper mapper,
              IUnitOfWork unitOfWork,
               IEmailExtend emailExtend
            ) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailExtend = emailExtend;
        }
        //check register user
        public async Task<IdentityResult> RegisterUserIdentityServices(UserIdentityModel userIdentityModel)
        {
            var user = _mapper.Map<UserIdentity>(userIdentityModel);
            var rsIdentity = await _unitOfWork._userIdentityRepository.CheckAndCreatePassword(user, user.PasswordHash??string.Empty);
            return rsIdentity;
        }
        //add role for user
        public async Task<IdentityResult> AddRoles(UserIdentityModel userIdentityModel)
        {
            UserIdentity userIdentity = await _unitOfWork._userIdentityRepository.CheckLoginUserIdentityEF(userIdentityModel.UserName, userIdentityModel.Password);
            var rsIdentity = await _unitOfWork._userIdentityRepository.AddRoleForUser(userIdentity);
            return rsIdentity;
        }
        // get model UserIdentityModel
        //public async Task<UserIdentityModel> CheckLoginUserIdentityServices(string username, string password)
        //{
        //    var dataUserIdentity =await _unitOfWork._userIdentityRepository.CheckLoginUserIdentityEF(username, password);
        //    if (dataUserIdentity is null ) return null;
        //    var userIdentityModel = _mapper.Map<UserIdentityModel>(dataUserIdentity);
        //    return userIdentityModel;
        //}

        public async Task<string> TokenMailConfirm(UserIdentityModel userIdentityModel)
        {
            UserIdentity userIdentity = await _unitOfWork._userIdentityRepository.CheckLoginUserIdentityEF(userIdentityModel.UserName, userIdentityModel.Password);
            string token = await _unitOfWork._userIdentityRepository.GenerateEmailConfirmationToken(userIdentity);
            return token;
        }
        public async Task<ModelConfirmMail> SetDataModelConfirmMail(UserIdentityModel userIdentityModel)
        {
            ModelConfirmMail modelConfirmMail = new ModelConfirmMail();
            UserIdentity userIdentity = await _unitOfWork._userIdentityRepository.CheckLoginUserIdentityEF(userIdentityModel.UserName, userIdentityModel.Password);
            string TokenConfirmMail = await TokenMailConfirm(userIdentityModel);
            modelConfirmMail.TokenConfirmMail = TokenConfirmMail;
            modelConfirmMail.IdUser = userIdentity.Id;
            return modelConfirmMail;
        }
        // send mail for user to confirm after created account
        public async Task SendMailForUserConfirm(UserIdentityModel userIdentityModel, string url, CancellationToken cancellationToken)
        {
            UserIdentity userIdentity = await _unitOfWork._userIdentityRepository.CheckLoginUserIdentityEF(userIdentityModel.UserName, userIdentityModel.Password);
            string body = await _emailExtend.GetTemplate("Templates\\ConfirmEmail.html");
            body = string.Format(body, userIdentity.FullName, url);

            await _emailExtend.SendEmailAsync(cancellationToken, new EmailRequest
            {
                To = userIdentity.Email,
                Subject = "Confirm Email For Register",
                Content = body
            });
        }
        public async Task<UserIdentityModel> GetObjectByID(string userId)
        {
            UserIdentity userIdentity = await _unitOfWork._userIdentityRepository.GetObjectUserIdentity(userId);
            var userIdentityModel = _mapper.Map<UserIdentityModel>(userIdentity);
            return userIdentityModel;
        }
        public async Task<IdentityResult> UpdateConfirmMail(UserIdentityModel userIdentityModel, string TokenConfirmMail)
        {
            UserIdentity userIdentity = await _unitOfWork._userIdentityRepository.CheckLoginUserIdentityEF(userIdentityModel.UserName, userIdentityModel.Password);
            IdentityResult identityResult = await _unitOfWork._userIdentityRepository.ConfirmMail(userIdentity, TokenConfirmMail);
            return identityResult;
        }


    }
}
