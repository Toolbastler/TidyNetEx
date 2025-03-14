using System;
	
/*
* Copyright (c) 2000 World Wide Web Consortium,
* (Massachusetts Institute of Technology, Institut National de
* Recherche en Informatique et en Automatique, Keio University). All
* Rights Reserved. This program is distributed under the W3C's Software
* Intellectual Property License. This program is distributed in the
* hope that it will be useful, but WITHOUT ANY WARRANTY; without even
* the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
* PURPOSE.
* See W3C License http://www.w3.org/Consortium/Legal/ for more details.
*/
namespace TidyNet.Dom
{
	/// <summary> This interface represents a notation declared in the DTD. A notation either 
	/// declares, by name, the format of an unparsed entity (see section 4.7 of 
	/// the XML 1.0 specification ), or is used for formal declaration of 
	/// processing instruction targets (see section 2.6 of the XML 1.0 
	/// specification ). The <code>nodeName</code> attribute inherited from 
	/// <code>Node</code> is set to the declared name of the notation.
	/// <br/>The DOM Level 1 does not support editing <code>Notation</code> nodes; 
	/// they are therefore readonly.
	/// <br/>A <code>Notation</code> node does not have any parent.
	/// <br/>See also the <a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-Core-20001113'>Document Object Model (DOM) Level 2 Core Specification</a>.
	/// </summary>
	internal interface INotation : INode
	{
		/// <summary> The public identifier of this notation. If the public identifier was 
		/// not specified, this is <code>null</code>.
		/// </summary>
		string PublicId
		{
			get;
		}

		/// <summary> The system identifier of this notation. If the system identifier was 
		/// not specified, this is <code>null</code>.
		/// </summary>
		string SystemId
		{
			get;
		}
	}
}