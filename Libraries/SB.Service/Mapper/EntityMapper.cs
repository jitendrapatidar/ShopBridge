using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SB.Model;
using SB.Repository.TableModel;

namespace SB.Service 
{
    

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public class EntityMapper<TSource, TDestination> where TSource : class where TDestination : class
    {
        private readonly IMapper _mapper;

        public EntityMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });
           _mapper = config.CreateMapper();

        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TDestination Translate(TSource obj)
        {
            
          return _mapper.Map<TDestination>(obj);
        }
    }
}
