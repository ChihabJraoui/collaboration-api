using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Mapping
{
    public interface IMapForm<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
