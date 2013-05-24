using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AzurePhotoManager.Models
{
    public class PhotoRepository : IPhotoRepository
    {
        AzurePhotoManagerContext context = new AzurePhotoManagerContext();

        public IQueryable<Photo> All
        {
            get { return context.Photos; }
        }

        public IQueryable<Photo> AllIncluding(params Expression<Func<Photo, object>>[] includeProperties)
        {
            IQueryable<Photo> query = context.Photos;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Photo Find(int id)
        {
            return context.Photos.Find(id);
        }

        public void InsertOrUpdate(Photo photo)
        {
            using (AzurePhotoManagerContext context = new AzurePhotoManagerContext())
            {
                Photo current = null;
                if (photo.Id == default(int))
                {
                    // New entity
                    current = context.Photos.FirstOrDefault<Photo>(p => p.Name == photo.Name);
                    if (current == null)
                    {
                        context.Photos.Add(photo);
                    }
                    else
                    {
                        photo.Id = current.Id;
                        context.Entry(photo).State = EntityState.Modified;
                    }
                }
                else
                {
                    // Existing entity
                    context.Entry(photo).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (AzurePhotoManagerContext context = new AzurePhotoManagerContext())
            {
                var photo = context.Photos.Find(id);
                context.Photos.Remove(photo);
                context.SaveChanges();
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

    public interface IPhotoRepository : IDisposable
    {
        IQueryable<Photo> All { get; }
        IQueryable<Photo> AllIncluding(params Expression<Func<Photo, object>>[] includeProperties);
        Photo Find(int id);
        void InsertOrUpdate(Photo photo);
        void Delete(int id);
        void Save();
    }
}