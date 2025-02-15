﻿using System;
using System.Collections.Generic;
using FracturedJson.Tokenizing;

namespace FracturedJson;

/// <summary>
/// A distinct thing that can be where ever JSON values are expected in a JSON-with-comments doc.  This could be an 
/// actual data value, such as a string, number, array, etc. (generally referred to here as "elements"), or it could be
/// a blank line or standalone comment.  In some cases, comments won't be stand-alone JsonItems, but will instead
/// be attached to elements to which they seem to belong.
/// </summary>
/// <remarks>
/// Much of this data is produced by the <see cref="Parsing.Parser"/>, but some of the properties - like all of the
/// length ones - are not set by Parser, but rather, provided for use by <see cref="Formatter"/>,
/// </remarks>
public class JsonItem
{
    public JsonItemType Type { get; set; }
    
    /// <summary>
    /// Line number from the input - if available - where this element began.
    /// </summary>
    public InputPosition InputPosition { get; set; }

    /// <summary>
    /// Nesting level of this item's contents if any.  A simple item, or an empty array or object, has a complexity of
    /// zero.  Non-empty arrays/objects have a complexity 1 greater than that of their child with the greatest
    /// complexity.
    /// </summary>
    public int Complexity { get; set; }

    /// <summary>
    /// Property name, if this is an element (real JSON value) that is contained in an object.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The text value of this item, non-recursively.  Null for objects and arrays.
    /// </summary>
    public string Value { get; set; } = string.Empty;
    
    /// <summary>
    /// Comment that belongs in front of this element on the same line, if any.
    /// </summary>
    public string PrefixComment { get; set; } = string.Empty;
    
    /// <summary>
    /// Comment (or, possibly many of them) that belongs in between the property name and value, if any.
    /// </summary>
    public string MiddleComment { get; set; } = string.Empty;
    
    /// <summary>
    /// Comment that belongs in front of this element on the same line, if any.
    /// </summary>
    public string PostfixComment { get; set; } = string.Empty;
    
    /// <summary>
    /// True if the postfix comment is to-end-of-line rather than block style.
    /// </summary>
    public bool IsPostCommentLineStyle { get; set; }
    
    public int NameLength { get; set; }
    public int ValueLength { get; set; }
    public int PrefixCommentLength { get; set; }
    public int MiddleCommentLength { get; set; }
    public int PostfixCommentLength { get; set; }
    
    /// <summary>
    /// The smallest possible size this item - including all comments and children if appropriate - can be written.
    /// </summary>
    public int MinimumTotalLength { get; set; }
    
    /// <summary>
    /// True if this item can't be written on a single line.  
    /// </summary>
    /// <remarks>
    /// For example, an item ending in a postfix line comment
    /// (like // ) can often be written on a single line, because the comment is the last thing.  But if it's a
    /// container with such an item inside it, it's impossible to inline the container, because there's no way to
    /// write the line comment and then a closing bracket.
    /// </remarks>
    public bool RequiresMultipleLines { get; set; }
    
    /// <summary>
    /// List of this item's contents, if it's an array or object.
    /// </summary>
    public IList<JsonItem> Children { get; set; } = Array.Empty<JsonItem>();

    public override string ToString()
    {
        // Intended for use by debuggers.
        var shortName = (Name.Length <= 15) ? Name : Name.Substring(0, 12) + "...";
        var shortVal = (Value.Length <= 15) ? Value : Value.Substring(0, 12) + "...";
        return $"{{ Name = {shortName}, Value = {shortVal} }}";
    }
}
