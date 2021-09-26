using System;
namespace CmnSoftwareBackend.Shared.Entities.Abstract
{
    public abstract class EntityBase<TKey, TUser> where TKey : struct where TUser : struct
    {
        public virtual TKey Id { get; set; }
        public virtual bool IsDeleted { get; set; } = false;
        public virtual bool IsActive { get; set; } = true;
        public virtual TUser? CreatedByUserId { get; set; }
        public virtual TUser? ModifiedByUserId { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
    }
}
