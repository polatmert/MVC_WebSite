using MyEverNote.Common_;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.WebApp.Initializer
{
    public class WebCommon:ICommon_
    {
        public string GetCurrentUsername()
        {
            if(HttpContext.Current.Session["login"]!=null)
            {
                EverNoteUser user = HttpContext.Current.Session["login"] as EverNoteUser;
                return user.Username;
            }

            return "system";
        }
    }
}