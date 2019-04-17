using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities.ValueObject;
using MyEverNote.Entities;
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
                    layerResult.Errors.Add("Kullanıcı adı kayıtlı");

                if (user.Email == data.Email)
                    layerResult.Errors.Add("Kullanıcı adı kayıtlı");
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
                if(!layerResult.Result.IsActive)
                    layerResult.Errors.Add("Kullanıcı aktifleştirilmemiştir.Lütfen e-posta adresinizi kontrol ediniz.");
            }
            else
            {
                layerResult.Errors.Add("Kullanıcı adı ve şifre uyuşmuyor");
            }

            return layerResult;
        }
    }
}
