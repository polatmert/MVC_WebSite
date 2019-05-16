using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities.ValueObject;
using MyEverNote.Common_.Helpers;
using MyEverNote.Entities;
using MyEverNote.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class EvernoteUserManager
    {
        private Repository<EverNoteUser> repo_user = new Repository<EverNoteUser>();

        public BusinessLayerResult<EverNoteUser> RegisterUser(RegisterViewModel data)
        {
            BusinessLayerResult<EverNoteUser> layerResult = new BusinessLayerResult<EverNoteUser>();
            EverNoteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);

            if (user != null)
            {
                if (user.Username == data.Username)
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");

                if (user.Email == data.Email)
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Email kayıtlı");
            }
            else
            {
                int dbResult = repo_user.Insert(new EverNoteUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false              
                });

                if (dbResult > 0)
                {
                    layerResult.Result = repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri=$"{siteUri}/Home/UserActivate/{layerResult.Result.ActivateGuid}";
                    string body  =($"Merhaba{layerResult.Result.Username} <br><br> Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.");
                    MailHelper.SendMail(body, layerResult.Result.Email, "MyEverNote Hersap Aktifleştirme");
                }
            }

            return layerResult;
        }

        public BusinessLayerResult<EverNoteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<EverNoteUser> layerResult = new BusinessLayerResult<EverNoteUser>();
            layerResult.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);

            if (layerResult.Result != null)
            {
                if (!layerResult.Result.IsActive)
                {
                    layerResult.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir");
                    layerResult.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz");
                }
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ve şifre uyuşmuyor");
            }

            return layerResult;
        }

        public BusinessLayerResult<EverNoteUser> ActivateUser(Guid activadeId)
        {
            BusinessLayerResult<EverNoteUser> layerResult = new BusinessLayerResult<EverNoteUser>();
            layerResult.Result = repo_user.Find(x => x.ActivateGuid == activadeId);

            if(layerResult.Result != null)
            {
                if(layerResult.Result.IsActive)
                {
                    layerResult.AddError(ErrorMessageCode.UserAlreadyActivate, "Kullanıcı zaten aktif edilmiştir.");
                    return layerResult;
                }
                layerResult.Result.IsActive = true;
                repo_user.Update(layerResult.Result);
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }
            return layerResult;
        }
    }
}
