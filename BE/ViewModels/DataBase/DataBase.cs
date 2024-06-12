using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.DataBase
{
    public enum WorkingStatus
    {
        None = 0,
        AddNew = 1,
        Update = 2,
        Delete = 3
    }
    public class UpdateResultExtend<T>
    {
        public T ID { get; set; } 
        public SaveStatus UpdateStatus { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Index { get; set; }
        public string CD { get; set; } = string.Empty;
        public string Seq { get; set; } = string.Empty;
        public UpdateResultExtend()
        {
            this.UpdateStatus = SaveStatus.Success;
        }
    }
    public enum SaveStatus
    {

        Fail = -1,
        FailNextProcess = -2,
        Success = 0,
        Warning = 1,
    }
    public class BaseViewModelExtend<T>
    {
        [Required]
        public T Id { get; set; }
        public WorkingStatus Working { get; set; }

        //public string UpdateBy { get; set; }
        //public string UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }

}
