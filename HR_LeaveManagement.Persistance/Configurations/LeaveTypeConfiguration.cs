using HR_LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Persistance.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType()
                {
                    Id = 1,
                    Name = "Vacation",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    DefaultDays = 10
                });
        }
    }
}
