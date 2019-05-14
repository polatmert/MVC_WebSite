using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities.ValueObject;
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

                if (dbResult > 1)
                {
                    layerResult.Result = repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);

                    //TODO:Aktivasyon maili atılacak
                    // layerResult.Result.ActivateGuid
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
    }
}
