﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using ModelConverter.Model;

namespace ModelConverter.Plugin.Collada.ColladaLib
{
	public class ColladaWriter
	{
		CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

		public ColladaWriter()
		{
		}

		public void Write(BaseModel model, string path)
		{
			XmlTextWriter doc = new XmlTextWriter(path, Encoding.UTF8);

			doc.WriteStartDocument();

			doc.WriteStartElement("COLLADA");
			doc.WriteAttributeString("version", "1.4.0");
			doc.WriteAttributeString("xmlns", "http://www.collada.org/2005/11/COLLADASchema");

			WriteAsset(doc);
			WriteGeometrieLibrary(doc, model);
			

			doc.WriteEndElement();
			doc.WriteEndDocument();

			doc.Close();
		}

		private void WriteGeometrieLibrary(XmlTextWriter doc, BaseModel model)
		{
			doc.WriteStartElement("library_geometries");
			doc.WriteStartElement("geometry");
			doc.WriteStartElement("mesh");

			#region Write Positions
			doc.WriteStartElement("source");
			doc.WriteAttributeString("id", "model-positions");

			doc.WriteStartElement("float_array");
			doc.WriteAttributeString("id", "model-positions-array");
			doc.WriteAttributeString("count", (model.Vertices.Count * 3).ToString());

			foreach (Vertex vertex in model.Vertices)
			{
				doc.WriteString(vertex.Coordinate.X.ToString("0.000000", culture) + " ");
				doc.WriteString(vertex.Coordinate.Y.ToString("0.000000", culture) + " ");
				doc.WriteString(vertex.Coordinate.Z.ToString("0.000000", culture) + " ");
			}

			doc.WriteEndElement();  // float_array
			
			doc.WriteStartElement("technique_common");
			doc.WriteStartElement("accessor");
			doc.WriteAttributeString("count", model.Vertices.Count.ToString());
			doc.WriteAttributeString("source", "#model-positions-array");
			doc.WriteAttributeString("stride", "3");
			
			doc.WriteStartElement("param");
			doc.WriteAttributeString("meter", "X");
			doc.WriteAttributeString("type", "float");
			doc.WriteEndElement();

			doc.WriteStartElement("param");
			doc.WriteAttributeString("meter", "Y");
			doc.WriteAttributeString("type", "float");
			doc.WriteEndElement();
			
			doc.WriteStartElement("param");
			doc.WriteAttributeString("meter", "Z");
			doc.WriteAttributeString("type", "float");
			doc.WriteEndElement();

			doc.WriteEndElement();  // accessor
			doc.WriteEndElement();  // technique_common

			doc.WriteEndElement(); // source
			#endregion

			#region Write Normals
			doc.WriteStartElement("source");
			doc.WriteAttributeString("id", "model-normals");

			doc.WriteStartElement("float_array");
			doc.WriteAttributeString("id", "model-normals-array");
			doc.WriteAttributeString("count", (model.Vertices.Count * 3).ToString());

			foreach (Vertex vertex in model.Vertices)
			{
				doc.WriteString(vertex.Normals.X.ToString("0.000000", culture) + " ");
				doc.WriteString(vertex.Normals.Y.ToString("0.000000", culture) + " ");
				doc.WriteString(vertex.Normals.Z.ToString("0.000000", culture) + " ");
			}

			doc.WriteEndElement();  // float_array

			doc.WriteStartElement("technique_common");
			doc.WriteStartElement("accessor");
			doc.WriteAttributeString("count", model.Vertices.Count.ToString());
			doc.WriteAttributeString("source", "#model-normals-array");
			doc.WriteAttributeString("stride", "3");

			doc.WriteStartElement("param");
			doc.WriteAttributeString("meter", "X");
			doc.WriteAttributeString("type", "float");
			doc.WriteEndElement();

			doc.WriteStartElement("param");
			doc.WriteAttributeString("meter", "Y");
			doc.WriteAttributeString("type", "float");
			doc.WriteEndElement();

			doc.WriteStartElement("param");
			doc.WriteAttributeString("meter", "Z");
			doc.WriteAttributeString("type", "float");
			doc.WriteEndElement();

			doc.WriteEndElement();  // accessor
			doc.WriteEndElement();  // technique_common

			doc.WriteEndElement(); // source
			#endregion

			#region Write UV
			doc.WriteStartElement("source");
			doc.WriteAttributeString("id", "model-uv");

			doc.WriteStartElement("float_array");
			doc.WriteAttributeString("id", "model-uv-array");
			doc.WriteAttributeString("count", (model.Vertices.Count * 3).ToString());

			foreach (Vertex vertex in model.Vertices)
			{
				doc.WriteString(vertex.TextureCoordinate.X.ToString("0.000000", culture) + " ");
				doc.WriteString(vertex.TextureCoordinate.Y.ToString("0.000000", culture) + " ");
			}

			doc.WriteEndElement();  // float_array

			doc.WriteStartElement("technique_common");
			doc.WriteStartElement("accessor");
			doc.WriteAttributeString("count", model.Vertices.Count.ToString());
			doc.WriteAttributeString("source", "#model-uv-array");
			doc.WriteAttributeString("stride", "3");

			doc.WriteStartElement("param");
			doc.WriteAttributeString("meter", "S");
			doc.WriteAttributeString("type", "float");
			doc.WriteEndElement();

			doc.WriteStartElement("param");
			doc.WriteAttributeString("meter", "T");
			doc.WriteAttributeString("type", "float");
			doc.WriteEndElement();

			doc.WriteEndElement(); // source
			#endregion

			#region write vertices node
			doc.WriteStartElement("vertices");
			doc.WriteAttributeString("id", "model-vertices");
			
			doc.WriteStartElement("input");
			doc.WriteAttributeString("semantic", "POSITION");
			doc.WriteAttributeString("source", "#model-positions");
			doc.WriteEndElement();

			doc.WriteEndElement();
			#endregion

			#region write polylist node
			doc.WriteStartElement("polylist");
			doc.WriteAttributeString("count", "");
			doc.WriteAttributeString("material", "");
			
			doc.WriteStartElement("input");
			doc.WriteAttributeString("offset", "0");
			doc.WriteAttributeString("semantic", "VERTEX");
			doc.WriteAttributeString("source", "#model-vertices");
			doc.WriteEndElement();
			
			doc.WriteStartElement("input");
			doc.WriteAttributeString("offset", "1");
			doc.WriteAttributeString("semantic", "NORMAL");
			doc.WriteAttributeString("source", "#model-normal");
			doc.WriteEndElement();

			doc.WriteStartElement("input");
			doc.WriteAttributeString("offset", "2");
			doc.WriteAttributeString("semantic", "TEXTCOORD");
			doc.WriteAttributeString("source", "#model-uv");
			doc.WriteEndElement();

			doc.WriteStartElement("vcount");
			for (int i = 0; i < model.Vertices.Count; i++)
			{
				doc.WriteString("3 ");
			}
			doc.WriteEndElement();

			doc.WriteStartElement("p");
			for (int i = 0; i < model.Vertices.Count; i++)
			{
				doc.WriteString(i.ToString() + " " + i.ToString() + " " + i.ToString() + " ");
			}
			doc.WriteEndElement();



			doc.WriteEndElement();
			#endregion

			doc.WriteEndElement(); // mesh
			doc.WriteEndElement(); // geometry
			doc.WriteEndElement(); // library_geometries
			doc.Flush();
		}

		private void WriteAsset(XmlTextWriter doc)
		{
			doc.WriteStartElement("asset");
			
			doc.WriteStartElement("contributor");
			
			doc.WriteElementString("author", "ModelConverter COLLADA Plugin");
			// TODO: write the correct version here
			doc.WriteElementString("authoring_tool", "ModelConverter x.x.x.x; COLLADA Plugin x.x.x.x");
			doc.WriteElementString("comments", null);
			doc.WriteElementString("copyright", null);
			doc.WriteElementString("source_data", null);

			doc.WriteEndElement();
				
			// TODO: write actual time here
			doc.WriteElementString("created", "1970-01-01T00:00:00");
			doc.WriteElementString("modified", "1970-01-01T00:00:00");
			
			doc.WriteStartElement("contributor");
			doc.WriteAttributeString("meter", "0.01");
			doc.WriteAttributeString("name", "centimeter");
			doc.WriteEndElement();
				
			doc.WriteElementString("up_axis", "Y_UP");

			doc.WriteEndElement();
			doc.Flush();
		}
	}
}