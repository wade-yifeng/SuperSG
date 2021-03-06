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
    
    public partial class User
        : Entity
    {
        public User()
        {
        }
    
        [Column(Name = "Id", DbType = "Int32", IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "UserUniqueId", DbType = "String", CanBeNull = false)]
        public string UserUniqueId { get; set; }
        [Column(Name = "Name", DbType = "String", CanBeNull = false)]
        public string Name { get; set; }
        [Column(Name = "Gender", DbType = "Boolean", CanBeNull = false)]
        public bool Gender { get; set; }
        [Column(Name = "WeixinId", DbType = "String", CanBeNull = false)]
        public string WeixinId { get; set; }
        [Column(Name = "Avatar", DbType = "String")]
        public string Avatar { get; set; }
        [Column(Name = "Mobile", DbType = "String")]
        public string Mobile { get; set; }
        [Column(Name = "Password", DbType = "String")]
        public string Password { get; set; }
        [Column(Name = "IsOriginalPassword", DbType = "Boolean", CanBeNull = false)]
        public bool IsOriginalPassword { get; set; }
        [Column(Name = "Position", DbType = "String")]
        public string Position { get; set; }
        [Column(Name = "Country", DbType = "String")]
        public string Country { get; set; }
        [Column(Name = "Province", DbType = "String")]
        public string Province { get; set; }
        [Column(Name = "City", DbType = "String")]
        public string City { get; set; }
        [Column(Name = "District", DbType = "String")]
        public string District { get; set; }
        [Column(Name = "EntryDate", DbType = "DateTime")]
    	private Nullable<System.DateTime> _entryDate;
    	public virtual Nullable<System.DateTime> EntryDate 
    	{
    		get { return _entryDate; }
    		set 
    		{
    			if (value != null) {
    				if (value.Value.Kind == DateTimeKind.Unspecified) {
    					_entryDate = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
    				} else {
    					_entryDate = value;
    				}
    			} else {
    				_entryDate = value;
    			}
    		}
    	}
        [Column(Name = "Email", DbType = "String")]
        public string Email { get; set; }
        [Column(Name = "Status", DbType = "Int32", CanBeNull = false)]
        public int Status { get; set; }
        [Column(Name = "Grade", DbType = "Byte", CanBeNull = false)]
        public byte Grade { get; set; }
        [Column(Name = "Point", DbType = "Int32", CanBeNull = false)]
        public int Point { get; set; }
        [Column(Name = "ProductAbility", DbType = "Int32", CanBeNull = false)]
        public int ProductAbility { get; set; }
        [Column(Name = "SalesAbility", DbType = "Int32", CanBeNull = false)]
        public int SalesAbility { get; set; }
        [Column(Name = "ExhibitAbility", DbType = "Int32", CanBeNull = false)]
        public int ExhibitAbility { get; set; }
        [Column(Name = "IsAdmin", DbType = "Boolean", CanBeNull = false)]
        public bool IsAdmin { get; set; }
        [Column(Name = "IsSuperAdmin", DbType = "Boolean", CanBeNull = false)]
        public bool IsSuperAdmin { get; set; }
        [Column(Name = "LastUpdateTime", DbType = "DateTime", CanBeNull = false)]
    	private System.DateTime _lastUpdateTime;
    	public virtual System.DateTime LastUpdateTime 
    	{
    		get { return _lastUpdateTime; }
    		set 
    		{
    			if (value.Kind == DateTimeKind.Unspecified) {
    				_lastUpdateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    			} else {
    				_lastUpdateTime = value;
    			}
    		}
    	}
        [Column(Name = "IsActive", DbType = "Boolean", CanBeNull = false)]
        public bool IsActive { get; set; }
    
        private ICollection<MessageReceiver> _MessageReceiver;
        public virtual ICollection<MessageReceiver> MessageReceiver
        {
            get { return this._MessageReceiver ?? (this._MessageReceiver = new HashSet<MessageReceiver>()); }
            set { this._MessageReceiver = value; }
        }
    }
}
