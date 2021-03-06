﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MocoChan.Converter;

namespace MocoChan.WinForms.Options
{
	public class ExtensionConverter : StringConverter
	{
		public static MocoConverter converter;

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(converter.extensions.Keys);
		}
	}
}
