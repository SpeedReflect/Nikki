﻿using System;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;
using CoreExtensions.Reflection;
using CoreExtensions.Management;
using CoreExtensions.Conversions;



namespace Nikki.Reflection.Abstract
{
    /// <summary>
    /// <see cref="ACollectable"/> class is a default collection of properties and fields of any 
    /// global type, which information can be accessed and modified through those properties. 
    /// It inherits from <see cref="IReflective"/> class and <see cref="ICastable{TypeID}"/> 
    /// interface and implements/overrides most of their methods.
    /// </summary>
	public abstract class ACollectable : IReflective, ICastable<ACollectable>
	{
        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        public abstract string CollectionName { get; set; }

        /// <summary>
        /// True if collection can be deleted from the database; false otherwise.
        /// </summary>
        public virtual bool Deletable { get; set; } = true;

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        public abstract GameINT GameINT { get; }

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        public abstract string GameSTR { get; }

        /// <summary>
        /// Gets <see cref="IEnumerable{T}"/> of all properties with 
        /// <see cref="AccessModifiableAttribute"/> attribute.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of strings.</returns>
        public IEnumerable<string> GetAccessibles()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if (Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
                    yield return property.Name;
            }
        }

        /// <summary>
        /// Gets <see cref="ASubPart"/> in the collection.
        /// </summary>
        /// <param name="name">Name of the <see cref="ASubPart"/>.</param>
        /// <param name="node">Node to which subpart belongs to, mainly Name of 
        /// the <see cref="ExpandableAttribute"/>.</param>
        /// <returns><see cref="ASubPart"/> of the collection if exists; null otherwise.</returns>
        public ASubPart GetSubPart(string name, string node)
        {
            var property = this.GetFastProperty(name);
            if (property == null) return null;
            foreach (var obj in property.GetCustomAttributes(typeof(ExpandableAttribute), true))
            {
                var attrib = obj as ExpandableAttribute;
                if (attrib.Name == node) return (ASubPart)property.GetValue(this);
            }
            return null;
        }

        /// <summary>
        /// Attempts to get <see cref="ASubPart"/> in the collection.
        /// </summary>
        /// <param name="name">Name of the <see cref="ASubPart"/>.</param>
        /// <param name="node">Node to which subpart belongs to, mainly Name of
        /// the <see cref="ExpandableAttribute"/>.</param>
        /// <param name="part"><see cref="ASubPart"/> in case exists; null otherwise.</param>
        /// <returns>True if <see cref="ASubPart"/> exists; false otherwise.</returns>
        public bool GetSubPart(string name, string node, out ASubPart part)
        {
            part = null;
            var property = this.GetFastProperty(name);
            if (property == null) return false;
            foreach (var obj in property.GetCustomAttributes(typeof(ExpandableAttribute), true))
            {
                var attrib = obj as ExpandableAttribute;
                if (attrib.Name == node)
                {
                    part = (ASubPart)property.GetValue(this);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets all nodes and subnodes from the class.
        /// </summary>
        /// <returns>Array of virtual nodes that can be used to build treeview.</returns>
        public virtual List<VirtualNode> GetAllNodes()
        {
            var list = new List<VirtualNode>();
            foreach (var property in this.GetType().GetProperties())
            {
                foreach (var obj in property.GetCustomAttributes(typeof(ExpandableAttribute), true))
                {
                    var attrib = obj as ExpandableAttribute;
                    var node = list.Find(c => c.NodeName == attrib.Name);
                    if (node == null)
                    {
                        node = new VirtualNode(attrib.Name);
                        list.Add(node);
                    }
                    node.SubNodes.Add(new VirtualNode(property.Name));
                }
            }
            list.Sort((x, y) => x.NodeName.CompareTo(y.NodeName));
            return list;
        }

        /// <summary>
        /// Checks if this class contains property with name specified that has AccessModifiable attribute.
        /// </summary>
        /// <param name="PropertyName">Name of the property to check.</param>
        /// <returns>True if property exists and has AccessModifiable attribute; false otherwise.</returns>
        public virtual bool ContainsAccessModifiable(string PropertyName)
        {
            var property = this.GetFastProperty(PropertyName);
            return property == null ? false : Attribute.IsDefined(property, typeof(AccessModifiableAttribute));
        }

        /// <summary>
        /// Returns the value of a field name provided.
        /// </summary>
        /// <param name="PropertyName">Field name to get the value from.</param>
        /// <returns>String value of a field name.</returns>
        public string GetValue(string PropertyName) 
            => this.GetFastPropertyValue(PropertyName)?.ToString();

        /// <summary>
        /// Sets value at a field specified.
        /// </summary>
        /// <param name="PropertyName">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool SetValue(string PropertyName, object value)
        {
            try
            {
                var property = this.GetFastProperty(PropertyName);
                if (property == null) return false;
                if (!Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
                    throw new FieldAccessException("This field is either non-modifiable or non-accessible");
                if (property.PropertyType.IsEnum)
                    property.SetValue(this, System.Enum.Parse(property.PropertyType, value.ToString()));
                else
                    property.SetValue(this, value.ReinterpretCast(property.PropertyType));
                return true;
            }
            catch (System.Exception) { return false; }
        }

        /// <summary>
        /// Sets value at a field specified.
        /// </summary>
        /// <param name="PropertyName">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <param name="error">Error occured when trying to set value.</param>
        /// <returns>True on success; false otherwise.</returns>
        public bool SetValue(string PropertyName, object value, out string error)
        {
            error = null;
            try
            {
                var property = this.GetFastProperty(PropertyName);
                if (property == null)
                {
                    error = $"Field named {PropertyName} does not exist.";
                    return false;
                }
                if (!Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
                    throw new FieldAccessException("This field is either non-modifiable or non-accessible");
                if (property.PropertyType.IsEnum)
                    property.SetValue(this, System.Enum.Parse(property.PropertyType, value.ToString()));
                else
                    property.SetValue(this, value.ReinterpretCast(property.PropertyType));
                return true;
            }
            catch (System.Exception e)
            {
                error = e.GetLowestMessage();
                return false;
            }
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public abstract ACollectable MemoryCast(string CName);
    }
}