﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MocoChan.Converter.Data
{
    public class Polygon
    {
		private Asset model;

        public int Point1Id;
		public Vertex Point1Vertex
		{
			get {
				return model.Vertices[Point1Id];
			}
		}

        public int Point2Id;
		public Vertex Point2Vertex
		{
			get
			{
				return model.Vertices[Point2Id];
			}
		}

        public int Point3Id;
		public Vertex Point3Vertex
		{
			get
			{
				return model.Vertices[Point3Id];
			}
		}

        public string materialId;

		public Polygon(Asset model)
		{
			this.model = model;
		}
    }
}
