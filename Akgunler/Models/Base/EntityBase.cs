using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models
{
    /// <summary>
    ///     Provides a base class for your objects which will be persisted to the database.
    ///     Benefits include the addition of an Id property along with a consistent manner for comparing
    ///     entities.
    ///     Since nearly all of the entities you create will have a type of int Id, this
    ///     base class leverages this assumption.  If you want an entity with a type other
    ///     than int, such as string, then use <see cref="EntityBaseWithTypedId{TId}" /> instead.
    /// </summary>
    public abstract class EfEntityBase : EntityBaseWithTypedId<long>
    {
    }

    public abstract class CrudBase : EntityBase
    {
        [Column(TypeName = "DateTime")]
        public DateTime? CreatedOn { get; set; }
        
        public long? CreatedById { get; set; }
        
        //public virtual Person CreatedBy { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? UpdatedOn { get; set; }

        public long? UpdatedById { get; set; }

        //public virtual Person UpdatedBy { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? DeletedOn { get; set; }

        public long? DeletedById { get; set; }

        //public virtual Person DeletedBy { get; set; }
    }
}
