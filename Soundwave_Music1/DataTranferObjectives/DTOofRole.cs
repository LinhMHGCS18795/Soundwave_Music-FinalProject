using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Soundwave_Music1.DataTranferObjectives
{
    public class DTOofRole
    {
        public List<CheckPermissionToRole> _Role_Permission { get; set; }
        public int Role_id { get; set; }
        public string Role_name { get; set; }
        public int Count_account_role { get; set; }
        public int Permission_id { get; set; }
        public string Permission_name { get; set; }

        public class CheckPermissionToRole
        {
            public int Role_id { get; set; }
            public int Permission_id { get; set; }
            public string Permission_name { get; set; }
            public bool Checked { get; set; }
        }
    }
}