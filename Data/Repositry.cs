using IdentityInCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityInCore.Data
{
    public class Repositry<T>:IRepositry<T> where T:class
    {
        private readonly UserContext context;
        //private readonly ILogger<Repositry<T>> logger;
        private DbSet<T> _entities;

        /// <summary>
        /// Initializing scontex
        /// </summary>
        /// <param name="scontext"> argument of type UserContext</param>
        public Repositry(UserContext scontext/*, ILogger<Repositry<T>> logger*/)
        {
            this.context = scontext;
            //this.logger = logger;
            _entities = context.Set<T>();
        }

        /// <summary>
        /// fn to get Iqueryable list of entity
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this._entities;
            }
        }

        /// <summary>
        /// fn to delte entity from database
        /// </summary>
        /// <param name="entity"> record of type T :where T is class </param>
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this._entities.Remove(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// fn to get particular entity record 
        /// </summary>
        /// <param name="id"> take id as argument</param>
        /// <returns></returns>
        T IRepositry<T>.GetById(object id)
        {
            return this._entities.Find(id);
        }

        /// <summary>
        /// fn to inser new entity record 
        /// </summary>
        /// <param name="entity">record of type T //where T:class</param>
        public void Insert(T entity)
        {
            try
            {
                
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this._entities.Add(entity);
                this.context.SaveChanges();
            }


                
            
            catch (ArgumentNullException dbEx)
            {
                //logger.LogError(dbEx.Message);
                throw new Exception($"{nameof(entity)} could not be saved: {dbEx.Message}");
            }
        


    }

        /// <summary>
        /// fn to update existing record 
        /// </summary>
        /// <param name="entity">record of type T //where T:class</param>
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.context.SaveChanges();
            }
                
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }


        }
    }
}
