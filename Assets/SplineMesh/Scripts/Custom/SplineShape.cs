﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SplineMesh {
    [ExecuteInEditMode]
    [RequireComponent(typeof(Spline))]
    public class SplineShape : MonoBehaviour
    {
        private Spline spline;
        private bool toUpdate = true;
        private GameObject generated;

        //public List<ExtrusionSegment.Vertex> shapeVertices = new List<ExtrusionSegment.Vertex>();
        public Material material;
        //public float textureScale = 1;
        public float sampleSpacing = 0.1f;

        private void OnValidate()
        {
            toUpdate = true;
        }

        private void OnEnable()
        {
            string generatedName = "generated by " + GetType().Name;
            var generatedTranform = transform.Find(generatedName);
            generated = generatedTranform != null ? generatedTranform.gameObject : UOUtility.Create(generatedName, gameObject, 
                typeof(MeshFilter), 
                typeof(MeshRenderer),
                typeof(MeshCollider));

            spline = GetComponentInParent<Spline>();
            spline.NodeListChanged += (s, e) => toUpdate = true;
        }

        private void Update()
        {
            if (toUpdate)
            {
                GenerateMesh();
                toUpdate = false;
            }
        }

        private void GenerateMesh()
        {
            float textureOffset = 0.0f;
            generated.GetComponent<MeshRenderer>().material = material;
            ExtrusionSegment mb = generated.GetComponent<ExtrusionSegment>();
            //mb.ShapeVertices = shapeVertices;
            //mb.TextureScale = textureScale;
            //mb.TextureOffset = textureOffset;
            mb.SampleSpacing = sampleSpacing;
            //mb.SetInterval(curve);

            //textureOffset += curve.Length;
        }
    }
}
