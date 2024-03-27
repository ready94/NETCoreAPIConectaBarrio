using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Services
{
    public class ComplaintService : IComplaintService
    {
        private const string TABLE = "SYS_T_COMPLAINTS";
    
        private IUserService _userSvc;

        public ComplaintService(IUserService userSvc)
        {
            _userSvc = userSvc;
        }

        public bool CreateComplaint(ComplaintModel complaint)
        {
            bool res = false;

            if (complaint != null)
            {
                string[] fields = ["IDCOMPLAINT_TYPE", "IDPRIORITY", "COMPLAINT_TITLE", "COMPLAINT_DESCRIPTION", "CREATION_USER", "CREATION_DATE", "ACTIVE"];
                object[] values = [complaint.IdComplaintType, complaint.IdPriority, complaint.Title, complaint.Description, complaint.CreationUser, complaint.CreationDate, true];
                res = SQLConnectionHelper.InsertBBDD(TABLE, fields, values);
            }

            return res;
        }

        public bool DeleteComplaint(int idUser, int idComplaint)
        {
            // Si el usuario es admin, se hace un borrado fisico, si no, un borrado logico
            if (this._userSvc.GetUserRole(idUser) == EnumRoles.ADMIN)
                return SQLConnectionHelper.DeleteBBDD(TABLE, ["IDCOMPLAINT_TYPE"], [idComplaint], [SQLRelationType.EQUAL]);
            else
                return SQLConnectionHelper.UpdateBBDD(TABLE, ["ACTIVE"], [false], ["IDCOMPLAINT_TYPE"], [idComplaint]);

        }

        public List<ComplaintModel> GetAllComplaints()
        {
            List<ComplaintModel> complaints = [];

            DataTable dt = SQLConnectionHelper.GetResultTable(TABLE);
            foreach (DataRow row in dt.Rows)
                complaints.Add(new ComplaintModel(row));

            return complaints;
        }

        public ComplaintModel? GetComplaint(int idComplaint)
        {
            DataRow? row = SQLConnectionHelper.GetResult(TABLE, ["IDCOMPLAINT"], [idComplaint], [SQLRelationType.EQUAL]);

            return new ComplaintModel(row);
        }

        public bool UpdateComplaint(ComplaintModel complaint, int idUser)
        {
            if (complaint != null)
            {
                string[] fields = ["IDCOMPLAINT_TYPE", "IDPRIORITY", "COMPLAINT_TITLE", "COMPLAINT_DESCRIPTION", "MODIFICATION_USER", "MODIFICATION_DATE", "ACTIVE"];
                object[] values = [complaint.IdComplaintType, complaint.IdPriority, complaint.Title, complaint.Description, idUser, DateTime.Now, complaint.Active];
                string[] fieldsFilter = ["IDCOMPLAINT"];
                object[] valuesFilter = [complaint.IdComplaint];

                return SQLConnectionHelper.UpdateBBDD(TABLE, fields, values, fieldsFilter, valuesFilter);
            }
            else return false;
        }
    }
}
