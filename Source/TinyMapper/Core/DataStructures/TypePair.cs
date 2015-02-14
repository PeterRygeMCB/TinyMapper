﻿using System;
using Nelibur.ObjectMapper.Core.Extensions;

namespace Nelibur.ObjectMapper.Core.DataStructures
{
    internal struct TypePair : IEquatable<TypePair>
    {
        public TypePair(Type source, Type target) : this()
        {
            Target = target;
            Source = source;
        }

        public bool IsDeepCloneable
        {
            get
            {
                if (IsEqualTypes == false)
                {
                    return false;
                }
                if (IsValueTypes && IsPrimitiveTypes)
                {
                    return true;
                }
                if (Source == typeof(string))
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsEnumTypes
        {
            get { return Source.IsEnum && Target.IsEnum; }
        }

        public bool IsEnumerableTypes
        {
            get { return Source.IsIEnumerable() && Target.IsIEnumerable(); }
        }

        public bool IsEqualTypes
        {
            get { return Source == Target; }
        }

        public Type Source { get; private set; }
        public Type Target { get; private set; }

        private bool IsPrimitiveTypes
        {
            get { return Source.IsPrimitive && Target.IsPrimitive; }
        }

        private bool IsValueTypes
        {
            get { return Source.IsValueType && Target.IsValueType; }
        }

        public static TypePair Create(Type source, Type target)
        {
            return new TypePair(source, target);
        }

        public static TypePair Create<TSource, TTarget>()
        {
            return new TypePair(typeof(TSource), typeof(TTarget));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is TypePair && Equals((TypePair)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0) * 397) ^ (Target != null ? Target.GetHashCode() : 0);
            }
        }

        public bool Equals(TypePair other)
        {
            return Source == other.Source && Target == other.Target;
        }
    }
}
