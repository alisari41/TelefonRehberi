using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.SeleniumTestTool.UserOperationClaim.Models
{
    public class Elements
    {
        public string Link { get; set; }

        public string XPathOperationClaimEdit { get; set; }
        public string XPathOperationClaimDelete { get; set; }
        public string XPathUserOperationClaimEdit { get; set; }
        public string XPathUserOperationClaimDelete { get; set; }


        public string BtnGuvenlikDegil { get; set; }
        public string BtnGuvenlikDegilKabulEt { get; set; }
        public string BtnAdmin { get; set; }
        public string BtnSignIn { get; set; }
        public string BtnLogout { get; set; }
        public string BtnKullancilar { get; set; }
        public string BtnOperationClaims { get; set; }
        public string BtnUserOperationClaims { get; set; }
        public string BtnTables { get; set; }
        public string BtnAdd { get; set; }
        public string BtnEdit { get; set; }
        public string BtnDelete { get; set; }


        public string TxtGirisEmail { get; set; }
        public string TxtGirisPassword { get; set; }
        public string TxtRol { get; set; }
        public string TxtKontrol { get; set; }
        public string TxtError { get; set; }


        public string TxtRolId { get; set; }
        public string TxtUserId { get; set; }
    }
}
