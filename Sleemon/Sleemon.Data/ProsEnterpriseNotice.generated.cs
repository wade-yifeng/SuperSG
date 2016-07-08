//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sleemon.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.Mapping;
    
    public partial class ProsEnterpriseNotice
        : Entity
    {
        [Column(Name = "Id", DbType = "Int32", IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "UserUniqueId", DbType = "String", CanBeNull = false)]
        public string UserUniqueId { get; set; }
        [Column(Name = "EnterpriseNoticeId", DbType = "Int32", CanBeNull = false)]
        public int EnterpriseNoticeId { get; set; }
        [Column(Name = "ProsDateTime", DbType = "DateTime", CanBeNull = false)]
    	private System.DateTime _prosDateTime;
    	public virtual System.DateTime ProsDateTime 
    	{
    		get { return _prosDateTime; }
    		set 
    		{
    			if (value.Kind == DateTimeKind.Unspecified) {
    				_prosDateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    			} else {
    				_prosDateTime = value;
    			}
    		}
    	}
        [Column(Name = "GroupKey", DbType = "Guid")]
        public Nullable<System.Guid> GroupKey { get; set; }
        [Column(Name = "Status", DbType = "Int32", CanBeNull = false)]
        public int Status { get; set; }
    
        public virtual EnterpriseNotice EnterpriseNotice { get; set; }
    }
}
