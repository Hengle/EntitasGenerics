﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entitas;

namespace Entitas.Generics
{
    public interface IContextDefinition
    {
        ContextInfo GetContextInfo();

        int ComponentCount { get; }

        List<IComponentDefinition> Components { get; }
    }

    /// <summary>
    /// The <see cref="ContextDefinition{TContext,TEntity}"/> defines the component types that will be in the context,
    /// and produces a contextInfo object for the Context base constructor.
    /// todo: evaluate if this should be replaced by a simple collection initializer of types  
    /// </summary>
    public class ContextDefinition<TContext, TEntity> : IEnumerable<IComponentDefinition>, IContextDefinition where TContext : IContext where TEntity : class, IEntity, new()
    {
        public ContextDefinition()
        {
            AddDefaultComponents();
        }

        public void AddDefaultComponents()
        {
            Add<UniqueTagHolderComponent>();
        }

        public List<IComponentDefinition> Components { get; } = new List<IComponentDefinition>();

        public List<string> ComponentNames { get; } = new List<string>();

        public List<Type> ComponentTypes { get; } = new List<Type>();

        public int ComponentCount { get; private set; }

        public IComponentDefinition<T> Add<T>() where T : class, IComponent, new()
        {
            var def = new ComponentDefinition<TContext, TEntity, T>();
            Components.Add(def);
            ComponentNames.Add(typeof(T).Name);
            ComponentTypes.Add(typeof(T));
            ComponentCount++;
            return def;
        }

        public ContextInfo GetContextInfo()
        {
            return new ContextInfo(typeof(TContext).Name, ComponentNames.ToArray(), ComponentTypes.ToArray());
        }

        public IEnumerator<IComponentDefinition> GetEnumerator() => Components.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
