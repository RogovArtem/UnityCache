using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace TestScripts {
	public class AttributeCache : MonoBehaviour {
		public int Tries = 1000;

		[Cached]
		public TestComponent Test = null;

		void Update() {
			DirectLoop();
			ReflectedLoop();
			CachedLoop();
		}

		void DirectLoop() {
			GC.Collect();
			Profiler.BeginSample("Init Cache Directly");
			for ( int i = 0; i < Tries; i++ ) {
				Direct();
			}
			Profiler.EndSample();
		}

		void ReflectedLoop() {
			GC.Collect();
			Profiler.BeginSample("Init Static Attribute Cache by Reflection (first time)");
			for ( int i = 0; i < Tries; i++ ) {
				Reflected();
			}
			Profiler.EndSample();
		}

		void CachedLoop() {
			GC.Collect();
			Profiler.BeginSample("Init Static Attribute Cache with known fields (next times)");
			for ( int i = 0; i < Tries; i++ ) {
				Cached();
			}
			Profiler.EndSample();
		}

		void Direct() {
			Test = GetComponent<TestComponent>();
		}

		void Reflected() {
			CacheHelper.CacheAll(this, false);
		}

		void Cached() {
			CacheHelper.CacheAll(this, true);
		}
	}
}