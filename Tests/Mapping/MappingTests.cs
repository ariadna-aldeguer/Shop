using Api.Models.Dtos;
using Api.Models.Profiles;
using AutoMapper;
using Data.Database.Entities;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mapping
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {

            _configuration = new MapperConfiguration(cfg => 
                    cfg.AddProfile<ProductProfile>());
            _configuration.AssertConfigurationIsValid();

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(Product), typeof(ProductDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            return RuntimeHelpers.GetUninitializedObject(type);
        }
    }
}
