using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GiftService
    {
        private ApplicationDbContext DbContext { get; set; }
        public GiftService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Gift AddGift(Gift gift)
        {
            DbContext.Gifts.Add(gift);

            DbContext.SaveChanges();

            return gift;
        }

        public Gift UpdateGift(Gift gift)
        {
            DbContext.Gifts.Update(gift);

            DbContext.SaveChanges();

            return gift;
        }
        public Gift Find(int id)
        {
            return DbContext.Gifts.Include(gift => gift.User)
                .SingleOrDefault(gift => gift.Id == id);
        }

        public Gift RemoveGift(Gift gift)
        {
            DbContext.Gifts.Remove(gift);
            DbContext.SaveChanges();

            return gift;
        }
    }
}
