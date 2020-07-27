using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using Nikki.Core;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;
using CoreExtensions.Reflection;
using CoreExtensions.Conversions;



namespace Nikki.Reflection.Abstract
{
    /// <summary>
    /// <see cref="Collectable"/> class is a default collection of properties and fields of any 
    /// global type, which information can be accessed and modified through those properties. 
    /// It inherits from <see cref="IReflective"/> class and <see cref="ICastable{TypeID}"/> 
    /// interface and implements/overrides most of their methods.
    /// </summary>
    [DebuggerDisplay("CollectionName = {CollectionName}")]
	public abstract class Collectable : IGameSelectable, IReflective, ICastable<Collectable>
	{
        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        public abstract string CollectionName { get; set; }

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
                {

                    yield return property.Name;

                }
            
            }
        }

        /// <summary>
        /// Gets <see cref="SubPart"/> in the collection.
        /// </summary>
        /// <param name="name">Name of the <see cref="SubPart"/>.</param>
        /// <param name="node">Node to which subpart belongs to, mainly Name of 
        /// the <see cref="ExpandableAttribute"/>.</param>
        /// <returns><see cref="SubPart"/> of the collection if exists; null otherwise.</returns>
        public SubPart GetSubPart(string name, string node)
        {
            var property = this.GetFastProperty(name);
            if (property == null) return null;
            
            foreach (var obj in property.GetCustomAttributes(typeof(ExpandableAttribute), true))
            {
            
                var attrib = obj as ExpandableAttribute;
                if (attrib.Name == node) return (SubPart)property.GetValue(this);
            
            }
            
            return null;
        }

        /// <summary>
        /// Attempts to get <see cref="SubPart"/> in the collection.
        /// </summary>
        /// <param name="name">Name of the <see cref="SubPart"/>.</param>
        /// <param name="node">Node to which subpart belongs to, mainly Name of
        /// the <see cref="ExpandableAttribute"/>.</param>
        /// <param name="part"><see cref="SubPart"/> in case exists; null otherwise.</param>
        /// <returns>True if <see cref="SubPart"/> exists; false otherwise.</returns>
        public bool GetSubPart(string name, string node, out SubPart part)
        {
            part = null;
            var property = this.GetFastProperty(name);
            if (property == null) return false;
            
            foreach (var obj in property.GetCustomAttributes(typeof(ExpandableAttribute), true))
            {
            
                var attrib = obj as ExpandableAttribute;
                
                if (attrib.Name == node)
                {
                
                    part = (SubPart)property.GetValue(this);
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
            return !(property is null) && Attribute.IsDefined(property, typeof(AccessModifiableAttribute));
        }

        /// <summary>
        /// Returns the value of a field name provided.
        /// </summary>
        /// <param name="propertyName">Field name to get the value from.</param>
        /// <returns>String value of a field name.</returns>
        public string GetValue(string propertyName) =>
            this.GetFastPropertyValue(propertyName)?.ToString() ?? String.Empty;

        /// <summary>
        /// Sets value at a field specified.
        /// </summary>
        /// <param name="propertyName">Name of the field to be modified.</param>
        /// <param name="value">Value to be set at the field specified.</param>
        /// <returns>True on success; false otherwise.</returns>
        public void SetValue(string propertyName, object value)
        {
            var property = this.GetFastProperty(propertyName);

            if (property == null)
            {

                throw new InfoAccessException(propertyName);

            }

            if (!Attribute.IsDefined(property, typeof(AccessModifiableAttribute)))
            {

                throw new ArgumentException($"Field or property named {propertyName} is not accessible");

            }

            if (property.PropertyType.IsEnum)
            {

                property.SetValue(this, System.Enum.Parse(property.PropertyType, value.ToString()));

            }
            else
            {

                property.SetValue(this, value.ReinterpretCast(property.PropertyType));

            }
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public abstract Collectable MemoryCast(string CName);
    
        /// <summary>
        /// Casts all memory of one object to another, considering they are of the same type.
        /// </summary>
        /// <param name="from"><see cref="Collectable"/> to cast memory from.</param>
        /// <param name="to"><see cref="Collectable"/> to cast memory to.</param>
        public virtual void MemoryCast(Collectable from, Collectable to)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            foreach (var property in from.GetType().GetProperties(flags))
            {

                // If property has a MemoryCastable attribute, simply set same value
                if (Attribute.IsDefined(property, typeof(MemoryCastableAttribute)))
                {

                    property.SetValue(to, property.GetValue(from));

                }

                // Else if property has an Expandable attribute, clone all values
                else if (Attribute.IsDefined(property, typeof(ExpandableAttribute)))
                {

                    var nodefrom = property.GetValue(from) as SubPart;
                    var nodeto = property.GetValue(to) as SubPart;
                    nodeto.CloneValuesFrom(nodefrom);

                }

            }

            foreach (var field in from.GetType().GetFields(flags))
            {

                // If field has a MemoryCastable attribute, simply set same value
                if (Attribute.IsDefined(field, typeof(MemoryCastableAttribute)))
                {

                    field.SetValue(to, field.GetValue(from));

                }

            }
        }
    }
}