﻿using VetAppointment.Application.Repositories.Base;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Infrastructure.Context;
using VetAppointment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VetAppointment.Application.Repositories.Impl
{
    public class DrugRepository : BaseRepository<Drug>, IDrugRepository
    {
        public DrugRepository(IDatabaseContext context) : base(context) { }

        public async Task DeleteAsync(Drug drug)
        {
            context.Remove(drug);
            await context.Save();
        }

        public async Task<Drug> UpdateAsync(Drug drug)
        {
            context.Set<Drug>().Update(drug);
            await context.Save();
            return drug;
        }
    }
}
